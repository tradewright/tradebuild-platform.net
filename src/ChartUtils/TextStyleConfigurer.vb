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

<TypeConverter(GetType(ExpandableObjectConverter)),
DefaultProperty("Font")>
Public Class TextStyleConfigurer
    Inherits SimpleTextStyleConfigurer

    Private WithEvents mOffsetConfigurer As SizeConfigurer
    Private WithEvents mSizeConfigurer As SizeConfigurer
    Private WithEvents mLeftMarginConfigurer As DimensionConfigurer
    Private WithEvents mRightMarginConfigurer As DimensionConfigurer

    Public Sub New(style As TextStyle)
        MyBase.New(style)
        mOffsetConfigurer = New SizeConfigurer(mStyle.Offset)
        mSizeConfigurer = New SizeConfigurer(mStyle.Size)
        mLeftMarginConfigurer = New DimensionConfigurer(mStyle.LeftMargin)
        mRightMarginConfigurer = New DimensionConfigurer(mStyle.RightMargin)
    End Sub

    Private Sub SizeValueChanged(sender As Object, e As EventArgs) _
            Handles mOffsetConfigurer.ValueChanged,
                    mSizeConfigurer.ValueChanged,
                    mLeftMarginConfigurer.ValueChanged,
                    mRightMarginConfigurer.ValueChanged
        If sender Is mOffsetConfigurer Then
            mStyle.Offset = mOffsetConfigurer.Value
        ElseIf sender Is mSizeConfigurer Then
            mStyle.Size = mSizeConfigurer.Value
        ElseIf sender Is mLeftMarginConfigurer Then
            mStyle.LeftMargin = mLeftMarginConfigurer.Value
        Else
            mStyle.RightMargin = mRightMarginConfigurer.Value
        End If
    End Sub

    <Category("Behavior"), Browsable(True),
    Description("Specifies how text is to be shortened if it is longer than the space allocated.")>
    Public Property Ellipsis() As EllipsisModes
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyEllipsis) Then
                Return mStyle.Ellipsis
            Else
                Return EllipsisModes.EllipsisNone
            End If
        End Get
        Set
            mStyle.Ellipsis = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether tab characters within the text are to be expanded.")>
    Public Property ExpandTabs() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyExpandTabs) Then
                Return mStyle.ExpandTabs
            Else
                Return False
            End If
        End Get
        Set
            mStyle.ExpandTabs = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether the text is to be displayed if any part of it extends into the region. If false, the text is only displayed if the alignment point is within the region.")>
    Public Property Extended() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyExtended) Then
                Return mStyle.Extended
            Else
                Return False
            End If
        End Get
        Set
            mStyle.Extended = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether the text is remain at the same horizontal position in the region, regardless of how the region is scrolled or scaled.")>
    Public Property FixedX() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyFixedX) Then
                Return mStyle.FixedX
            Else
                Return False
            End If
        End Get
        Set
            mStyle.FixedX = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether the text is remain at the same vertical position in the region, regardless of how the region is scrolled or scaled.")>
    Public Property FixedY() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyFixedY) Then
                Return mStyle.FixedY
            Else
                Return False
            End If
        End Get
        Set
            mStyle.FixedY = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies how multiline text, or text with a specified size, is to be justified within the space allocated for it.")>
    Public Property Justification() As TextJustifyModes
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyJustification) Then
                Return mStyle.Justification
            Else
                Return TextJustifyModes.JustifyCentre
            End If
        End Get
        Set
            mStyle.Justification = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
        Description("Specifies the margin at the left side of the text's area.")>
    Public Property LeftMargin() As DimensionConfigurer
        Get
            Return mLeftMarginConfigurer
        End Get
        Set
            mStyle.LeftMargin = getDimensionFromDimensionConfigurer(value)
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether line feeds and carriage returns in the text are actioned.")>
    Public Property Multiline() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyMultiLine) Then
                Return mStyle.MultiLine
            Else
                Return False
            End If
        End Get
        Set
            mStyle.MultiLine = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
        Description("Specifies the offset by which the text's alignment point is to be adjusted.")>
    Public Property Offset() As SizeConfigurer
        Get
            Return mOffsetConfigurer
        End Get
        Set
            mStyle.Offset = getSizeFromSizeConfigurer(value)
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
        Description("Specifies the margin at the right side of the text's area.")>
    Public Property RightMargin() As DimensionConfigurer
        Get
            Return mRightMarginConfigurer
        End Get
        Set
            mStyle.RightMargin = getDimensionFromDimensionConfigurer(value)
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
        Description("Specifies the size of the area in which the text is to be displayed.")>
    Public Property Size() As SizeConfigurer
        Get
            Return mSizeConfigurer
        End Get
        Set
            mStyle.Size = getSizeFromSizeConfigurer(value)
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies the number of characters between tab stops.")>
    Public Property TabWidth() As Integer
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyTabWidth) Then
                Return mStyle.TabWidth
            Else
                Return 8
            End If
        End Get
        Set
            mStyle.TabWidth = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether the text automatically flows over multiple lines.")>
    Public Property WordWrap() As Boolean
        Get
            If mStyle.IsPropertySet(TextPropertyFlags.TextPropertyWordWrap) Then
                Return mStyle.WordWrap
            Else
                Return False
            End If
        End Get
        Set
            mStyle.WordWrap = value
        End Set
    End Property

    Private Function getDimensionFromDimensionConfigurer(configurer As DimensionConfigurer) As ChartSkil27.Dimension
        If configurer Is Nothing Then Return Nothing
        If configurer.None Then Return Nothing
        Return configurer.Value
    End Function

    Private Function getSizeFromSizeConfigurer(configurer As SizeConfigurer) As ChartSkil27.Size
        If configurer Is Nothing Then Return Nothing
        If configurer.None Then Return Nothing
        Return configurer.Value
    End Function

End Class
