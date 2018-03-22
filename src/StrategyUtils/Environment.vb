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
Imports TimeframeUtils27

Imports TradeWright.Trading.Utils.Contracts

Public Module Environment

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "Environment"

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

    Public ReadOnly Property ContractIdentifier() As String
        Get
            ContractIdentifier = getContract.Specifier.ToString
        End Get
    End Property

    Public ReadOnly Property MinutesToEndOfTradingSession() As Double
        Get
            MinutesToEndOfTradingSession = (CurrentTradingContext.Session.CurrentSessionEndTime - Timestamp).TotalMinutes
        End Get
    End Property

    Public ReadOnly Property TickSize() As Double
        Get
            TickSize = getContract.TickSize
        End Get
    End Property

    Public ReadOnly Property TickValue() As Double
        Get
            TickValue = getContract.TickValue
        End Get
    End Property

    Public ReadOnly Property Timestamp() As Date
        Get
            Timestamp = CurrentTradingContext.Timestamp
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub Assert(condition As Boolean, Optional message As String = "")
        If Not condition Then Throw New InvalidOperationException(message)
    End Sub

    Public Sub AssertArgument(condition As Boolean, Optional message As String = "", Optional paramName As String = "")
        If Not condition Then Throw New ArgumentException(message, paramName)
    End Sub

    Public Sub ChartStudyValue(Study As AutoTradingEnvironment.Study, ValueName As String, Optional Timeframe As AutoTradingEnvironment.Timeframe = Nothing)
        Assert(CurrentInitialisationContext IsNot Nothing, "Method can only be called during strategy initialisation")

        If Timeframe Is Nothing Then Timeframe = CurrentResourceContext.PrimaryTimeframe

        CurrentInitialisationContext.ChartStudyValue(Study.Study, ValueName, Timeframe.Timeframe)
    End Sub

    Public Sub DefineBooleanParameter(Name As String, Optional DefaultValue As Boolean = False)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineDateParameter(Name As String)
        DefineStringParameter(Name, CStr(Date.MinValue))
    End Sub

    Public Sub DefineDateParameter(Name As String, DefaultValue As Date)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineDecimalParameter(Name As String, Optional DefaultValue As Decimal = 0.0@)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineDoubleParameter(Name As String, Optional DefaultValue As Double = 0.0#)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineIntegerParameter(Name As String, Optional DefaultValue As Integer = 0)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineLongParameter(Name As String, Optional DefaultValue As Long = 0)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineShortParameter(Name As String, Optional DefaultValue As Short = 0)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineSingleParameter(Name As String, Optional DefaultValue As Single = 0.0)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineStringParameter(Name As String, Optional DefaultValue As String = "")
        Assert(CurrentStrategyRunner.DefaultParameters IsNot Nothing, "Can't define parameters at this point")
        AssertArgument(Name <> "", "Parameter name must be supplied")

        CurrentStrategyRunner.DefaultParameters.SetParameterValue(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineUnsignedIntegerParameter(Name As String, Optional DefaultValue As UInteger = 0)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Sub DefineUnsignedLongParameter(Name As String, Optional DefaultValue As ULong = 0)
        DefineStringParameter(Name, CStr(DefaultValue))
    End Sub

    Public Function GetBooleanParameterValue(Name As String, Optional DefaultValue As Boolean = False) As Boolean
        Return CBool(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetDateParameterValue(Name As String) As Date
        Return CDate(GetStringParameterValue(Name, CStr(Date.MinValue)))
    End Function

    Public Function GetDateParameterValue(Name As String, DefaultValue As Date) As Date
        Return CDate(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetDecimalParameterValue(Name As String, Optional DefaultValue As Decimal = 0.0@) As Decimal
        Return CDec(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetDoubleParameterValue(Name As String, Optional DefaultValue As Double = 0.0#) As Double
        Return CDbl(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetIntegerParameterValue(Name As String, Optional DefaultValue As Integer = 0) As Integer
        Return CInt(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetLongParameterValue(Name As String, Optional DefaultValue As Long = 0L) As Long
        Return CLng(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetShortParameterValue(Name As String, Optional DefaultValue As Short = 0) As Short
        Return CShort(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetSingleParameterValue(Name As String, Optional DefaultValue As Single = 0.0F) As Single
        Return CSng(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetStringParameterValue(Name As String, Optional DefaultValue As String = "") As String
        Assert(Not CurrentInitialisationContext Is Nothing Or CurrentTradingContext Is Nothing, "Can't get parameters at this point")
        AssertArgument(Name <> "", "Parameter name must be supplied")

        GetStringParameterValue = CurrentStrategyRunner.Parameters.GetParameterValue(Name, DefaultValue)
    End Function

    Public Function GetUnsignedIntegerParameterValue(Name As String, Optional DefaultValue As Integer = 0) As UInteger
        Return CUInt(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Function GetUnsignedLongParameterValue(Name As String, Optional DefaultValue As Long = 0L) As ULong
        Return CULng(GetStringParameterValue(Name, CStr(DefaultValue)))
    End Function

    Public Sub LogTradeMessage(Message As String)
        CurrentTradingContext.LogTradeMessage(Message)
    End Sub

#End Region

#Region "Helper Functions"

    Private Function getContract() As IContract
        getContract = Nothing
        If CurrentInitialisationContext IsNot Nothing Then
            getContract = CurrentInitialisationContext.Contract
        ElseIf CurrentTradingContext IsNot Nothing Then
            getContract = CurrentTradingContext.Contract
        Else
            Assert(False, "No contract available at this point")
        End If
    End Function

#End Region

End Module