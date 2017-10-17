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
DefaultProperty("Color")>
Public NotInheritable Class LineStyleConfigurer
    Inherits ExtendedPropertyWrapper

    Private mStyle As LineStyle

    Private WithEvents mOffset1Configurer As SizeConfigurer
    Private WithEvents mOffset2Configurer As SizeConfigurer

    Public Sub New(host As ExtendedPropertyHost, prop As ExtendedProperty)
        MyBase.New(host, prop)
        mStyle = getLineStyle(Inherited)

        mOffset1Configurer = New SizeConfigurer(mStyle.Offset1)
        mOffset2Configurer = New SizeConfigurer(mStyle.Offset2)
    End Sub

    Private Sub OffSetValueChanged(sender As Object, e As EventArgs) Handles mOffset1Configurer.ValueChanged, mOffset2Configurer.ValueChanged
        If sender Is mOffset1Configurer Then
            mStyle.Offset1 = mOffset1Configurer.Value
        Else
            mStyle.Offset2 = mOffset2Configurer.Value
        End If
    End Sub

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the outline color of the line's end arrowhead.")> _
    Public Property ArrowEndColor() As Color
        Get
            Return ColorTranslator.FromOle(CInt(mStyle.ArrowEndColor))
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowEndColor = CInt(ColorTranslator.ToOle(Value))
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the fill color of the line's end arrowhead.")> _
    Public Property ArrowEndFillColor() As Color
        Get
            Return ColorTranslator.FromOle(CInt(mStyle.ArrowEndFillColor))
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowEndFillColor = CInt(ColorTranslator.ToOle(Value))
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the fill style of the line's end arrowhead.")> _
    Public Property ArrowEndFillStyle() As FillStyles
        Get
            Return mStyle.ArrowEndFillStyle
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowEndFillStyle = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the length of the line's end arrowhead.")> _
    Public Property ArrowEndLength() As Integer
        Get
            Return mStyle.ArrowEndLength
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowEndLength = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the style of the line's end arrowhead.")> _
    Public Property ArrowEndStyle() As ArrowStyles
        Get
            Return mStyle.ArrowEndStyle
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowEndStyle = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the width of the line's end arrowhead.")> _
    Public Property ArrowEndWidth() As Integer
        Get
            Return mStyle.ArrowEndWidth
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowEndWidth = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the outline color of the line's start arrowhead.")> _
    Public Property ArrowStartColor() As Color
        Get
            Return ColorTranslator.FromOle(CInt(mStyle.ArrowStartColor))
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowStartColor = CInt(ColorTranslator.ToOle(Value))
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the fill color of the line's start arrowhead.")> _
    Public Property ArrowStartFillColor() As Color
        Get
            Return ColorTranslator.FromOle(CInt(mStyle.ArrowStartFillColor))
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowStartFillColor = CInt(ColorTranslator.ToOle(Value))
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the fill style of the line's start arrowhead.")> _
    Public Property ArrowStartFillStyle() As FillStyles
        Get
            Return mStyle.ArrowStartFillStyle
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowStartFillStyle = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the length of the line's start arrowhead.")> _
    Public Property ArrowStartLength() As Integer
        Get
            Return mStyle.ArrowStartLength
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowStartLength = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the style of the line's start arrowhead.")> _
    Public Property ArrowStartStyle() As ArrowStyles
        Get
            Return mStyle.ArrowStartStyle
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowStartStyle = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the width of the line's start arrowhead.")> _
    Public Property ArrowStartWidth() As Integer
        Get
            Return mStyle.ArrowStartWidth
        End Get
        Set
            If Inherited Then Return
            mStyle.ArrowStartWidth = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the line's color.")> _
    Public Property Color() As Color
        Get
            Return ColorTranslator.FromOle(CInt(mStyle.Color))
        End Get
        Set
            If Inherited Then Return
            mStyle.Color = CInt(ColorTranslator.ToOle(Value))
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the whether the line extends indefinitely beyond it's endpoint.")> _
    Public Property ExtendAfter() As Boolean
        Get
            Return mStyle.ExtendAfter
        End Get
        Set
            If Inherited Then Return
            mStyle.ExtendAfter = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the whether the line extends indefinitely beyond it's startpoint.")> _
    Public Property ExtendBefore() As Boolean
        Get
            Return mStyle.ExtendBefore
        End Get
        Set
            If Inherited Then Return
            mStyle.ExtendBefore = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the whether the line should be visible if neither its endpoint nor its startpoint are within the visible region of the chart.")> _
    Public Property Extended() As Boolean
        Get
            Return mStyle.Extended
        End Get
        Set
            If Inherited Then Return
            mStyle.Extended = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the whether the line's horizontal position should remain unchanged as the chart is scrolled.")> _
    Public Property FixedX() As Boolean
        Get
            Return mStyle.FixedX
        End Get
        Set
            If Inherited Then Return
            mStyle.FixedX = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the whether the line's vertical position should remain unchanged as the chart is scrolled.")> _
    Public Property FixedY() As Boolean
        Get
            Return mStyle.FixedY
        End Get
        Set
            If Inherited Then Return
            mStyle.FixedY = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the whether the line's vertical extent should be taken into account when the chart is automatically rescaled.")> _
    Public Property IncludeInAutoscale() As Boolean
        Get
            Return mStyle.IncludeInAutoscale
        End Get
        Set
            If Inherited Then Return
            mStyle.IncludeInAutoscale = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the layer on which the line is to be drawn.")> _
    Public Property Layer() As LayerNumbers
        Get
            Return mStyle.Layer
        End Get
        Set
            If Inherited Then Return
            mStyle.Layer = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the line's style.")> _
    Public Property LineStyle() As LineStyles
        Get
            Return mStyle.LineStyle
        End Get
        Set
            If Inherited Then Return
            mStyle.LineStyle = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the offset by which the line's start point is to be adjusted.")>
    Public Property Offset1() As SizeConfigurer
        Get
            Return mOffset1Configurer
        End Get
        Set
            If Inherited Then Return
            mStyle.Offset1 = getSizeFromSizeConfigurer(value)
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the offset by which the line's end point is to be adjusted.")>
    Public Property Offset2() As SizeConfigurer
        Get
            Return mOffset2Configurer
        End Get
        Set
            If Inherited Then Return
            mStyle.Offset2 = getSizeFromSizeConfigurer(value)
        End Set
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the line's thickness.")> _
    Public Property Thickness() As Integer
        Get
            Return mStyle.Thickness
        End Get
        Set
            If Inherited Then Return
            mStyle.Thickness = value
        End Set
    End Property

    Private Function getLineStyle(inherited As Boolean) As LineStyle
        If inherited Then
            Return DirectCast(mHost.GetValue(mProperty), LineStyle)
        Else
            Return DirectCast(mHost.GetLocalValue(mProperty), LineStyle)
        End If
    End Function

    Private Function getSizeFromSizeConfigurer(configurer As SizeConfigurer) As ChartSkil27.Size
        If configurer Is Nothing Then Return Nothing
        If configurer.None Then Return Nothing
        Return configurer.Value
    End Function

    Protected Overrides Sub setLocalValue()
        mHost.SetValue(mProperty, DirectCast(mHost.GetValue(mProperty), LineStyle).clone)
    End Sub

    Private Function substituteIfNull(value As Object, substituteValue As Object) As Object
        Return IIf(value Is Nothing, substituteValue, value)
    End Function

    Private Function substituteIfSame(value As Object, comparisonValue As Object, substituteValue As Object) As Object
        Return IIf(value Is comparisonValue, substituteValue, value)
    End Function

End Class

'Public Class LineStyleConfigurerConverter
'    Inherits ExpandableObjectConverter

'    Public Overrides Function ConvertTo(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object, destinationType As System.Type) As Object
'        If destinationType Is GetType(String) Then Return ""
'        Return MyBase.ConvertTo(context, culture, value, destinationType)
'    End Function
'End Class