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


Option Explicit On
Public NotInheritable Class ResourceContext

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "ResourceContext"

#End Region

#Region "Member variables"

    Private mPrimaryBracketOrder As AutoTradingEnvironment.BracketOrder
    Private mPrimaryBollingerBands As Study
    Private mPrimaryMACD As AutoTradingEnvironment.Study
    Private mPrimarySMA As AutoTradingEnvironment.Study
    Private mPrimarySwing As AutoTradingEnvironment.Study
    Private mPrimaryTimeframe As AutoTradingEnvironment.Timeframe

#End Region

#Region "Constructors"

    Friend Sub New()
    End Sub

    Friend Sub New(o As ResourceContext)
        mPrimaryTimeframe = o.PrimaryTimeframe
        mPrimaryBollingerBands = o.PrimaryBollingerBands
        mPrimaryMACD = o.PrimaryMACD
        mPrimarySMA = o.PrimarySMA
        mPrimarySwing = o.PrimarySwing
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Friend ReadOnly Property PrimaryBollingerBands() As AutoTradingEnvironment.Study
        Get
            Return mPrimaryBollingerBands
        End Get
    End Property

    Friend ReadOnly Property PrimaryBracketOrder() As AutoTradingEnvironment.BracketOrder
        Get
            Return mPrimaryBracketOrder
        End Get
    End Property

    Friend ReadOnly Property PrimaryMACD() As AutoTradingEnvironment.Study
        Get
            Return mPrimaryMACD
        End Get
    End Property

    Friend ReadOnly Property PrimarySMA() As AutoTradingEnvironment.Study
        Get
            Return mPrimarySMA
        End Get
    End Property

    Friend ReadOnly Property PrimarySwing() As AutoTradingEnvironment.Study
        Get
            Return mPrimarySwing
        End Get
    End Property

    Friend Property PrimaryTimeframe() As AutoTradingEnvironment.Timeframe
        Get
            Return mPrimaryTimeframe
        End Get
        Set
            mPrimaryTimeframe = Value
        End Set
    End Property

#End Region

#Region "Methods"

    Friend Sub ClearPrimaryBracketOrder()
        mPrimaryBracketOrder = Nothing
    End Sub

    Friend Sub SetPrimaryBracketOrder(BracketOrder As AutoTradingEnvironment.BracketOrder)
        mPrimaryBracketOrder = BracketOrder
    End Sub

    Friend Sub SetPrimaryStudyOfType(pStudy As Study)
        If TypeOf pStudy.Study Is CommonStudiesLib27.BollingerBands Then
            If PrimaryBollingerBands Is Nothing Then mPrimaryBollingerBands = pStudy
        ElseIf TypeOf pStudy.Study Is CommonStudiesLib27.MACD Then
            If mPrimaryMACD Is Nothing Then mPrimaryMACD = pStudy
        ElseIf TypeOf pStudy.Study Is CommonStudiesLib27.SMA Then
            If mPrimarySMA Is Nothing Then mPrimarySMA = pStudy
        ElseIf TypeOf pStudy.Study Is CommonStudiesLib27.Swing Then
            If mPrimarySwing Is Nothing Then mPrimarySwing = pStudy
        End If
    End Sub

#End Region

#Region "Helper Functions"
#End Region

End Class