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

Public Class fTimePeriodSpecifier

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

    Private mCancelled As Boolean

#End Region

#Region "Constructors"

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        mCancelled = False
        TimePeriodSpecifier1.Focus()
        If TimePeriodSpecifier1.IsTimePeriodValid Then OkButton.Enabled = True
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "Control Event Handlers"

    Private Sub CancelButton_Click() Handles CancelButton.Click
        mCancelled = True
        Me.Hide()
    End Sub

    Private Sub OKButton_Click() Handles OkButton.Click
        Me.Hide()
    End Sub

    Private Sub TimePeriodSpecifier_Change(sender As Object, e As System.EventArgs) Handles TimePeriodSpecifier1.Change
        If TimePeriodSpecifier1.IsTimePeriodValid Then
            OkButton.Enabled = True
        Else
            OkButton.Enabled = False
        End If
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Friend ReadOnly Property Cancelled() As Boolean
        Get
            Cancelled = mCancelled
        End Get
    End Property

    Friend ReadOnly Property TimePeriod() As TimePeriod
        Get
            Dim tp As TimePeriod
            If mCancelled Then
                Return Nothing
            Else
                tp = TimePeriodSpecifier1.TimePeriod
                TimePeriod = tp
            End If
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub Initialise(initialTimePeriod As TimePeriod, validator As ITimePeriodValidator)
        TimePeriodSpecifier1.Initialise(initialTimePeriod, validator)
    End Sub

#End Region

#Region "Helper Functions"


#End Region

End Class