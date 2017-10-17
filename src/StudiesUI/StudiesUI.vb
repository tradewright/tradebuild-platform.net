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

Imports TradeWright.Trading.Utils.Charts

Public Class StudiesUI

    Private Shared mConfigForm As fStudyConfigurer

    Private Sub New()
        ' prevent instantiation 
    End Sub

    '/**
    '   Returns the required studyConfiguration if the config form is not cancelled by the user
    '*/
    Public Shared Function ShowConfigForm(chartManager As ChartManager, studyName As String, slName As String, defaultConfiguration As StudyConfiguration) As StudyConfiguration

        If mConfigForm Is Nothing Then mConfigForm = New fStudyConfigurer

        Dim noParameterModification = ((defaultConfiguration IsNot Nothing) AndAlso (chartManager.BaseStudy IsNot Nothing) AndAlso (defaultConfiguration.Study Is chartManager.BaseStudy))

        mConfigForm.Initialise(chartManager,
                               studyName,
                               slName,
                               defaultConfiguration,
                               noParameterModification)

        Dim result = mConfigForm.ShowDialog(Form.ActiveForm)
        If result = DialogResult.OK Then
            Return mConfigForm.StudyConfiguration
        Else
            Return Nothing
        End If
    End Function


End Class
