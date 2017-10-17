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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class StudyLibConfigurer
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
		UserControl_Initialize()
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
	Friend WithEvents CancelButton_Renamed As System.Windows.Forms.Button
    Friend WithEvents Frame1 As System.Windows.Forms.GroupBox
	Friend WithEvents AddButton As System.Windows.Forms.Button
	Friend WithEvents NameText As System.Windows.Forms.TextBox
	Friend WithEvents EnabledCheck As System.Windows.Forms.CheckBox
	Friend WithEvents ApplyButton As System.Windows.Forms.Button
	Friend WithEvents DownButton As System.Windows.Forms.Button
	Friend WithEvents UpButton As System.Windows.Forms.Button
	Friend WithEvents RemoveButton As System.Windows.Forms.Button
	Friend WithEvents StudyLibList As System.Windows.Forms.ListBox
	Friend WithEvents Label9 As System.Windows.Forms.Label
    'Friend WithEvents OutlineBox As Microsoft.VisualBasic.PowerPacks.RectangleShape
    'Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AddButton = New System.Windows.Forms.Button
        Me.DownButton = New System.Windows.Forms.Button
        Me.UpButton = New System.Windows.Forms.Button
        Me.RemoveButton = New System.Windows.Forms.Button
        Me.CancelButton_Renamed = New System.Windows.Forms.Button
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.ProgIdText = New System.Windows.Forms.TextBox
        Me.CustomOpt = New System.Windows.Forms.RadioButton
        Me.BuiltInOpt = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.NameText = New System.Windows.Forms.TextBox
        Me.EnabledCheck = New System.Windows.Forms.CheckBox
        Me.ApplyButton = New System.Windows.Forms.Button
        Me.StudyLibList = New System.Windows.Forms.ListBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'AddButton
        '
        Me.AddButton.BackColor = System.Drawing.SystemColors.Control
        Me.AddButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.AddButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AddButton.Location = New System.Drawing.Point(152, 8)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AddButton.Size = New System.Drawing.Size(25, 41)
        Me.AddButton.TabIndex = 7
        Me.AddButton.Text = "+"
        Me.ToolTip1.SetToolTip(Me.AddButton, "Add new")
        Me.AddButton.UseVisualStyleBackColor = False
        '
        'DownButton
        '
        Me.DownButton.BackColor = System.Drawing.SystemColors.Control
        Me.DownButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.DownButton.Enabled = False
        Me.DownButton.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.DownButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DownButton.Location = New System.Drawing.Point(152, 144)
        Me.DownButton.Name = "DownButton"
        Me.DownButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DownButton.Size = New System.Drawing.Size(25, 33)
        Me.DownButton.TabIndex = 9
        Me.DownButton.Text = "ò"
        Me.ToolTip1.SetToolTip(Me.DownButton, "Move down")
        Me.DownButton.UseVisualStyleBackColor = False
        '
        'UpButton
        '
        Me.UpButton.BackColor = System.Drawing.SystemColors.Control
        Me.UpButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.UpButton.Enabled = False
        Me.UpButton.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.UpButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UpButton.Location = New System.Drawing.Point(152, 96)
        Me.UpButton.Name = "UpButton"
        Me.UpButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.UpButton.Size = New System.Drawing.Size(25, 33)
        Me.UpButton.TabIndex = 8
        Me.UpButton.Text = "ñ"
        Me.ToolTip1.SetToolTip(Me.UpButton, "Move up")
        Me.UpButton.UseVisualStyleBackColor = False
        '
        'RemoveButton
        '
        Me.RemoveButton.BackColor = System.Drawing.SystemColors.Control
        Me.RemoveButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.RemoveButton.Enabled = False
        Me.RemoveButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RemoveButton.Location = New System.Drawing.Point(152, 216)
        Me.RemoveButton.Name = "RemoveButton"
        Me.RemoveButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RemoveButton.Size = New System.Drawing.Size(25, 41)
        Me.RemoveButton.TabIndex = 10
        Me.RemoveButton.Text = "X"
        Me.ToolTip1.SetToolTip(Me.RemoveButton, "Delete")
        Me.RemoveButton.UseVisualStyleBackColor = False
        '
        'CancelButton_Renamed
        '
        Me.CancelButton_Renamed.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton_Renamed.Cursor = System.Windows.Forms.Cursors.Default
        Me.CancelButton_Renamed.Enabled = False
        Me.CancelButton_Renamed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CancelButton_Renamed.Location = New System.Drawing.Point(352, 232)
        Me.CancelButton_Renamed.Name = "CancelButton_Renamed"
        Me.CancelButton_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CancelButton_Renamed.Size = New System.Drawing.Size(65, 25)
        Me.CancelButton_Renamed.TabIndex = 15
        Me.CancelButton_Renamed.Text = "Cancel"
        Me.CancelButton_Renamed.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.ProgIdText)
        Me.Frame1.Controls.Add(Me.CustomOpt)
        Me.Frame1.Controls.Add(Me.BuiltInOpt)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(216, 80)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(265, 113)
        Me.Frame1.TabIndex = 12
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Library details"
        '
        'ProgIdText
        '
        Me.ProgIdText.AcceptsReturn = True
        Me.ProgIdText.BackColor = System.Drawing.SystemColors.Window
        Me.ProgIdText.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ProgIdText.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ProgIdText.Location = New System.Drawing.Point(51, 80)
        Me.ProgIdText.MaxLength = 0
        Me.ProgIdText.Name = "ProgIdText"
        Me.ProgIdText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ProgIdText.Size = New System.Drawing.Size(201, 20)
        Me.ProgIdText.TabIndex = 17
        '
        'CustomOpt
        '
        Me.CustomOpt.BackColor = System.Drawing.SystemColors.Control
        Me.CustomOpt.Cursor = System.Windows.Forms.Cursors.Default
        Me.CustomOpt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CustomOpt.Location = New System.Drawing.Point(11, 40)
        Me.CustomOpt.Name = "CustomOpt"
        Me.CustomOpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CustomOpt.Size = New System.Drawing.Size(265, 25)
        Me.CustomOpt.TabIndex = 16
        Me.CustomOpt.TabStop = True
        Me.CustomOpt.Text = "Use custom study library"
        Me.CustomOpt.UseVisualStyleBackColor = False
        '
        'BuiltInOpt
        '
        Me.BuiltInOpt.BackColor = System.Drawing.SystemColors.Control
        Me.BuiltInOpt.Cursor = System.Windows.Forms.Cursors.Default
        Me.BuiltInOpt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BuiltInOpt.Location = New System.Drawing.Point(11, 16)
        Me.BuiltInOpt.Name = "BuiltInOpt"
        Me.BuiltInOpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BuiltInOpt.Size = New System.Drawing.Size(265, 25)
        Me.BuiltInOpt.TabIndex = 15
        Me.BuiltInOpt.TabStop = True
        Me.BuiltInOpt.Text = "Use TradeBuild's built-in study library"
        Me.BuiltInOpt.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(51, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(89, 17)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Prog ID"
        '
        'NameText
        '
        Me.NameText.AcceptsReturn = True
        Me.NameText.BackColor = System.Drawing.SystemColors.Window
        Me.NameText.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.NameText.ForeColor = System.Drawing.SystemColors.WindowText
        Me.NameText.Location = New System.Drawing.Point(272, 56)
        Me.NameText.MaxLength = 0
        Me.NameText.Name = "NameText"
        Me.NameText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NameText.Size = New System.Drawing.Size(201, 20)
        Me.NameText.TabIndex = 1
        '
        'EnabledCheck
        '
        Me.EnabledCheck.BackColor = System.Drawing.SystemColors.Control
        Me.EnabledCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.EnabledCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.EnabledCheck.Location = New System.Drawing.Point(224, 32)
        Me.EnabledCheck.Name = "EnabledCheck"
        Me.EnabledCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.EnabledCheck.Size = New System.Drawing.Size(169, 17)
        Me.EnabledCheck.TabIndex = 0
        Me.EnabledCheck.Text = "Enabled"
        Me.EnabledCheck.UseVisualStyleBackColor = False
        '
        'ApplyButton
        '
        Me.ApplyButton.BackColor = System.Drawing.SystemColors.Control
        Me.ApplyButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.ApplyButton.Enabled = False
        Me.ApplyButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ApplyButton.Location = New System.Drawing.Point(424, 232)
        Me.ApplyButton.Name = "ApplyButton"
        Me.ApplyButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ApplyButton.Size = New System.Drawing.Size(65, 25)
        Me.ApplyButton.TabIndex = 5
        Me.ApplyButton.Text = "Apply"
        Me.ApplyButton.UseVisualStyleBackColor = False
        '
        'StudyLibList
        '
        Me.StudyLibList.BackColor = System.Drawing.SystemColors.Window
        Me.StudyLibList.Cursor = System.Windows.Forms.Cursors.Default
        Me.StudyLibList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.StudyLibList.Location = New System.Drawing.Point(8, 8)
        Me.StudyLibList.Name = "StudyLibList"
        Me.StudyLibList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StudyLibList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.StudyLibList.Size = New System.Drawing.Size(137, 251)
        Me.StudyLibList.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(224, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(41, 17)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Name"
        '
        'StudyLibConfigurer
        '
        Me.Controls.Add(Me.CancelButton_Renamed)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.AddButton)
        Me.Controls.Add(Me.NameText)
        Me.Controls.Add(Me.EnabledCheck)
        Me.Controls.Add(Me.ApplyButton)
        Me.Controls.Add(Me.DownButton)
        Me.Controls.Add(Me.UpButton)
        Me.Controls.Add(Me.RemoveButton)
        Me.Controls.Add(Me.StudyLibList)
        Me.Controls.Add(Me.Label9)
        Me.Name = "StudyLibConfigurer"
        Me.Size = New System.Drawing.Size(542, 317)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgIdText As System.Windows.Forms.TextBox
    Friend WithEvents CustomOpt As System.Windows.Forms.RadioButton
    Friend WithEvents BuiltInOpt As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
#End Region 
End Class