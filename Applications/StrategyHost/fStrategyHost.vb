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

Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection

Imports ChartSkil27
Imports OrderUtils27
Imports StudyUtils27
Imports TickfileUtils27
Imports TickUtils27
Imports TimeframeUtils27
Imports TradingUI27
Imports TWControls40
Imports TWUtilities40

Imports TradeWright.Trading.TradeBuild.AutoTradingEnvironment
Imports TradeWright.Trading.Utils.Charts
Imports TradeWright.Trading.Utils.Contracts
Imports TradeWright.Trading.Utils.Sessions
Imports TradeWright.Utilities.Time
Imports TradeWright.Trading.UI.Trading

Friend Class fStrategyHost
    Inherits Form

#Region "Interfaces"

    Implements IStrategyHostView

#End Region

#Region "Events"

#End Region

#Region "Constants"

#End Region

#Region "Enums"

    Private Enum BOListColumns
        ColumnKey = 0
        ColumnStartTime
        ColumnEndTime
        ColumnAction
        ColumnQuantity
        ColumnEntryPrice
        ColumnExitPrice
        ColumnProfit
        ColumnMaxProfit
        ColumnMaxLoss
        ColumnRisk
        ColumnEntryReason
        ColumnTargetReason
        ColumnStopReason
        ColumnDescription
    End Enum

    ' Character widths of the bracket order list columns
    Private Enum BOListColumnWidths
        WidthKey = 10 + 2
        WidthStartTime = 20 + 2
        WidthEndTime = 20 + 2
        WidthDescription = 50 + 2
        WidthAction = 5 + 2
        WidthQuantity = 5 + 2
        WidthQuantityOutstanding = 5 + 2
        WidthEntryPrice = 8 + 2
        WidthExitPrice = 8 + 2
        WidthProfit = 8 + 2
        WidthMaxProfit = 8 + 2
        WidthMaxLoss = 8 + 2
        WidthRisk = 8 + 2
        WidthEntryReason = 7 + 2
        WidthTargetReason = 7 + 2
        WidthStopReason = 7 + 2
        WidthClosedOut = 4 + 2
    End Enum

#End Region

#Region "Types"

    Friend Class ChartInfo
        Friend Chart As UI.Trading.MarketChart
        Friend BracketOrderLineSeries As LineSeries
    End Class

#End Region

#Region "Member variables"

    Private mModel As IStrategyHostModel
    Private mController As IStrategyHostController

    Private mContract As IContract
    Private mSecType As SecurityType
    Private mTickSize As Double

    Private WithEvents mSession As TradeWright.Trading.Utils.Sessions.Session
    Private mSessionInProgress As Boolean
    Private mTradingPlatformStarted As Boolean

    Private mParams As Parameters

    Private mProfitStudyBase As StudyBaseForDoubleInput

    Private mPriceMarketCharts As New List(Of ChartInfo)

    Private mTradeStudyBase As StudyBaseForDoubleInput

    Private mPosition As Integer
    Private mOverallProfit As Double
    Private mSessionProfit As Double
    Private mMaxProfit As Double
    Private mDrawdown As Double

    Private mDetailsHidden As Boolean

    Private mTheme As ITheme

    Private mChartStyle As ChartStyle

    Private mPriceFormatter As New Utils.Contracts.PriceFormatter()

#End Region

#Region "Form Event Handlers"

    Private Sub Form_Load(eventSender As System.Object, eventArgs As System.EventArgs) Handles MyBase.Load
        mChartStyle = CreateChartStyle()
        TWUtilities.LogMessage("Form loaded")

        ChartControlToolstrip.Chart = PriceChart.CurrentChart

        Me.Text = GetAppTitle()
    End Sub

    Private Sub Form_Resize(eventSender As System.Object, eventArgs As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then Exit Sub

        If ClientRectangle.Height < minimumHeight() Or mDetailsHidden Then
            Me.WindowState = FormWindowState.Normal
            Me.Height = minimumHeight() + Me.Height - Me.ClientRectangle.Height
            Exit Sub
        End If
    End Sub

    Private Sub Form_Closed(eventSender As System.Object, eventArgs As FormClosedEventArgs) Handles Me.FormClosed
        TWUtilities.LogMessage("Unloading main form")

        If Not mController Is Nothing Then mController.Finish()

        If mModel.ShowChart Then
            TWUtilities.LogMessage("Finishing charts")
            PriceChart.Finish()
            ProfitChart.Finish()
            TradeChart.Finish()
        End If

        TWUtilities.LogMessage("Closing other forms")

        For Each f As Form In Application.OpenForms
            If Not TypeOf f Is fStrategyHost Then
                TWUtilities.LogMessage($"Closing form: caption={f.Text}; type={TypeName(f)}")
                f.Close()
            End If
        Next

        gFinished = True
    End Sub

#End Region

#Region "IStrategyHostView Interface Members"

    Public Sub AddStudyToChart(pChartIndex As Integer, pStudy As _IStudy, pStudyValueNames As Dictionary(Of String, Object)) Implements IStrategyHostView.AddStudyToChart
        Dim lChartManager = mPriceMarketCharts(pChartIndex).Chart.ChartManager

        Dim lStudyConfig = lChartManager.GetDefaultStudyConfiguration(pStudy.Name, pStudy.LibraryName)
        Debug.Assert(lStudyConfig IsNot Nothing, "Can't get default study configuration")

        lStudyConfig.Study = pStudy
        lStudyConfig.UnderlyingStudy = pStudy.UnderlyingStudy

        For Each lSvc As StudyValueConfiguration In lStudyConfig.StudyValueConfigurations
            lSvc.IncludeInChart = pStudyValueNames.ContainsKey(lSvc.ValueName)
        Next

        lChartManager.ApplyStudyConfiguration(lStudyConfig, ReplayNumbers.ReplayAll)
    End Sub

    Public Function AddTimeframe(pTimeframe As TimeframeUtils27.Timeframe) As Integer Implements IStrategyHostView.AddTimeframe
        Dim lChart = PriceChart.AddRaw(pTimeframe,
                                mModel.Ticker.StudyBase.StudyManager,
                                mModel.Contract.SessionStartTime,
                                mModel.Contract.SessionEndTime,
                                mModel.Contract.Specifier.LocalSymbol,
                                mModel.Contract.Specifier.SecType,
                                mModel.Contract.Specifier.Exchange,
                                mModel.Contract.TickSize,
                                If(mModel.IsTickReplay, "", mModel.Contract.Specifier.LocalSymbol),
                                Not mModel.IsTickReplay)

        Dim lBracketOrderLineSeries = New LineSeries
        lChart.Regions.Item(ChartUtils.ChartRegionNamePrice).AddGraphicObjectSeries(DirectCast(lBracketOrderLineSeries, _IGraphicObjectSeries), LayerNumbers.LayerHighestUser)
        lBracketOrderLineSeries.Thickness = 2
        lBracketOrderLineSeries.ArrowEndStyle = ArrowStyles.ArrowClosed
        lBracketOrderLineSeries.ArrowEndWidth = 8
        lBracketOrderLineSeries.ArrowEndLength = 12

        Dim chartInfo = New ChartInfo() With {.Chart = lChart, .BracketOrderLineSeries = lBracketOrderLineSeries}
        mPriceMarketCharts.Add(chartInfo)

        Return mPriceMarketCharts.Count - 1
    End Function

    Public Sub ClearPriceAndProfitFields1() Implements IStrategyHostView.ClearPriceAndProfitFields
        clearPriceAndProfitFields()
    End Sub

    Public Sub DisablePriceDrawing(Optional pTimeframeIndex As Integer = Nothing) Implements IStrategyHostView.DisablePriceDrawing
        If pTimeframeIndex = 0 Then
            For Each chartInfo In mPriceMarketCharts
                chartInfo.Chart.DisableDrawing()
            Next
        Else
            mPriceMarketCharts(pTimeframeIndex).Chart.DisableDrawing()
        End If
    End Sub

    Public Sub DisableProfitDrawing() Implements IStrategyHostView.DisableProfitDrawing
        ProfitChart.DisableDrawing()
    End Sub

    Public Sub DisableStart() Implements IStrategyHostView.DisableStart
        StartButton.Enabled = False
        StopButton.Enabled = True
    End Sub

    Public Sub DisableTradeDrawing() Implements IStrategyHostView.DisableTradeDrawing
        TradeChart.DisableDrawing()
    End Sub

    Public Sub EnablePriceDrawing(Optional pTimeframeIndex As Integer = Nothing) Implements IStrategyHostView.EnablePriceDrawing
        If pTimeframeIndex = 0 Then
            For Each chartInfo In mPriceMarketCharts
                chartInfo.Chart.EnableDrawing()
            Next
        Else
            mPriceMarketCharts(pTimeframeIndex).Chart.EnableDrawing()
        End If
    End Sub

    Public Sub EnableProfitDrawing() Implements IStrategyHostView.EnableProfitDrawing
        ProfitChart.EnableDrawing()
    End Sub

    Public Sub EnableStart() Implements IStrategyHostView.EnableStart
        StartButton.Enabled = True
        StopButton.Enabled = False
    End Sub

    Public Sub EnableTradeDrawing() Implements IStrategyHostView.EnableTradeDrawing
        TradeChart.EnableDrawing()
    End Sub

    Public Sub NotifyBracketOrderProfile(profile As BracketOrderProfile) Implements IStrategyHostView.NotifyBracketOrderProfile
        Static sBracketOrderNumber As Integer

        sBracketOrderNumber = sBracketOrderNumber + 1
        Dim lListItem = New ListViewItem(profile.Key) With {
            .BackColor = ColorTranslator.FromOle(If(sBracketOrderNumber Mod 2 = 0, CInt(mTheme.GridRowBackColorEven), CInt(mTheme.GridRowBackColorOdd))),
            .ForeColor = ColorTranslator.FromOle(CInt(mTheme.GridForeColor))
        }

        Dim subItems([Enum].GetValues(GetType(BOListColumns)).Length - 1) As ListViewItem.ListViewSubItem

        addSubItem(subItems, BOListColumns.ColumnAction - 1, If(profile.Action = OrderActions.OrderActionBuy, "BUY", "SELL"))
        addSubItem(subItems, BOListColumns.ColumnDescription - 1, profile.Description)
        addSubItem(subItems, BOListColumns.ColumnEndTime - 1, FormatDateTime(profile.EndTime, vbGeneralDate))
        addSubItem(subItems, BOListColumns.ColumnEntryPrice - 1, mPriceFormatter.FormatPrice(profile.EntryPrice, mSecType, mTickSize))
        addSubItem(subItems, BOListColumns.ColumnEntryReason - 1, profile.EntryReason)
        addSubItem(subItems, BOListColumns.ColumnExitPrice - 1, mPriceFormatter.FormatPrice(profile.ExitPrice, mSecType, mTickSize))
        addSubItem(subItems, BOListColumns.ColumnMaxLoss - 1, $"{profile.MaxLoss,0:0.00}")
        addSubItem(subItems, BOListColumns.ColumnMaxProfit - 1, $"{profile.MaxProfit,0:0.00}")
        addSubItem(subItems, BOListColumns.ColumnProfit - 1, $"{profile.Profit,0:0.00}")
        addSubItem(subItems, BOListColumns.ColumnQuantity - 1, CStr(profile.Quantity))
        addSubItem(subItems, BOListColumns.ColumnRisk - 1, $"{profile.Risk,0:0.00}")
        addSubItem(subItems, BOListColumns.ColumnStartTime - 1, FormatDateTime(profile.StartTime, vbGeneralDate))
        addSubItem(subItems, BOListColumns.ColumnStopReason - 1, profile.StopReason)
        addSubItem(subItems, BOListColumns.ColumnTargetReason - 1, profile.TargetReason)

        lListItem.SubItems.AddRange(subItems)
        BracketOrderList.Items.Add(lListItem)
    End Sub

    Private Sub addSubItem(ByRef subItems() As ListViewItem.ListViewSubItem, index As Integer, text As String)
        subItems(index) = New ListViewItem.ListViewSubItem With {.Text = text}
    End Sub

    Public Sub NotifyError(pTitle As String, pMessage As String, pSeverity As IStrategyHostView.ErrorSeverities) Implements IStrategyHostView.NotifyError
        Select Case pSeverity
            Case IStrategyHostView.ErrorSeverities.ErrorSeverityInformation
                TWControls.ModelessMsgBox(pMessage, MsgBoxStyles.MsgBoxInformation, pTitle, Me)
            Case IStrategyHostView.ErrorSeverities.ErrorSeverityWarning
                MsgBox(pMessage, MsgBoxStyle.Exclamation, pTitle)
            Case IStrategyHostView.ErrorSeverities.ErrorSeverityCritical
                MsgBox(pMessage, MsgBoxStyle.Critical, pTitle)
        End Select
    End Sub

    Public Sub NotifyEventsPerSecond(Value As Integer) Implements IStrategyHostView.NotifyEventsPerSecond
        EventsPerSecondLabel.Text = CStr(Value)
    End Sub

    Public Sub NotifyEventsPlayed(Value As Integer) Implements IStrategyHostView.NotifyEventsPlayed
        EventsPlayedLabel.Text = CStr(Value)
    End Sub

    Public Sub NotifyInitialisationCompleted() Implements IStrategyHostView.NotifyInitialisationCompleted
        If Not mModel.ShowChart Then Exit Sub

        PriceChart.SelectChart(mPriceMarketCharts(0).Chart)
    End Sub

    Public Sub NotifyMicrosecsPerEvent(Value As Integer) Implements IStrategyHostView.NotifyMicrosecsPerEvent
        MicrosecsPerEventLabel.Text = CStr(Value)
    End Sub

    Public Sub NotifyNewTradeBar(pBarNumber As Integer, pTimestamp As Date) Implements IStrategyHostView.NotifyNewTradeBar
        If mModel.ShowChart Then
            mTradeStudyBase.NotifyBarNumber(pBarNumber, pTimestamp)
            mTradeStudyBase.NotifyValue(mOverallProfit + mSessionProfit, pTimestamp)
        End If
    End Sub

    Public Sub NotifyPosition(Value As Integer) Implements IStrategyHostView.NotifyPosition
        mPosition = Value
        Position.Text = CStr(mPosition)
    End Sub

    Public Sub NotifyReplayProgress(pTickfileTimestamp As Date, pEventsPlayed As Integer, pPercentComplete As Single) Implements IStrategyHostView.NotifyReplayProgress
        PercentCompleteLabel.Text = Format(pPercentComplete, "0.0")
        TheTime.Text = FormatTimestamp(pTickfileTimestamp, TradeWright.Utilities.Time.TimestampFormats.DateAndTimeISO8601 Or TradeWright.Utilities.Time.TimestampFormats.NoMillisecs)

        processDrawdown()
        processMaxProfit()
        processSessionProfit()
    End Sub

    Public Sub NotifyReplayStarted() Implements IStrategyHostView.NotifyReplayStarted
        mOverallProfit = 0#
        mSessionProfit = 0#
        mMaxProfit = 0#
        mDrawdown = 0#
    End Sub

    Public Sub NotifySessionDrawdown(Value As Double) Implements IStrategyHostView.NotifySessionDrawdown
        mDrawdown = Value
        If Not mModel.IsTickReplay Then processDrawdown()
    End Sub

    Public Sub NotifySessionMaxProfit(Value As Double) Implements IStrategyHostView.NotifySessionMaxProfit
        mMaxProfit = Value
        If Not mModel.IsTickReplay Then processMaxProfit()
    End Sub

    Public Sub NotifySessionProfit(Value As Double, pTimestamp As Date) Implements IStrategyHostView.NotifySessionProfit
        mSessionProfit = Value
        If Not mModel.IsTickReplay Then processSessionProfit()
        updateProfitCharts(pTimestamp)
    End Sub

    Public Sub NotifyTick(ByRef ev As TickUtils27.GenericTickEventData) Implements IStrategyHostView.NotifyTick
        If mModel.IsTickReplay Then Exit Sub

        Select Case ev.Tick.TickType
            Case TickTypes.TickTypeAsk
                AskLabel.Text = mPriceFormatter.FormatPrice(ev.Tick.Price, mSecType, mTickSize)
                AskSizeLabel.Text = CStr(ev.Tick.Size)
            Case TickTypes.TickTypeBid
                BidLabel.Text = mPriceFormatter.FormatPrice(ev.Tick.Price, mSecType, mTickSize)
                BidSizeLabel.Text = CStr(ev.Tick.Size)
            Case TickTypes.TickTypeTrade
                TradeLabel.Text = mPriceFormatter.FormatPrice(ev.Tick.Price, mSecType, mTickSize)
                TradeSizeLabel.Text = CStr(ev.Tick.Size)
            Case TickTypes.TickTypeVolume
                If Not mModel.IsTickReplay Then VolumeLabel.Text = CStr(ev.Tick.Size)
        End Select
    End Sub

    Public Sub NotifyTickerCreated() Implements IStrategyHostView.NotifyTickerCreated
        mContract = mModel.Contract
        mSecType = mContract.Specifier.SecType
        mTickSize = mContract.TickSize
        mSession = mModel.Session

        For Each chartInfo In mPriceMarketCharts
            chartInfo.Chart.StudyManager = mModel.Ticker.StudyBase.StudyManager
        Next

        If mProfitStudyBase Is Nothing Then mProfitStudyBase = initialiseProfitChart()
        If mTradeStudyBase Is Nothing Then mTradeStudyBase = initialiseTradeChart()

        Me.Text = GetAppTitle()

        SSTab2.SelectedIndex = 3
    End Sub

    Public Sub NotifyTickfileCompleted(pTickfile As TickfileSpecifier, pEventsPlayed As Integer) Implements IStrategyHostView.NotifyTickfileCompleted
        For i = 0 To TickfileOrganiser1.TickfileSpecifiers.Count - 1
            If pTickfile Is TickfileOrganiser1.TickfileSpecifiers(i) Then
                If i < TickfileOrganiser1.TickfileSpecifiers.Count - 1 Then
                    TickfileOrganiser1.ListIndex = i + 1
                End If
                Exit For
            End If
        Next

        mOverallProfit += mSessionProfit
        mSessionProfit = 0
    End Sub

    Public Sub NotifyTradingStart() Implements IStrategyHostView.NotifyTradingStart
        mTradingPlatformStarted = True
    End Sub

    Public ReadOnly Property Parameters() As Parameters Implements IStrategyHostView.Parameters
        Get
            Return mParams
        End Get
    End Property

    Public Sub ResetBracketOrderList() Implements IStrategyHostView.ResetBracketOrderList
        BracketOrderList.Clear()
        setupBracketOrderList()
    End Sub

    Public Sub ResetPriceChart() Implements IStrategyHostView.ResetPriceChart
        PriceChart.Clear()
    End Sub

    Public Sub ResetProfitChart() Implements IStrategyHostView.ResetProfitChart
        mProfitStudyBase = Nothing
    End Sub

    Public Sub ResetTradeChart() Implements IStrategyHostView.ResetTradeChart
        mTradeStudyBase = Nothing
    End Sub

    Public Sub ShowTradeLine(pStartTime As Date, pEndTime As Date, pEntryPrice As Double, pExitPrice As Double, pProfit As Double) Implements IStrategyHostView.ShowTradeLine
        Static OleColorBlue As Integer = ColorTranslator.ToOle(Color.Blue)
        Static OleColorBlack As Integer = ColorTranslator.ToOle(Color.Black)
        Static OleColorRed As Integer = ColorTranslator.ToOle(Color.Red)

        If Not mModel.ShowChart Then Exit Sub

        For Each chartInfo In mPriceMarketCharts
            Dim line = chartInfo.BracketOrderLineSeries.Add
            line.Point1 = ChartSkil.NewPoint(chartInfo.Chart.GetXFromTimestamp(pStartTime), pEntryPrice)
            line.Point2 = ChartSkil.NewPoint(chartInfo.Chart.GetXFromTimestamp(pEndTime), pExitPrice)

            If pProfit > 0 Then
                line.Color = OleColorBlue
                line.ArrowEndColor = OleColorBlue
                line.ArrowEndFillColor = OleColorBlue
            ElseIf pProfit = 0 Then
                line.Color = OleColorBlack
                line.ArrowEndColor = OleColorBlack
                line.ArrowEndFillColor = OleColorBlack
            Else
                line.Color = OleColorRed
                line.ArrowEndColor = OleColorRed
                line.ArrowEndFillColor = OleColorRed
            End If
        Next

    End Sub

    Public ReadOnly Property Strategy() As IStrategy Implements IStrategyHostView.Strategy
        Get
            Return DirectCast(Activator.CreateInstance(DirectCast(StrategyCombo.SelectedItem, Type)), IStrategy)
        End Get
    End Property

    Public Sub UpdateLastChartBars() Implements IStrategyHostView.UpdateLastChartBars
        PriceChart.UpdateLastBar()
        ProfitChart.UpdateLastBar()
        TradeChart.UpdateLastBar()
    End Sub

    Public Sub WriteLogText1(pMessage As String) Implements IStrategyHostView.WriteLogText
        writeLogText(pMessage)
    End Sub

#End Region

#Region "mSession Event Handlers"

    Private Sub mSession_Ended(s As Object, e As SessionEventArgs) Handles mSession.Ended
        TWUtilities.LogMessage($"Session ended at: {FormatTimestamp(e.Timestamp, Utilities.Time.TimestampFormats.DateAndTimeISO8601 Or Utilities.Time.TimestampFormats.NoMillisecs)}")

        If Not mModel.IsTickReplay And mSessionInProgress And mTradingPlatformStarted Then
            TWUtilities.LogMessage("Strategy Host closing")
            mController.Finish()
            Me.Close()
        ElseIf mModel.ShowChart Then
            Static sBarNumber As Integer

            sBarNumber += 1
            mProfitStudyBase.NotifyBarNumber(sBarNumber, mSession.CurrentSessionStartTime)
            mProfitStudyBase.NotifyValue(mOverallProfit, mSession.CurrentSessionStartTime)
        End If
    End Sub

    Private Sub mSession_Started(s As Object, e As SessionEventArgs) Handles mSession.Started
        TWUtilities.LogMessage($"Session started at: {FormatTimestamp(e.Timestamp, Utilities.Time.TimestampFormats.DateAndTimeISO8601 Or Utilities.Time.TimestampFormats.NoMillisecs)}")

        mSessionInProgress = True
    End Sub

#End Region

#Region "Control Event Handlers"

    Private Sub BracketOrderList_ColumnClick(eventSender As System.Object, eventArgs As ColumnClickEventArgs) Handles BracketOrderList.ColumnClick
        Static currentColumn As Integer

        If eventArgs.Column = currentColumn Then
            If BracketOrderList.Sorting = SortOrder.Ascending Then
                BracketOrderList.Sorting = SortOrder.Descending
            Else
                BracketOrderList.Sorting = SortOrder.Ascending
            End If
        Else
            BracketOrderList.ListViewItemSorter = New BracketOrderListSorter(eventArgs.Column)
            BracketOrderList.Sort()
        End If

    End Sub

    Private Sub BracketOrderList_DoubleClick(eventSender As System.Object, eventArgs As System.EventArgs) Handles BracketOrderList.DoubleClick
        If Not mModel.ShowChart Then Exit Sub

        Dim bracketOrderStartTime = CDate(BracketOrderList.FocusedItem.SubItems(BOListColumns.ColumnStartTime).Text)

        For Each chartInfo In mPriceMarketCharts
            Dim lPeriodNumber = chartInfo.Chart.Periods.Item(bracketOrderStartTime).PeriodNumber
            chartInfo.Chart.LastVisiblePeriod = lPeriodNumber + CInt(Int(chartInfo.Chart.LastVisiblePeriod - chartInfo.Chart.FirstVisiblePeriod) / 2) - 1
        Next

        SSTab1.SelectedIndex = 0
    End Sub

    Private Sub DummyProfitProfileCheck_Click(sender As Object, e As System.EventArgs) Handles DummyProfitProfileCheck.Click
        mModel.LogDummyProfitProfile = DummyProfitProfileCheck.Checked
    End Sub

    Private Sub MoreButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles MoreButton.Click
        Static sPrevHeight As Integer

        If mDetailsHidden Then
            mDetailsHidden = False
            MoreButton.Text = "Less <<<"
            Me.Height = sPrevHeight
        Else
            mDetailsHidden = True
            MoreButton.Text = "More >>>"
            sPrevHeight = Me.Height
            Me.Height = minimumHeight() + Me.Height - Me.ClientRectangle.Height
        End If
    End Sub

    Private Sub NoMoneyManagementCheck_Click(sender As Object, e As System.EventArgs) Handles NoMoneyManagementCheck.Click
        mModel.UseMoneyManagement = Not NoMoneyManagementCheck.Checked
    End Sub

    Private Sub PriceChart_Change(sender As Object, e As MultichartChangeEventArgs) Handles PriceChart.Change
        If e.ChangeType = Trading.UI.Trading.MultiChart.MultiChartChangeTypes.SelectionChanged Then
            ChartControlToolstrip.Chart = PriceChart.CurrentChart
        End If
    End Sub

    Private Sub ProfitProfileCheck_Click(sender As Object, e As System.EventArgs) Handles ProfitProfileCheck.Click
        mModel.LogProfitProfile = ProfitProfileCheck.Checked
    End Sub

    Private Sub ResultsPathButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles ResultsPathButton.Click
        ResultsPathText.Text = TWControls.ChoosePath(TWUtilities.ApplicationSettingsFolder & "Results")
    End Sub

    Private Sub ResultsPathText_TextChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles ResultsPathButton.TextChanged
        If mModel IsNot Nothing Then mModel.ResultsPath = ResultsPathText.Text
    End Sub

    Private Sub SeparateSessionsCheck_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles SeparateSessionsCheck.Click
        mModel.SeparateSessions = SeparateSessionsCheck.Checked
    End Sub

    Private Sub ShowChartCheck_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles ShowChartCheck.Click
        mModel.ShowChart = ShowChartCheck.Checked
    End Sub

    Private Sub StartButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles StartButton.Click
        startprocessing()
    End Sub

    Private Sub StopButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles StopButton.Click
        mController.StopTickfileReplay()
        StartButton.Enabled = True
        StopButton.Enabled = False
    End Sub

    Private Sub StopStrategyFactoryCombo_TextChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles StopStrategyFactoryCombo.TextChanged
        getDefaultParams()
    End Sub

    Private Sub StopStrategyFactoryCombo_SelectedIndexChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles StopStrategyFactoryCombo.SelectedIndexChanged
        getDefaultParams()
    End Sub

    Private Sub StrategyCombo_TextChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles StrategyCombo.TextChanged
        mModel.StrategyClassName = StrategyCombo.Text
        Me.Text = GetAppTitle()
        getDefaultParams()
    End Sub

    Private Sub StrategyCombo_SelectedIndexChanged(eventSender As System.Object, eventArgs As System.EventArgs) Handles StrategyCombo.SelectedIndexChanged
        mModel.StrategyClassName = StrategyCombo.Text
        Me.Text = GetAppTitle()
        getDefaultParams()
    End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Friend Sub Initialise(
                    pModel As IStrategyHostModel,
                    pController As IStrategyHostController)
        mModel = pModel
        mController = pController

        TWUtilities.LogMessage("Locating available trading strategies")
        locateTradingStrategies()

        TWUtilities.LogMessage("Locating available position management strategy factories")
        locatePositionManagementStrategyFactories()

        TWUtilities.LogMessage("Initialising charts")
        initialisePriceChart()
        ProfitChart.Style = mChartStyle
        TradeChart.Style = mChartStyle

        TWUtilities.LogMessage("Applying theme")
        applyTheme(DirectCast(New BlackTheme, ITheme))

        TWUtilities.LogMessage("Setting up bracket order list")
        setupBracketOrderList()

        TWUtilities.LogMessage("Setting controls from model")
        ResultsPathText.Text = mModel.ResultsPath
        NoMoneyManagementCheck.Checked = Not mModel.UseMoneyManagement
        If mModel.StrategyClassName <> "" Then StrategyCombo.Text = mModel.StrategyClassName
        If mModel.StopStrategyFactoryClassName <> "" Then StopStrategyFactoryCombo.Text = mModel.StopStrategyFactoryClassName
        ShowChartCheck.Checked = mModel.ShowChart

        If mModel.UseLiveBroker Then
            SymbolText.Enabled = True
            SymbolText.Text = mModel.Symbol.LocalSymbol
            SymbolText.Focus()
        Else
            TWUtilities.LogMessage("Enabling TickfileOrganiser")
            TickfileOrganiser1.Enabled = True

            If Not mModel.TickfileStoreInput Is Nothing Then
                TickfileOrganiser1.Initialise(mModel.TickfileStoreInput, DirectCast(mModel.ContractStorePrimary, ContractUtils27.IContractStore))
            End If

            TickfileOrganiser1.Focus()
        End If
    End Sub

    Private Sub locatePositionManagementStrategyFactories()
        Dim dir = New DirectoryInfo(Application.StartupPath & "\Strategies")

        If Not dir.Exists Then Return

        For Each file In dir.EnumerateFiles("*.dll")
            Dim strategies = From t In Assembly.LoadFrom(file.FullName).GetTypes()
                             Where t.GetInterfaces().Contains(GetType(IPositionManagementStrategyFactory)) And Not t.IsAbstract
                             Select t

            For Each s In strategies
                StopStrategyFactoryCombo.Items.Add(s)
            Next
        Next
    End Sub

    Private Sub locateTradingStrategies()
        Dim dir = New DirectoryInfo(Application.StartupPath & "\Strategies")

        If Not dir.Exists Then Return

        For Each file In dir.EnumerateFiles("*.dll")
            Dim strategies = From t In Assembly.LoadFrom(file.FullName).GetTypes()
                             Where t.GetInterfaces().Contains(GetType(IStrategy)) And Not t.IsAbstract
                             Select t
            StrategyCombo.Items.AddRange(strategies.ToArray())
        Next
    End Sub

    Friend Sub Start()
        getDefaultParams()
        startprocessing()
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub applyTheme(pTheme As ITheme)
        mTheme = pTheme
        Me.BackColor = ColorTranslator.FromOle(CInt(mTheme.BaseColor))
        StrategyHost.ApplyTheme(mTheme, Me.Controls)
    End Sub

    Private Sub calcAverageCharacterWidths(afont As Font, ByRef letterWidth As Single, ByRef digitWidth As Single)
        letterWidth = getAverageCharacterWidth("ABCDEFGH IJKLMNOP QRST UVWX YZ ABCDEFGH IJKLMNOP QRST UVWX YZ ABCDEFGH IJKLMNOP QRST UVWX YZ", afont)
        digitWidth = getAverageCharacterWidth(".0123456789.0123456789.0123456789.0123456789", afont)
    End Sub

    Private Sub clearPerformanceFields()
        EventsPlayedLabel.Text = ""
        PercentCompleteLabel.Text = ""
        EventsPerSecondLabel.Text = ""
        MicrosecsPerEventLabel.Text = ""
    End Sub

    Private Sub clearPriceAndProfitFields()
        BidLabel.Text = ""
        BidSizeLabel.Text = ""
        AskLabel.Text = ""
        AskSizeLabel.Text = ""
        TradeLabel.Text = ""
        TradeSizeLabel.Text = ""
        Profit.Text = ""
        Drawdown.Text = ""
        MaxProfit.Text = ""
        Position.Text = ""
    End Sub

    Private Function getAverageCharacterWidth(
                text As String,
                font As Font) As Integer
        Return CInt(TextRenderer.MeasureText(text, font).Width / Len(text))
    End Function

    Private Sub getDefaultParams()
        If StrategyCombo.Text = "" Then Exit Sub
        If StopStrategyFactoryCombo.Text = "" Then Exit Sub

        Dim lPMFactories As New List(Of IPositionManagementStrategyFactory) From {
            DirectCast(Activator.CreateInstance(DirectCast(StopStrategyFactoryCombo.SelectedItem, Type)), IPositionManagementStrategyFactory)
        }

        mParams = mController.GetDefaultParameters(DirectCast(Activator.CreateInstance(DirectCast(StrategyCombo.SelectedItem, Type)), IStrategy), lPMFactories)

        ParamGrid.DataSource = DirectCast(mParams, MSDATASRC.DataSource)
        ParamGrid.Columns(0).Width = ParamGrid.Width * 0.6F
        ParamGrid.Columns(1).Width = ParamGrid.Width * 0.3F

        StartButton.Enabled = True
    End Sub

    Private Sub initialisePriceChart(Optional pTimestamp As Date = #12:00:00 AM#)
        If Not mModel.ShowChart Then Exit Sub

        PriceChart.InitialiseRaw($"PriceChart.{Guid.NewGuid.ToString()}", mChartStyle, Nothing, "", "", ColorTranslator.FromOle(CInt(mChartStyle.ChartBackColor)))
    End Sub

    Private Function initialiseProfitChart() As StudyBaseForDoubleInput
        If Not mModel.ShowChart Then Return Nothing

        Dim lStudyBase = StudyUtils.CreateStudyBaseForDoubleInput(
                                            mModel.StudyLibraryManager.CreateStudyManager(
                                                            StartOfDayAsDate + mContract.SessionStartTime,
                                                            StartOfDayAsDate + mContract.SessionEndTime,
                                                            TWUtilities.GetTimeZone(mContract.TimezoneName)))

        ProfitChart.Initialise(TimeframeUtils.CreateTimeframes(lStudyBase), Not mModel.IsTickReplay)
        ProfitChart.DisableDrawing()
        ProfitChart.ShowChart(TWUtilities.GetTimePeriod(1, TimePeriodUnits.TimePeriodDay),
                                New ChartSpecifier(0, True),
                                mChartStyle,
                                pTitle:="Profit by Session")
        ProfitChart.PriceRegion.YScaleQuantum = 0.01

        Return DirectCast(lStudyBase, StudyBaseForDoubleInput)
    End Function

    Private Function initialiseTradeChart() As StudyBaseForDoubleInput
        If Not mModel.ShowChart Then Return Nothing

        Dim lStudyBase = StudyUtils.CreateStudyBaseForDoubleInput(
                                            mModel.StudyLibraryManager.CreateStudyManager(
                                                            StartOfDayAsDate + mContract.SessionStartTime,
                                                            StartOfDayAsDate + mContract.SessionEndTime,
                                                            TWUtilities.GetTimeZone(mContract.TimezoneName)))

        TradeChart.Initialise(TimeframeUtils.CreateTimeframes(lStudyBase), Not mModel.IsTickReplay)
        TradeChart.DisableDrawing()
        TradeChart.ShowChart(TWUtilities.GetTimePeriod(0, TimePeriodUnits.TimePeriodNone),
                             New ChartSpecifier(0, True),
                             mChartStyle,
                             pTitle:="Profit by Trade")
        TradeChart.PriceRegion.YScaleQuantum = 0.01

        Return DirectCast(lStudyBase, StudyBaseForDoubleInput)
    End Function

    Private Function minimumHeight() As Integer
        minimumHeight = SSTab2.Top + SSTab2.Height
    End Function

    Private Sub processDrawdown()
        Drawdown.Text = $"{mDrawdown,0:0.00}"
    End Sub

    Private Sub processMaxProfit()
        MaxProfit.Text = $"{mMaxProfit,0:0.00}"
    End Sub

    Private Sub processSessionProfit()
        Profit.Text = $"{mSessionProfit,0:0.00}"
    End Sub

    Private Sub setupBracketOrderList()
        Dim letterWidth As Single
        Dim digitWidth As Single

        calcAverageCharacterWidths(BracketOrderList.Font, letterWidth, digitWidth)

        BracketOrderList.Columns.Add("Key", CInt(BOListColumnWidths.WidthKey * digitWidth), HorizontalAlignment.Left)
        BracketOrderList.Columns.Add("Start time", CInt(BOListColumnWidths.WidthStartTime * digitWidth), HorizontalAlignment.Left)
        BracketOrderList.Columns.Add("End time", CInt(BOListColumnWidths.WidthEndTime * digitWidth), HorizontalAlignment.Left)
        BracketOrderList.Columns.Add("Action", CInt(BOListColumnWidths.WidthAction * letterWidth), HorizontalAlignment.Left)
        BracketOrderList.Columns.Add("Qty", CInt(BOListColumnWidths.WidthQuantity * digitWidth), HorizontalAlignment.Right)
        BracketOrderList.Columns.Add("Entry", CInt(BOListColumnWidths.WidthExitPrice * digitWidth), HorizontalAlignment.Right)
        BracketOrderList.Columns.Add("Exit", CInt(BOListColumnWidths.WidthExitPrice * digitWidth), HorizontalAlignment.Right)
        BracketOrderList.Columns.Add("Profit", CInt(BOListColumnWidths.WidthProfit * digitWidth), HorizontalAlignment.Right)
        BracketOrderList.Columns.Add("Max profit", CInt(BOListColumnWidths.WidthMaxProfit * digitWidth), HorizontalAlignment.Right)
        BracketOrderList.Columns.Add("Max loss", CInt(BOListColumnWidths.WidthMaxLoss * digitWidth), HorizontalAlignment.Right)
        BracketOrderList.Columns.Add("Risk", CInt(BOListColumnWidths.WidthRisk * digitWidth), HorizontalAlignment.Right)
        BracketOrderList.Columns.Add("Entry reason", CInt(BOListColumnWidths.WidthEntryReason * letterWidth), HorizontalAlignment.Left)
        BracketOrderList.Columns.Add("Target reason", CInt(BOListColumnWidths.WidthTargetReason * letterWidth), HorizontalAlignment.Left)
        BracketOrderList.Columns.Add("Stop reason", CInt(BOListColumnWidths.WidthStopReason * letterWidth), HorizontalAlignment.Left)
        BracketOrderList.Columns.Add("Description", CInt(BOListColumnWidths.WidthDescription * letterWidth), HorizontalAlignment.Left)

        BracketOrderList.ListViewItemSorter = New BracketOrderListSorter(BOListColumns.ColumnStartTime)
        BracketOrderList.Sorting = SortOrder.Descending
    End Sub

    Private Sub startprocessing()
        clearPerformanceFields()

        mPriceMarketCharts.Clear()

        If TickfileOrganiser1.TickfileCount <> 0 Then
            TickfileOrganiser1.ListIndex = 0
            mController.StartTickfileReplay(TickfileOrganiser1.TickfileSpecifiers)
        Else
            mController.StartLiveProcessing(mModel.Symbol)
        End If
    End Sub

    Private Sub updateProfitCharts(pTimestamp As Date)
        If mModel.ShowChart And mPosition <> 0 Then
            mProfitStudyBase.NotifyValue(mOverallProfit + mSessionProfit, pTimestamp)
            mTradeStudyBase.NotifyValue(mOverallProfit + mSessionProfit, pTimestamp)
        End If
    End Sub

    Private Sub writeLogText(pMessage As String)
        LogText.AppendText(pMessage & vbCrLf)
    End Sub

#End Region

End Class