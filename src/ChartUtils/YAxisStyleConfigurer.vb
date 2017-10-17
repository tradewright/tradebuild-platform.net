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

<TypeConverter(GetType(ExtendedPropertyWrapperConverter)),
DefaultProperty("BackGroundColors")>
Public NotInheritable Class YAxisStyleConfigurer
    Inherits ExtendedPropertyWrapper

    Private mStyle As ChartRegionStyle

    Private Shared ChartSkil As ChartSkil27.ChartSkil = New ChartSkil27.ChartSkil

    Public Sub New(host As ExtendedPropertyHost, prop As ExtendedProperty)
        MyBase.New(host, prop)
        mStyle = getStyle(Inherited)
    End Sub

    <Category("Appearance"), Browsable(True),
    Description("Specifies the colors used for the region's background: the first value is the color at the left hand edge; the second is the color at the right hand edge.")>
    Public Property BackGroundColors() As BackgroundColorsConfigurer
        Get
            Dim configurer = New BackgroundColorsConfigurer(CType(mStyle.BackGradientFillColors, Integer()))
            AddHandler configurer.PropertyChanged, Sub(sender As Object, e As PropertyChangedEventArgs) mStyle.BackGradientFillColors = DirectCast(sender, BackgroundColorsConfigurer).Colors
            Return configurer
        End Get
        Set
            mStyle.BackGradientFillColors = value.Colors
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies where the cursor label is to appear.")>
    Public Property CursorLabelPosition() As CursorTextPositions
        Get
            Return mStyle.YCursorTextPosition
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.YCursorTextPosition = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the style of the cursor label.")>
    Public ReadOnly Property CursorLabelStyle() As TextStyleConfigurer
        Get
            If mStyle.YCursorTextStyle Is Nothing Then mStyle.YCursorTextStyle = ChartSkil.GetDefaultTextStyle.clone
            Return New TextStyleConfigurer(mStyle.YCursorTextStyle)
        End Get
    End Property

    Private Function getStyle(inherited As Boolean) As ChartRegionStyle
        If inherited Then
            Return DirectCast(mHost.GetValue(mProperty), ChartRegionStyle)
        Else
            Return DirectCast(mHost.GetLocalValue(mProperty), ChartRegionStyle)
        End If
    End Function

    Protected Overrides Sub setLocalValue()
        mHost.SetValue(mProperty, DirectCast(mHost.GetValue(mProperty), ChartRegionStyle).clone)
    End Sub

End Class
