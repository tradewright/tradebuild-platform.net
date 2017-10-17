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
Imports ExtProps40
Imports System.ComponentModel

<TypeConverter(GetType(SimpleLineConfigurerConverter)),
DefaultProperty("Color")>
Public Class SimpleLineStyleConfigurer

    Private mStyle As LineStyle

    Public Sub New(style As LineStyle)
        mStyle = style
    End Sub

    <Category("Appearance"), Browsable(True),
    Description("Specifies the line's color.")>
    Public Property Color() As Color
        Get
            If mStyle.IsPropertySet(LinePropertyFlags.LinePropertyColor) Then
                Return ColorTranslator.FromOle(CInt(mStyle.Color))
            Else
                Return Drawing.Color.Black
            End If

        End Get
        Set
            mStyle.Color = CInt(ColorTranslator.ToOle(Value))
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the line's style.")>
    Public Property LineStyle() As LineStyles
        Get
            If mStyle.IsPropertySet(LinePropertyFlags.LinePropertyLineStyle) Then
                Return mStyle.LineStyle
            Else
                Return LineStyles.LineSolid
            End If
        End Get
        Set
            mStyle.LineStyle = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the line's thickness.")>
    Public Property Thickness() As Integer
        Get
            If mStyle.IsPropertySet(LinePropertyFlags.LinePropertyThickness) Then
                Return mStyle.Thickness
            Else
                Return 1
            End If
        End Get
        Set
            mStyle.Thickness = value
        End Set
    End Property

End Class

Friend NotInheritable Class SimpleLineConfigurerConverter
    Inherits ExpandableObjectConverter

    Public Overrides Function ConvertTo(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object, destinationType As System.Type) As Object
        If destinationType Is GetType(System.String) Then Return ""
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class