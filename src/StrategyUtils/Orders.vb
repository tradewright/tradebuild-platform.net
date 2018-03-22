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

Public Module Orders

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "Orders"

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

    Public ReadOnly Property AccountBalance() As Double
        Get
            ' dummy value at present, awaiting development of an Account Service Provider
            AccountBalance = 10000
        End Get
    End Property

    Public ReadOnly Property ActiveBuySize() As Integer
        Get
            ActiveBuySize = getDefaultOrderContext(True, False).ActiveSize
        End Get
    End Property

    Public ReadOnly Property ActiveSellSize() As Integer
        Get
            ActiveSellSize = getDefaultOrderContext(False, False).ActiveSize
        End Get
    End Property

    Public ReadOnly Property ActiveSimulatedBuySize() As Integer
        Get
            ActiveSimulatedBuySize = getDefaultOrderContext(True, True).ActiveSize
        End Get
    End Property

    Public ReadOnly Property ActiveSimulatedSellSize() As Integer
        Get
            ActiveSimulatedSellSize = getDefaultOrderContext(False, True).ActiveSize
        End Get
    End Property

    Public ReadOnly Property ActiveSize(OrderContext As OrderContext) As Integer
        Get
            ActiveSize = OrderContext.OrderContext.ActiveSize
        End Get
    End Property

    Public ReadOnly Property AveragePrice(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Double
        Get
            AveragePrice = getBracketOrder(BracketOrder).EntryOrder.AveragePrice
        End Get
    End Property

    Public ReadOnly Property EntryPrice(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Double
        Get
            EntryPrice = getBracketOrder(BracketOrder).EntryOrder.LimitPrice
        End Get
    End Property

    Public ReadOnly Property EntryOffset(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Integer
        Get
            EntryOffset = getBracketOrder(BracketOrder).EntryOrder.Offset
        End Get
    End Property

    Public ReadOnly Property EntryTriggerPrice(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Double
        Get
            EntryTriggerPrice = getBracketOrder(BracketOrder).EntryOrder.TriggerPrice
        End Get
    End Property

    Public ReadOnly Property IsBuy(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Boolean
        Get
            IsBuy = getBracketOrder(BracketOrder).LongPosition
        End Get
    End Property

    Public ReadOnly Property IsCompleted(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Boolean
        Get
            IsCompleted = (getBracketOrder(BracketOrder).State = BracketOrderStates.BracketOrderStateClosed)
        End Get
    End Property

    Public ReadOnly Property IsSell(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Boolean
        Get
            IsSell = Not getBracketOrder(BracketOrder).LongPosition
        End Get
    End Property

    Public ReadOnly Property PendingBuySize() As Integer
        Get
            PendingBuySize = getDefaultOrderContext(True, False).PendingSize
        End Get
    End Property

    Public ReadOnly Property PendingSellSize() As Integer
        Get
            PendingSellSize = getDefaultOrderContext(False, False).PendingSize
        End Get
    End Property

    Public ReadOnly Property PendingSimulatedBuySize() As Integer
        Get
            PendingSimulatedBuySize = getDefaultOrderContext(True, True).PendingSize
        End Get
    End Property

    Public ReadOnly Property PendingSimulatedSellSize() As Integer
        Get
            PendingSimulatedSellSize = getDefaultOrderContext(False, True).PendingSize
        End Get
    End Property

    Public ReadOnly Property PendingSize(OrderContext As OrderContext) As Integer
        Get
            PendingSize = OrderContext.OrderContext.PendingSize
        End Get
    End Property

    Public ReadOnly Property PrimaryBracketOrder() As AutoTradingEnvironment.BracketOrder
        Get
            PrimaryBracketOrder = CurrentResourceContext.PrimaryBracketOrder
        End Get
    End Property

    Public ReadOnly Property QuantityFilled(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Integer
        Get
            QuantityFilled = getBracketOrder(BracketOrder).EntryOrder.QuantityFilled
        End Get
    End Property

    Public ReadOnly Property Size(OrderContext As OrderContext) As Integer
        Get
            Size = OrderContext.OrderContext.Size
        End Get
    End Property

    Public ReadOnly Property StopLossPrice(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Double
        Get
            StopLossPrice = getBracketOrder(BracketOrder).StopLossOrder.LimitPrice
        End Get
    End Property

    Public ReadOnly Property StopLossOffset(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Integer
        Get
            StopLossOffset = getBracketOrder(BracketOrder).StopLossOrder.Offset
        End Get
    End Property

    Public ReadOnly Property StopLossTriggerPrice(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Double
        Get
            StopLossTriggerPrice = getBracketOrder(BracketOrder).StopLossOrder.TriggerPrice
        End Get
    End Property

    Public ReadOnly Property TargetPrice(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Double
        Get
            TargetPrice = getBracketOrder(BracketOrder).TargetOrder.LimitPrice
        End Get
    End Property

    Public ReadOnly Property TargetOffset(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Integer
        Get
            TargetOffset = getBracketOrder(BracketOrder).TargetOrder.Offset
        End Get
    End Property

    Public ReadOnly Property TargetTriggerPrice(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing) As Double
        Get
            TargetTriggerPrice = getBracketOrder(BracketOrder).TargetOrder.TriggerPrice
        End Get
    End Property

    Public ReadOnly Property TotalPendingPositionSize() As Integer
        Get
            TotalPendingPositionSize = CurrentTradingContext.PositionManager.PendingPositionSize
        End Get
    End Property

    Public ReadOnly Property TotalPositionSize() As Integer
        Get
            TotalPositionSize = CurrentTradingContext.PositionManager.PositionSize
        End Get
    End Property

    Public ReadOnly Property TotalSimulatedPendingPositionSize() As Integer
        Get
            TotalSimulatedPendingPositionSize = CurrentTradingContext.PositionManagerSimulated.PendingPositionSize
        End Get
    End Property

    Public ReadOnly Property TotalSimulatedPositionSize() As Integer
        Get
            TotalSimulatedPositionSize = CurrentTradingContext.PositionManagerSimulated.PositionSize
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub AdjustStop(StopTriggerPrice As Double, Optional Quantity As Integer = -1, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).AdjustStop(StopTriggerPrice, Quantity)
    End Sub

    Public Function Buy(Number As Integer,
                        EntryOrder As OrderSpecifier,
                        Optional StopLossOrder As OrderSpecifier = Nothing,
                        Optional TargetOrder As OrderSpecifier = Nothing,
                        Optional CancelPrice As Double = 0,
                        Optional CancelAfter As Integer = 0,
                        Optional NotifyCompletion As Boolean = False,
                        Optional OrderContext As OrderContext = Nothing) As AutoTradingEnvironment.BracketOrder
        Buy = buyOrSell(True, OrderContext, False, Number, EntryOrder, StopLossOrder, TargetOrder, CancelPrice, CancelAfter, NotifyCompletion)
    End Function

    Public Function BuySimulated(Number As Integer,
                                 EntryOrder As OrderSpecifier,
                                 Optional StopLossOrder As OrderSpecifier = Nothing,
                                 Optional TargetOrder As OrderSpecifier = Nothing,
                                 Optional CancelPrice As Double = 0,
                                 Optional CancelAfter As Integer = 0,
                                 Optional NotifyCompletion As Boolean = False,
                                 Optional OrderContext As OrderContext = Nothing) As AutoTradingEnvironment.BracketOrder
        BuySimulated = buyOrSell(True, OrderContext, True, Number, EntryOrder, StopLossOrder, TargetOrder, CancelPrice, CancelAfter, NotifyCompletion)
    End Function

    Public Sub CancelBracketOrder(Optional EvenIfFilled As Boolean = False, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).Cancel(EvenIfFilled)
    End Sub

    Public Function CanTrade(Optional OrderContext As OrderContext = Nothing) As Boolean
        If OrderContext Is Nothing Then
            CanTrade = canTradeContext(Nothing, True, False) And canTradeContext(Nothing, False, False)
        Else
            ' note the value of pIsBuy in this call is irrelevant
            CanTrade = canTradeContext(OrderContext, False, False)
        End If
    End Function

    Public Function CanTradeSimulated(Optional OrderContext As OrderContext = Nothing) As Boolean
        If OrderContext Is Nothing Then
            CanTradeSimulated = canTradeContext(Nothing, True, True) And canTradeContext(Nothing, False, True)
        Else
            ' note the value of pIsBuy in this call is irrelevant
            CanTradeSimulated = canTradeContext(OrderContext, False, True)
        End If
    End Function

    Public Sub ClearPrimaryBracketOrder()
        CurrentResourceContext.ClearPrimaryBracketOrder()
    End Sub

    Public Sub CloseAllPositions()
        CurrentTradingContext.PositionManager.ClosePositions()
    End Sub

    Public Sub CloseAllSimulatedPositions()
        CurrentTradingContext.PositionManagerSimulated.ClosePositions()
    End Sub

    Public Sub AllowUnprotectedPositions()
        Assert(Not CurrentInitialisationContext Is Nothing, "Method can only be called during strategy initialisation")

        CurrentInitialisationContext.AllowUnprotectedPositions = True

        For Each oc As OrderUtils27.OrderContext In CurrentInitialisationContext.PositionManager.OrderContexts
            oc.PreventUnprotectedPositions = False
        Next oc
    End Sub

    Public Function Sell(Number As Integer,
                         EntryOrder As OrderSpecifier,
                         Optional StopLossOrder As OrderSpecifier = Nothing,
                         Optional TargetOrder As OrderSpecifier = Nothing,
                         Optional CancelPrice As Double = 0,
                         Optional CancelAfter As Integer = 0,
                         Optional NotifyCompletion As Boolean = False,
                         Optional OrderContext As OrderContext = Nothing) As AutoTradingEnvironment.BracketOrder
        Sell = buyOrSell(False, OrderContext, False, Number, EntryOrder, StopLossOrder, TargetOrder, CancelPrice, CancelAfter, NotifyCompletion)
    End Function

    Public Function SellSimulated(Number As Integer,
                                  EntryOrder As OrderSpecifier,
                                  Optional StopLossOrder As OrderSpecifier = Nothing,
                                  Optional TargetOrder As OrderSpecifier = Nothing,
                                  Optional CancelPrice As Double = 0,
                                  Optional CancelAfter As Integer = 0,
                                  Optional NotifyCompletion As Boolean = False,
                                  Optional OrderContext As OrderContext = Nothing) As AutoTradingEnvironment.BracketOrder
        SellSimulated = buyOrSell(False, OrderContext, True, Number, EntryOrder, StopLossOrder, TargetOrder, CancelPrice, CancelAfter, NotifyCompletion)
    End Function

    Public Sub SetEntryReason(Reason As String, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).EntryReason = Reason
    End Sub

    Public Sub SetNewEntryPrice(Price As Double, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewEntryPrice(Price)
    End Sub

    Public Sub SetNewEntryTriggerPrice(Price As Double, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewEntryTriggerPrice(Price)
    End Sub

    Public Sub SetNewQuantity(Quantity As Integer, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewQuantity(Quantity)
    End Sub

    Public Sub SetNewStopLossOffset(Offset As Integer, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewStopLossOffset(Offset)
    End Sub

    Public Sub SetNewStopLossPrice(Price As Double, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewStopLossPrice(Price)
    End Sub

    Public Sub SetNewStopLossTriggerPrice(Price As Double, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewStopLossTriggerPrice(Price)
    End Sub

    Public Sub SetNewTargetOffset(Offset As Integer, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewTargetOffset(Offset)
    End Sub

    Public Sub SetNewTargetPrice(Price As Double, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewTargetPrice(Price)
    End Sub

    Public Sub SetNewTargetTriggerPrice(Price As Double, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).SetNewTargetTriggerPrice(Price)
    End Sub

    Public Sub SetPrimaryBracketOrder(BracketOrder As AutoTradingEnvironment.BracketOrder)
        CurrentResourceContext.SetPrimaryBracketOrder(BracketOrder)
    End Sub

    Public Sub SetStopReason(Reason As String, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).StopReason = Reason
    End Sub

    Public Sub SetTargetReason(Reason As String, Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).TargetReason = Reason
    End Sub

    Public Sub Update(Optional BracketOrder As AutoTradingEnvironment.BracketOrder = Nothing)
        getBracketOrder(BracketOrder).Update()
    End Sub


#End Region

#Region "Helper Functions"

    Private Function buyOrSell(pIsBuy As Boolean,
                               pOrderContext As OrderContext,
                               pIsSimulated As Boolean,
                               pNumber As Integer,
                               pEntryOrder As OrderSpecifier,
                               pStopLossOrder As OrderSpecifier,
                               pTargetOrder As OrderSpecifier,
                               pCancelPrice As Double,
                               pCancelAfter As Integer,
                               pNotifyCompletion As Boolean) As AutoTradingEnvironment.BracketOrder
        Assert(Not CurrentTradingContext Is Nothing, "Method can only be called during strategy execution")
        AssertArgument(pNumber > 0, "Number must be greater than 0")
        AssertArgument(Not pEntryOrder Is Nothing, "pEntryOrder must be supplied")

        If pStopLossOrder Is Nothing Then pStopLossOrder = DeclareStopLossOrder(StopLossOrderType.None)
        If pTargetOrder Is Nothing Then pTargetOrder = DeclareTargetOrder(TargetOrderType.None)

        Dim lOrderContext As OrderUtils27.OrderContext
        If pOrderContext Is Nothing Then
            lOrderContext = getDefaultOrderContext(pIsBuy, pIsSimulated)
        Else
            lOrderContext = pOrderContext.OrderContext
            Assert(lOrderContext.IsSimulated = pIsSimulated, "Order context has incorrect simulated property")
        End If

        validateOrderSpecifier(pEntryOrder, OrderRole.Entry)
        validateOrderSpecifier(pStopLossOrder, OrderRole.StopLoss)
        validateOrderSpecifier(pTargetOrder, OrderRole.Target)

        Dim lBracketOrder As _IBracketOrder
        If pIsBuy Then
            lBracketOrder = doBuy(lOrderContext, pNumber, pEntryOrder, pStopLossOrder, pTargetOrder, pCancelPrice, pCancelAfter)
        Else
            lBracketOrder = doSell(lOrderContext, pNumber, pEntryOrder, pStopLossOrder, pTargetOrder, pCancelPrice, pCancelAfter)
        End If

        buyOrSell = New AutoTradingEnvironment.BracketOrder(lBracketOrder)
        CurrentStrategyRunner.MapCOMBracketOrderToBracketOrder(lBracketOrder, buyOrSell)

        requestNotification(lBracketOrder, pNotifyCompletion)
        createAttachedStrategies(lBracketOrder)
    End Function

    Private Function canTradeContext(pOrderContext As OrderContext, pIsBuy As Boolean, pIsSimulated As Boolean) As Boolean
        Dim lOrderContext As OrderUtils27.OrderContext
        If pOrderContext Is Nothing Then
            lOrderContext = getDefaultOrderContext(pIsBuy, pIsSimulated)
        Else
            lOrderContext = pOrderContext.OrderContext
            Assert(lOrderContext.IsSimulated = pIsSimulated, "Order context has incorrect simulated property")
        End If

        canTradeContext = lOrderContext.IsReady
    End Function

    Private Sub createAttachedStrategies(pBracketOrder As _IBracketOrder)
        Dim lFactory As IPositionManagementStrategyFactory
        If TypeOf CurrentStrategy Is IStrategy Then
            For Each lFactory In CurrentStrategyRunner.PositionManagementStrategyFactories
                CurrentTradingContext.ApplyPositionManagementStrategy(pBracketOrder, lFactory.CreateStrategy(CurrentTradingContext), New ResourceContext(CurrentStrategyRunner.GetPositionManagementStrategyResourceContext(lFactory)))
            Next lFactory
        End If
    End Sub

    Private Function doBuy(pOrderContext As OrderUtils27.OrderContext,
                           pNumber As Integer,
                           pEntryOrderSpec As OrderSpecifier,
                           pStopLossOrderSpec As OrderSpecifier,
                           pTargetOrderSpec As OrderSpecifier,
                           pCancelPrice As Double,
                           pCancelAfter As Integer) As _IBracketOrder
        Dim entryType = CType(pEntryOrderSpec.OrderType, BracketEntryTypes)
        Dim entryTIF = CType(pEntryOrderSpec.TIF, OrderTIFs)

        Dim stopLossType = CType(pStopLossOrderSpec.OrderType, BracketStopLossTypes)
        Dim stopLossTIF = CType(pStopLossOrderSpec.TIF, OrderTIFs)

        Dim targetType = CType(pTargetOrderSpec.OrderType, BracketTargetTypes)
        Dim targetTIF = CType(pTargetOrderSpec.TIF, OrderTIFs)

        Return pOrderContext.Buy(pNumber,
                                entryType,
                                pEntryOrderSpec.Price,
                                pEntryOrderSpec.Offset,
                                pEntryOrderSpec.TriggerPrice,
                                stopLossType,
                                pStopLossOrderSpec.TriggerPrice,
                                pStopLossOrderSpec.Offset,
                                pStopLossOrderSpec.Price,
                                targetType,
                                pTargetOrderSpec.Price,
                                pTargetOrderSpec.Offset,
                                pTargetOrderSpec.TriggerPrice,
                                entryTIF,
                                stopLossTIF,
                                targetTIF,
                                pCancelPrice,
                                pCancelAfter)
    End Function

    Private Function doSell(pOrderContext As OrderUtils27.OrderContext,
                            pNumber As Integer,
                            pEntryOrderSpec As OrderSpecifier,
                            pStopLossOrderSpec As OrderSpecifier,
                            pTargetOrderSpec As OrderSpecifier,
                            pCancelPrice As Double,
                            pCancelAfter As Integer) As _IBracketOrder
        Dim entryType = CType(pEntryOrderSpec.OrderType, BracketEntryTypes)
        Dim entryTIF = CType(pEntryOrderSpec.TIF, OrderTIFs)

        Dim stopLossType = CType(pStopLossOrderSpec.OrderType, BracketStopLossTypes)
        Dim stopLossTIF = CType(pStopLossOrderSpec.TIF, OrderTIFs)

        Dim targetType = CType(pTargetOrderSpec.OrderType, BracketTargetTypes)
        Dim targetTIF = CType(pTargetOrderSpec.TIF, OrderTIFs)

        Return pOrderContext.Sell(pNumber,
                                entryType,
                                pEntryOrderSpec.Price,
                                pEntryOrderSpec.Offset,
                                pEntryOrderSpec.TriggerPrice,
                                stopLossType,
                                pStopLossOrderSpec.TriggerPrice,
                                pStopLossOrderSpec.Offset,
                                pStopLossOrderSpec.Price,
                                targetType,
                                pTargetOrderSpec.Price,
                                pTargetOrderSpec.Offset,
                                pTargetOrderSpec.TriggerPrice,
                                entryTIF,
                                stopLossTIF,
                                targetTIF,
                                pCancelPrice,
                                pCancelAfter)
    End Function

    Private Function getBracketOrder(pBracketOrder As AutoTradingEnvironment.BracketOrder) As _IBracketOrder
        If pBracketOrder Is Nothing Then pBracketOrder = CurrentResourceContext.PrimaryBracketOrder

        Dim lObj = pBracketOrder.BracketOrder

        Return DirectCast(lObj, _IBracketOrder)
    End Function

    Private Function getDefaultOrderContext(pIsBuy As Boolean, pIsSimulated As Boolean) As OrderUtils27.OrderContext
        Dim lOrderContext As OrderUtils27.OrderContext

        If pIsBuy Then
            If pIsSimulated Then
                lOrderContext = CurrentTradingContext.DefaultBuyOrderContextSimulated
            Else
                lOrderContext = CurrentTradingContext.DefaultBuyOrderContext
            End If
        Else
            If pIsSimulated Then
                lOrderContext = CurrentTradingContext.DefaultSellOrderContextSimulated
            Else
                lOrderContext = CurrentTradingContext.DefaultSellOrderContext
            End If
        End If

        getDefaultOrderContext = lOrderContext
    End Function

    Private Sub requestNotification(pBracketOrder As _IBracketOrder, pNotifyCompletion As Boolean)
        If pNotifyCompletion Then CurrentStrategyRunner.RequestBracketOrderNotification(pBracketOrder, CurrentStrategy, CurrentResourceContext)
    End Sub

    Private Sub validateOrderSpecifier(pOrderSpec As OrderSpecifier, pRole As OrderRole)
        AssertArgument(pOrderSpec.OrderRole = pRole, "Order specifier not correct role (entry, stop-loss or target)")
    End Sub

#End Region

End Module