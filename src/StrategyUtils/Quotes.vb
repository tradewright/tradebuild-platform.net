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


Public Module Quotes

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "Quotes"

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

    Public ReadOnly Property AskPrice() As Double
        Get
            AskPrice = CurrentTradingContext.AskQuote.Price
        End Get
    End Property

    Public ReadOnly Property AskSize() As Integer
        Get
            AskSize = CurrentTradingContext.AskQuote.Size
        End Get
    End Property

    Public ReadOnly Property AskTimestamp() As Date
        Get
            AskTimestamp = CurrentTradingContext.AskQuote.Timestamp
        End Get
    End Property

    Public ReadOnly Property BidPrice() As Double
        Get
            BidPrice = CurrentTradingContext.BidQuote.Price
        End Get
    End Property

    Public ReadOnly Property BidSize() As Integer
        Get
            BidSize = CurrentTradingContext.BidQuote.Size
        End Get
    End Property

    Public ReadOnly Property BidTimestamp() As Date
        Get
            BidTimestamp = CurrentTradingContext.BidQuote.Timestamp
        End Get
    End Property

    Public ReadOnly Property ClosePrice() As Double
        Get
            ClosePrice = CurrentTradingContext.CloseQuote.Price
        End Get
    End Property

    Public ReadOnly Property CloseTimestamp() As Date
        Get
            CloseTimestamp = CurrentTradingContext.CloseQuote.Timestamp
        End Get
    End Property

    Public ReadOnly Property HighPrice() As Double
        Get
            HighPrice = CurrentTradingContext.HighQuote.Price
        End Get
    End Property

    Public ReadOnly Property HighTimestamp() As Date
        Get
            HighTimestamp = CurrentTradingContext.HighQuote.Timestamp
        End Get
    End Property

    Public ReadOnly Property LowPrice() As Double
        Get
            LowPrice = CurrentTradingContext.LowQuote.Price
        End Get
    End Property

    Public ReadOnly Property LowTimestamp() As Date
        Get
            LowTimestamp = CurrentTradingContext.LowQuote.Timestamp
        End Get
    End Property

    Public ReadOnly Property OpenPrice() As Double
        Get
            OpenPrice = CurrentTradingContext.OpenQuote.Price
        End Get
    End Property

    Public ReadOnly Property OpenTimestamp() As Date
        Get
            OpenTimestamp = CurrentTradingContext.OpenQuote.Timestamp
        End Get
    End Property

    Public ReadOnly Property IsCurrentTickAsk() As Boolean
        Get
            IsCurrentTickAsk = (CurrentTradingContext.CurrentTick.TickType = TickUtils27.TickTypes.TickTypeAsk)
        End Get
    End Property

    Public ReadOnly Property IsCurrentTickBid() As Boolean
        Get
            IsCurrentTickBid = (CurrentTradingContext.CurrentTick.TickType = TickUtils27.TickTypes.TickTypeBid)
        End Get
    End Property

    Public ReadOnly Property IsCurrentTickTrade() As Boolean
        Get
            IsCurrentTickTrade = (CurrentTradingContext.CurrentTick.TickType = TickUtils27.TickTypes.TickTypeTrade)
        End Get
    End Property

    Public ReadOnly Property IsCurrentTickVolume() As Boolean
        Get
            IsCurrentTickVolume = (CurrentTradingContext.CurrentTick.TickType = TickUtils27.TickTypes.TickTypeVolume)
        End Get
    End Property

    Public ReadOnly Property TradePrice() As Double
        Get
            TradePrice = CurrentTradingContext.TradeQuote.Price
        End Get
    End Property

    Public ReadOnly Property TradeSize() As Integer
        Get
            TradeSize = CurrentTradingContext.TradeQuote.Size
        End Get
    End Property

    Public ReadOnly Property TradeTimestamp() As Date
        Get
            TradeTimestamp = CurrentTradingContext.TradeQuote.Timestamp
        End Get
    End Property

    Public ReadOnly Property Volume() As Integer
        Get
            Volume = CurrentTradingContext.VolumeQuote.Size
        End Get
    End Property

    Public ReadOnly Property VolumeTimestamp() As Date
        Get
            VolumeTimestamp = CurrentTradingContext.VolumeQuote.Timestamp
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub StartTickData(pStrategy As IPositionManagementStrategy)
        CurrentTradingContext.StartTickData(pStrategy)
    End Sub

    Public Sub StopTickData(pStrategy As IPositionManagementStrategy)
        CurrentTradingContext.StopTickData(pStrategy)
    End Sub

#End Region

#Region "Helper Functions"
#End Region

End Module