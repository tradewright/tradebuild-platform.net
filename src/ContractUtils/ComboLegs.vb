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

Imports System.Collections.ObjectModel
Imports System.Runtime.Serialization

Namespace Contracts
    <DataContract>
    Public NotInheritable Class ComboLegs
        Implements IComboLegs

        '@================================================================================
        ' Description
        '@================================================================================
        
        '
        '@================================================================================
        ' Amendment history
        '@================================================================================
        
        '
        '
        '

        '@================================================================================
        ' Interfaces
        '@================================================================================


        '@================================================================================
        ' Events
        '@================================================================================

        Public Event CollectionChanged(sender As Object, e As Specialized.NotifyCollectionChangedEventArgs) Implements Specialized.INotifyCollectionChanged.CollectionChanged
        
        '@================================================================================
        ' Types
        '@================================================================================
        
        '@================================================================================
        ' Constants
        '@================================================================================

        '@================================================================================
        ' Member variables
        '@================================================================================

        <DataMember>
        Private WithEvents mComboLegs As ObservableCollection(Of IComboleg) = New ObservableCollection(Of IComboleg)

        '@================================================================================
        ' Enums
        '@================================================================================
        
        '@================================================================================
        ' Constructors
        '@================================================================================

        Friend Sub New()
        End Sub

        '@================================================================================
        ' mComboLegs Event Handlers
        '@================================================================================
        
        Private Sub mComboLegs_CollectionChanged(sender As Object, e As Specialized.NotifyCollectionChangedEventArgs) Handles mComboLegs.CollectionChanged
            RaiseEvent CollectionChanged(Me, e)
        End Sub

        '@================================================================================
        ' Properties
        '@================================================================================

        Public ReadOnly Property Count() As Integer Implements IComboLegs.Count
            Get
                Count = mComboLegs.Count()
            End Get
        End Property

        '@================================================================================
        ' Methods
        '@================================================================================

        Friend Sub Add(pComboLeg As IComboleg)
            mComboLegs.Add(pComboLeg)
        End Sub

        Friend Function AddLeg(pContractSpec As IContractSpecifier, pIsBuyLeg As Boolean, pRatio As Integer) As IComboleg
            AddLeg = New ComboLeg(pContractSpec, pIsBuyLeg, pRatio)
            Add(AddLeg)
        End Function

        Public Function GetEnumerator() As IEnumerator(Of IComboleg) Implements IEnumerable(Of IComboleg).GetEnumerator
            Return mComboLegs.GetEnumerator
        End Function

        Private Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return mComboLegs.GetEnumerator
        End Function

        Public Function Item(pIndex As Integer) As IComboleg Implements IComboLegs.Item
            Item = mComboLegs.Item(pIndex)
        End Function

        Friend Sub Remove(index As Integer)
            mComboLegs.RemoveAt(index)
        End Sub

        Public Overrides Function ToString() As String
            Dim s = String.Empty
            Dim i = 0
            For Each comboLegObj In mComboLegs
                i = i + 1
                s = $"{s}Combo leg{i,0:d}: {comboLegObj.ToString()}; "
            Next
            Return s
        End Function

        '@================================================================================
        ' Helper Functions
        '@================================================================================

    End Class
End Namespace
