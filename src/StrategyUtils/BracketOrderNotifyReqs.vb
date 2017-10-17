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

Imports OrderUtils27

Friend NotInheritable Class BracketOrderNotifyRequests

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "BracketOrderNotifyRequests"

#End Region

#Region "Member variables"

    Private mBracketOrderNotificationRequests As New Dictionary(Of String, List(Of NotificationRequest))

#End Region

#Region "Constructors"

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Friend Function GetEnumerator(pBracketOrder As IBracketOrder) As IEnumerator(Of NotificationRequest)
        Dim lKey = pBracketOrder.Key
        AssertArgument(mBracketOrderNotificationRequests.ContainsKey(lKey), "No notification requests for pBracketOrder")

        Return mBracketOrderNotificationRequests.Item(lKey).GetEnumerator

    End Function

#End Region

#Region "Methods"

    Friend Sub Add(pBracketOrder As _IBracketOrder, pStrategy As Object, pResourceContext As ResourceContext)
        Dim lKey = pBracketOrder.Key

        Dim lRequests As List(Of NotificationRequest)
        If mBracketOrderNotificationRequests.ContainsKey(lKey) Then
            lRequests = mBracketOrderNotificationRequests.Item(lKey)
        Else
            lRequests = New List(Of NotificationRequest)
            mBracketOrderNotificationRequests.Add(lKey, lRequests)
        End If

        Dim lNotificationRequest As New NotificationRequest(DirectCast(pStrategy, IBracketOrderEventSink), pResourceContext)
        lRequests.Add(lNotificationRequest)
    End Sub

#End Region

#Region "Helper Functions"
#End Region

End Class