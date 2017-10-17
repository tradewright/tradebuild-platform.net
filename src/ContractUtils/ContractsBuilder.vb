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
    Public NotInheritable Class ContractsBuilder
        Implements IContractsBuilder

#Region "Interfaces"


#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

        Private Const ModuleName As String = "ContractsBuilder"

#End Region

#Region "Member variables"

        Private mContracts As New Contracts

#End Region

#Region "Constructors"

#End Region

#Region "IContractsBuilder Interface Members"

        Private Sub IContractsBuilder_Add(pContract As IContract) Implements IContractsBuilder.Add
            'AssertArgument gContractSpecsCompatible(mContracts.ContractSpecifier, pContract.Specifier), "Contract not compatible with contract spec"
            Add(pContract)
        End Sub

        Private ReadOnly Property IContractsBuilder_Contracts() As IContracts Implements IContractsBuilder.Contracts
            Get
                IContractsBuilder_Contracts = Contracts
            End Get
        End Property

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

        Public ReadOnly Property Contracts() As Contracts
            Get
                Contracts = mContracts
            End Get
        End Property

#End Region

#Region "Methods"

        Public Sub Add(pContract As IContract)
            mContracts.Add(pContract)
        End Sub

        'Friend Sub Initialise( _
        ''                ContractSpec As IContractSpecifier)
        'Const ProcName As String = "Initialise"
        'On Error GoTo Err
        '
        'Set mContracts = New Contracts
        'mContracts.Initialise ContractSpec
        '
        'Exit Sub
        '
        'Err:
        'gHandleUnexpectedError ProcName, ModuleName
        'End Sub

#End Region

#Region "Helper Functions"
#End Region

    End Class
End Namespace
