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
Imports TWUtilities40

Namespace Contracts

    Public Class ContractStore
        Implements IContractStore
        Implements ContractUtils27.IContractStore

        Private mComContractStore As ContractUtils27.IContractStore

        Private Sub New()
        End Sub

        Public Sub New(comContractStore As ContractUtils27.IContractStore)
            mComContractStore = comContractStore
        End Sub

        Public Sub Finish() Implements IContractStore.Finish
            mComContractStore.Finish()
        End Sub

        Public Function FetchContracts(pContractSpecifier As IContractSpecifier,
                                       Optional pListener As IContractFetchListener = Nothing,
                                       Optional pCookie As Object = Nothing) As _IFuture _
                        Implements IContractStore.FetchContracts
            Dim lContractSpec = CType(pContractSpecifier, ContractUtils27.ContractSpecifierClass)
            Dim lListener As ContractUtils27._IContractFetchListener = Nothing
            If pListener IsNot Nothing Then lListener = New ComContractFetchListener(pListener)
            Return mComContractStore.FetchContracts(lContractSpec, lListener)
        End Function

        Public Function Supports(pCapabilities As ContractStoreCapabilities) As Boolean Implements IContractStore.Supports
            Return mComContractStore.Supports(CType(pCapabilities, ContractUtils27.ContractStoreCapabilities))
        End Function

        Public Function FetchContracts(pContractSpecifier As _IContractSpecifier,
                                       Optional pListener As _IContractFetchListener = Nothing,
                                       Optional pCookie As Object = Nothing) As _IFuture _
                        Implements _IContractStore.FetchContracts
            Return mComContractStore.FetchContracts(pContractSpecifier, pListener, pCookie)
        End Function

        Private Sub _IContractStore_Finish() Implements _IContractStore.Finish
            mComContractStore.Finish()
        End Sub

        Public Function Supports(pCapabilities As ContractUtils27.ContractStoreCapabilities) As Boolean Implements _IContractStore.Supports
            Return mComContractStore.Supports(pCapabilities)
        End Function
    End Class

End Namespace
