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
Imports TWUtilities40

Public MustInherit Class SingleInstrumentStrategyBase
    Implements IBracketOrderEventSink
    Implements IStrategy

#Region "IBracketOrderEventSink Interface Members"

    Protected Overridable Sub NotifyBracketOrderCompletion(ByVal BracketOrder As AutoTradingEnvironment.BracketOrder) Implements IBracketOrderEventSink.NotifyBracketOrderCompletion
        CheckTradingOpportunity()
    End Sub

    Protected Overridable Sub NotifyBracketOrderFill(ByVal BracketOrder As AutoTradingEnvironment.BracketOrder) Implements IBracketOrderEventSink.NotifyBracketOrderFill
        ' nothing to do here
    End Sub

    Protected Overridable Sub NotifyBracketOrderStopLossAdjusted(ByVal BracketOrder As AutoTradingEnvironment.BracketOrder) Implements IBracketOrderEventSink.NotifyBracketOrderStopLossAdjusted
        ' nothing to do here
    End Sub

#End Region

#Region "IStrategy Interface Members"

    Protected MustOverride Sub DefineDefaultParameters() Implements IStrategy.DefineDefaultParameters

    Private Sub Initialise(ByVal pContext As InitialisationContext) Implements IStrategy.Initialise
        ProcessParameters(pContext)
        DeclareResources(pContext)
    End Sub

    Protected MustOverride ReadOnly Property Name() As String Implements IStrategy.Name

    Private ReadOnly Property Key As String Implements IStrategy.Key
        Get
            Static sKey As String = (New Guid).ToString
            Return sKey
        End Get
    End Property

    Protected Overridable Sub NotifyNoLivePositions() Implements IStrategy.NotifyNoLivePositions
        CheckTradingOpportunity()
    End Sub

    Protected Overridable Sub NotifyNoSimulatedPositions() Implements IStrategy.NotifyNoSimulatedPositions
    End Sub

    Protected Overridable Sub NotifySimulatedTradingReadinessChange() Implements IStrategy.NotifySimulatedTradingReadinessChange
    End Sub

    Private Sub NotifyTick(ByVal pType As TickType) Implements IStrategy.NotifyTick
        If IsEndOfSession() Then Exit Sub
        If AcceptTick() Then
            InitialiseTick()
            CheckTradingOpportunity()
        End If
    End Sub

    Protected Overridable Sub NotifyTradingReadinessChange() Implements IStrategy.NotifyTradingReadinessChange
    End Sub

    Protected Overridable Sub Start(ByVal pTradingContext As TradingContext) Implements IStrategy.Start
    End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Protected MustOverride Function AcceptTick() As Boolean

    Protected Overridable Function CancelPendingBuyOrders() As Boolean
        Return False
    End Function

    Protected Overridable Function CancelPendingSellOrders() As Boolean
        Return False
    End Function

    Protected Sub CheckTradingOpportunity()
        If IsBuySignal() Then
            If CancelPendingSellOrders() Then
                ' do nothing till pending orders cancelled
            Else
                If Not IsPendingBuyOrders() Then
                    PlaceBuyOrders()
                Else
                    ModifyPendingBuyOrders()
                End If
            End If
        ElseIf IsSellSignal() Then
            If CancelPendingBuyOrders() Then
                ' do nothing till pending orders cancelled
            Else
                If Not IsPendingSellOrders() Then
                    PlaceSellOrders()
                Else
                    ModifyPendingSellOrders()
                End If
            End If
        End If
    End Sub

    Protected Overridable Sub DeclareResources(pContext As InitialisationContext)
    End Sub

    Protected Overridable Sub InitialiseTick()
    End Sub

    Protected Overridable Function IsBuySignal() As Boolean
        Return False
    End Function

    Protected Overridable Function IsEndOfSession() As Boolean
        Return False
    End Function

    Private Function IsPendingBuyOrders() As Boolean
        IsPendingBuyOrders = PendingSimulatedBuySize <> 0
    End Function

    Private Function IsPendingSellOrders() As Boolean
        IsPendingSellOrders = PendingSimulatedSellSize <> 0
    End Function

    Protected Overridable Function IsSellSignal() As Boolean
        Return False
    End Function

    Protected Overridable Sub ModifyPendingBuyOrders()
    End Sub

    Protected Overridable Sub ModifyPendingSellOrders()
    End Sub

    Protected Overridable Sub PlaceBuyOrders()
    End Sub

    Protected Overridable Sub PlaceSellOrders()
    End Sub

    Protected Overridable Sub ProcessParameters(pContext As InitialisationContext)
    End Sub

#End Region

#Region "Helper Functions"

#End Region

End Class
