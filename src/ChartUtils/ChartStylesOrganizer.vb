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
Imports System.Windows.Forms
Imports System.Collections.Generic

Public Class ChartStylesOrganizer
    Private mStyleDict As Dictionary(Of String, TreeNode) = New Dictionary(Of String, TreeNode)

    Private Shared ChartSkil As ChartSkil27.ChartSkil = New ChartSkil27.ChartSkil

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub Initialise()
        For Each style As ChartStyle In ChartSkil.ChartStylesManager
            mStyleDict.Add(style.Name, New TreeNode(style.Name))
        Next

        For Each style As ChartStyle In ChartSkil.ChartStylesManager
            If style.BasedOn Is Nothing Then
                StylesTree.Nodes.Add(mStyleDict.Item(style.Name))
            Else
                mStyleDict.Item(style.BasedOn.Name).Nodes.Add(mStyleDict.Item(style.Name))
            End If
        Next
        StylesTree.ExpandAll()
    End Sub

    Private Sub StylesTree_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles StylesTree.AfterSelect
        PropertyGrid1.SelectedObject = New ChartStyleConfigurer(DirectCast(ChartSkil.ChartStylesManager.Item(e.Node.Text), ChartStyle))
    End Sub

End Class
