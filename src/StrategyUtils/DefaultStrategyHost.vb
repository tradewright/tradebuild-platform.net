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


Imports OrderUtils27
Imports TickfileUtils27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Contracts
Imports TickerUtils27
Imports TradeWright.Trading.TradeBuild.AutoTradingEnvironment
Imports TradeWright.Trading.Utils.Sessions

Public NotInheritable Class DefaultStrategyHost
    Implements ILogListener
    Implements IStrategyHost

#Region "Interfaces"


#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "DefaultStrategyHost"

#End Region

#Region "Member variables"

    Private mModel As IStrategyHostModel
    Private mView As IStrategyHostView
    Private mController As IStrategyHostController

    Private mStrategyRunner As StrategyRunner

#End Region

#Region "Constructors"

#Region "ILogListener Interface Members"

    Private Sub ILogListener_Finish() Implements ILogListener.Finish
    End Sub

    Private Sub ILogListener_Notify(pLogrec As LogRecord) Implements ILogListener.Notify
        Select Case pLogrec.InfoType
            Case "strategy.tradereason"
                mView.WriteLogText(formatLogRecord(pLogrec, False))
            Case "position.profit"
                mView.NotifySessionProfit(CDbl(pLogrec.Data), mModel.Ticker.Timestamp)
            Case "position.drawdown"
                mView.NotifySessionDrawdown(CDbl(pLogrec.Data))
            Case "position.maxprofit"
                mView.NotifySessionMaxProfit(CDbl(pLogrec.Data))
            Case "position.bracketorderprofilestruct"
                mController.NotifyBracketOrderProfile(DirectCast(pLogrec.Data, BracketOrderProfile))
            Case "position.position"
                mController.NotifyPosition(CInt(pLogrec.Data))
            Case "position.order", "position.moneymanagement"
                Logger.Log(LogLevels.LogLevelNormal, CStr(pLogrec.Data))
                mView.WriteLogText(formatLogRecord(pLogrec, False))
            Case "position.ordersimulated", "position.moneymanagementsimulated"
                Logger.Log(LogLevels.LogLevelNormal, CStr(pLogrec.Data))
        End Select
    End Sub

#End Region

#Region "IStrategyHost Interface Members"

    Private Sub IStrategyHost_AddTimeframe(pTimeframe As TimeframeUtils27.Timeframe) Implements IStrategyHost.AddTimeframe
        mController.AddTimeframe(pTimeframe)
    End Sub

    Private Sub IStrategyHost_ChartStudyValue(pStudy As StudyUtils27._IStudy, pValueName As String, pTimeframe As TimeframeUtils27.Timeframe) Implements IStrategyHost.ChartStudyValue
        mController.ChartStudyValue(pStudy, pValueName, pTimeframe)
    End Sub

    Private Sub IStrategyHost_ContractInvalid(pMessage As String) Implements IStrategyHost.ContractInvalid
        mController.ContractInvalid(pMessage)
    End Sub

    Private ReadOnly Property IStrategyHost_ContractStorePrimary() As IContractStore Implements IStrategyHost.ContractStorePrimary
        Get
            Return mModel.ContractStorePrimary
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_ContractStoreSecondary() As IContractStore Implements IStrategyHost.ContractStoreSecondary
        Get
            Return mModel.ContractStoreSecondary
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_HistoricalDataStoreInput() As HistDataUtils27.IHistoricalDataStore Implements IStrategyHost.HistoricalDataStoreInput
        Get
            IStrategyHost_HistoricalDataStoreInput = mModel.HistoricalDataStoreInput
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_LogDummyProfitProfile() As Boolean Implements IStrategyHost.LogDummyProfitProfile
        Get
            Return mModel.LogDummyProfitProfile
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_LogParameters() As Boolean Implements IStrategyHost.LogParameters
        Get
            Return mModel.LogParameters
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_LogProfitProfile() As Boolean Implements IStrategyHost.LogProfitProfile
        Get
            IStrategyHost_LogProfitProfile = mModel.LogProfitProfile
        End Get
    End Property

    Private Sub IStrategyHost_NotifyTickfileCompleted(ByVal pTickfile As TickfileSpecifier, ByVal pEventsPlayed As Integer) Implements IStrategyHost.NotifyTickfileCompleted
        mController.NotifyTickfileCompleted(pTickfile, pEventsPlayed)
    End Sub

    Private ReadOnly Property IStrategyHost_OrderSubmitterFactoryLive() As OrderUtils27.IOrderSubmitterFactory Implements IStrategyHost.OrderSubmitterFactoryLive
        Get
            IStrategyHost_OrderSubmitterFactoryLive = mModel.OrderSubmitterFactoryLive
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_OrderSubmitterFactorySimulated() As OrderUtils27.IOrderSubmitterFactory Implements IStrategyHost.OrderSubmitterFactorySimulated
        Get
            IStrategyHost_OrderSubmitterFactorySimulated = mModel.OrderSubmitterFactorySimulated
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_RealtimeTickers() As TickerUtils27.Tickers Implements IStrategyHost.RealtimeTickers
        Get
            IStrategyHost_RealtimeTickers = mModel.RealtimeTickers
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_ResultsPath() As String Implements IStrategyHost.ResultsPath
        Get
            IStrategyHost_ResultsPath = mModel.ResultsPath
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_StudyLibraryManager() As StudyUtils27.StudyLibraryManager Implements IStrategyHost.StudyLibraryManager
        Get
            IStrategyHost_StudyLibraryManager = mModel.StudyLibraryManager
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_TickfileStoreInput() As TickfileUtils27.ITickfileStore Implements IStrategyHost.TickfileStoreInput
        Get
            IStrategyHost_TickfileStoreInput = mModel.TickfileStoreInput
        End Get
    End Property

    Private ReadOnly Property IStrategyHost_UseMoneyManagement() As Boolean Implements IStrategyHost.UseMoneyManagement
        Get
            IStrategyHost_UseMoneyManagement = mModel.UseMoneyManagement
        End Get
    End Property

    Private Sub IStrategyHost_NotifyReplayCompleted() Implements IStrategyHost.NotifyReplayCompleted
        mController.NotifyReplayCompleted()
    End Sub

    Private Sub IStrategyHost_NotifyReplayEvent(ByRef ev As NotificationEventData) Implements IStrategyHost.NotifyReplayEvent
        mController.NotifyReplayEvent(ev)
    End Sub

    Private Sub IStrategyHost_NotifyReplayProgress(pTickfileTimestamp As Date, pEventsPlayed As Integer, pPercentComplete As Single) Implements IStrategyHost.NotifyReplayProgress
        mController.NotifyReplayProgress(pTickfileTimestamp, pEventsPlayed, pPercentComplete)
    End Sub

    Private Sub IStrategyHost_NotifyReplayStarted() Implements IStrategyHost.NotifyReplayStarted
        mController.NotifyReplayStarted()
    End Sub

    Private Sub IStrategyHost_TickerCreated(pTicker As Ticker, pContract As IContract, pSession As Session, pClock As Clock) Implements IStrategyHost.TickerCreated
        mController.TickerCreated(pTicker, pContract, pSession, pClock)
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Public Sub Finish()
        TWUtilities.GetLogger("log").RemoveLogListener(Me)
    End Sub

    Public Sub Initialise(pModel As IStrategyHostModel, pView As IStrategyHostView, pController As IStrategyHostController)
        mModel = pModel
        mView = pView
        mController = pController
        mStrategyRunner = New StrategyRunner(Me)
        mController.Initialise(mStrategyRunner, mModel, mView)

        setupLogging()
    End Sub

#End Region

#Region "Helper Functions"

    Private Function formatLogRecord(pLogrec As LogRecord, pIncludeTimestamp As Boolean) As String
        Static formatter As _ILogFormatter
        Static formatterWithTimestamp As _ILogFormatter

        If pIncludeTimestamp Then
            If formatterWithTimestamp Is Nothing Then formatterWithTimestamp = TWUtilities.CreateBasicLogFormatter(TimestampFormats.TimestampTimeOnlyLocal,  , True, False)
            formatLogRecord = formatterWithTimestamp.FormatRecord(pLogrec)
        Else
            If formatter Is Nothing Then formatter = TWUtilities.CreateBasicLogFormatter(,  , False, False)
            formatLogRecord = formatter.FormatRecord(pLogrec)
        End If
    End Function

    Private Sub setupLogging()
        TWUtilities.GetLogger("log").AddLogListener(Me)
        TWUtilities.GetLogger("position.profit").AddLogListener(Me)
        TWUtilities.GetLogger("position.drawdown").AddLogListener(Me)
        TWUtilities.GetLogger("position.maxprofit").AddLogListener(Me)
        TWUtilities.GetLogger("position.bracketorderprofilestruct").AddLogListener(Me)
        TWUtilities.GetLogger("position.position").AddLogListener(Me)
        TWUtilities.GetLogger("position.order").AddLogListener(Me)
        TWUtilities.GetLogger("position.ordersimulated").AddLogListener(Me)
        TWUtilities.GetLogger("position.moneymanagement").AddLogListener(Me)
        TWUtilities.GetLogger("position.moneymanagementsimulated").AddLogListener(Me)
        TWUtilities.GetLogger("strategy.trademessage").AddLogListener(Me)
    End Sub

#End Region

#End Region

End Class