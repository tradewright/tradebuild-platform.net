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


Imports TWUtilities40

Public Class BarFormatterLibConfigurer
    Inherits System.Windows.Forms.UserControl

    Implements System.Windows.Forms.IButtonControl

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const NewBarFormatterLibraryName As String = "New BarFormatter library"

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

    Private Sub BarFormatterLibConfigurer_LostFocus(eventSender As System.Object, eventArgs As System.EventArgs) Handles MyBase.LostFocus
        checkForOutstandingUpdates()
    End Sub

    Private Sub BarFormatterLibConfigurer_Resize(eventSender As System.Object, eventArgs As System.EventArgs) Handles MyBase.Resize
        MyBase.Width = 500
        MyBase.Height = 267
    End Sub

#End Region

#Region "IButtonControl Interface Members"

    Public Property DialogResult() As System.Windows.Forms.DialogResult Implements System.Windows.Forms.IButtonControl.DialogResult
        Get
            Return System.Windows.Forms.DialogResult.None
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
            newName = NewBarFormatterLibraryName
            nameStub = NewBarFormatterLibraryName
        Else
            newName = BarFormatters.BuiltInBarFormatterLibraryName
            nameStub = BarFormatters.BuiltInBarFormatterLibraryName
        End If

        Do While invalidName(newName)
            i = i + 1
            newName = nameStub & CInt(i)
        Loop

        clearFields()
        enableFields()

        EnabledCheck.Checked = True
        NameText.Text = newName
        If InStr(1, NameText.Text, BarFormatters.BuiltInBarFormatterLibraryName) <> 0 Then
            BuiltInOpt.Checked = True
        Else
            CustomOpt.Checked = True
        End If
        ClassNameText.Text = ""

    End Sub

    Private Sub ApplyButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles ApplyButton.Click
        If applyProperties() Then
            If mCurrSLIndex >= 0 Then
                mNames.Remove(BarFormatterLibList.Items.Item(mCurrSLIndex))
                mNames.Add(NameText.Text, NameText.Text)
                BarFormatterLibList.Items.Item(mCurrSLIndex) = NameText.Text
                enableApplyButton(False)
                enableCancelButton(False)
            Else
                mNames.Add(NameText.Text, NameText.Text)
                BarFormatterLibList.Items.Add(NameText.Text)
                enableApplyButton(False)
                enableCancelButton(False)
                BarFormatterLibList.SetSelected(BarFormatterLibList.Items.Count - 1, True)
            End If
        End If
    End Sub

    Private Sub BuiltInOpt_CheckedChanged(eventSender As System.Object, eventArgs As System.EventArgs)
        If eventSender.Checked Then
            ClassNameText.Enabled = False
            ClassNameText.BackColor = System.Drawing.SystemColors.Control
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
        BarFormatterLibList.SetSelected(Index, False)
        BarFormatterLibList.SetSelected(Index, True)
    End Sub

    Private Sub CustomOpt_CheckedChanged(eventSender As System.Object, eventArgs As System.EventArgs)
        If eventSender.Checked Then
            ClassNameText.Enabled = True
            ClassNameText.BackColor = System.Drawing.SystemColors.Window
            If mNoCheck Then Exit Sub
            enableApplyButton(isValidFields)
            enableCancelButton(True)
        End If
    End Sub

    Private Sub DownButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles DownButton.Click
        Dim s As String
        Dim i As Integer
        Dim thisSL As ConfigurationSection = Nothing

        For i = BarFormatterLibList.Items.Count - 2 To 0 Step -1
            If BarFormatterLibList.GetSelected(i) And Not BarFormatterLibList.GetSelected(i + 1) Then

                thisSL = findSL(BarFormatterLibList.Items.Item(i))
                If thisSL.MoveDown Then
                    s = BarFormatterLibList.Items.Item(i)
                    BarFormatterLibList.Items.Remove(i)
                    BarFormatterLibList.Items.Insert(i + 1, s)
                    If i = mCurrSLIndex Then mCurrSLIndex = mCurrSLIndex + 1
                    BarFormatterLibList.SetSelected(i + 1, True)
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
        For i = BarFormatterLibList.Items.Count - 1 To 0 Step -1
            If BarFormatterLibList.GetSelected(i) Then
                s = BarFormatterLibList.Items.Item(i)
                BarFormatterLibList.Items.RemoveAt(i)
                mNames.Remove(s)
                sl = findSL(s)
                If Not sl Is Nothing Then
                    mCurrSLsList.RemoveConfigurationSection(BarFormatters.ConfigSectionBarFormatterLibrary & "(" & sl.InstanceQualifier & ")")
                End If
            End If
        Next
        mCurrSL = Nothing
        mCurrSLIndex = -1

        DownButton.Enabled = False
        UpButton.Enabled = False
        RemoveButton.Enabled = False

    End Sub

    Private Sub BarFormatterLibList_SelectedIndexChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles BarFormatterLibList.SelectedIndexChanged
        setDownButton()
        setUpButton()
        setRemoveButton()

        If BarFormatterLibList.SelectedItems.Count > 1 Then
            checkForOutstandingUpdates()
            clearFields()
            disableFields()
            mCurrSL = Nothing
            mCurrSLIndex = -1
            Exit Sub
        End If

        If BarFormatterLibList.SelectedIndex = mCurrSLIndex Then Exit Sub

        checkForOutstandingUpdates()
        clearFields()
        enableFields()

        mCurrSL = Nothing
        mCurrSLIndex = -1
        mCurrSL = findSL(BarFormatterLibList.Text)
        mCurrSLIndex = BarFormatterLibList.SelectedIndex

        mNoCheck = True
        EnabledCheck.CheckState = IIf(mCurrSL.GetAttribute(BarFormatters.AttributeNameEnabled) = "True", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        NameText.Text = mCurrSL.InstanceQualifier
        If mCurrSL.GetAttribute(BarFormatters.AttributeNameBarFormatterLibraryBuiltIn) = "True" Then
            BuiltInOpt.Checked = True
            ' preserve whatever is in the config
            ClassNameText.Text = mCurrSL.GetAttribute(BarFormatters.AttributeNameBarFormatterLibraryTypeName, "")
        Else
            CustomOpt.Checked = True
            ClassNameText.Text = mCurrSL.GetAttribute(BarFormatters.AttributeNameBarFormatterLibraryTypeName)
        End If
        mNoCheck = False

    End Sub

    Private Sub UpButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles UpButton.Click
        Dim s As String
        Dim i As Integer
        Dim thisSL As ConfigurationSection

        For i = 1 To BarFormatterLibList.Items.Count - 1
            If BarFormatterLibList.GetSelected(i) And Not BarFormatterLibList.GetSelected(i - 1) Then
                thisSL = findSL(BarFormatterLibList.Items.Item(i))
                If thisSL.MoveUp Then
                    s = BarFormatterLibList.Items.Item(i)
                    BarFormatterLibList.Items.RemoveAt(i)
                    BarFormatterLibList.Items.Insert(i - 1, s)
                    If i = mCurrSLIndex Then mCurrSLIndex = mCurrSLIndex - 1
                    BarFormatterLibList.SetSelected(i - 1, True)
                End If
            End If
        Next

        setUpButton()
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public ReadOnly Property Dirty() As Boolean
        Get
            Dirty = ApplyButton.Enabled
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
                mCurrSLsList = mConfig.AddConfigurationSection(BarFormatters.ConfigSectionBarFormatterLibraries, , BarFormatters.BarFormatterLibrariesRenderer)
            End If

            mCurrSL = mCurrSLsList.AddConfigurationSection(BarFormatters.ConfigSectionBarFormatterLibrary & "(" & NameText.Text & ")")
        End If

        If mCurrSL.InstanceQualifier <> NameText.Text Then
            mCurrSL.InstanceQualifier = NameText.Text
        End If
        mCurrSL.SetAttribute(BarFormatters.AttributeNameEnabled, IIf(EnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked, "True", "False"))
        If BuiltInOpt.Checked Then
            mCurrSL.SetAttribute(BarFormatters.AttributeNameBarFormatterLibraryBuiltIn, "True")
            If ClassNameText.Text <> "" Then mCurrSL.SetAttribute(BarFormatters.AttributeNameBarFormatterLibraryTypeName, ClassNameText.Text)
        Else
            mCurrSL.SetAttribute(BarFormatters.AttributeNameBarFormatterLibraryBuiltIn, "False")
            mCurrSL.SetAttribute(BarFormatters.AttributeNameBarFormatterLibraryTypeName, ClassNameText.Text)
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
        ClassNameText.Text = ""
        mNoCheck = False
    End Sub

    Private Sub clearSelection()
        For i As Integer = 0 To BarFormatterLibList.Items.Count - 1
            BarFormatterLibList.SetSelected(i, False)
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
        ClassNameText.Enabled = False
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
        ClassNameText.Enabled = True
    End Sub

    Private Function findSL(name As String) As ConfigurationSection
        If mCurrSLsList Is Nothing Then Return Nothing
        Return mCurrSLsList.GetConfigurationSection(BarFormatters.ConfigSectionBarFormatterLibrary & "(" & name & ")")
    End Function

    Private Function hasBuiltIn() As Boolean
        If mCurrSLsList Is Nothing Then Return False
        For Each sl As ConfigurationSection In mCurrSLsList
            If sl.GetAttribute(BarFormatters.AttributeNameBarFormatterLibraryBuiltIn) = "True" Then
                Return True
            End If
        Next
    End Function

    Private Function invalidName(name As String) As Boolean
        If name = "" Then Return False

        If Not mNames.Contains(name) Then Return False

        If BarFormatterLibList.Items.Count = 0 Then
            Return True
        ElseIf name = BarFormatterLibList.Items.Item(mCurrSLIndex) Then
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
        ElseIf ClassNameText.Text = "" Then
        ElseIf InStr(1, ClassNameText.Text, ".") < 2 Then
        ElseIf InStr(1, ClassNameText.Text, ".") = Len(ClassNameText.Text) Then
        ElseIf Len(ClassNameText.Text) > 39 Then
        Else
            Return True
        End If
        Return False
    End Function

    Private Sub loadConfig(configdata As ConfigurationSection)

        mConfig = configdata

        mCurrSLsList = mConfig.GetConfigurationSection(BarFormatters.ConfigSectionBarFormatterLibraries)

        BarFormatterLibList.Items.Clear()

        If Not mCurrSLsList Is Nothing Then
            For Each cs As ConfigurationSection In mCurrSLsList
                Dim slname = cs.InstanceQualifier
                BarFormatterLibList.Items.Add(slname)
                mNames.Add(slname, slname)
            Next

            BarFormatterLibList.SelectedIndex = -1
            If BarFormatterLibList.Items.Count > 0 Then
                BarFormatterLibList.SetSelected(0, True)
            End If
        End If
    End Sub

    Private Sub setDownButton()
        For i As Integer = 0 To BarFormatterLibList.Items.Count - 2
            If BarFormatterLibList.GetSelected(i) And Not BarFormatterLibList.GetSelected(i + 1) Then
                If Not mReadOnly Then DownButton.Enabled = True
                Exit Sub
            End If
        Next
        DownButton.Enabled = False
    End Sub

    Private Sub setRemoveButton()
        If BarFormatterLibList.SelectedItems.Count <> 0 Then
            If Not mReadOnly Then RemoveButton.Enabled = True
        Else
            RemoveButton.Enabled = False
        End If
    End Sub

    Private Sub setUpButton()
        For i As Integer = 1 To BarFormatterLibList.Items.Count - 1
            If BarFormatterLibList.GetSelected(i) And Not BarFormatterLibList.GetSelected(i - 1) Then
                If Not mReadOnly Then UpButton.Enabled = True
                Exit Sub
            End If
        Next
        UpButton.Enabled = False
    End Sub

#End Region

End Class