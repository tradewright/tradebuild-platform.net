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

Public Class FutureHandler(Of V, U)

    Private WithEvents mFutureWaiter As New FutureWaiter

    Private mCompletionAction As Action(Of V, U)
    Private mCancellationAction As Action(Of V, U)
    Private mErrorAction As Action(Of Integer, String, String)

    Public Sub New(future As _IFuture,
                   Optional continuationData As U = Nothing,
                   Optional completionAction As Action(Of V, U) = Nothing,
                   Optional cancellationAction As Action(Of V, U) = Nothing,
                   Optional errorAction As Action(Of Integer, String, String) = Nothing)
        mCompletionAction = completionAction
        mCancellationAction = cancellationAction
        mErrorAction = errorAction
        mFutureWaiter.Add(future, continuationData)
    End Sub

    Private Sub mFutureWaiter_WaitCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitCompleted
        If ev.Future.IsPending Then Exit Sub

        If ev.Future.IsFaulted Then
            If mErrorAction IsNot Nothing Then mErrorAction(ev.Future.ErrorNumber, ev.Future.ErrorMessage, ev.Future.ErrorSource)
        ElseIf ev.Future.IsCancelled Then
            If mCancellationAction IsNot Nothing Then mCancellationAction(DirectCast(ev.Future.Value, V), DirectCast(ev.ContinuationData, U))
        ElseIf ev.Future.IsAvailable Then
            If mCompletionAction IsNot Nothing Then mCompletionAction(DirectCast(ev.Future.Value, V), DirectCast(ev.ContinuationData, U))
        End If
    End Sub

    Public Sub Cancel()
        mFutureWaiter.Cancel()
    End Sub

End Class
