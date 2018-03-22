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

Imports ChartSkil27
Imports TWUtilities40

Imports TradeWright.Trading.UI.Trading

Public NotInheritable Class ChartControlToolsSupport

    Private mChart As MarketChart
    Private mBarSeries As BarSeries
    Private mParent As Control

    Private components As System.ComponentModel.IContainer

    Friend WithEvents mBarsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mCandlesticksToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mLineToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mShowCrosshairToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mShowCursorToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mThinnerBarsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mThickerBarsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mNarrowToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mWidenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mScaleDownToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mScaleUpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mScrollDownToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mScrollUpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mScrollLeftToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mScrollRightToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mScrollEndToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents mToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mAutoScaleToolStripButton As System.Windows.Forms.ToolStripButton

    Public Sub Load(parent As Control, resources As System.ComponentModel.ComponentResourceManager)
        mParent = parent

        mBarsToolStripButton = New System.Windows.Forms.ToolStripButton
        mCandlesticksToolStripButton = New System.Windows.Forms.ToolStripButton
        mLineToolStripButton = New System.Windows.Forms.ToolStripButton
        mToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        mShowCrosshairToolStripButton = New System.Windows.Forms.ToolStripButton
        mShowCursorToolStripButton = New System.Windows.Forms.ToolStripButton
        mToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        mThinnerBarsToolStripButton = New System.Windows.Forms.ToolStripButton
        mThickerBarsToolStripButton = New System.Windows.Forms.ToolStripButton
        mToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        mNarrowToolStripButton = New System.Windows.Forms.ToolStripButton
        mWidenToolStripButton = New System.Windows.Forms.ToolStripButton
        mScaleDownToolStripButton = New System.Windows.Forms.ToolStripButton
        mScaleUpToolStripButton = New System.Windows.Forms.ToolStripButton
        mToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        mScrollDownToolStripButton = New System.Windows.Forms.ToolStripButton
        mScrollUpToolStripButton = New System.Windows.Forms.ToolStripButton
        mScrollLeftToolStripButton = New System.Windows.Forms.ToolStripButton
        mScrollRightToolStripButton = New System.Windows.Forms.ToolStripButton
        mScrollEndToolStripButton = New System.Windows.Forms.ToolStripButton
        mToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        mAutoScaleToolStripButton = New System.Windows.Forms.ToolStripButton

        '
        'BarsToolStripButton
        '
        mBarsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mBarsToolStripButton.Image = CType(resources.GetObject("BarsToolStripButton.Image"), System.Drawing.Image)
        mBarsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mBarsToolStripButton.Name = "BarsToolStripButton"
        mBarsToolStripButton.Size = New System.Drawing.Size(23, 22)
        mBarsToolStripButton.Text = "Display as bars"
        mBarsToolStripButton.ToolTipText = "Display as bars"
        '
        'CandlesticksToolStripButton
        '
        mCandlesticksToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mCandlesticksToolStripButton.Image = CType(resources.GetObject("CandlesticksToolStripButton.Image"), System.Drawing.Image)
        mCandlesticksToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mCandlesticksToolStripButton.Name = "CandlesticksToolStripButton"
        mCandlesticksToolStripButton.Size = New System.Drawing.Size(23, 22)
        mCandlesticksToolStripButton.Text = "Display as candlesticks"
        mCandlesticksToolStripButton.ToolTipText = "Display as candlesticks"
        '
        'LineToolStripButton
        '
        mLineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mLineToolStripButton.Image = CType(resources.GetObject("LineToolStripButton.Image"), System.Drawing.Image)
        mLineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mLineToolStripButton.Name = "LineToolStripButton"
        mLineToolStripButton.Size = New System.Drawing.Size(23, 22)
        mLineToolStripButton.Text = "Display as line"
        '
        'ToolStripSeparator4
        '
        mToolStripSeparator4.Name = "ToolStripSeparator4"
        mToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ShowCrosshairToolStripButton
        '
        mShowCrosshairToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mShowCrosshairToolStripButton.Image = CType(resources.GetObject("ShowCrosshairToolStripButton.Image"), System.Drawing.Image)
        mShowCrosshairToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mShowCrosshairToolStripButton.Name = "ShowCrosshairToolStripButton"
        mShowCrosshairToolStripButton.Size = New System.Drawing.Size(23, 22)
        mShowCrosshairToolStripButton.Text = "Show crosshair"
        '
        'ShowCursorToolStripButton
        '
        mShowCursorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mShowCursorToolStripButton.Image = CType(resources.GetObject("ShowCursorToolStripButton.Image"), System.Drawing.Image)
        mShowCursorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mShowCursorToolStripButton.Name = "ShowCursorToolStripButton"
        mShowCursorToolStripButton.Size = New System.Drawing.Size(23, 22)
        mShowCursorToolStripButton.Text = "Show cursor"
        '
        'ToolStripSeparator5
        '
        mToolStripSeparator5.Name = "ToolStripSeparator5"
        mToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ThinnerBarsToolStripButton
        '
        mThinnerBarsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mThinnerBarsToolStripButton.Image = CType(resources.GetObject("ThinnerBarsToolStripButton.Image"), System.Drawing.Image)
        mThinnerBarsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mThinnerBarsToolStripButton.Name = "ThinnerBarsToolStripButton"
        mThinnerBarsToolStripButton.Size = New System.Drawing.Size(23, 22)
        mThinnerBarsToolStripButton.Text = "Make bars thinner"
        '
        'ThickerBarsToolStripButton
        '
        mThickerBarsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mThickerBarsToolStripButton.Image = CType(resources.GetObject("ThickerBarsToolStripButton.Image"), System.Drawing.Image)
        mThickerBarsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mThickerBarsToolStripButton.Name = "ThickerBarsToolStripButton"
        mThickerBarsToolStripButton.Size = New System.Drawing.Size(23, 22)
        mThickerBarsToolStripButton.Text = "Make bars thicker"
        '
        'ToolStripSeparator6
        '
        mToolStripSeparator6.Name = "ToolStripSeparator6"
        mToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'NarrowToolStripButton
        '
        mNarrowToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mNarrowToolStripButton.Image = CType(resources.GetObject("NarrowToolStripButton.Image"), System.Drawing.Image)
        mNarrowToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mNarrowToolStripButton.Name = "NarrowToolStripButton"
        mNarrowToolStripButton.Size = New System.Drawing.Size(23, 22)
        mNarrowToolStripButton.Text = "Narrower bar spacing"
        mNarrowToolStripButton.ToolTipText = "Reduce bar spacing"
        '
        'WidenToolStripButton
        '
        mWidenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mWidenToolStripButton.Image = CType(resources.GetObject("WidenToolStripButton.Image"), System.Drawing.Image)
        mWidenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mWidenToolStripButton.Name = "WidenToolStripButton"
        mWidenToolStripButton.Size = New System.Drawing.Size(23, 22)
        mWidenToolStripButton.Text = "Wider bar spacing"
        mWidenToolStripButton.ToolTipText = "Increase bar spacing"
        '
        'ScaleDownToolStripButton
        '
        mScaleDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mScaleDownToolStripButton.Image = CType(resources.GetObject("ScaleDownToolStripButton.Image"), System.Drawing.Image)
        mScaleDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mScaleDownToolStripButton.Name = "ScaleDownToolStripButton"
        mScaleDownToolStripButton.Size = New System.Drawing.Size(23, 22)
        mScaleDownToolStripButton.Text = "Scale down"
        mScaleDownToolStripButton.ToolTipText = "Compress vertical scale"
        '
        'ScaleUpToolStripButton
        '
        mScaleUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mScaleUpToolStripButton.Image = CType(resources.GetObject("ScaleUpToolStripButton.Image"), System.Drawing.Image)
        mScaleUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mScaleUpToolStripButton.Name = "ScaleUpToolStripButton"
        mScaleUpToolStripButton.Size = New System.Drawing.Size(23, 22)
        mScaleUpToolStripButton.Text = "Scale up"
        mScaleUpToolStripButton.ToolTipText = "Expand vertical scale"
        '
        'ToolStripSeparator7
        '
        mToolStripSeparator7.Name = "ToolStripSeparator7"
        mToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ScrollDownToolStripButton
        '
        mScrollDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mScrollDownToolStripButton.Image = CType(resources.GetObject("ScrollDownToolStripButton.Image"), System.Drawing.Image)
        mScrollDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mScrollDownToolStripButton.Name = "ScrollDownToolStripButton"
        mScrollDownToolStripButton.Size = New System.Drawing.Size(23, 22)
        mScrollDownToolStripButton.Text = "Scroll down"
        '
        'ScrollUpToolStripButton
        '
        mScrollUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mScrollUpToolStripButton.Image = CType(resources.GetObject("ScrollUpToolStripButton.Image"), System.Drawing.Image)
        mScrollUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mScrollUpToolStripButton.Name = "ScrollUpToolStripButton"
        mScrollUpToolStripButton.Size = New System.Drawing.Size(23, 22)
        mScrollUpToolStripButton.Text = "Scroll up"
        '
        'ScrollLeftToolStripButton
        '
        mScrollLeftToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mScrollLeftToolStripButton.Image = CType(resources.GetObject("ScrollLeftToolStripButton.Image"), System.Drawing.Image)
        mScrollLeftToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mScrollLeftToolStripButton.Name = "ScrollLeftToolStripButton"
        mScrollLeftToolStripButton.Size = New System.Drawing.Size(23, 22)
        mScrollLeftToolStripButton.Text = "Scroll left"
        '
        'ScrollRightToolStripButton
        '
        mScrollRightToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mScrollRightToolStripButton.Image = CType(resources.GetObject("ScrollRightToolStripButton.Image"), System.Drawing.Image)
        mScrollRightToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mScrollRightToolStripButton.Name = "ScrollRightToolStripButton"
        mScrollRightToolStripButton.Size = New System.Drawing.Size(23, 22)
        mScrollRightToolStripButton.Text = "Scroll right"
        '
        'ScrollEndToolStripButton
        '
        mScrollEndToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mScrollEndToolStripButton.Image = CType(resources.GetObject("ScrollEndToolStripButton.Image"), System.Drawing.Image)
        mScrollEndToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mScrollEndToolStripButton.Name = "ScrollEndToolStripButton"
        mScrollEndToolStripButton.Size = New System.Drawing.Size(23, 22)
        mScrollEndToolStripButton.Text = "Scroll to end"
        '
        'ToolStripSeparator8
        '
        mToolStripSeparator8.Name = "ToolStripSeparator8"
        mToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'AutoScaleToolStripButton
        '
        mAutoScaleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        mAutoScaleToolStripButton.Image = CType(resources.GetObject("AutoScaleToolStripButton.Image"), System.Drawing.Image)
        mAutoScaleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        mAutoScaleToolStripButton.Name = "AutoScaleToolStripButton"
        mAutoScaleToolStripButton.Size = New System.Drawing.Size(23, 22)
        mAutoScaleToolStripButton.Text = "Auto-scale"

    End Sub

    Private Sub mChart_StateChange(ev As StateChangeEventData)
        Dim State = CType(ev.State, MarketChart.ChartState)
        Select Case State
            Case MarketChart.ChartState.Blank

            Case MarketChart.ChartState.Created

            Case MarketChart.ChartState.Initialised

            Case MarketChart.ChartState.Loaded
                setupButtons()
                mParent.Enabled = True
        End Select
    End Sub

    Private Sub PriceRegion_AutoscalingChanged()
        mAutoScaleToolStripButton.Checked = mChart.PriceRegion.Autoscaling
    End Sub

    Private Sub mBarsToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mBarsToolStripButton.Click
        mBarSeries.Style.DisplayMode = BarDisplayModes.BarDisplayModeBar
        mBarsToolStripButton.Checked = True
        mCandlesticksToolStripButton.Checked = False
        mLineToolStripButton.Checked = False
    End Sub

    Private Sub mCandlesticksToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mCandlesticksToolStripButton.Click
        mBarSeries.Style.DisplayMode = BarDisplayModes.BarDisplayModeCandlestick
        mBarsToolStripButton.Checked = False
        mCandlesticksToolStripButton.Checked = True
        mLineToolStripButton.Checked = False
    End Sub

    Private Sub mLineToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mLineToolStripButton.Click
        mBarSeries.Style.DisplayMode = BarDisplayModes.BarDisplayModeLine
        mBarsToolStripButton.Checked = False
        mCandlesticksToolStripButton.Checked = False
        mLineToolStripButton.Checked = True
    End Sub

    Private Sub mShowCrosshairToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mShowCrosshairToolStripButton.Click
        mChart.PointerStyle = PointerStyles.PointerCrosshairs
        mShowCrosshairToolStripButton.Checked = True
        mShowCursorToolStripButton.Checked = False
    End Sub

    Private Sub mShowCursorToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mShowCursorToolStripButton.Click
        mChart.PointerStyle = PointerStyles.PointerDisc
        mShowCursorToolStripButton.Checked = True
        mShowCrosshairToolStripButton.Checked = False
    End Sub

    Private Sub mThinnerBarsToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mThinnerBarsToolStripButton.Click
        Select Case mBarSeries.DisplayMode
            Case BarDisplayModes.BarDisplayModeCandlestick
                If mBarSeries.Width > 0.1 Then
                    mBarSeries.Style.Width = mBarSeries.Width - CSng(0.1)
                End If
                If mBarSeries.Width <= 0.1 Then
                    mThinnerBarsToolStripButton.Enabled = False
                End If
            Case BarDisplayModes.BarDisplayModeBar
                If mBarSeries.Thickness > 1 Then
                    mBarSeries.Style.Thickness = mBarSeries.Thickness - 1
                End If
                If mBarSeries.Thickness = 1 Then
                    mThinnerBarsToolStripButton.Enabled = False
                End If
        End Select
    End Sub

    Private Sub mThickerBarsToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mThickerBarsToolStripButton.Click
        Select Case mBarSeries.DisplayMode
            Case BarDisplayModes.BarDisplayModeCandlestick
                mBarSeries.Style.Width = mBarSeries.Width + CSng(0.1)
            Case BarDisplayModes.BarDisplayModeBar
                mBarSeries.Style.Thickness = mBarSeries.Thickness + 1
        End Select
        mThinnerBarsToolStripButton.Enabled = True
    End Sub

    Private Sub mNarrowToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mNarrowToolStripButton.Click
        If mChart.PeriodWidth > 3 Then
            mChart.PeriodWidth = mChart.PeriodWidth - 2
        End If
        If mChart.PeriodWidth = 3 Then
            mNarrowToolStripButton.Enabled = False
        End If
    End Sub

    Private Sub mWidenToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mWidenToolStripButton.Click
        mChart.PeriodWidth = mChart.PeriodWidth + 2
        mNarrowToolStripButton.Enabled = True
    End Sub

    Private Sub mScaleDownToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mScaleDownToolStripButton.Click
        mChart.PriceRegion.ScaleUp(-0.09091)
    End Sub

    Private Sub mScaleUpToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mScaleUpToolStripButton.Click
        mChart.PriceRegion.ScaleUp(0.1)
    End Sub

    Private Sub mScrollDownToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mScrollDownToolStripButton.Click
        mChart.PriceRegion.ScrollVerticalProportion(-0.2)
    End Sub

    Private Sub mScrollUpToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mScrollUpToolStripButton.Click
        mChart.PriceRegion.ScrollVerticalProportion(0.2)
    End Sub

    Private Sub mScrollLeftToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mScrollLeftToolStripButton.Click
        mChart.ScrollX(-CInt((mChart.ChartWidth * 0.2)))
    End Sub

    Private Sub mScrollRightToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mScrollRightToolStripButton.Click
        mChart.ScrollX(CInt((mChart.ChartWidth * 0.2)))
    End Sub

    Private Sub mScrollEndToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mScrollEndToolStripButton.Click
        mChart.LastVisiblePeriod = mChart.CurrentPeriodNumber
    End Sub

    Private Sub mAutoScaleToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles mAutoScaleToolStripButton.Click
        mChart.PriceRegion.Autoscaling = Not mAutoScaleToolStripButton.Checked
    End Sub

    Private Sub mBarSeries_PropertyChanged(ByRef ev As PropertyChangedEventData)
        If ev.PropertyName.ToUpper = "DISPLAYMODE" Then setupDisplayModeButtons()
    End Sub

    Public Property Chart() As MarketChart
        Get
            Return mChart
        End Get
        Set
            If mChart IsNot Nothing Then RemoveHandler mChart.StateChange, AddressOf mChart_StateChange

            mChart = Value
            If mChart Is Nothing Then
                mParent.Enabled = False
                Exit Property
            End If

            AddHandler mChart.StateChange, AddressOf mChart_StateChange

            If mChart.State = MarketChart.ChartState.Loaded Then
                setupButtons()
                mParent.Enabled = True
            Else
                mParent.Enabled = False
            End If
        End Set
    End Property

    Public ReadOnly Property Items As System.Windows.Forms.ToolStripItem()
        Get
            Return New System.Windows.Forms.ToolStripItem() {mBarsToolStripButton,
                                                                    mCandlesticksToolStripButton,
                                                                    mLineToolStripButton,
                                                                    mToolStripSeparator4,
                                                                    mShowCrosshairToolStripButton,
                                                                    mShowCursorToolStripButton,
                                                                    mToolStripSeparator5,
                                                                    mThinnerBarsToolStripButton,
                                                                    mThickerBarsToolStripButton,
                                                                    mToolStripSeparator6,
                                                                    mNarrowToolStripButton,
                                                                    mWidenToolStripButton,
                                                                    mScaleDownToolStripButton,
                                                                    mScaleUpToolStripButton,
                                                                    mToolStripSeparator7,
                                                                    mScrollDownToolStripButton,
                                                                    mScrollUpToolStripButton,
                                                                    mScrollLeftToolStripButton,
                                                                    mScrollRightToolStripButton,
                                                                    mScrollEndToolStripButton,
                                                                    mToolStripSeparator8,
                                                                    mAutoScaleToolStripButton}
        End Get
    End Property

    Private Sub setupButtons()
        Static prevPriceRegion As ChartRegion

        Try
            If mBarSeries IsNot Nothing Then RemoveHandler mBarSeries.PropertyChanged, AddressOf mBarSeries_PropertyChanged
            If prevPriceRegion IsNot Nothing Then RemoveHandler prevPriceRegion.AutoscalingChanged, AddressOf PriceRegion_AutoscalingChanged
        Catch ex As Exception
        End Try

        mBarSeries = mChart.TradeBarSeries
        If mBarSeries Is Nothing Then Exit Sub

        AddHandler mBarSeries.PropertyChanged, AddressOf mBarSeries_PropertyChanged

        setupDisplayModeButtons()

        If mChart.PriceRegion.PointerStyle = PointerStyles.PointerCrosshairs Then
            mShowCrosshairToolStripButton.Checked = True
            mShowCursorToolStripButton.Checked = False
        Else
            mShowCrosshairToolStripButton.Checked = False
            mShowCursorToolStripButton.Checked = True
        End If

        mNarrowToolStripButton.Enabled = (mChart.PeriodWidth > 3)

        mAutoScaleToolStripButton.Checked = mChart.PriceRegion.Autoscaling

        AddHandler mChart.PriceRegion.AutoscalingChanged, AddressOf PriceRegion_AutoscalingChanged
        prevPriceRegion = mChart.PriceRegion
    End Sub

    Private Sub setupDisplayModeButtons()
        If mBarSeries.DisplayMode = BarDisplayModes.BarDisplayModeBar Then
            mBarsToolStripButton.Checked = True
            mCandlesticksToolStripButton.Checked = False
            mThinnerBarsToolStripButton.Enabled = (mBarSeries.Thickness > 1)
        Else
            mBarsToolStripButton.Checked = False
            mCandlesticksToolStripButton.Checked = True
            mThinnerBarsToolStripButton.Enabled = (mBarSeries.Width > 0.1)
        End If
    End Sub

End Class
