#Region "License"

' The MIT License (MIT)
'
' Copyright (c) 2017 Richard L King (TradeWright Software Systems)
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.

#End Region

Imports System.Collections.Generic

Imports OrderUtils27
Imports StudyUtils27
Imports TickerUtils27
Imports TimeframeUtils27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Contracts
Imports TradeWright.Trading.Utils.Sessions

Public NotInheritable Class InitialisationContext
    Implements IStateChangeListener
    Implements ITaskCompletionListener

#Region "Interfaces"


#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "InitialisationContext"

#End Region

#Region "Member variables"

    Private mStrategyHost As IStrategyHost

    Private mTicker As Ticker
    Private mContract As IContract
    Private mSession As Session
    Private mClock As Clock
    Private mInitialTimestamp As Date

    Private mPositionManager As PositionManager
    Private mPositionManagerSimulated As PositionManager

    Private mStudyManager As StudyManager
    Private mTimeframes As Timeframes

    Private mStrategyRunner As StrategyRunner

    Private mBarsOutstanding As Integer
    Private mStudiesOutstanding As Integer

    Private mAllowUnprotectedPositions As Boolean

    Private mInitialising As Boolean = True

#End Region

#Region "Constructors"

    Private Sub New()
    End Sub

    Friend Sub New(pStrategyHost As IStrategyHost,
                   pStrategyRunner As StrategyRunner,
                   pTicker As Ticker,
                   pContract As IContract,
                   pSession As Session,
                   pClock As Clock)
        mStrategyHost = pStrategyHost

        mStrategyRunner = pStrategyRunner
        mTicker = pTicker
        mTimeframes = mTicker.Timeframes
        mStudyManager = mTicker.StudyBase.StudyManager
        mPositionManager = mTicker.PositionManager
        mPositionManagerSimulated = mTicker.PositionManagerSimulated

        mContract = pContract
        mSession = pSession
        mClock = pClock

        If mTicker.IsTickReplay Then mInitialTimestamp = mClock.Timestamp

        Exit Sub
    End Sub

#End Region

#Region "IStateChangeListener Interface Members"

    Private Sub IStateChangeListener_Change(ByRef ev As StateChangeEventData) Implements IStateChangeListener.Change
        Try
            Dim lState = CType(ev.State, TimeframeStates)
            If lState <> TimeframeStates.TimeframeStateLoaded Then Exit Sub

            mBarsOutstanding -= 1
            If mBarsOutstanding = 0 And mStudiesOutstanding = 0 Then
                mStrategyRunner.InitialisationCompleted()
                mStrategyRunner = Nothing
                mInitialising = False
            End If

            DirectCast(ev.Source, TimeframeUtils27.Timeframe).RemoveStateChangeListener(Me)
        Catch ex As Exception
            ex.Source = ex.StackTrace
            Throw
        End Try
    End Sub

#End Region

#Region "ITaskCompletionListener Interface Members"

    Private Sub ITaskCompletionListener_TaskCompleted(ByRef ev As TaskCompletionEventData) Implements ITaskCompletionListener.TaskCompleted
        mStudiesOutstanding -= 1
        If mBarsOutstanding = 0 And mStudiesOutstanding = 0 Then
            mStrategyRunner.InitialisationCompleted()
            mStrategyRunner = Nothing
            mInitialising = False
        End If
    End Sub

#End Region

#Region "Properties"


    Public Property AllowUnprotectedPositions() As Boolean
        Get
            Return mAllowUnprotectedPositions
        End Get
        Set
            mAllowUnprotectedPositions = Value
        End Set
    End Property

    Public ReadOnly Property Contract() As IContract
        Get
            Return mContract
        End Get
    End Property

    Public ReadOnly Property IsTickReplay() As Boolean
        Get
            Return mTicker.IsTickReplay
        End Get
    End Property

    Public ReadOnly Property PositionManager() As PositionManager
        Get
            Return mPositionManager
        End Get
    End Property

    Public ReadOnly Property PositionManagerSimulated() As PositionManager
        Get
            Return mPositionManagerSimulated
        End Get
    End Property

    Public ReadOnly Property Session() As Session
        Get
            Return mSession
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Creates a <c>IStudy</c> object, taking its input from a specified existing
    ''' <c>IStudy</c> object.
    ''' 
    '''  This method can only be called during the <c>Strategy</c> object's
    '''   <c>Initialise</c> method. Calling it elsewhere results in an
    '''   <c>InvalidOperationException</c>. If a <c>Strategy</c>
    '''   object needs to create studies at other times, it must use the
    '''   <c>Ticker.StudyManager.AddStudy</c> method.
    '''
    '''   Note that the <c>Strategy</c> object's <c>Start</c> method
    '''   is not called until all studies created using this method have been fully initialised,
    '''   and all timeframes created using the <c>addTimeframe</c> method have had their
    '''   historical data fully loaded.
    ''' </summary>
    ''' 
    ''' <param name="pName">The name by which the required study is identified in the relevant study library.</param>
    ''' 
    ''' <param name="pUnderlyingStudy">The existing study which is to provide input to the new study.</param>
    ''' 
    ''' <param name="pInputValueNames">An array containing the names of the output values from the underlying study that
    ''' will be used as input to the new study.</param>
    ''' 
    ''' <param name="pIncludeDataOutsideSession">Indicates whether data from outside the session is to be passed to the study.</param>
    ''' 
    ''' <param name="pParams">The parameters to be passed to the new study. If this argument is <c>Nothing</c>,
    ''' the default parameters defined in the study definition will be used.</param>
    ''' 
    ''' <param name="pNumberOfValuesToCache">The number of past values that the study should retain. If this argument is omitted, all
    ''' past values are retained.</param>
    ''' 
    ''' <param name="pLibraryName">The name of the study library from which the new <c>Study</c> object is to be
    ''' created. If this is not supplied or is blank, the new <c>Study</c> object is
    ''' created from the first study library configured into TradeBuild that can create
    ''' studies with the required name.</param>
    ''' 
    ''' <returns>The new study.</returns>
    Public Function AddStudy(pName As String, pUnderlyingStudy As _IStudy, ByRef pInputValueNames() As String, pIncludeDataOutsideSession As Boolean, Optional pParams As Parameters = Nothing, Optional pNumberOfValuesToCache As Integer = 0, Optional pLibraryName As String = "") As _IStudy
        If Not mInitialising Then Throw New InvalidOperationException("Initialisation already completed")

        AddStudy = mStudyManager.AddStudy(pName, pUnderlyingStudy, pInputValueNames, pIncludeDataOutsideSession, pParams, pLibraryName, pNumberOfValuesToCache)

        Dim lTC = mStudyManager.StartStudy(AddStudy, 0)
        If lTC IsNot Nothing Then
            lTC.AddTaskCompletionListener(Me)
            mStudiesOutstanding += 1
        End If
    End Function

    ''' <summary>Adds a timeframe of the specified bar length to the underlying ticker, and returns
    ''' it.
    '''
    '''   This method can only be called during the <c>IStrategy</c> object's
    '''   <c>Initialise</c> method. Calling it elsewhere results in an
    '''   <c>InvalidOperationException</c>. If a <c>Strategy</c>
    '''   object needs to create timeframes at other times, it must use the <c>Ticker.Timeframes.Add</c>
    '''   method.
    '''
    '''   Note that the <c>Strategy</c> object's <c>Start</c> method
    '''   is not called until all timeframes created using this method have had their
    '''   historical data fully loaded, and all studies created using the <c>addStudy</c>
    '''   method have been fully initialised.
    ''' </summary>
    ''' 
    ''' <returns>
    '''   The created <c>Timeframe</c> object.
    ''' </returns>
    ''' 
    ''' <param name="pTimePeriod">
    '''   The time period for this timeframe.
    ''' </param>
    ''' 
    ''' <param name="pNumberOfBarsToFetch">
    '''   The number of bars of historical data to be loaded into this timeframe for
    '''   initialising studies.
    ''' </param>
    ''' 
    ''' <param name="pIncludeBarsOutsideSession">
    '''   If set to <c>True</c>, then bars that occur outside the session times
    '''   defined in the contract for the underlying ticker will be included in the
    '''   retrieved historical data.
    ''' </param>
    '''
    Public Function AddTimeframe(pTimePeriod As TimePeriod, pNumberOfBarsToFetch As Integer, Optional pIncludeBarsOutsideSession As Boolean = False, Optional pShowInChart As Boolean = False) As TimeframeUtils27.Timeframe
        If Not mInitialising Then Throw New InvalidOperationException("Initialisation already completed")

        Dim lTimeframe As TimeframeUtils27.Timeframe
        If mTicker.IsTickReplay Then
            lTimeframe = mTimeframes.AddHistorical(pTimePeriod, "", pNumberOfBarsToFetch,  , BarUtils.BarStartTime(mInitialTimestamp, pTimePeriod, StartOfDayAsDate + mContract.SessionStartTime),  , pIncludeBarsOutsideSession)
        Else
            lTimeframe = mTimeframes.Add(pTimePeriod, "", pNumberOfBarsToFetch,  ,  , pIncludeBarsOutsideSession)
        End If

        Static sTimeframes As New Dictionary(Of String, TimeframeUtils27.Timeframe)
        If Not sTimeframes.ContainsKey(lTimeframe.Key) Then
            sTimeframes.Add(lTimeframe.Key, lTimeframe)
            lTimeframe.AddStateChangeListener(Me)
            mBarsOutstanding += 1

            If pShowInChart Then mStrategyHost.AddTimeframe(lTimeframe)
        End If

        Return lTimeframe
    End Function

    ''' <summary>
    '''   Displays an output value from a study on the chart.
    '''
    '''   This method can only be called during the <c>Strategy</c> object's
    '''   <c>Initialise</c> method. Calling it elsewhere results in an
    '''   <c>InvalidOperationException</c>.
    ''' </summary>
    ''' 
    ''' <param name="pStudy">
    '''   The existing study whose value is to be displayed.
    ''' </param>
    ''' 
    ''' <param name ="pValueName">
    '''   The names of the study value to be displayed on the chart.
    ''' </param>
    ''' 
    ''' <param name="pTimeframe">
    '''   The <c>Timeframe</c> that identifies the chart on which the study value is to be displayed.
    '''   If this is not supplied or is <c>Nothing</c>, the value is displayed
    '''   on the chart containing the first created timeframe.
    ''' </param>

    Public Sub ChartStudyValue(pStudy As _IStudy, pValueName As String, Optional pTimeframe As TimeframeUtils27.Timeframe = Nothing)
        If Not mInitialising Then Throw New InvalidOperationException("Initialisation already completed")

        mStrategyHost.ChartStudyValue(pStudy, pValueName, pTimeframe)
    End Sub

#End Region

#Region "Helper Functions"

#End Region

End Class