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

Imports ChartSkil27
Imports StudyUtils27

Public Class StudyValueHandlers
    Implements System.Collections.IEnumerable

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mStudyValueHandlers As Dictionary(Of String, StudyValueHandler) = New Dictionary(Of String, StudyValueHandler)


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

    Public ReadOnly Property Count() As Integer
        Get
            Count = mStudyValueHandlers.Count()
        End Get
    End Property

#End Region

#Region "Methods"

    Public Function Add(chartMgr As ChartManager, region As ChartRegion, study As _IStudy, studyValueConfig As StudyValueConfiguration, studyValueDef As StudyUtils27.StudyValueDefinition, updatePerTick As Boolean) As StudyValueHandler
        Add = New StudyValueHandler
        Add.Initialise(chartMgr, region, study, studyValueConfig, studyValueDef, updatePerTick)
        mStudyValueHandlers.Add(studyValueConfig.ValueName, Add)
    End Function

    Friend Sub Clear()
        mStudyValueHandlers.Clear()
    End Sub

    Public Function Item(index As String) As StudyValueHandler
        Item = mStudyValueHandlers.Item(index)
    End Function

    Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mStudyValueHandlers.Values.GetEnumerator
    End Function

    Friend Sub Update()
        For Each lSvh As StudyValueHandler In mStudyValueHandlers.Values
            lSvh.Update()
        Next
    End Sub

#End Region

#Region "Helper Functions"
#End Region

End Class