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

Friend Class StudyConfigurerInputLine

    Private mStudyInputDef As StudyInputDefinition

    Friend ReadOnly Property InputValueName() As String
        Get
            Return InputValueCombo.Text
        End Get
    End Property

    Friend Sub Initialise(studyInputDef As StudyInputDefinition, defaultInputValueName As String, studyValueDefs As StudyValueDefinitions, noInputModification As Boolean)

        mStudyInputDef = studyInputDef

        InputValueNameLabel.Text = studyInputDef.name

        ToolTip1.SetToolTip(InputValueCombo, studyInputDef.description)

        InitialiseInputValueCombo(studyValueDefs)

        If defaultInputValueName <> "" Then
            InputValueCombo.SelectedItem = defaultInputValueName
        End If

        InputValueCombo.Enabled = Not noInputModification
    End Sub

    Public Sub InitialiseInputValueCombo(studyValueDefs As StudyValueDefinitions)
        Dim valueDef As StudyValueDefinition
        Dim selIndex As Integer

        InputValueCombo.Items.Clear()

        selIndex = -1
        InputValueCombo.Items.Add("")
        For Each valueDef In studyValueDefs
            If typesCompatible(valueDef.valueType, mStudyInputDef.inputType) Then
                InputValueCombo.Items.Add(valueDef.name)
                If UCase(mStudyInputDef.name) = UCase(valueDef.name) Then selIndex = InputValueCombo.Items.Count - 1
                If valueDef.isDefault And selIndex = -1 Then selIndex = InputValueCombo.Items.Count - 1
            End If
        Next valueDef

        If InputValueCombo.Items.Count <> 0 And selIndex <> -1 Then
            InputValueCombo.SelectedIndex = selIndex
        End If

    End Sub

    Private Function typesCompatible(sourceValueType As StudyValueTypes, sinkInputType As StudyInputTypes) As Boolean
        Select Case sourceValueType
            Case StudyValueTypes.ValueTypeInteger
                Select Case sinkInputType
                    Case StudyInputTypes.InputTypeInteger
                        typesCompatible = True
                    Case StudyInputTypes.InputTypeReal
                        typesCompatible = True
                End Select
            Case StudyValueTypes.ValueTypeReal
                Select Case sinkInputType
                    Case StudyInputTypes.InputTypeReal
                        typesCompatible = True
                End Select
            Case StudyValueTypes.ValueTypeString
                Select Case sinkInputType
                    Case StudyInputTypes.InputTypeString
                        typesCompatible = True
                End Select
            Case StudyValueTypes.ValueTypeDate
                Select Case sinkInputType
                    Case StudyInputTypes.InputTypeDate
                        typesCompatible = True
                End Select
        End Select
    End Function

End Class
