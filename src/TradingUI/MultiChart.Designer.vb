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
Partial Class MultiChart
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MultiChart))
        Me.MultiChartControlToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripTimePeriodSelector1 = New TradeWright.Trading.UI.Trading.ToolStripTimePeriodSelector()
        Me.ChangeTimeframeToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AddTimeframeToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.RemoveTimeframeToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MultiChartChartSelectorToolStrip = New System.Windows.Forms.ToolStrip()
        Me.MultiChartToolStripPanel1 = New System.Windows.Forms.ToolStripPanel()
        Me.MultiChartControlToolStrip.SuspendLayout()
        Me.MultiChartToolStripPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MultiChartControlToolStrip
        '
        Me.MultiChartControlToolStrip.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MultiChartControlToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.MultiChartControlToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTimePeriodSelector1, Me.ChangeTimeframeToolStripButton, Me.AddTimeframeToolStripButton, Me.RemoveTimeframeToolStripButton})
        Me.MultiChartControlToolStrip.Location = New System.Drawing.Point(156, 0)
        Me.MultiChartControlToolStrip.Name = "MultiChartControlToolStrip"
        Me.MultiChartControlToolStrip.Size = New System.Drawing.Size(81, 25)
        Me.MultiChartControlToolStrip.TabIndex = 5
        Me.MultiChartControlToolStrip.Text = "ToolStrip1"
        '
        'ToolStripTimePeriodSelector1
        '
        Me.ToolStripTimePeriodSelector1.Name = "ToolStripTimePeriodSelector1"
        Me.ToolStripTimePeriodSelector1.Size = New System.Drawing.Size(121, 23)
        Me.ToolStripTimePeriodSelector1.Text = "ToolStripTimePeriodSelector1"
        Me.ToolStripTimePeriodSelector1.TimePeriod = Nothing
        Me.ToolStripTimePeriodSelector1.UseShortTimePeriodStrings = True
        Me.ToolStripTimePeriodSelector1.Visible = False
        '
        'ChangeTimeframeToolStripButton
        '
        Me.ChangeTimeframeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ChangeTimeframeToolStripButton.Image = CType(resources.GetObject("ChangeTimeframeToolStripButton.Image"), System.Drawing.Image)
        Me.ChangeTimeframeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ChangeTimeframeToolStripButton.Name = "ChangeTimeframeToolStripButton"
        Me.ChangeTimeframeToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.ChangeTimeframeToolStripButton.Text = "Change"
        Me.ChangeTimeframeToolStripButton.ToolTipText = "Change the timeframe for this chart"
        '
        'AddTimeframeToolStripButton
        '
        Me.AddTimeframeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AddTimeframeToolStripButton.Image = CType(resources.GetObject("AddTimeframeToolStripButton.Image"), System.Drawing.Image)
        Me.AddTimeframeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AddTimeframeToolStripButton.Name = "AddTimeframeToolStripButton"
        Me.AddTimeframeToolStripButton.Size = New System.Drawing.Size(23, 23)
        Me.AddTimeframeToolStripButton.Text = "ToolStripButton1"
        Me.AddTimeframeToolStripButton.ToolTipText = "Add a chart with a different timeframe"
        '
        'RemoveTimeframeToolStripButton
        '
        Me.RemoveTimeframeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RemoveTimeframeToolStripButton.Image = CType(resources.GetObject("RemoveTimeframeToolStripButton.Image"), System.Drawing.Image)
        Me.RemoveTimeframeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RemoveTimeframeToolStripButton.Name = "RemoveTimeframeToolStripButton"
        Me.RemoveTimeframeToolStripButton.Size = New System.Drawing.Size(23, 23)
        Me.RemoveTimeframeToolStripButton.Text = "ToolStripButton1"
        Me.RemoveTimeframeToolStripButton.ToolTipText = "Remove the current chart"
        '
        'MultiChartChartSelectorToolStrip
        '
        Me.MultiChartChartSelectorToolStrip.AllowItemReorder = True
        Me.MultiChartChartSelectorToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.MultiChartChartSelectorToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.MultiChartChartSelectorToolStrip.Location = New System.Drawing.Point(6, 0)
        Me.MultiChartChartSelectorToolStrip.MinimumSize = New System.Drawing.Size(150, 0)
        Me.MultiChartChartSelectorToolStrip.Name = "MultiChartChartSelectorToolStrip"
        Me.MultiChartChartSelectorToolStrip.Size = New System.Drawing.Size(150, 25)
        Me.MultiChartChartSelectorToolStrip.TabIndex = 4
        Me.MultiChartChartSelectorToolStrip.Text = "ToolStrip1"
        '
        'MultiChartToolStripPanel1
        '
        Me.MultiChartToolStripPanel1.Controls.Add(Me.MultiChartChartSelectorToolStrip)
        Me.MultiChartToolStripPanel1.Controls.Add(Me.MultiChartControlToolStrip)
        Me.MultiChartToolStripPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MultiChartToolStripPanel1.Location = New System.Drawing.Point(0, 482)
        Me.MultiChartToolStripPanel1.Name = "MultiChartToolStripPanel1"
        Me.MultiChartToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.MultiChartToolStripPanel1.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.MultiChartToolStripPanel1.Size = New System.Drawing.Size(485, 25)
        '
        'MultiChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MultiChartToolStripPanel1)
        Me.Name = "MultiChart"
        Me.Size = New System.Drawing.Size(485, 507)
        Me.MultiChartControlToolStrip.ResumeLayout(False)
        Me.MultiChartControlToolStrip.PerformLayout()
        Me.MultiChartToolStripPanel1.ResumeLayout(False)
        Me.MultiChartToolStripPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    '    Friend WithEvents MktChart As TradeWright.Trading.UI.Trading.MarketChart
    Friend WithEvents MultiChartControlToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents AddTimeframeToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RemoveTimeframeToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MultiChartChartSelectorToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ChangeTimeframeToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MultiChartToolStripPanel1 As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ToolStripTimePeriodSelector1 As TradeWright.Trading.UI.Trading.ToolStripTimePeriodSelector

End Class
