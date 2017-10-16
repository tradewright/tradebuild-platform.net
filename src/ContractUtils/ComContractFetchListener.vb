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

Imports ContractUtils27

Namespace Contracts

    Public Class ComContractFetchListener
        Implements ContractUtils27._IContractFetchListener

        Private mListener As IContractFetchListener

        Friend Sub New(listener As IContractFetchListener)
            mListener = mListener
        End Sub

        Public Sub FetchCancelled(pCookie As Object) Implements _IContractFetchListener.FetchCancelled
            mListener.FetchCancelled(pCookie)
        End Sub

        Public Sub FetchCompleted(pCookie As Object) Implements _IContractFetchListener.FetchCompleted
            mListener.FetchCancelled(pCookie)
        End Sub

        Public Sub FetchFailed(pCookie As Object, pErrorCode As Integer, pErrorMessage As String, pErrorSource As String) Implements _IContractFetchListener.FetchFailed
            mListener.FetchFailed(pCookie, pErrorCode, pErrorMessage, pErrorSource)
        End Sub

        Public Sub NotifyContract(pCookie As Object, pContract As _IContract) Implements _IContractFetchListener.NotifyContract
            mListener.NotifyContract(pCookie, CType(pContract, IContract))
        End Sub
    End Class

End Namespace
