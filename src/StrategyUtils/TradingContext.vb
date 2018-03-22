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
Imports System.Runtime.InteropServices

Imports MarketDataUtils27
Imports OrderUtils27
Imports StudyUtils27
Imports TickerUtils27
Imports TickUtils27
Imports TimeframeUtils27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Contracts
Imports TradeWright.Trading.Utils.Sessions

Public NotInheritable Class TradingContext
    Implements IGenericTickListener

#Region "Interfaces"


#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "TradingContext"

#End Region

#Region "Member variables"

    Private mStrategyRunner As StrategyRunner

    Private mTicker As Ticker
    Private mContract As IContract
    Private mSession As Session

    Private mPositionManager As PositionManager
    Private WithEvents mBracketOrders As BracketOrders

    Private mOrderContextBuy As OrderUtils27.OrderContext
    Private mOrderContextBuySimulated As OrderUtils27.OrderContext
    Private mOrderContextSell As OrderUtils27.OrderContext
    Private mOrderContextSellSimulated As OrderUtils27.OrderContext

    Private mPositionManagerSimulated As PositionManager
    Private mTimeframes As Timeframes

    Private mTickNumber As Integer

    Private mStrategy As IStrategy

    Private mActivePositionManagementStrategies As New Dictionary(Of String, IPositionManagementStrategy)
    Private mPositionManagementResourceContexts As New Dictionary(Of String, ResourceContext)

    Private mClock As Clock

    Private mCurrentTick As GenericTick

#End Region

#Region "Constructors"

    Public Sub New(pStrategyRunner As StrategyRunner,
                   pStrategy As IStrategy,
                   pTicker As Ticker,
                   pContract As IContract,
                   pSession As Session,
                   pClock As Clock,
                   pAllowUnprotectedPositions As Boolean)
        mStrategyRunner = pStrategyRunner
        mStrategy = pStrategy

        mTicker = pTicker
        mClock = pClock
        mContract = pContract
        mSession = pSession
        mTicker.AddGenericTickListener(Me)

        mPositionManager = mTicker.PositionManager
        mBracketOrders = mPositionManager.BracketOrders
        mPositionManagerSimulated = mTicker.PositionManagerSimulated

        mOrderContextBuy = mPositionManager.OrderContexts.Add("BUY")
        If Not pAllowUnprotectedPositions Then mOrderContextBuy.PreventUnprotectedPositions = True

        mOrderContextSell = mPositionManager.OrderContexts.Add("SELL")
        If Not pAllowUnprotectedPositions Then mOrderContextSell.PreventUnprotectedPositions = True

        mOrderContextBuySimulated = mPositionManagerSimulated.OrderContexts.Add("BUY")
        mOrderContextSellSimulated = mPositionManagerSimulated.OrderContexts.Add("SELL")
    End Sub

#End Region

#Region "IGenericTickListener_NotifyTick Interface Members"

    Private Sub IGenericTickListener_NoMoreTicks(ByRef ev As GenericTickEventData) Implements IGenericTickListener.NoMoreTicks

    End Sub

    Private Sub IGenericTickListener_NotifyTick(ByRef ev As GenericTickEventData) Implements IGenericTickListener.NotifyTick
        Try
            mTickNumber = mTickNumber + 1

            Static sGotFirstAsk As Boolean
            Static sGotFirstBid As Boolean

            If ev.Tick.TickType = TickUtils27.TickTypes.TickTypeAsk Then sGotFirstAsk = True
            If ev.Tick.TickType = TickUtils27.TickTypes.TickTypeBid Then sGotFirstBid = True

            If Not (sGotFirstAsk And sGotFirstBid) Then Exit Sub

            Select Case ev.Tick.TickType
                Case TickUtils27.TickTypes.TickTypeAsk, TickUtils27.TickTypes.TickTypeBid, TickUtils27.TickTypes.TickTypeTrade, TickUtils27.TickTypes.TickTypeVolume
                Case Else
                    Exit Sub
            End Select
            mCurrentTick = ev.Tick

            For Each lPMStrategy In mActivePositionManagementStrategies.Values
                mStrategyRunner.SetCurrent(lPMStrategy, mPositionManagementResourceContexts.Item(lPMStrategy.Key))
                lPMStrategy.NotifyTick(DirectCast(ev.Tick.TickType, TickType))
                mStrategyRunner.SetNotCurrent()
            Next lPMStrategy


            mStrategyRunner.SetCurrent(mStrategy, Nothing)
            mStrategy.NotifyTick(DirectCast(mCurrentTick.TickType, TickType))
            mStrategyRunner.SetNotCurrent()

        Catch ex As Exception
            ex.Source = ex.StackTrace
            Throw
        End Try
    End Sub

#End Region

#Region "mBracketOrders Event Handlers"

    Private Sub mBracketOrders_CollectionChanged(ByRef ev As CollectionChangeEventData) Handles mBracketOrders.CollectionChanged
        Try
            If ev.ChangeType <> CollectionChangeTypes.CollItemAdded Then Exit Sub
            OrderUtils.CreateBracketProfitCalculator(DirectCast(ev.AffectedItem, _IBracketOrder), DirectCast(mTicker, _IMarketDataSource))
        Catch e As COMException
            Throw New COMException(e.Message, e.ErrorCode) With {.Source = .Source & vbCrLf & e.StackTrace}
        End Try
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property AskQuote() As Quote
        Get
            AskQuote = New Quote(mTicker.CurrentQuote(TickUtils27.TickTypes.TickTypeAsk))
        End Get
    End Property

    Public ReadOnly Property BidQuote() As Quote
        Get
            BidQuote = New Quote(mTicker.CurrentQuote(TickUtils27.TickTypes.TickTypeBid))
        End Get
    End Property

    Public ReadOnly Property CloseQuote() As Quote
        Get
            CloseQuote = New Quote(mTicker.CurrentQuote(TickUtils27.TickTypes.TickTypeClosePrice))
        End Get
    End Property

    Public ReadOnly Property Contract() As IContract
        Get
            Contract = mContract
        End Get
    End Property

    Public ReadOnly Property CurrentTick() As GenericTick
        Get
            CurrentTick = mCurrentTick
        End Get
    End Property

    Public ReadOnly Property DefaultBuyOrderContext() As OrderUtils27.OrderContext
        Get
            DefaultBuyOrderContext = mOrderContextBuy
        End Get
    End Property

    Public ReadOnly Property DefaultBuyOrderContextSimulated() As OrderUtils27.OrderContext
        Get
            DefaultBuyOrderContextSimulated = mOrderContextBuySimulated
        End Get
    End Property

    Public ReadOnly Property DefaultSellOrderContext() As OrderUtils27.OrderContext
        Get
            DefaultSellOrderContext = mOrderContextSell
        End Get
    End Property

    Public ReadOnly Property DefaultSellOrderContextSimulated() As OrderUtils27.OrderContext
        Get
            DefaultSellOrderContextSimulated = mOrderContextSellSimulated
        End Get
    End Property

    Public ReadOnly Property HighQuote() As Quote
        Get
            HighQuote = New Quote(mTicker.CurrentQuote(TickUtils27.TickTypes.TickTypeHighPrice))
        End Get
    End Property

    Public ReadOnly Property LowQuote() As Quote
        Get
            LowQuote = New Quote(mTicker.CurrentQuote(TickUtils27.TickTypes.TickTypeLowPrice))
        End Get
    End Property

    Public ReadOnly Property OpenQuote() As Quote
        Get
            OpenQuote = New Quote(mTicker.CurrentQuote(TickUtils27.TickTypes.TickTypeOpenPrice))
        End Get
    End Property

    Public ReadOnly Property PositionManager() As PositionManager
        Get
            PositionManager = mPositionManager
        End Get
    End Property

    Public ReadOnly Property PositionManagerSimulated() As PositionManager
        Get
            PositionManagerSimulated = mPositionManagerSimulated
        End Get
    End Property

    Public ReadOnly Property Session() As Session
        Get
            Session = mSession
        End Get
    End Property

    Public ReadOnly Property TickNumber() As Integer
        Get
            TickNumber = mTickNumber
        End Get
    End Property

    Public ReadOnly Property Timestamp() As Date
        Get
            Timestamp = mClock.Timestamp
        End Get
    End Property

    Public ReadOnly Property TradeQuote() As Quote
        Get
            TradeQuote = New Quote(mTicker.CurrentQuote(TickUtils27.TickTypes.TickTypeTrade))
        End Get
    End Property

    Public ReadOnly Property VolumeQuote() As Quote
        Get
            VolumeQuote = New Quote(mTicker.CurrentQuote(TickUtils27.TickTypes.TickTypeVolume))
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub ApplyPositionManagementStrategy(pBracketOrder As _IBracketOrder, pStrategy As IPositionManagementStrategy, pResourceContext As ResourceContext)
        mPositionManagementResourceContexts.Add(pStrategy.Key, pResourceContext)

        mStrategyRunner.SetCurrent(pStrategy, pResourceContext)
        mStrategyRunner.RequestBracketOrderNotification(pBracketOrder, pStrategy, pResourceContext)
        pStrategy.Start(Me, mStrategyRunner.GetBracketOrderFromCOMBracketOrder(pBracketOrder))
        mStrategyRunner.SetNotCurrent()
    End Sub

    Public Function getBars(pTimeframe As TimeframeUtils27.Timeframe) As BarUtils27.Bars
        getBars = DirectCast(pTimeframe.BarStudy.BarsFuture.Value, BarUtils27.Bars)
    End Function

    Public Function GetStudyValue(Study As AutoTradingEnvironment.Study, Optional ValueName As String = "", Optional Ref As Integer = 0) As Object
        AssertArgument(Not Study Is Nothing, "Study is nothing")
        AssertArgument(TypeOf Study.Study Is _IStudy, "Study does not refer to a Study object")

        Dim lStudy = DirectCast(Study.Study, _IStudy)
        If ValueName = "" Then ValueName = lStudy.StudyDefinition.DefaultValueName
        GetStudyValue = lStudy.GetStudyValue(ValueName, Ref).Value
    End Function

    Public Sub LogTradeMessage(pMessage As String, Optional pLogLevel As LogLevels = LogLevels.LogLevelNormal)
        If pMessage = "" Then Exit Sub

        Dim lTimepart = TWUtilities.FormatTimestamp(Timestamp, TimestampFormats.TimestampDateAndTimeISO8601) & "  "

        Dim lSpacer As String
        If InStr(1, pMessage, vbCrLf) = 0 Then
            TradeLogger.Log(pLogLevel, lTimepart & pMessage)
        Else
            lSpacer = Space(Len(lTimepart))
            TradeLogger.Log(pLogLevel, lTimepart & Replace(pMessage, vbCrLf, vbCrLf & lSpacer))
        End If

    End Sub

    Public Sub StartTickData(pStrategy As IPositionManagementStrategy)
        mActivePositionManagementStrategies.Add(pStrategy.Key, pStrategy)
    End Sub

    Public Sub StopTickData(pStrategy As IPositionManagementStrategy)
        mActivePositionManagementStrategies.Remove(pStrategy.Key)
    End Sub

#End Region

#Region "Helper Functions"
#End Region

End Class