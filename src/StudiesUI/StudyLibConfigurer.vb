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

Option Strict Off
Imports System.Windows.Forms

Imports TWUtilities40

Public Class StudyLibConfigurer
    Inherits System.Windows.Forms.UserControl

    Implements IButtonControl

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ProjectName As String = "StudiesUI26"
    Private Const ModuleName As String = "StudyLibConfigurer"

    Private Const AttributeNameStudyLibraryBuiltIn As String = "BuiltIn"
    Private Const AttributeNameStudyLibraryEnabled As String = "Enabled"
    Private Const AttributeNameStudyLibraryName As String = "Name"
    Private Const AttributeNameStudyLibraryProgId As String = "ProgId"

    Private Const ConfigNameStudyLibrary As String = "StudyLibrary"
    Private Const ConfigNameStudyLibraries As String = "StudyLibraries"

    Private Const NewStudyLibraryName As String = "New study library"
    Private Const BuiltInStudyLibraryName As String = "Built-in"

    Private Const StudyLibrariesRenderer As String = "StudiesUI26.StudyLibConfigurer"

#End Region

#Region "Member variables"

    Private mConfig As ConfigurationSection

    Private mCurrSLsList As ConfigurationSection
    Private mCurrSL As ConfigurationSection
    Private mCurrSLIndex As Integer

    Private mNames As Collection

    Private mNoCheck As Boolean

    Private mReadOnly As Boolean

#End Region

#Region "Constructors"

    Private Sub UserControl_Initialize()
        MyBase.Width = 500
        MyBase.Height = 267
    End Sub

    Private Sub StudyLibConfigurer_LostFocus(eventSender As System.Object, eventArgs As System.EventArgs) Handles MyBase.LostFocus
        checkForOutstandingUpdates()
    End Sub

    Private Sub StudyLibConfigurer_Resize(eventSender As System.Object, eventArgs As System.EventArgs) Handles MyBase.Resize
        MyBase.Width = 500
        MyBase.Height = 267
    End Sub

#End Region

#Region "IButtonControl Interface Members"

    Public Property DialogResult() As System.Windows.Forms.DialogResult Implements System.Windows.Forms.IButtonControl.DialogResult
        Get
            Return DialogResult.None
        End Get
        Set

        End Set
    End Property

    Public Sub NotifyDefault(value As Boolean) Implements System.Windows.Forms.IButtonControl.NotifyDefault

    End Sub

    Public Sub PerformClick() Implements System.Windows.Forms.IButtonControl.PerformClick
        ApplyButton.PerformClick()
    End Sub

#End Region

#Region "Control Event Handlers"

    Private Sub AddButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles AddButton.Click
        Dim newName As String
        Dim nameStub As String
        Dim i As Integer

        checkForOutstandingUpdates()
        clearSelection()

        mCurrSL = Nothing
        mCurrSLIndex = -1

        If hasBuiltIn() Then
            newName = NewStudyLibraryName
            nameStub = NewStudyLibraryName
        Else
            newName = BuiltInStudyLibraryName
            nameStub = BuiltInStudyLibraryName
        End If

        Do While invalidName(newName)
            i = i + 1
            newName = nameStub & CInt(i)
        Loop

        clearFields()
        enableFields()

        EnabledCheck.Checked = True
        NameText.Text = newName
        If InStr(1, NameText.Text, BuiltInStudyLibraryName) <> 0 Then
            BuiltInOpt.Checked = True
        Else
            CustomOpt.Checked = True
        End If
        ProgIdText.Text = ""

    End Sub

    Private Sub ApplyButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles ApplyButton.Click
        If applyProperties() Then
            If mCurrSLIndex >= 0 Then
                mNames.Remove(StudyLibList.Items.Item(mCurrSLIndex))
                mNames.Add(NameText.Text, NameText.Text)
                StudyLibList.Items.Item(mCurrSLIndex) = NameText.Text
                enableApplyButton(False)
                enableCancelButton(False)
            Else
                mNames.Add(NameText.Text, NameText.Text)
                StudyLibList.Items.Add(NameText.Text)
                enableApplyButton(False)
                enableCancelButton(False)
                StudyLibList.SetSelected(StudyLibList.Items.Count - 1, True)
            End If
        End If
    End Sub

    Private Sub BuiltInOpt_CheckedChanged(eventSender As System.Object, eventArgs As System.EventArgs)
        If eventSender.Checked Then
            ProgIdText.Enabled = False
            ProgIdText.BackColor = System.Drawing.SystemColors.Control
            If mNoCheck Then Exit Sub
            enableApplyButton(isValidFields)
            enableCancelButton(True)
        End If
    End Sub

    Private Sub CancelButton_Renamed_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles CancelButton_Renamed.Click
        Dim Index As Integer
        Index = mCurrSLIndex
        enableApplyButton(False)
        enableCancelButton(False)
        clearFields()
        mCurrSL = Nothing
        mCurrSLIndex = -1
        StudyLibList.SetSelected(Index, False)
        StudyLibList.SetSelected(Index, True)
    End Sub

    Private Sub CustomOpt_CheckedChanged(eventSender As System.Object, eventArgs As System.EventArgs)
        If eventSender.Checked Then
            ProgIdText.Enabled = True
            ProgIdText.BackColor = System.Drawing.SystemColors.Window
            If mNoCheck Then Exit Sub
            enableApplyButton(isValidFields)
            enableCancelButton(True)
        End If
    End Sub

    Private Sub DownButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles DownButton.Click
        Dim s As String
        Dim i As Integer
        Dim thisSL As ConfigurationSection = Nothing

        For i = StudyLibList.Items.Count - 2 To 0 Step -1
            If StudyLibList.GetSelected(i) And Not StudyLibList.GetSelected(i + 1) Then

                thisSL = findSL(StudyLibList.Items.Item(i))
                If thisSL.MoveDown Then
                    s = StudyLibList.Items.Item(i)
                    StudyLibList.Items.Remove(i)
                    StudyLibList.Items.Insert(i + 1, s)
                    If i = mCurrSLIndex Then mCurrSLIndex = mCurrSLIndex + 1
                    StudyLibList.SetSelected(i + 1, True)
                End If
            End If
        Next

        setDownButton()
    End Sub

    Private Sub EnabledCheck_CheckStateChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles EnabledCheck.CheckStateChanged
        If mNoCheck Then Exit Sub
        enableApplyButton(isValidFields)
        enableCancelButton(True)
    End Sub

    Private Sub NameText_TextChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles NameText.TextChanged
        If mNoCheck Then Exit Sub
        enableApplyButton(isValidFields)
        enableCancelButton(True)
    End Sub

    Private Sub ProgIdText_TextChanged(eventSender As System.Object, eventArgs As System.EventArgs)
        If mNoCheck Then Exit Sub
        enableApplyButton(isValidFields)
        enableCancelButton(True)
    End Sub

    Private Sub RemoveButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles RemoveButton.Click
        Dim s As String
        Dim i As Integer
        Dim sl As ConfigurationSection

        clearFields()
        disableFields()
        enableApplyButton(False)
        enableCancelButton(False)
        For i = StudyLibList.Items.Count - 1 To 0 Step -1
            If StudyLibList.GetSelected(i) Then
                s = StudyLibList.Items.Item(i)
                StudyLibList.Items.RemoveAt(i)
                mNames.Remove(s)
                sl = findSL(s)
                If Not sl Is Nothing Then
                    mCurrSLsList.RemoveConfigurationSection(ConfigNameStudyLibrary & "(" & sl.InstanceQualifier & ")")
                End If
            End If
        Next
        mCurrSL = Nothing
        mCurrSLIndex = -1

        DownButton.Enabled = False
        UpButton.Enabled = False
        RemoveButton.Enabled = False

    End Sub

    Private Sub StudyLibList_SelectedIndexChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles StudyLibList.SelectedIndexChanged
        setDownButton()
        setUpButton()
        setRemoveButton()

        If StudyLibList.SelectedItems.Count > 1 Then
            checkForOutstandingUpdates()
            clearFields()
            disableFields()
            mCurrSL = Nothing
            mCurrSLIndex = -1
            Exit Sub
        End If

        If StudyLibList.SelectedIndex = mCurrSLIndex Then Exit Sub

        checkForOutstandingUpdates()
        clearFields()
        enableFields()

        mCurrSL = Nothing
        mCurrSLIndex = -1
        mCurrSL = findSL(StudyLibList.Text)
        mCurrSLIndex = StudyLibList.SelectedIndex

        mNoCheck = True
        EnabledCheck.CheckState = IIf(mCurrSL.GetAttribute(AttributeNameStudyLibraryEnabled) = "True", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        NameText.Text = mCurrSL.InstanceQualifier
        If mCurrSL.GetAttribute(AttributeNameStudyLibraryBuiltIn) = "True" Then
            BuiltInOpt.Checked = True
            ' preserve whatever is in the config
            ProgIdText.Text = mCurrSL.GetAttribute(AttributeNameStudyLibraryProgId, "")
        Else
            CustomOpt.Checked = True
            ProgIdText.Text = mCurrSL.GetAttribute(AttributeNameStudyLibraryProgId)
        End If
        mNoCheck = False

    End Sub

    Private Sub UpButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles UpButton.Click
        Dim s As String
        Dim i As Integer
        Dim thisSL As ConfigurationSection

        For i = 1 To StudyLibList.Items.Count - 1
            If StudyLibList.GetSelected(i) And Not StudyLibList.GetSelected(i - 1) Then
                thisSL = findSL(StudyLibList.Items.Item(i))
                If thisSL.MoveUp Then
                    s = StudyLibList.Items.Item(i)
                    StudyLibList.Items.RemoveAt(i)
                    StudyLibList.Items.Insert(i - 1, s)
                    If i = mCurrSLIndex Then mCurrSLIndex = mCurrSLIndex - 1
                    StudyLibList.SetSelected(i - 1, True)
                End If
            End If
        Next

        setUpButton()
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public ReadOnly Property dirty() As Boolean
        Get
            dirty = ApplyButton.Enabled
        End Get
    End Property

#End Region

#Region "Methods"

    Public Function ApplyChanges() As Boolean
        If applyProperties() Then
            enableApplyButton(False)
            enableCancelButton(False)
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Initialise(configdata As ConfigurationSection, Optional isReadOnly As Boolean = False)
        mReadOnly = isReadOnly
        checkForOutstandingUpdates()
        clearFields()
        mCurrSLsList = Nothing
        mCurrSLIndex = -1
        mNames = New Collection
        loadConfig(configdata)
        If mReadOnly Then disableControls()
    End Sub

#End Region

#Region "Helper Functions"

    Private Function applyProperties() As Boolean
        If mCurrSL Is Nothing Then

            If mCurrSLsList Is Nothing Then
                mCurrSLsList = mConfig.AddConfigurationSection(ConfigNameStudyLibraries, , StudyLibrariesRenderer)
            End If

            mCurrSL = mCurrSLsList.AddConfigurationSection(ConfigNameStudyLibrary & "(" & NameText.Text & ")")
        End If

        If mCurrSL.InstanceQualifier <> NameText.Text Then
            mCurrSL.InstanceQualifier = NameText.Text
        End If
        mCurrSL.SetAttribute(AttributeNameStudyLibraryEnabled, IIf(EnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked, "True", "False"))
        If BuiltInOpt.Checked Then
            mCurrSL.SetAttribute(AttributeNameStudyLibraryBuiltIn, "True")
            If ProgIdText.Text <> "" Then mCurrSL.SetAttribute(AttributeNameStudyLibraryProgId, ProgIdText.Text)
        Else
            mCurrSL.SetAttribute(AttributeNameStudyLibraryBuiltIn, "False")
            mCurrSL.SetAttribute(AttributeNameStudyLibraryProgId, ProgIdText.Text)
        End If

        Return True
    End Function

    Private Sub checkForOutstandingUpdates()
        If ApplyButton.Enabled Then
            If MsgBox("Do you want to apply the changes you have made?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
                applyProperties()
            End If
            enableApplyButton(False)
            enableCancelButton(False)
        End If
    End Sub

    Private Sub clearFields()
        mNoCheck = True
        EnabledCheck.Checked = False
        NameText.Text = ""
        ProgIdText.Text = ""
        mNoCheck = False
    End Sub

    Private Sub clearSelection()
        For i As Integer = 0 To StudyLibList.Items.Count - 1
            StudyLibList.SetSelected(i, False)
        Next
    End Sub

    Private Sub disableControls()
        AddButton.Enabled = False
        UpButton.Enabled = False
        DownButton.Enabled = False
        RemoveButton.Enabled = False
        CancelButton_Renamed.Enabled = False
        ApplyButton.Enabled = False
    End Sub


    Private Sub disableFields()
        EnabledCheck.Enabled = False
        NameText.Enabled = False
        BuiltInOpt.Enabled = False
        CustomOpt.Enabled = False
        ProgIdText.Enabled = False
    End Sub

    Private Sub enableApplyButton(enable As Boolean)
        If mReadOnly Then Exit Sub
        If enable Then
            ApplyButton.Enabled = True
        Else
            ApplyButton.Enabled = False
        End If
    End Sub

    Private Sub enableCancelButton(enable As Boolean)
        If mReadOnly Then Exit Sub
        If enable Then
            CancelButton_Renamed.Enabled = True
        Else
            CancelButton_Renamed.Enabled = False
        End If
    End Sub

    Private Sub enableFields()
        EnabledCheck.Enabled = True
        NameText.Enabled = True
        BuiltInOpt.Enabled = True
        CustomOpt.Enabled = True
        ProgIdText.Enabled = True
    End Sub

    Private Function findSL(name As String) As ConfigurationSection
        If mCurrSLsList Is Nothing Then Return Nothing
        Return mCurrSLsList.GetConfigurationSection(ConfigNameStudyLibrary & "(" & name & ")")
    End Function

    Private Function hasBuiltIn() As Boolean
        If mCurrSLsList Is Nothing Then Return False
        For Each sl As ConfigurationSection In mCurrSLsList
            If sl.GetAttribute(AttributeNameStudyLibraryBuiltIn) = "True" Then
                Return True
            End If
        Next
    End Function

    Private Function invalidName(name As String) As Boolean
        If name = "" Then Return False

        If Not mNames.Contains(name) Then Return False

        If StudyLibList.Items.Count = 0 Then
            Return True
        ElseIf name = StudyLibList.Items.Item(mCurrSLIndex) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function isValidFields() As Boolean
        On Error Resume Next
        If invalidName(NameText.Text) Then
        ElseIf Not CustomOpt.Checked Then
            Return True
        ElseIf ProgIdText.Text = "" Then
        ElseIf InStr(1, ProgIdText.Text, ".") < 2 Then
        ElseIf InStr(1, ProgIdText.Text, ".") = Len(ProgIdText.Text) Then
        ElseIf Len(ProgIdText.Text) > 39 Then
        Else
            Return True
        End If
        Return False
    End Function

    Private Sub loadConfig(configdata As ConfigurationSection)

        mConfig = configdata

        mCurrSLsList = mConfig.GetConfigurationSection(ConfigNameStudyLibraries)

        StudyLibList.Items.Clear()

        If Not mCurrSLsList Is Nothing Then
            For Each sl As ConfigurationSection In mCurrSLsList
                Dim slname = sl.InstanceQualifier
                StudyLibList.Items.Add(slname)
                mNames.Add(slname, slname)
            Next

            StudyLibList.SelectedIndex = -1
            If StudyLibList.Items.Count > 0 Then
                StudyLibList.SetSelected(0, True)
            End If
        End If
    End Sub

    Private Sub setDownButton()
        For i As Integer = 0 To StudyLibList.Items.Count - 2
            If StudyLibList.GetSelected(i) And Not StudyLibList.GetSelected(i + 1) Then
                If Not mReadOnly Then DownButton.Enabled = True
                Exit Sub
            End If
        Next
        DownButton.Enabled = False
    End Sub

    Private Sub setRemoveButton()
        If StudyLibList.SelectedItems.Count <> 0 Then
            If Not mReadOnly Then RemoveButton.Enabled = True
        Else
            RemoveButton.Enabled = False
        End If
    End Sub

    Private Sub setUpButton()
        For i As Integer = 1 To StudyLibList.Items.Count - 1
            If StudyLibList.GetSelected(i) And Not StudyLibList.GetSelected(i - 1) Then
                If Not mReadOnly Then UpButton.Enabled = True
                Exit Sub
            End If
        Next
        UpButton.Enabled = False
    End Sub

#End Region

End Class