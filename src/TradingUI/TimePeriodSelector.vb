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

Public Class TimePeriodSelector

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const TimePeriodCustom As String = "Custom"

#End Region

#Region "Member variables"

    Private mSpecifier As fTimePeriodSpecifier

    Private mLatestTimePeriod As TimePeriod

    Private mValidator As ITimePeriodValidator

    Private mUseShortTimePeriodStrings As Boolean

#End Region

#Region "Constructors"

    Private Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub New(useShortTimePeriodStrings As Boolean)
        Me.New()
        mUseShortTimePeriodStrings = useShortTimePeriodStrings
        If mUseShortTimePeriodStrings Then
            DisplayMember = "shortName"
        Else
            DisplayMember = "name"
        End If
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "Control Event Handlers"

    Protected Overrides Sub OnSelectionChangeCommitted(e As System.EventArgs)
        If Me.Text = TimePeriodCustom Then
            If mSpecifier Is Nothing Then
                mSpecifier = New fTimePeriodSpecifier
                mSpecifier.Initialise(mLatestTimePeriod, mValidator)
            End If
            mSpecifier.ShowDialog()
            If Not mSpecifier.Cancelled Then
                selectComboEntry(mSpecifier.TimePeriod)
            Else
                selectComboEntry(mLatestTimePeriod)
            End If
        End If
        MyBase.OnSelectionChangeCommitted(e)
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    ' make the items collection inaccessible to users
    Protected Shadows ReadOnly Property Items() As System.Windows.Forms.ComboBox.ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    Public Property TimePeriod() As TimePeriod
        Get
            Return CType(Me.SelectedItem, TimePeriod)
        End Get
        Set
            selectComboEntry(Value)
        End Set
    End Property

    Public Property UseShortTimePeriodStrings() As Boolean
        Get
            Return mUseShortTimePeriodStrings
        End Get
        Set
            mUseShortTimePeriodStrings = value
            If mUseShortTimePeriodStrings Then
                MyBase.DisplayMember = "shortName"
            Else
                MyBase.DisplayMember = "name"
            End If
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub AppendEntry(timePeriod As TimePeriod)
        addComboEntry(timePeriod)
    End Sub

    Public Sub Initialise(validator As ITimePeriodValidator)
        If validator Is Nothing Then Throw New ArgumentNullException("validator")
        mValidator = validator

        Me.Items.Clear()
        MyBase.Items.Add(TimePeriodCustom)
        addComboEntry(TWUtilities.GetTimePeriod(5, TimePeriodUnits.TimePeriodSecond))
        addComboEntry(TWUtilities.GetTimePeriod(15, TimePeriodUnits.TimePeriodSecond))
        addComboEntry(TWUtilities.GetTimePeriod(30, TimePeriodUnits.TimePeriodSecond))
        addComboEntry(TWUtilities.GetTimePeriod(1, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(2, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(3, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(4, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(5, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(8, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(10, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(13, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(15, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(20, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(21, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(30, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(34, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(55, TimePeriodUnits.TimePeriodMinute))
        addComboEntry(TWUtilities.GetTimePeriod(1, TimePeriodUnits.TimePeriodHour))
        addComboEntry(TWUtilities.GetTimePeriod(1, TimePeriodUnits.TimePeriodDay))
        addComboEntry(TWUtilities.GetTimePeriod(1, TimePeriodUnits.TimePeriodWeek))
        addComboEntry(TWUtilities.GetTimePeriod(1, TimePeriodUnits.TimePeriodMonth))
        addComboEntry(TWUtilities.GetTimePeriod(100, TimePeriodUnits.TimePeriodVolume))
        addComboEntry(TWUtilities.GetTimePeriod(1000, TimePeriodUnits.TimePeriodVolume))
        addComboEntry(TWUtilities.GetTimePeriod(4, TimePeriodUnits.TimePeriodTickMovement))
        addComboEntry(TWUtilities.GetTimePeriod(5, TimePeriodUnits.TimePeriodTickMovement))
        addComboEntry(TWUtilities.GetTimePeriod(10, TimePeriodUnits.TimePeriodTickMovement))
        addComboEntry(TWUtilities.GetTimePeriod(20, TimePeriodUnits.TimePeriodTickMovement))
    End Sub

    Public Function SelectTimePeriod(timeperiod As TimePeriod) As Boolean
        Return selectComboEntry(timeperiod)
    End Function

#End Region

#Region "Helper Functions"

    Private Sub addComboEntry(timePeriod As TimePeriod)
        If mValidator.IsValidTimePeriod(timePeriod) Then Me.Items.Add(timePeriod)
    End Sub

    Private Function selectComboEntry(timePeriod As TimePeriod) As Boolean
        If timePeriod Is Nothing Then Return True
        If mValidator.IsValidTimePeriod(timePeriod) Then
            If Not Me.Items.Contains(timePeriod) Then addComboEntry(timePeriod)
            Me.Text = CStr(IIf(mUseShortTimePeriodStrings, timePeriod.ToShortString, timePeriod.ToString))
            mLatestTimePeriod = timePeriod
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class
