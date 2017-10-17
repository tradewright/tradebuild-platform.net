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
Imports TickUtils27

Public Enum EntryOrderType
    None = BracketEntryTypes.BracketEntryTypeNone
    Market = BracketEntryTypes.BracketEntryTypeMarket
    MarketOnOpen = BracketEntryTypes.BracketEntryTypeMarketOnOpen
    MarketOnClose = BracketEntryTypes.BracketEntryTypeMarketOnClose
    MarketIfTouched = BracketEntryTypes.BracketEntryTypeMarketIfTouched
    MarketToLimit = BracketEntryTypes.BracketEntryTypeMarketToLimit
    Bid = BracketEntryTypes.BracketEntryTypeBid
    Ask = BracketEntryTypes.BracketEntryTypeAsk
    Last = BracketEntryTypes.BracketEntryTypeLast
    Limit = BracketEntryTypes.BracketEntryTypeLimit
    LimitOnOpen = BracketEntryTypes.BracketEntryTypeLimitOnOpen
    LimitOnClose = BracketEntryTypes.BracketEntryTypeLimitOnClose
    LimitIfTouched = BracketEntryTypes.BracketEntryTypeLimitIfTouched
    [Stop] = BracketEntryTypes.BracketEntryTypeStop
    StopLimit = BracketEntryTypes.BracketEntryTypeStopLimit
End Enum

Public Enum OrderRole
    Entry = BracketOrderRoles.BracketOrderRoleEntry
    StopLoss = BracketOrderRoles.BracketOrderRoleStopLoss
    Target = BracketOrderRoles.BracketOrderRoleTarget
End Enum

Public Enum OrderTIF
    None = OrderTIFs.OrderTIFNone
    Day = OrderTIFs.OrderTIFDay
    GoodTillCancelled = OrderTIFs.OrderTIFGoodTillCancelled
    ImmediateOrCancel = OrderTIFs.OrderTIFImmediateOrCancel
End Enum

Public Enum StopLossOrderType
    None = BracketStopLossTypes.BracketStopLossTypeNone
    [Stop] = BracketStopLossTypes.BracketStopLossTypeStop
    StopLimit = BracketStopLossTypes.BracketStopLossTypeStopLimit
    Bid = BracketStopLossTypes.BracketStopLossTypeBid
    Ask = BracketStopLossTypes.BracketStopLossTypeAsk
    Last = BracketStopLossTypes.BracketStopLossTypeLast
    Auto = BracketStopLossTypes.BracketStopLossTypeAuto
End Enum

Public Enum TargetOrderType
    None = BracketTargetTypes.BracketTargetTypeNone
    Limit = BracketTargetTypes.BracketTargetTypeLimit
    LimitIfTouched = BracketTargetTypes.BracketTargetTypeLimitIfTouched
    MarketIfTouched = BracketTargetTypes.BracketTargetTypeMarketIfTouched
    Bid = BracketTargetTypes.BracketTargetTypeBid
    Ask = BracketTargetTypes.BracketTargetTypeAsk
    Last = BracketTargetTypes.BracketTargetTypeLast
    Auto = BracketTargetTypes.BracketTargetTypeAuto
End Enum

Public Enum TickType
    Bid = TickTypes.TickTypeBid
    Ask = TickTypes.TickTypeAsk
    ClosePrice = TickTypes.TickTypeClosePrice
    HighPrice = TickTypes.TickTypeHighPrice
    LowPrice = TickTypes.TickTypeLowPrice
    MarketDepth = TickTypes.TickTypeMarketDepth
    MarketDepthReset = TickTypes.TickTypeMarketDepthReset
    Trade = TickTypes.TickTypeTrade
    Volume = TickTypes.TickTypeVolume
    OpenInterest = TickTypes.TickTypeOpenInterest
    OpenPrice = TickTypes.TickTypeOpenPrice
End Enum

