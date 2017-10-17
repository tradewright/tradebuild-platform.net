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

Imports System.Collections.Generic
Imports TWUtilities40

Public Class BarFormatterLibManager
#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

#End Region

#Region "Class variables"

    Private Shared sLibManager As BarFormatterLibManager = New BarFormatterLibManager

#End Region

#Region "Member variables"

    Private mLibrariesList As New List(Of BarFormatterLibrary)
    Private mLibrariesIndex As New Dictionary(Of String, Integer)

    Private mConfig As ConfigurationSection
    Private mLibsConfig As ConfigurationSection

#End Region

#Region "Constructors"

    Private Sub New()
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Friend ReadOnly Property AvailableBarFormatterFactories() As List(Of BarFormatterFactoryListEntry)
        Get
            Dim bffEntryList As New List(Of BarFormatterFactoryListEntry)

            For Each library In mLibrariesList
                Dim spList = library.GetFactoryNames
                For Each spName In spList
                    Dim bffEntry As BarFormatterFactoryListEntry
                    bffEntry.FactoryName = spName
                    bffEntry.LibraryName = library.Name
                    bffEntryList.Add(bffEntry)
                Next
            Next

            Return bffEntryList

        End Get
    End Property

    Friend Shared ReadOnly Property LibraryManager() As BarFormatterLibManager
        Get
            Return sLibManager
        End Get
    End Property

#End Region

#Region "Methods"

    Friend Function Add(library As BarFormatterLibrary, name As String) As BarFormatterLibrary
        If name <> "" Then library.Name = name

        If mLibrariesList.Contains(library) Then Throw New ArgumentException("This BarFormatterLibrary object has already been added")

        Try
            mLibrariesIndex.Add(name, mLibrariesList.Count)
            mLibrariesList.Add(library)
        Catch ex As Exception
            Throw New ArgumentException("A BarFormatterLibrary with this Name has already been added")
        End Try

        Return library

    End Function

    Friend Sub AddConfigEntry(assemblyQualifiedTypeName As String, enabled As Boolean, name As String)
        If mConfig Is Nothing Then Exit Sub

        Dim cs = mLibsConfig.AddConfigurationSection(BarFormatters.ConfigSectionBarFormatterLibrary & "(" & name & ")")
        cs.SetAttribute(BarFormatters.AttributeNameEnabled, CStr(enabled))
        cs.SetAttribute(BarFormatters.AttributeNameBarFormatterLibraryTypeName, assemblyQualifiedTypeName)
    End Sub

    Friend Function AddBarFormatterLibrary(assemblyQualifiedTypeName As String, enabled As Boolean, name As String) As BarFormatterLibrary
        If enabled Then
            AddBarFormatterLibrary = Add(DirectCast(Activator.CreateInstance(Type.GetType(assemblyQualifiedTypeName)), BarFormatterLibrary), name)
        Else
            AddBarFormatterLibrary = Nothing
        End If
        AddConfigEntry(assemblyQualifiedTypeName, enabled, name)
    End Function

    Public Function CreateBarFormatterFactory(barFormatterName As String, libName As String) As IBarFormatterFactory
        Dim i As Integer
        Dim library As BarFormatterLibrary
        Dim factory As IBarFormatterFactory = Nothing

        If mLibrariesIndex.TryGetValue(libName, i) Then
            library = mLibrariesList(i)
            factory = library.CreateFactory(barFormatterName)
        Else
            For Each library In mLibrariesList
                factory = library.CreateFactory(barFormatterName)
                If Not factory Is Nothing Then Exit For
            Next
        End If

        Return factory

    End Function

    Friend Sub LoadBarFormatterLibraryConfiguration(config As ConfigurationSection)
        Try
            mLibsConfig = config.GetConfigurationSection(BarFormatters.ConfigSectionBarFormatterLibraries)
        Catch ex As Exception

        End Try

        If Not mLibsConfig Is Nothing Then
            mConfig = config
            loadBarFormatterLibs()
        End If
    End Sub

    Friend Sub Remove(library As BarFormatterLibrary, Optional removeFromConfig As Boolean = True)
        Dim i As Integer
        If library Is Nothing Then Exit Sub

        For i = 0 To mLibrariesList.Count - 1
            If mLibrariesList.Item(i) Is library Then
                mLibrariesList.RemoveAt(i)
                mLibrariesIndex.Remove(library.Name)
                If removeFromConfig Then removeLibraryFromConfig(library)
                Exit For
            End If
        Next
    End Sub

    Friend Sub RemoveAll(Optional removeFromConfig As Boolean = True)
        If removeFromConfig Then
            For Each library In mLibrariesList
                removeLibraryFromConfig(library)
            Next
        End If

        mLibrariesList.Clear()
        mLibrariesIndex.Clear()
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub loadBarFormatterLibs()
        Dim cs As ConfigurationSection
        Dim libName As String
        Dim libTypeName As String = ""
        Dim libType As Type
        Dim library As BarFormatterLibrary

        For Each cs In mLibsConfig
            If CBool(cs.GetAttribute(BarFormatters.AttributeNameEnabled, "True")) Then
                libName = cs.InstanceQualifier

                Try
                    If CBool(cs.GetAttribute(BarFormatters.AttributeNameBarFormatterLibraryBuiltIn, "False")) Then
                        libType = GetType(BuiltInBarFormatterLib)
                    Else
                        libTypeName = cs.GetAttribute(BarFormatters.AttributeNameBarFormatterLibraryTypeName)
                        libType = Type.GetType(libTypeName)
                    End If

                    library = DirectCast(Activator.CreateInstance(libType), BarFormatterLibrary)
                    Add(library, libName)
                Catch ex As Exception
                    Throw New InvalidOperationException("Invalid bar formatter library type in configuration file: " & libTypeName, ex)
                End Try
            End If
        Next
    End Sub

    Private Sub removeLibraryFromConfig(library As BarFormatterLibrary)
        If mConfig IsNot Nothing Then mLibsConfig.RemoveConfigurationSection(BarFormatters.ConfigSectionBarFormatterLibrary & "(" & library.Name & ")")
    End Sub

#End Region

End Class
