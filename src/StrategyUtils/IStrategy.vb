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


Public Interface IStrategy

    ''
    ' This interface must be implemented by classes that operate as trading strategies
    ' within the TradeBuild Strategy Host program.
    '
    '@/

#Region "Properties"

    ReadOnly Property Key() As String
    ReadOnly Property Name() As String

#End Region

#Region "Methods"

    Sub DefineDefaultParameters()

    ''
    ' Called to enable the <code>Strategy</code> object to initialise itself, for example
    ' by creating any necessary timeframes and studies.
    '
    ' @param StrategyContext
    '   The <code>StrategyContext</code> object to be used by this <code>Strategy</code>.
    ' @param pParams
    '   The parameters for this <code>Strategy</code>.
    '@/
    Sub Initialise(pInitialisationContext As InitialisationContext)

    Sub NotifyNoLivePositions()

    Sub NotifyNoSimulatedPositions()

    Sub NotifySimulatedTradingReadinessChange()

    ''
    ' Called for each tick in the underlying <code>Ticker</code> object.
    '
    ' @remarks
    '   The relevant prices and sizes relating to this tick can be obtained using the
    '   revelant properties of the <code>TradingContext</code> object, such as
    '   <code>AskPrice</code>, <code>AskPriceString</code>, <code>AskSize</code> etc.
    ' @param pType
    '   The type of tick that has occurred.
    '@/
    Sub NotifyTick(pType As TickType)

    Sub NotifyTradingReadinessChange()

    ''
    ' Called after historic price data has been loaded,
    ' but before the first tick is notified.
    Sub Start(pTradingContext As TradingContext)

#End Region

End Interface