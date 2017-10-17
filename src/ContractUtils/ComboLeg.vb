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

Imports System.Runtime.Serialization

Namespace Contracts
    <DataContract()> Public NotInheritable Class ComboLeg
        Implements IComboleg

        Private Sub New()
        End Sub

        Friend Sub New(pContractSpec As IContractSpecifier, pIsBuyLeg As Boolean, pRatio As Integer)
            ContractSpec = pContractSpec
            IsBuyLeg = pIsBuyLeg
            Ratio = pRatio
        End Sub

#Region "Properties"

        <DataMember()>
        Public ReadOnly Property ContractSpec() As IContractSpecifier Implements IComboleg.ContractSpec

        <DataMember()>
        Public ReadOnly Property IsBuyLeg() As Boolean Implements IComboleg.IsBuyLeg

        <DataMember()>
        Public ReadOnly Property Ratio() As Integer Implements IComboleg.Ratio

#End Region

#Region "Methods"

        Public Overrides Function ToString() As String
            Return $"action={If(IsBuyLeg, "BUY", "SELL")}; ratio={Ratio}; contractSpec=({ContractSpec.ToString()})"
        End Function

#End Region

#Region "Helper Functions"
#End Region

End Class
End Namespace
