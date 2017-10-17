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
Imports TimeframeUtils27

Imports TradeWright.Trading.Utils.Charts.BarFormatters

Public Class fApplyTemplate

    Private mTemplateManager As TemplateManager
    Private mTemplateableObject As ISupportsTemplates

    Friend Sub New(templateManager As TemplateManager,
                templateableObject As ISupportsTemplates)
        InitializeComponent()
        TemplateSelector1.Initialise(templateManager, templateableObject, False)
        mTemplateManager = templateManager
        mTemplateableObject = templateableObject
    End Sub

    Private Sub ApplyButton_Click(sender As Object, e As EventArgs) Handles ApplyButton.Click
        If Not TemplateSelector1.IsExistingTemplateSelected Then
            Me.Close()
        Else
            mTemplateableObject.LoadFromTemplate(TemplateSelector1.SelectedTemplate)
        End If
        Me.Close()
    End Sub

    Private Sub TemplateSelector1_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles TemplateSelector1.PropertyChanged
        If e.PropertyName = "IsReady" Then
            ApplyButton.Enabled = TemplateSelector1.IsReady
        End If
    End Sub

End Class