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

Imports BarUtils27
Imports ChartSkil27
Imports MarketDataUtils27
Imports StudyUtils27
Imports TimeframeUtils27
Imports TWUtilities40

Imports TradeWright.Trading.Utils.Charts.BarFormatters
Imports TradeWright.Trading.Utils.Contracts

Imports System.Runtime.InteropServices.Marshal

Public Class MultiChart

#Region "Interfaces"

    Implements ISupportsTemplates

#End Region

#Region "Events"

    Public Event Change(sender As Object, e As MultichartChangeEventArgs)

    Public Event ChartStateChanged(sender As Object, e As ChartStateChangedEventArgs)

#End Region

#Region "Enums"

    Enum MultiChartChangeTypes
        SelectionChanged
        Added
        Removed
        TimeframeChanged
        SymbolChanged
    End Enum

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "MultiChart"

    Private Const ConfigSectionChartSpecifier As String = "ChartSpecifier"
    Private Const ConfigSectioMarketChart As String = "MarketChart"
    Private Const ConfigSectionMarketCharts As String = "MarketCharts"
    Private Const ConfigSectionMarketChartTemplate As String = "MarketChartTemplate"

    Private Const ConfigSettingBarFormatterFactoryName As String = "&BarFormatterFactoryName"
    Private Const ConfigSettingBarFormatterLibraryName As String = "&BarFormatterLibraryName"
    Private Const ConfigSettingChartStyle As String = "&ChartStyle"
    Private Const ConfigSettingCurrentChart As String = "&CurrentChart"
    Private Const ConfigSettingOrder As String = "&Order"
    Private Const ConfigSettingDataSourceKey As String = "&DataSourceKey"
    Private Const ConfigSettingWorkspace As String = "&Workspace"
    Private Const ConfigSettingIndex As String = "&Index"

#End Region

#Region "Member variables"

    Private mID As String

    Private mCharts As New Dictionary(Of String, MarketChart)

    Private mStyle As ChartStyle
    Private mSpec As ChartSpecifier
    Private mIsHistoric As Boolean

    Private mCurrentChart As MarketChart

    Private mBarFormatterLibManager As BarFormatterLibManager

    Private mBarFormatterFactoryName As String
    Private mBarFormatterLibraryName As String

    Private mConfig As ConfigurationSection

    Private mTicker As IMarketDataSource
    Private mTimeframes As Timeframes

    Private mExcludeCurrentBar As Boolean

    Private mIsRaw As Boolean

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        DefaultChartBackColor = Color.FromArgb(&HC0C0C0)

        'Me.MktChart.HorizontalMouseScrollingAllowed = True
        'Me.MktChart.VerticalMouseScrollingAllowed = True
        'Me.MktChart.Autoscrolling = True
        'Me.MktChart.ChartBackColor = DefaultChartBackColor
        'Me.MktChart.PointerCrosshairsColor = System.Drawing.Color.FromArgb(254, 29, 12)
        'Me.MktChart.PointerDiscColor = System.Drawing.Color.FromArgb(255, 255, 137)
        'Me.MktChart.PointerStyle = ChartSkil27.PointerStyles.PointerCrosshairs
        'Me.MktChart.HorizontalScrollBarVisible = True
        'Me.MktChart.YAxisWidthCm = 1.3!
    End Sub

#End Region

#Region "Constructors"

    Public Overridable Sub OnChange(ev As MultichartChangeEventArgs)
        RaiseEvent Change(Me, ev)
    End Sub

    Public Overridable Sub OnChartStateChanged(ev As ChartStateChangedEventArgs)
        RaiseEvent ChartStateChanged(Me, ev)
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "Control Event Handlers"

    Private Sub AddTimeframeToolStripButton_Click(sender As Object, e As System.EventArgs) Handles AddTimeframeToolStripButton.Click
        MyBase.OnClick(EventArgs.Empty)
        ChangeTimeframeToolStripButton.CheckState = CheckState.Unchecked
        If AddTimeframeToolStripButton.CheckState = CheckState.Unchecked Then
            showTimeframeSelector()
            AddTimeframeToolStripButton.CheckState = CheckState.Checked
        Else
            hideTimeframeSelector()
            AddTimeframeToolStripButton.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub ChangeTimeframeToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles ChangeTimeframeToolStripButton.Click
        MyBase.OnClick(EventArgs.Empty)
        AddTimeframeToolStripButton.CheckState = CheckState.Unchecked
        If ChangeTimeframeToolStripButton.CheckState = CheckState.Unchecked Then
            showTimeframeSelector()
            ChangeTimeframeToolStripButton.CheckState = CheckState.Checked
        Else
            hideTimeframeSelector()
            ChangeTimeframeToolStripButton.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub ChartSelectorButton_Click(sender As System.Object, e As System.EventArgs)
        Dim tsb = CType(sender, ToolStripButton)
        If CurrentChart IsNot Nothing Then
            Dim currentSelectorButton = getSelectorButtonFromChart(CurrentChart)
            If currentSelectorButton Is tsb Then Exit Sub
            currentSelectorButton.Checked = False
        End If
        tsb.Checked = True
        Dim targetChart = getChartFromSelectorButton(tsb)
        switchToChart(targetChart)

        MyBase.OnClick(EventArgs.Empty)
        fireChange(targetChart, MultiChartChangeTypes.SelectionChanged)
    End Sub

    Private Sub ChartSelectorButton_LocationChanged(sender As System.Object, e As System.EventArgs)
        saveChartOrderToConfig(mConfig)
    End Sub

    Private Sub MktChart_Click(sender As Object, e As EventArgs)
        MyBase.OnClick(e)
    End Sub

    Private Sub MktChart_DoubleClick(sender As Object, e As EventArgs)
        MyBase.OnDoubleClick(e)
    End Sub

    Private Sub MktChart_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
    End Sub

    Private Sub MktChart_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.OnKeyPress(e)
    End Sub

    Private Sub MktChart_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyUp(e)
    End Sub

    Private Sub MktChart_StateChange(ev As StateChangeEventData)
        Dim changedChart = CType(ev.Source, MarketChart)
        If changedChart Is CurrentChart And ev.State = MarketChart.ChartStates.ChartStateLoaded Then
            ChangeTimeframeToolStripButton.Enabled = True
        End If
        OnChartStateChanged(New ChartStateChangedEventArgs(changedChart, DirectCast(ev.State, MarketChart.ChartStates)))
    End Sub

    Private Sub MktChart_TimeframeChange(sender As System.Object, ev As System.EventArgs)
        Dim lChart = CType(sender, MarketChart)
        Dim tsb = CType(lChart.Tag, ToolStripButton)
        tsb.Text = lChart.TimePeriod.ToShortString
        tsb.ToolTipText = String.Format("Show {0} chart", lChart.TimePeriod.ToString)
        fireChange(lChart, MultiChartChangeTypes.TimeframeChanged)
    End Sub

    Private Sub MultiChartChartSelectorToolStrip_Click(sender As Object, e As EventArgs) Handles MultiChartChartSelectorToolStrip.Click
        MyBase.OnClick(EventArgs.Empty)
    End Sub

    Private Sub MultiChartControlToolStrip_Click(sender As Object, e As EventArgs) Handles MultiChartControlToolStrip.Click
        MyBase.OnClick(EventArgs.Empty)
    End Sub

    Private Sub MultiChartToolStripPanel1_Click(sender As Object, e As EventArgs) Handles MultiChartToolStripPanel1.Click
        MyBase.OnClick(EventArgs.Empty)
    End Sub

    Private Sub RemoveTimeframeToolStripButton_Click(sender As Object, e As System.EventArgs) Handles RemoveTimeframeToolStripButton.Click
        MyBase.OnClick(EventArgs.Empty)
        AddTimeframeToolStripButton.CheckState = CheckState.Unchecked
        ChangeTimeframeToolStripButton.CheckState = CheckState.Unchecked
        Remove(mCurrentChart)
    End Sub

    Private Sub TimeframeSelector_SelectionChangeCommitted(sender As Object, e As System.EventArgs)
        If ChangeTimeframeToolStripButton.CheckState = CheckState.Checked Then
            CurrentChart.ChangeTimeframe(ToolStripTimePeriodSelector1.TimePeriod)
            ChangeTimeframeToolStripButton.CheckState = CheckState.Unchecked
        Else
            Add(ToolStripTimePeriodSelector1.TimePeriod)
            AddTimeframeToolStripButton.CheckState = CheckState.Unchecked
        End If
        hideTimeframeSelector()
    End Sub

    Protected Overrides Sub OnVisibleChanged(e As System.EventArgs)
        If CurrentChart IsNot Nothing Then
            If MyBase.Visible Then
                If CurrentChart.State = MarketChart.ChartStates.ChartStateLoaded Then CurrentChart.EnableDrawing()
            Else
                If CurrentChart.State = MarketChart.ChartStates.ChartStateLoaded Then CurrentChart.DisableDrawing()
            End If
        End If
        MyBase.OnVisibleChanged(e)
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public ReadOnly Property CurrentChart() As MarketChart
        Get
            Return mCurrentChart
        End Get
    End Property

    Public Overridable Property ConfigurationSection() As ConfigurationSection
        Get
            Return mConfig
        End Get
        Set
            If Value Is mConfig Then Exit Property
            If mConfig IsNot Nothing Then mConfig.Remove()
            mConfig = Value
            If mConfig Is Nothing Then Return

            storeSettings(mConfig)
        End Set
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Return MultiChartChartSelectorToolStrip.Items.Count
        End Get
    End Property

    Public Property DefaultChartBackColor As Color

    Public WriteOnly Property Style() As ChartStyle
        Set
            mStyle = Value

            If Not mConfig Is Nothing Then
                If mStyle Is Nothing Then
                    mConfig.SetSetting(ConfigSettingChartStyle, "")
                Else
                    mConfig.SetSetting(ConfigSettingChartStyle, mStyle.Name)
                End If
            End If

            forEachChart(Sub(c) c.Style = mStyle)
        End Set
    End Property

    Public ReadOnly Property Ticker As IMarketDataSource
        Get
            Return mTicker
        End Get
    End Property

    Public ReadOnly Property Timeframes As Timeframes
        Get
            Return mTimeframes
        End Get
    End Property

#End Region

#Region "Methods"

    Public Function Add(pTimePeriod As TimePeriod,
                Optional pTitle As String = "",
                Optional pUpdatePerTick As Boolean = True,
                Optional pInitialNumberOfBars As Long = -1,
                Optional pIncludeBarsOutsideSession As Boolean = False) As MarketChart
        If mTimeframes Is Nothing Then Throw New InvalidOperationException("Can't add non-raw charts to this MultiChart: one or more raw charts have already been added")

        Dim lchart = addChart()
        lchart.Initialise(mTimeframes, pUpdatePerTick)

        Dim tsb = addSelectorButton(pTimePeriod, lchart)

        ' we notify the add before calling ShowChart or ShowHistoric chart so that it's before
        ' the ChartStates.ChartStateInitialised and ChartStates.ChartStateLoaded events
        fireChange(lchart, MultiChartChangeTypes.Added)

        Dim lChartSpec = New ChartSpecifier(
                        CInt(IIf(pInitialNumberOfBars = -1, mSpec.InitialNumberOfBars, pInitialNumberOfBars)),
                        CBool(IIf(pIncludeBarsOutsideSession, True, mSpec.IncludeBarsOutsideSession)),
                        mSpec.FromTime,
                        mSpec.ToTime)

        lchart.ShowChart(pTimePeriod, lChartSpec, mStyle, mBarFormatterLibManager, mBarFormatterFactoryName, mBarFormatterLibraryName, mExcludeCurrentBar, pTitle)

        If Not mConfig Is Nothing Then
            lchart.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionMarketCharts).AddConfigurationSection(ConfigSectioMarketChart & "(" & TWUtilities.GenerateGUIDString & ")")
        End If
        saveChartOrderToConfig(mConfig)

        tsb.PerformClick()

        fireChange(lchart, MultiChartChangeTypes.SelectionChanged)

        Return lchart
    End Function

    Public Function AddRaw(
                pTimeframe As Timeframe,
                pStudyManager As StudyManager,
                Optional pLocalSymbol As String = "",
                Optional pSecType As SecurityType = SecurityType.None,
                Optional pExchange As String = "",
                Optional pTickSize As Double = 0.0,
                Optional pTitle As String = "",
                Optional pUpdatePerTick As Boolean = True) As MarketChart
        Return AddRaw(pTimeframe, pStudyManager, StartOfDay, StartOfDay, pLocalSymbol, pSecType, pExchange, pTickSize, pTitle, pUpdatePerTick)
    End Function

    Public Function AddRaw(
                pTimeframe As Timeframe,
                pStudyManager As StudyManager,
                pSessionStartTime As TimeSpan,
                pSessionEndTime As TimeSpan,
                Optional pLocalSymbol As String = "",
                Optional pSecType As SecurityType = SecurityType.None,
                Optional pExchange As String = "",
                Optional pTickSize As Double = 0.0,
                Optional pTitle As String = "",
                Optional pUpdatePerTick As Boolean = True) As MarketChart
        If mTimeframes IsNot Nothing Then Throw New InvalidOperationException("Can't add raw charts to this MultiChart")

        Dim lChart = addChart()
        lChart.InitialiseRaw(pStudyManager, pUpdatePerTick)

        Dim tsb = addSelectorButton(pTimeframe.TimePeriod, lChart)

        ' we notify the add before calling ShowChart so that it's before
        ' the ChartStates.ChartStateInitialised and ChartStates.ChartStateLoaded events
        fireChange(lChart, MultiChartChangeTypes.Added)

        lChart.ShowChartRaw(pTimeframe,
                    mStyle,
                    pSessionStartTime,
                    pSessionEndTime,
                    pLocalSymbol,
                    pSecType,
                    pExchange,
                    pTickSize,
                    mBarFormatterLibManager,
                    mBarFormatterFactoryName,
                    mBarFormatterLibraryName,
                    pTitle)

        tsb.PerformClick()

        fireChange(lChart, MultiChartChangeTypes.SelectionChanged)

        Return lChart
    End Function

    Public Sub ChangeCurrentChartTimePeriod(timePeriod As TimePeriod)
        CurrentChart.ChangeTimeframe(timePeriod)
    End Sub

    Public Sub ChangeTicker(ticker As IMarketDataSource, timeframes As Timeframes)
        gDiagLogger.Log($"Changing ticker for multichart {mID}", NameOf(ChangeTicker), ModuleName)

        If timeframes Is Nothing Then Throw New NullReferenceException("timeframes must not be Nothing")

        mTicker = ticker
        mTimeframes = timeframes
        'mConfig.SetSetting(ConfigSettingWorkspace, workspaceName)
        'mConfig.SetSetting(ConfigSettingDataSourceKey, mDataSource.Key)

        forEachChart(Sub(c)
                         If c Is CurrentChart Then
                             ' this is the currently visible chart, so we want to load the new ticker into it right away
                             c.ChangeTicker(mTimeframes, False)
                         Else
                             ' this chart is not currently visible, so we won't load the new ticker into it fully until
                             ' the user switches to it
                             c.ChangeTicker(mTimeframes, True)
                         End If
                     End Sub)

        fireChange(CurrentChart, MultiChartChangeTypes.SymbolChanged)
    End Sub

    Public Sub Clear()
        forEachChart(Sub(c) removeChart(c))
        MultiChartChartSelectorToolStrip.Items.Clear()
        mCurrentChart = Nothing
        saveChartOrderToConfig(mConfig)
    End Sub

    Public Overridable Sub Finish()
        'If mTimeframes IsNot Nothing Then ReleaseComObject(mTimeframes)
        mTimeframes = Nothing

        'If mConfig IsNot Nothing Then ReleaseComObject(mConfig)
        mConfig = Nothing

        For Each tsb As ToolStripButton In MultiChartChartSelectorToolStrip.Items
            CType(tsb.Tag, MarketChart).Finish()
            tsb.Tag = Nothing
        Next
        MultiChartChartSelectorToolStrip.Items.Clear()
        '        If MktChart IsNot Nothing Then MktChart.Finish()
        '        MktChart = Nothing
    End Sub

    Public Sub Initialise(
                    id As String,
                    ticker As IMarketDataSource,
                    timeframes As Timeframes,
                    timePeriodValidator As ITimePeriodValidator,
                    Optional backColor? As System.Drawing.Color = Nothing,
                    Optional chartSpec As ChartSpecifier = Nothing,
                    Optional style As ChartStyle = Nothing,
                    Optional barFormatterLibManager As BarFormatterLibManager = Nothing,
                    Optional barFormatterFactoryName As String = "",
                    Optional barFormatterLibraryName As String = "",
                    Optional excludeCurrentBar As Boolean = False)
        If barFormatterFactoryName <> "" And barFormatterLibManager Is Nothing Then Throw New ArgumentException("If pBarFormatterFactoryName is not blank then pBarFormatterLibManager must be supplied")
        If barFormatterLibraryName <> "" And barFormatterLibManager Is Nothing Then Throw New ArgumentException("If pBarFormatterLibraryName is not blank then pBarFormatterLibManager must be supplied")
        If (barFormatterLibraryName <> "" And barFormatterFactoryName = "") Or (barFormatterLibraryName = "" And barFormatterFactoryName <> "") Then Throw New ArgumentException("pBarFormatterLibraryName and pBarFormatterFactoryName must both be blank or non-blank")

        mID = id
        mTicker = ticker
        mTimeframes = timeframes
        mSpec = chartSpec

        If style Is Nothing Then
            mStyle = ChartSkil.ChartStylesManager.DefaultStyle
        Else
            mStyle = style
        End If
        '        MktChart.Style = mStyle

        'If backColor Is Nothing Then
        '    MktChart.ChartBackColor = DefaultChartBackColor
        'Else
        '    MktChart.ChartBackColor = backColor.Value
        'End If

        mIsHistoric = (mSpec IsNot Nothing AndAlso mSpec.ToTime <> Date.MinValue)

        mBarFormatterLibManager = barFormatterLibManager
        mBarFormatterFactoryName = barFormatterFactoryName
        mBarFormatterLibraryName = barFormatterLibraryName

        mExcludeCurrentBar = excludeCurrentBar

        setupTimeframeSelector(timePeriodValidator)

        storeSettings(mConfig)
    End Sub

    Public Sub InitialiseRaw(
                id As String,
                Optional style As ChartStyle = Nothing,
                Optional barFormatterLibManager As BarFormatterLibManager = Nothing,
                Optional barFormatterFactoryName As String = "",
                Optional barFormatterLibraryName As String = "",
                Optional backColor As System.Drawing.Color? = Nothing)
        If barFormatterFactoryName <> "" And barFormatterLibManager Is Nothing Then Throw New ArgumentException("If pBarFormatterFactoryName is not blank then pBarFormatterLibManager must be supplied")
        If barFormatterLibraryName <> "" And barFormatterLibManager Is Nothing Then Throw New ArgumentException("If pBarFormatterLibraryName is not blank then pBarFormatterLibManager must be supplied")
        If (barFormatterLibraryName <> "" And barFormatterFactoryName = "") Or (barFormatterLibraryName = "" And barFormatterFactoryName <> "") Then Throw New ArgumentException("pBarFormatterLibraryName and pBarFormatterFactoryName must both be blank or non-blank")

        mID = id
        mIsRaw = True

        If style Is Nothing Then
            mStyle = ChartSkil.ChartStylesManager.DefaultStyle
        Else
            mStyle = style
        End If
        'Me.MktChart.Style = mStyle

        'If backColor Is Nothing Then
        '    MktChart.ChartBackColor = DefaultChartBackColor
        'Else
        '    MktChart.ChartBackColor = backColor.Value
        'End If

        mBarFormatterLibManager = barFormatterLibManager
        mBarFormatterFactoryName = barFormatterFactoryName
        mBarFormatterLibraryName = barFormatterLibraryName

        MultiChartControlToolStrip.Visible = False
    End Sub

    Public Overridable Sub LoadFromConfig(config As ConfigurationSection)
        If config Is Nothing Then Throw New ArgumentNullException(NameOf(config))

        gDiagLogger.Log(String.Format("Loading multichart from config {0}", config.Path), NameOf(LoadFromConfig), ModuleName)

        mConfig = config

        'Me.MktChart.Dock = DockStyle.Fill
        'Me.MktChart.Visible = True

        loadConfig(mConfig, False)
    End Sub

    Public Overridable Sub LoadFromTemplate(template As Template) Implements ISupportsTemplates.LoadFromTemplate
        If template Is Nothing Then Throw New ArgumentNullException(NameOf(template))

        gDiagLogger.Log($"Loading multichart from template {template.Name}", NameOf(LoadFromTemplate), ModuleName)

        mConfig = template.LoadData(mConfig)
        loadConfig(mConfig, False)
    End Sub

    Public Sub Remove()
        If mCurrentChart IsNot Nothing Then Remove(mCurrentChart)
    End Sub

    Public Sub Remove(chart As MarketChart)
        removeChart(chart)
        removeSelectorButton(getSelectorButtonFromChart(chart))
        saveChartOrderToConfig(mConfig)
        selectPreviousChart(chart)
    End Sub

    Public Overridable Sub RemoveFromConfig()
        If mConfig Is Nothing Then Return
        mConfig.Remove()
    End Sub

    Public Sub SaveToTemplate(template As Template) Implements ISupportsTemplates.SaveToTemplate
        If template Is Nothing Then Throw New ArgumentNullException(NameOf(template))
        template.Data = mConfig
    End Sub

    Public Sub ScrollToTime(time As Date)
        CurrentChart.ScrollToTime(time)
    End Sub

    Public Sub SelectChart(chart As MarketChart)
        If Not mCharts.ContainsKey(chart.ID) Then Throw New ArgumentException("Chart is not a member of this Multichart", NameOf(chart))
        getSelectorButtonFromChart(chart).PerformClick()
    End Sub

    Public Sub SelectPreviousSessionCurrentChart()
        selectPreviousSessionCurrentChart(mConfig)
    End Sub

    Public Sub UpdateLastBar()
        forEachChart(Sub(c) c.UpdateLastBar())
    End Sub

#End Region

#Region "Helper Functions"

    Private Function addChart(Optional id As String = "") As MarketChart
        Static chartNumber As Integer
        'Me.MktChart.Dock = DockStyle.None
        'Me.MktChart.Visible = False

        chartNumber += 1

        Dim lChart = New MarketChart

        Me.Controls.Add(lChart)

        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MultiChart))
        CType(lChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        lChart.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        lChart.Enabled = True
        lChart.Location = New System.Drawing.Point(0, 0)
        lChart.Name = "Chart" & chartNumber
        '        lChart.OcxState = MktChart.OcxState
        lChart.Visible = False
        CType(lChart, System.ComponentModel.ISupportInitialize).EndInit()
        lChart.Size = New System.Drawing.Size(Me.Width, MultiChartToolStripPanel1.Top)
        Me.ResumeLayout(False)
        Me.PerformLayout()

        AddHandler lChart.Click, AddressOf MktChart_Click
        AddHandler lChart.DoubleClick, AddressOf MktChart_DoubleClick
        AddHandler lChart.TimePeriodChange, AddressOf MktChart_TimeframeChange
        AddHandler lChart.StateChange, AddressOf MktChart_StateChange
        AddHandler lChart.KeyDown, AddressOf MktChart_KeyDown
        AddHandler lChart.KeyPress, AddressOf MktChart_KeyPress
        AddHandler lChart.KeyUp, AddressOf MktChart_KeyUp

        lChart.ID = If(String.IsNullOrEmpty(id), Guid.NewGuid.ToString, id)
        mCharts.Add(lChart.ID, lChart)
        Return lChart
    End Function

    Private Sub addFromConfig(chartSect As ConfigurationSection, asTemplate As Boolean)
        Dim lChart = addChart(chartSect.InstanceQualifier)

        If asTemplate Then
            lChart.LoadFromConfig(mTimeframes, chartSect, mBarFormatterLibManager, False)
        Else
            lChart.LoadFromConfig(mTimeframes, chartSect, mBarFormatterLibManager, True)
        End If
        addSelectorButton(lChart.TimePeriod, lChart)

        fireChange(lChart, MultiChartChangeTypes.Added)
    End Sub

    Private Function addSelectorButton(timePeriod As TimePeriod, pChart As MarketChart) As ToolStripButton
        Dim button = New ToolStripButton(timePeriod.ToShortString)
        button.ToolTipText = String.Format("Show {0} chart", timePeriod.ToString)
        MultiChartChartSelectorToolStrip.Items.Add(button)
        button.CheckOnClick = False
        button.Tag = pChart
        AddHandler button.Click, AddressOf ChartSelectorButton_Click
        AddHandler button.LocationChanged, AddressOf ChartSelectorButton_LocationChanged

        pChart.Tag = button

        RemoveTimeframeToolStripButton.Enabled = True

        Return button
    End Function

    Private Sub closeChart(chart As MarketChart)
        chart.Finish()
        chart.RemoveFromConfig()
        Me.Controls.Remove(chart)

        RemoveHandler chart.Click, AddressOf MktChart_Click
        RemoveHandler chart.DoubleClick, AddressOf MktChart_DoubleClick
        RemoveHandler chart.TimePeriodChange, AddressOf MktChart_TimeframeChange
        RemoveHandler chart.StateChange, AddressOf MktChart_StateChange
        RemoveHandler chart.KeyDown, AddressOf MktChart_KeyDown
        RemoveHandler chart.KeyPress, AddressOf MktChart_KeyPress
        RemoveHandler chart.KeyUp, AddressOf MktChart_KeyUp

        chart.Dispose()
    End Sub

    Private Sub fireChange(chart As MarketChart, changeType As MultiChartChangeTypes)
        OnChange(New MultichartChangeEventArgs(chart, changeType))
    End Sub

    Private Sub forEach(Of T)(items As IEnumerable(Of T), predicate As Predicate(Of T), action As Action(Of T))
        Dim selectedItems = From item In items
                            Where predicate(item)
        For Each item As T In selectedItems
            action(item)
        Next
    End Sub

    Private Sub forEachChart(action As Action(Of MarketChart))
        forEach(mCharts.Values, Function(c) c IsNot Nothing, action)

        Dim charts = From c In mCharts.Values
                     Where c IsNot Nothing
        For Each chart As MarketChart In charts
            action(chart)
        Next
    End Sub

    Private Function getChartFromSelectorButton(button As ToolStripButton) As MarketChart
        Return DirectCast(button.Tag, MarketChart)
    End Function

    Private Function getSelectorButtonFromChart(chart As MarketChart) As ToolStripButton
        Return DirectCast(chart.Tag, ToolStripButton)
    End Function

    Private Function getSelectorButtonIndex(button As ToolStripButton) As Integer
        Return MultiChartChartSelectorToolStrip.Items.IndexOf(DirectCast(button, ToolStripItem))
    End Function

    Private Sub hideChart(chart As MarketChart)
        If chart.State = MarketChart.ChartStates.ChartStateLoaded Then chart.DisableDrawing()
        chart.Anchor = AnchorStyles.None
        chart.Dock = DockStyle.None
        chart.Visible = False
    End Sub

    Private Sub hideTimeframeSelector()
        ToolStripTimePeriodSelector1.Visible = False
    End Sub

    Private Sub loadConfig(config As ConfigurationSection, asTemplate As Boolean)
        mSpec = ChartSpecifier.LoadFromConfiguration(config.GetConfigurationSection(ConfigSectionChartSpecifier))

        Dim lStyleName = config.GetSetting(ConfigSettingChartStyle, "")
        If ChartSkil.ChartStylesManager.Contains(lStyleName) Then
            mStyle = ChartSkil.ChartStylesManager.Item(lStyleName)
        Else
            mStyle = ChartSkil.ChartStylesManager.DefaultStyle
        End If

        mBarFormatterFactoryName = config.GetSetting(ConfigSettingBarFormatterFactoryName, "")
        mBarFormatterLibraryName = config.GetSetting(ConfigSettingBarFormatterLibraryName, "")

        mIsHistoric = (mSpec.ToTime <> Date.MinValue)

        Dim currentCharts = New List(Of MarketChart)(mCharts.Values)

        Dim chartsConfig = config.AddConfigurationSection(ConfigSectionMarketCharts)
        Dim chartOrder = chartsConfig.GetSetting(ConfigSettingOrder, "")
        If chartOrder <> "" Then
            Dim charts() = Split(chartOrder, ",")
            Dim chartConfigs = New Dictionary(Of String, ConfigurationSection)
            For Each cs As ConfigurationSection In chartsConfig
                chartConfigs.Add(cs.InstanceQualifier, cs)
            Next
            For Each instanceQualifier In charts
                addFromConfig(chartConfigs(instanceQualifier), asTemplate)
            Next
        Else
            For Each cs As ConfigurationSection In chartsConfig
                addFromConfig(cs, asTemplate)
            Next
        End If

        If asTemplate Then
            ' now remove charts that were previously opened. We don't do this before adding the ones from the template,
            ' because reducing Count to 0 might be significant to clients (eg they might Finish() this MultiChart)
            For Each chart In currentCharts
                removeChart(chart)
                removeSelectorButton(getSelectorButtonFromChart(chart))
            Next
        End If
    End Sub

    Private Sub printConfig(pConfig As ConfigurationSection, pIndent As Integer)
        Debug.WriteLine(Space(pIndent) & pConfig.Name & CStr(IIf(pConfig.InstanceQualifier <> "", "(" & pConfig.InstanceQualifier & ")", "")) & vbCrLf)
        For Each lConfig As ConfigurationSection In pConfig
            printConfig(lConfig, pIndent + 1)
        Next
    End Sub

    Private Sub removeChart(chartToRemove As MarketChart)
        Try
            If chartToRemove Is CurrentChart Then mCurrentChart = Nothing
            closeChart(chartToRemove)

            fireChange(chartToRemove, MultiChartChangeTypes.Removed)

        Catch ex As Exception
            gErrorLogger.Log(LogLevels.LogLevelSevere, ex.ToString)
            Throw
        End Try
    End Sub

    Private Sub removeSelectorButton(button As ToolStripButton)
        button.Tag = Nothing
        MultiChartChartSelectorToolStrip.Items.Remove(button)

        If MultiChartChartSelectorToolStrip.Items.Count = 0 Then
            RemoveTimeframeToolStripButton.Enabled = False
            'Me.MktChart.Dock = DockStyle.Fill
            'Me.MktChart.Visible = True
        End If
    End Sub

    Private Sub selectPreviousChart(chart As MarketChart)
        Dim index = getSelectorButtonIndex(getSelectorButtonFromChart(chart))
        For i = index - 1 To -1 Step -1
            Dim button = DirectCast(MultiChartChartSelectorToolStrip.Items(i), ToolStripButton)
            If button IsNot Nothing Then
                SelectChart(getChartFromSelectorButton(button))
                Return
            End If
        Next
        'Me.MktChart.Dock = DockStyle.Fill
        'Me.MktChart.Visible = True
        If Not mConfig Is Nothing Then mConfig.RemoveSetting(ConfigSettingCurrentChart)
    End Sub

    Private Sub selectPreviousSessionCurrentChart(config As ConfigurationSection)
        If config Is Nothing Then Return

        Dim id = config.GetSetting(ConfigSettingCurrentChart, "")
        If id <> "" Then
            If MultiChartChartSelectorToolStrip.Items.Count > 0 Then getSelectorButtonFromChart(mCharts.Item(id)).PerformClick()
        End If
    End Sub

    Private Sub setupTimeframeSelector(timePeriodValidator As ITimePeriodValidator)
        ToolStripTimePeriodSelector1.UseShortTimePeriodStrings = False
        AddHandler ToolStripTimePeriodSelector1.TimePeriodSelector.SelectionChangeCommitted, AddressOf TimeframeSelector_SelectionChangeCommitted
        ToolStripTimePeriodSelector1.Initialise(timePeriodValidator)
    End Sub

    Private Sub showChart(chart As MarketChart)
        ' start the chart if it hasn't yet been started
        chart.Start()

        If chart.State = MarketChart.ChartStates.ChartStateLoaded Then
            chart.EnableDrawing()
            ChangeTimeframeToolStripButton.Enabled = True
        Else
            ChangeTimeframeToolStripButton.Enabled = False
        End If

        chart.Visible = True

        mCurrentChart = chart
        Me.ContextMenuStrip = chart.ContextMenuStrip

        If Not mConfig Is Nothing Then mConfig.SetSetting(ConfigSettingCurrentChart, mCurrentChart.ID)

        If Not mIsRaw Then ToolStripTimePeriodSelector1.TimePeriod = chart.TimePeriod

        fireChange(chart, MultiChartChangeTypes.SelectionChanged)
    End Sub

    Private Sub showTimeframeSelector()
        ToolStripTimePeriodSelector1.Visible = True
    End Sub

    Private Sub storeSettings(config As ConfigurationSection)
        If config Is Nothing Then Exit Sub

        If Not mSpec Is Nothing Then mSpec.ConfigurationSection = config.AddConfigurationSection(ConfigSectionChartSpecifier)

        If Not mStyle Is Nothing Then config.SetSetting(ConfigSettingChartStyle, mStyle.Name)

        Dim cs = config.AddConfigurationSection(ConfigSectionMarketCharts)
        forEachChart(Sub(c) c.ConfigurationSection = cs.AddConfigurationSection($"{ConfigSectioMarketChart}({c.ID})"))
        saveChartOrderToConfig(config)
    End Sub

    Private Sub switchToChart(newChart As MarketChart)
        If CurrentChart Is newChart Then Exit Sub
        If CurrentChart IsNot Nothing Then hideChart(CurrentChart)
        showChart(newChart)
    End Sub

    Private Sub saveChartOrderToConfig(config As ConfigurationSection)
        If config Is Nothing Then Return

        Dim chartsConfig = config.GetConfigurationSection(ConfigSectionMarketCharts)

        Dim names = New List(Of String)
        forEachChart(Sub(c) names.Add(c.ID))

        config.GetConfigurationSection(ConfigSectionMarketCharts).SetSetting(ConfigSettingOrder, String.Join(",", names))
    End Sub


#End Region

End Class

#Region "Classes"

Public Class ChartStateChangedEventArgs
    Inherits System.EventArgs

    Public chart As MarketChart
    Public state As MarketChart.ChartStates

    Friend Sub New(chart As MarketChart, state As MarketChart.ChartStates)
        Me.chart = chart
        Me.state = state
    End Sub
#End Region

End Class

Public Class MultichartChangeEventArgs
    Inherits System.EventArgs

    Public chart As MarketChart
    Public ChangeType As MultiChart.MultiChartChangeTypes

    Friend Sub New(chart As MarketChart, ChangeType As MultiChart.MultiChartChangeTypes)
        Me.chart = chart
        Me.ChangeType = ChangeType
    End Sub
End Class
