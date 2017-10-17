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

Imports TradeWright.Trading.Utils.Sessions

Imports TWUtilities40

Namespace Contracts
    Friend NotInheritable Class SessionBuilderFutBldr

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

        Private Const ModuleName As String = "SessionBuilderFutBldr"

#End Region

#Region "Member variables"

        Private WithEvents mFutureBuilder As New FutureBuilder
        Private WithEvents mFutureWaiter As New FutureWaiter
        Private mUseExchangeTimeZone As Boolean

        Private mSelfRef As Object

#End Region

#Region "Constructors"

        Public Sub New()
            mSelfRef = Me
        End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "mFutureBuilder Event Handlers"

        Private Sub mFutureBuilder_Cancelled(ByRef ev As CancelledEventData) Handles mFutureBuilder.Cancelled
            mFutureWaiter.Clear()
            mFutureBuilder.Cancel()

            mSelfRef = Nothing
        End Sub

#End Region

#Region "mFutureWaiter Event Handlers"

        Private Sub mFutureWaiter_WaitCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitCompleted
            If Not ev.Future.IsAvailable Then Exit Sub

            Dim lContract = DirectCast(ev.Future.Value, IContract)

            Dim lSessionBuilder = New SessionBuilder(lContract.SessionStartTime, lContract.SessionEndTime, TW.GetTimeZone(If(mUseExchangeTimeZone, lContract.TimezoneName, "")))

            mFutureBuilder.Value = lSessionBuilder
            mFutureBuilder.Complete()

            mSelfRef = Nothing
        End Sub

#End Region

#Region "Properties"

        Friend ReadOnly Property Future() As _IFuture
            Get
                Return mFutureBuilder.Future
            End Get
        End Property

#End Region

#Region "Methods"

        Friend Sub Initialise(pContractFuture As IFuture, pUseExchangeTimeZone As Boolean)
            mFutureWaiter.Add(pContractFuture)
            mUseExchangeTimeZone = pUseExchangeTimeZone
        End Sub

#End Region

#Region "Helper Functions"
#End Region

    End Class
End Namespace
