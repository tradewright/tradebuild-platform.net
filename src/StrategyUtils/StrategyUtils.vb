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

Friend Module StrategyUtils

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

#End Region

#Region "Member variables"

    Friend StudyUtils As New StudyUtils27.StudyUtils
    Friend TickerUtils As New TickerUtils27.TickerUtils
    Friend TWUtilities As New TWUtilities40.TWUtilities
    Friend OrderUtils As New OrderUtils27.OrderUtils
    Friend BarUtils As New BarUtils27.BarUtils
    Friend MarketDataUtils As New MarketDataUtils27.MarketDataUtils

#End Region

#Region "Constructors"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Friend ReadOnly Property Logger() As TWUtilities40.Logger
        Get
            Static sLogger As TWUtilities40.Logger
            If sLogger Is Nothing Then sLogger = TWUtilities.GetLogger("strategyutils")
            Return sLogger
        End Get
    End Property

    Friend ReadOnly Property StrategyLogger() As TWUtilities40.Logger
        Get
            Static sLogger As TWUtilities40.Logger
            If sLogger Is Nothing Then sLogger = TWUtilities.GetLogger("strategy")
            Return sLogger
        End Get
    End Property

    Friend ReadOnly Property TradeLogger() As TWUtilities40.Logger
        Get
            Static sLogger As TWUtilities40.Logger
            If sLogger Is Nothing Then
                sLogger = TWUtilities.GetLogger("strategy.trademessage")
                sLogger.LogToParent = True
            End If
            Return sLogger
        End Get
    End Property

#End Region

#Region "Methods"

#End Region

#Region "Helper Functions"
#End Region

End Module