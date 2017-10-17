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

Imports TimeframeUtils27

Public Module Bars

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "Bars"

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

    Public Function BarNumber(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Integer
        BarNumber = getBars(Timeframe).Bar(Ref).BarNumber
    End Function

    Public Function CloseValue(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        CloseValue = getBars(Timeframe).CloseValue(Ref)
    End Function

    Public Function CurrentBarNumber(Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Integer
        CurrentBarNumber = getBars(Timeframe).CurrentBarNumber
    End Function

    Public Function HigherHighValue(Rank As Integer, Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        HigherHighValue = getBars(Timeframe).HigherHighValue(Rank, Ref)
    End Function

    Public Function HighestCloseValue(NumberOfBars As Integer, Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        HighestCloseValue = getBars(Timeframe).HighestCloseValue(NumberOfBars, Ref)
    End Function

    Public Function HighestHighValue(NumberOfBars As Integer, Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        HighestHighValue = CDbl(getBars(Timeframe).HighestHighValue(NumberOfBars, Ref))
    End Function

    Public Function HighValue(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        HighValue = getBars(Timeframe).HighValue(Ref)
    End Function

    Public Function IsDownBar(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Boolean
        IsDownBar = getBars(Timeframe).Down(Ref)
    End Function

    Public Function IsInsideBar(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Boolean
        IsInsideBar = getBars(Timeframe).Inside(Ref)
    End Function

    Public Function IsOutsideBar(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Boolean
        IsOutsideBar = getBars(Timeframe).Outside(Ref)
    End Function

    Public Function IsUpBar(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Boolean
        IsUpBar = getBars(Timeframe).Up(Ref)
    End Function

    Public Function LowerLowValue(Rank As Integer, Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        LowerLowValue = getBars(Timeframe).LowerLowValue(Rank, Ref)
    End Function

    Public Function LowestCloseValue(NumberOfBars As Integer, Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        LowestCloseValue = getBars(Timeframe).LowestCloseValue(NumberOfBars, Ref)
    End Function

    Public Function LowestLowValue(NumberOfBars As Integer, Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        LowestLowValue = getBars(Timeframe).LowestLowValue(NumberOfBars, Ref)
    End Function

    Public Function LowValue(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        LowValue = getBars(Timeframe).LowValue(Ref)
    End Function

    Public Function NumberOfBars(Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Integer
        NumberOfBars = getBars(Timeframe).Count
    End Function

    Public Function OpenValue(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Double
        OpenValue = getBars(Timeframe).OpenValue(Ref)
    End Function

    Public Function TickVolume(Optional Ref As Integer = 0, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing) As Integer
        TickVolume = getBars(Timeframe).TickVolume(Ref)
    End Function

#End Region

#Region "Helper Functions"

    Private Function getBars(Optional timeframe As AutoTradingEnvironment.Timeframe = Nothing) As BarUtils27.Bars
        If timeframe Is Nothing Then timeframe = CurrentResourceContext.PrimaryTimeframe
        Assert(Not timeframe Is Nothing, "No Timeframe is currently defined")

        Dim tf = timeframe.Timeframe
        Return DirectCast(tf.BarStudy.BarsFuture.Value, BarUtils27.Bars)
    End Function
#End Region

End Module