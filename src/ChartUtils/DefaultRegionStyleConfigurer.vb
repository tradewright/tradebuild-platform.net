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
Public NotInheritable Class DefaultRegionStyleConfigurer
    Inherits ExtendedPropertyWrapper

    Private mStyle As ChartRegionStyle

    Private Shared ChartSkil As ChartSkil27.ChartSkil = New ChartSkil27.ChartSkil

    Public Sub New(host As ExtendedPropertyHost, prop As ExtendedProperty)
        MyBase.New(host, prop)
        mStyle = getStyle(Inherited)
    End Sub

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether the region is automatically rescaled vertically to fit its contents.")>
    Public Property Autoscaling() As Boolean
        Get
            Return mStyle.Autoscaling
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.Autoscaling = Value
        End Set
    End Property

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
    Description("Specifies what type of cursor label, if any, appears in the region.")>
    Public Property CursorLabelMode() As CursorTextModes
        Get
            Return mStyle.CursorTextMode
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.CursorTextMode = Value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies where the cursor label is to appear. This is only relevant if a combined X and Y cursor label is to be shown.")>
    Public Property CursorLabelPosition() As CursorTextPositions
        Get
            Return mStyle.CursorTextPosition
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.CursorTextPosition = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the style of the cursor label. This is only relevant if a combined X and Y cursor label is to be shown.")>
    Public ReadOnly Property CursorLabelStyle() As TextStyleConfigurer
        Get
            If mStyle.CursorTextStyle Is Nothing Then mStyle.CursorTextStyle = ChartSkil.GetDefaultTextStyle.clone
            Return New TextStyleConfigurer(mStyle.CursorTextStyle)
        End Get
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether the pointer should snap to the nearest tick .")>
    Public Property PointerSnapsToNearestTick() As Boolean
        Get
            Return mStyle.CursorSnapsToTickBoundaries
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.CursorSnapsToTickBoundaries = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the approximate spacing, in cm, between horizontal gridlines.")>
    Public Property HorizontalGridlineSpacing() As Double
        Get
            Return mStyle.YGridlineSpacing
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.YGridlineSpacing = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the position of horizontal gridline labels.")>
    Public Property HorizontalGridLabelPosition() As YGridTextPositions
        Get
            Return mStyle.YGridTextPosition
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.YGridTextPosition = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the appearance of horizontal grid labels.")>
    Public ReadOnly Property HorizontalGridLabelStyle() As TextStyleConfigurer
        Get
            If mStyle.YGridTextStyle Is Nothing Then mStyle.YGridTextStyle = ChartSkil.GetDefaultTextStyle.clone
            Return New TextStyleConfigurer(mStyle.YGridTextStyle)
        End Get
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the appearance of the horizontal gridlines.")>
    Public ReadOnly Property HorizontalGridLineStyle() As SimpleLineStyleConfigurer
        Get
            Return New SimpleLineStyleConfigurer(mStyle.YGridLineStyle)
        End Get
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the position of vertical grid labels.")>
    Public Property VerticalGridLabelPosition() As XGridTextPositions
        Get
            Return mStyle.XGridTextPosition
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.XGridTextPosition = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the appearance of vertical grid labels.")>
    Public ReadOnly Property VerticalGridLabelStyle() As TextStyleConfigurer
        Get
            If mStyle.XGridTextStyle Is Nothing Then mStyle.XGridTextStyle = ChartSkil.GetDefaultTextStyle.clone
            Return New TextStyleConfigurer(mStyle.XGridTextStyle)
        End Get
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the appearance of the vertical gridlines.")>
    Public ReadOnly Property VerticalGridLineStyle() As SimpleLineStyleConfigurer
        Get
            Return New SimpleLineStyleConfigurer(mStyle.XGridLineStyle)
        End Get
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies whether vertical gridlines are displayed in the region.")>
    Public Property ShowVerticalGrid() As Boolean
        Get
            Return mStyle.HasXGrid
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.HasXGrid = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies whether horizontal gridlines are displayed in the region.")>
    Public Property ShowHorizontalGrid() As Boolean
        Get
            Return mStyle.HasYGrid
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.HasYGrid = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies whether vertical gridline labels are displayed in the region.")>
    Public Property ShowVerticalGridLabels() As Boolean
        Get
            Return mStyle.HasXGridText
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.HasXGridText = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies whether horizontal gridline labels are displayed in the region.")>
    Public Property ShowHorizontalGridLabels() As Boolean
        Get
            Return mStyle.HasYGridText
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.HasYGridText = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the style of gridlines indicating the end of the trading session.")>
    Public ReadOnly Property SessionEndGridLineStyle() As SimpleLineStyleConfigurer
        Get
            Return New SimpleLineStyleConfigurer(mStyle.SessionEndGridLineStyle)
        End Get
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the style of gridlines indicating the start of the trading session.")>
    Public ReadOnly Property SessionStartGridLineStyle() As SimpleLineStyleConfigurer
        Get
            Return New SimpleLineStyleConfigurer(mStyle.SessionStartGridLineStyle)
        End Get
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies where the X-axis cursor label is to appear.")>
    Public Property XAxisCursorLabelPosition() As CursorTextPositions
        Get
            Return mStyle.XCursorTextPosition
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.XCursorTextPosition = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
        Description("Specifies the style of the cursor label for the X-axis.")>
    Public ReadOnly Property XAxisCursorLabelStyle() As TextStyleConfigurer
        Get
            If mStyle.XCursorTextStyle Is Nothing Then mStyle.XCursorTextStyle = ChartSkil.GetDefaultTextStyle.clone
            Return New TextStyleConfigurer(mStyle.XCursorTextStyle)
        End Get
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies where the Y-axis cursor label is to appear.")>
    Public Property YAxisCursorLabelPosition() As CursorTextPositions
        Get
            Return mStyle.YCursorTextPosition
        End Get
        Set
            If Inherited Then Exit Property
            mStyle.YCursorTextPosition = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the style of the cursor label for the Y-axis.")>
    Public ReadOnly Property YAxisCursorLabelStyle() As TextStyleConfigurer
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
