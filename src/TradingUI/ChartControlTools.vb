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

Public Class ChartControlTools

    Private mChart As MarketChart
    Private mBarSeries As BarSeries

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub mChart_StateChange(ev As StateChangeEventData)
        Dim State = CType(ev.State, MarketChart.ChartStates)
        Select Case State
            Case MarketChart.ChartStates.ChartStateBlank

            Case MarketChart.ChartStates.ChartStateCreated

            Case MarketChart.ChartStates.ChartStateInitialised

            Case MarketChart.ChartStates.ChartStateLoaded
                setupButtons()
                Me.Enabled = True
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
        Set(value As MarketChart)
            If mChart IsNot Nothing Then RemoveHandler mChart.StateChange, AddressOf mChart_StateChange

            mChart = value
            If mChart Is Nothing Then
                Me.Enabled = False
                Exit Property
            End If

            AddHandler mChart.StateChange, AddressOf mChart_StateChange

            If mChart.State = MarketChart.ChartStates.ChartStateLoaded Then
                setupButtons()
                Me.Enabled = True
            Else
                Me.Enabled = False
            End If
        End Set
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
