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

Imports TradeWright.Trading.Utils.Contracts
Imports TradeWright.Trading.Utils.Sessions

Imports TickfileUtils27

Public Interface IStrategyHost
    ReadOnly Property ContractStorePrimary As IContractStore
    ReadOnly Property ContractStoreSecondary As IContractStore
    ReadOnly Property HistoricalDataStoreInput As HistDataUtils27.IHistoricalDataStore
    ReadOnly Property LogDummyProfitProfile As Boolean
    ReadOnly Property LogParameters As Boolean
    ReadOnly Property LogProfitProfile As Boolean
    ReadOnly Property OrderSubmitterFactoryLive As OrderUtils27.IOrderSubmitterFactory
    ReadOnly Property OrderSubmitterFactorySimulated As OrderUtils27.IOrderSubmitterFactory
    ReadOnly Property RealtimeTickers As TickerUtils27.Tickers
    ReadOnly Property ResultsPath As String
    ReadOnly Property StudyLibraryManager As StudyUtils27.StudyLibraryManager
    ReadOnly Property TickfileStoreInput As TickfileUtils27.ITickfileStore
    ReadOnly Property UseMoneyManagement As Boolean
    Sub AddTimeframe(pTimeframe As TimeframeUtils27.Timeframe)
    Sub ChartStudyValue(pStudy As StudyUtils27._IStudy, pValueName As String, pTimeframe As TimeframeUtils27.Timeframe)
    Sub ContractInvalid(pMessage As String)
    Sub NotifyReplayCompleted()
    Sub NotifyReplayEvent(ByRef ev As TWUtilities40.NotificationEventData)
    Sub NotifyTickfileCompleted(ByVal pTickfile As TickfileSpecifier, ByVal pEventsPlayed As Integer)
    Sub NotifyReplayProgress(tickfileTimestamp As Date, eventsPlayed As Integer, percentComplete As Single)
    Sub NotifyReplayStarted()
    Sub TickerCreated(pTicker As TickerUtils27.Ticker, pContract As IContract, pSession As Session, pClock As TWUtilities40.Clock)
End Interface

