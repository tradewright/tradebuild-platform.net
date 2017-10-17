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
Public NotInheritable Class SimpleTextStyleExtendedPropertyConfigurer
    Inherits ExtendedPropertyWrapper

    Private mStyle As TextStyle

    Public Sub New(host As ExtendedPropertyHost, prop As ExtendedProperty)
        MyBase.New(host, prop)
        mStyle = getStyle(Inherited)
    End Sub

    <Category("Behavior"), Browsable(True),
Description("Specifies how the text is aligned in relation to its locating point.")>
    Public Property Align() As TextAlignModes
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyAlign) Then
                Return mStyle.Align
            Else
                Return TextAlignModes.AlignBoxTopLeft
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.Align = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the angle (in degrees) that the text is inclined to the horizontal.")>
    Public Property Angle() As Double
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyAngle) Then
                Return ChartUtils.ChartSkil.RadiansToDegrees(mStyle.Angle)
            Else
                Return 0.0
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.Angle = ChartUtils.ChartSkil.DegreesToRadians(value)
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies whether there is a box surrounding the text.")>
    Public Property Box() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyBox) Then
                Return mStyle.Box
            Else
                Return False
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.Box = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the outline color of the box surrounding the text.")>
    Public Property BoxColor() As Color
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyBoxColor) Then
                Return ColorTranslator.FromOle(mStyle.BoxColor)
            Else
                Return Drawing.Color.Black
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.BoxColor = ColorTranslator.ToOle(value)
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the fill color of the box surrounding the text.")>
    Public Property BoxFillColor() As Color
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyBoxFillColor) Then
                Return ColorTranslator.FromOle(mStyle.BoxFillColor)
            Else
                Return Drawing.Color.White
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.BoxFillColor = ColorTranslator.ToOle(value)
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the fill style of the box surrounding the text.")>
    Public Property BoxFillStyle() As FillStyles
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyBoxFillStyle) Then
                Return mStyle.BoxFillStyle
            Else
                Return FillStyles.FillSolid
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.BoxFillStyle = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies that the box surrounding the text is to be filled with the color of the region's background.")>
    Public Property BoxFillWithBackgroundColor() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyBoxFillWithBackgroundColor) Then
                Return mStyle.BoxFillWithBackgroundColor
            Else
                Return False
            End If

        End Get
        Set
            If Inherited Then Return
            mStyle.BoxFillWithBackgroundColor = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the outline style of the box surrounding the text.")>
    Public Property BoxStyle() As LineStyles
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyBoxStyle) Then
                Return mStyle.BoxStyle
            Else
                Return LineStyles.LineSolid
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.BoxStyle = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the outline thickness of the box surrounding the text.")>
    Public Property BoxThickness() As Integer
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyBoxThickness) Then
                Return mStyle.BoxThickness
            Else
                Return 1
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.BoxThickness = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the text's color.")>
    Public Property Color() As Color
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyColor) Then
                Return ColorTranslator.FromOle(mStyle.Color)
            Else
                Return Drawing.Color.Black
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.Color = ColorTranslator.ToOle(value)
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the text's font.")>
    Public Property Font() As Font
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyFont) Then
                Return mStyle.Font.ToFont
            Else
                Return New Font("Arial", 8)
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.Font = value.ToStdFont
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies whether the text is to be taken into account when vertically rescaling the region.")>
    Public Property IncludeInAutoscale() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyIncludeInAutoscale) Then
                Return mStyle.IncludeInAutoscale
            Else
                Return True
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.IncludeInAutoscale = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the horizontal distance (in millimetres) between the text and its surrounding box.")>
    Public Property PaddingX() As Double
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyPaddingX) Then
                Return mStyle.PaddingX
            Else
                Return 1.0
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.PaddingX = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the horizontal distance (in millimetres) between the text and its surrounding box.")>
    Public Property PaddingY() As Double
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyPaddingY) Then
                Return mStyle.PaddingY
            Else
                Return 1
            End If
        End Get
        Set
            If Inherited Then Return
            mStyle.PaddingY = value
        End Set
    End Property

    Private Function getStyle(inherited As Boolean) As TextStyle
        If inherited Then
            Return DirectCast(mHost.GetValue(mProperty), TextStyle)
        Else
            Return DirectCast(mHost.GetLocalValue(mProperty), TextStyle)
        End If
    End Function

    Protected Overrides Sub setLocalValue()
        mHost.SetValue(mProperty, DirectCast(mHost.GetValue(mProperty), LineStyle).clone)
    End Sub

End Class
