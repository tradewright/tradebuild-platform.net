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

Imports ChartSkil27
Imports MarketDataUtils27
Imports TickUtils27

Imports System.Runtime.InteropServices.Marshal

Public Class TickerBidAskHandler
    Implements IQuoteListener

    Private Shared sBidAskHandlers As New List(Of TickerBidAskHandler)

    Private ReadOnly _Periods As Periods
    Private ReadOnly _Ticker As IMarketDataSource

    Private _BidLine As ChartSkil27.Line
    Private _BidLine1 As ChartSkil27.Line

    Private _AskLine As ChartSkil27.Line
    Private _AskLine1 As ChartSkil27.Line

    Private _PriceRegion As ChartRegion
    Private _Yaxis As ChartRegion

    Sub New(chart As MarketChart, ticker As IMarketDataSource)
        _Periods = chart.Periods
        _Ticker = ticker
        _PriceRegion = chart.PriceRegion
        setupBidAndAskLines()
    End Sub

    Public Shared Function Attach(chart As MarketChart, ticker As IMarketDataSource) As TickerBidAskHandler
        'Dim fh As New FutureHandler(Of IContract, Object)(ticker.ContractFuture,
        '                                          completionAction:=Sub(c, o)
        '                                                                If c.Specifier.SecType <> SecurityTypes.SecTypeIndex Then
        '                                                                    Dim bah = New TickerBidAskHandler(chart, ticker)
        '                                                                    sBidAskHandlers.Add(bah)
        '                                                                    bah.Show()
        '                                                                End If
        '                                                            End Sub)
        Dim bah = New TickerBidAskHandler(chart, ticker)
        sBidAskHandlers.Add(bah)
        bah.Show()
        Return bah
    End Function

    Sub Finish()
        _Ticker.RemoveQuoteListener(Me)
        clearFields()
    End Sub

    Sub Hide()
        setAskLinePrice(0)
        setBidLinePrice(0)
        _Ticker.RemoveQuoteListener(Me)
    End Sub

    Sub Show()
        If _Ticker.State = MarketDataSourceStates.MarketDataSourceStateRunning Then
            setAskLinePrice(_Ticker.CurrentTick(TickTypes.TickTypeAsk).Price)
            setBidLinePrice(_Ticker.CurrentTick(TickTypes.TickTypeBid).Price)
        End If
        _Ticker.AddQuoteListener(Me)
    End Sub

    Private Sub clearFields()
        'ReleaseComObject(_Ticker)

        'ReleaseComObject(_BidLine)
        _BidLine = Nothing

        'ReleaseComObject(_BidLine1)
        _BidLine1 = Nothing

        'ReleaseComObject(_AskLine)
        _AskLine = Nothing

        'ReleaseComObject(_AskLine1)
        _AskLine1 = Nothing

        'ReleaseComObject(_PriceRegion)
        _PriceRegion = Nothing

        'ReleaseComObject(_Yaxis)
        _Yaxis = Nothing

    End Sub

    Private Sub setAskLinePrice(price As Double)
        _AskLine.SetPosition(ChartSkil.NewPoint(1, price), ChartSkil.NewPoint(98, price))
        If price = 0 Then
            _AskLine1.SetPosition(ChartSkil.NewPoint(-1000, 0), ChartSkil.NewPoint(-1000, 0))
        Else
            Dim margin = ChartSkil.NewDimension(0.5).LengthLogicalY(_PriceRegion)
            _AskLine1.SetPosition(ChartSkil.NewPoint(_Periods.CurrentPeriodNumber, price + margin), ChartSkil.NewPoint(_Periods.CurrentPeriodNumber - 10, price + margin))
        End If
    End Sub

    Private Sub setBidLinePrice(price As Double)
        _BidLine.SetPosition(ChartSkil.NewPoint(1, price), ChartSkil.NewPoint(98, price))
        If price = 0 Then
            _BidLine1.SetPosition(ChartSkil.NewPoint(-1000, 0), ChartSkil.NewPoint(-1000, 0))
        Else
            Dim margin = ChartSkil.NewDimension(0.5).LengthLogicalY(_PriceRegion)
            _BidLine1.SetPosition(ChartSkil.NewPoint(_Periods.CurrentPeriodNumber, price - margin), ChartSkil.NewPoint(_Periods.CurrentPeriodNumber - 10, price - margin))
        End If
    End Sub

    Private Sub setupBidAndAskLines()

        _Yaxis = _PriceRegion.YAxisRegion

        _AskLine = _Yaxis.AddLine(LayerNumbers.LayerGrid)
        _AskLine.Color = RGB(255, 0, 0)
        _AskLine.Extended = True

        _AskLine1 = _PriceRegion.AddLine(LayerNumbers.LayerInvisible)
        _AskLine1.FixedX = True
        _AskLine1.IncludeInAutoscale = True

        _BidLine = _Yaxis.AddLine(LayerNumbers.LayerGrid)
        _BidLine.Color = RGB(0, 0, 255)
        _BidLine.Extended = True

        _BidLine1 = _PriceRegion.AddLine(LayerNumbers.LayerInvisible)
        _BidLine1.FixedX = True
        _BidLine1.IncludeInAutoscale = True

    End Sub

    Private Sub Ask(ByRef ev As QuoteEventData) Implements _IQuoteListener.Ask
        Try
            setAskLinePrice(ev.Quote.Price)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Bid(ByRef ev As QuoteEventData) Implements _IQuoteListener.Bid
        Try
            setBidLinePrice(ev.Quote.Price)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ignore(ByRef ev As QuoteEventData) Implements _IQuoteListener.High,
                                                                        _IQuoteListener.Low,
                                                                        _IQuoteListener.OpenInterest,
                                                                        _IQuoteListener.PreviousClose,
                                                                        _IQuoteListener.SessionOpen,
                                                                        _IQuoteListener.Trade,
                                                                        _IQuoteListener.Volume

    End Sub

End Class
