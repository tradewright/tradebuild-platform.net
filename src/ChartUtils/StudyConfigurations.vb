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

Public Class StudyConfigurations
    Implements System.Collections.IEnumerable

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const ConfigSectionStudyConfig As String = "StudyConfig"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mStudyConfigurations As IDictionary(Of String, StudyConfiguration) = New Dictionary(Of String, StudyConfiguration)

    Private mConfig As ConfigurationSection

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If Value Is mConfig Then Exit Property
            If mConfig IsNot Nothing Then mConfig.Remove()
            mConfig = Value
            If mConfig Is Nothing Then Return
            storeSettings()
        End Set
    End Property


    Public ReadOnly Property Count() As Integer
        Get
            Count = mStudyConfigurations.Count()
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub Add(studyConfig As StudyConfiguration)
        On Error Resume Next
        ' the studyconfig may already be in the collection because it was added when
        ' recreating this object from the config
        If Not mStudyConfigurations.Keys.Contains(studyConfig.Id) Then
            mStudyConfigurations.Add(studyConfig.Id, studyConfig)
            storeStudyConfigSettings(studyConfig)
        End If
    End Sub

    Public Sub Clear()
        For Each sc As StudyConfiguration In mStudyConfigurations.Values
            sc.RemoveFromConfig()
        Next
        mStudyConfigurations.Clear()
    End Sub

    ' Only called when a chart has been cleared, so no need to worry about whether regions are
    ' still in use etc
    Friend Sub Finish()
        For Each studyConfig As StudyConfiguration In mStudyConfigurations.Values
            studyConfig.Finish()
        Next
    End Sub

    Public Function Item(index As String) As StudyConfiguration
        Item = mStudyConfigurations.Item(index)
    End Function

    Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mStudyConfigurations.Values.GetEnumerator
    End Function

    Friend Sub LoadFromConfig(
                config As ConfigurationSection)

        mConfig = config
        If mConfig Is Nothing Then Exit Sub

        For Each scSect As ConfigurationSection In mConfig
            Dim sc = New StudyConfiguration
            sc.LoadFromConfig(scSect)
            mStudyConfigurations.Add(sc.Id, sc)
        Next
    End Sub

    Public Sub Remove(studyConfig As StudyConfiguration)
        mStudyConfigurations.Remove(studyConfig.Id)
        studyConfig.RemoveFromConfig()
    End Sub

    Friend Sub Update()
        For Each lSc As StudyConfiguration In mStudyConfigurations.Values
            lSc.Update()
        Next
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub storeSettings()
        Dim sc As StudyConfiguration
        For Each sc In mStudyConfigurations.Values
            storeStudyConfigSettings(sc)
        Next
    End Sub

    Private Sub storeStudyConfigSettings(
                    studyConfig As StudyConfiguration)
        If Not mConfig Is Nothing Then
            studyConfig.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionStudyConfig & "(" & studyConfig.Id & ")")
        End If
    End Sub

#End Region

End Class