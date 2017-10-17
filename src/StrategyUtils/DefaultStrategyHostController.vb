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

Imports StudyUtils27
Imports TickerUtils27
Imports TickfileUtils27
Imports TickUtils27
Imports TimeframeUtils27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Contracts
Imports TradeWright.Trading.Utils.Sessions

Public NotInheritable Class DefaultStrategyHostController
    Implements IGenericTickListener
    Implements _IStateChangeListener
    Implements IStrategyHostController


#Region "Interfaces"


#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

    Private Structure StudyConfigToShow
        Dim Timeframe As TimeframeUtils27.Timeframe
        Dim Study As _IStudy
        Dim StudyValueNames As Dictionary(Of String, Object)

        Friend Sub New(Study As _IStudy, Timeframe As TimeframeUtils27.Timeframe)
            StudyValueNames = New Dictionary(Of String, Object)
            Me.Study = Study
            Me.Timeframe = Timeframe
        End Sub
    End Structure

#End Region

#Region "Constants"

    Private Const ModuleName As String = "DefaultStrategyHostController"

#End Region

#Region "Member variables"

    Private mModel As IStrategyHostModel
    Private mView As IStrategyHostView

    Private mStrategyRunner As StrategyRunner

    Private mTimeframeIndexes As New Dictionary(Of String, Integer)

    Private mNumberOfTimeframesLoading As Integer

    Private mReplayStartTime As Date

    Private mPriceChartTimePeriod As TimePeriod

    Private mStudiesToShow As New List(Of StudyConfigToShow)

    Private mTotalElapsedSecs As Double
    Private mElapsedSecsCurrTickfile As Double
    Private mTotalEvents As Integer
    Private mEventsCurrTickfile As Integer

    Private mTradeBarNumber As Integer

#End Region

#Region "Constructors"

#Region "IGenericTickListener Interface Members"

    Private Sub IGenericTickListener_NoMoreTicks(ByRef ev As GenericTickEventData) Implements IGenericTickListener.NoMoreTicks

    End Sub

    Private Sub IGenericTickListener_NotifyTick(ByRef ev As GenericTickEventData) Implements IGenericTickListener.NotifyTick
        mView.NotifyTick(ev)
    End Sub

#End Region

#Region "IStateChangeListener Interface Members"

    Private Sub IStateChangeListener_Change(ByRef ev As StateChangeEventData) Implements _IStateChangeListener.Change
        Try
            If ev.State <> TimeframeStates.TimeframeStateLoaded Then Exit Sub

            Dim lTimeframe = DirectCast(ev.Source, TimeframeUtils27.Timeframe)
            mNumberOfTimeframesLoading = mNumberOfTimeframesLoading - 1

            Dim lChartIndex As Integer
            If mModel.ShowChart Then
                lChartIndex = mTimeframeIndexes.Item(lTimeframe.TimePeriod.ToString)
                addStudiesForChart(lTimeframe)
            End If

            If mNumberOfTimeframesLoading = 0 Then
                mView.EnablePriceDrawing()
                If mModel.IsTickReplay Then mView.DisablePriceDrawing()

                mModel.Ticker.AddGenericTickListener(Me)
                mView.NotifyTradingStart()

                If mModel.IsTickReplay Then
                    mStrategyRunner.StartReplay()
                    mReplayStartTime = Date.Now
                Else
                    mStrategyRunner.StartLiveData()
                End If
            End If
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(IStateChangeListener_Change), NameOf(DefaultStrategyHostController))
        End Try
    End Sub

#End Region

#Region "IStrategyHostController Interface Members"

    Private Sub IStrategyHostController_AddTimeframe(pTimeframe As TimeframeUtils27.Timeframe) Implements IStrategyHostController.AddTimeframe
        monitorTimeframe(pTimeframe)

        If Not mModel.ShowChart Then Exit Sub
        If mTimeframeIndexes.ContainsKey(pTimeframe.TimePeriod.ToString) Then Exit Sub

        Dim lIndex = mView.AddTimeframe(pTimeframe)

        mTimeframeIndexes.Add(pTimeframe.TimePeriod.ToString, lIndex)

        mView.DisablePriceDrawing(lIndex)

        If mPriceChartTimePeriod Is Nothing Then mPriceChartTimePeriod = pTimeframe.TimePeriod
    End Sub

    Private Sub IStrategyHostController_ChartStudyValue(pStudy As _IStudy,
                                                        pValueName As String,
                                                        pTimeframe As TimeframeUtils27.Timeframe) Implements IStrategyHostController.ChartStudyValue
        If Not mModel.ShowChart Then Exit Sub

        Dim studyToShow = findStudyConfigToShow(pStudy, pTimeframe)
        If Not studyToShow.StudyValueNames.ContainsKey(pValueName) Then
            studyToShow.StudyValueNames.Add(pValueName, Nothing)
        End If
    End Sub

    Private Sub IStrategyHostController_ContractInvalid(pMessage As String) Implements IStrategyHostController.ContractInvalid
        mView.NotifyError("Invalid contract", pMessage, IStrategyHostView.ErrorSeverities.ErrorSeverityCritical)
        mView.EnableStart()
    End Sub

    Private Sub IStrategyHostController_Finish() Implements IStrategyHostController.Finish
        StopReplay()
        mModel = Nothing
        mView = Nothing
    End Sub

    Private Function IStrategyHostController_GetDefaultParameters(pStrategy As IStrategy, pPositionManagementStrategyFactories As List(Of IPositionManagementStrategyFactory)) As Parameters Implements IStrategyHostController.GetDefaultParameters
        IStrategyHostController_GetDefaultParameters = mStrategyRunner.GetDefaultParameters(pStrategy, pPositionManagementStrategyFactories)
    End Function

    Private Sub IStrategyHostController_Initialise(pStrategyRunner As StrategyRunner, pModel As IStrategyHostModel, pView As IStrategyHostView) Implements IStrategyHostController.Initialise
        mStrategyRunner = pStrategyRunner
        mModel = pModel
        mView = pView
    End Sub

    Private Sub IStrategyHostController_NotifyBracketOrderProfile(Value As OrderUtils27.BracketOrderProfile) Implements IStrategyHostController.NotifyBracketOrderProfile
        mView.NotifyBracketOrderProfile(Value)
        showBracketOrderLine(Value)
    End Sub

    Private Sub IStrategyHostController_NotifyPosition(pPosition As Integer) Implements IStrategyHostController.NotifyPosition
        Static sPrevPosition As Integer

        mView.NotifyPosition(pPosition)

        If (pPosition <> 0 And sPrevPosition = 0) Or (pPosition > 0 And sPrevPosition < 0) Or (pPosition < 0 And sPrevPosition > 0) Then
            If mModel.IsTickReplay Then
                mView.EnableTradeDrawing()
                mView.DisableTradeDrawing()
            End If
            mTradeBarNumber = mTradeBarNumber + 1
            If mModel.ShowChart Then
                Logger.Log(LogLevels.LogLevelNormal, $"New trade bar: {mTradeBarNumber} at {mModel.Ticker.Timestamp}")
                mView.NotifyNewTradeBar(mTradeBarNumber, mModel.Ticker.Timestamp)
            End If
        End If
        sPrevPosition = pPosition
    End Sub

    Private Sub IStrategyHostController_NotifyReplayCompleted() Implements IStrategyHostController.NotifyReplayCompleted
        mModel.Ticker.RemoveGenericTickListener(Me)
        mModel.Ticker.Finish()

        mTotalElapsedSecs = mTotalElapsedSecs + mElapsedSecsCurrTickfile
        mElapsedSecsCurrTickfile = 0

        mTotalEvents = mTotalEvents + mEventsCurrTickfile
        mEventsCurrTickfile = 0

        If mModel.ShowChart Then
            If mModel.IsTickReplay Then
                ' ensure final bars in charts are displayed
                mView.UpdateLastChartBars()
            End If
            mView.EnablePriceDrawing()
            mView.EnableProfitDrawing()
            mView.EnableTradeDrawing()
        End If

        mView.EnableStart()
    End Sub

    Private Sub IStrategyHostController_NotifyReplayEvent(ByRef ev As NotificationEventData) Implements IStrategyHostController.NotifyReplayEvent
        Dim lMessage As String

        Dim lEventCode = CType(ev.EventCode, TickfileEventCodes)
        Select Case lEventCode
            Case TickfileEventCodes.TickfileEventFileDoesNotExist
                lMessage = "Tickfile does not exist"
            Case TickfileEventCodes.TickfileEventFileIsEmpty
                lMessage = "Tickfile is empty"
            Case TickfileEventCodes.TickfileEventFileIsInvalid
                lMessage = "Tickfile is invalid"
            Case TickfileEventCodes.TickfileEventFileFormatNotSupported
                lMessage = "Tickfile format is not supported"
            Case TickfileEventCodes.TickfileEventNoContractDetails
                lMessage = "No contract details are available for this tickfile"
            Case TickfileEventCodes.TickfileEventDataSourceNotAvailable
                lMessage = "Tickfile data source is not available"
            Case TickfileEventCodes.TickfileEventAmbiguousContractDetails
                lMessage = "A unique contract for this tickfile cannot be determined"
            Case Else
                lMessage = "An unspecified error has occurred"
        End Select

        If ev.EventMessage <> "" Then lMessage = lMessage & ev.EventMessage

        mView.NotifyError("Tickfile problem", lMessage, IStrategyHostView.ErrorSeverities.ErrorSeverityCritical)
        mView.EnableStart()

        mStrategyRunner.StopTesting()
    End Sub

    Private Sub IStrategyHostController_NotifyReplayProgress(pTickfileTimestamp As Date, pEventsPlayed As Integer, pPercentComplete As Single) Implements IStrategyHostController.NotifyReplayProgress
        mView.NotifyReplayProgress(pTickfileTimestamp, pEventsPlayed, pPercentComplete)
        mEventsCurrTickfile = pEventsPlayed

        Dim lTotalEvents = mTotalEvents + mEventsCurrTickfile

        mElapsedSecsCurrTickfile = (Date.Now - mReplayStartTime).TotalSeconds
        Dim lTotalElapsedSecs = mTotalElapsedSecs + mElapsedSecsCurrTickfile

        If lTotalEvents <> 0 Then
            mView.NotifyEventsPlayed(lTotalEvents)
            mView.NotifyEventsPerSecond(CInt(lTotalEvents / lTotalElapsedSecs))
            mView.NotifyMicrosecsPerEvent(CInt(lTotalElapsedSecs * 1000000 / lTotalEvents))
        End If

        If mModel.ShowChart Then
            mView.EnablePriceDrawing()
            mView.DisablePriceDrawing()
        End If
    End Sub

    Private Sub IStrategyHostController_NotifyReplayStarted() Implements IStrategyHostController.NotifyReplayStarted
        If mModel.IsTickReplay Then mView.DisablePriceDrawing()
        mView.NotifyReplayStarted()
    End Sub

    Private Sub IStrategyHostController_NotifyTickfileCompleted(ByVal pTickfile As TickfileSpecifier, ByVal pEventsPlayed As Integer) Implements IStrategyHostController.NotifyTickfileCompleted
        mView.NotifyTickfileCompleted(pTickfile, pEventsPlayed)
    End Sub

    Private Sub IStrategyHostController_StartLiveProcessing(pSymbol As IContractSpecifier) Implements IStrategyHostController.StartLiveProcessing
        prepare()
        mStrategyRunner.PrepareSymbol(pSymbol)
    End Sub

    Private Sub IStrategyHostController_StartTickfileReplay(pTickFileSpecifiers As TickFileSpecifiers) Implements IStrategyHostController.StartTickfileReplay
        prepare()
        mModel.TickFileSpecifiers = pTickFileSpecifiers
        mStrategyRunner.PrepareTickFile(mModel.TickFileSpecifiers)
    End Sub

    Private Sub IStrategyHostController_StopTickfileReplay() Implements IStrategyHostController.StopTickfileReplay
        StopReplay()
    End Sub

    Private Sub IStrategyHostController_TickerCreated(pTicker As Ticker, pContract As IContract, pSession As Session, pClock As Clock) Implements IStrategyHostController.TickerCreated
        resetView()

        mModel.Ticker = pTicker
        mModel.Clock = pClock
        mModel.Contract = pContract
        mModel.Session = pSession
        mModel.IsTickReplay = pTicker.IsTickReplay
        mView.NotifyTickerCreated()

        mStrategyRunner.StartStrategy(mView.Strategy, mView.Parameters)
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

#End Region

#Region "Helper Functions"

    Private Sub addStudiesForChart(pTimeframe As TimeframeUtils27.Timeframe)
        For Each studyToShow In mStudiesToShow
            With studyToShow
                If .Timeframe Is pTimeframe Then
                    mView.AddStudyToChart(mTimeframeIndexes.Item(.Timeframe.TimePeriod.ToString), .Study, .StudyValueNames)
                End If
            End With
        Next
    End Sub

    Private Function findStudyConfigToShow(pStudy As _IStudy, pTimeframe As TimeframeUtils27.Timeframe) As StudyConfigToShow
        For Each s In mStudiesToShow
            If s.Study Is pStudy And s.Timeframe Is pTimeframe Then Return s
        Next

        Dim studyToSHow = New StudyConfigToShow(pStudy, pTimeframe)
        mStudiesToShow.Add(studyToSHow)
        Return studyToSHow
    End Function

    Private Sub monitorTimeframe(pTimeframe As TimeframeUtils27.Timeframe)
        If pTimeframe.State = TimeframeStates.TimeframeStateLoading Then
            mNumberOfTimeframesLoading = mNumberOfTimeframesLoading + 1
            pTimeframe.AddStateChangeListener(Me)
        End If
    End Sub

    Private Sub prepare()
        resetView()

        mTradeBarNumber = 0

        mStudiesToShow.Clear()
        mTimeframeIndexes.Clear()
    End Sub

    Private Sub resetView()
        mView.DisableStart()
        mView.ClearPriceAndProfitFields()
        mView.ResetBracketOrderList()

        If mModel.ShowChart Then
            mView.ResetPriceChart()
            mView.ResetProfitChart()
            mView.ResetTradeChart()
            mView.DisablePriceDrawing()
        End If
    End Sub

    Private Sub showBracketOrderLine(ByRef pBracketOrderProfile As OrderUtils27.BracketOrderProfile)
        If Not mModel.ShowChart Then Exit Sub

        Dim lLineStartTime As Date
        lLineStartTime = BarUtils.BarStartTime(pBracketOrderProfile.StartTime, mPriceChartTimePeriod, StartOfDayAsDate + mModel.Contract.SessionStartTime)

        Dim lLineEndTime As Date
        lLineEndTime = BarUtils.BarStartTime(pBracketOrderProfile.EndTime, mPriceChartTimePeriod, StartOfDayAsDate + mModel.Contract.SessionStartTime)

        mView.ShowTradeLine(lLineStartTime, lLineEndTime, pBracketOrderProfile.EntryPrice, pBracketOrderProfile.ExitPrice, pBracketOrderProfile.Profit)
    End Sub

    Private Sub StopReplay()
        If Not mStrategyRunner Is Nothing Then
            Logger.Log(LogLevels.LogLevelNormal, "Stopping strategy host")
            mStrategyRunner.StopTesting()
        End If
    End Sub

#End Region

#End Region

End Class