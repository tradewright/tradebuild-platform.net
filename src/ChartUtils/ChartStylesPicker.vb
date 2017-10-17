﻿#Region "License"

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
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Imports ChartSkil27

<ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)> Public Class ChartStylePicker
    Inherits ToolStripControlHost

    Public Event SelectedEntryChanged(sender As Object, e As EventArgs)

    Private Const NoChartStyle As String = "(None)"

    Private mCombo As ComboBox

    Public Sub New()
        MyBase.New(New ComboBox())
        mCombo = DirectCast(MyBase.Control, ComboBox)
        mCombo.DropDownStyle = ComboBoxStyle.DropDownList
        mCombo.Sorted = True
        mCombo.DisplayMember = "Name"
        loadCombo()
    End Sub

    Public Shadows ReadOnly Property Control() As Control
        Get
            Throw New NotSupportedException
        End Get
    End Property

    Protected Overrides Sub OnSubscribeControlEvents(c As System.Windows.Forms.Control)
        MyBase.OnSubscribeControlEvents(c)

        Dim combo = CType(c, ComboBox)

        AddHandler combo.SelectedIndexChanged, AddressOf handleSelectionChange

    End Sub

    <Category("Appearance")> Public Property DropDownHeight() As Integer
        Get
            Return mCombo.DropDownHeight
        End Get
        Set
            mCombo.DropDownHeight = value
        End Set
    End Property

    <Category("Appearance")> Public Property DropDownWidth() As Integer
        Get
            Return mCombo.DropDownWidth
        End Get
        Set
            mCombo.DropDownWidth = value
        End Set
    End Property

    <Category("Appearance")> Public Property FlatStyle() As FlatStyle
        Get
            Return mCombo.FlatStyle
        End Get
        Set
            mCombo.FlatStyle = value
        End Set
    End Property

    <Category("Appearance")> Public Property IntegralHeight() As Boolean
        Get
            Return mCombo.IntegralHeight
        End Get
        Set
            mCombo.IntegralHeight = value
        End Set
    End Property

    <Browsable(False)> Public ReadOnly Property SelectedEntry() As ChartStyle
        Get
            If mCombo.SelectedIndex = 0 Then
                Return Nothing
            Else
                Return DirectCast(mCombo.SelectedItem, ChartStyle)
            End If
        End Get
    End Property

    Public Sub SelectItem(style As ChartStyle)
        If style Is Nothing Then
            mCombo.SelectedIndex = mCombo.FindStringExact(NoChartStyle)
        Else
            mCombo.SelectedIndex = mCombo.Items.IndexOf(style)
        End If
    End Sub

    Private Sub handleSelectionChange(sender As Object, e As EventArgs)
        RaiseEvent SelectedEntryChanged(sender, e)
    End Sub

    Private Sub loadCombo()
        mCombo.Items.Add(NoChartStyle)
        For Each style As ChartStyle In ChartUtils.ChartSkil.ChartStylesManager
            mCombo.Items.Add(style)
        Next
    End Sub

End Class
