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
Imports TimeframeUtils27
Imports TWUtilities40

Imports System.Collections.Generic

Imports TradeWright.Trading.Utils.Contracts

Public Module ChartUtils
    Public Const ProjectName As String = "ChartUtils"
    Private Const ModuleName = "ChartUtils"

    Private Const ConfigSectionDefaultStudyConfig As String = "DefaultStudyConfig"

    Public Const OneMicroSecond As Double = 0.0000000000115740740740741

    Public Const ChartRegionNameCustom As String = "$custom"
    Public Const ChartRegionNameDefault As String = "$default"
    Public Const ChartRegionNameUnderlying As String = "$underlying"
    Public Const ChartRegionNamePrice As String = "Price"
    Public Const ChartRegionNameVolume As String = "Volume"

    Friend Const COMZeroDate As Date = #12/30/1899 00:00:00#

    Friend StudyUtils As New StudyUtils
    Friend TWUtilities As New TWUtilities
    Friend ChartSkil As New ChartSkil

    Private mDefaultStudyConfigurations As New Dictionary(Of String, StudyConfiguration)

    Private mConfig As ConfigurationSection

    Friend ReadOnly Property Logger() As FormattingLogger
        Get
            Static sLogger As FormattingLogger
            If sLogger Is Nothing Then sLogger = TWUtilities.CreateFormattingLogger("chartutils", ProjectName)
            Return sLogger
        End Get
    End Property

    Public Function CreateBarsStudyConfig(
                ByVal pTimeframe As Timeframe,
                ByVal pSecType As SecurityType,
                ByVal pStudyLibManager As StudyLibraryManager,
                Optional ByVal pBarFormatterFactoryName As String = "",
                Optional ByVal pBarFormatterLibraryName As String = "") As StudyConfiguration
        Dim lStudy = DirectCast(pTimeframe.BarStudy, _IStudy)

        Dim studyConfig = GetDefaultStudyConfiguration(lStudy.Name, pStudyLibManager, lStudy.LibraryName)
        studyConfig.Study = lStudy
        studyConfig.UnderlyingStudy = lStudy.UnderlyingStudy

        Dim studyDef As StudyDefinition
        studyDef = lStudy.StudyDefinition

        studyConfig.ChartRegionName = ChartRegionNamePrice

        Dim InputValueNames(3) As String
        InputValueNames(0) = StudyUtils.InputNameTrade
        InputValueNames(1) = StudyUtils.InputNameVolume
        InputValueNames(2) = StudyUtils.InputNameTickVolume
        InputValueNames(3) = StudyUtils.InputNameOpenInterest

        studyConfig.InputValueNames = InputValueNames
        studyConfig.Name = studyDef.Name

        Dim params As New Parameters
        params.SetParameterValue("Bar length", CStr(pTimeframe.TimePeriod.Length))
        params.SetParameterValue("Time units", TWUtilities.TimePeriodUnitsToString(pTimeframe.TimePeriod.Units))
        studyConfig.Parameters = params

        Dim studyValueConfig As StudyValueConfiguration
        studyValueConfig = studyConfig.StudyValueConfigurations.Item(StudyUtils.BarStudyValueBar)
        studyValueConfig.ChartRegionName = ChartRegionNamePrice
        studyValueConfig.IncludeInChart = True
        studyValueConfig.BarFormatterFactoryName = pBarFormatterFactoryName
        studyValueConfig.BarFormatterLibraryName = pBarFormatterLibraryName

        studyValueConfig = studyConfig.StudyValueConfigurations.Item(StudyUtils.BarStudyValueVolume)
        If pSecType = SecurityType.None Then
            studyValueConfig.IncludeInChart = False
        ElseIf pSecType <> SecurityType.Cash And pSecType <> SecurityType.Index Then
            studyValueConfig.ChartRegionName = ChartRegionNameVolume
            studyValueConfig.IncludeInChart = True
        Else
            studyValueConfig.IncludeInChart = False
        End If

        Return studyConfig
    End Function

    Public Function GetDefaultStudyConfiguration(name As String, studyLibManager As StudyLibraryManager, studyLibName As String) As StudyConfiguration
        Dim studyConfig As StudyConfiguration = Nothing
        If mDefaultStudyConfigurations.TryGetValue(calcDefaultStudyKey(name, studyLibName), studyConfig) Then
            Dim sc = studyConfig.Clone
            ' ensure that each instance of the default study config has its own
            ' StudyValueHandlers
            sc.StudyValueHandlers = New StudyValueHandlers()
            Return sc
        End If

        'no default study config currently exists so we'll create one from the study definition
        Dim sd = studyLibManager.GetStudyDefinition(name, studyLibName)

        studyConfig = New StudyConfiguration
        studyConfig.Name = name
        studyConfig.StudyLibraryName = studyLibName

        Select Case sd.DefaultRegion
            Case StudyDefaultRegions.StudyDefaultRegionNone
                studyConfig.ChartRegionName = ChartRegionNameUnderlying
            Case StudyDefaultRegions.StudyDefaultRegionCustom
                studyConfig.ChartRegionName = ChartRegionNameCustom
            Case StudyDefaultRegions.StudyDefaultRegionUnderlying
                studyConfig.ChartRegionName = ChartRegionNameUnderlying
            Case Else
                studyConfig.ChartRegionName = ChartRegionNameUnderlying
        End Select

        studyConfig.Parameters = studyLibManager.GetStudyDefaultParameters(name, studyLibName)

        Dim inputValueNames(sd.StudyInputDefinitions.Count - 1) As String

        inputValueNames(0) = StudyUtils.DefaultStudyValueName
        If sd.StudyInputDefinitions.Count > 1 Then
            For i = 2 To sd.StudyInputDefinitions.Count
                inputValueNames(i - 1) = sd.StudyInputDefinitions.Item(i).Name
            Next
        End If
        studyConfig.InputValueNames = inputValueNames


        For Each studyValueDef As StudyValueDefinition In sd.StudyValueDefinitions
            Dim studyValueconfig = studyConfig.StudyValueConfigurations.Add(studyValueDef.Name)

            studyValueconfig.IncludeInChart = studyValueDef.IncludeInChart
            Select Case studyValueDef.ValueMode
                Case StudyValueModes.ValueModeNone
                    studyValueconfig.DataPointStyle = DirectCast(studyValueDef.ValueStyle, DataPointStyle)
                Case StudyValueModes.ValueModeLine
                    studyValueconfig.LineStyle = DirectCast(studyValueDef.ValueStyle, LineStyle)
                Case StudyValueModes.ValueModeBar
                    studyValueconfig.BarStyle = DirectCast(studyValueDef.ValueStyle, BarStyle)
                Case StudyValueModes.ValueModeText
                    studyValueconfig.TextStyle = DirectCast(studyValueDef.ValueStyle, TextStyle)
            End Select

            Select Case studyValueDef.DefaultRegion
                Case StudyValueDefaultRegions.StudyValueDefaultRegionNone
                    studyValueconfig.ChartRegionName = ChartRegionNameDefault
                Case StudyValueDefaultRegions.StudyValueDefaultRegionCustom
                    studyValueconfig.ChartRegionName = ChartRegionNameCustom
                Case StudyValueDefaultRegions.StudyValueDefaultRegionDefault
                    studyValueconfig.ChartRegionName = ChartRegionNameDefault
                Case StudyValueDefaultRegions.StudyValueDefaultRegionUnderlying
                    studyValueconfig.ChartRegionName = ChartRegionNameUnderlying
            End Select

        Next
        SetDefaultStudyConfiguration(studyConfig)
        Return studyConfig
    End Function

    Public Sub LoadDefaultStudyConfigurationsFromConfig(config As ConfigurationSection)
        Const ProcName = "LoadDefaultStudyConfigurationsFromConfig"
        mConfig = config
        If mConfig Is Nothing Then
            TWUtilities.LogMessage("mConfig is Nothing")
        Else
            TWUtilities.LogMessage("Config section is " & mConfig.Path)
        End If

        mDefaultStudyConfigurations.Clear()

        For Each cs As ConfigurationSection In mConfig
            Dim sc = New StudyConfiguration
            sc.LoadFromConfig(cs)
            Dim key = calcDefaultStudyKey(sc.Name, sc.StudyLibraryName)
            If mDefaultStudyConfigurations.ContainsKey(key) Then
                Logger.Log($"Config file contains more than one default configuration for study {sc.Name}({sc.StudyLibraryName})", ProcName, ModuleName)
            Else
                mDefaultStudyConfigurations.Add(calcDefaultStudyKey(sc.Name, sc.StudyLibraryName), sc)
            End If
        Next
    End Sub

    Public Sub SetDefaultStudyConfiguration(studyConfig As StudyConfiguration)
        Dim sc As StudyConfiguration = Nothing
        Dim key = calcDefaultStudyKey(studyConfig.Name, studyConfig.StudyLibraryName)

        If mDefaultStudyConfigurations.TryGetValue(key, sc) Then
            sc.RemoveFromConfig()
            mDefaultStudyConfigurations.Remove(key)
        End If

        sc = studyConfig.Clone
        sc.UnderlyingStudy = Nothing
        mDefaultStudyConfigurations.Add(key, sc)
        If mConfig IsNot Nothing Then
            sc.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionDefaultStudyConfig & "(" & sc.Id & ")")
        End If
    End Sub

    Private Function calcDefaultStudyKey(studyName As String, StudyLibraryName As String) As String
        Return $"$${studyName}$${StudyLibraryName}$$"
    End Function

End Module
