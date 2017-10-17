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

Public Class StudyHorizontalRules
    Implements System.Collections.IEnumerable

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const ConfigSectionHorizontalRule As String = "HorizontalRule"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mStudyHorizRules As List(Of StudyHorizontalRule) = New List(Of StudyHorizontalRule)

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
            If mConfig IsNot Nothing Then mConfig.Remove()
            mConfig = Value
            If mConfig Is Nothing Then Return
            storeSettings()
        End Set
    End Property

    Public ReadOnly Property count() As Integer
        Get
            count = mStudyHorizRules.Count()
        End Get
    End Property

#End Region

#Region "Methods"

    Public Function add() As StudyHorizontalRule
        add = New StudyHorizontalRule
        mStudyHorizRules.Add(add)
    End Function

    Public Function clone() As StudyHorizontalRules
        Dim hr As StudyHorizontalRule
        Dim newHr As StudyHorizontalRule
        clone = New StudyHorizontalRules
        For Each hr In mStudyHorizRules
            newHr = clone.add
            newHr.color = hr.color
            newHr.style = hr.style
            newHr.thickness = hr.thickness
            newHr.y = hr.y
        Next hr
    End Function

    Public Function item(index As Integer) As StudyHorizontalRule
        item = mStudyHorizRules.Item(index)
    End Function

    Friend Sub LoadFromConfig(
                    config As ConfigurationSection)
        Dim shrSect As ConfigurationSection
        Dim shr As StudyHorizontalRule

        mConfig = config
        If mConfig Is Nothing Then Exit Sub

        For Each shrSect In mConfig
            shr = New StudyHorizontalRule
            shr.LoadFromConfig(shrSect)
            mStudyHorizRules.Add(shr)
        Next
    End Sub

    Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mStudyHorizRules.GetEnumerator
    End Function

#End Region

#Region "Helper Functions"

    Private Sub storeSettings()
        For Each shr In mStudyHorizRules
            storeRuleSettings(shr)
        Next
    End Sub

    Private Sub storeRuleSettings(
                    shr As StudyHorizontalRule)
        If Not mConfig Is Nothing Then
            shr.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionHorizontalRule & "(" & ChartUtils.TWUtilities.GenerateGUIDString & ")")
        End If
    End Sub

#End Region

End Class