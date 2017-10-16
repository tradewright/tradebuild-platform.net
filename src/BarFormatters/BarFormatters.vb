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

Public Structure BarFormatterFactoryListEntry
    Dim FactoryName As String
    Dim LibraryName As String

    Public Overrides Function ToString() As String
        Return FactoryName & "  (" & LibraryName & ")"
    End Function
End Structure

Public Module BarFormatters

    Friend Const AttributeNameEnabled As String = "Enabled"
    Friend Const AttributeNameBarFormatterLibraryBuiltIn As String = "BuiltIn"
    Friend Const AttributeNameBarFormatterLibraryName As String = "Name"
    Friend Const AttributeNameBarFormatterLibraryTypeName As String = "Type"

    Friend Const ConfigSectionBarFormatterLibraries As String = "BarFormatterLibraries"
    Friend Const ConfigSectionBarFormatterLibrary As String = "BarFormatterLibrary"

    Friend Const BuiltInBarFormatterLibraryName As String = "BuiltIn"
    Friend Const BarFormatterLibrariesRenderer As String = "BarFormatterLibConfigurer"

    Friend TWUtilities As New TWUtilities40.TWUtilities

    Friend ErrorLogger As Logger = TWUtilities.GetLogger("error")

    Public Function AddBarFormatterLibrary(
                    assemblyQualifiedTypeName As String,
                    enabled As Boolean,
                    Optional name As String = "") As BarFormatterLibrary
        If name = "" Then name = TWUtilities.GenerateGUIDString
        Return BarFormatterLibManager.LibraryManager.AddBarFormatterLibrary(assemblyQualifiedTypeName, enabled, name)
    End Function

    Public Function CreateBarFormatterFactory(pBarFormatterName As String, pLibraryName As String) As IBarFormatterFactory
        Return BarFormatterLibManager.LibraryManager.CreateBarFormatterFactory(pBarFormatterName, pLibraryName)
    End Function

    Public Function GetAvailableBarFormatterFactories() As List(Of BarFormatterFactoryListEntry)
        Return BarFormatterLibManager.LibraryManager.AvailableBarFormatterFactories
    End Function

    Public Sub LoadBarFormatterLibraryConfiguration(config As ConfigurationSection)
        BarFormatterLibManager.LibraryManager.LoadBarFormatterLibraryConfiguration(config)
    End Sub

    Friend Sub NotifyUnhandledError(e As Exception, procName As String, moduleName As String)
        TWUtilities.UnhandledErrorHandler.Notify(procName,
                                                moduleName,
                                                NameOf(BarFormatters),
                                                pErrorNumber:=e.HResult,
                                                pErrorDesc:=e.Message,
                                                pErrorSource:=e.StackTrace)
    End Sub

    Public Sub RemoveBarFormatterLibrary(library As BarFormatterLibrary)
        BarFormatterLibManager.LibraryManager.Remove(library)
    End Sub

    Public Sub RemoveAllBarFormatterLibraries(Optional removeFromConfig As Boolean = True)
        BarFormatterLibManager.LibraryManager.RemoveAll(removeFromConfig)
    End Sub

    Public Sub SetDefaultBarFormatterLibraryConfig(
                    configdata As ConfigurationSection)
        Dim libsConfig As ConfigurationSection = Nothing
        Dim libConfig As ConfigurationSection

        Try
            libsConfig = configdata.GetConfigurationSection(ConfigSectionBarFormatterLibraries)
        Catch ex As Exception

        End Try

        If libsConfig IsNot Nothing Then Throw New InvalidOperationException("BarFormatter libraries list is not empty")

        libsConfig = configdata.AddConfigurationSection(ConfigSectionBarFormatterLibraries, , BarFormatterLibrariesRenderer)
        libConfig = libsConfig.AddConfigurationSection(ConfigSectionBarFormatterLibrary & "(" & BuiltInBarFormatterLibraryName & ")")

        libConfig.SetAttribute(AttributeNameEnabled, "True")
        libConfig.SetAttribute(AttributeNameBarFormatterLibraryBuiltIn, "True")
    End Sub
End Module
