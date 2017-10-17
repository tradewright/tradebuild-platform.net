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


Public Interface IStrategyHostView

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

    Enum ErrorSeverities
        ErrorSeverityNone
        ErrorSeverityInformation
        ErrorSeverityWarning
        ErrorSeverityCritical
    End Enum

#End Region

#Region "Types"

#End Region

#Region "Constants"

#End Region

#Region "Member variables"

#End Region

#Region "Constructors"

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    ReadOnly Property Strategy() As IStrategy

    ReadOnly Property Parameters() As TWUtilities40.Parameters

#End Region

#Region "Methods"

    Sub AddStudyToChart(pChartIndex As Integer, pStudy As StudyUtils27._IStudy, pStudyValueNames As System.Collections.Generic.Dictionary(Of String, Object))

    Function AddTimeframe(pTimeframe As TimeframeUtils27.Timeframe) As Integer

    Sub ClearPriceAndProfitFields()

    ' no pIndex => all charts
    Sub DisablePriceDrawing(Optional pTimeframeIndex As Integer = 0)

    Sub DisableProfitDrawing()

    Sub DisableStart()

    Sub DisableTradeDrawing()

    ' no pIndex => all charts
    Sub EnablePriceDrawing(Optional pTimeframeIndex As Integer = 0)

    Sub EnableProfitDrawing()

    Sub EnableStart()

    Sub EnableTradeDrawing()

    Sub NotifyError(pTitle As String, pMessage As String, pSeverity As ErrorSeverities)

    Sub NotifyBracketOrderProfile(Value As OrderUtils27.BracketOrderProfile)

    Sub NotifyEventsPerSecond(Value As Integer)

    Sub NotifyEventsPlayed(Value As Integer)

    Sub NotifyMicrosecsPerEvent(Value As Integer)

    Sub NotifyNewTradeBar(pBarNumber As Integer, pTimestamp As Date)

    Sub NotifyPosition(Value As Integer)

    Sub NotifyReplayProgress(pTickfileTimestamp As Date, pEventsPlayed As Integer, pPercentComplete As Single)

    Sub NotifyReplayStarted()

    Sub NotifySessionDrawdown(Value As Double)

    Sub NotifySessionMaxProfit(Value As Double)

    Sub NotifySessionProfit(Value As Double, pTimestamp As Date)

    Sub NotifyTick(ByRef ev As TickUtils27.GenericTickEventData)

    Sub NotifyTickerCreated()

    Sub NotifyTickfileCompleted(pTickfile As TickfileUtils27.TickfileSpecifier, ByVal pEventsPlayed As Integer)

    Sub NotifyTradingStart()

    Sub ResetBracketOrderList()

    Sub ResetPriceChart()

    Sub ResetProfitChart()

    Sub ResetTradeChart()

    Sub ShowTradeLine(pStartTime As Date, pEndTime As Date, pEntryPrice As Double, pExitPrice As Double, pProfit As Double)

    Sub UpdateLastChartBars()

    Sub WriteLogText(pMessage As String)

#End Region

#Region "Helper Functions"
#End Region

End Interface