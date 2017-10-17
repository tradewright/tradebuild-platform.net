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


Imports StudyUtils27

Public Module BollingerBands

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "BollingerBands"

    Public Const ValueBottom As String = "Bottom"
    Public Const ValueCentre As String = "Centre"
    Public Const ValueSpread As String = "Spread"
    Public Const ValueTop As String = "Top"

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

    Public Function BollingerBottom(Optional Ref As Integer = 0, Optional BollingerBands As AutoTradingEnvironment.Study = Nothing) As Double
        Return CDbl(getBB(BollingerBands).Bottom(Ref).Value)
    End Function

    Public Function BollingerCentre(Optional Ref As Integer = 0, Optional BollingerBands As AutoTradingEnvironment.Study = Nothing) As Double
        Return CDbl(getBB(BollingerBands).Centre(Ref).Value)
    End Function

    Public Function BollingerInBottomBand(Value As Double, Optional Ref As Integer = 0, Optional BollingerBands As AutoTradingEnvironment.Study = Nothing) As Boolean
        Return CBool(getBB(BollingerBands).InBottomBand(Value, Ref).Value)
    End Function

    Public Function BollingerInCentreBand(Value As Double, Optional Ref As Integer = 0, Optional BollingerBands As AutoTradingEnvironment.Study = Nothing) As Boolean
        Return CBool(getBB(BollingerBands).InCentreBand(Value, Ref).Value)
    End Function

    Public Function BollingerInTopBand(Value As Double, Optional Ref As Integer = 0, Optional BollingerBands As AutoTradingEnvironment.Study = Nothing) As Boolean
        Return CBool(getBB(BollingerBands).InTopBand(Value, Ref).Value)
    End Function

    Public Function BollingerSpread(Optional Ref As Integer = 0, Optional BollingerBands As AutoTradingEnvironment.Study = Nothing) As Double
        Return CDbl(getBB(BollingerBands).Spread(Ref).Value)
    End Function

    Public Function BollingerTop(Optional Ref As Integer = 0, Optional BollingerBands As AutoTradingEnvironment.Study = Nothing) As Double
        Return CDbl(getBB(BollingerBands).Top(Ref).Value)
    End Function

#End Region

#Region "Helper Functions"

    Private Function getBB(BB As AutoTradingEnvironment.Study) As CommonStudiesLib27.BollingerBands
        If BB Is Nothing Then BB = CurrentResourceContext.PrimaryBollingerBands
        Assert(Not BB Is Nothing, "No BollingerBands currently defined")

        AssertArgument(TypeOf BB.Study Is CommonStudiesLib27.BollingerBands, "Study does not refer to a BollingerBands study")

        Return DirectCast(BB.Study, CommonStudiesLib27.BollingerBands)
    End Function
#End Region

End Module