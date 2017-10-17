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
Imports System.ComponentModel

<DefaultProperty("DefaultRegionStyle")>
Public NotInheritable Class ChartStyleConfigurer

    Private mStyle As ChartStyle

    Public Sub New(chartStyle As ChartStyle)
        mStyle = chartStyle
    End Sub

    <Category("Behavior"), Browsable(True), _
    Description("Specifies whether the chart automatically scrolls forward each time a new period is added.")>
    Public ReadOnly Property Autoscrolling() As ExtendedPropertyValueConfigurer(Of Boolean)
        Get
            Return New ExtendedPropertyValueConfigurer(Of Boolean)(mStyle.ExtendedPropertyHost, mStyle.AutoscrollingProperty)
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property BasedOn() As ChartStyle
        Get
            Return mStyle.BasedOn
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the color of the chart's background area.")>
    Public ReadOnly Property ChartBackColor() As ExtendedPropertyValueConfigurer(Of System.Drawing.Color)
        Get
            Return New ExtendedPropertyValueConfigurer(Of System.Drawing.Color)(
                                            mStyle.ExtendedPropertyHost,
                                            mStyle.ChartBackColorProperty,
                                            Function(x) ColorTranslator.FromOle(CInt(x)),
                                            Function(x) ColorTranslator.ToOle(x))
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the style of the crosshair lines.")>
    Public ReadOnly Property CrosshairStyle() As SimpleLineStyleExtendedPropertyConfigurer
        Get
            Return New SimpleLineStyleExtendedPropertyConfigurer(mStyle.ExtendedPropertyHost, mStyle.CrosshairLineStyleProperty)
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the default style for new regions in the chart.")>
    Public ReadOnly Property DefaultRegionStyle() As DefaultRegionStyleConfigurer
        Get
            Return New DefaultRegionStyleConfigurer(mStyle.ExtendedPropertyHost, mStyle.DefaultRegionStyleProperty)
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the default style for the YAxis area of new regions in the chart.")>
    Public ReadOnly Property DefaultYAxisRegionStyle() As YAxisStyleConfigurer
        Get
            Return New YAxisStyleConfigurer(mStyle.ExtendedPropertyHost, mStyle.DefaultYAxisRegionStyleProperty)
        End Get
    End Property

    <Category("Behavior"), Browsable(True), _
    Description("Specifies whether scrolling the chart horizontally by dragging with the mouse is allowed.")>
    Public ReadOnly Property HorizontalMouseScrollingAllowed() As ExtendedPropertyValueConfigurer(Of Boolean)
        Get
            Return New ExtendedPropertyValueConfigurer(Of Boolean)(mStyle.ExtendedPropertyHost, mStyle.HorizontalMouseScrollingAllowedProperty)
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the whether the horizontal scroll bar is visible.")>
    Public ReadOnly Property HorizontalScrollBarVisible() As ExtendedPropertyValueConfigurer(Of Boolean)
        Get
            Return New ExtendedPropertyValueConfigurer(Of Boolean)(mStyle.ExtendedPropertyHost, mStyle.HorizontalScrollBarVisibleProperty)
        End Get
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether scrolling the chart vertically by dragging with the mouse is allowed.")>
    Public ReadOnly Property VerticalMouseScrollingAllowed() As ExtendedPropertyValueConfigurer(Of Boolean)
        Get
            Return New ExtendedPropertyValueConfigurer(Of Boolean)(mStyle.ExtendedPropertyHost, mStyle.VerticalMouseScrollingAllowedProperty)
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies style of the chart's XAxis area.")>
    Public ReadOnly Property XAxisRegionStyle() As XAxisStyleConfigurer
        Get
            Return New XAxisStyleConfigurer(mStyle.ExtendedPropertyHost, mStyle.XAxisRegionStyleProperty)
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies whether the XAxis area of the chart is visible.")>
    Public ReadOnly Property XAxisVisible() As ExtendedPropertyValueConfigurer(Of Boolean)
        Get
            Return New ExtendedPropertyValueConfigurer(Of Boolean)(mStyle.ExtendedPropertyHost, mStyle.XAxisVisibleProperty)
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies whether the YAxis area of the chart is visible.")>
    Public ReadOnly Property YAxisVisible() As ExtendedPropertyValueConfigurer(Of Boolean)
        Get
            Return New ExtendedPropertyValueConfigurer(Of Boolean)(mStyle.ExtendedPropertyHost, mStyle.YAxisVisibleProperty)
        End Get
    End Property

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the width in centimeters of the chart's YAxis area.")>
    Public ReadOnly Property YAxisWidthCm() As ExtendedPropertyValueConfigurer(Of Single)
        Get
            Return New ExtendedPropertyValueConfigurer(Of Single)(mStyle.ExtendedPropertyHost, mStyle.YAxisWidthCmProperty)
        End Get
    End Property

End Class

