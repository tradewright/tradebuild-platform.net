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

Imports TWUtilities40

Imports TradeWright.Trading.Utils.Contracts

Public Class TemplateManager

    Private Const ConfigSectionTemplate As String = "Template"

    Public Const ConfigSettingDefaultForSecTypes As String = "&DefaultForSecTypes"
    Public Const ConfigSettingNotes As String = ".Notes"
    Public Const ConfigSettingType As String = "&Type"

    <Flags>
    Friend Enum SecurityTypeFlags
        None = 0
        Cash = 1
        Combo = 2
        Future = 4
        FuturesOption = 8
        Index = 16
        [Option] = 32
        Stock = 64
        All = Cash + Combo + Future + FuturesOption + Index + [Option] + Stock
    End Enum

    Private mTemplates As New List(Of Template)
    Private mTemplateStore As ConfigurationSection
    Private mDefaultTemplateCash As Template
    Private mDefaultTemplateCombo As Template
    Private mDefaultTemplateFuture As Template
    Private mDefaultTemplateFuturesOption As Template
    Private mDefaultTemplateIndex As Template
    Private mDefaultTemplateOption As Template
    Private mDefaultTemplateStock As Template

    Public Sub New(templateStore As ConfigurationSection)
        mTemplateStore = templateStore
        loadTemplates()
    End Sub

    Public ReadOnly Property Templates As IEnumerable(Of Template)
        Get
            Return mTemplates
        End Get
    End Property

    Public ReadOnly Property DefaultTemplateForSecType(secType As SecurityType) As Template
        Get
            Dim template = getDefaultTemplateForSecType(secType)
            If template Is Nothing Then Throw New InvalidOperationException("No default template defined for this secType")
            Return template
        End Get
    End Property

    Public ReadOnly Property IsDefaultTemplateDefinedForSecType(secType As SecurityType) As Boolean
        Get
            Return getDefaultTemplateForSecType(secType) IsNot Nothing
        End Get
    End Property

    Friend Function CreateTemplate(name As String, templateableObject As ISupportsTemplates, isDefaultForSecTypes As SecurityTypeFlags, notes As String) As Template
        Dim templateConfig = mTemplateStore.AddConfigurationSection(ConfigSectionTemplate & "(" & name & ")")
        Dim newTemplate = New Template(templateConfig, isDefaultForSecTypes, notes)
        mTemplates.Add(newTemplate)
        updateTemplate(newTemplate, templateableObject, isDefaultForSecTypes, notes)
        Return newTemplate
    End Function

    Friend Sub DeleteTemplate(template As Template)
        mTemplates.Remove(template)
        template.Config.Remove()
    End Sub

    Public Sub LoadFromUserSelectedTemplate(templateableObject As ISupportsTemplates, owner As Form)
        Dim f = New fApplyTemplate(Me, templateableObject)
        f.ShowDialog(owner)
    End Sub

    Friend Sub OverwriteTemplate(template As Template, templateableObject As ISupportsTemplates, isDefaultForSecTypes As SecurityTypeFlags, notes As String)
        template.Config.RemoveAllChildren()
        updateTemplate(template, templateableObject, isDefaultForSecTypes, notes)
    End Sub

    Public Sub SaveAsUserSelectedTemplate(templateableObject As ISupportsTemplates, owner As Form)
        Dim f = New fSaveAsTemplate(Me, templateableObject)
        f.ShowDialog(owner)
    End Sub

    Public Function TryGetDefaultTemplateForSecType(secType As SecurityType, ByRef template As Template) As Boolean
        template = getDefaultTemplateForSecType(secType)
        Return template IsNot Nothing
    End Function

    Public Function getDefaultTemplateForSecType(secType As SecurityType) As Template
        Dim template As Template
        Select Case secType
            Case SecurityType.Cash
                template = mDefaultTemplateCash
            Case SecurityType.Combo
                template = mDefaultTemplateCombo
            Case SecurityType.Future
                template = mDefaultTemplateFuture
            Case SecurityType.FuturesOption
                template = mDefaultTemplateFuturesOption
            Case SecurityType.Index
                template = mDefaultTemplateIndex
            Case SecurityType.Option
                template = mDefaultTemplateOption
            Case SecurityType.Stock
                template = mDefaultTemplateStock
            Case Else
                Throw New ArgumentException("secType")
        End Select
        Return template
    End Function

    Private Function isSecTypeFlagSet(Value As SecurityTypeFlags, flag As SecurityTypeFlags) As Boolean
        Return (Value And flag) = flag
    End Function

    Private Sub loadTemplates()
        For Each cs As ConfigurationSection In mTemplateStore
            Dim template = New Template(cs)
            mTemplates.Add(template)
            setOrClearDefaultTemplates(template)
        Next
    End Sub

    Private Sub setOrClearDefaultTemplate(template As Template, secType As SecurityTypeFlags, ByRef defaultTemplate As Template)
        If isSecTypeFlagSet(template.IsDefaultForSecTypes, secType) Then
            If defaultTemplate IsNot Nothing AndAlso defaultTemplate IsNot template Then defaultTemplate.IsDefaultForSecTypes = defaultTemplate.IsDefaultForSecTypes And (Not secType)
            defaultTemplate = template
        End If
    End Sub

    Private Sub setOrClearDefaultTemplates(template As Template)
        setOrClearDefaultTemplate(template, SecurityTypeFlags.Cash, mDefaultTemplateCash)
        setOrClearDefaultTemplate(template, SecurityTypeFlags.Combo, mDefaultTemplateCombo)
        setOrClearDefaultTemplate(template, SecurityTypeFlags.Future, mDefaultTemplateFuture)
        setOrClearDefaultTemplate(template, SecurityTypeFlags.FuturesOption, mDefaultTemplateFuturesOption)
        setOrClearDefaultTemplate(template, SecurityTypeFlags.Index, mDefaultTemplateIndex)
        setOrClearDefaultTemplate(template, SecurityTypeFlags.Option, mDefaultTemplateOption)
        setOrClearDefaultTemplate(template, SecurityTypeFlags.Stock, mDefaultTemplateStock)
    End Sub

    Private Sub updateTemplate(template As Template, templateableObject As ISupportsTemplates, isDefaultForSecTypes As SecurityTypeFlags, notes As String)
        templateableObject.SaveToTemplate(template)
        template.IsDefaultForSecTypes = isDefaultForSecTypes
        template.Notes = notes
        setOrClearDefaultTemplates(template)
    End Sub
End Class
