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

Imports System.Collections.Generic

Imports StudyUtils27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Charts

Public Class StudyPicker
    Inherits System.Windows.Forms.UserControl

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

    Private WithEvents mChartManager As ChartManager

    Private mAvailableStudies() As StudyUtils27.StudyListEntry

    Private mStudyConfigurations As StudyConfigurations

    Private mNodeMap As Dictionary(Of String, TreeNode)

#End Region

#Region "UserControl Event Handlers"

#End Region

#Region "Control Event Handlers"

    Private Sub AddButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles AddButton.Click
        Dim slName = mAvailableStudies(StudyList.SelectedIndex).StudyLibrary
        Dim defaultStudyConfig = mChartManager.GetDefaultStudyConfiguration(mAvailableStudies(StudyList.SelectedIndex).Name, slName)

        If Not defaultStudyConfig Is Nothing Then
            addStudyToChart(defaultStudyConfig)
        Else
            Dim studyConfig = StudiesUI.ShowConfigForm(mChartManager, mAvailableStudies(StudyList.SelectedIndex).Name, mAvailableStudies(StudyList.SelectedIndex).StudyLibrary, Nothing)
            If studyConfig Is Nothing Then Exit Sub
            addStudyToChart(studyConfig)
        End If

    End Sub

    Private Sub ChangeButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles ChangeButton.Click
        Dim studyConfig = mChartManager.GetStudyConfiguration(ChartStudiesTree.SelectedNode.Name)

        ' NB: the following line displays a modal form, so we can remove the existing
        ' study and deal with any related studies after it
        Dim newStudyConfig = StudiesUI.ShowConfigForm(mChartManager, studyConfig.Name, studyConfig.StudyLibraryName, studyConfig)
        If newStudyConfig IsNot Nothing Then

            If studyConfig.Study Is mChartManager.BaseStudy Then
                newStudyConfig.Study = studyConfig.Study
                mChartManager.SetBaseStudyConfiguration(newStudyConfig)
            Else
                mChartManager.ReplaceStudyConfiguration(studyConfig, newStudyConfig)
            End If

            RemoveButton.Enabled = False
            ChangeButton.Enabled = False
            HelpText.Text = ""
        End If

    End Sub

    Private Sub ChartStudiesTree_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles ChartStudiesTree.NodeMouseClick
        Dim studyDef As StudyUtils27.StudyDefinition
        Dim studyConfig As StudyConfiguration

        If e.Node Is Nothing Then
            RemoveButton.Enabled = False
            ChangeButton.Enabled = False
        Else
            studyConfig = mChartManager.GetStudyConfiguration(e.Node.Name)
            studyDef = mChartManager.StudyLibraryManager.GetStudyDefinition(studyConfig.Name, studyConfig.StudyLibraryName)
            If Not studyDef Is Nothing Then
                StudyList.SelectedIndex = -1
                AddButton.Enabled = False
                ConfigureButton.Enabled = False

                'WebBrowser1.DocumentText = studyDef.description
                HelpText.Text = studyDef.Description
                RemoveButton.Enabled = Not (studyConfig.Study Is mChartManager.BaseStudy)
                ChangeButton.Enabled = True
            End If
        End If
    End Sub

    Private Sub ConfigureButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles ConfigureButton.Click
        Dim slName = mAvailableStudies(StudyList.SelectedIndex).StudyLibrary
        Dim defaultStudyConfig = mChartManager.GetDefaultStudyConfiguration(mAvailableStudies(StudyList.SelectedIndex).Name, slName)
        Dim studyConfig As StudyConfiguration
        If defaultStudyConfig IsNot Nothing Then
            studyConfig = StudiesUI.ShowConfigForm(mChartManager, mAvailableStudies(StudyList.SelectedIndex).Name, mAvailableStudies(StudyList.SelectedIndex).StudyLibrary, defaultStudyConfig)
        Else
            studyConfig = StudiesUI.ShowConfigForm(mChartManager, mAvailableStudies(StudyList.SelectedIndex).Name, mAvailableStudies(StudyList.SelectedIndex).StudyLibrary, Nothing)
        End If
        If studyConfig IsNot Nothing Then addStudyToChart(studyConfig)
    End Sub

    Private Sub RemoveButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles RemoveButton.Click
        Dim studyConfig As StudyConfiguration
        studyConfig = mChartManager.GetStudyConfiguration(ChartStudiesTree.SelectedNode.Name)
        mChartManager.RemoveStudyConfiguration(studyConfig)
        RemoveButton.Enabled = False
        ChangeButton.Enabled = False
    End Sub

    Private Sub StudyList_SelectedIndexChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles StudyList.SelectedIndexChanged
        If mChartManager Is Nothing Then Exit Sub

        If StudyList.SelectedIndex <> -1 Then
            RemoveButton.Enabled = False
            ChangeButton.Enabled = False

            AddButton.Enabled = True
            ConfigureButton.Enabled = True
            Dim spName = mAvailableStudies(StudyList.SelectedIndex).StudyLibrary
            Dim studyDef = mChartManager.StudyLibraryManager.GetStudyDefinition(mAvailableStudies(StudyList.SelectedIndex).Name, spName)
            HelpText.Text = studyDef.Description
        Else
            AddButton.Enabled = False
            ConfigureButton.Enabled = False
        End If
    End Sub

#End Region

#Region "mChartManager Event Handlers"

    Private Sub mChartManager_StudyAdded(sender As Object, study As _IStudy) Handles mChartManager.StudyAdded
        Dim parentNode As TreeNode = Nothing

        If Not study Is Nothing Then
            mNodeMap.TryGetValue(study.Id, parentNode)
        End If
        addChartStudiesTreeNode(study, parentNode)
    End Sub

    Private Sub mChartManager_StudyRemoved(sender As Object, study As _IStudy) Handles mChartManager.StudyRemoved
        Dim lNode As TreeNode = Nothing
        If mNodeMap.TryGetValue(study.Id, lNode) Then
            If lNode Is Nothing Then
                ChartStudiesTree.Nodes.Remove(lNode)
            Else
                lNode.Parent.Nodes.Remove(lNode)
            End If
            mNodeMap.Remove(study.Id)
        End If
    End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Public Sub Initialise(pChartManager As ChartManager)

        mNodeMap = New Dictionary(Of String, TreeNode)

        mChartManager = pChartManager

        HelpText.Text = ""
        ChartStudiesTree.Nodes.Clear()
        StudyList.Items.Clear()

        If mChartManager Is Nothing Then
        ElseIf mChartManager.BaseStudyConfiguration Is Nothing Then
        Else
            addEntryToChartStudiesTree(mChartManager.BaseStudyConfiguration, Nothing)
            mAvailableStudies = mChartManager.StudyLibraryManager.GetAvailableStudies

            For i = 0 To UBound(mAvailableStudies)
                Dim itemText = mAvailableStudies(i).Name & "  (" & mAvailableStudies(i).StudyLibrary & ")"
                StudyList.Items.Add(itemText)
            Next
        End If

        AddButton.Enabled = False
        ConfigureButton.Enabled = False
        RemoveButton.Enabled = False
        ChangeButton.Enabled = False

    End Sub

#End Region

#Region "Helper Functions"

    Private Sub addChartStudiesTreeNode(study As _IStudy, parentNode As TreeNode)
        If parentNode Is Nothing Then
            mNodeMap.Add(study.Id, ChartStudiesTree.Nodes.Add(study.Id, study.InstanceName))
        Else
            mNodeMap.Add(study.Id, parentNode.Nodes.Add(study.Id, study.InstanceName))
            parentNode.Expand()
        End If
    End Sub

    Private Sub addEntryToChartStudiesTree(
                studyConfig As StudyConfiguration,
                parentStudyConfig As StudyConfiguration)

        Dim parentNode As TreeNode = Nothing
        If studyConfig Is Nothing Then Exit Sub

        If parentStudyConfig IsNot Nothing Then mNodeMap.TryGetValue(parentStudyConfig.Study.Id, parentNode)

        addChartStudiesTreeNode(studyConfig.Study, parentNode)

        For Each childStudyConfig As StudyConfiguration In studyConfig.StudyConfigurations
            addEntryToChartStudiesTree(childStudyConfig, studyConfig)
        Next
    End Sub

    Private Sub addStudyToChart(studyConfig As StudyConfiguration)
        Try
            mChartManager.AddStudyConfiguration(studyConfig)
            mChartManager.StartStudy(studyConfig.Study)
        Catch ex As Exception
            gLogger.Log(LogLevels.LogLevelSevere, "Error at: " & ex.Source & vbCrLf & ex.ToString)
            Initialise(Nothing)
        End Try
    End Sub

    Private Sub ReplaceStudyInChart(
                    oldStudyConfig As StudyConfiguration,
                    newStudyConfig As StudyConfiguration)
        Try
            mChartManager.ReplaceStudyConfiguration(oldStudyConfig, newStudyConfig)
        Catch ex As Exception
            gLogger.Log(LogLevels.LogLevelSevere, ex.ToString)
            Initialise(Nothing)
        End Try
    End Sub

#End Region

End Class