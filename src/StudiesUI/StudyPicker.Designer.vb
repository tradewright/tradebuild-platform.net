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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class StudyPicker
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
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.RemoveButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ChartStudiesTree = New System.Windows.Forms.TreeView()
        Me.ChangeButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ConfigureButton = New System.Windows.Forms.Button()
        Me.StudyList = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.HelpText = New System.Windows.Forms.TextBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
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
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.Controls.Add(Me.HelpText)
        Me.SplitContainer1.Size = New System.Drawing.Size(509, 341)
        Me.SplitContainer1.SplitterDistance = 267
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
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ConfigureButton)
        Me.SplitContainer2.Panel1.Controls.Add(Me.StudyList)
        Me.SplitContainer2.Panel1.Controls.Add(Me.AddButton)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RemoveButton)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ChartStudiesTree)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ChangeButton)
        Me.SplitContainer2.Size = New System.Drawing.Size(509, 267)
        Me.SplitContainer2.SplitterDistance = 231
        Me.SplitContainer2.TabIndex = 0
        '
        'AddButton
        '
        Me.AddButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddButton.BackColor = System.Drawing.SystemColors.Control
        Me.AddButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.AddButton.Enabled = False
        Me.AddButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue
        Me.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AddButton.Location = New System.Drawing.Point(203, 44)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AddButton.Size = New System.Drawing.Size(25, 25)
        Me.AddButton.TabIndex = 24
        Me.AddButton.Text = ">"
        Me.ToolTip1.SetToolTip(Me.AddButton, "Add study to chart")
        Me.AddButton.UseVisualStyleBackColor = False
        '
        'RemoveButton
        '
        Me.RemoveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveButton.BackColor = System.Drawing.SystemColors.Control
        Me.RemoveButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.RemoveButton.Enabled = False
        Me.RemoveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue
        Me.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RemoveButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RemoveButton.Location = New System.Drawing.Point(204, 75)
        Me.RemoveButton.Name = "RemoveButton"
        Me.RemoveButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RemoveButton.Size = New System.Drawing.Size(25, 25)
        Me.RemoveButton.TabIndex = 21
        Me.RemoveButton.Text = "<"
        Me.ToolTip1.SetToolTip(Me.RemoveButton, "Remove study from chart")
        Me.RemoveButton.UseVisualStyleBackColor = False
        '
        'ChartStudiesTree
        '
        Me.ChartStudiesTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChartStudiesTree.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ChartStudiesTree.HideSelection = False
        Me.ChartStudiesTree.Location = New System.Drawing.Point(4, 32)
        Me.ChartStudiesTree.Name = "ChartStudiesTree"
        Me.ChartStudiesTree.ShowLines = False
        Me.ChartStudiesTree.ShowPlusMinus = False
        Me.ChartStudiesTree.ShowRootLines = False
        Me.ChartStudiesTree.Size = New System.Drawing.Size(266, 195)
        Me.ChartStudiesTree.TabIndex = 32
        '
        'ChangeButton
        '
        Me.ChangeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChangeButton.BackColor = System.Drawing.SystemColors.Control
        Me.ChangeButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChangeButton.Enabled = False
        Me.ChangeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue
        Me.ChangeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ChangeButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChangeButton.Location = New System.Drawing.Point(197, 234)
        Me.ChangeButton.Name = "ChangeButton"
        Me.ChangeButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChangeButton.Size = New System.Drawing.Size(73, 25)
        Me.ChangeButton.TabIndex = 31
        Me.ChangeButton.Text = "Change"
        Me.ToolTip1.SetToolTip(Me.ChangeButton, "Change selected study's configuration")
        Me.ChangeButton.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(4, 8, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(274, 32)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Studies in chart"
        '
        'ConfigureButton
        '
        Me.ConfigureButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfigureButton.BackColor = System.Drawing.SystemColors.Control
        Me.ConfigureButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.ConfigureButton.Enabled = False
        Me.ConfigureButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue
        Me.ConfigureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ConfigureButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ConfigureButton.Location = New System.Drawing.Point(125, 234)
        Me.ConfigureButton.Name = "ConfigureButton"
        Me.ConfigureButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ConfigureButton.Size = New System.Drawing.Size(73, 25)
        Me.ConfigureButton.TabIndex = 29
        Me.ConfigureButton.Text = "Co&nfigure"
        Me.ToolTip1.SetToolTip(Me.ConfigureButton, "Configure selected study")
        Me.ConfigureButton.UseVisualStyleBackColor = False
        '
        'StudyList
        '
        Me.StudyList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StudyList.BackColor = System.Drawing.SystemColors.Window
        Me.StudyList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.StudyList.Cursor = System.Windows.Forms.Cursors.Default
        Me.StudyList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.StudyList.HorizontalExtent = 1024
        Me.StudyList.HorizontalScrollbar = True
        Me.StudyList.Location = New System.Drawing.Point(4, 32)
        Me.StudyList.Name = "StudyList"
        Me.StudyList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StudyList.Size = New System.Drawing.Size(194, 195)
        Me.StudyList.TabIndex = 28
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(4, 8, 0, 4)
        Me.Label2.Size = New System.Drawing.Size(231, 32)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Available studies"
        '
        'HelpText
        '
        Me.HelpText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.HelpText.Location = New System.Drawing.Point(4, 2)
        Me.HelpText.Multiline = True
        Me.HelpText.Name = "HelpText"
        Me.HelpText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.HelpText.Size = New System.Drawing.Size(505, 67)
        Me.HelpText.TabIndex = 1
        '
        'StudyPicker
        '
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "StudyPicker"
        Me.Size = New System.Drawing.Size(509, 341)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents RemoveButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ConfigureButton As System.Windows.Forms.Button
    Friend WithEvents StudyList As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ChartStudiesTree As System.Windows.Forms.TreeView
    Friend WithEvents ChangeButton As System.Windows.Forms.Button
    Friend WithEvents HelpText As System.Windows.Forms.TextBox
#End Region
End Class