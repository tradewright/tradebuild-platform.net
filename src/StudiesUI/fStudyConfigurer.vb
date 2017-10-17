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

Imports ChartSkil27
Imports StudyUtils27
Imports TradeWright.Trading.Utils.Charts
Imports TWUtilities40

Public Class fStudyConfigurer


    '

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

#Region "Member variables"

    Private mStudyConfig As StudyConfiguration

#End Region

#Region "Form Event Handlers"

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "Control Event Handlers"

    Private Sub AddButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles AddButton.Click
        mStudyConfig = StudyConfigurer1.GetStudyConfiguration
        Me.DialogResult = DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub CancelButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles CanclButton.Click, CanclButton.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub SetDefaultButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles SetDefaultButton.Click
        Try
            ChartUtils.SetDefaultStudyConfiguration(StudyConfigurer1.GetStudyConfiguration)
        Catch ex As Exception
            gLogger.Log(LogLevels.LogLevelSevere, ex.ToString)
            Throw New InvalidOperationException("can't set default config at this time", ex)
        End Try
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public ReadOnly Property StudyConfiguration() As StudyConfiguration
        Get
            Return mStudyConfig
        End Get
    End Property

#End Region

#Region "Methods"

    Friend Sub Initialise(chartManager As ChartManager,
                          studyName As String,
                          StudyLibraryName As String,
                          initialConfiguration As StudyConfiguration,
                          noParameterModification As Boolean)

        Me.Text = studyName

        StudyConfigurer1.Initialise(chartManager, studyName, StudyLibraryName, initialConfiguration, noParameterModification)
    End Sub

#End Region

#Region "Helper Functions"

#End Region

End Class