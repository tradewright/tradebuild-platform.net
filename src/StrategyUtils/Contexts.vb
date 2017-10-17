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

Friend Module Contexts

    Private NotInheritable Class Contexts
        Friend Strategy As Object
        Friend StrategyRunner As StrategyRunner
        Friend InitialisationContext As InitialisationContext
        Friend TradingContext As TradingContext
        Friend ResourceContext As ResourceContext
    End Class

    Private mContextsStack As New Stack(Of Contexts)

    Public ReadOnly Property CurrentInitialisationContext() As InitialisationContext
        Get
            Return mContextsStack.Peek().InitialisationContext
        End Get
    End Property

    Public ReadOnly Property CurrentResourceContext() As ResourceContext
        Get
            Return mContextsStack.Peek().ResourceContext
        End Get
    End Property

    Public ReadOnly Property CurrentStrategyRunner() As StrategyRunner
        Get
            Return mContextsStack.Peek().StrategyRunner
        End Get
    End Property

    Public ReadOnly Property CurrentStrategy() As Object
        Get
            Return mContextsStack.Peek().Strategy
        End Get
    End Property

    Public ReadOnly Property CurrentTradingContext() As TradingContext
        Get
            Return mContextsStack.Peek().TradingContext
        End Get
    End Property

    Public Sub ClearStrategyRunner()
        Dim lContexts = mContextsStack.Pop()
        lContexts.InitialisationContext = Nothing
        lContexts.ResourceContext = Nothing
        lContexts.Strategy = Nothing
        lContexts.StrategyRunner = Nothing
        lContexts.TradingContext = Nothing
    End Sub

    Public Sub SetStrategyRunner(pStrategyRunner As StrategyRunner, pInitialisationContext As InitialisationContext, pTradingContext As TradingContext, pResourceContext As ResourceContext, pStrategy As Object)
        Dim lContexts As New Contexts
        lContexts.StrategyRunner = pStrategyRunner
        lContexts.InitialisationContext = pInitialisationContext
        lContexts.TradingContext = pTradingContext
        lContexts.ResourceContext = pResourceContext
        lContexts.Strategy = pStrategy
        mContextsStack.Push(lContexts)
    End Sub

End Module
