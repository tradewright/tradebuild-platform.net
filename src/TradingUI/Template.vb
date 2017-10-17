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

Public Class Template
    Private mConfig As ConfigurationSection
    Private mDefaults As TemplateManager.SecurityTypeFlags
    Private mNotes As String
    Private mType As String

    Private Sub New()
    End Sub

    ''' <summary>
    ''' Creates a new Template object with all its data loaded from the supplied configuration section.
    ''' </summary>
    ''' <param name="config">The configuration section containing the template's data.</param>
    ''' <remarks>This constructor should only be used when the configuration section contains all the template data for the new object. </remarks>
    Friend Sub New(config As ConfigurationSection)
        mConfig = config
        mDefaults = CType([Enum].Parse(GetType(TemplateManager.SecurityTypeFlags), config.GetSetting(TemplateManager.ConfigSettingDefaultForSecTypes, "None")), TemplateManager.SecurityTypeFlags)
        mNotes = config.GetSetting(TemplateManager.ConfigSettingNotes, "")
        mType = config.GetSetting(TemplateManager.ConfigSettingType, "")
    End Sub

    Friend Sub New(config As ConfigurationSection, isDefaultForSecTypes As TemplateManager.SecurityTypeFlags, notes As String)
        mConfig = config
        Me.IsDefaultForSecTypes = isDefaultForSecTypes
        Me.Notes = notes
    End Sub

    Public ReadOnly Property Config As ConfigurationSection
        Get
            Return mConfig
        End Get
    End Property

    Public WriteOnly Property Data As ConfigurationSection
        Set
            mConfig.CloneConfigSection(value)
            mType = value.Name
            mConfig.SetSetting(TemplateManager.ConfigSettingType, mType)
        End Set
    End Property

    Friend Property IsDefaultForSecTypes() As TemplateManager.SecurityTypeFlags
        Get
            Return mDefaults
        End Get
        Set
            mDefaults = value
            mConfig.SetSetting(TemplateManager.ConfigSettingDefaultForSecTypes, mDefaults.ToString())
        End Set
    End Property

    Public ReadOnly Property Name As String
        Get
            Return ToString()
        End Get
    End Property

    Public Property Notes As String
        Get
            Return mNotes
        End Get
        Friend Set
            mNotes = value
            mConfig.SetSetting(TemplateManager.ConfigSettingNotes, value)
        End Set
    End Property

    Public Function LoadData(currentData As ConfigurationSection) As ConfigurationSection
        Dim data = mConfig.GetConfigurationSection(mType)
        Dim parentConfig = currentData.Parent
        currentData.Remove()
        parentConfig.CloneConfigSection(data)
        Return parentConfig.GetConfigurationSection(mType)
    End Function

    Public Overrides Function ToString() As String
        Return Config.InstanceQualifier ' & CStr(IIf(IsDefault, "[Default]", ""))
    End Function

End Class
