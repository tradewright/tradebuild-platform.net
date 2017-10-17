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

Imports StudyUtils27
Imports TickUtils27
Imports TradeBuild27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Charts.BarFormatters

Public Class ConfigUtils
    Private Sub New()
        ' prevent instantiation
    End Sub

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

    Public Enum ConfigFlags
        ConfigFlagSetAsDefault = 1
        ConfigFlagIncludeDefaultStudyLibrary = 2
        ConfigFlagIncludeDefaultBarFormatterLibrary = 4
    End Enum

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "ConfigUtils"

    Private Const AttributeNameAppInstanceConfigDefault As String = "Default"

    Private Const AttributeValueTrue As String = "True"
    Private Const AttributeValueFalse As String = "False"

    Private Const ConfigSectionAppConfig As String = "AppConfig"
    Private Const ConfigSectionAppConfigs As String = "AppConfigs"
    Private Const ConfigNameTradeBuild As String = "TradeBuild"

    Private Const ConfigSectionPathSeparator As String = "/"

#End Region

#Region "Member variables"

    Private Shared StudyUtils As New StudyUtils27.StudyUtils
    Private Shared TradeBuild As New TradeBuild27.TradeBuild

    Private Shared TWUtilities As New TWUtilities40.TWUtilities

#End Region

#Region "Constructors"

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Public Shared Function AddAppInstanceConfig(
                pConfigStore As ConfigurationStore,
                pNewAppConfigName As String,
                pFlags As ConfigFlags,
                Optional pPermittedServiceProviderRoles As ServiceProviderRoles =
                                ServiceProviderRoles.SPRoleOrderSubmissionSimulated Or
                                ServiceProviderRoles.SPRoleHistoricalDataInput Or
                                ServiceProviderRoles.SPRoleContractDataPrimary Or
                                ServiceProviderRoles.SPRoleRealtimeData,
                Optional pTwsServer As String = "",
                Optional pTwsPort As Integer = 7496,
                Optional pTwsClientId As Integer = -1,
                Optional pTwsConnectionRetryIntervalSecs As Integer = 60,
                Optional pTwsLogLevel As String = "ERROR",
                Optional pMarketDataSourceOptions As MarketDataSourceOptions = MarketDataSourceOptions.MarketDataSourceOptUseExchangeTimeZone,
                Optional pNumberOfMarketDepthRows As Integer = 20,
                Optional pTickfilesPath As String = "",
                Optional pOrderPersistenceFilePath As String = "") As ConfigurationSection

        checkValidConfigurationFile(pConfigStore)

        If GetAppInstanceConfig(pConfigStore, pNewAppConfigName) IsNot Nothing Then Throw New InvalidOperationException("App instance config already exists")

        Dim newAppConfigSection = pConfigStore.AddConfigurationSection(generateAppInstanceSectionPath(pNewAppConfigName))
        If CBool(pFlags And ConfigFlags.ConfigFlagSetAsDefault) Then setDefault(pConfigStore, newAppConfigSection)

        Dim newAppTradeBuildSection = newAppConfigSection.AddConfigurationSection(ConfigNameTradeBuild)
        TradeBuild.SetDefaultTradeBuildConfiguration(newAppTradeBuildSection,
                                    pPermittedServiceProviderRoles,
                                    pTwsServer,
                                    pTwsPort,
                                    pTwsClientId,
                                    pTwsConnectionRetryIntervalSecs,
                                    pTwsLogLevel,
                                    pMarketDataSourceOptions,
                                    pNumberOfMarketDepthRows,
                                    pTickfilesPath,
                                    pOrderPersistenceFilePath)

        If CBool(pFlags And ConfigFlags.ConfigFlagIncludeDefaultStudyLibrary) Then
            StudyUtils.SetDefaultStudyLibraryConfig(newAppTradeBuildSection)
        End If

        If CBool(pFlags And ConfigFlags.ConfigFlagIncludeDefaultBarFormatterLibrary) Then
            BarFormatters.SetDefaultBarFormatterLibraryConfig(newAppTradeBuildSection)
        End If

        Return newAppConfigSection
    End Function

    Public Shared Function GetAppInstanceConfig(
                pConfigStore As ConfigurationStore,
                pName As String) As ConfigurationSection
        Return pConfigStore.GetConfigurationSection(generateAppInstanceSectionPath(pName))
    End Function

    Public Shared Function GetAppInstanceConfigs(
                pConfigStore As ConfigurationStore) As ConfigurationSection
        Return getAppInstanceConfigsSection(pConfigStore)
    End Function

    Public Shared Function GetDefaultAppInstanceConfig(
                pConfigStore As ConfigurationStore) As ConfigurationSection
        Dim sections = getAppInstanceConfigsSection(pConfigStore)

        For Each section As ConfigurationSection In sections
            If CBool(section.GetAttribute(AttributeNameAppInstanceConfigDefault, AttributeValueFalse)) Then
                Return section
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function GetTradeBuildConfig(
                pAppInstanceConfig As ConfigurationSection) As ConfigurationSection
        Return pAppInstanceConfig.AddConfigurationSection(ConfigNameTradeBuild)
    End Function

    Public Shared Function InitialiseConfigFile(
                pConfigStore As ConfigurationStore) As ConfigurationStore
        Dim sections = getAppInstanceConfigsSection(pConfigStore)
        If sections IsNot Nothing Then
            log("Removing existing app instance configs section from config file")
            pConfigStore.RemoveConfigurationSection(getAppInstanceConfigsPath)
        End If

        log("Creating app instance configs section in config file")
        pConfigStore.AddConfigurationSection(ConfigSectionPathSeparator & ConfigSectionAppConfigs)

        Return pConfigStore
    End Function

    Public Shared Function IsValidConfigurationFile(
                pConfigStore As ConfigurationStore) As Boolean
        Return getAppInstanceConfigsSection(pConfigStore) IsNot Nothing
    End Function

    Public Shared Sub RemoveAppInstanceConfig(
                pConfigStore As ConfigurationStore,
                name As String)
        pConfigStore.RemoveConfigurationSection(generateAppInstanceSectionPath(name))
    End Sub

    Public Shared Function SetDefaultAppInstanceConfig(
                pConfigStore As ConfigurationStore,
                name As String) As ConfigurationSection
        checkValidConfigurationFile(pConfigStore)

        Dim cs = GetAppInstanceConfig(pConfigStore, name)
        If cs Is Nothing Then Throw New ArgumentException("Specified app instance config does not exist")

        setDefault(pConfigStore, cs)
        Return cs
    End Function

    Public Shared Sub UnsetDefaultAppInstanceConfig(
                pConfigStore As ConfigurationStore)
        Dim defaultConfig = GetDefaultAppInstanceConfig(pConfigStore)
        If defaultConfig IsNot Nothing Then defaultConfig.SetAttribute(AttributeNameAppInstanceConfigDefault, AttributeValueFalse)
    End Sub

#End Region

#Region "Helper Functions"

    Private Shared Sub checkValidConfigurationFile(
                pConfigStore As ConfigurationStore)
        If Not IsValidConfigurationFile(pConfigStore) Then Throw New ArgumentException("Configuration file has not been correctly intialised")
    End Sub

    Private Shared Function generateAppInstanceSectionPath(
                name As String) As String
        Return ConfigSectionPathSeparator & ConfigSectionAppConfigs & ConfigSectionPathSeparator & ConfigSectionAppConfig & "(" & name & ")"
    End Function

    Private Shared Function getAppInstanceConfigsPath() As String
        Return ConfigSectionPathSeparator & ConfigSectionAppConfigs
    End Function

    Private Shared Function getAppInstanceConfigsSection(
                pConfigStore As ConfigurationStore) As ConfigurationSection
        Return pConfigStore.GetConfigurationSection(getAppInstanceConfigsPath)
    End Function

    Private Shared Sub log(
                message As String,
                Optional level As LogLevels = LogLevels.LogLevelDetail)
        Static lLogger As Logger
        If lLogger Is Nothing Then lLogger = TWUtilities.GetLogger("configutils.log")

        lLogger.Log(level, message)
    End Sub

    Private Shared Sub setDefault(
                pConfigStore As ConfigurationStore,
                targetSection As ConfigurationSection)
        Dim sections = getAppInstanceConfigsSection(pConfigStore)

        For Each section As ConfigurationSection In sections
            If section Is targetSection Then
                If Not CBool(section.GetAttribute(AttributeNameAppInstanceConfigDefault, AttributeValueFalse)) Then
                    section.SetAttribute(AttributeNameAppInstanceConfigDefault, AttributeValueTrue)
                End If
            Else
                If CBool(section.GetAttribute(AttributeNameAppInstanceConfigDefault, AttributeValueFalse)) Then
                    section.SetAttribute(AttributeNameAppInstanceConfigDefault, AttributeValueFalse)
                End If
            End If
        Next
    End Sub

#End Region

End Class
