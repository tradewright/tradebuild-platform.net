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

Friend Class SecTypeConverter
    Inherits TypeConverter

    Private Shared _SecTypes As StandardValuesCollection = New StandardValuesCollection(New SecurityType() {SecurityType.None,
                                                              SecurityType.Stock,
                                                              SecurityType.Future,
                                                              SecurityType.Index,
                                                              SecurityType.Cash,
                                                              SecurityType.Option,
                                                              SecurityType.FuturesOption})

    Public Overrides Function GetStandardValuesSupported(context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function GetStandardValues(context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return _SecTypes

    End Function

    Public Overrides Function GetStandardValuesExclusive(context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function CanConvertFrom(context As System.ComponentModel.ITypeDescriptorContext, sourceType As System.Type) As Boolean
        If sourceType Is GetType(System.String) Then Return True
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    'Public Overrides Function CanConvertTo(context As System.ComponentModel.ITypeDescriptorContext, destinationType As System.Type) As Boolean
    '    If destinationType Is GetType(System.String) Then Return True
    '    Return MyBase.CanConvertTo(context, destinationType)
    'End Function

    Public Overrides Function ConvertFrom(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object) As Object
        If TypeOf value Is System.String Then
            Return Contract.SecTypeFromString(CStr(value))
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overrides Function ConvertTo(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object, destinationType As System.Type) As Object
        If destinationType Is GetType(System.String) AndAlso TypeOf value Is SecurityType Then
            Return Contract.SecTypeToString(CType(value, SecurityType))
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

    Public Overrides Function IsValid(context As System.ComponentModel.ITypeDescriptorContext, value As Object) As Boolean
        Select Case CInt(value)
            Case SecurityType.Cash,
                SecurityType.Combo,
                SecurityType.Future,
                SecurityType.FuturesOption,
                SecurityType.Index,
                SecurityType.None,
                SecurityType.Option,
                SecurityType.Stock
                Return True
            Case Else
                Return False
        End Select
        Return MyBase.IsValid(context, value)
    End Function

End Class
