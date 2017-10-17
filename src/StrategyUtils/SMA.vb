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


Public Module SMA

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "SMA"

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

    Public Function SMA(Optional Ref As Integer = 0, Optional SMAId As AutoTradingEnvironment.Study = Nothing) As Double
        SMA = CDbl(getSMA(SMAId).ma(Ref).Value)
    End Function

#End Region

#Region "Helper Functions"

    Private Function getSMA(SMA As AutoTradingEnvironment.Study) As CommonStudiesLib27.SMA
        If SMA Is Nothing Then SMA = CurrentResourceContext.PrimarySMA
        Assert(Not SMA Is Nothing, "No SMA currently defined")

        Dim lObj = SMA.Study
        AssertArgument(TypeOf lObj Is CommonStudiesLib27.SMA, "ResourceIdentifier does not refer to an SMA study")

        getSMA = DirectCast(lObj, CommonStudiesLib27.SMA)
    End Function
#End Region

End Module