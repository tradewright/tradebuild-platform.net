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
    Public NotInheritable Class ComboContractSpecBldr

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

        Private Const ModuleName As String = "ComboContractSpecBldr"

#End Region

#Region "Member variables"

        Private mContractSpecifier As ContractSpecifier
        Private mComboLegs As ComboLegs

#End Region

#Region "Constructors"

        Public Sub New()
            mContractSpecifier = New ContractSpecifier
            mContractSpecifier.SecType = SecurityType.Combo
            mComboLegs = New ComboLegs
            mContractSpecifier.ComboLegs = mComboLegs
        End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

        Public ReadOnly Property ContractSpecifier() As IContractSpecifier
            Get
                ContractSpecifier = mContractSpecifier
            End Get
        End Property

#End Region

#Region "Methods"

        Public Function AddLeg(contractSpec As IContractSpecifier, isBuyLeg As Boolean, ratio As Integer) As IComboleg
            Return mComboLegs.AddLeg(contractSpec, isBuyLeg, ratio)
        End Function

        Public Sub RemoveLeg(index As Integer)
            mComboLegs.Remove(index)
        End Sub

#End Region

#Region "Helper Functions"
#End Region

    End Class
End Namespace