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
Imports BarUtils27
Imports TWUtilities40

Public Class StudyValueConfigurations
    Implements System.Collections.IEnumerable

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const ConfigSectionStudyValueConfig As String = "StudyValueConfiguration"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mStudyValueConfigurations As Dictionary(Of String, StudyValueConfiguration) = New Dictionary(Of String, StudyValueConfiguration)

    Private mConfig As ConfigurationSection

#End Region

#Region "Constructors"

    Friend Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Friend WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If Value Is mConfig Then Exit Property
            mConfig = Value
            storeSettings()
        End Set
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Count = mStudyValueConfigurations.Count()
        End Get
    End Property

#End Region

#Region "Methods"

    Public Function Add(valueName As String) As StudyValueConfiguration
        Add = New StudyValueConfiguration
        Add.ValueName = valueName
        mStudyValueConfigurations.Add(valueName, Add)
        storeStudyValueSettings(Add)
    End Function

    Public Function Clone() As StudyValueConfigurations
        Clone = New StudyValueConfigurations

        For Each svc As StudyValueConfiguration In mStudyValueConfigurations.Values
            Dim newSvc = Clone.Add(svc.ValueName)
            newSvc.BarStyle = svc.BarStyle
            newSvc.ChartRegionName = svc.ChartRegionName
            newSvc.DataPointStyle = svc.DataPointStyle
            newSvc.IncludeInChart = svc.IncludeInChart
            newSvc.Layer = svc.Layer
            newSvc.LineStyle = svc.LineStyle
            newSvc.TextStyle = svc.TextStyle
        Next svc
    End Function

    Public Function Item(key As String) As StudyValueConfiguration
        Item = mStudyValueConfigurations.Item(key)
    End Function

    Friend Sub LoadFromConfig(
                    config As ConfigurationSection)
        mConfig = config
        If mConfig Is Nothing Then Exit Sub

        For Each svcSect As ConfigurationSection In mConfig
            Dim svc = New StudyValueConfiguration
            svc.LoadFromConfig(svcSect)
            mStudyValueConfigurations.Add(svc.ValueName, svc)
        Next
    End Sub

    Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mStudyValueConfigurations.Values.GetEnumerator
    End Function

#End Region

#Region "Helper Functions"

    Private Sub storeSettings()
        For Each svc As StudyValueConfiguration In mStudyValueConfigurations.Values
            storeStudyValueSettings(svc)
        Next
    End Sub

    Private Sub storeStudyValueSettings(
                    studyValueConfig As StudyValueConfiguration)
        If Not mConfig Is Nothing Then
            studyValueConfig.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionStudyValueConfig & "(" & ChartUtils.TWUtilities.GenerateGUIDString & ")")
        End If
    End Sub
#End Region

End Class