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


Imports CommonStudiesLib27
Imports StudyUtils27

Public Module MACD

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "MACD"

    Public Const ValueMACD As String = "MACD"
    Public Const ValueMACDHist As String = "MACD hist"
    Public Const ValueMACDLowerBalance As String = "MACD lower balance"
    Public Const ValueMACDSignal As String = "MACD signal"
    Public Const ValueStrength As String = "Strength"
    Public Const ValueStrengthCount As String = "Strength count"
    Public Const ValueMACDUpperBalance As String = "MACD upper balance"

#End Region

#Region "Member variables"

#End Region

#Region "Constructors"

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Public Function MACDHist(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Double
        MACDHist = CDbl(getMACD(MACD).MACDHist(Ref).Value)
    End Function

    Public Function MACDHistDown(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDHistDown = CBool(getMACD(MACD).MACDHistDown(Ref).Value)
    End Function

    Public Function MACDHistUp(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDHistUp = CBool(getMACD(MACD).MACDHistUp(Ref).Value)
    End Function

    Public Function MACDNoStrength(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDNoStrength = CBool(CType(getMACD(MACD).strength(Ref).Value, MACDStrengths) = MACDStrengths.MACDNoStrength)
    End Function

    Public Function MACDSignal(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Double
        MACDSignal = CDbl(getMACD(MACD).MACDSignal(Ref).Value)
    End Function

    Public Function MACDStrengthConfirmed(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDStrengthConfirmed = CBool(CType(getMACD(MACD).strength(Ref).Value, MACDStrengths) = MACDStrengths.MACDConfirmedStrength)
    End Function

    Public Function MACDStrengthCount(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Double
        MACDStrengthCount = CDbl(getMACD(MACD).strengthCount(Ref).Value)
    End Function

    Public Function MACDStrengthPotential(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDStrengthPotential = CBool(CType(getMACD(MACD).strength(Ref).Value, MACDStrengths) = MACDStrengths.MACDPotentialStrength)
    End Function

    Public Function MACDValue(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Double
        MACDValue = CDbl(getMACD(MACD).MACD(Ref).Value)
    End Function

    Public Function MACDValueDown(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDValueDown = CBool(getMACD(MACD).MACDDown(Ref).Value)
    End Function

    Public Function MACDValueUp(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDValueUp = CBool(getMACD(MACD).MACDUp(Ref).Value)
    End Function

    Public Function MACDWeaknessConfirmed(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDWeaknessConfirmed = CBool(CType(getMACD(MACD).strength(Ref).Value, MACDStrengths) = MACDStrengths.MACDConfirmedWeakness)
    End Function

    Public Function MACDWeaknessPotential(Optional Ref As Integer = 0, Optional MACD As AutoTradingEnvironment.Study = Nothing) As Boolean
        MACDWeaknessPotential = CBool(CType(getMACD(MACD).strength(Ref).Value, MACDStrengths) = MACDStrengths.MACDPotentialWeakness)
    End Function

#End Region

#Region "Helper Functions"

    Private Function getMACD(MACD As AutoTradingEnvironment.Study) As CommonStudiesLib27.MACD
        If MACD Is Nothing Then MACD = CurrentResourceContext.PrimaryMACD
        Assert(MACD IsNot Nothing, "No MACD is currently defined")

        Dim lObj = MACD.Study
        AssertArgument(TypeOf lObj Is CommonStudiesLib27.MACD, "Study does not refer to a MACD study")

        getMACD = DirectCast(lObj, CommonStudiesLib27.MACD)
    End Function
#End Region

End Module