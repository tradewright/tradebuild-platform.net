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
Imports StudyUtils27
Imports TradeWright.Trading.Utils.Charts

Friend Class StudyConfigurerParameterLine

    Private mStudyParameterDef As StudyParameterDefinition

    Friend ReadOnly Property Value() As String
        Get
            Try
                If mStudyParameterDef.parameterType = StudyParameterTypes.ParameterTypeBoolean Then
                    Return ParameterValueCheck.Checked.ToString
                ElseIf ParameterValueText.Visible Then
                    Return ParameterValueText.Text
                ElseIf ParameterValueUpDown.Visible Then
                    Return CStr(ParameterValueUpDown.Value)
                Else
                    Return ParameterValueCombo.Text
                End If
            Catch ex As Exception
                gLogger.log(TWUtilities40.LogLevels.LogLevelSevere, ex.ToString)
                Return ""
            End Try
        End Get
    End Property

    Friend Sub Initialise(studyParameterDef As StudyParameterDefinition, defaultValue As String, noParameterModification As Boolean)
        Try
            Dim permittedParamValues() As Object
            Dim numPermittedParamValues As Integer

            mStudyParameterDef = studyParameterDef

            ParameterValueCombo.Visible = False
            ParameterValueText.Visible = False
            ParameterValueCheck.Visible = False
            ParameterValueUpDown.Visible = False

            ParameterNameLabel.Text = studyParameterDef.name

            permittedParamValues = DirectCast(studyParameterDef.permittedValues, Object())

            Try
                numPermittedParamValues = UBound(permittedParamValues)
            Catch
                numPermittedParamValues = -1
            End Try

            If numPermittedParamValues <> -1 Then
                For Each permittedParamValue As Object In permittedParamValues
                    ParameterValueCombo.Items.Add(permittedParamValue)
                Next permittedParamValue
                ParameterValueCombo.Left = ParameterValueText.Left
                ParameterValueCombo.SelectedItem = defaultValue
                ToolTip1.SetToolTip(ParameterValueCombo, studyParameterDef.description)
                ParameterValueCombo.Visible = True
                ParameterValueCombo.Enabled = (Not noParameterModification)
            ElseIf studyParameterDef.parameterType = StudyParameterTypes.ParameterTypeBoolean Then
                ParameterValueCheck.Left = ParameterValueText.Left
                Select Case UCase(defaultValue)
                    Case "Y", "YES", "T", "TRUE", "1"
                        ParameterValueCheck.CheckState = System.Windows.Forms.CheckState.Checked
                    Case "N", "NO", "F", "FALSE", "0"
                        ParameterValueCheck.CheckState = System.Windows.Forms.CheckState.Unchecked
                    Case Else
                        ParameterValueCheck.CheckState = System.Windows.Forms.CheckState.Unchecked
                End Select
                ParameterValueCheck.Visible = True
                ParameterValueCheck.Enabled = (Not noParameterModification)
            Else
                If studyParameterDef.parameterType = StudyParameterTypes.ParameterTypeInteger Then
                    ParameterValueUpDown.Minimum = 1
                    ParameterValueUpDown.Maximum = 255
                    ParameterValueUpDown.Value = CDec(defaultValue)
                    ToolTip1.SetToolTip(ParameterValueUpDown, studyParameterDef.description)
                    ParameterValueUpDown.Visible = True
                    ParameterValueUpDown.Enabled = (Not noParameterModification)
                Else
                    If studyParameterDef.parameterType = StudyParameterTypes.ParameterTypeReal Then
                        ParameterValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
                    Else
                        ParameterValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
                    End If
                    ParameterValueText.Text = defaultValue
                    ToolTip1.SetToolTip(ParameterValueText, studyParameterDef.description)
                    ParameterValueText.Visible = True
                    ParameterValueText.Enabled = (Not noParameterModification)
                End If
            End If

        Catch ex As Exception
            gLogger.log(TWUtilities40.LogLevels.LogLevelSevere, ex.ToString)
        End Try
    End Sub

End Class
