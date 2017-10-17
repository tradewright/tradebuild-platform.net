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

Imports HistDataUtils27
Imports MarketDataUtils27
Imports OrderUtils27
Imports StudyUtils27
Imports TickerUtils27
Imports TickfileUtils27
Imports TickUtils27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Contracts
Imports TradeWright.Trading.Utils.Sessions


Public NotInheritable Class StrategyRunner
    Implements IChangeListener
    Implements ICollectionChangeListener
    Implements IDeferredAction

#Region "Interfaces"


#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "StrategyRunner"
#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mStrategyHost As IStrategyHost

    Private mTickfileStoreInput As ITickfileStore
    Private mStudyLibraryManager As StudyLibraryManager
    Private mContractStorePrimary As IContractStore
    Private mContractStoreSecondary As IContractStore

    Private mHistoricalDataStoreInput As IHistoricalDataStore
    Private mOrderSubmitterFactoryLive As IOrderSubmitterFactory
    Private mOrderSubmitterFactorySimulated As IOrderSubmitterFactory

    Private WithEvents mReplayController As ReplayController
    Private WithEvents mTickfileReplayTC As TaskController

    Private WithEvents mFutureWaiter As New FutureWaiter

    Private mTicker As Ticker
    Private mContract As IContract
    Private mSession As Session
    Private mClock As Clock

    Private mStrategy As IStrategy
    Private mStrategyResourceContext As ResourceContext

    Private mPositionManagementStrategyFactories As List(Of IPositionManagementStrategyFactory)
    Private mPosnMgmtStrategyResourceContexts As Dictionary(Of String, ResourceContext)

    Private mDefaultParameters As Parameters
    Private mParams As Parameters

    Private mInitialisationContext As InitialisationContext
    Private mInitialisationCompleted As Boolean

    Private mTradingContext As TradingContext

    Private mTickers As Tickers

    Private mStartReplayWhenReady As Boolean

    Private mBracketOrderNotificationRequests As New BracketOrderNotifyRequests

    Private mBracketOrderMapping As New Dictionary(Of String, AutoTradingEnvironment.BracketOrder)

#End Region

#Region "Constructors"

    Public Sub New(pStrategyHost As IStrategyHost)
        mStrategyHost = pStrategyHost
        mTickfileStoreInput = mStrategyHost.TickfileStoreInput
        mStudyLibraryManager = mStrategyHost.StudyLibraryManager
        mContractStorePrimary = mStrategyHost.ContractStorePrimary
        mContractStoreSecondary = mStrategyHost.ContractStoreSecondary
        mHistoricalDataStoreInput = mStrategyHost.HistoricalDataStoreInput
        mOrderSubmitterFactoryLive = mStrategyHost.OrderSubmitterFactoryLive
        mOrderSubmitterFactorySimulated = mStrategyHost.OrderSubmitterFactorySimulated
        Randomize()
    End Sub

#Region "IChangeListener Interface Members"

    Private Sub IChangeListener_Change(ByRef ev As ChangeEventData) Implements IChangeListener.Change
        If TypeOf ev.Source Is IBracketOrder Then
            If ev.ChangeType = BracketOrderChangeTypes.BracketOrderCreated Then Exit Sub

            Dim lBracketOrder = DirectCast(ev.Source, IBracketOrder)
            Dim en = mBracketOrderNotificationRequests.GetEnumerator(lBracketOrder)
            Do While en.MoveNext
                Dim lRequest = en.Current
                setStrategyRunner(lRequest.EventSink, lRequest.ResourceContext)
                If ev.ChangeType = BracketOrderChangeTypes.BracketOrderCompleted Then
                    lBracketOrder.RemoveChangeListener(Me)
                    lRequest.EventSink.NotifyBracketOrderCompletion(GetResourceIdForBracketOrder(lBracketOrder))
                ElseIf ev.ChangeType = BracketOrderChangeTypes.BracketOrderEntryOrderFilled Then
                    lRequest.EventSink.NotifyBracketOrderFill(GetResourceIdForBracketOrder(lBracketOrder))
                ElseIf ev.ChangeType = BracketOrderChangeTypes.BracketOrderStopLossOrderChanged Then
                    lRequest.EventSink.NotifyBracketOrderStopLossAdjusted(GetResourceIdForBracketOrder(lBracketOrder))
                End If
                ClearStrategyRunner()
            Loop

        ElseIf TypeOf ev.Source Is PositionManager Then
            Dim lPositionManager = DirectCast(ev.Source, PositionManager)
            If ev.ChangeType = PositionManagerChangeTypes.PositionSizeChanged Then
                If lPositionManager.PositionSize = 0 And lPositionManager.PendingPositionSize = 0 Then
                    setStrategyRunner(mStrategy, mStrategyResourceContext)
                    If lPositionManager.IsSimulated Then
                        mStrategy.NotifyNoSimulatedPositions()
                    Else
                        mStrategy.NotifyNoLivePositions()
                    End If
                    ClearStrategyRunner()
                End If
            End If
        ElseIf TypeOf ev.Source Is OrderUtils27.OrderContext Then
            Dim lOrderContext = DirectCast(ev.Source, OrderUtils27.OrderContext)
            If lOrderContext.IsSimulated Then
                mStrategy.NotifyTradingReadinessChange()
            Else
                mStrategy.NotifySimulatedTradingReadinessChange()
            End If
        End If
    End Sub

#End Region

#Region "ICollectionChangeListener Interface Members"

    Private Sub ICollectionChangeListener_Change(ByRef ev As CollectionChangeEventData) Implements ICollectionChangeListener.Change
        If Not TypeOf ev.Source Is OrderContexts Then Exit Sub

        If Not ev.ChangeType = CollectionChangeTypes.CollItemAdded Then Exit Sub

        Dim lOrderContext = DirectCast(ev.AffectedItem, OrderUtils27.OrderContext)
        lOrderContext.AddChangeListener(Me)
    End Sub

#End Region

#Region "IDeferredAction Interface Members"

    Public Sub RunDeferredAction(Data As Object) Implements _IDeferredAction.Run
        Try
            Assert(mTicker.State = MarketDataSourceStates.MarketDataSourceStateReady, "MarketDataSource not ready")
            mClock = DirectCast(mTicker.ClockFuture.Value, Clock)
            mContract = Utils.Contracts.Contract.FromCOM(DirectCast(mTicker.ContractFuture.Value, ContractUtils27._IContract))
            mSession = Session.FromCOM(DirectCast(mTicker.SessionFuture.Value, SessionUtils27.Session))
            mStrategyHost.TickerCreated(mTicker, mContract, mSession, mClock)
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(RunDeferredAction), NameOf(StrategyRunner))
        End Try
    End Sub

#End Region

#Region "mFutureWaiter Event Handlers"

    Private Sub mFutureWaiter_WaitAllCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitAllCompleted
        TWUtilities.DeferAction(Me)
    End Sub

    Private Sub mFutureWaiter_WaitCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitCompleted
        Try
            If ev.Future.IsFaulted Then
                mStrategyHost.ContractInvalid(ev.Future.ErrorMessage)
            ElseIf Not ev.Future.IsAvailable Then
            ElseIf TypeOf ev.Future.Value Is ContractUtils27._IContract Then
                Dim lContract = Contract.FromCOM(DirectCast(ev.Future.Value, ContractUtils27._IContract))

                mTicker = mTickers.CreateTicker(ev.Future, False, lContract.Specifier.Key)

                mFutureWaiter.Add(mTicker.SessionFuture)
                mFutureWaiter.Add(mTicker.ClockFuture)

                setupLogging(lContract.Specifier.LocalSymbol)
            End If
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mFutureWaiter_WaitCompleted), NameOf(StrategyRunner))
        End Try
    End Sub

#End Region

#Region "mReplayController Event Handlers"

    Private Sub mReplayController_NotifyEvent(ByRef ev As NotificationEventData) Handles mReplayController.NotifyEvent
        Try
            ev.Source = Me
            mStrategyHost.NotifyReplayEvent(ev)
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mReplayController_NotifyEvent), NameOf(StrategyRunner))
        End Try
    End Sub

    Private Sub mReplayController_ReplayProgress(pTickfileTimestamp As Date, pEventsPlayed As Integer, pPercentComplete As Integer) Handles mReplayController.ReplayProgress
        Try
            mStrategyHost.NotifyReplayProgress(pTickfileTimestamp, pEventsPlayed, pPercentComplete)
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mReplayController_ReplayProgress), NameOf(StrategyRunner))
        End Try
    End Sub

    Private Sub mReplayController_ReplayStarted() Handles mReplayController.ReplayStarted
        Try
            mStrategyHost.NotifyReplayStarted()
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mReplayController_ReplayStarted), NameOf(StrategyRunner))
        End Try
    End Sub

    Private Sub mReplayController_TickfileCompleted(ByRef ev As TickfileEventData, pEventsPlayed As Integer) Handles mReplayController.TickfileCompleted
        Try
            mStrategyHost.NotifyTickfileCompleted(ev.Specifier, pEventsPlayed)
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mFutureWaiter_WaitCompleted), NameOf(StrategyRunner))
        End Try
    End Sub

#End Region

#Region "mTickfileReplayTC Event Handlers"

    Private Sub mTickfileReplayTC_Completed(ByRef ev As TaskCompletionEventData) Handles mTickfileReplayTC.Completed
        Try
            mReplayController = Nothing
            mStrategyHost.NotifyReplayCompleted()
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mTickfileReplayTC_Completed), NameOf(StrategyRunner))
        End Try
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property DefaultParameters() As Parameters
        Get
            DefaultParameters = mDefaultParameters
        End Get
    End Property

    Public ReadOnly Property Parameters() As Parameters
        Get
            Parameters = mParams
        End Get
    End Property

    Friend ReadOnly Property PositionManagementStrategyFactories() As List(Of IPositionManagementStrategyFactory)
        Get
            PositionManagementStrategyFactories = mPositionManagementStrategyFactories
        End Get
    End Property

#End Region

#Region "Methods"

    Friend Function GetPositionManagementStrategyResourceContext(pFactory As IPositionManagementStrategyFactory) As ResourceContext
        Return mPosnMgmtStrategyResourceContexts.Item(pFactory.Key)
    End Function

    Public Function GetResourceIdForBracketOrder(pBracketOrder As _IBracketOrder) As AutoTradingEnvironment.BracketOrder
        GetResourceIdForBracketOrder = mBracketOrderMapping.Item(pBracketOrder.Key)
    End Function

    Friend Sub InitialisationCompleted()
        mInitialisationCompleted = True
        Dim lAllowUnprotectedPositions As Boolean
        lAllowUnprotectedPositions = mInitialisationContext.AllowUnprotectedPositions
        mInitialisationContext = Nothing
        mTicker.PositionManager.AddChangeListener(Me)
        mTicker.PositionManager.OrderContexts.AddCollectionChangeListener(Me)
        mTicker.PositionManagerSimulated.AddChangeListener(Me)
        mTicker.PositionManagerSimulated.OrderContexts.AddCollectionChangeListener(Me)

        mTradingContext = New TradingContext(Me, mStrategy, mTicker, mContract, mSession, mClock, lAllowUnprotectedPositions)

        setStrategyRunner(mStrategy, mStrategyResourceContext)
        mStrategy.Start(mTradingContext)
        ClearStrategyRunner()

        startReplayIfReady()
    End Sub

    Friend Sub MapBracketOrderToResourceId(pBracketOrder As _IBracketOrder, pIdentifer As AutoTradingEnvironment.BracketOrder)
        mBracketOrderMapping.Add(pBracketOrder.Key, pIdentifer)
    End Sub

    Public Sub PrepareSymbol(pSymbol As IContractSpecifier)
        mTickers = mStrategyHost.RealtimeTickers
        Dim lContractFetcher = New ContractFetcher(mContractStorePrimary, mContractStoreSecondary)
        mFutureWaiter.Add(lContractFetcher.FetchContract(pSymbol))
    End Sub

    Public Sub PrepareTickFile(pTickfiles As TickFileSpecifiers)
        Assert(mReplayController Is Nothing, "mReplayController is not Nothing")

        Dim lContractStorePrimary = TryCast(mContractStorePrimary, ContractUtils27.IContractStore)
        Assert(Not (mContractStorePrimary IsNot Nothing And lContractStorePrimary Is Nothing), "Primary contract store does not implement the COM-based IContractStore")

        Dim lContractStoreSecondary = TryCast(mContractStoreSecondary, ContractUtils27.IContractStore)
        Assert(Not (mContractStoreSecondary IsNot Nothing And lContractStoreSecondary Is Nothing), "Secondary contract store does not implement the COM-based IContractStore")

        Dim lTickfileDataManager = MarketDataUtils.CreateSequentialTickDataManager(pTickfiles,
                                                                         mTickfileStoreInput,
                                                                         mStudyLibraryManager,
                                                                         lContractStorePrimary,
                                                                         lContractStoreSecondary,
                                                                         MarketDataSourceOptions.MarketDataSourceOptUseExchangeTimeZone,
                                                                         pReplaySpeed:=0)

        mReplayController = DirectCast(lTickfileDataManager, TickfileDataManager).ReplayController

        Dim lOrderManager As New OrderManager
        mTickers = TickerUtils.CreateTickers(lTickfileDataManager, mHistoricalDataStoreInput, lOrderManager, mOrderSubmitterFactoryLive, mOrderSubmitterFactorySimulated)

        mFutureWaiter.Add(mReplayController.TickStream(0).ContractFuture)
    End Sub

    Friend Sub RequestBracketOrderNotification(pBracketOrder As _IBracketOrder, pStrategy As Object, pResourceContext As ResourceContext)
        mBracketOrderNotificationRequests.Add(pBracketOrder, pStrategy, pResourceContext)
        pBracketOrder.AddChangeListener(Me)
    End Sub

    Friend Sub SetCurrent(pStrategy As Object, pResourceContext As ResourceContext)
        If pResourceContext Is Nothing Then pResourceContext = mStrategyResourceContext
        setStrategyRunner(pStrategy, pResourceContext)
    End Sub

    Friend Sub SetNotCurrent()
        ClearStrategyRunner()
    End Sub

    Public Function GetDefaultParameters(pStrategy As IStrategy, pPositionManagementStrategyFactories As List(Of IPositionManagementStrategyFactory)) As Parameters
        mStrategy = pStrategy
        mPositionManagementStrategyFactories = pPositionManagementStrategyFactories

        Dim lParams As New Parameters
        mDefaultParameters = lParams

        setStrategyRunner(mStrategy, Nothing)
        mStrategy.DefineDefaultParameters()
        ClearStrategyRunner()

        Dim lPMFactory As IPositionManagementStrategyFactory
        Dim lPMFactoryParams As New Parameters
        For Each lPMFactory In mPositionManagementStrategyFactories
            mDefaultParameters = lPMFactoryParams

            setStrategyRunner(lPMFactory, Nothing)
            lPMFactory.DefineDefaultParameters()
            ClearStrategyRunner()

            lParams = mergeParameters(lParams, lPMFactoryParams)
        Next lPMFactory

        mDefaultParameters = Nothing
        GetDefaultParameters = lParams
    End Function

    Public Sub StartLiveData()
        mTicker.StartMarketData()
    End Sub

    Public Sub StartReplay()
        mStartReplayWhenReady = True
        startReplayIfReady()
    End Sub

    Public Sub StartStrategy(pStrategy As IStrategy, pParams As Parameters)
        mStrategy = pStrategy
        mParams = pParams

        LogParameters(mParams)
        initialiseStrategy()
        initialisePositionManagementStrategyFactories()
    End Sub

    Public Sub StopTesting()
        If Not mReplayController Is Nothing Then
            ' prevent event handler being fired on completion, which would
            ' reload the main form again
            mTickfileReplayTC = Nothing
            If mReplayController.ReplayInProgress Then mReplayController.StopReplay()
            mReplayController = Nothing
        End If

        If Not mTicker Is Nothing Then mTicker.Finish()
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub initialisePositionManagementStrategyFactories()
        mPosnMgmtStrategyResourceContexts = New Dictionary(Of String, ResourceContext)

        For Each lPMFactory As IPositionManagementStrategyFactory In mPositionManagementStrategyFactories
            Dim lResourceContext As New ResourceContext
            mPosnMgmtStrategyResourceContexts.Add(lPMFactory.Key, lResourceContext)
            setStrategyRunner(mPositionManagementStrategyFactories, lResourceContext)
            lPMFactory.Initialise(mInitialisationContext)
            ClearStrategyRunner()
        Next
    End Sub
    Private Sub initialiseStrategy()
        mInitialisationCompleted = False
        mInitialisationContext = New InitialisationContext(mStrategyHost, Me, mTicker, mContract, mSession, mClock)

        mStrategyResourceContext = New ResourceContext()
        setStrategyRunner(mStrategy, mStrategyResourceContext)
        mStrategy.Initialise(mInitialisationContext)
        ClearStrategyRunner()
    End Sub

    Private Sub LogParameters(ByVal pParams As Parameters)
        If mStrategyHost.LogParameters Then
            Dim s = "Strategy Parameters:" & vbCrLf
            If Not pParams Is Nothing Then
                For Each lParam As Parameter In pParams
                    s = $"{s}{vbTab}{lParam.Name}={lParam.Value}{vbCrLf}"
                Next
            End If
            StrategyLogger.Log(LogLevels.LogLevelNormal, s)
        End If
    End Sub

    Private Function mergeParameters(pParams1 As Parameters, pParams2 As Parameters) As Parameters
        Const DefaultValue As String = "***$Default$***"

        Dim lParams As New Parameters

        Dim lParam As Parameter
        For Each lParam In pParams1
            lParams.SetParameterValue(lParam.Name, lParam.Value)
        Next lParam

        For Each lParam In pParams2
            If lParams.GetParameterValue(lParam.Name, DefaultValue) = DefaultValue Then lParams.SetParameterValue(lParam.Name, lParam.Value)
        Next lParam

        mergeParameters = lParams
    End Function

    Private Sub setStrategyRunner(pStrategy As Object, pResourceContext As ResourceContext)
        Contexts.SetStrategyRunner(Me, mInitialisationContext, mTradingContext, pResourceContext, pStrategy)
    End Sub

    Private Sub setupLogging(pSymbol As String)
        Static sLoggingSetup As Boolean

        If sLoggingSetup Then Exit Sub
        sLoggingSetup = True

        Dim lResultsPath As String
        lResultsPath = mStrategyHost.ResultsPath
        If lResultsPath = "" Then lResultsPath = TWUtilities.ApplicationSettingsFolder & "\TestResults\"
        If Right(lResultsPath, 1) <> "\" Then lResultsPath = lResultsPath & "\"

        Dim lFilenameDiscriminator As String
        lFilenameDiscriminator = CStr(Int(1000000 * Rnd() + 1))

        Dim lLogfile = TWUtilities.CreateFileLogListener($"{lResultsPath}Logs\{mStrategy.Name}-{pSymbol}-{lFilenameDiscriminator}.log", includeTimestamp:=False, includeLogLevel:=False)
        TWUtilities.GetLogger("position.order").AddLogListener(lLogfile)
        TWUtilities.GetLogger("position.simulatedorder").AddLogListener(lLogfile)
        TWUtilities.GetLogger("strategy").AddLogListener(lLogfile)
        TWUtilities.GetLogger("position.moneymanagement").AddLogListener(lLogfile)

        lLogfile = TWUtilities.CreateFileLogListener($"{lResultsPath}Orders\{mStrategy.Name}-{pSymbol}-{lFilenameDiscriminator}.log", includeTimestamp:=False, includeLogLevel:=False)
        TWUtilities.GetLogger("position.orderdetail").AddLogListener(lLogfile)

        lLogfile = TWUtilities.CreateFileLogListener($"{lResultsPath}Orders\{mStrategy.Name}-{pSymbol}-{lFilenameDiscriminator}-Profile.log", includeTimestamp:=False, includeLogLevel:=False)
        TWUtilities.GetLogger("position.bracketorderprofilestring").AddLogListener(lLogfile)

        lLogfile = TWUtilities.CreateFileLogListener($"{lResultsPath}Orders\{mStrategy.Name}-{pSymbol}-{lFilenameDiscriminator}-TradeProfile.log", includeTimestamp:=False, includeLogLevel:=False)
        TWUtilities.GetLogger("position.tradeprofile").AddLogListener(lLogfile)

        If mStrategyHost.LogProfitProfile Then
            lLogfile = TWUtilities.CreateFileLogListener($"{lResultsPath}Orders\{mStrategy.Name}-{pSymbol}-{lFilenameDiscriminator}-Profit.log", includeTimestamp:=False, includeLogLevel:=False)
            TWUtilities.GetLogger("position.profitprofile").AddLogListener(lLogfile)
        End If
    End Sub

    Private Sub startReplayIfReady()
        If mReplayController Is Nothing Then Exit Sub
        If Not mInitialisationCompleted Then Exit Sub
        If Not mStartReplayWhenReady Then Exit Sub

        mStartReplayWhenReady = False

        Logger.Log(LogLevels.LogLevelNormal, "Tickfile replay started")
        mTicker.StartMarketData()
        mTickfileReplayTC = mReplayController.StartReplay
    End Sub

#End Region

#End Region

End Class