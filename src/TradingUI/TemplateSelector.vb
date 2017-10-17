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

Imports System.ComponentModel

Public Class TemplateSelector
    Implements INotifyPropertyChanged

    Private mTemplateManager As TemplateManager
    Private mTemplateableObject As ISupportsTemplates
    Private mIsReady As Boolean
    Private mIsExistingTemplateSelected As Boolean
    Private mAllowNew As Boolean

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub Initialise(templateManager As TemplateManager, templateableObject As ISupportsTemplates, allowNew As Boolean)
        mTemplateManager = templateManager
        mTemplateableObject = templateableObject

        TemplateListBox.DataSource = mTemplateManager.Templates

        mAllowNew = allowNew
        If mAllowNew Then
            NameTextBox.Enabled = True
            NotesTextBox.Enabled = True
            enableCheckboxes()
        Else
            NameTextBox.Enabled = False
            NotesTextBox.Enabled = False
            AllCheckBox.Enabled = False
            disableAndClearCheckboxes()
        End If
    End Sub

    Protected Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub

    Private Sub AllCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AllCheckBox.CheckedChanged
        If AllCheckBox.Checked Then
            disableAndClearCheckboxes()
        Else
            enableCheckboxes()
        End If
    End Sub

    Private Sub NameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles NameTextBox.KeyDown
        If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
        ElseIf e.KeyValue < &H20 Or e.KeyValue >= &H80 Then
            e.Handled = True
        End If
    End Sub

    Private Sub NameTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NameTextBox.KeyPress
        If e.KeyChar = "<" Or e.KeyChar = ">" Or e.KeyChar = "&" Or e.KeyChar = """" Then
            e.Handled = True
        End If
    End Sub

    Private Sub NameTextBox_TextChanged(sender As Object, e As EventArgs) Handles NameTextBox.TextChanged
        checkIfExistingTemplateSelected()
        'IsExistingTemplateSelected = TemplateListBox.SelectedIndex <> -1 AndAlso CType(TemplateListBox.SelectedItem, Template).Name = NameTextBox.Text
        IsReady = (NameTextBox.Text <> "")
    End Sub

    Private Sub TemplateListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TemplateListBox.SelectedIndexChanged
        If TemplateListBox.SelectedIndex <> -1 Then
            NameTextBox.Text = CType(TemplateListBox.SelectedItem, Template).name
            setSecurityTypeCheckboxes(CType(TemplateListBox.SelectedItem, Template).IsDefaultForSecTypes)
            If mAllowNew Then enableCheckboxes()
            NotesTextBox.Text = CType(TemplateListBox.SelectedItem, Template).Notes
            IsExistingTemplateSelected = True
            IsReady = True
        Else
            clearCheckboxes()
            NotesTextBox.Text = ""
            IsExistingTemplateSelected = False
            IsReady = False
        End If
    End Sub

    Public Property IsReady As Boolean
        Get
            Return mIsReady
        End Get
        Private Set
            If value <> mIsReady Then
                mIsReady = value
                OnPropertyChanged(New PropertyChangedEventArgs("IsReady"))
            End If
        End Set
    End Property

    Public Property IsExistingTemplateSelected() As Boolean
        Get
            Return mIsExistingTemplateSelected
        End Get
        Private Set
            If value <> mIsExistingTemplateSelected Then
                mIsExistingTemplateSelected = value
                OnPropertyChanged(New PropertyChangedEventArgs("IsExistingTemplateSelected"))
            End If
        End Set
    End Property

    Public ReadOnly Property Notes As String
        Get
            Return NotesTextBox.Text
        End Get
    End Property

    Public ReadOnly Property SelectedTemplate() As Template
        Get
            If Not IsExistingTemplateSelected() Then Throw New InvalidOperationException("No selected template")
            Return CType(TemplateListBox.SelectedItem, Template)
        End Get
    End Property

    Friend ReadOnly Property SecurityTypeFlags() As TemplateManager.SecurityTypeFlags
        Get
            Dim flags As TemplateManager.SecurityTypeFlags
            If AllCheckBox.Checked Then
                flags = TemplateManager.SecurityTypeFlags.All
            Else
                If CashCheckBox.Checked Then flags = TemplateManager.SecurityTypeFlags.Cash
                If ComboCheckBox.Checked Then flags = flags Or TemplateManager.SecurityTypeFlags.Combo
                If FuturesCheckBox.Checked Then flags = flags Or TemplateManager.SecurityTypeFlags.Future
                If FuturesOptionsCheckBox.Checked Then flags = flags Or TemplateManager.SecurityTypeFlags.FuturesOption
                If IndexesCheckBox.Checked Then flags = flags Or TemplateManager.SecurityTypeFlags.Index
                If OptionsCheckBox.Checked Then flags = flags Or TemplateManager.SecurityTypeFlags.Option
                If StocksCheckBox.Checked Then flags = flags Or TemplateManager.SecurityTypeFlags.Stock
            End If
            Return flags
        End Get
    End Property

    Public ReadOnly Property TemplateName As String
        Get
            Return NameTextBox.Text
        End Get
    End Property

    Private Sub checkIfExistingTemplateSelected()
        Dim found = False
        For Each item As Template In TemplateListBox.Items
            If String.Compare(item.Name, NameTextBox.Text, ignoreCase:=True) = 0 Then
                TemplateListBox.SelectedItem = item
                found = True
                Exit For
            End If
        Next
        If Not found Then TemplateListBox.SelectedIndex = -1
    End Sub

    Private Sub clearCheckbox(checkbox As CheckBox)
        checkbox.Checked = False
    End Sub

    Private Sub clearCheckboxes()
        clearCheckbox(AllCheckBox)
        clearCheckbox(CashCheckBox)
        clearCheckbox(ComboCheckBox)
        clearCheckbox(FuturesCheckBox)
        clearCheckbox(FuturesOptionsCheckBox)
        clearCheckbox(IndexesCheckBox)
        clearCheckbox(OptionsCheckBox)
        clearCheckbox(StocksCheckBox)
    End Sub

    Private Sub disableAndClearCheckbox(checkbox As CheckBox)
        clearCheckbox(checkbox)
        checkbox.Enabled = False
    End Sub

    Private Sub disableAndClearCheckboxes()
        disableAndClearCheckbox(CashCheckBox)
        disableAndClearCheckbox(ComboCheckBox)
        disableAndClearCheckbox(FuturesCheckBox)
        disableAndClearCheckbox(FuturesOptionsCheckBox)
        disableAndClearCheckbox(IndexesCheckBox)
        disableAndClearCheckbox(OptionsCheckBox)
        disableAndClearCheckbox(StocksCheckBox)
    End Sub

    Private Sub enableCheckboxes()
        AllCheckBox.Enabled = True
        CashCheckBox.Enabled = True
        ComboCheckBox.Enabled = True
        FuturesCheckBox.Enabled = True
        FuturesOptionsCheckBox.Enabled = True
        IndexesCheckBox.Enabled = True
        OptionsCheckBox.Enabled = True
        StocksCheckBox.Enabled = True
    End Sub

    Private Function isSecTypeSet(value As TemplateManager.SecurityTypeFlags, flag As TemplateManager.SecurityTypeFlags) As Boolean
        Return (value And flag) = flag
    End Function

    Private Sub setSecurityTypeCheckbox(checkBox As CheckBox, value As Boolean)
        checkBox.Checked = value
    End Sub

    Private Sub setSecurityTypeCheckboxes(value As TemplateManager.SecurityTypeFlags)
        If isSecTypeSet(value, TemplateManager.SecurityTypeFlags.All) Then
            disableAndClearCheckboxes()
            AllCheckBox.Checked = True
            AllCheckBox.Enabled = True
        Else
            CashCheckBox.Checked = isSecTypeSet(value, TemplateManager.SecurityTypeFlags.Cash)
            ComboCheckBox.Checked = isSecTypeSet(value, TemplateManager.SecurityTypeFlags.Combo)
            FuturesCheckBox.Checked = isSecTypeSet(value, TemplateManager.SecurityTypeFlags.Future)
            FuturesOptionsCheckBox.Checked = isSecTypeSet(value, TemplateManager.SecurityTypeFlags.FuturesOption)
            IndexesCheckBox.Checked = isSecTypeSet(value, TemplateManager.SecurityTypeFlags.Index)
            OptionsCheckBox.Checked = isSecTypeSet(value, TemplateManager.SecurityTypeFlags.Option)
            StocksCheckBox.Checked = isSecTypeSet(value, TemplateManager.SecurityTypeFlags.Stock)
        End If
    End Sub

End Class
