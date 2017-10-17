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

Imports TradeWright.Trading.Utils.Contracts
Imports TradeWright.Trading.Utils.Sessions

Imports TWUtilities40

Public NotInheritable Class DefaultStrategyHostModel
    Implements IStrategyHostModel

#Region "Interfaces"


#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "DefaultStrategyHostModel"

#End Region

#Region "Member variables"

#End Region

#Region "Constructors"

#End Region

#Region "IStrategyHostModel Interface Members"

    Public Property Clock As Clock Implements IStrategyHostModel.Clock

    Public Property Session As Session Implements IStrategyHostModel.Session

    Public Property Contract() As IContract Implements IStrategyHostModel.Contract

    Public Property ContractStorePrimary() As IContractStore Implements IStrategyHostModel.ContractStorePrimary

    Public Property ContractStoreSecondary() As IContractStore Implements IStrategyHostModel.ContractStoreSecondary

    Public Property HistoricalDataStoreInput() As HistDataUtils27.IHistoricalDataStore Implements IStrategyHostModel.HistoricalDataStoreInput

    Public Property IsTickReplay() As Boolean Implements IStrategyHostModel.IsTickReplay

    Public Property LogDummyProfitProfile() As Boolean Implements IStrategyHostModel.LogDummyProfitProfile

    Public Property LogParameters() As Boolean Implements IStrategyHostModel.LogParameters

    Public Property LogProfitProfile() As Boolean Implements IStrategyHostModel.LogProfitProfile

    Public Property OrderSubmitterFactoryLive() As OrderUtils27.IOrderSubmitterFactory Implements IStrategyHostModel.OrderSubmitterFactoryLive

    Public Property OrderSubmitterFactorySimulated() As OrderUtils27.IOrderSubmitterFactory Implements IStrategyHostModel.OrderSubmitterFactorySimulated

    Public Property RealtimeTickers() As TickerUtils27.Tickers Implements IStrategyHostModel.RealtimeTickers

    Public Property ResultsPath() As String Implements IStrategyHostModel.ResultsPath

    Public Property SeparateSessions() As Boolean Implements IStrategyHostModel.SeparateSessions

    Public Property ShowChart() As Boolean Implements IStrategyHostModel.ShowChart

    Public Property StopStrategyFactoryClassName() As String Implements IStrategyHostModel.StopStrategyFactoryClassName

    Public Property StrategyClassName() As String Implements IStrategyHostModel.StrategyClassName

    Public Property StudyLibraryManager() As StudyUtils27.StudyLibraryManager Implements IStrategyHostModel.StudyLibraryManager

    Public Property Symbol() As IContractSpecifier Implements IStrategyHostModel.Symbol

    Public Property Ticker() As TickerUtils27.Ticker Implements IStrategyHostModel.Ticker

    Public Property TickFileSpecifiers() As TickfileUtils27.TickFileSpecifiers Implements IStrategyHostModel.TickFileSpecifiers

    Public Property TickfileStoreInput() As TickfileUtils27.ITickfileStore Implements IStrategyHostModel.TickfileStoreInput

    Public Property UseLiveBroker() As Boolean Implements IStrategyHostModel.UseLiveBroker

    Public Property UseMoneyManagement() As Boolean Implements IStrategyHostModel.UseMoneyManagement

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

#End Region

#Region "Helper Functions"
#End Region

End Class