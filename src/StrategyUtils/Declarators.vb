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

Public Module Declarators

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "Declarators"

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

    'Public Function DeclareChart(Timeframe As String) As ResourceIdentifier)
    '    Throw New NotSupportedException("Chart declaration not yet implemented")
    'End Function

    Public Function DeclareEntryOrder(OrderType As EntryOrderType,
                                      Optional Price As Double = 0,
                                      Optional Offset As Integer = 0,
                                      Optional TriggerPrice As Double = 0,
                                      Optional TimeInForce As OrderTIF = OrderTIF.Day) _
                    As OrderSpecifier
        Assert(Not CurrentTradingContext Is Nothing, "Method can only be called during strategy execution")

        Select Case OrderType
            Case EntryOrderType.Market, EntryOrderType.MarketOnOpen, EntryOrderType.MarketOnClose, EntryOrderType.MarketToLimit
                AssertArgument(Price = 0#, "Price must be 0.0 for this order type")
                AssertArgument(Offset = 0, "Offset must be 0 for this order type")
                AssertArgument(TriggerPrice = 0#, "TriggerPrice must be 0.0 for this order type")
            Case EntryOrderType.MarketIfTouched
                AssertArgument(Price = 0#, "Price must be 0.0 for this order type")
                AssertArgument(Offset = 0, "Offset must be 0 for this order type")
                AssertArgument(TriggerPrice <> 0#, "TriggerPrice must not be 0.0 for this order type")
            Case EntryOrderType.Bid, EntryOrderType.Ask, EntryOrderType.Last
                AssertArgument(Price = 0#, "Price must be 0.0 for this order type")
                AssertArgument(TriggerPrice = 0#, "TriggerPrice must be 0.0 for this order type")
            Case EntryOrderType.Limit, EntryOrderType.LimitOnOpen, EntryOrderType.LimitOnClose
                AssertArgument(TriggerPrice = 0#, "TriggerPrice must be 0.0 for this order type")
            Case EntryOrderType.LimitIfTouched, EntryOrderType.StopLimit
                AssertArgument(Price <> 0#, "Price must not be 0.0 for this order type")
                AssertArgument(TriggerPrice <> 0#, "TriggerPrice must not be 0.0 for this order type")
            Case EntryOrderType.Stop
                AssertArgument(Price = 0#, "Price must be 0.0 for this order type")
                AssertArgument(Offset = 0, "Offset must be 0 for this order type")
                AssertArgument(TriggerPrice <> 0#, "TriggerPrice must not be 0.0 for this order type")
            Case Else
                AssertArgument(False, "Invalid entry order type")
        End Select

        Select Case TimeInForce
            Case OrderTIF.Day
            Case OrderTIF.GoodTillCancelled
            Case OrderTIF.ImmediateOrCancel
            Case Else
                AssertArgument(False, "Invalid TimeInForce")
        End Select

        Return New OrderSpecifier(OrderRole.Entry, OrderType, Price, Offset, TriggerPrice, TimeInForce)
    End Function

    Public Function DeclareStopLossOrder(OrderType As StopLossOrderType,
                                         Optional Price As Double = 0.0,
                                         Optional Offset As Integer = 0,
                                         Optional TriggerPrice As Double = 0.0,
                                         Optional TimeInForce As OrderTIF = OrderTIF.GoodTillCancelled) _
                    As OrderSpecifier
        Assert(Not CurrentTradingContext Is Nothing, "Method can only be called during strategy execution")

        Select Case OrderType
            Case StopLossOrderType.Stop
                AssertArgument(Price = 0#, "Price must be 0.0 for this order type")
                AssertArgument(Offset = 0, "Offset must be 0 for this order type")
                AssertArgument(TriggerPrice <> 0#, "TriggerPrice must not be 0.0 for this order type")
            Case StopLossOrderType.StopLimit
                AssertArgument(Price <> 0#, "Price must not be 0.0 for this order type")
                AssertArgument(TriggerPrice <> 0#, "TriggerPrice must not be 0.0 for this order type")
            Case StopLossOrderType.Bid, StopLossOrderType.Ask, StopLossOrderType.Last, StopLossOrderType.Auto
                AssertArgument(Price = 0#, "Price must be 0.0 for this order type")
                AssertArgument(TriggerPrice = 0#, "TriggerPrice must be 0.0 for this order type")
            Case StopLossOrderType.None
                Price = 0.0
                Offset = 0
                TriggerPrice = 0.0
                TimeInForce = OrderTIF.None
            Case Else
                AssertArgument(False, "Invalid stop-loss order type")
        End Select

        Select Case TimeInForce
            Case OrderTIF.Day
            Case OrderTIF.GoodTillCancelled
            Case OrderTIF.ImmediateOrCancel
            Case OrderTIF.None
                AssertArgument(OrderType = StopLossOrderType.None, "Invalid TimeInForce")
            Case Else
                AssertArgument(False, "Invalid TimeInForce")
        End Select

        Return New OrderSpecifier(OrderRole.StopLoss, OrderType, Price, Offset, TriggerPrice, TimeInForce)
    End Function

    Public Function DeclareTargetOrder(OrderType As TargetOrderType,
                                       Optional Price As Double = 0,
                                       Optional Offset As Integer = 0,
                                       Optional TriggerPrice As Double = 0,
                                       Optional TimeInForce As OrderTIF = OrderTIF.GoodTillCancelled) _
                    As OrderSpecifier
        Assert(Not CurrentTradingContext Is Nothing, "Method can only be called during strategy execution")

        Select Case OrderType
            Case TargetOrderType.Limit
                AssertArgument(TriggerPrice = 0#, "TriggerPrice must be 0.0 for this order type")
            Case TargetOrderType.LimitIfTouched
                AssertArgument(Price <> 0#, "Price must not be 0.0 for this order type")
                AssertArgument(TriggerPrice <> 0#, "TriggerPrice must not be 0.0 for this order type")
            Case TargetOrderType.MarketIfTouched
                AssertArgument(Price = 0#, "Price must be 0.0 for this order type")
                AssertArgument(Offset = 0, "Offset must be 0 for this order type")
                AssertArgument(TriggerPrice <> 0#, "TriggerPrice must not be 0.0 for this order type")
            Case TargetOrderType.Bid, TargetOrderType.Ask, TargetOrderType.Last, TargetOrderType.Auto
                AssertArgument(Price = 0#, "Price must be 0.0 for this order type")
                AssertArgument(TriggerPrice = 0#, "TriggerPrice must be 0.0 for this order type")
            Case TargetOrderType.None
                Price = 0.0
                Offset = 0
                TriggerPrice = 0.0
                TimeInForce = OrderTIF.None
            Case Else
                AssertArgument(False, "Invalid target order type")
        End Select

        Select Case TimeInForce
            Case OrderTIF.Day
            Case OrderTIF.GoodTillCancelled
            Case OrderTIF.ImmediateOrCancel
            Case OrderTIF.None
                AssertArgument(OrderType = TargetOrderType.None, "Invalid TimeInForce")
            Case Else
                AssertArgument(False, "Invalid TimeInForce")
        End Select

        Return New OrderSpecifier(OrderRole.Target, OrderType, Price, Offset, TriggerPrice, TimeInForce)
    End Function

    Public Function DeclareOrderContext(Name As String) As OrderContext
        Assert(Not CurrentInitialisationContext Is Nothing, "Method can only be called during strategy initialisation")

        StrategyLogger.Log(TWUtilities40.LogLevels.LogLevelNormal, $"Declare OrderContext: {Name}")

        Dim oc As OrderUtils27.OrderContext
        oc = CurrentInitialisationContext.PositionManager.OrderContexts.Add(Name)
        If Not CurrentInitialisationContext.AllowUnprotectedPositions Then oc.PreventUnprotectedPositions = True

        DeclareOrderContext = New OrderContext(oc)
    End Function

    Public Function DeclareSimulatedOrderContext(Name As String) As OrderContext
        Assert(Not CurrentInitialisationContext Is Nothing, "Method can only be called during strategy initialisation")

        StrategyLogger.Log(TWUtilities40.LogLevels.LogLevelNormal, $"Declare Simulated OrderContext: {Name}")

        Dim oc As OrderUtils27.OrderContext
        oc = CurrentInitialisationContext.PositionManagerSimulated.OrderContexts.Add(Name)

        DeclareSimulatedOrderContext = New OrderContext(oc)
    End Function

    Public Function DeclareStudy(Name As String,
                                 BasedOn As AutoTradingEnvironment.Study,
                                 Optional Parameters As String = "",
                                 Optional IncludeBarsOutsideSession As Boolean = False,
                                 Optional NumberOfValuesToCache As Integer = 3,
                                 Optional InputNames As String = "") _
                    As AutoTradingEnvironment.Study
        Assert(Not CurrentInitialisationContext Is Nothing, "Method can only be called during strategy initialisation")

        Return doDeclareStudy(Name, BasedOn.Study, Parameters, IncludeBarsOutsideSession, NumberOfValuesToCache, InputNames)
    End Function

    Public Function DeclareStudy(Name As String,
                                 Optional BasedOn As AutoTradingEnvironment.Timeframe = Nothing,
                                 Optional Parameters As String = "",
                                 Optional IncludeBarsOutsideSession As Boolean = False,
                                 Optional NumberOfValuesToCache As Integer = 3,
                                 Optional InputNames As String = "") _
                    As AutoTradingEnvironment.Study
        Assert(Not CurrentInitialisationContext Is Nothing, "Method can only be called during strategy initialisation")

        If BasedOn Is Nothing Then BasedOn = CurrentResourceContext.PrimaryTimeframe
        Return doDeclareStudy(Name, BasedOn.Timeframe, Parameters, IncludeBarsOutsideSession, NumberOfValuesToCache, InputNames)
    End Function

    Public Function DeclareTimeframe(BarLength As Integer,
                                     Optional BarUnit As String = "minutes",
                                     Optional NumberOfBars As Integer = 500,
                                     Optional IncludeBarsOutsideSession As Boolean = False,
                                     Optional ShowInChart As Boolean = True) As AutoTradingEnvironment.Timeframe
        Assert(Not CurrentInitialisationContext Is Nothing, "Method can only be called during strategy initialisation")

        Dim tp = TWUtilities.GetTimePeriod(BarLength, TWUtilities.TimePeriodUnitsFromString(BarUnit))
        StrategyLogger.Log(TWUtilities40.LogLevels.LogLevelNormal, $"Declare Timeframe: {tp.ToString}(Number of bars={NumberOfBars}; Session only={Not IncludeBarsOutsideSession}; Show in chart={ShowInChart})")

        Dim tf = CurrentInitialisationContext.AddTimeframe(tp, NumberOfBars, IncludeBarsOutsideSession, ShowInChart)

        DeclareTimeframe = New AutoTradingEnvironment.Timeframe(tf)
        If CurrentResourceContext.PrimaryTimeframe Is Nothing Then CurrentResourceContext.PrimaryTimeframe = DeclareTimeframe
    End Function

#End Region

#Region "Helper Functions"

    Public Function doDeclareStudy(Name As String,
                                 BasedOn As Object,
                                 Optional Parameters As String = "",
                                 Optional IncludeBarsOutsideSession As Boolean = False,
                                 Optional NumberOfValuesToCache As Integer = 3,
                                 Optional InputNames As String = "") _
                    As Study
        StrategyLogger.Log(TWUtilities40.LogLevels.LogLevelNormal, $"Declare Study: {Name}({Parameters})")
        If InputNames = "" Then InputNames = StudyUtils.DefaultStudyValueName

        Dim lInputNames() = Split(InputNames, ",")

        Dim lBaseStudy As StudyUtils27._IStudy = Nothing

        If TypeOf BasedOn Is StudyUtils27._IStudy Then
            lBaseStudy = DirectCast(BasedOn, StudyUtils27._IStudy)
        ElseIf TypeOf BasedOn Is TimeframeUtils27.Timeframe Then
            Dim tf = DirectCast(BasedOn, TimeframeUtils27.Timeframe)
            lBaseStudy = DirectCast(tf.BarStudy, StudyUtils27._IStudy)
        Else
            AssertArgument(False, NameOf(BasedOn), "Specified value is not a study or a timeframe")
        End If

        Dim lLibraryName = String.Empty
        Dim lStudyName As String
        Dim p = InStr(1, Name, "\")
        If p = 0 Then
            lStudyName = Name
        Else
            AssertArgument(p <> 1, "Study name cannot start with '\'")
            lLibraryName = Left(Name, p - 1)
            lStudyName = Right(Name, Len(Name) - p)
        End If

        Dim lStudy = CurrentInitialisationContext.AddStudy(lStudyName,
                                                 lBaseStudy,
                                                 lInputNames,
                                                 IncludeBarsOutsideSession,
                                                 TWUtilities.CreateParametersFromString(Parameters.Replace(vbCrLf, "")),
                                                 NumberOfValuesToCache,
                                                 lLibraryName)

        doDeclareStudy = New Study(lStudy)
        CurrentResourceContext.SetPrimaryStudyOfType(doDeclareStudy)
    End Function

#End Region

End Module