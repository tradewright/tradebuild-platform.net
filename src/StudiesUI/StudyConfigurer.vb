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
Imports System.Collections.Generic
Imports TradeWright.Trading.Utils.Charts
Imports TWUtilities40

Public Class StudyConfigurer

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const CompatibleNode As String = "YES"

    Private Const RegionDefault As String = "Use default"
    Private Const RegionCustom As String = "Use own region"
    Private Const RegionUnderlying As String = "Use underlying study's region"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mChart As AxChartSkil27.AxChart
    Private mStudyname As String
    Private mStudyLibraryName As String

    Private mStudyDefinition As StudyDefinition

    Private mBaseStudyConfig As StudyConfiguration

    Private mInitialConfiguration As StudyConfiguration

    Private mParameterLines As New List(Of StudyConfigurerParameterLine)

    Private mValueLines As New List(Of StudyConfigurerValueLine)

    Private mInputLines As New List(Of StudyConfigurerInputLine)

    Private mCompatibleStudies As New Dictionary(Of String, _IStudy)

    Private mPrevSelectedBaseStudiesTreeNode As TreeNode

    Private mNodeMap As New Dictionary(Of String, TreeNode)

#End Region

#Region "Form Event Handlers"


    Public Sub New()
        MyBase.New()
        Try
            InitializeComponent()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

#End Region

#Region "XXXX Interface members"

#End Region

#Region "Control Event Handlers"

    Private Sub BaseStudiesTree_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles BaseStudiesTree.AfterSelect
        e.Node.Expand()
        If e.Node.Tag Is Nothing Then
            BaseStudiesTree.SelectedNode = mPrevSelectedBaseStudiesTreeNode
        Else
            mPrevSelectedBaseStudiesTreeNode = e.Node
            Dim studyValueDefs = baseStudyValueDefs()
            For i As Integer = 0 To mStudyDefinition.StudyInputDefinitions.Count - 1
                mInputLines(i).InitialiseInputValueCombo(studyValueDefs)
            Next
        End If
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Public Sub Clear()
        mNodeMap.Clear()
        mPrevSelectedBaseStudiesTreeNode = Nothing
        initialiseControls()
    End Sub

    Public Function GetStudyConfiguration() As StudyConfiguration
        Try
            Dim studyParamDef As StudyParameterDefinition
            Dim studyValueDefs As StudyValueDefinitions
            Dim studyValueDef As StudyValueDefinition
            Dim studyValueconfig As StudyValueConfiguration
            Dim studyHorizRule As StudyHorizontalRule
            Dim regionName As String = ""
            Dim inputValueNames() As String
            Dim i As Integer

            Dim studyConfig = New StudyConfiguration
            studyConfig.Name = mStudyname
            studyConfig.StudyLibraryName = mStudyLibraryName
            If Not BaseStudiesTree.SelectedNode Is Nothing Then
                studyConfig.UnderlyingStudy = DirectCast(BaseStudiesTree.SelectedNode.Tag, IStudy)
            End If

            If Not mInitialConfiguration Is Nothing AndAlso TypeOf mInitialConfiguration.UnderlyingStudy Is StudyInputHandler Then
                studyConfig.Study = mInitialConfiguration.Study
            End If

            ReDim inputValueNames(mStudyDefinition.StudyInputDefinitions.Count - 1)
            For i = 0 To UBound(inputValueNames)
                inputValueNames(i) = mInputLines(i).InputValueName
            Next
            studyConfig.InputValueNames = inputValueNames

            If ChartRegionCombo.SelectedItem.ToString = RegionDefault Then
                Select Case mStudyDefinition.DefaultRegion
                    Case StudyDefaultRegions.StudyDefaultRegionNone
                        regionName = ChartUtils.ChartRegionNameUnderlying
                    Case StudyDefaultRegions.StudyDefaultRegionCustom
                        regionName = ChartUtils.ChartRegionNameCustom
                    Case StudyDefaultRegions.StudyDefaultRegionUnderlying
                        regionName = ChartUtils.ChartRegionNameUnderlying
                End Select
            ElseIf ChartRegionCombo.SelectedItem.ToString = RegionCustom Then
                regionName = ChartUtils.ChartRegionNameCustom
            ElseIf ChartRegionCombo.SelectedItem.ToString = RegionUnderlying Then
                regionName = ChartUtils.ChartRegionNameUnderlying
            Else
                regionName = ChartRegionCombo.SelectedItem.ToString
            End If
            studyConfig.ChartRegionName = regionName

            Dim params = New Parameters
            For i = 0 To mStudyDefinition.StudyParameterDefinitions.Count - 1
                studyParamDef = mStudyDefinition.StudyParameterDefinitions.Item(i + 1)
                params.SetParameterValue(studyParamDef.Name, mParameterLines(i).Value)
            Next

            studyConfig.Parameters = params

            studyValueDefs = mStudyDefinition.StudyValueDefinitions

            For i = 0 To studyValueDefs.Count - 1
                studyValueDef = studyValueDefs.Item(i + 1)

                studyValueconfig = studyConfig.StudyValueConfigurations.Add(studyValueDef.Name)
                mValueLines(i).ApplyUpdates(studyValueconfig)

            Next

            If Line1Text.Text <> "" Then
                studyHorizRule = studyConfig.StudyHorizontalRules.add
                studyHorizRule.y = CDbl(Line1Text.Text)
                studyHorizRule.color = System.Drawing.ColorTranslator.ToOle(Line1ColorPicker.Color)
            End If

            If Line2Text.Text <> "" Then
                studyHorizRule = studyConfig.StudyHorizontalRules.add
                studyHorizRule.y = CDbl(Line2Text.Text)
                studyHorizRule.color = System.Drawing.ColorTranslator.ToOle(Line2ColorPicker.Color)
            End If

            If Line3Text.Text <> "" Then
                studyHorizRule = studyConfig.StudyHorizontalRules.add
                studyHorizRule.y = CDbl(Line3Text.Text)
                studyHorizRule.color = System.Drawing.ColorTranslator.ToOle(Line3ColorPicker.Color)
            End If

            If Line4Text.Text <> "" Then
                studyHorizRule = studyConfig.StudyHorizontalRules.add
                studyHorizRule.y = CDbl(Line4Text.Text)
                studyHorizRule.color = System.Drawing.ColorTranslator.ToOle(Line4ColorPicker.Color)
            End If

            If Line5Text.Text <> "" Then
                studyHorizRule = studyConfig.StudyHorizontalRules.add
                studyHorizRule.y = CDbl(Line5Text.Text)
                studyHorizRule.color = System.Drawing.ColorTranslator.ToOle(Line5ColorPicker.Color)
            End If

            Return studyConfig

        Catch ex As Exception
            gLogger.Log(TWUtilities40.LogLevels.LogLevelSevere, ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Sub Initialise(
                chartManager As ChartManager,
                studyName As String,
                studyLibraryName As String,
                initialConfiguration As StudyConfiguration,
                noParameterModification As Boolean)
        Try
            If initialConfiguration Is Nothing Then Throw New ArgumentException("initialConfiguration cannot be Nothing")

            Clear()

            mChart = chartManager.Chart
            mStudyDefinition = chartManager.StudyLibraryManager.GetStudyDefinition(studyName, studyLibraryName)
            mStudyLibraryName = studyLibraryName
            mBaseStudyConfig = chartManager.BaseStudyConfiguration
            mInitialConfiguration = initialConfiguration

            processRegionNames(chartManager.RegionNames)

            setupBaseStudiesTree()

            processStudyDefinition(mInitialConfiguration.StudyValueConfigurations,
                        mInitialConfiguration.Parameters,
                        mInitialConfiguration.StudyHorizontalRules,
                        noParameterModification)
        Catch ex As Exception
            gLogger.Log(TWUtilities40.LogLevels.LogLevelSevere, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub addBaseStudiesTreeEntry(
                    studyConfig As StudyConfiguration,
                    parentStudyConfig As StudyConfiguration)
        Dim lNode As TreeNode

        If studyConfig Is Nothing Then Exit Sub

        If mInitialConfiguration IsNot Nothing Then
            If mInitialConfiguration.Study Is studyConfig.Study Then Exit Sub
        End If

        If parentStudyConfig Is Nothing Then
            lNode = BaseStudiesTree.Nodes.Add(studyConfig.Study.Id, studyConfig.Study.InstanceName)
            mNodeMap.Add(studyConfig.Study.Id, lNode)
        Else
            Dim parentNode = mNodeMap.Item(parentStudyConfig.Study.Id)
            lNode = parentNode.Nodes.Add(studyConfig.Study.Id, studyConfig.Study.InstanceName)
            mNodeMap.Add(studyConfig.Study.Id, lNode)
            parentNode.Expand()
        End If

        If (Not TypeOf studyConfig.Study Is StudyInputHandler Or
            Not mStudyDefinition.NeedsBars) _
        Then
            If studiesCompatible(studyConfig.Study.StudyDefinition, mStudyDefinition) Then
                lNode.Tag = studyConfig.Study
                If mPrevSelectedBaseStudiesTreeNode Is Nothing Then
                    mPrevSelectedBaseStudiesTreeNode = lNode
                    BaseStudiesTree.SelectedNode = lNode
                End If
            Else
                lNode.BackColor = Color.FromArgb(&HC0C0C0)
                lNode.ForeColor = Color.FromArgb(&H808080)
            End If
        End If

        For Each childStudyConfig As StudyConfiguration In studyConfig.StudyConfigurations
            addBaseStudiesTreeEntry(childStudyConfig, studyConfig)
        Next
    End Sub

    Private Function baseStudyValueDefs() As StudyValueDefinitions
        If BaseStudiesTree.SelectedNode Is Nothing Then
            Return Nothing
        Else
            Return CType(BaseStudiesTree.SelectedNode.Tag, IStudy).StudyDefinition.StudyValueDefinitions
        End If
    End Function

    Private Sub initialiseControls()

        BaseStudiesTree.Enabled = True

        For Each inputLine As StudyConfigurerInputLine In mInputLines
            inputLine.Visible = False
        Next

        For Each parameterLine As StudyConfigurerParameterLine In mParameterLines
            parameterLine.Visible = False
        Next

        For Each valueLine As StudyConfigurerValueLine In mValueLines
            valueLine.Visible = False
        Next

    End Sub

    Private Sub processRegionNames(ByRef regionNames() As String)
        Dim i As Integer

        ChartRegionCombo.Items.Clear()

        ChartRegionCombo.Items.Add(RegionDefault)
        ChartRegionCombo.Items.Add(RegionCustom)

        For i = 0 To UBound(regionNames)
            ChartRegionCombo.Items.Add(regionNames(i))
        Next
        ChartRegionCombo.SelectedIndex = 0
    End Sub

    Private Sub processStudyDefinition(
                pStudyValueconfigs As StudyValueConfigurations,
                pParameters As Parameters,
                pStudyHorizRules As StudyHorizontalRules,
                noParameterModification As Boolean)

        mStudyname = mStudyDefinition.Name

        HelpText.Text = mStudyDefinition.Description

        If mInitialConfiguration.ChartRegionName = mInitialConfiguration.InstanceFullyQualifiedName Then
            ChartRegionCombo.SelectedItem = RegionCustom
        Else
            ChartRegionCombo.SelectedItem = mInitialConfiguration.ChartRegionName
        End If

        Dim allowInputModification As Boolean = False
        If mInitialConfiguration.UnderlyingStudy IsNot Nothing Then
            If TypeOf mInitialConfiguration.UnderlyingStudy Is StudyInputHandler Then
                allowInputModification = True
                mCompatibleStudies.Add(mInitialConfiguration.UnderlyingStudy.Id, mInitialConfiguration.UnderlyingStudy)
                BaseStudiesTree.Nodes.Clear()
                Dim node = New TreeNode(mInitialConfiguration.UnderlyingStudy.InstanceName)
                node.Tag = mInitialConfiguration.UnderlyingStudy
                BaseStudiesTree.Nodes.Add(node)
                BaseStudiesTree.SelectedNode = node
                BaseStudiesTree.Enabled = False
            Else
                BaseStudiesTree.SelectedNode = mNodeMap.Item(mInitialConfiguration.UnderlyingStudy.Id)
            End If
        End If

        Dim studyInputDefinitions = mStudyDefinition.StudyInputDefinitions

        Dim inputline As StudyConfigurerInputLine

        For i As Integer = 0 To studyInputDefinitions.Count - 1
            Dim studyInputDef = studyInputDefinitions.Item(i + 1)

            If i >= mInputLines.Count Then
                inputline = New StudyConfigurerInputLine
                mInputLines.Add(inputline)
                inputline.Location = New System.Drawing.Point(0, 45 * i)
                inputline.Size = New System.Drawing.Size(InputsPanel.Size.Width, 39)
                inputline.Anchor = CType(AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
                InputsPanel.Controls.Add(inputline)
            Else
                inputline = mInputLines(i)
            End If

            If mInitialConfiguration Is Nothing Then
                inputline.Initialise(studyInputDef, "", baseStudyValueDefs, allowInputModification)
            Else
                inputline.Initialise(studyInputDef, mInitialConfiguration.InputValueNames(i), baseStudyValueDefs, allowInputModification)
            End If
            inputline.Visible = True

        Next

        If studyInputDefinitions.Count < mInputLines.Count Then
            For i As Integer = studyInputDefinitions.Count + 1 To mInputLines.Count
                mInputLines(i - 1).Visible = False
            Next
        End If

        Dim studyParameterDefinitions = mStudyDefinition.StudyParameterDefinitions

        For i As Integer = 0 To studyParameterDefinitions.Count - 1
            Dim studyParamDef = studyParameterDefinitions.Item(i + 1)

            Dim paramline As StudyConfigurerParameterLine
            If i >= mParameterLines.Count Then
                paramline = New StudyConfigurerParameterLine
                mParameterLines.Add(paramline)
                paramline.Location = New System.Drawing.Point(0, 26 * i)
                paramline.Size = New System.Drawing.Size(ParametersPanel.Size.Width, 21)
                paramline.Anchor = CType(AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
                ParametersPanel.Controls.Add(paramline)
            Else
                paramline = mParameterLines(i)
            End If

            paramline.Initialise(studyParamDef, pParameters.GetParameterValue(studyParamDef.Name), noParameterModification)
            paramline.Visible = True

        Next

        If studyParameterDefinitions.Count < mParameterLines.Count Then
            For i As Integer = studyParameterDefinitions.Count + 1 To mParameterLines.Count
                mParameterLines(i - 1).Visible = False
            Next
        End If

        Dim studyValueDefinitions = mStudyDefinition.StudyValueDefinitions

        For i As Integer = 0 To studyValueDefinitions.Count - 1
            Dim studyValueDef = studyValueDefinitions.Item(i + 1)

            Dim studyValueConfig As StudyValueConfiguration = Nothing
            If Not pStudyValueconfigs Is Nothing Then
                Try
                    studyValueConfig = pStudyValueconfigs.Item(studyValueDef.Name)
                Catch
                    studyValueConfig = Nothing
                End Try
            End If

            Dim valueLine As StudyConfigurerValueLine
            If i >= mValueLines.Count Then
                valueLine = New StudyConfigurerValueLine
                mValueLines.Add(valueLine)
                valueLine.Location = New System.Drawing.Point(0, 26 * (i + 1))
                valueLine.Size = New System.Drawing.Size(ValuesPanel.Size.Width, 21)
                valueLine.Anchor = CType(AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right, AnchorStyles)
                ValuesPanel.Controls.Add(valueLine)
            Else
                valueLine = mValueLines(i)
                valueLine.Visible = True
            End If

            valueLine.Initialise(studyValueDef, studyValueconfig, mChart)
            valueLine.Visible = True

        Next

        If studyValueDefinitions.Count < mValueLines.Count Then
            For i As Integer = studyValueDefinitions.Count + 1 To mValueLines.Count
                mValueLines(i - 1).Visible = False
            Next
        End If

        If Not pStudyHorizRules Is Nothing Then
            Dim studyHorizRule As StudyHorizontalRule
            If pStudyHorizRules.count >= 1 Then
                studyHorizRule = pStudyHorizRules.item(1)
                Line1Text.Text = Format(studyHorizRule.y, "0.000")
                Line1ColorPicker.Color = System.Drawing.ColorTranslator.FromOle(studyHorizRule.color)
            End If
            If pStudyHorizRules.count >= 2 Then
                studyHorizRule = pStudyHorizRules.item(2)
                Line2Text.Text = Format(studyHorizRule.y, "0.000")
                Line2ColorPicker.Color = System.Drawing.ColorTranslator.FromOle(studyHorizRule.color)
            End If
            If pStudyHorizRules.count >= 3 Then
                studyHorizRule = pStudyHorizRules.item(3)
                Line3Text.Text = Format(studyHorizRule.y, "0.000")
                Line3ColorPicker.Color = System.Drawing.ColorTranslator.FromOle(studyHorizRule.color)
            End If
            If pStudyHorizRules.count >= 4 Then
                studyHorizRule = pStudyHorizRules.item(4)
                Line4Text.Text = Format(studyHorizRule.y, "0.000")
                Line4ColorPicker.Color = System.Drawing.ColorTranslator.FromOle(studyHorizRule.color)
            End If
            If pStudyHorizRules.count >= 5 Then
                studyHorizRule = pStudyHorizRules.item(5)
                Line5Text.Text = Format(studyHorizRule.y, "0.000")
                Line5ColorPicker.Color = System.Drawing.ColorTranslator.FromOle(studyHorizRule.color)
            End If
        End If
    End Sub

    Private Sub setupBaseStudiesTree()
        Dim studyConfig As StudyConfiguration

        BaseStudiesTree.Nodes.Clear()

        If mBaseStudyConfig Is Nothing Then Exit Sub

        studyConfig = mBaseStudyConfig
        addBaseStudiesTreeEntry(studyConfig, Nothing)
    End Sub

    Private Function studiesCompatible(sourceStudyDefinition As StudyDefinition, sinkStudyDefinition As StudyDefinition) As Boolean
        studiesCompatible = False
        For i = 1 To sinkStudyDefinition.StudyInputDefinitions.Count
            Dim sinkInputDef = sinkStudyDefinition.StudyInputDefinitions.Item(i)
            For Each sourceValueDef As StudyValueDefinition In sourceStudyDefinition.StudyValueDefinitions
                If typesCompatible(sourceValueDef.ValueType, sinkInputDef.InputType) Then
                    studiesCompatible = True
                    Exit For
                End If
            Next sourceValueDef
            If Not studiesCompatible Then Exit Function
        Next
    End Function

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

    Private Class BaseStudyComboItem
        Private _study As IStudy

        Sub New(study As IStudy)
            _study = study
        End Sub

        ReadOnly Property instanceName() As String
            Get
                Return _study.InstanceName
            End Get
        End Property

        ReadOnly Property study() As IStudy
            Get
                Return _study
            End Get
        End Property
#End Region

End Class

End Class
