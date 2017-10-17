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
Partial Class StudyConfigurer
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.InputsGroup = New System.Windows.Forms.GroupBox
        Me.BaseStudiesTree = New System.Windows.Forms.TreeView
        Me.InputsPanel = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.ChartRegionCombo = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ParametersGroup = New System.Windows.Forms.GroupBox
        Me.ParametersPanel = New System.Windows.Forms.Panel
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Line5ColorPicker = New TradeWright.Trading.UI.Studies.ColorPicker
        Me.Line4ColorPicker = New TradeWright.Trading.UI.Studies.ColorPicker
        Me.Line3ColorPicker = New TradeWright.Trading.UI.Studies.ColorPicker
        Me.Line2ColorPicker = New TradeWright.Trading.UI.Studies.ColorPicker
        Me.Line1ColorPicker = New TradeWright.Trading.UI.Studies.ColorPicker
        Me.Line5Text = New System.Windows.Forms.TextBox
        Me.Line4Text = New System.Windows.Forms.TextBox
        Me.Line3Text = New System.Windows.Forms.TextBox
        Me.Line2Text = New System.Windows.Forms.TextBox
        Me.Line1Text = New System.Windows.Forms.TextBox
        Me.ValuesGroup = New System.Windows.Forms.GroupBox
        Me.ValuesPanel = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.HelpText = New System.Windows.Forms.TextBox
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.InputsGroup.SuspendLayout()
        Me.ParametersGroup.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.ValuesGroup.SuspendLayout()
        Me.ValuesPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.HelpText)
        Me.SplitContainer1.Size = New System.Drawing.Size(850, 435)
        Me.SplitContainer1.SplitterDistance = 347
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ValuesGroup)
        Me.SplitContainer2.Size = New System.Drawing.Size(850, 347)
        Me.SplitContainer2.SplitterDistance = 330
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.InputsGroup)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.ParametersGroup)
        Me.SplitContainer4.Size = New System.Drawing.Size(330, 347)
        Me.SplitContainer4.SplitterDistance = 164
        Me.SplitContainer4.TabIndex = 0
        '
        'InputsGroup
        '
        Me.InputsGroup.Controls.Add(Me.BaseStudiesTree)
        Me.InputsGroup.Controls.Add(Me.InputsPanel)
        Me.InputsGroup.Controls.Add(Me.Label2)
        Me.InputsGroup.Controls.Add(Me.ChartRegionCombo)
        Me.InputsGroup.Controls.Add(Me.Label1)
        Me.InputsGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputsGroup.Location = New System.Drawing.Point(0, 0)
        Me.InputsGroup.Name = "InputsGroup"
        Me.InputsGroup.Size = New System.Drawing.Size(164, 347)
        Me.InputsGroup.TabIndex = 12
        Me.InputsGroup.TabStop = False
        Me.InputsGroup.Text = "Inputs"
        '
        'BaseStudiesTree
        '
        Me.BaseStudiesTree.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BaseStudiesTree.HideSelection = False
        Me.BaseStudiesTree.Location = New System.Drawing.Point(13, 78)
        Me.BaseStudiesTree.Name = "BaseStudiesTree"
        Me.BaseStudiesTree.ShowLines = False
        Me.BaseStudiesTree.ShowPlusMinus = False
        Me.BaseStudiesTree.ShowRootLines = False
        Me.BaseStudiesTree.Size = New System.Drawing.Size(144, 63)
        Me.BaseStudiesTree.TabIndex = 5
        '
        'InputsPanel
        '
        Me.InputsPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputsPanel.AutoScroll = True
        Me.InputsPanel.Location = New System.Drawing.Point(11, 151)
        Me.InputsPanel.Name = "InputsPanel"
        Me.InputsPanel.Size = New System.Drawing.Size(147, 194)
        Me.InputsPanel.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Base study"
        '
        'ChartRegionCombo
        '
        Me.ChartRegionCombo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChartRegionCombo.FormattingEnabled = True
        Me.ChartRegionCombo.Location = New System.Drawing.Point(8, 37)
        Me.ChartRegionCombo.Name = "ChartRegionCombo"
        Me.ChartRegionCombo.Size = New System.Drawing.Size(150, 21)
        Me.ChartRegionCombo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Chart region"
        '
        'ParametersGroup
        '
        Me.ParametersGroup.Controls.Add(Me.ParametersPanel)
        Me.ParametersGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ParametersGroup.Location = New System.Drawing.Point(0, 0)
        Me.ParametersGroup.Name = "ParametersGroup"
        Me.ParametersGroup.Size = New System.Drawing.Size(162, 347)
        Me.ParametersGroup.TabIndex = 15
        Me.ParametersGroup.TabStop = False
        Me.ParametersGroup.Text = "Parameters"
        '
        'ParametersPanel
        '
        Me.ParametersPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ParametersPanel.AutoScroll = True
        Me.ParametersPanel.Location = New System.Drawing.Point(6, 16)
        Me.ParametersPanel.Name = "ParametersPanel"
        Me.ParametersPanel.Size = New System.Drawing.Size(150, 330)
        Me.ParametersPanel.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.Line5ColorPicker)
        Me.GroupBox4.Controls.Add(Me.Line4ColorPicker)
        Me.GroupBox4.Controls.Add(Me.Line3ColorPicker)
        Me.GroupBox4.Controls.Add(Me.Line2ColorPicker)
        Me.GroupBox4.Controls.Add(Me.Line1ColorPicker)
        Me.GroupBox4.Controls.Add(Me.Line5Text)
        Me.GroupBox4.Controls.Add(Me.Line4Text)
        Me.GroupBox4.Controls.Add(Me.Line3Text)
        Me.GroupBox4.Controls.Add(Me.Line2Text)
        Me.GroupBox4.Controls.Add(Me.Line1Text)
        Me.GroupBox4.Location = New System.Drawing.Point(0, 298)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(516, 48)
        Me.GroupBox4.TabIndex = 17
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Horizontal lines"
        '
        'Line5ColorPicker
        '
        Me.Line5ColorPicker.AllowNull = False
        Me.Line5ColorPicker.Color = System.Drawing.Color.Blue
        Me.Line5ColorPicker.Location = New System.Drawing.Point(383, 19)
        Me.Line5ColorPicker.Name = "Line5ColorPicker"
        Me.Line5ColorPicker.OleColor = 16711680
        Me.Line5ColorPicker.Size = New System.Drawing.Size(20, 20)
        Me.Line5ColorPicker.TabIndex = 23
        '
        'Line4ColorPicker
        '
        Me.Line4ColorPicker.AllowNull = False
        Me.Line4ColorPicker.Color = System.Drawing.Color.Blue
        Me.Line4ColorPicker.Location = New System.Drawing.Point(299, 19)
        Me.Line4ColorPicker.Name = "Line4ColorPicker"
        Me.Line4ColorPicker.OleColor = 16711680
        Me.Line4ColorPicker.Size = New System.Drawing.Size(20, 20)
        Me.Line4ColorPicker.TabIndex = 22
        '
        'Line3ColorPicker
        '
        Me.Line3ColorPicker.AllowNull = False
        Me.Line3ColorPicker.Color = System.Drawing.Color.Blue
        Me.Line3ColorPicker.Location = New System.Drawing.Point(219, 19)
        Me.Line3ColorPicker.Name = "Line3ColorPicker"
        Me.Line3ColorPicker.OleColor = 16711680
        Me.Line3ColorPicker.Size = New System.Drawing.Size(20, 20)
        Me.Line3ColorPicker.TabIndex = 21
        '
        'Line2ColorPicker
        '
        Me.Line2ColorPicker.AllowNull = False
        Me.Line2ColorPicker.Color = System.Drawing.Color.Blue
        Me.Line2ColorPicker.Location = New System.Drawing.Point(137, 19)
        Me.Line2ColorPicker.Name = "Line2ColorPicker"
        Me.Line2ColorPicker.OleColor = 16711680
        Me.Line2ColorPicker.Size = New System.Drawing.Size(20, 20)
        Me.Line2ColorPicker.TabIndex = 20
        '
        'Line1ColorPicker
        '
        Me.Line1ColorPicker.AllowNull = False
        Me.Line1ColorPicker.Color = System.Drawing.Color.Blue
        Me.Line1ColorPicker.Location = New System.Drawing.Point(56, 19)
        Me.Line1ColorPicker.Name = "Line1ColorPicker"
        Me.Line1ColorPicker.OleColor = 16711680
        Me.Line1ColorPicker.Size = New System.Drawing.Size(20, 20)
        Me.Line1ColorPicker.TabIndex = 19
        '
        'Line5Text
        '
        Me.Line5Text.Location = New System.Drawing.Point(337, 19)
        Me.Line5Text.Name = "Line5Text"
        Me.Line5Text.Size = New System.Drawing.Size(40, 20)
        Me.Line5Text.TabIndex = 17
        '
        'Line4Text
        '
        Me.Line4Text.Location = New System.Drawing.Point(253, 19)
        Me.Line4Text.Name = "Line4Text"
        Me.Line4Text.Size = New System.Drawing.Size(40, 20)
        Me.Line4Text.TabIndex = 15
        '
        'Line3Text
        '
        Me.Line3Text.Location = New System.Drawing.Point(173, 19)
        Me.Line3Text.Name = "Line3Text"
        Me.Line3Text.Size = New System.Drawing.Size(40, 20)
        Me.Line3Text.TabIndex = 13
        '
        'Line2Text
        '
        Me.Line2Text.Location = New System.Drawing.Point(91, 19)
        Me.Line2Text.Name = "Line2Text"
        Me.Line2Text.Size = New System.Drawing.Size(40, 20)
        Me.Line2Text.TabIndex = 11
        '
        'Line1Text
        '
        Me.Line1Text.Location = New System.Drawing.Point(11, 19)
        Me.Line1Text.Name = "Line1Text"
        Me.Line1Text.Size = New System.Drawing.Size(40, 20)
        Me.Line1Text.TabIndex = 0
        '
        'ValuesGroup
        '
        Me.ValuesGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValuesGroup.Controls.Add(Me.ValuesPanel)
        Me.ValuesGroup.Location = New System.Drawing.Point(0, 0)
        Me.ValuesGroup.Name = "ValuesGroup"
        Me.ValuesGroup.Size = New System.Drawing.Size(516, 295)
        Me.ValuesGroup.TabIndex = 16
        Me.ValuesGroup.TabStop = False
        Me.ValuesGroup.Text = "Output values"
        '
        'ValuesPanel
        '
        Me.ValuesPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValuesPanel.AutoScroll = True
        Me.ValuesPanel.Controls.Add(Me.Label11)
        Me.ValuesPanel.Controls.Add(Me.Label10)
        Me.ValuesPanel.Controls.Add(Me.Label9)
        Me.ValuesPanel.Controls.Add(Me.Label8)
        Me.ValuesPanel.Controls.Add(Me.Label7)
        Me.ValuesPanel.Controls.Add(Me.Label6)
        Me.ValuesPanel.Controls.Add(Me.Label5)
        Me.ValuesPanel.Controls.Add(Me.Label4)
        Me.ValuesPanel.Location = New System.Drawing.Point(5, 16)
        Me.ValuesPanel.Name = "ValuesPanel"
        Me.ValuesPanel.Size = New System.Drawing.Size(505, 273)
        Me.ValuesPanel.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(449, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Advanced"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(406, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(30, 13)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Style"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(328, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 13)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Thickness"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(257, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Display as"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(198, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Colors"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(150, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Scale"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(118, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Show"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Value name"
        '
        'HelpText
        '
        Me.HelpText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpText.Location = New System.Drawing.Point(0, 0)
        Me.HelpText.Multiline = True
        Me.HelpText.Name = "HelpText"
        Me.HelpText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.HelpText.Size = New System.Drawing.Size(850, 84)
        Me.HelpText.TabIndex = 1
        '
        'StudyConfigurer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "StudyConfigurer"
        Me.Size = New System.Drawing.Size(850, 435)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.InputsGroup.ResumeLayout(False)
        Me.InputsGroup.PerformLayout()
        Me.ParametersGroup.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ValuesGroup.ResumeLayout(False)
        Me.ValuesPanel.ResumeLayout(False)
        Me.ValuesPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents InputsGroup As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ChartRegionCombo As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ParametersGroup As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Line5Text As System.Windows.Forms.TextBox
    Friend WithEvents Line4Text As System.Windows.Forms.TextBox
    Friend WithEvents Line3Text As System.Windows.Forms.TextBox
    Friend WithEvents Line2Text As System.Windows.Forms.TextBox
    Friend WithEvents Line1Text As System.Windows.Forms.TextBox
    Friend WithEvents ValuesGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ValuesPanel As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents InputsPanel As System.Windows.Forms.Panel
    Friend WithEvents ParametersPanel As System.Windows.Forms.Panel
    Friend WithEvents Line5ColorPicker As ColorPicker
    Friend WithEvents Line4ColorPicker As ColorPicker
    Friend WithEvents Line3ColorPicker As ColorPicker
    Friend WithEvents Line2ColorPicker As ColorPicker
    Friend WithEvents Line1ColorPicker As ColorPicker
    Friend WithEvents HelpText As System.Windows.Forms.TextBox
    Friend WithEvents BaseStudiesTree As System.Windows.Forms.TreeView

End Class
