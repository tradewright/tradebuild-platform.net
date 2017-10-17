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

Imports BarUtils27
Imports TWUtilities40

Public Class TimePeriodSpecifier

    Event Change(sender As Object, e As System.EventArgs)

#Region "Interfaces"

#End Region

#Region "Events"


#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

#End Region

#Region "Member variables"

    Private mValidator As ITimePeriodValidator

#End Region

#Region "Constructors"

    Protected Sub OnChange(e As System.EventArgs)
        RaiseEvent Change(Me, EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnEnabledChanged(e As System.EventArgs)
        MyBase.OnEnabledChanged(e)
        TimePeriodLengthText.Enabled = Me.Enabled
        TimePeriodCombo.Enabled = Me.Enabled
    End Sub

    Protected Overrides Sub OnResize(e As System.EventArgs)
        Me.Size = New Size(163, 51)
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "Control Event Handlers"

    Private Sub TimePeriodLengthText_Change()
        OnChange(EventArgs.Empty)
    End Sub

    Private Sub TimePeriodLengthText_KeyPress(ByRef KeyAscii As Short)
        Try

            If KeyAscii = System.Windows.Forms.Keys.Back Then Exit Sub
            If KeyAscii = System.Windows.Forms.Keys.Tab Then Exit Sub
            If KeyAscii = System.Windows.Forms.Keys.Left Then Exit Sub
            If KeyAscii = System.Windows.Forms.Keys.Right Then Exit Sub

            If Chr(KeyAscii) < "0" Or Chr(KeyAscii) > "9" Then KeyAscii = 0 : Exit Sub
            Dim l = CInt(TimePeriodLengthText.Text & CStr(KeyAscii))
        Catch
            KeyAscii = 0
        End Try
    End Sub

    Private Sub TimePeriodCombo_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles TimePeriodCombo.SelectionChangeCommitted
        OnChange(EventArgs.Empty)
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public Property ControlsBackColor() As System.Drawing.Color
        Get
            Return TimePeriodCombo.BackColor
        End Get
        Set
            TimePeriodLengthText.BackColor = Value
            TimePeriodCombo.BackColor = Value
        End Set
    End Property

    Public Property ControlsForeColor() As System.Drawing.Color
        Get
            Return TimePeriodCombo.ForeColor
        End Get
        Set
            TimePeriodLengthText.ForeColor = Value
            TimePeriodCombo.ForeColor = Value
        End Set
    End Property

    Public ReadOnly Property IsTimePeriodValid() As Boolean
        Get
            If TimePeriodLengthText.Text = "" Then Return False
            Return mValidator.IsValidTimePeriod(TimePeriod)
        End Get
    End Property

    Public ReadOnly Property TimePeriod() As TimePeriod
        Get
            Return TWUtilities.GetTimePeriod(CInt(TimePeriodLengthText.Text), TWUtilities.TimePeriodUnitsFromString(TimePeriodCombo.SelectedItem.ToString))
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub Initialise(initialTimePeriod As TimePeriod, validator As ITimePeriodValidator)
        mValidator = validator
        TimePeriodLengthText.Text = CStr(initialTimePeriod.Length)
        setupTimePeriodUnitsCombo(initialTimePeriod)
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub addItem(value As TimePeriodUnits)
        If mValidator.IsSupportedTimePeriodUnit(value) Then TimePeriodCombo.Items.Add(TWUtilities.TimePeriodUnitsToString(value))
    End Sub

    Private Function setUnitsSelection(value As TimePeriodUnits) As Boolean
        If mValidator.IsSupportedTimePeriodUnit(value) Then
            TimePeriodCombo.SelectedItem = TWUtilities.TimePeriodUnitsToString(value)
            Return True
        End If
    End Function

    Private Sub setupTimePeriodUnitsCombo(initialTimePeriod As TimePeriod)
        addItem(TimePeriodUnits.TimePeriodSecond)
        addItem(TimePeriodUnits.TimePeriodMinute)
        addItem(TimePeriodUnits.TimePeriodHour)
        addItem(TimePeriodUnits.TimePeriodDay)
        addItem(TimePeriodUnits.TimePeriodWeek)
        addItem(TimePeriodUnits.TimePeriodMonth)
        addItem(TimePeriodUnits.TimePeriodYear)
        addItem(TimePeriodUnits.TimePeriodVolume)
        addItem(TimePeriodUnits.TimePeriodTickVolume)
        addItem(TimePeriodUnits.TimePeriodTickMovement)
        If setUnitsSelection(initialTimePeriod.Units) Then
        ElseIf setUnitsSelection(TimePeriodUnits.TimePeriodMinute) Then
        ElseIf setUnitsSelection(TimePeriodUnits.TimePeriodHour) Then
        ElseIf setUnitsSelection(TimePeriodUnits.TimePeriodDay) Then
        ElseIf setUnitsSelection(TimePeriodUnits.TimePeriodWeek) Then
        ElseIf setUnitsSelection(TimePeriodUnits.TimePeriodMonth) Then
        Else
            setUnitsSelection(TimePeriodUnits.TimePeriodYear)
        End If

    End Sub

#End Region

End Class
