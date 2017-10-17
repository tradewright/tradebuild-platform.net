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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class fStrategyHost
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents PriceChart As TradeWright.Trading.UI.Trading.MultiChart
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents ProfitChart As TradeWright.Trading.UI.Trading.MarketChart
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents TradeChart As TradeWright.Trading.UI.Trading.MarketChart
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents BracketOrderList As System.Windows.Forms.ListView
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControlExtra
    Public WithEvents TickfileOrganiser1 As AxTradingUI27.AxTickfileOrganiser
	Public WithEvents ShowChartCheck As System.Windows.Forms.CheckBox
	Public WithEvents StopStrategyFactoryCombo As System.Windows.Forms.ComboBox
	Public WithEvents SymbolText As System.Windows.Forms.TextBox
	Public WithEvents StrategyCombo As System.Windows.Forms.ComboBox
	Public WithEvents DummyProfitProfileCheck As System.Windows.Forms.CheckBox
	Public WithEvents ProfitProfileCheck As System.Windows.Forms.CheckBox
    Public WithEvents NoMoneyManagementCheck As System.Windows.Forms.CheckBox
    Public WithEvents SeparateSessionsCheck As System.Windows.Forms.CheckBox
    Public WithEvents StopButton As System.Windows.Forms.Button
    Public WithEvents StartButton As System.Windows.Forms.Button
    Public WithEvents LiveTradesCheck As System.Windows.Forms.CheckBox
    Public WithEvents ResultsPathText As System.Windows.Forms.TextBox
    Public WithEvents ResultsPathButton As System.Windows.Forms.Button
    Public WithEvents Label As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents _SSTab2_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents ParamGrid As AxMSDATGRD.AxDataGrid
    Public WithEvents _SSTab2_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents LogText As System.Windows.Forms.TextBox
    Public WithEvents _SSTab2_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents MoreButton As System.Windows.Forms.Button
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents VolumeLabel As System.Windows.Forms.Label
    Public WithEvents BidSizeLabel As System.Windows.Forms.Label
    Public WithEvents BidLabel As System.Windows.Forms.Label
    Public WithEvents TradeSizeLabel As System.Windows.Forms.Label
    Public WithEvents TradeLabel As System.Windows.Forms.Label
    Public WithEvents AskSizeLabel As System.Windows.Forms.Label
    Public WithEvents AskLabel As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents MicrosecsPerEventLabel As System.Windows.Forms.Label
    Public WithEvents EventsPerSecondLabel As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents PercentCompleteLabel As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents EventsPlayedLabel As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Profit As System.Windows.Forms.Label
    Public WithEvents Drawdown As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents MaxProfit As System.Windows.Forms.Label
    Public WithEvents Position As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents TheTime As System.Windows.Forms.Label
    Public WithEvents _SSTab2_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents SSTab2 As System.Windows.Forms.TabControlExtra
    Friend WithEvents ChartControlToolstrip As TradeWright.Trading.UI.Trading.ChartControlToolstrip
    Public CommonDialogsOpen As System.Windows.Forms.OpenFileDialog
    Public CommonDialogsSave As System.Windows.Forms.SaveFileDialog
    Public CommonDialogsFont As System.Windows.Forms.FontDialog
    Public CommonDialogsColor As System.Windows.Forms.ColorDialog
    Public CommonDialogsPrint As System.Windows.Forms.PrintDialog
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fStrategyHost))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ResultsPathButton = New System.Windows.Forms.Button()
        Me.SSTab1 = New System.Windows.Forms.TabControlExtra()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.PriceChartToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.PriceChart = New TradeWright.Trading.UI.Trading.MultiChart()
        Me.ChartControlToolstrip = New TradeWright.Trading.UI.Trading.ChartControlToolstrip()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.ProfitChart = New TradeWright.Trading.UI.Trading.MarketChart()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.TradeChart = New TradeWright.Trading.UI.Trading.MarketChart()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.BracketOrderList = New System.Windows.Forms.ListView()
        Me.SSTab2 = New System.Windows.Forms.TabControlExtra()
        Me._SSTab2_TabPage0 = New System.Windows.Forms.TabPage()
        Me.TickfileOrganiser1 = New AxTradingUI27.AxTickfileOrganiser()
        Me.ShowChartCheck = New System.Windows.Forms.CheckBox()
        Me.StopStrategyFactoryCombo = New System.Windows.Forms.ComboBox()
        Me.SymbolText = New System.Windows.Forms.TextBox()
        Me.StrategyCombo = New System.Windows.Forms.ComboBox()
        Me.DummyProfitProfileCheck = New System.Windows.Forms.CheckBox()
        Me.ProfitProfileCheck = New System.Windows.Forms.CheckBox()
        Me.NoMoneyManagementCheck = New System.Windows.Forms.CheckBox()
        Me.SeparateSessionsCheck = New System.Windows.Forms.CheckBox()
        Me.StopButton = New System.Windows.Forms.Button()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.LiveTradesCheck = New System.Windows.Forms.CheckBox()
        Me.ResultsPathText = New System.Windows.Forms.TextBox()
        Me.Label = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me._SSTab2_TabPage1 = New System.Windows.Forms.TabPage()
        Me.ParamGrid = New AxMSDATGRD.AxDataGrid()
        Me._SSTab2_TabPage2 = New System.Windows.Forms.TabPage()
        Me.LogText = New System.Windows.Forms.TextBox()
        Me._SSTab2_TabPage3 = New System.Windows.Forms.TabPage()
        Me.MoreButton = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.VolumeLabel = New System.Windows.Forms.Label()
        Me.BidSizeLabel = New System.Windows.Forms.Label()
        Me.BidLabel = New System.Windows.Forms.Label()
        Me.TradeSizeLabel = New System.Windows.Forms.Label()
        Me.TradeLabel = New System.Windows.Forms.Label()
        Me.AskSizeLabel = New System.Windows.Forms.Label()
        Me.AskLabel = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.MicrosecsPerEventLabel = New System.Windows.Forms.Label()
        Me.EventsPerSecondLabel = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PercentCompleteLabel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.EventsPlayedLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Profit = New System.Windows.Forms.Label()
        Me.Drawdown = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MaxProfit = New System.Windows.Forms.Label()
        Me.Position = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TheTime = New System.Windows.Forms.Label()
        Me.CommonDialogsOpen = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialogsSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogsFont = New System.Windows.Forms.FontDialog()
        Me.CommonDialogsColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogsPrint = New System.Windows.Forms.PrintDialog()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.PriceChartToolStripContainer.ContentPanel.SuspendLayout()
        Me.PriceChartToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.PriceChartToolStripContainer.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        CType(Me.ProfitChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage2.SuspendLayout()
        CType(Me.TradeChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage3.SuspendLayout()
        Me.SSTab2.SuspendLayout()
        Me._SSTab2_TabPage0.SuspendLayout()
        CType(Me.TickfileOrganiser1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab2_TabPage1.SuspendLayout()
        CType(Me.ParamGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab2_TabPage2.SuspendLayout()
        Me._SSTab2_TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ResultsPathButton
        '
        Me.ResultsPathButton.BackColor = System.Drawing.SystemColors.Control
        Me.ResultsPathButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.ResultsPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ResultsPathButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ResultsPathButton.Location = New System.Drawing.Point(674, 155)
        Me.ResultsPathButton.Name = "ResultsPathButton"
        Me.ResultsPathButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ResultsPathButton.Size = New System.Drawing.Size(38, 20)
        Me.ResultsPathButton.TabIndex = 14
        Me.ResultsPathButton.Text = "..."
        Me.ToolTip1.SetToolTip(Me.ResultsPathButton, "Select results path")
        Me.ResultsPathButton.UseVisualStyleBackColor = False
        '
        'SSTab1
        '
        Me.SSTab1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.DisplayStyle = System.Windows.Forms.TabStyle.Rectangular
        '
        '
        '
        Me.SSTab1.DisplayStyleProvider.BlendStyle = System.Windows.Forms.BlendStyle.Normal
        Me.SSTab1.DisplayStyleProvider.BorderColorDisabled = System.Drawing.SystemColors.Control
        Me.SSTab1.DisplayStyleProvider.BorderColorFocused = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.BorderColorHighlighted = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.BorderColorUnselected = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.CloserButtonFillColorFocused = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonFillColorFocusedActive = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.CloserButtonFillColorHighlighted = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonFillColorHighlightedActive = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(176, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.CloserButtonFillColorSelected = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonFillColorSelectedActive = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.CloserButtonFillColorUnselected = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonOutlineColorFocused = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonOutlineColorFocusedActive = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonOutlineColorHighlighted = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonOutlineColorHighlightedActive = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonOutlineColorSelected = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonOutlineColorSelectedActive = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserButtonOutlineColorUnselected = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.CloserColorFocused = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.CloserColorFocusedActive = System.Drawing.Color.White
        Me.SSTab1.DisplayStyleProvider.CloserColorHighlighted = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.CloserColorHighlightedActive = System.Drawing.Color.White
        Me.SSTab1.DisplayStyleProvider.CloserColorSelected = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.CloserColorSelectedActive = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.CloserColorUnselected = System.Drawing.Color.Empty
        Me.SSTab1.DisplayStyleProvider.FocusTrack = False
        Me.SSTab1.DisplayStyleProvider.HotTrack = True
        Me.SSTab1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SSTab1.DisplayStyleProvider.Opacity = 1.0!
        Me.SSTab1.DisplayStyleProvider.Overlap = 0
        Me.SSTab1.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 5)
        Me.SSTab1.DisplayStyleProvider.PageBackgroundColorDisabled = System.Drawing.SystemColors.Control
        Me.SSTab1.DisplayStyleProvider.PageBackgroundColorFocused = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.PageBackgroundColorHighlighted = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.PageBackgroundColorSelected = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.PageBackgroundColorUnselected = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.SelectedTabIsLarger = False
        Me.SSTab1.DisplayStyleProvider.ShowTabCloser = False
        Me.SSTab1.DisplayStyleProvider.TabColorDisabled1 = System.Drawing.SystemColors.Control
        Me.SSTab1.DisplayStyleProvider.TabColorDisabled2 = System.Drawing.SystemColors.Control
        Me.SSTab1.DisplayStyleProvider.TabColorFocused1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TabColorFocused2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TabColorHighLighted1 = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TabColorHighLighted2 = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TabColorSelected1 = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TabColorSelected2 = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TabColorUnSelected1 = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TabColorUnSelected2 = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TabPageMargin = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.SSTab1.DisplayStyleProvider.TextColorFocused = System.Drawing.Color.White
        Me.SSTab1.DisplayStyleProvider.TextColorHighlighted = System.Drawing.Color.White
        Me.SSTab1.DisplayStyleProvider.TextColorSelected = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.SSTab1.DisplayStyleProvider.TextColorUnselected = System.Drawing.Color.Black
        Me.SSTab1.HotTrack = True
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(-4, 250)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(745, 369)
        Me.SSTab1.TabIndex = 32
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.PriceChartToolStripContainer)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 23)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(737, 342)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Price chart"
        '
        'PriceChartToolStripContainer
        '
        '
        'PriceChartToolStripContainer.ContentPanel
        '
        Me.PriceChartToolStripContainer.ContentPanel.Controls.Add(Me.PriceChart)
        Me.PriceChartToolStripContainer.ContentPanel.Size = New System.Drawing.Size(737, 317)
        Me.PriceChartToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PriceChartToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.PriceChartToolStripContainer.Name = "PriceChartToolStripContainer"
        Me.PriceChartToolStripContainer.Size = New System.Drawing.Size(737, 342)
        Me.PriceChartToolStripContainer.TabIndex = 54
        Me.PriceChartToolStripContainer.Text = "ToolStripContainer1"
        '
        'PriceChartToolStripContainer.TopToolStripPanel
        '
        Me.PriceChartToolStripContainer.TopToolStripPanel.Controls.Add(Me.ChartControlToolstrip)
        '
        'PriceChart
        '
        Me.PriceChart.ConfigurationSection = Nothing
        Me.PriceChart.DefaultChartBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PriceChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PriceChart.Location = New System.Drawing.Point(0, 0)
        Me.PriceChart.Name = "PriceChart"
        Me.PriceChart.Size = New System.Drawing.Size(737, 317)
        Me.PriceChart.TabIndex = 52
        '
        'ChartControlToolstrip
        '
        Me.ChartControlToolstrip.Chart = Nothing
        Me.ChartControlToolstrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ChartControlToolstrip.Enabled = False
        Me.ChartControlToolstrip.Location = New System.Drawing.Point(3, 0)
        Me.ChartControlToolstrip.Name = "ChartControlToolstrip"
        Me.ChartControlToolstrip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChartControlToolstrip.Size = New System.Drawing.Size(433, 25)
        Me.ChartControlToolstrip.TabIndex = 53
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.ProfitChart)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 23)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(737, 342)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Daily profit chart"
        '
        'ProfitChart
        '
        Me.ProfitChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProfitChart.Enabled = True
        Me.ProfitChart.Location = New System.Drawing.Point(0, 0)
        Me.ProfitChart.MinimumTicksHeight = 0
        Me.ProfitChart.Name = "ProfitChart"
        Me.ProfitChart.OcxState = CType(resources.GetObject("ProfitChart.OcxState"), System.Windows.Forms.AxHost.State)
        Me.ProfitChart.Size = New System.Drawing.Size(737, 342)
        Me.ProfitChart.TabIndex = 53
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.TradeChart)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 23)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(737, 342)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "Trade chart"
        '
        'TradeChart
        '
        Me.TradeChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TradeChart.Enabled = True
        Me.TradeChart.Location = New System.Drawing.Point(0, 0)
        Me.TradeChart.MinimumTicksHeight = 0
        Me.TradeChart.Name = "TradeChart"
        Me.TradeChart.OcxState = CType(resources.GetObject("TradeChart.OcxState"), System.Windows.Forms.AxHost.State)
        Me.TradeChart.Size = New System.Drawing.Size(737, 342)
        Me.TradeChart.TabIndex = 54
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.BracketOrderList)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 23)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(737, 342)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Bracket order details"
        '
        'BracketOrderList
        '
        Me.BracketOrderList.AllowColumnReorder = True
        Me.BracketOrderList.BackColor = System.Drawing.SystemColors.Window
        Me.BracketOrderList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BracketOrderList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BracketOrderList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BracketOrderList.FullRowSelect = True
        Me.BracketOrderList.HideSelection = False
        Me.BracketOrderList.LabelEdit = True
        Me.BracketOrderList.Location = New System.Drawing.Point(0, 0)
        Me.BracketOrderList.Name = "BracketOrderList"
        Me.BracketOrderList.Size = New System.Drawing.Size(737, 342)
        Me.BracketOrderList.TabIndex = 33
        Me.BracketOrderList.UseCompatibleStateImageBehavior = False
        Me.BracketOrderList.View = System.Windows.Forms.View.Details
        '
        'SSTab2
        '
        Me.SSTab2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SSTab2.Controls.Add(Me._SSTab2_TabPage0)
        Me.SSTab2.Controls.Add(Me._SSTab2_TabPage1)
        Me.SSTab2.Controls.Add(Me._SSTab2_TabPage2)
        Me.SSTab2.Controls.Add(Me._SSTab2_TabPage3)
        Me.SSTab2.DisplayStyle = System.Windows.Forms.TabStyle.Rectangular
        '
        '
        '
        Me.SSTab2.DisplayStyleProvider.BlendStyle = System.Windows.Forms.BlendStyle.Normal
        Me.SSTab2.DisplayStyleProvider.BorderColorDisabled = System.Drawing.SystemColors.Control
        Me.SSTab2.DisplayStyleProvider.BorderColorFocused = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.BorderColorHighlighted = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.BorderColorUnselected = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.CloserButtonFillColorFocused = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonFillColorFocusedActive = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.CloserButtonFillColorHighlighted = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonFillColorHighlightedActive = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(176, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.CloserButtonFillColorSelected = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonFillColorSelectedActive = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.CloserButtonFillColorUnselected = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonOutlineColorFocused = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonOutlineColorFocusedActive = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonOutlineColorHighlighted = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonOutlineColorHighlightedActive = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonOutlineColorSelected = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonOutlineColorSelectedActive = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserButtonOutlineColorUnselected = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.CloserColorFocused = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.CloserColorFocusedActive = System.Drawing.Color.White
        Me.SSTab2.DisplayStyleProvider.CloserColorHighlighted = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.CloserColorHighlightedActive = System.Drawing.Color.White
        Me.SSTab2.DisplayStyleProvider.CloserColorSelected = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.CloserColorSelectedActive = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.CloserColorUnselected = System.Drawing.Color.Empty
        Me.SSTab2.DisplayStyleProvider.FocusTrack = False
        Me.SSTab2.DisplayStyleProvider.HotTrack = True
        Me.SSTab2.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SSTab2.DisplayStyleProvider.Opacity = 1.0!
        Me.SSTab2.DisplayStyleProvider.Overlap = 0
        Me.SSTab2.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 5)
        Me.SSTab2.DisplayStyleProvider.PageBackgroundColorDisabled = System.Drawing.SystemColors.Control
        Me.SSTab2.DisplayStyleProvider.PageBackgroundColorFocused = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.PageBackgroundColorHighlighted = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.PageBackgroundColorSelected = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.PageBackgroundColorUnselected = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.SelectedTabIsLarger = False
        Me.SSTab2.DisplayStyleProvider.ShowTabCloser = False
        Me.SSTab2.DisplayStyleProvider.TabColorDisabled1 = System.Drawing.SystemColors.Control
        Me.SSTab2.DisplayStyleProvider.TabColorDisabled2 = System.Drawing.SystemColors.Control
        Me.SSTab2.DisplayStyleProvider.TabColorFocused1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TabColorFocused2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TabColorHighLighted1 = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TabColorHighLighted2 = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TabColorSelected1 = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TabColorSelected2 = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TabColorUnSelected1 = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TabColorUnSelected2 = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TabPageMargin = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.SSTab2.DisplayStyleProvider.TextColorFocused = System.Drawing.Color.White
        Me.SSTab2.DisplayStyleProvider.TextColorHighlighted = System.Drawing.Color.White
        Me.SSTab2.DisplayStyleProvider.TextColorSelected = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.SSTab2.DisplayStyleProvider.TextColorUnselected = System.Drawing.Color.Black
        Me.SSTab2.HotTrack = True
        Me.SSTab2.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab2.Location = New System.Drawing.Point(-4, 0)
        Me.SSTab2.Name = "SSTab2"
        Me.SSTab2.SelectedIndex = 0
        Me.SSTab2.Size = New System.Drawing.Size(745, 250)
        Me.SSTab2.TabIndex = 0
        '
        '_SSTab2_TabPage0
        '
        Me._SSTab2_TabPage0.Controls.Add(Me.TickfileOrganiser1)
        Me._SSTab2_TabPage0.Controls.Add(Me.ShowChartCheck)
        Me._SSTab2_TabPage0.Controls.Add(Me.StopStrategyFactoryCombo)
        Me._SSTab2_TabPage0.Controls.Add(Me.SymbolText)
        Me._SSTab2_TabPage0.Controls.Add(Me.StrategyCombo)
        Me._SSTab2_TabPage0.Controls.Add(Me.DummyProfitProfileCheck)
        Me._SSTab2_TabPage0.Controls.Add(Me.ProfitProfileCheck)
        Me._SSTab2_TabPage0.Controls.Add(Me.NoMoneyManagementCheck)
        Me._SSTab2_TabPage0.Controls.Add(Me.SeparateSessionsCheck)
        Me._SSTab2_TabPage0.Controls.Add(Me.StopButton)
        Me._SSTab2_TabPage0.Controls.Add(Me.StartButton)
        Me._SSTab2_TabPage0.Controls.Add(Me.LiveTradesCheck)
        Me._SSTab2_TabPage0.Controls.Add(Me.ResultsPathText)
        Me._SSTab2_TabPage0.Controls.Add(Me.ResultsPathButton)
        Me._SSTab2_TabPage0.Controls.Add(Me.Label)
        Me._SSTab2_TabPage0.Controls.Add(Me.Label13)
        Me._SSTab2_TabPage0.Location = New System.Drawing.Point(4, 23)
        Me._SSTab2_TabPage0.Name = "_SSTab2_TabPage0"
        Me._SSTab2_TabPage0.Size = New System.Drawing.Size(737, 223)
        Me._SSTab2_TabPage0.TabIndex = 0
        Me._SSTab2_TabPage0.Text = "Controls"
        '
        'TickfileOrganiser1
        '
        Me.TickfileOrganiser1.Location = New System.Drawing.Point(0, 24)
        Me.TickfileOrganiser1.Name = "TickfileOrganiser1"
        Me.TickfileOrganiser1.OcxState = CType(resources.GetObject("TickfileOrganiser1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.TickfileOrganiser1.Size = New System.Drawing.Size(377, 169)
        Me.TickfileOrganiser1.TabIndex = 2
        '
        'ShowChartCheck
        '
        Me.ShowChartCheck.BackColor = System.Drawing.SystemColors.Control
        Me.ShowChartCheck.Checked = True
        Me.ShowChartCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowChartCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.ShowChartCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ShowChartCheck.Location = New System.Drawing.Point(399, 56)
        Me.ShowChartCheck.Name = "ShowChartCheck"
        Me.ShowChartCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowChartCheck.Size = New System.Drawing.Size(121, 18)
        Me.ShowChartCheck.TabIndex = 7
        Me.ShowChartCheck.Text = "Show chart"
        Me.ShowChartCheck.UseVisualStyleBackColor = False
        '
        'StopStrategyFactoryCombo
        '
        Me.StopStrategyFactoryCombo.BackColor = System.Drawing.SystemColors.Window
        Me.StopStrategyFactoryCombo.Cursor = System.Windows.Forms.Cursors.Default
        Me.StopStrategyFactoryCombo.DisplayMember = "Name"
        Me.StopStrategyFactoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StopStrategyFactoryCombo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.StopStrategyFactoryCombo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.StopStrategyFactoryCombo.Location = New System.Drawing.Point(399, 29)
        Me.StopStrategyFactoryCombo.Name = "StopStrategyFactoryCombo"
        Me.StopStrategyFactoryCombo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StopStrategyFactoryCombo.Size = New System.Drawing.Size(233, 21)
        Me.StopStrategyFactoryCombo.Sorted = True
        Me.StopStrategyFactoryCombo.TabIndex = 4
        '
        'SymbolText
        '
        Me.SymbolText.AcceptsReturn = True
        Me.SymbolText.BackColor = System.Drawing.SystemColors.Window
        Me.SymbolText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SymbolText.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.SymbolText.Enabled = False
        Me.SymbolText.ForeColor = System.Drawing.SystemColors.WindowText
        Me.SymbolText.Location = New System.Drawing.Point(55, 5)
        Me.SymbolText.MaxLength = 0
        Me.SymbolText.Name = "SymbolText"
        Me.SymbolText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SymbolText.Size = New System.Drawing.Size(121, 13)
        Me.SymbolText.TabIndex = 1
        '
        'StrategyCombo
        '
        Me.StrategyCombo.BackColor = System.Drawing.SystemColors.Window
        Me.StrategyCombo.Cursor = System.Windows.Forms.Cursors.Default
        Me.StrategyCombo.DisplayMember = "Name"
        Me.StrategyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StrategyCombo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.StrategyCombo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.StrategyCombo.Location = New System.Drawing.Point(399, 5)
        Me.StrategyCombo.Name = "StrategyCombo"
        Me.StrategyCombo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StrategyCombo.Size = New System.Drawing.Size(233, 21)
        Me.StrategyCombo.Sorted = True
        Me.StrategyCombo.TabIndex = 3
        '
        'DummyProfitProfileCheck
        '
        Me.DummyProfitProfileCheck.BackColor = System.Drawing.SystemColors.Control
        Me.DummyProfitProfileCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.DummyProfitProfileCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DummyProfitProfileCheck.Location = New System.Drawing.Point(400, 106)
        Me.DummyProfitProfileCheck.Name = "DummyProfitProfileCheck"
        Me.DummyProfitProfileCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DummyProfitProfileCheck.Size = New System.Drawing.Size(129, 24)
        Me.DummyProfitProfileCheck.TabIndex = 10
        Me.DummyProfitProfileCheck.Text = "Dummy profit profile"
        Me.DummyProfitProfileCheck.UseVisualStyleBackColor = False
        '
        'ProfitProfileCheck
        '
        Me.ProfitProfileCheck.BackColor = System.Drawing.SystemColors.Control
        Me.ProfitProfileCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.ProfitProfileCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ProfitProfileCheck.Location = New System.Drawing.Point(399, 80)
        Me.ProfitProfileCheck.Name = "ProfitProfileCheck"
        Me.ProfitProfileCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ProfitProfileCheck.Size = New System.Drawing.Size(97, 20)
        Me.ProfitProfileCheck.TabIndex = 8
        Me.ProfitProfileCheck.Text = "Profit profile"
        Me.ProfitProfileCheck.UseVisualStyleBackColor = False
        '
        'NoMoneyManagementCheck
        '
        Me.NoMoneyManagementCheck.BackColor = System.Drawing.SystemColors.Control
        Me.NoMoneyManagementCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.NoMoneyManagementCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.NoMoneyManagementCheck.Location = New System.Drawing.Point(399, 136)
        Me.NoMoneyManagementCheck.Name = "NoMoneyManagementCheck"
        Me.NoMoneyManagementCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NoMoneyManagementCheck.Size = New System.Drawing.Size(154, 19)
        Me.NoMoneyManagementCheck.TabIndex = 12
        Me.NoMoneyManagementCheck.Text = "No money management"
        Me.NoMoneyManagementCheck.UseVisualStyleBackColor = False
        '
        'SeparateSessionsCheck
        '
        Me.SeparateSessionsCheck.BackColor = System.Drawing.SystemColors.Control
        Me.SeparateSessionsCheck.Checked = True
        Me.SeparateSessionsCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SeparateSessionsCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.SeparateSessionsCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.SeparateSessionsCheck.Location = New System.Drawing.Point(535, 80)
        Me.SeparateSessionsCheck.Name = "SeparateSessionsCheck"
        Me.SeparateSessionsCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SeparateSessionsCheck.Size = New System.Drawing.Size(177, 20)
        Me.SeparateSessionsCheck.TabIndex = 9
        Me.SeparateSessionsCheck.Text = "Separate session per tick file"
        Me.SeparateSessionsCheck.UseVisualStyleBackColor = False
        '
        'StopButton
        '
        Me.StopButton.BackColor = System.Drawing.SystemColors.Control
        Me.StopButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.StopButton.Enabled = False
        Me.StopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.StopButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.StopButton.Location = New System.Drawing.Point(639, 29)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StopButton.Size = New System.Drawing.Size(73, 25)
        Me.StopButton.TabIndex = 6
        Me.StopButton.Text = "Stop"
        Me.StopButton.UseVisualStyleBackColor = False
        '
        'StartButton
        '
        Me.StartButton.BackColor = System.Drawing.SystemColors.Control
        Me.StartButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.StartButton.Enabled = False
        Me.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.StartButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.StartButton.Location = New System.Drawing.Point(639, 5)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartButton.Size = New System.Drawing.Size(73, 25)
        Me.StartButton.TabIndex = 5
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = False
        '
        'LiveTradesCheck
        '
        Me.LiveTradesCheck.BackColor = System.Drawing.SystemColors.Control
        Me.LiveTradesCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.LiveTradesCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LiveTradesCheck.Location = New System.Drawing.Point(535, 106)
        Me.LiveTradesCheck.Name = "LiveTradesCheck"
        Me.LiveTradesCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LiveTradesCheck.Size = New System.Drawing.Size(161, 24)
        Me.LiveTradesCheck.TabIndex = 11
        Me.LiveTradesCheck.Text = "Live trades"
        Me.LiveTradesCheck.UseVisualStyleBackColor = False
        '
        'ResultsPathText
        '
        Me.ResultsPathText.AcceptsReturn = True
        Me.ResultsPathText.BackColor = System.Drawing.SystemColors.Window
        Me.ResultsPathText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ResultsPathText.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ResultsPathText.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ResultsPathText.Location = New System.Drawing.Point(535, 155)
        Me.ResultsPathText.MaxLength = 0
        Me.ResultsPathText.Name = "ResultsPathText"
        Me.ResultsPathText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ResultsPathText.Size = New System.Drawing.Size(133, 20)
        Me.ResultsPathText.TabIndex = 13
        '
        'Label
        '
        Me.Label.BackColor = System.Drawing.SystemColors.Control
        Me.Label.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.Location = New System.Drawing.Point(0, 5)
        Me.Label.Name = "Label"
        Me.Label.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label.Size = New System.Drawing.Size(49, 25)
        Me.Label.TabIndex = 37
        Me.Label.Text = "Symbol"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(397, 158)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(81, 17)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "Results path"
        '
        '_SSTab2_TabPage1
        '
        Me._SSTab2_TabPage1.Controls.Add(Me.ParamGrid)
        Me._SSTab2_TabPage1.Location = New System.Drawing.Point(4, 23)
        Me._SSTab2_TabPage1.Name = "_SSTab2_TabPage1"
        Me._SSTab2_TabPage1.Size = New System.Drawing.Size(737, 223)
        Me._SSTab2_TabPage1.TabIndex = 1
        Me._SSTab2_TabPage1.Text = "Parameters"
        '
        'ParamGrid
        '
        Me.ParamGrid.DataSource = Nothing
        Me.ParamGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ParamGrid.Location = New System.Drawing.Point(0, 0)
        Me.ParamGrid.Name = "ParamGrid"
        Me.ParamGrid.OcxState = CType(resources.GetObject("ParamGrid.OcxState"), System.Windows.Forms.AxHost.State)
        Me.ParamGrid.Size = New System.Drawing.Size(737, 223)
        Me.ParamGrid.TabIndex = 15
        '
        '_SSTab2_TabPage2
        '
        Me._SSTab2_TabPage2.Controls.Add(Me.LogText)
        Me._SSTab2_TabPage2.Location = New System.Drawing.Point(4, 23)
        Me._SSTab2_TabPage2.Name = "_SSTab2_TabPage2"
        Me._SSTab2_TabPage2.Size = New System.Drawing.Size(737, 223)
        Me._SSTab2_TabPage2.TabIndex = 2
        Me._SSTab2_TabPage2.Text = "Log"
        '
        'LogText
        '
        Me.LogText.AcceptsReturn = True
        Me.LogText.BackColor = System.Drawing.SystemColors.Window
        Me.LogText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LogText.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.LogText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogText.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogText.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LogText.Location = New System.Drawing.Point(0, 0)
        Me.LogText.MaxLength = 0
        Me.LogText.Multiline = True
        Me.LogText.Name = "LogText"
        Me.LogText.ReadOnly = True
        Me.LogText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LogText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.LogText.Size = New System.Drawing.Size(737, 223)
        Me.LogText.TabIndex = 39
        Me.LogText.WordWrap = False
        '
        '_SSTab2_TabPage3
        '
        Me._SSTab2_TabPage3.Controls.Add(Me.MoreButton)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label11)
        Me._SSTab2_TabPage3.Controls.Add(Me.VolumeLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.BidSizeLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.BidLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.TradeSizeLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.TradeLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.AskSizeLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.AskLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label7)
        Me._SSTab2_TabPage3.Controls.Add(Me.MicrosecsPerEventLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.EventsPerSecondLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label3)
        Me._SSTab2_TabPage3.Controls.Add(Me.PercentCompleteLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label2)
        Me._SSTab2_TabPage3.Controls.Add(Me.EventsPlayedLabel)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label1)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label8)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label10)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label9)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label4)
        Me._SSTab2_TabPage3.Controls.Add(Me.Profit)
        Me._SSTab2_TabPage3.Controls.Add(Me.Drawdown)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label12)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label5)
        Me._SSTab2_TabPage3.Controls.Add(Me.MaxProfit)
        Me._SSTab2_TabPage3.Controls.Add(Me.Position)
        Me._SSTab2_TabPage3.Controls.Add(Me.Label14)
        Me._SSTab2_TabPage3.Controls.Add(Me.TheTime)
        Me._SSTab2_TabPage3.Location = New System.Drawing.Point(4, 23)
        Me._SSTab2_TabPage3.Name = "_SSTab2_TabPage3"
        Me._SSTab2_TabPage3.Size = New System.Drawing.Size(737, 223)
        Me._SSTab2_TabPage3.TabIndex = 3
        Me._SSTab2_TabPage3.Text = "Results"
        '
        'MoreButton
        '
        Me.MoreButton.BackColor = System.Drawing.SystemColors.Control
        Me.MoreButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.MoreButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MoreButton.Location = New System.Drawing.Point(440, 32)
        Me.MoreButton.Name = "MoreButton"
        Me.MoreButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MoreButton.Size = New System.Drawing.Size(65, 25)
        Me.MoreButton.TabIndex = 16
        Me.MoreButton.Text = "Less <<<"
        Me.MoreButton.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(8, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Volume"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'VolumeLabel
        '
        Me.VolumeLabel.BackColor = System.Drawing.SystemColors.Window
        Me.VolumeLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.VolumeLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.VolumeLabel.Location = New System.Drawing.Point(136, 80)
        Me.VolumeLabel.Name = "VolumeLabel"
        Me.VolumeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.VolumeLabel.Size = New System.Drawing.Size(49, 17)
        Me.VolumeLabel.TabIndex = 55
        Me.VolumeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BidSizeLabel
        '
        Me.BidSizeLabel.BackColor = System.Drawing.SystemColors.Window
        Me.BidSizeLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.BidSizeLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BidSizeLabel.Location = New System.Drawing.Point(136, 64)
        Me.BidSizeLabel.Name = "BidSizeLabel"
        Me.BidSizeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BidSizeLabel.Size = New System.Drawing.Size(49, 17)
        Me.BidSizeLabel.TabIndex = 2
        Me.BidSizeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BidLabel
        '
        Me.BidLabel.BackColor = System.Drawing.SystemColors.Window
        Me.BidLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.BidLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BidLabel.Location = New System.Drawing.Point(80, 64)
        Me.BidLabel.Name = "BidLabel"
        Me.BidLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BidLabel.Size = New System.Drawing.Size(49, 17)
        Me.BidLabel.TabIndex = 34
        Me.BidLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TradeSizeLabel
        '
        Me.TradeSizeLabel.BackColor = System.Drawing.SystemColors.Window
        Me.TradeSizeLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.TradeSizeLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TradeSizeLabel.Location = New System.Drawing.Point(136, 48)
        Me.TradeSizeLabel.Name = "TradeSizeLabel"
        Me.TradeSizeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TradeSizeLabel.Size = New System.Drawing.Size(49, 17)
        Me.TradeSizeLabel.TabIndex = 40
        Me.TradeSizeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TradeLabel
        '
        Me.TradeLabel.BackColor = System.Drawing.SystemColors.Window
        Me.TradeLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.TradeLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TradeLabel.Location = New System.Drawing.Point(80, 48)
        Me.TradeLabel.Name = "TradeLabel"
        Me.TradeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TradeLabel.Size = New System.Drawing.Size(49, 17)
        Me.TradeLabel.TabIndex = 41
        Me.TradeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AskSizeLabel
        '
        Me.AskSizeLabel.BackColor = System.Drawing.SystemColors.Window
        Me.AskSizeLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.AskSizeLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AskSizeLabel.Location = New System.Drawing.Point(136, 32)
        Me.AskSizeLabel.Name = "AskSizeLabel"
        Me.AskSizeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AskSizeLabel.Size = New System.Drawing.Size(49, 17)
        Me.AskSizeLabel.TabIndex = 51
        Me.AskSizeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AskLabel
        '
        Me.AskLabel.BackColor = System.Drawing.SystemColors.Window
        Me.AskLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.AskLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AskLabel.Location = New System.Drawing.Point(80, 32)
        Me.AskLabel.Name = "AskLabel"
        Me.AskLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AskLabel.Size = New System.Drawing.Size(49, 17)
        Me.AskLabel.TabIndex = 50
        Me.AskLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(248, 152)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(105, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Microsecs per event"
        '
        'MicrosecsPerEventLabel
        '
        Me.MicrosecsPerEventLabel.BackColor = System.Drawing.SystemColors.Window
        Me.MicrosecsPerEventLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.MicrosecsPerEventLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.MicrosecsPerEventLabel.Location = New System.Drawing.Point(360, 152)
        Me.MicrosecsPerEventLabel.Name = "MicrosecsPerEventLabel"
        Me.MicrosecsPerEventLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MicrosecsPerEventLabel.Size = New System.Drawing.Size(57, 13)
        Me.MicrosecsPerEventLabel.TabIndex = 48
        Me.MicrosecsPerEventLabel.Text = " "
        Me.MicrosecsPerEventLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'EventsPerSecondLabel
        '
        Me.EventsPerSecondLabel.BackColor = System.Drawing.SystemColors.Window
        Me.EventsPerSecondLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.EventsPerSecondLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.EventsPerSecondLabel.Location = New System.Drawing.Point(360, 136)
        Me.EventsPerSecondLabel.Name = "EventsPerSecondLabel"
        Me.EventsPerSecondLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.EventsPerSecondLabel.Size = New System.Drawing.Size(57, 13)
        Me.EventsPerSecondLabel.TabIndex = 47
        Me.EventsPerSecondLabel.Text = " "
        Me.EventsPerSecondLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(248, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Events per second"
        '
        'PercentCompleteLabel
        '
        Me.PercentCompleteLabel.BackColor = System.Drawing.SystemColors.Window
        Me.PercentCompleteLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.PercentCompleteLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.PercentCompleteLabel.Location = New System.Drawing.Point(360, 120)
        Me.PercentCompleteLabel.Name = "PercentCompleteLabel"
        Me.PercentCompleteLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PercentCompleteLabel.Size = New System.Drawing.Size(57, 13)
        Me.PercentCompleteLabel.TabIndex = 45
        Me.PercentCompleteLabel.Text = " "
        Me.PercentCompleteLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(248, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Percent complete"
        '
        'EventsPlayedLabel
        '
        Me.EventsPlayedLabel.BackColor = System.Drawing.SystemColors.Window
        Me.EventsPlayedLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.EventsPlayedLabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.EventsPlayedLabel.Location = New System.Drawing.Point(360, 104)
        Me.EventsPlayedLabel.Name = "EventsPlayedLabel"
        Me.EventsPlayedLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.EventsPlayedLabel.Size = New System.Drawing.Size(57, 13)
        Me.EventsPlayedLabel.TabIndex = 43
        Me.EventsPlayedLabel.Text = " "
        Me.EventsPlayedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(248, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Events played"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(8, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Bid"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(8, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(49, 13)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Last"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 32)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Ask"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(248, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Profit/Loss"
        '
        'Profit
        '
        Me.Profit.BackColor = System.Drawing.SystemColors.Window
        Me.Profit.Cursor = System.Windows.Forms.Cursors.Default
        Me.Profit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Profit.Location = New System.Drawing.Point(360, 32)
        Me.Profit.Name = "Profit"
        Me.Profit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Profit.Size = New System.Drawing.Size(57, 13)
        Me.Profit.TabIndex = 28
        Me.Profit.Text = " "
        Me.Profit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Drawdown
        '
        Me.Drawdown.BackColor = System.Drawing.SystemColors.Window
        Me.Drawdown.Cursor = System.Windows.Forms.Cursors.Default
        Me.Drawdown.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Drawdown.Location = New System.Drawing.Point(360, 48)
        Me.Drawdown.Name = "Drawdown"
        Me.Drawdown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Drawdown.Size = New System.Drawing.Size(57, 13)
        Me.Drawdown.TabIndex = 27
        Me.Drawdown.Text = " "
        Me.Drawdown.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(248, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(57, 13)
        Me.Label12.TabIndex = 26
        Me.Label12.Text = "Drawdown"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(248, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Max profit"
        '
        'MaxProfit
        '
        Me.MaxProfit.BackColor = System.Drawing.SystemColors.Window
        Me.MaxProfit.Cursor = System.Windows.Forms.Cursors.Default
        Me.MaxProfit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.MaxProfit.Location = New System.Drawing.Point(360, 64)
        Me.MaxProfit.Name = "MaxProfit"
        Me.MaxProfit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MaxProfit.Size = New System.Drawing.Size(57, 13)
        Me.MaxProfit.TabIndex = 24
        Me.MaxProfit.Text = " "
        Me.MaxProfit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Position
        '
        Me.Position.BackColor = System.Drawing.SystemColors.Window
        Me.Position.Cursor = System.Windows.Forms.Cursors.Default
        Me.Position.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Position.Location = New System.Drawing.Point(360, 80)
        Me.Position.Name = "Position"
        Me.Position.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Position.Size = New System.Drawing.Size(57, 13)
        Me.Position.TabIndex = 23
        Me.Position.Text = " "
        Me.Position.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(248, 80)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(57, 13)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "Position"
        '
        'TheTime
        '
        Me.TheTime.BackColor = System.Drawing.SystemColors.Window
        Me.TheTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.TheTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TheTime.Location = New System.Drawing.Point(79, 104)
        Me.TheTime.Name = "TheTime"
        Me.TheTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TheTime.Size = New System.Drawing.Size(121, 17)
        Me.TheTime.TabIndex = 21
        Me.TheTime.Text = " "
        '
        'fStrategyHost
        '
        Me.AcceptButton = Me.StartButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(736, 615)
        Me.Controls.Add(Me.SSTab1)
        Me.Controls.Add(Me.SSTab2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(4, 30)
        Me.Name = "fStrategyHost"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "TradeBuild Strategy Host v2.7"
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.PriceChartToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.PriceChartToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.PriceChartToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.PriceChartToolStripContainer.ResumeLayout(False)
        Me.PriceChartToolStripContainer.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        CType(Me.ProfitChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        CType(Me.TradeChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        Me.SSTab2.ResumeLayout(False)
        Me._SSTab2_TabPage0.ResumeLayout(False)
        Me._SSTab2_TabPage0.PerformLayout()
        CType(Me.TickfileOrganiser1, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab2_TabPage1.ResumeLayout(False)
        CType(Me.ParamGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab2_TabPage2.ResumeLayout(False)
        Me._SSTab2_TabPage2.PerformLayout()
        Me._SSTab2_TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PriceChartToolStripContainer As ToolStripContainer
#End Region
End Class