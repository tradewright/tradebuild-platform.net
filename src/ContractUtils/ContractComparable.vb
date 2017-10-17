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

Namespace Contracts
    Friend NotInheritable Class ContractComparable
        Implements IComparable

#Region "Interfaces"


#End Region

#Region "Events"
        
#End Region

#Region "Enums"
        
#End Region

#Region "Types"
        
#End Region

#Region "Constants"

        Private Const ModuleName As String = "ContractComparable"

#End Region

#Region "Member variables"

        Private mSortKeys() As ContractSortKeyId
        Private mContractSpec As IContractSpecifier

#End Region

#Region "Constructors"
        
        Friend Sub New(pContractSpec As IContractSpecifier, ByRef pSortkeys() As ContractSortKeyId)
            mContractSpec = pContractSpec
            mSortKeys = pSortkeys
        End Sub

#End Region

#Region "IComparable Interface Members"

        Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
            If Not TypeOf obj Is ContractComparable Then Throw New ArgumentException("Type of pobj is not ContractComparable")

            Return Contract.CompareContractSpecs(mContractSpec, DirectCast(obj, ContractComparable).ContractSpec, mSortKeys)
        End Function

#End Region

#Region "XXXX Event Handlers"
        
#End Region

#Region "Properties"

        Friend ReadOnly Property ContractSpec() As IContractSpecifier
            Get
                ContractSpec = mContractSpec
            End Get
        End Property

#End Region

#Region "Methods"

#End Region

#Region "Helper Functions"

#End Region

End Class
End Namespace
