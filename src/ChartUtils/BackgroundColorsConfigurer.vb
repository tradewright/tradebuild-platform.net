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

<TypeConverter(GetType(BackgroundColorsConfigurerConverter)),
DefaultProperty("Color1")>
Public Class BackgroundColorsConfigurer
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Private mColors() As Integer

    Public Sub New(colors() As Integer)
        mColors = colors
    End Sub

    <Category("Appearance"), Browsable(True),
    Description("Specifies the first color.")>
    Public Property Color1() As Color
        Get
            Return ColorTranslator.FromOle(CInt(mColors(0)))
        End Get
        Set
            mColors(0) = CInt(ColorTranslator.ToOle(Value))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Color1"))
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the second color.")>
    Public Property Color2() As Color
        Get
            Return ColorTranslator.FromOle(CInt(mColors(1)))
        End Get
        Set
            mColors(1) = CInt(ColorTranslator.ToOle(Value))
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Color2"))
        End Set
    End Property

    <Browsable(False)>
    Friend ReadOnly Property Colors As Integer()
        Get
            Return mColors
        End Get
    End Property

End Class

Friend NotInheritable Class BackgroundColorsConfigurerConverter
    Inherits ExpandableObjectConverter

    Public Overrides Function ConvertTo(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object, destinationType As System.Type) As Object
        If destinationType Is GetType(System.String) Then Return ""
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class

