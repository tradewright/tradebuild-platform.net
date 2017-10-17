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

Imports System.ComponentModel

Imports TradeWright.Trading.Utils.Contracts

Friend Class ExchangeConverter
    Inherits StringConverter

    Shared _Exchanges As StandardValuesCollection

    Shared Sub New()
        Dim lExchanges = GetExchangeCodes()
        Dim exchangesEx(lExchanges.Count) As String
        exchangesEx(0) = ""
        For i = 0 To lExchanges.Count - 1
            exchangesEx(i + 1) = lExchanges(i)
        Next
        _Exchanges = New StandardValuesCollection(exchangesEx)
    End Sub

    Public Overrides Function GetStandardValuesSupported(context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function GetStandardValues(context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return _Exchanges
    End Function

    Public Overrides Function GetStandardValuesExclusive(context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
End Class
