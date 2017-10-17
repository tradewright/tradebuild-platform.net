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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChartControlTools
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChartControlTools))
        Me.ToolStripPanel1 = New System.Windows.Forms.ToolStripPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.mBarsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mCandlesticksToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mLineToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mShowCrosshairToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mShowCursorToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mThinnerBarsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mThickerBarsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mNarrowToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mWidenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mScaleDownToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mScaleUpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.mScrollDownToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mScrollUpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mScrollLeftToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mScrollRightToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mScrollEndToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.mToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.mAutoScaleToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripPanel1
        '
        Me.ToolStripPanel1.Controls.Add(Me.ToolStrip1)
        Me.ToolStripPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ToolStripPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripPanel1.Name = "ToolStripPanel1"
        Me.ToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.ToolStripPanel1.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.ToolStripPanel1.Size = New System.Drawing.Size(591, 25)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AllowMerge = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mBarsToolStripButton, Me.mCandlesticksToolStripButton, Me.mLineToolStripButton, Me.mToolStripSeparator4, Me.mShowCrosshairToolStripButton, Me.mShowCursorToolStripButton, Me.mToolStripSeparator5, Me.mThinnerBarsToolStripButton, Me.mThickerBarsToolStripButton, Me.mToolStripSeparator6, Me.mNarrowToolStripButton, Me.mWidenToolStripButton, Me.mScaleDownToolStripButton, Me.mScaleUpToolStripButton, Me.mToolStripSeparator7, Me.mScrollDownToolStripButton, Me.mScrollUpToolStripButton, Me.mScrollLeftToolStripButton, Me.mScrollRightToolStripButton, Me.mScrollEndToolStripButton, Me.mToolStripSeparator8, Me.mAutoScaleToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(455, 25)
        Me.ToolStrip1.TabIndex = 0
        '
        'mBarsToolStripButton
        '
        Me.mBarsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mBarsToolStripButton.Image = CType(resources.GetObject("mBarsToolStripButton.Image"), System.Drawing.Image)
        Me.mBarsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mBarsToolStripButton.Name = "mBarsToolStripButton"
        Me.mBarsToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mBarsToolStripButton.Text = "Display as bars"
        Me.mBarsToolStripButton.ToolTipText = "Display as bars"
        '
        'mCandlesticksToolStripButton
        '
        Me.mCandlesticksToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mCandlesticksToolStripButton.Image = CType(resources.GetObject("mCandlesticksToolStripButton.Image"), System.Drawing.Image)
        Me.mCandlesticksToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mCandlesticksToolStripButton.Name = "mCandlesticksToolStripButton"
        Me.mCandlesticksToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mCandlesticksToolStripButton.Text = "Display as candlesticks"
        Me.mCandlesticksToolStripButton.ToolTipText = "Display as candlesticks"
        '
        'mLineToolStripButton
        '
        Me.mLineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mLineToolStripButton.Image = CType(resources.GetObject("mLineToolStripButton.Image"), System.Drawing.Image)
        Me.mLineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mLineToolStripButton.Name = "mLineToolStripButton"
        Me.mLineToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mLineToolStripButton.Text = "Display as line"
        '
        'mToolStripSeparator4
        '
        Me.mToolStripSeparator4.Name = "mToolStripSeparator4"
        Me.mToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'mShowCrosshairToolStripButton
        '
        Me.mShowCrosshairToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mShowCrosshairToolStripButton.Image = CType(resources.GetObject("mShowCrosshairToolStripButton.Image"), System.Drawing.Image)
        Me.mShowCrosshairToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mShowCrosshairToolStripButton.Name = "mShowCrosshairToolStripButton"
        Me.mShowCrosshairToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mShowCrosshairToolStripButton.Text = "Show crosshair"
        '
        'mShowCursorToolStripButton
        '
        Me.mShowCursorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mShowCursorToolStripButton.Image = CType(resources.GetObject("mShowCursorToolStripButton.Image"), System.Drawing.Image)
        Me.mShowCursorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mShowCursorToolStripButton.Name = "mShowCursorToolStripButton"
        Me.mShowCursorToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mShowCursorToolStripButton.Text = "Show cursor"
        '
        'mToolStripSeparator5
        '
        Me.mToolStripSeparator5.Name = "mToolStripSeparator5"
        Me.mToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'mThinnerBarsToolStripButton
        '
        Me.mThinnerBarsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mThinnerBarsToolStripButton.Image = CType(resources.GetObject("mThinnerBarsToolStripButton.Image"), System.Drawing.Image)
        Me.mThinnerBarsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mThinnerBarsToolStripButton.Name = "mThinnerBarsToolStripButton"
        Me.mThinnerBarsToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mThinnerBarsToolStripButton.Text = "Make bars thinner"
        '
        'mThickerBarsToolStripButton
        '
        Me.mThickerBarsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mThickerBarsToolStripButton.Image = CType(resources.GetObject("mThickerBarsToolStripButton.Image"), System.Drawing.Image)
        Me.mThickerBarsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mThickerBarsToolStripButton.Name = "mThickerBarsToolStripButton"
        Me.mThickerBarsToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mThickerBarsToolStripButton.Text = "Make bars thicker"
        '
        'mToolStripSeparator6
        '
        Me.mToolStripSeparator6.Name = "mToolStripSeparator6"
        Me.mToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'mNarrowToolStripButton
        '
        Me.mNarrowToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mNarrowToolStripButton.Image = CType(resources.GetObject("mNarrowToolStripButton.Image"), System.Drawing.Image)
        Me.mNarrowToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mNarrowToolStripButton.Name = "mNarrowToolStripButton"
        Me.mNarrowToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mNarrowToolStripButton.Text = "Narrower bar spacing"
        Me.mNarrowToolStripButton.ToolTipText = "Reduce bar spacing"
        '
        'mWidenToolStripButton
        '
        Me.mWidenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mWidenToolStripButton.Image = CType(resources.GetObject("mWidenToolStripButton.Image"), System.Drawing.Image)
        Me.mWidenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mWidenToolStripButton.Name = "mWidenToolStripButton"
        Me.mWidenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mWidenToolStripButton.Text = "Wider bar spacing"
        Me.mWidenToolStripButton.ToolTipText = "Increase bar spacing"
        '
        'mScaleDownToolStripButton
        '
        Me.mScaleDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mScaleDownToolStripButton.Image = CType(resources.GetObject("mScaleDownToolStripButton.Image"), System.Drawing.Image)
        Me.mScaleDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mScaleDownToolStripButton.Name = "mScaleDownToolStripButton"
        Me.mScaleDownToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mScaleDownToolStripButton.Text = "Scale down"
        Me.mScaleDownToolStripButton.ToolTipText = "Compress vertical scale"
        '
        'mScaleUpToolStripButton
        '
        Me.mScaleUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mScaleUpToolStripButton.Image = CType(resources.GetObject("mScaleUpToolStripButton.Image"), System.Drawing.Image)
        Me.mScaleUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mScaleUpToolStripButton.Name = "mScaleUpToolStripButton"
        Me.mScaleUpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mScaleUpToolStripButton.Text = "Scale up"
        Me.mScaleUpToolStripButton.ToolTipText = "Expand vertical scale"
        '
        'mToolStripSeparator7
        '
        Me.mToolStripSeparator7.Name = "mToolStripSeparator7"
        Me.mToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'mScrollDownToolStripButton
        '
        Me.mScrollDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mScrollDownToolStripButton.Image = CType(resources.GetObject("mScrollDownToolStripButton.Image"), System.Drawing.Image)
        Me.mScrollDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mScrollDownToolStripButton.Name = "mScrollDownToolStripButton"
        Me.mScrollDownToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mScrollDownToolStripButton.Text = "Scroll down"
        '
        'mScrollUpToolStripButton
        '
        Me.mScrollUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mScrollUpToolStripButton.Image = CType(resources.GetObject("mScrollUpToolStripButton.Image"), System.Drawing.Image)
        Me.mScrollUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mScrollUpToolStripButton.Name = "mScrollUpToolStripButton"
        Me.mScrollUpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mScrollUpToolStripButton.Text = "Scroll up"
        '
        'mScrollLeftToolStripButton
        '
        Me.mScrollLeftToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mScrollLeftToolStripButton.Image = CType(resources.GetObject("mScrollLeftToolStripButton.Image"), System.Drawing.Image)
        Me.mScrollLeftToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mScrollLeftToolStripButton.Name = "mScrollLeftToolStripButton"
        Me.mScrollLeftToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mScrollLeftToolStripButton.Text = "Scroll left"
        '
        'mScrollRightToolStripButton
        '
        Me.mScrollRightToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mScrollRightToolStripButton.Image = CType(resources.GetObject("mScrollRightToolStripButton.Image"), System.Drawing.Image)
        Me.mScrollRightToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mScrollRightToolStripButton.Name = "mScrollRightToolStripButton"
        Me.mScrollRightToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mScrollRightToolStripButton.Text = "Scroll right"
        '
        'mScrollEndToolStripButton
        '
        Me.mScrollEndToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mScrollEndToolStripButton.Image = CType(resources.GetObject("mScrollEndToolStripButton.Image"), System.Drawing.Image)
        Me.mScrollEndToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mScrollEndToolStripButton.Name = "mScrollEndToolStripButton"
        Me.mScrollEndToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mScrollEndToolStripButton.Text = "Scroll to end"
        '
        'mToolStripSeparator8
        '
        Me.mToolStripSeparator8.Name = "mToolStripSeparator8"
        Me.mToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'mAutoScaleToolStripButton
        '
        Me.mAutoScaleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mAutoScaleToolStripButton.Image = CType(resources.GetObject("mAutoScaleToolStripButton.Image"), System.Drawing.Image)
        Me.mAutoScaleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mAutoScaleToolStripButton.Name = "mAutoScaleToolStripButton"
        Me.mAutoScaleToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.mAutoScaleToolStripButton.Text = "Auto-scale"
        '
        'ChartControlTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.ToolStripPanel1)
        Me.Name = "ChartControlTools"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Size = New System.Drawing.Size(591, 27)
        Me.ToolStripPanel1.ResumeLayout(False)
        Me.ToolStripPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripPanel1 As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
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


End Class
