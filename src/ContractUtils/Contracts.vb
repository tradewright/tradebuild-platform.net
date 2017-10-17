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

Imports TWUtilities40

Namespace Contracts
    Public NotInheritable Class Contracts
        Implements IContracts
        Implements IEnumerable(Of IContract)


#Region "Description"
        
        '
#End Region

#Region "Amendment history"
        
        '
        '
        '

#End Region

#Region "Interfaces"


#End Region

#Region "Events"

        Event CollectionChanged(ByRef ev As CollectionChangeEventData)

#End Region

#Region "Constants"

        Private Const ModuleName As String = "Contracts"

#End Region

#Region "Enums"
        
#End Region

#Region "Types"
        
#End Region

#Region "Member variables"

        Private mContracts As SortedDictionary(Of ContractComparable, IContract) = New SortedDictionary(Of ContractComparable, IContract)

        Private mChangeListeners As New TWUtilities40.Listeners

        Private mSortKeys() As ContractSortKeyId

#End Region

#Region "Constructors"

        Public Sub New()
            ReDim mSortKeys(7)
            mSortKeys(0) = ContractSortKeyId.SecType
            mSortKeys(1) = ContractSortKeyId.Symbol
            mSortKeys(2) = ContractSortKeyId.Mutiplier
            mSortKeys(3) = ContractSortKeyId.Exchange
            mSortKeys(4) = ContractSortKeyId.Currency
            mSortKeys(5) = ContractSortKeyId.Expiry
            mSortKeys(6) = ContractSortKeyId.Strike
            mSortKeys(7) = ContractSortKeyId.Right
        End Sub

#End Region

#Region "IContracts Interface Members"

        Public Sub AddCollectionChangeListener(pListener As ICollectionChangeListener) Implements IContracts.AddCollectionChangeListener
            mChangeListeners.Add(pListener)
        End Sub

        Public ReadOnly Property Count() As Integer Implements IContracts.Count
            Get
                Return mContracts.Count
            End Get
        End Property

        Public Function Item(pKey As IContractSpecifier) As IContract Implements IContracts.Item
            Return mContracts.Item(createComparable(pKey))
        End Function

        Public Function ItemAtIndex(pIndex As Integer) As IContract Implements IContracts.ItemAtIndex
            If Not pIndex > 0 And pIndex <= mContracts.Count Then Throw New ArgumentException("Invalid index")

            Dim i = 0
            For Each contract In mContracts.Values
                If i = pIndex Then
                    Return contract
                    Exit Function
                End If
                i += 1
            Next
            Return Nothing
        End Function

        Public Sub RemoveCollectionChangeListener(pListener As ICollectionChangeListener) Implements IContracts.RemoveCollectionChangeListener
            mChangeListeners.Remove(pListener)
        End Sub

        Public Property SortKeys() As ContractSortKeyId() Implements IContracts.SortKeys
            Get
                SortKeys = mSortKeys
            End Get
            Set(Value As ContractSortKeyId())
                setSortKeys(Value)
            End Set
        End Property

#End Region

#Region "IEnumerable(Of IContract) Interface Members"

        Public Function GetEnumerator() As IEnumerator(Of IContract) Implements IEnumerable(Of IContract).GetEnumerator
            Return mContracts.Values.GetEnumerator
        End Function

        Private Function GetEnumerator1() As IEnumerator Implements Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

#End Region

#Region "xxxx Event Handlers"
        
#End Region

#Region "Properties"

#End Region

#Region "Methods"

        Friend Sub Add(pContract As IContract)
            AddContract(mContracts, pContract)
            fireCollectionChange(CollectionChangeTypes.CollItemAdded, pContract)
        End Sub

        Public Overrides Function ToString() As String
            Dim s = String.Empty
            For Each contract In mContracts.Values
                s = s & contract.Specifier.ToString() & vbCrLf
            Next
            Return s
        End Function

#End Region

#Region "Helper Functions"

        Private Sub AddContract(pContracts As SortedDictionary(Of ContractComparable, IContract), pContract As IContract)
            pContracts.Add(createComparable(pContract.Specifier), pContract)
        End Sub

        Private Function createComparable(contractSpec As IContractSpecifier) As ContractComparable
            createComparable = New ContractComparable(contractSpec, mSortKeys)
        End Function

        Private Sub fireCollectionChange(changeType As CollectionChangeTypes, affectedObject As IContract)
            Dim ev As CollectionChangeEventData
            ev.Source = Me
            ev.ChangeType = changeType
            ev.AffectedItem = affectedObject

            Static sInit As Boolean
            Static sCurrentListeners() As Object
            Static sSomeListeners As Boolean

            If Not sInit Or Not mChangeListeners.Valid Then
                sInit = True
                sSomeListeners = mChangeListeners.GetCurrentListeners(sCurrentListeners)
            End If
            If sSomeListeners Then
                For i = 0 To UBound(sCurrentListeners)
                    Dim lListener = DirectCast(sCurrentListeners(i), ICollectionChangeListener)
                    lListener.Change(ev)
                Next
            End If

            RaiseEvent CollectionChanged(ev)
        End Sub

        Private Sub setSortKeys(Value() As ContractSortKeyId)
            If Value Is Nothing Then
                ReDim mSortKeys(0)
                mSortKeys(0) = ContractSortKeyId.None
            Else
                mSortKeys = Value
            End If

            Dim newContracts = New SortedDictionary(Of ContractComparable, IContract)

            For Each contract In mContracts.Values
                AddContract(newContracts, contract)
            Next

            mContracts = newContracts
        End Sub

#End Region

End Class
End Namespace
