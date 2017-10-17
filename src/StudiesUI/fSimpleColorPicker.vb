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

Imports System.Windows.Forms

Friend Class fSimpleColorPicker
	Inherits System.Windows.Forms.Form

#Region "Description"
    '
    '

#End Region

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "External procedure declarations"

#End Region

#Region "Shared variables"

    Private Shared gCustColors(15) As Integer

#End Region

#Region "Member variables"

    Private mSelectedColor As Color

#End Region

#Region "Form Event Handlers"

    Private Sub fSimpleColorPicker_KeyDown(eventSender As System.Object, eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If CType(eventArgs.KeyCode, Short) = System.Windows.Forms.Keys.Escape Then
            Me.DialogResult = DialogResult.Cancel
            Me.Hide()
        End If
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "Control Event Handlers"

    Private Sub ColorLabel_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles _
                                                                                                _ColorLabel_0.Click,
                                                                                                _ColorLabel_1.Click,
                                                                                                _ColorLabel_2.Click,
                                                                                                _ColorLabel_3.Click,
                                                                                                _ColorLabel_4.Click,
                                                                                                _ColorLabel_5.Click,
                                                                                                _ColorLabel_6.Click,
                                                                                                _ColorLabel_7.Click,
                                                                                                _ColorLabel_8.Click,
                                                                                                _ColorLabel_9.Click,
                                                                                                _ColorLabel_10.Click,
                                                                                                _ColorLabel_11.Click,
                                                                                                _ColorLabel_12.Click,
                                                                                                _ColorLabel_13.Click,
                                                                                                _ColorLabel_14.Click,
                                                                                                _ColorLabel_15.Click,
                                                                                                _ColorLabel_16.Click,
                                                                                                _ColorLabel_17.Click,
                                                                                                _ColorLabel_18.Click,
                                                                                                _ColorLabel_19.Click
        mSelectedColor = CType(eventSender, Label).BackColor
        Me.DialogResult = DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub InitialColorLabel_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles InitialColorLabel.Click
        mSelectedColor = InitialColorLabel.BackColor 'System.Drawing.ColorTranslator.ToOle(InitialColorLabel.BackColor)
        Me.DialogResult = DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub MoreColorsButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles MoreColorsButton.Click
        Dim cd As New ColorDialog With {.Color = InitialColorLabel.BackColor, .CustomColors = gCustColors}

        If cd.ShowDialog(Me) = DialogResult.OK Then
            mSelectedColor = cd.Color
            InitialColorLabel.BackColor = mSelectedColor
            Me.DialogResult = DialogResult.OK
            Me.Hide()
        End If
    End Sub

    Private Sub NoColorButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles NoColorButton.Click
        mSelectedColor = ColorPicker.NullColor
        Me.DialogResult = DialogResult.OK
        Me.Hide()
    End Sub

#End Region

#Region "Properties"

    Friend WriteOnly Property initialColor() As Color
        Set
            InitialColorLabel.BackColor = Value
            mSelectedColor = Value
        End Set
    End Property

    Friend ReadOnly Property selectedColor() As Color
        Get
            Return mSelectedColor
        End Get
    End Property

#End Region

#Region "Methods"

#End Region

#Region "Helper Functions"
#End Region

End Class