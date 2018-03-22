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

Imports System.Runtime.InteropServices.Marshal
Imports System.ComponentModel

Imports ChartSkil27
Imports StudyUtils27
Imports TimeframeUtils27

Imports TradeWright.Trading.Utils.Charts.BarFormatters
Imports TradeWright.Trading.Utils.Charts
Imports TradeWright.Trading.Utils.Contracts
Imports TradeWright.Trading.UI.Studies
Imports TWUtilities40

Public Class MarketChart
    Inherits AxChartSkil27.AxChart

    Private components As System.ComponentModel.IContainer
    Private WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Private WithEvents AddStudyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents EditStudyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents RemoveStudyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ConfigureChartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents LoadingProgressBar As System.Windows.Forms.ProgressBar

#Region "Constructors"

    Public Sub New()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MarketChart))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddStudyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditStudyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveStudyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ConfigureChartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadingProgressBar = New System.Windows.Forms.ProgressBar
        'MyBase.BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        'Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddStudyToolStripMenuItem, Me.EditStudyToolStripMenuItem, Me.RemoveStudyToolStripMenuItem, Me.ToolStripSeparator1, Me.ConfigureChartToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(178, 120)
        '
        'AddStudyToolStripMenuItem
        '
        Me.AddStudyToolStripMenuItem.Name = "AddStudyToolStripMenuItem"
        Me.AddStudyToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.AddStudyToolStripMenuItem.Text = "Add a study"
        '
        'EditStudyToolStripMenuItem
        '
        Me.EditStudyToolStripMenuItem.Name = "EditStudyToolStripMenuItem"
        Me.EditStudyToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.EditStudyToolStripMenuItem.Text = "Edit a study"
        '
        'RemoveStudyToolStripMenuItem
        '
        Me.RemoveStudyToolStripMenuItem.Name = "RemoveStudyToolStripMenuItem"
        Me.RemoveStudyToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RemoveStudyToolStripMenuItem.Text = "Remove a study"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(174, 6)
        '
        'ConfigureChartToolStripMenuItem
        '
        Me.ConfigureChartToolStripMenuItem.Name = "ConfigureChartToolStripMenuItem"
        Me.ConfigureChartToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ConfigureChartToolStripMenuItem.Text = "Configure the chart"
        '
        'LoadingProgressBar
        '
        Me.LoadingProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LoadingProgressBar.Location = New System.Drawing.Point(0, 0)
        'Me.LoadingProgressBar.Location = New System.Drawing.Point(0, Me.Height - LoadingProgressBar.Height)
        Me.LoadingProgressBar.Name = "LoadingProgressBar"
        Me.LoadingProgressBar.Size = New System.Drawing.Size(Me.Width, 19)
        Me.LoadingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.LoadingProgressBar.TabIndex = 2
        Me.LoadingProgressBar.Visible = False
        Me.LoadingProgressBar.BringToFront()
        '
        'MarketChart
        '
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Name = "TBChart"
        Me.Size = New System.Drawing.Size(650, 502)
        Me.Controls.Add(LoadingProgressBar)
        Me.ContextMenuStrip1.ResumeLayout(False)
        'MyBase.EndInit()
        'Me.ResumeLayout(False)

        If mAvailableStudies IsNot Nothing Then
            For Each studyEntry In mAvailableStudies
                Dim tsmi = New ToolStripMenuItem(studyEntry.Name & " (" & studyEntry.StudyLibrary & ")")
                AddStudyToolStripMenuItem.DropDownItems.Add(tsmi)
                tsmi.Tag = studyEntry
                tsmi.Size = New System.Drawing.Size(200, 22)
                AddHandler tsmi.Click, AddressOf AddStudyToolStripMenuItem_Click
            Next
        End If

        setState(ChartState.Blank)

        AddHandler MyBase.ClickEvent, Sub(s, e) OnClick(EventArgs.Empty)
        AddHandler MyBase.DblCLick, Sub(s, e) OnDoubleClick(EventArgs.Empty)
        AddHandler MyBase.KeyDownEvent, Sub(s, e) OnKeyDown(New KeyEventArgs(getKeyValue(e.keyCode, e.shift)))
        AddHandler MyBase.KeyPressEvent, Sub(s, e) OnKeyPress(New KeyPressEventArgs(Microsoft.VisualBasic.ChrW(e.keyAscii)))
        AddHandler MyBase.KeyUpEvent, Sub(s, e) OnKeyUp(New KeyEventArgs(getKeyValue(e.keyCode, e.shift)))
        AddHandler MyBase.MouseDownEvent, Sub(s, e) OnMouseDown(New MouseEventArgs(getMouseButton(DirectCast(CInt(e.button), VB6MouseButtonConstants)), 1, CInt(e.x), CInt(e.y), 0))
        AddHandler MyBase.MouseMoveEvent, Sub(s, e) OnMouseMove(New MouseEventArgs(getMouseButton(DirectCast(CInt(e.button), VB6MouseButtonConstants)), 1, CInt(e.x), CInt(e.y), 0))
        AddHandler MyBase.MouseUpEvent, Sub(s, e) OnMouseUp(New MouseEventArgs(getMouseButton(DirectCast(CInt(e.button), VB6MouseButtonConstants)), 1, CInt(e.x), CInt(e.y), 0))
    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#End Region

#Region "Events"

    Public Shadows Event Click(sender As Object, e As EventArgs)
    Public Shadows Event DoubleClick(sender As Object, e As EventArgs)

    Public Shadows Event KeyDown(sender As Object, e As KeyEventArgs)
    Public Shadows Event KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
    Public Shadows Event KeyUp(sender As Object, e As KeyEventArgs)

    Public Shadows Event MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    Public Shadows Event MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    Public Shadows Event MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)

    Public Event StateChange(ev As StateChangeEventData)

    Public Event TickerChange(sender As Object, ev As System.EventArgs)
    Public Event TimePeriodChange(sender As Object, ev As System.EventArgs)

#End Region

#Region "Enums"

    ''' <summary>
    ''' Represents the current state of the chart.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ChartState
        ''' <summary>
        ''' No chart has yet been started, or a chart has been cleared.
        ''' </summary>
        Blank

        ''' <summary>
        ''' The chart has been created, but the ticker has not yet become ready.
        ''' </summary>
        Created

        ''' <summary>
        ''' The ticker is now ready and historical data has been requested.
        ''' </summary>
        Initialised

        ''' <summary>
        ''' Historic data has been fetched and is being added to the chart.
        ''' </summary>
        Loading

        ''' <summary>
        ''' All historic data has been added to the chart.
        ''' </summary>
        Loaded
    End Enum

    <Flags>
    Private Enum VB6MouseButtonConstants
        LeftButton = 1
        RightButton = 2
        MiddleButton = 4
    End Enum

    <Flags>
    Private Enum VB6ShiftConstants
        AltMask = 4
        CtrlMask = 2
        ShiftMask = 1
    End Enum

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "MarketChart"

    Private Const ConfigSectionChartControl As String = "ChartControl"
    Private Const ConfigSectionChartSpecifier As String = "ChartSpecifier"
    Private Const ConfigSectionStudies As String = "Studies"

    Private Const ConfigSettingBarFormatterFactoryName As String = "&BarFormatterFactoryName"
    Private Const ConfigSettingBarFormatterLibraryName As String = "&BarFormatterLibraryName"
    Private Const ConfigSettingID As String = "&ID"
    Private Const ConfigSettingIsHistoricChart As String = "&IsHistoricChart"
    Private Const ConfigSettingTimePeriod As String = "&TimePeriod"
    Private Const ConfigSettingTickerKey As String = "&TickerKey"
    Private Const ConfigSettingWorkspace As String = "&Workspace"

#End Region

#Region "Shared variables"

    Private Shared mAvailableStudies() As StudyListEntry

#End Region

#Region "Member variables"

    Private mID As String

    Private WithEvents mChartManager As ChartManager

    Private mTimeframes As Timeframes
    Private WithEvents mTimeframe As Timeframe

    Private mTimePeriod As TimePeriod

    Private mUpdatePerTick As Boolean = True

    Private mState As ChartState

    Private mIsHistoricChart As Boolean

    Private mChartSpec As ChartSpecifier
    Private mChartStyle As ChartStyle

    Private mLocalSymbol As String
    Private mSecType As SecurityType
    Private mExchange As String
    Private mTickSize As Double
    Private mSessionEndTime As TimeSpan
    Private mSessionStartTime As TimeSpan

    Private mPriceRegion As ChartRegion
    Private mVolumeRegion As ChartRegion

    Private mStatusText As Text

    Private mStudyManager As StudyManager
    Private mBarFormatterLibManager As BarFormatterLibManager

    Private mBarFormatterFactoryName As String
    Private mBarFormatterLibraryName As String

    Private mConfig As ConfigurationSection
    Private mLoadedFromConfig As Boolean

    Private mDeferStart As Boolean

    Private mMinimumTicksHeight As Integer

    Private mInitialised As Boolean

    Private mExcludeCurrentBar As Boolean

    Private WithEvents mFutureWaiter As New FutureWaiter

    Private mTitle As String

    Private mIsRaw As Boolean

    Private mPreviousBaseStudyConfig As StudyConfiguration

#End Region

#Region "Control Event Handlers"

    Private Sub AddStudyToolStripMenuItem_Click(sender As Object, e As System.EventArgs)
        Dim studyEntry = DirectCast((DirectCast(sender, ToolStripMenuItem)).Tag, StudyListEntry)
        Dim defaultStudyConfig = ChartUtils.GetDefaultStudyConfiguration(studyEntry.Name, mStudyManager.StudyLibraryManager, studyEntry.StudyLibrary)

        addStudyToChart(defaultStudyConfig)
    End Sub

    Private Sub EditStudyToolStripMenuItem_Click(sender As Object, e As System.EventArgs)
        Dim tsmi = DirectCast(sender, ToolStripMenuItem)
        Dim studyConfig = DirectCast(tsmi.Tag, StudyConfiguration)
        Dim newStudyConfig = StudiesUI.ShowConfigForm(mChartManager, studyConfig.Name, studyConfig.StudyLibraryName, studyConfig)
        If newStudyConfig IsNot Nothing Then

            If studyConfig.Study Is mChartManager.BaseStudy Then
                newStudyConfig.Study = studyConfig.Study
                mChartManager.SetBaseStudyConfiguration(newStudyConfig)
            Else
                mChartManager.ReplaceStudyConfiguration(studyConfig, newStudyConfig)
            End If
        End If
    End Sub

    Private Sub RemoveStudyToolStripMenuItem_Click(sender As Object, e As System.EventArgs)
        Dim tsmi = DirectCast(sender, ToolStripMenuItem)
        Dim studyConfig = DirectCast(tsmi.Tag, StudyConfiguration)
        mChartManager.RemoveStudyConfiguration(studyConfig)
    End Sub

#End Region

#Region "mChartManager Event Handlers"

    Private Sub mChartManager_StudyAdded(sender As Object, study As _IStudy) Handles mChartManager.StudyAdded
        If study IsNot mChartManager.BaseStudy Then setupRemoveStudyToolStripMenuItem(study)
        setupEditStudyToolStripMenuItem(study)
    End Sub

    Private Sub mChartManager_StudyRemoved(sender As Object, study As _IStudy) Handles mChartManager.StudyRemoved
        For Each tsi As ToolStripMenuItem In EditStudyToolStripMenuItem.DropDownItems
            If DirectCast(tsi.Tag, StudyConfiguration).Study Is study Then
                EditStudyToolStripMenuItem.DropDownItems.Remove(tsi)
                Exit For
            End If
        Next

        For Each tsi As ToolStripMenuItem In RemoveStudyToolStripMenuItem.DropDownItems
            If DirectCast(tsi.Tag, StudyConfiguration).Study Is study Then
                RemoveStudyToolStripMenuItem.DropDownItems.Remove(tsi)
                Exit For
            End If
        Next
    End Sub

#End Region

#Region "mFutureWaiter Event Handlers"

    Private Sub mFutureWaiter_WaitCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitCompleted
        Try
            If ev.Future.IsAvailable Then
                ' this means that the contract info is available, so we can
                ' now start the chart

                setContractProperties(DirectCast(mTimeframes.ContractFuture.Value, IContract))

                If mDeferStart Then Exit Sub

                setupStudies()
                loadchart()
            End If
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mFutureWaiter_WaitCompleted), NameOf(MarketChart))
        End Try
    End Sub

#End Region

#Region "mTimeframe Event Handlers"

    ' The following is commented out because Timeframe no longer has a BarLoadProgress event. We need to find a new way
    ' of generating this information
    'Private Sub mTimeframe_BarLoadProgress(barsRetrieved As Integer, percentComplete As Single) Handles mTimeframe.BarLoadProgress
    '    If Not LoadingProgressBar.Visible Then
    '        LoadingProgressBar.Visible = True
    '        mStatusText.Text = "Loading historical data"
    '        setState(ChartStates.ChartStateLoading)
    '        MyBase.EnableDrawing()
    '        MyBase.DisableDrawing()
    '    End If
    '    LoadingProgressBar.Value = CInt(percentComplete)
    'End Sub

    Private Sub mTimeframe_BarsLoaded() Handles mTimeframe.BarsLoaded
        Try
            LoadingProgressBar.Visible = False
            mStatusText.Text = ""
            MyBase.EnableDrawing()

            setState(ChartState.Loaded)
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mTimeframe_BarsLoaded), NameOf(MarketChart))
        End Try
    End Sub

#End Region

#Region "Properties"

    <Browsable(False)>
    Public ReadOnly Property ChartManager() As ChartManager
        Get
            Return mChartManager
        End Get
    End Property

    <Browsable(False)>
    Public Overrides WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If Value Is mConfig Then Exit Property
            If mConfig IsNot Nothing Then mConfig.Remove()
            mConfig = Value
            If mConfig Is Nothing Then Return

            storeSettings(mConfig)

            MyBase.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionChartControl)
            If Not mChartManager Is Nothing Then mChartManager.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionStudies)
        End Set
    End Property

    <Browsable(False)>
    Public Property ID As String
        Get
            Return mID
        End Get
        Friend Set
            mID = Value
        End Set

    End Property

    <Browsable(False)>
    Public ReadOnly Property InitialNumberOfBars() As Integer
        Get
            Return mChartSpec.InitialNumberOfBars
        End Get
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies the minimum number of ticks which the price area of the chart will display")>
    Public Property MinimumTicksHeight() As Integer
        Get
            Return mMinimumTicksHeight
        End Get
        Set
            mMinimumTicksHeight = Value
            If mMinimumTicksHeight * mTickSize <> 0 Then
                mPriceRegion.MinimumHeight = mMinimumTicksHeight * mTickSize
            End If
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property PriceRegion() As ChartRegion
        Get
            Return mPriceRegion
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property RegionNames() As String()
        Get
            Return mChartManager.RegionNames
        End Get
    End Property

    <Browsable(False)>
    Public Shadows ReadOnly Property State() As ChartState
        Get
            Return mState
        End Get
    End Property

    Public ReadOnly Property StatusText() As Text
        Get
            Return mStatusText
        End Get
    End Property

    <Browsable(False)>
    Public WriteOnly Property StudyManager As StudyManager
        Set
            setStudyManager(Value)
            mChartManager.Finish()
            mChartManager = New ChartManager(mStudyManager, Me, mBarFormatterLibManager, False)
            mChartManager.UpdatePerTick = mUpdatePerTick
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property TimeframeCaption() As String
        Get
            Return mTimePeriod.ToString
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property TimeframeShortCaption() As String
        Get
            Return mTimePeriod.ToShortString
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property Timeframe() As Timeframe
        Get
            Return mTimeframe
        End Get
    End Property

    <Browsable(False)>
    Public Shadows ReadOnly Property TimePeriod() As TimePeriod
        Get
            Return mTimePeriod
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property TradeBarSeries() As BarSeries
        Get
            TradeBarSeries = DirectCast(mChartManager.BaseStudyConfiguration.ValueSeries(StudyUtils.BarStudyValueBar), BarSeries)
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property VolumeRegion() As ChartRegion
        Get
            Return mVolumeRegion
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub ChangeTicker(pTimeframes As Timeframes, deferStart As Boolean)
        gDiagLogger.Log("Changing ticker for chart", NameOf(ChangeTicker), ModuleName)

        If mIsRaw Then Throw New InvalidOperationException("Already initialised as raw")
        mTimeframes = pTimeframes
        setStudyManager(mTimeframes.StudyBase.StudyManager)

        If State = ChartState.Blank Then Exit Sub

        mDeferStart = deferStart

        mPreviousBaseStudyConfig = mChartManager.BaseStudyConfiguration

        'ReleaseComObject(mPriceRegion)
        'If mVolumeRegion IsNot Nothing Then ReleaseComObject(mVolumeRegion)

        mChartManager.ClearChart()

        setState(ChartState.Blank)

        initialiseChart(mChartSpec.IncludeBarsOutsideSession)
        If Not mDeferStart Then
            createTimeframe()
            prepareChart()
        End If

        RaiseEvent TickerChange(Me, EventArgs.Empty)
    End Sub

    Public Sub ChangeTimeframe(newTimePeriod As TimePeriod)
        gDiagLogger.Log($"Changing timeframe for chart to {newTimePeriod.ToString()}", NameOf(ChangeTimeframe), ModuleName)

        If State <> ChartState.Loaded Then Throw New InvalidOperationException("Can't change timeframe until chart is loaded")

        Dim baseStudyConfig = mChartManager.BaseStudyConfiguration

        'ReleaseComObject(mPriceRegion)
        'If mVolumeRegion IsNot Nothing Then ReleaseComObject(mVolumeRegion)

        mChartManager.ClearChart()

        setState(ChartState.Blank)

        mTimePeriod = newTimePeriod
        mConfig?.SetSetting(ConfigSettingTimePeriod, mTimePeriod.ToString)

        createTimeframe()

        baseStudyConfig.Study = CType(mTimeframe.BarStudy, IStudy)
        baseStudyConfig.StudyValueConfigurations.Item(StudyUtils.BarStudyValueBar).BarFormatterFactoryName = mBarFormatterFactoryName
        baseStudyConfig.StudyValueConfigurations.Item(StudyUtils.BarStudyValueBar).BarFormatterLibraryName = mBarFormatterLibraryName
        baseStudyConfig.Parameters = CType(mTimeframe.BarStudy, IStudy).Parameters

        initialiseChart(mChartSpec.IncludeBarsOutsideSession)
        mChartManager.SetBaseStudyConfiguration(baseStudyConfig)

        loadchart()

        RaiseEvent TimePeriodChange(Me, EventArgs.Empty)
    End Sub

    Public Shadows Sub Finish()
        If mChartManager IsNot Nothing Then mChartManager.Finish()
        '        If mTimeframes IsNot Nothing Then ReleaseComObject(mTimeframes)
        '        If mTimeframe IsNot Nothing Then ReleaseComObject(mTimeframe)

        mLocalSymbol = ""
        mSecType = SecurityType.None
        mExchange = ""
        mTickSize = 0#
        mSessionEndTime = StartOfDay
        mSessionStartTime = StartOfDay

        'If mPriceRegion IsNot Nothing Then ReleaseComObject(mPriceRegion)
        'If mVolumeRegion IsNot Nothing Then ReleaseComObject(mVolumeRegion)
        'If mStatusText IsNot Nothing Then ReleaseComObject(mStatusText)

        'MyBase.Finish()
        'MyBase.Dispose()
    End Sub

    Public Sub Initialise(
                pTimeframes As Timeframes,
                pUpdatePerTick As Boolean)
        If mIsRaw Then Throw New InvalidOperationException("Already initialised as raw")
        mTimeframes = pTimeframes
        mUpdatePerTick = pUpdatePerTick
    End Sub

    Public Sub InitialiseRaw(
                pStudyManager As StudyManager,
                pUpdatePerTick As Boolean)
        mIsRaw = True
        setStudyManager(pStudyManager)
        mUpdatePerTick = pUpdatePerTick
    End Sub

    Public Shadows Sub LoadFromConfig(
                pTimeframes As Timeframes,
                config As ConfigurationSection,
                pBarFormatterLibManager As BarFormatterLibManager,
                deferStart As Boolean)
        gDiagLogger.Log($"Loading chart from config {config.InstanceQualifier}", NameOf(LoadFromConfig), ModuleName)

        mConfig = config

        mDeferStart = deferStart

        mTimeframes = pTimeframes
        setStudyManager(mTimeframes.StudyBase.StudyManager)
        mBarFormatterLibManager = pBarFormatterLibManager

        loadConfig(mConfig)

        initialiseChart(mChartSpec.IncludeBarsOutsideSession)
        If Not mDeferStart Then
            createTimeframe()
            prepareChart()
        End If
    End Sub

    Public Sub LoadFromTemplate(pTimeframes As Timeframes,
                config As ConfigurationSection,
                pBarFormatterLibManager As BarFormatterLibManager)
        gDiagLogger.Log($"Loading chart from template {config.InstanceQualifier}", NameOf(LoadFromTemplate), ModuleName)

        If mConfig IsNot Nothing Then
            Dim parentConfig = mConfig.Parent
            mConfig.Remove()
            parentConfig.CloneConfigSection(config)
            mConfig = parentConfig.GetConfigurationSection(config.Name)
        End If

        mTimeframes = pTimeframes
        setStudyManager(mTimeframes.StudyBase.StudyManager)
        mBarFormatterLibManager = pBarFormatterLibManager

        loadConfig(config)
    End Sub

    Public Overridable Shadows Sub OnClick(e As EventArgs)
        RaiseEvent Click(Me, e)
    End Sub

    Public Overridable Shadows Sub OnDoubleClick(e As EventArgs)
        RaiseEvent DoubleClick(Me, e)
    End Sub

    Public Overridable Shadows Sub OnKeyDown(e As KeyEventArgs)
        RaiseEvent KeyDown(Me, e)
    End Sub

    Public Overridable Shadows Sub OnKeyPress(e As KeyPressEventArgs)
        RaiseEvent KeyPress(Me, e)
    End Sub

    Public Overridable Shadows Sub OnKeyUp(e As KeyEventArgs)
        RaiseEvent KeyUp(Me, e)
    End Sub

    Public Overridable Shadows Sub OnMouseDown(e As MouseEventArgs)
        RaiseEvent MouseDown(Me, e)
    End Sub

    Public Overridable Shadows Sub OnMouseMove(e As MouseEventArgs)
        RaiseEvent MouseMove(Me, e)
    End Sub

    Public Overridable Shadows Sub OnMouseUp(e As MouseEventArgs)
        RaiseEvent MouseUp(Me, e)
    End Sub

    Public Shadows Sub RemoveFromConfig()
        If mConfig Is Nothing Then Exit Sub
        gDiagLogger.Log($"Chart removed from config at: {mConfig.Path}", NameOf(RemoveFromConfig), ModuleName)
        mConfig.Remove()
        'ReleaseComObject(mConfig)
        mConfig = Nothing
    End Sub

    Public Sub SaveAsTemplate(config As ConfigurationSection)
        config.CloneConfigSection(mConfig)
    End Sub

    Public Sub ScrollToTime(time As Date)
        mChartManager.ScrollToTime(time)
    End Sub

    Public Sub ShowChart(
                pTimePeriod As TimePeriod,
                pChartSpec As ChartSpecifier,
                pChartStyle As ChartStyle,
                Optional pBarFormatterLibManager As BarFormatterLibManager = Nothing,
                Optional pBarFormatterFactoryName As String = "",
                Optional pBarFormatterLibraryName As String = "",
                Optional pExcludeCurrentBar As Boolean = False,
                Optional pTitle As String = "")
        If pBarFormatterFactoryName <> "" And pBarFormatterLibManager Is Nothing Then Throw New ArgumentException("If pBarFormatterFactoryName is not blank then pBarFormatterLibManager must be supplied")
        If pBarFormatterLibraryName <> "" And pBarFormatterLibManager Is Nothing Then Throw New ArgumentException("If pBarFormatterLibraryName is not blank then pBarFormatterLibManager must be supplied")
        If (pBarFormatterLibraryName <> "" And pBarFormatterFactoryName = "") Or (pBarFormatterLibraryName = "" And pBarFormatterFactoryName <> "") Then Throw New ArgumentException("pBarFormatterLibraryName and pBarFormatterFactoryName must both be blank or non-blank")

        setState(ChartState.Blank)

        If Not mTimeframes Is Nothing Then
            mInitialised = False
            ClearChart()
        End If

        setStudyManager(mTimeframes.StudyBase.StudyManager)
        mBarFormatterLibManager = pBarFormatterLibManager

        mTimePeriod = pTimePeriod
        mChartSpec = pChartSpec
        mChartStyle = pChartStyle
        mBarFormatterFactoryName = pBarFormatterFactoryName
        mBarFormatterLibraryName = pBarFormatterLibraryName
        mExcludeCurrentBar = pExcludeCurrentBar
        mTitle = pTitle

        storeSettings(mConfig)
        createTimeframe()
        initialiseChart(mChartSpec.IncludeBarsOutsideSession)
        prepareChart()
    End Sub

    Public Sub ShowChartRaw(
                pTimeframe As Timeframe,
                pChartStyle As ChartStyle,
                Optional pLocalSymbol As String = "",
                Optional pSecType As SecurityType = SecurityType.None,
                Optional pExchange As String = "",
                Optional pTickSize As Double = 0.0,
                Optional pBarFormatterLibManager As BarFormatterLibManager = Nothing,
                Optional pBarFormatterFactoryName As String = "",
                Optional pBarFormatterLibraryName As String = "",
                Optional pTitle As String = "")
        ShowChartRaw(pTimeframe,
                     pChartStyle,
                     StartOfDay,
                     StartOfDay,
                     pLocalSymbol,
                     pSecType,
                     pExchange,
                     pTickSize,
                     pBarFormatterLibManager,
                     pBarFormatterFactoryName,
                     pBarFormatterLibraryName)
    End Sub

    Public Sub ShowChartRaw(
                pTimeframe As Timeframe,
                pChartStyle As ChartStyle,
                pSessionStartTime As TimeSpan,
                pSessionEndTime As TimeSpan,
                Optional pLocalSymbol As String = "",
                Optional pSecType As SecurityType = SecurityType.None,
                Optional pExchange As String = "",
                Optional pTickSize As Double = 0.0,
                Optional pBarFormatterLibManager As BarFormatterLibManager = Nothing,
                Optional pBarFormatterFactoryName As String = "",
                Optional pBarFormatterLibraryName As String = "",
                Optional pTitle As String = "")
        If pBarFormatterFactoryName <> "" And pBarFormatterLibManager Is Nothing Then Throw New ArgumentException("If pBarFormatterFactoryName is not blank then pBarFormatterLibManager must be supplied")
        If pBarFormatterLibraryName <> "" And pBarFormatterLibManager Is Nothing Then Throw New ArgumentException("If pBarFormatterLibraryName is not blank then pBarFormatterLibManager must be supplied")
        If (pBarFormatterLibraryName <> "" And pBarFormatterFactoryName = "") Or (pBarFormatterLibraryName = "" And pBarFormatterFactoryName <> "") Then Throw New ArgumentException("pBarFormatterLibraryName and pBarFormatterFactoryName must both be blank or non-blank")

        setState(ChartState.Blank)

        mBarFormatterLibManager = pBarFormatterLibManager

        mTimeframe = pTimeframe
        mTimePeriod = mTimeframe.TimePeriod
        mChartStyle = pChartStyle
        mLocalSymbol = pLocalSymbol
        mSecType = pSecType
        mExchange = pExchange
        mTickSize = pTickSize
        mSessionEndTime = pSessionEndTime
        MyBase.SessionEndTime = COMStartOfDay + mSessionEndTime
        mSessionStartTime = pSessionStartTime
        MyBase.SessionStartTime = COMStartOfDay + mSessionStartTime
        mBarFormatterFactoryName = pBarFormatterFactoryName
        mBarFormatterLibraryName = pBarFormatterLibraryName
        mTitle = pTitle

        initialiseChart(False)
        prepareChart()
    End Sub

    Public Sub Start()
        If Not (mDeferStart And mState = ChartState.Created) Then Exit Sub
        mDeferStart = False
        createTimeframe()
        prepareChart()
    End Sub

    Public Sub UpdateLastBar()
        mChartManager.UpdateLastBar()
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub addStudyToChart(studyConfig As StudyConfiguration)
        mChartManager.AddStudyConfiguration(studyConfig)
    End Sub

    Private Function createPriceFormatter() As ChartSkil27.IPriceFormatter
        If mTickSize = 0# Then
            createPriceFormatter = New ChartPriceFormatter(SecurityType.None, 0.0001)
        Else
            createPriceFormatter = New ChartPriceFormatter(mSecType, mTickSize)
        End If
    End Function

    Private Sub createTimeframe()
        'If mTimeframe IsNot Nothing Then ReleaseComObject(mTimeframe)

        gDiagLogger.Log(String.Format("Creating timeframe: {0}", mTimePeriod.ToString), "createTimeframe()", ModuleName)

        If mChartSpec.ToTime <> Date.MinValue Then
            mTimeframe = mTimeframes.AddHistorical(mTimePeriod,
                                                "",
                                                mChartSpec.InitialNumberOfBars,
                                                mChartSpec.FromTime,
                                                mChartSpec.ToTime,
                                                ,
                                                mChartSpec.IncludeBarsOutsideSession)
        Else
            mTimeframe = mTimeframes.Add(mTimePeriod,
                                        "",
                                        mChartSpec.InitialNumberOfBars,
                                         mChartSpec.FromTime,
                                         ,
                                        mChartSpec.IncludeBarsOutsideSession,
                                        mExcludeCurrentBar)
        End If
    End Sub

    Private Function getKeyValue(keyCode As Short, shift As Short) As System.Windows.Forms.Keys
        Dim ctrlPressed = (shift And VB6ShiftConstants.CtrlMask) = VB6ShiftConstants.CtrlMask
        Dim shiftPressed = (shift And VB6ShiftConstants.ShiftMask) = VB6ShiftConstants.ShiftMask
        Dim altPressed = (shift And VB6ShiftConstants.AltMask) = VB6ShiftConstants.AltMask
        Dim keyVal As System.Windows.Forms.Keys = DirectCast(CInt(keyCode), System.Windows.Forms.Keys)
        If ctrlPressed Then keyVal = keyVal Or System.Windows.Forms.Keys.Control
        If shiftPressed Then keyVal = keyVal Or System.Windows.Forms.Keys.Shift
        If altPressed Then keyVal = keyVal Or System.Windows.Forms.Keys.Alt
        Return keyVal
    End Function

    Private Function getMouseButton(VB6button As VB6MouseButtonConstants) As System.Windows.Forms.MouseButtons
        Select Case VB6button
            Case VB6MouseButtonConstants.LeftButton
                Return MouseButtons.Left
            Case VB6MouseButtonConstants.MiddleButton
                Return MouseButtons.Middle
            Case VB6MouseButtonConstants.RightButton
                Return MouseButtons.Right
            Case Else
                Return MouseButtons.None
        End Select
    End Function

    Private Sub initialiseChart(pIncludeBarsOutsideSession As Boolean)
        gDiagLogger.Log($"Initialising chart: {mTimePeriod.ToString()}", NameOf(initialiseChart), ModuleName)

        RemoveStudyToolStripMenuItem.DropDownItems.Clear()
        EditStudyToolStripMenuItem.DropDownItems.Clear()

        DisableDrawing()

        If Not mInitialised Then
            mChartManager = New ChartManager(mStudyManager, Me, mBarFormatterLibManager, pIncludeBarsOutsideSession)
            mChartManager.UpdatePerTick = mUpdatePerTick
            If mConfig IsNot Nothing Then mChartManager.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionStudies)

            If mChartStyle Is Nothing Then
                gDiagLogger.Log("No chart style is defined", NameOf(initialiseChart), ModuleName)
            Else
                gDiagLogger.Log(String.Format("Setting chart style to {0}", mChartStyle.Name), NameOf(initialiseChart), ModuleName)
                MyBase.Style = mChartStyle
            End If

            mInitialised = True
        End If

        mPriceRegion = Regions.Add(100, 25, , , ChartUtils.ChartRegionNamePrice)

        setStatusText()
        EnableDrawing()

        setState(ChartState.Created)
    End Sub

    Private Sub loadchart()
        gDiagLogger.Log("Loading chart", "loadchart()", ModuleName)
        MyBase.DisableDrawing()

        MyBase.TimePeriod = mTimePeriod

        MyBase.SessionStartTime = COMStartOfDay + mSessionStartTime
        MyBase.SessionEndTime = COMStartOfDay + mSessionEndTime

        If mTickSize = 0# Then
            mPriceRegion.YScaleQuantum = 0.001
        Else
            mPriceRegion.YScaleQuantum = mTickSize
            If mMinimumTicksHeight * mTickSize <> 0 Then
                mPriceRegion.MinimumHeight = mMinimumTicksHeight * mTickSize
            End If
        End If

        mPriceRegion.PriceFormatter = createPriceFormatter()

        If mTitle <> "" Then
            mPriceRegion.Title.Text = mTitle
        ElseIf mLocalSymbol <> "" Then
            mPriceRegion.Title.Text = mLocalSymbol & " (" & mExchange & ") " & TimeframeCaption
        End If
        mPriceRegion.Title.Color = ColorTranslator.ToOle(Color.Blue)

        If mSecType = SecurityType.None Then
        ElseIf mSecType <> SecurityType.Cash And mSecType <> SecurityType.Index Then
            If MyBase.Regions.Contains(ChartUtils.ChartRegionNameVolume) Then
                mVolumeRegion = MyBase.Regions.Item(ChartUtils.ChartRegionNameVolume)
            Else
                mVolumeRegion = MyBase.Regions.Add(20, , , , ChartUtils.ChartRegionNameVolume)
            End If

            mVolumeRegion.MinimumHeight = 10
            mVolumeRegion.IntegerYScale = True
            mVolumeRegion.Title.Text = "Volume"
            mVolumeRegion.Title.Color = ColorTranslator.ToOle(Color.Blue)
        End If

        If mIsRaw Then
            MyBase.EnableDrawing()
            setState(ChartState.Initialised)
            mStatusText.Text = ""
            setState(ChartState.Loaded)
        ElseIf mTimeframe.State <> TimeframeStates.TimeframeStateLoaded Then
            mStatusText.Text = "Fetching historical data"
            setState(ChartState.Initialised)
            MyBase.EnableDrawing()    ' causes the loading text to appear
            MyBase.DisableDrawing()
        Else
            MyBase.EnableDrawing()
            setState(ChartState.Initialised)
            mStatusText.Text = ""
            setState(ChartState.Loaded)
        End If
    End Sub

    Private Sub loadConfig(config As ConfigurationSection)
        mLoadedFromConfig = True

        mTimePeriod = TWUtilities.TimePeriodFromString(config.GetSetting(ConfigSettingTimePeriod))
        gDiagLogger.Log(String.Format("Chart time period is {0}", mTimePeriod.ToString), "loadConfig()", ModuleName)

        mChartSpec = ChartSpecifier.LoadFromConfiguration(config.GetConfigurationSection(ConfigSectionChartSpecifier))

        MyBase.LoadFromConfig(config.AddConfigurationSection(ConfigSectionChartControl))

        mIsHistoricChart = CBool(config.GetSetting(ConfigSettingIsHistoricChart, "False"))
        mBarFormatterFactoryName = config.GetSetting(ConfigSettingBarFormatterFactoryName, "")
        mBarFormatterLibraryName = config.GetSetting(ConfigSettingBarFormatterLibraryName, "")
    End Sub

    Private Sub loadStudiesFromConfig(config As ConfigurationSection)
        mChartManager.LoadFromConfig(config.AddConfigurationSection(ConfigSectionStudies), DirectCast(mTimeframe.BarStudy, IStudy), mChartSpec.IncludeBarsOutsideSession)
    End Sub

    Private Sub prepareChart()
        If mTimeframes Is Nothing Then
            setupStudies()
            loadchart()
        ElseIf mTimeframes.ContractFuture Is Nothing Then
            setupStudies()
            loadchart()
        ElseIf mTimeframes.ContractFuture.IsAvailable Then
            setContractProperties(DirectCast(mTimeframes.ContractFuture.Value, IContract))

            setupStudies()
            loadchart()
        Else
            mFutureWaiter = New FutureWaiter
            mFutureWaiter.Add(mTimeframes.ContractFuture)
        End If
    End Sub

    Private Sub setContractProperties(pContract As IContract)
        mLocalSymbol = pContract.Specifier.LocalSymbol
        mSecType = pContract.Specifier.SecType
        mExchange = pContract.Specifier.Exchange
        mTickSize = pContract.TickSize
        mSessionEndTime = pContract.SessionEndTime
        MyBase.SessionEndTime = COMStartOfDay + mSessionEndTime
        mSessionStartTime = pContract.SessionStartTime
        MyBase.SessionStartTime = COMStartOfDay + mSessionStartTime
    End Sub

    Private Sub setState(value As ChartState)
        Dim stateEv As StateChangeEventData
        mState = value
        stateEv.State = mState
        stateEv.Source = Me
        RaiseEvent StateChange(stateEv)
    End Sub

    Private Sub setStatusText()
        mStatusText = mPriceRegion.AddText(, LayerNumbers.LayerHighestUser)
        Dim font = New stdole.StdFont
        font.Size = 18
        mStatusText.Font = font
        mStatusText.Color = RGB(0, 0, 0)
        mStatusText.Box = True
        mStatusText.BoxFillColor = RGB(255, 255, 255)
        mStatusText.BoxFillStyle = FillStyles.FillSolid
        mStatusText.Position = ChartSkil.NewPoint(50, 50, CoordinateSystems.CoordsRelative, CoordinateSystems.CoordsRelative)
        mStatusText.Align = TextAlignModes.AlignBoxCentreCentre
        mStatusText.FixedX = True
        mStatusText.FixedY = True
    End Sub

    Private Sub setStudyManager(studyNanager As StudyManager)
        mStudyManager = studyNanager
        If mAvailableStudies Is Nothing Then
            mAvailableStudies = DirectCast(mStudyManager.StudyLibraryManager.GetAvailableStudies, StudyListEntry())
        End If
    End Sub

    Private Sub setupEditStudyToolStripMenuItem(study As _IStudy)
        Dim tsmi = New ToolStripMenuItem(study.InstanceName)
        EditStudyToolStripMenuItem.DropDownItems.Add(tsmi)
        tsmi.Tag = mChartManager.GetStudyConfiguration(study.Id)
        tsmi.Size = New System.Drawing.Size(200, 22)
        AddHandler tsmi.Click, AddressOf EditStudyToolStripMenuItem_Click
    End Sub

    Private Sub setupRemoveStudyToolStripMenuItem(study As _IStudy)
        Dim tsmi = New ToolStripMenuItem(study.InstanceName)
        RemoveStudyToolStripMenuItem.DropDownItems.Add(tsmi)
        tsmi.Tag = mChartManager.GetStudyConfiguration(study.Id)
        tsmi.Size = New System.Drawing.Size(200, 22)
        AddHandler tsmi.Click, AddressOf RemoveStudyToolStripMenuItem_Click
    End Sub

    Private Sub setupStudies()
        gDiagLogger.Log("Setting up studies", "setupStudies()", ModuleName)
        If mLoadedFromConfig Then
            loadStudiesFromConfig(mConfig)
            mLoadedFromConfig = False
        Else
            If mPreviousBaseStudyConfig Is Nothing Then
                Dim baseStudyConfig = ChartUtils.CreateBarsStudyConfig(
                                                        mTimeframe,
                                                        mSecType,
                                                        mStudyManager.StudyLibraryManager,
                                                        mBarFormatterFactoryName,
                                                        mBarFormatterLibraryName)
                mChartManager.SetBaseStudyConfiguration(baseStudyConfig)
            Else
                mPreviousBaseStudyConfig.Study = CType(mTimeframe.BarStudy, IStudy)
                mChartManager.SetBaseStudyConfiguration(mPreviousBaseStudyConfig)
                mPreviousBaseStudyConfig = Nothing
            End If
        End If
        Exit Sub
    End Sub

    Private Sub storeSettings(config As ConfigurationSection)
        If config Is Nothing Then Exit Sub

        config.SetSetting(ConfigSettingTimePeriod, mTimePeriod.ToString)
        mChartSpec.ConfigurationSection = config.AddConfigurationSection(ConfigSectionChartSpecifier)
        config.SetSetting(ConfigSettingIsHistoricChart, CStr(mIsHistoricChart))
        config.SetSetting(ConfigSettingBarFormatterFactoryName, mBarFormatterFactoryName)
        config.SetSetting(ConfigSettingBarFormatterLibraryName, mBarFormatterLibraryName)
    End Sub

#End Region

End Class
