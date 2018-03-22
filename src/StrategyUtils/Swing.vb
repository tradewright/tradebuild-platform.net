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

Public Module Swing

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "Swing"

    Public Const ValueSwingHighLine As String = "Swing high line"
    Public Const ValueSwingLowLine As String = "Swing low line"
    Public Const ValueSwingLine As String = "Swing line"
    Public Const ValueSwingPoint As String = "Swing point"
    Public Const ValueSwingHighPoint As String = "Swing high point"
    Public Const ValueSwingLowPoint As String = "Swing low point"


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

    Public Function PotentialSwingHigh(Optional Swing As AutoTradingEnvironment.Study = Nothing) As Double
        PotentialSwingHigh = CDbl(getSwing(Swing).PotentialSwingHighPoint.Value)
    End Function

    Public Function PotentialSwingHighBarNumber(Optional Swing As AutoTradingEnvironment.Study = Nothing) As Integer
        PotentialSwingHighBarNumber = getSwing(Swing).PotentialSwingHighPoint.BarNumber
    End Function

    Public Function PotentialSwingHighTime(Optional Swing As AutoTradingEnvironment.Study = Nothing) As Date
        PotentialSwingHighTime = getSwing(Swing).PotentialSwingHighPoint.Timestamp
    End Function

    Public Function PotentialSwingLow(Optional Swing As AutoTradingEnvironment.Study = Nothing) As Double
        PotentialSwingLow = CDbl(getSwing(Swing).PotentialSwingLowPoint.Value)
    End Function

    Public Function PotentialSwingLowBarNumber(Optional Swing As AutoTradingEnvironment.Study = Nothing) As Integer
        PotentialSwingLowBarNumber = getSwing(Swing).PotentialSwingLowPoint.BarNumber
    End Function

    Public Function PotentialSwingLowTime(Optional Swing As AutoTradingEnvironment.Study = Nothing) As Date
        PotentialSwingLowTime = getSwing(Swing).PotentialSwingLowPoint.Timestamp
    End Function

    Public Function SwingHigh(Optional Ref As Integer = 0, Optional Swing As AutoTradingEnvironment.Study = Nothing) As Double
        SwingHigh = CDbl(getSwing(Swing).SwingHighPoint(Ref).Value)
    End Function

    Public Function SwingHighBarNumber(Optional Ref As Integer = 0, Optional Swing As AutoTradingEnvironment.Study = Nothing) As Integer
        SwingHighBarNumber = getSwing(Swing).SwingHighPoint(Ref).BarNumber
    End Function

    Public Function SwingHighTime(Optional Ref As Integer = 0, Optional Swing As AutoTradingEnvironment.Study = Nothing) As Date
        SwingHighTime = getSwing(Swing).SwingHighPoint(Ref).Timestamp
    End Function

    Public Function SwingLow(Optional Ref As Integer = 0, Optional Swing As AutoTradingEnvironment.Study = Nothing) As Double
        SwingLow = CDbl(getSwing(Swing).SwingLowPoint(Ref).Value)
    End Function

    Public Function SwingLowBarNumber(Optional Ref As Integer = 0, Optional Swing As AutoTradingEnvironment.Study = Nothing) As Integer
        SwingLowBarNumber = getSwing(Swing).SwingLowPoint(Ref).BarNumber
    End Function

    Public Function SwingLowTime(Optional Ref As Integer = 0, Optional Swing As AutoTradingEnvironment.Study = Nothing) As Date
        SwingLowTime = getSwing(Swing).SwingLowPoint(Ref).Timestamp
    End Function

#End Region

#Region "Helper Functions"

    Private Function getSwing(Swing As AutoTradingEnvironment.Study) As CommonStudiesLib27.Swing
        If Swing Is Nothing Then Swing = CurrentResourceContext.PrimarySwing
        Assert(Not Swing Is Nothing, "No Swing is currently defined")

        Dim lObj = Swing.Study
        AssertArgument(TypeOf lObj Is CommonStudiesLib27.Swing, "Swing does not refer to a Swing study")

        getSwing = DirectCast(lObj, CommonStudiesLib27.Swing)
    End Function
#End Region

End Module