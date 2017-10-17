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


Public NotInheritable Class OrderSpecifier

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "OrderSpecifier"

#End Region

#Region "Member variables"

    Private mOrderRole As OrderRole
    Private mOrderType As Integer
    Private mPrice As Double
    Private mOffset As Integer
    Private mTriggerPrice As Double
    Private mTIF As Integer

#End Region

#Region "Constructors"

    Friend Sub New(pOrderRole As OrderRole, pOrderType As Integer, pPrice As Double, pOffset As Integer, pTriggerPrice As Double, pTIF As Integer)
        mOrderRole = pOrderRole
        mOrderType = pOrderType
        mPrice = pPrice
        mOffset = pOffset
        mTriggerPrice = pTriggerPrice
        mTIF = pTIF
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Friend ReadOnly Property OrderRole() As OrderRole
        Get
            OrderRole = mOrderRole
        End Get
    End Property

    Friend ReadOnly Property OrderType() As Integer
        Get
            OrderType = mOrderType
        End Get
    End Property

    Friend ReadOnly Property Price() As Double
        Get
            Price = mPrice
        End Get
    End Property

    Friend ReadOnly Property Offset() As Integer
        Get
            Offset = mOffset
        End Get
    End Property

    Friend ReadOnly Property TriggerPrice() As Double
        Get
            TriggerPrice = mTriggerPrice
        End Get
    End Property

    Friend ReadOnly Property TIF() As Integer
        Get
            TIF = mTIF
        End Get
    End Property

#End Region

#Region "Methods"

#End Region

#Region "Helper Functions"
#End Region

End Class