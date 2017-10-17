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
Imports BarUtils27
Imports TWUtilities40

Public Class StudyConfiguration


#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const ConfigSectionParameters As String = "Parameters"
    Private Const ConfigSectionStudyConfigs As String = "StudyConfigs"
    Private Const ConfigSectionStudyValueConfigurations As String = "StudyValueConfigurations"
    Private Const ConfigSectionStudyHorizontalRules As String = "StudyHorizontalRules"

    Private Const ConfigSettingName As String = ".Name"
    Private Const ConfigSettingStudyLibraryName As String = ".StudyLibraryName"
    Private Const ConfigSettingInstanceFullyQualifiedName As String = ".InstanceFullyQualifiedName"
    Private Const ConfigSettingInputValueNames As String = ".InputValueNames"
    Private Const ConfigSettingChartRegionName As String = ".ChartRegionName"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mStudyConfigs As StudyConfigurations

    Private mParameters As Parameters
    Private mStudyValueConfigurations As StudyValueConfigurations
    Private mStudyValueHandlers As StudyValueHandlers
    Private mStudyHorizontalRules As StudyHorizontalRules
    Private mHorizontalRulesLineSeries As LineSeries
    Private mName As String
    Private mStudyLibraryName As String
    Private mInstanceFullyQualifiedName As String
    Private mStudy As _IStudy
    Private mUnderlyingStudy As _IStudy
    Private mInputValueNames() As String
    Private mChartRegionName As String

    Private mConfig As ConfigurationSection

    Private mIdent As String

#End Region

#Region "Constructors"

    Public Sub New()
        Me.New(ChartUtils.TWUtilities.GenerateGUIDString)
    End Sub

    Public Sub New(id As String)
        MyBase.New()
        ReDim mInputValueNames(0)
        mInputValueNames(0) = ChartUtils.StudyUtils.DefaultStudyValueName
        mStudyConfigs = New StudyConfigurations
        mStudyValueConfigurations = New StudyValueConfigurations
        mStudyValueHandlers = New StudyValueHandlers
        mStudyHorizontalRules = New StudyHorizontalRules
        mIdent = id
    End Sub


#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"


    Public Property ChartRegionName() As String
        Get
            Return mChartRegionName
        End Get
        Set
            mChartRegionName = Value
            storeSettings()
        End Set
    End Property

    Public WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If Value Is mConfig Then Exit Property
            If mConfig IsNot Nothing Then mConfig.Remove()
            mConfig = Value
            If mConfig Is Nothing Then Return
            storeSettings()
        End Set
    End Property

    Public Property HorizontalRulesLineSeries() As LineSeries
        Get
            HorizontalRulesLineSeries = mHorizontalRulesLineSeries
        End Get
        Set
            mHorizontalRulesLineSeries = Value
        End Set
    End Property

    Friend ReadOnly Property Id() As String
        Get
            Return mIdent
        End Get
    End Property

    Public Property InputValueNames() As String()
        Get
            InputValueNames = mInputValueNames
        End Get
        Set
            mInputValueNames = Value
            storeSettings()
        End Set
    End Property

    Public Property InstanceFullyQualifiedName() As String
        Get
            InstanceFullyQualifiedName = mInstanceFullyQualifiedName
        End Get
        Set
            mInstanceFullyQualifiedName = Value
            storeSettings()
        End Set
    End Property

    Public ReadOnly Property Key() As String
        Get
            Return Study.InstanceName
        End Get
    End Property


    Public Property Name() As String
        Get
            Return mName
        End Get
        Set
            mName = Value
            storeSettings()
        End Set
    End Property

    Public Property Parameters() As Parameters
        Get
            Return mParameters
        End Get
        Set
            mParameters = Value
            storeSettings()
        End Set
    End Property

    Public Property Study() As _IStudy
        Get
            Return mStudy
        End Get
        Set
            mStudy = Value
        End Set
    End Property

    Public ReadOnly Property StudyConfigurations() As StudyConfigurations
        Get
            Return mStudyConfigs
        End Get
    End Property

    Public ReadOnly Property StudyHorizontalRules() As StudyHorizontalRules
        Get
            Return mStudyHorizontalRules
        End Get
    End Property

    Public Property StudyLibraryName() As String
        Get
            Return mStudyLibraryName
        End Get
        Set
            mStudyLibraryName = Value
            storeSettings()
        End Set
    End Property

    Public ReadOnly Property StudyValueConfigurations() As StudyValueConfigurations
        Get
            Return mStudyValueConfigurations
        End Get
    End Property

    Public Property StudyValueHandlers() As StudyValueHandlers
        Get
            Return mStudyValueHandlers
        End Get
        Friend Set
            mStudyValueHandlers = Value
        End Set
    End Property

    Public Property UnderlyingStudy() As _IStudy
        Get
            Return mUnderlyingStudy
        End Get
        Set
            mUnderlyingStudy = Value
        End Set
    End Property

    Public ReadOnly Property ValueSeries(valueName As String) As _IGraphicObjectSeries
        Get
            Return mStudyValueHandlers.Item(valueName).ValueSeries
        End Get
    End Property

#End Region

#Region "Methods"

    Friend Sub ClearStudyConfigs()
        mStudyConfigs = New StudyConfigurations
    End Sub

    Friend Sub ClearStudyValueHandlers()
        mStudyValueHandlers = New StudyValueHandlers
    End Sub

    Public Function Clone() As StudyConfiguration
        Clone = New StudyConfiguration(mIdent)
        Clone.ChartRegionName = mChartRegionName
        Clone.InputValueNames = mInputValueNames
        Clone.Name = mName
        Clone.Parameters = mParameters.Clone
        Clone.StudyLibraryName = mStudyLibraryName

        Dim newHrs = Clone.StudyHorizontalRules
        For Each hr As StudyHorizontalRule In mStudyHorizontalRules
            Dim newHr = newHrs.add
            newHr.color = hr.color
            newHr.style = hr.style
            newHr.thickness = hr.thickness
            newHr.y = hr.y
        Next hr

        Clone.Study = mStudy

        Dim newSvcs = Clone.StudyValueConfigurations
        For Each svc As StudyValueConfiguration In mStudyValueConfigurations
            Dim newSvc = newSvcs.Add(svc.ValueName)
            newSvc.BarFormatterFactoryName = svc.BarFormatterFactoryName
            newSvc.BarFormatterLibraryName = svc.BarFormatterLibraryName
            newSvc.BarStyle = svc.BarStyle
            newSvc.ChartRegionName = svc.ChartRegionName
            newSvc.DataPointStyle = svc.DataPointStyle
            newSvc.IncludeInChart = svc.IncludeInChart
            newSvc.Layer = svc.Layer
            newSvc.LineStyle = svc.LineStyle
            newSvc.TextStyle = svc.TextStyle
        Next svc

        Clone.UnderlyingStudy = mUnderlyingStudy

        Dim newScs = Clone.StudyConfigurations
        For Each sc As StudyConfiguration In mStudyConfigs
            newScs.Add(sc.Clone)
        Next

        ' don't do a deep copy of the studyValueHandlers because it's
        ' immutable as far as non-Friend callers are concerned
        Clone.StudyValueHandlers = mStudyValueHandlers
    End Function

    ' Only called when a chart has been cleared, so no need to worry about whether regions are
    ' still in use etc
    Friend Sub Finish()
        If Not mStudyValueHandlers Is Nothing Then
            For Each svh As StudyValueHandler In mStudyValueHandlers
                svh.Finish()
                mStudy.RemoveStudyValueListener(svh)
            Next
            mStudyValueHandlers.Clear()
        End If

        For Each sc As StudyConfiguration In mStudyConfigs
            sc.Finish()
        Next
    End Sub

    Friend Sub LoadFromConfig(
                    config As ConfigurationSection)
        mConfig = config

        If mConfig Is Nothing Then Exit Sub

        mIdent = mConfig.InstanceQualifier
        mName = mConfig.GetSetting(ConfigSettingName)
        mStudyLibraryName = mConfig.GetSetting(ConfigSettingStudyLibraryName)
        mInstanceFullyQualifiedName = mConfig.GetSetting(ConfigSettingInstanceFullyQualifiedName)
        mInputValueNames = Split(mConfig.GetSetting(ConfigSettingInputValueNames), ",")
        mChartRegionName = mConfig.GetSetting(ConfigSettingChartRegionName)

        mParameters = ChartUtils.TWUtilities.LoadParametersFromConfig(mConfig.GetConfigurationSection(ConfigSectionParameters))

        mStudyValueConfigurations = New StudyValueConfigurations
        mStudyValueConfigurations.LoadFromConfig(mConfig.GetConfigurationSection(ConfigSectionStudyValueConfigurations))

        mStudyHorizontalRules = New StudyHorizontalRules
        mStudyHorizontalRules.LoadFromConfig(mConfig.AddConfigurationSection(ConfigSectionStudyHorizontalRules))

        mStudyConfigs = New StudyConfigurations
        mStudyConfigs.LoadFromConfig(mConfig.AddConfigurationSection(ConfigSectionStudyConfigs))
    End Sub

    Public Sub RemoveFromConfig()
        If Not mConfig Is Nothing Then mConfig.Remove()
    End Sub

    Public Overrides Function ToString() As String
        Return InstanceFullyQualifiedName
    End Function

    Friend Sub Update()
        If Not mStudyValueHandlers Is Nothing Then mStudyValueHandlers.Update()
        If Not mStudyConfigs Is Nothing Then mStudyConfigs.Update()
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub storeSettings()
        If mConfig Is Nothing Then Exit Sub

        mConfig.SetSetting(ConfigSettingName, mName)
        mConfig.SetSetting(ConfigSettingStudyLibraryName, mStudyLibraryName)
        mConfig.SetSetting(ConfigSettingInstanceFullyQualifiedName, mInstanceFullyQualifiedName)
        mConfig.SetSetting(ConfigSettingInputValueNames, Join(mInputValueNames, ","))
        mConfig.SetSetting(ConfigSettingChartRegionName, mChartRegionName)

        mParameters.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionParameters)
        mStudyValueConfigurations.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionStudyValueConfigurations)
        mStudyHorizontalRules.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionStudyHorizontalRules)

        mStudyConfigs.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionStudyConfigs)
    End Sub

#End Region

End Class