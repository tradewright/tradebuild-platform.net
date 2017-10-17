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

<ComClass()> Public Class ChartPriceFormatter
    Implements ChartSkil27.IPriceFormatter

    Private mSecType As SecurityType
    Private mTickSize As Double
    Private mFormatter As New Utils.Contracts.PriceFormatter()

    Public Sub New(pSecType As SecurityType, pTickSize As Double, Optional pFormatter As Utils.Contracts.PriceFormatter = Nothing)
        mSecType = pSecType
        mTickSize = pTickSize
        If pFormatter IsNot Nothing Then mFormatter = pFormatter
    End Sub

    Public Function FormatPrice(pValue As Double) As String Implements ChartSkil27._IPriceFormatter.FormatPrice
        Return mFormatter.FormatPrice(pValue, mSecType, mTickSize)
    End Function

    Public WriteOnly Property IntegerYScale() As Boolean Implements ChartSkil27._IPriceFormatter.IntegerYScale
        Set

        End Set
    End Property

    Public WriteOnly Property YScaleGridSpacing() As Double Implements ChartSkil27._IPriceFormatter.YScaleGridSpacing
        Set

        End Set
    End Property

    Public WriteOnly Property YScaleQuantum() As Double Implements ChartSkil27._IPriceFormatter.YScaleQuantum
        Set

        End Set
    End Property
End Class
