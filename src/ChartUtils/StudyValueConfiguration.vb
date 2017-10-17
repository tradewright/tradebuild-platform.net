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
Imports System.Type
Imports BarUtils27
Imports TWUtilities40

Public Class StudyValueConfiguration

#Region "Description"
    
    '
#End Region

#Region "Amendment history"
    
    '
    '
    '

#End Region

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const ConfigSectionBarStyle As String = "BarStyle"
    Private Const ConfigSectionDataPointStyle As String = "DataPointStyle"
    Private Const ConfigSectionLineStyle As String = "LineStyle"
    Private Const ConfigSectionTextStyle As String = "TextStyle"

    Private Const ConfigSettingBarFormatterFactoryName As String = "&BarFormatterFactoryName"
    Private Const ConfigSettingBarFormatterLibraryName As String = "&BarFormatterLibraryName"
    Private Const ConfigSettingChartRegionName As String = "&ChartRegionName"
    Private Const ConfigSettingIncludeInChart As String = "&IncludeInChart"
    Private Const ConfigSettingLayer As String = "&Layer"
    Private Const ConfigSettingOffsetX As String = "&X"
    Private Const ConfigSettingOffsetY As String = "&Y"
    Private Const ConfigSettingTypeName As String = "&TypeName"
    Private Const ConfigSettingValueName As String = "&ValueName"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mIncludeInChart As Boolean
    Private mValueName As String

    Private mBarStyle As BarStyle
    Private mDataPointStyle As DataPointStyle
    Private mLineStyle As LineStyle
    Private mTextStyle As TextStyle

    Private mChartRegionName As String
    Private mLayer As Integer
    Private mOffsetX As Double
    Private mOffsetY As Double

    Private mBarFormatterFactoryName As String
    Private mBarFormatterLibraryName As String

    Private mConfig As ConfigurationSection

#End Region

#Region "Constructors"

    Friend Sub New()
        MyBase.New()

        'mLayer = LayerNumbers.LayerHighestUser
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public Property BarFormatterFactoryName() As String
        Get
            Return mBarFormatterFactoryName
        End Get
        Set
            mBarFormatterFactoryName = Value
        End Set
    End Property

    Public Property BarFormatterLibraryName() As String
        Get
            Return mBarFormatterLibraryName
        End Get
        Set
            mBarFormatterLibraryName = Value
        End Set
    End Property

    Public Property BarStyle() As BarStyle
        Get
            Return mBarStyle
        End Get
        Set
            mBarStyle = Value
            storeSettings()
        End Set
    End Property

    Public Property ChartRegionName() As String
        Get
            Return mChartRegionName
        End Get
        Set
            mChartRegionName = Value
            storeSettings()
        End Set
    End Property

    Friend WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If Value Is mConfig Then Exit Property
            If mConfig IsNot Nothing Then mConfig.Remove()
            mConfig = Value
            If mConfig Is Nothing Then Return
            storeSettings()
        End Set
    End Property

    Public Property DataPointStyle() As DataPointStyle
        Get
            Return mDataPointStyle
        End Get
        Set
            mDataPointStyle = Value
            storeSettings()
        End Set
    End Property

    Public Property IncludeInChart() As Boolean
        Get
            Return mIncludeInChart
        End Get
        Set
            mIncludeInChart = Value
            storeSettings()
        End Set
    End Property

    Public Property Layer() As Integer
        Get
            Return mLayer
        End Get
        Set
            mLayer = Value
            storeSettings()
        End Set
    End Property

    Public Property LineStyle() As LineStyle
        Get
            Return mLineStyle
        End Get
        Set
            mLineStyle = Value
            storeSettings()
        End Set
    End Property

    Public Property OffsetX() As Double
        Get
            Return mOffsetX
        End Get
        Set
            mOffsetX = Value
            storeSettings()
        End Set
    End Property

    Public Property OffsetY() As Double
        Get
            Return mOffsetY
        End Get
        Set
            mOffsetY = Value
            storeSettings()
        End Set
    End Property

    Public Property TextStyle() As TextStyle
        Get
            Return mTextStyle
        End Get
        Set
            mTextStyle = Value
            storeSettings()
        End Set
    End Property

    Public Property ValueName() As String
        Get
            Return mValueName
        End Get
        Set
            mValueName = Value
            storeSettings()
        End Set
    End Property

#End Region

#Region "Methods"

    Friend Sub RemoveFromConfig()
        If Not mConfig Is Nothing Then mConfig.Remove()
    End Sub

    Friend Sub LoadFromConfig( _
                    config As ConfigurationSection)
        mConfig = config

        Dim cs = mConfig.GetConfigurationSection(ConfigSectionBarStyle)
        If Not cs Is Nothing Then
            mBarStyle = New BarStyle
            mBarStyle.LoadFromConfig(cs)
        End If

        cs = mConfig.GetConfigurationSection(ConfigSectionDataPointStyle)
        If Not cs Is Nothing Then
            mDataPointStyle = New DataPointStyle
            mDataPointStyle.LoadFromConfig(cs)
        End If

        cs = mConfig.GetConfigurationSection(ConfigSectionLineStyle)
        If Not cs Is Nothing Then
            mLineStyle = New LineStyle
            mLineStyle.LoadFromConfig(cs)
        End If

        cs = mConfig.GetConfigurationSection(ConfigSectionTextStyle)
        If Not cs Is Nothing Then
            mTextStyle = New TextStyle
            mTextStyle.LoadFromConfig(cs)
        End If

        mBarFormatterFactoryName = mConfig.GetSetting(ConfigSettingBarFormatterFactoryName, "")
        mBarFormatterLibraryName = mConfig.GetSetting(ConfigSettingBarFormatterLibraryName, "")

        mIncludeInChart = CType(mConfig.GetSetting(ConfigSettingIncludeInChart), Boolean)
        mValueName = mConfig.GetSetting(ConfigSettingValueName)
        mChartRegionName = mConfig.GetSetting(ConfigSettingChartRegionName)
        mLayer = CType(mConfig.GetSetting(ConfigSettingLayer), Integer)
        mOffsetX = CType(mConfig.GetSetting(ConfigSettingOffsetX), Double)
        mOffsetY = CType(mConfig.GetSetting(ConfigSettingOffsetY), Double)
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub storeSettings()
        If mConfig Is Nothing Then Exit Sub

        If Not mBarStyle Is Nothing Then mBarStyle.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionBarStyle)
        If Not mDataPointStyle Is Nothing Then mDataPointStyle.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionDataPointStyle)
        If Not mLineStyle Is Nothing Then mLineStyle.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionLineStyle)
        If Not mTextStyle Is Nothing Then mTextStyle.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionTextStyle)

        mConfig.SetSetting(ConfigSettingBarFormatterFactoryName, mBarFormatterFactoryName)
        mConfig.SetSetting(ConfigSettingBarFormatterLibraryName, mBarFormatterLibraryName)
        mConfig.SetSetting(ConfigSettingIncludeInChart, CType(mIncludeInChart, String))
        mConfig.SetSetting(ConfigSettingValueName, CType(mValueName, String))
        mConfig.SetSetting(ConfigSettingChartRegionName, CType(mChartRegionName, String))
        mConfig.SetSetting(ConfigSettingLayer, CType(mLayer, String))
        mConfig.SetSetting(ConfigSettingOffsetX, CType(mOffsetX, String))
        mConfig.SetSetting(ConfigSettingOffsetY, CType(mOffsetY, String))
    End Sub

#End Region

End Class
