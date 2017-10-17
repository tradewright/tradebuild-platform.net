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

Imports ExtProps40
Imports System.ComponentModel

<TypeConverter(GetType(ExtendedPropertyWrapperConverter))>
Public MustInherit Class ExtendedPropertyWrapper
    Protected mHost As ExtendedPropertyHost
    Protected mProperty As ExtendedProperty

    Public Sub New(host As ExtendedPropertyHost, prop As ExtendedProperty)
        mHost = host
        mProperty = prop
    End Sub

    <Category("Appearance"), Browsable(True), ParenthesizePropertyNameAttribute(True), _
    Description("Specifies whether these settings are local to this style or are inherited from the parent style.")>
    Public Property Inherited As Boolean
        Get
            Return Not mHost.IsPropertySet(mProperty)
        End Get
        Set
            If value Then
                If Not Inherited Then
                    mHost.ClearValue(mProperty)
                End If
            ElseIf Inherited Then
                setLocalValue()
            End If
        End Set
    End Property

    Protected MustOverride Sub setLocalValue()

End Class

Public Class ExtendedPropertyWrapperConverter
    Inherits ExpandableObjectConverter

    Public Overrides Function ConvertTo(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object, destinationType As System.Type) As Object
        Dim wrapper = DirectCast(value, ExtendedPropertyWrapper)
        If destinationType Is GetType(String) Then
            If wrapper.Inherited Then
                Return "Inherited settings"
            Else
                Return "Local settings"
            End If
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

End Class
