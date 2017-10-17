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

Imports System.Windows.Forms

Public Class BracketOrderListSorter
    Implements IComparer

    Private mCol As Integer = 0

    Public Sub New(column As Integer)
        mCol = column
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
        If TypeOf x IsNot ListViewItem Or TypeOf y IsNot ListViewItem Then Throw New ArgumentException()

        Dim string1 = DirectCast(x, ListViewItem).SubItems(mCol).Text
        Dim string2 = DirectCast(y, ListViewItem).SubItems(mCol).Text

        Dim result As Integer

        Dim v As Double
        Dim w As Double
        Dim d As Date
        Dim e As Date

        If Double.TryParse(string1, v) And Double.TryParse(string2, w) Then
            result = If(v > w, 1, If(v < w, -1, 0))
        ElseIf Date.TryParse(string1, d) And Date.TryParse(string2, e) Then
            result = Date.Compare(d, e)
        Else
            result = String.Compare(string1, string2)
        End If
        Return result
    End Function
End Class
