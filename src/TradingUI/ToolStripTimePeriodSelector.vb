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

Imports BarUtils27
Imports TWUtilities40

Imports System.Windows.Forms.Design

Public Class ToolStripTimePeriodSelector

    Private mTimePeriodSelector As TimePeriodSelector

    Public Sub New()
        Me.New(True)
    End Sub

    Public Sub New(useShortTimePeriodStrings As Boolean)
        MyBase.New(New TimePeriodSelector(useShortTimePeriodStrings))
        mTimePeriodSelector = DirectCast(MyBase.Control, TimePeriodSelector)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

#Region "Properties"

    Public Property TimePeriod() As TimePeriod
        Get
            Return CType(MyBase.Control, TimePeriodSelector).TimePeriod
        End Get
        Set
            CType(MyBase.Control, TimePeriodSelector).TimePeriod = Value
        End Set
    End Property

    Public ReadOnly Property TimePeriodSelector() As TimePeriodSelector
        Get
            Return CType(MyBase.Control, TimePeriodSelector)
        End Get
    End Property

    Public Property UseShortTimePeriodStrings() As Boolean
        Get
            Return mTimePeriodSelector.UseShortTimePeriodStrings
        End Get
        Set
            mTimePeriodSelector.UseShortTimePeriodStrings = value
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub AppendEntry(timePeriod As TimePeriod)
        CType(MyBase.Control, TimePeriodSelector).AppendEntry(timePeriod)
    End Sub

    Public Sub Initialise(validator As ITimePeriodValidator)
        CType(MyBase.Control, TimePeriodSelector).Initialise(validator)
    End Sub

    Public Sub SelectTimePeriod(ByRef tfDesignator As TimePeriod)
        CType(MyBase.Control, TimePeriodSelector).SelectTimePeriod(tfDesignator)
    End Sub

#End Region

End Class
