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

Public Interface IStrategyHostController
    Sub AddTimeframe(pTimeframe As TimeframeUtils27.Timeframe)
    Sub ChartStudyValue(pStudy As StudyUtils27._IStudy, pValueName As String, pTimeframe As TimeframeUtils27.Timeframe)
    Sub ContractInvalid(pMessage As String)
    Sub Finish()
    Sub Initialise(pStrategyRunner As StrategyRunner, pModel As IStrategyHostModel, pView As IStrategyHostView)
    Function GetDefaultParameters(pStrategy As IStrategy, pPositionManagementStrategyFactories As System.Collections.Generic.List(Of IPositionManagementStrategyFactory)) As TWUtilities40.Parameters
    Sub NotifyBracketOrderProfile(Value As OrderUtils27.BracketOrderProfile)
    Sub NotifyInitialisationCompleted()
    Sub NotifyPosition(pPosition As Integer)
    Sub NotifyReplayEvent(ByRef ev As TWUtilities40.NotificationEventData)
    Sub NotifyReplayCompleted()
    Sub NotifyReplayProgress(pTickfileTimestamp As Date, pEventsPlayed As Integer, pPercentComplete As Single)
    Sub NotifyReplayStarted()
    Sub NotifyTickfileCompleted(ByVal pTickfile As TickfileUtils27.TickfileSpecifier, ByVal pEventsPlayed As Integer)
    Sub StartLiveProcessing(pSymbol As IContractSpecifier)
    Sub StartTickfileReplay(pTickFileSpecifiers As TickfileUtils27.TickFileSpecifiers)
    Sub StopTickfileReplay()
    Sub TickerCreated(pTicker As TickerUtils27.Ticker, pContract As IContract, pSession As Session, pClock As TWUtilities40.Clock)
End Interface

