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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class fSimpleColorPicker
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
	Public WithEvents NoColorButton As System.Windows.Forms.Button
	Public WithEvents MoreColorsButton As System.Windows.Forms.Button
	Public WithEvents _ColorLabel_19 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_18 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_17 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_16 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_15 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_14 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_13 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_12 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_11 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_10 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_1 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_2 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_3 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_4 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_5 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_6 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_0 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_7 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_8 As System.Windows.Forms.Label
	Public WithEvents _ColorLabel_9 As System.Windows.Forms.Label
    'Public WithEvents Line1 As Microsoft.VisualBasic.PowerPacks.LineShape
    'Public WithEvents Line2 As Microsoft.VisualBasic.PowerPacks.LineShape
    'Public WithEvents Line3 As Microsoft.VisualBasic.PowerPacks.LineShape
    'Public WithEvents Line4 As Microsoft.VisualBasic.PowerPacks.LineShape
	Public WithEvents InitialColorLabel As System.Windows.Forms.Label
    '	Public WithEvents ColorLabel As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'Public WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.NoColorButton = New System.Windows.Forms.Button()
        Me.MoreColorsButton = New System.Windows.Forms.Button()
        Me._ColorLabel_19 = New System.Windows.Forms.Label()
        Me._ColorLabel_18 = New System.Windows.Forms.Label()
        Me._ColorLabel_17 = New System.Windows.Forms.Label()
        Me._ColorLabel_16 = New System.Windows.Forms.Label()
        Me._ColorLabel_15 = New System.Windows.Forms.Label()
        Me._ColorLabel_14 = New System.Windows.Forms.Label()
        Me._ColorLabel_13 = New System.Windows.Forms.Label()
        Me._ColorLabel_12 = New System.Windows.Forms.Label()
        Me._ColorLabel_11 = New System.Windows.Forms.Label()
        Me._ColorLabel_10 = New System.Windows.Forms.Label()
        Me._ColorLabel_1 = New System.Windows.Forms.Label()
        Me._ColorLabel_2 = New System.Windows.Forms.Label()
        Me._ColorLabel_3 = New System.Windows.Forms.Label()
        Me._ColorLabel_4 = New System.Windows.Forms.Label()
        Me._ColorLabel_5 = New System.Windows.Forms.Label()
        Me._ColorLabel_6 = New System.Windows.Forms.Label()
        Me._ColorLabel_0 = New System.Windows.Forms.Label()
        Me._ColorLabel_7 = New System.Windows.Forms.Label()
        Me._ColorLabel_8 = New System.Windows.Forms.Label()
        Me._ColorLabel_9 = New System.Windows.Forms.Label()
        Me.InitialColorLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'NoColorButton
        '
        Me.NoColorButton.BackColor = System.Drawing.SystemColors.Control
        Me.NoColorButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.NoColorButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.NoColorButton.Location = New System.Drawing.Point(8, 205)
        Me.NoColorButton.Name = "NoColorButton"
        Me.NoColorButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NoColorButton.Size = New System.Drawing.Size(105, 25)
        Me.NoColorButton.TabIndex = 22
        Me.NoColorButton.Text = "No color"
        Me.NoColorButton.UseVisualStyleBackColor = False
        '
        'MoreColorsButton
        '
        Me.MoreColorsButton.BackColor = System.Drawing.SystemColors.Control
        Me.MoreColorsButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.MoreColorsButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MoreColorsButton.Location = New System.Drawing.Point(8, 184)
        Me.MoreColorsButton.Name = "MoreColorsButton"
        Me.MoreColorsButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MoreColorsButton.Size = New System.Drawing.Size(105, 22)
        Me.MoreColorsButton.TabIndex = 0
        Me.MoreColorsButton.Text = "More colors..."
        Me.MoreColorsButton.UseVisualStyleBackColor = False
        '
        '_ColorLabel_19
        '
        Me._ColorLabel_19.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer))
        Me._ColorLabel_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_19.Location = New System.Drawing.Point(8, 176)
        Me._ColorLabel_19.Name = "_ColorLabel_19"
        Me._ColorLabel_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_19.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_19.TabIndex = 21
        '
        '_ColorLabel_18
        '
        Me._ColorLabel_18.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me._ColorLabel_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_18.Location = New System.Drawing.Point(8, 112)
        Me._ColorLabel_18.Name = "_ColorLabel_18"
        Me._ColorLabel_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_18.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_18.TabIndex = 20
        '
        '_ColorLabel_17
        '
        Me._ColorLabel_17.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._ColorLabel_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_17.Location = New System.Drawing.Point(8, 104)
        Me._ColorLabel_17.Name = "_ColorLabel_17"
        Me._ColorLabel_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_17.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_17.TabIndex = 19
        '
        '_ColorLabel_16
        '
        Me._ColorLabel_16.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._ColorLabel_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_16.Location = New System.Drawing.Point(8, 96)
        Me._ColorLabel_16.Name = "_ColorLabel_16"
        Me._ColorLabel_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_16.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_16.TabIndex = 18
        '
        '_ColorLabel_15
        '
        Me._ColorLabel_15.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._ColorLabel_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_15.Location = New System.Drawing.Point(8, 88)
        Me._ColorLabel_15.Name = "_ColorLabel_15"
        Me._ColorLabel_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_15.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_15.TabIndex = 17
        '
        '_ColorLabel_14
        '
        Me._ColorLabel_14.BackColor = System.Drawing.Color.White
        Me._ColorLabel_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_14.Location = New System.Drawing.Point(8, 80)
        Me._ColorLabel_14.Name = "_ColorLabel_14"
        Me._ColorLabel_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_14.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_14.TabIndex = 16
        '
        '_ColorLabel_13
        '
        Me._ColorLabel_13.BackColor = System.Drawing.Color.Yellow
        Me._ColorLabel_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_13.Location = New System.Drawing.Point(8, 72)
        Me._ColorLabel_13.Name = "_ColorLabel_13"
        Me._ColorLabel_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_13.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_13.TabIndex = 15
        '
        '_ColorLabel_12
        '
        Me._ColorLabel_12.BackColor = System.Drawing.Color.Magenta
        Me._ColorLabel_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_12.Location = New System.Drawing.Point(8, 64)
        Me._ColorLabel_12.Name = "_ColorLabel_12"
        Me._ColorLabel_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_12.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_12.TabIndex = 14
        '
        '_ColorLabel_11
        '
        Me._ColorLabel_11.BackColor = System.Drawing.Color.Cyan
        Me._ColorLabel_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_11.Location = New System.Drawing.Point(8, 56)
        Me._ColorLabel_11.Name = "_ColorLabel_11"
        Me._ColorLabel_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_11.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_11.TabIndex = 13
        '
        '_ColorLabel_10
        '
        Me._ColorLabel_10.BackColor = System.Drawing.Color.Green
        Me._ColorLabel_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_10.Location = New System.Drawing.Point(8, 40)
        Me._ColorLabel_10.Name = "_ColorLabel_10"
        Me._ColorLabel_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_10.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_10.TabIndex = 12
        '
        '_ColorLabel_1
        '
        Me._ColorLabel_1.BackColor = System.Drawing.Color.Red
        Me._ColorLabel_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_1.Location = New System.Drawing.Point(8, 32)
        Me._ColorLabel_1.Name = "_ColorLabel_1"
        Me._ColorLabel_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_1.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_1.TabIndex = 11
        '
        '_ColorLabel_2
        '
        Me._ColorLabel_2.BackColor = System.Drawing.Color.Blue
        Me._ColorLabel_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_2.Location = New System.Drawing.Point(8, 48)
        Me._ColorLabel_2.Name = "_ColorLabel_2"
        Me._ColorLabel_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_2.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_2.TabIndex = 10
        '
        '_ColorLabel_3
        '
        Me._ColorLabel_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer))
        Me._ColorLabel_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_3.Location = New System.Drawing.Point(8, 144)
        Me._ColorLabel_3.Name = "_ColorLabel_3"
        Me._ColorLabel_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_3.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_3.TabIndex = 9
        '
        '_ColorLabel_4
        '
        Me._ColorLabel_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me._ColorLabel_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_4.Location = New System.Drawing.Point(8, 120)
        Me._ColorLabel_4.Name = "_ColorLabel_4"
        Me._ColorLabel_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_4.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_4.TabIndex = 8
        '
        '_ColorLabel_5
        '
        Me._ColorLabel_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._ColorLabel_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_5.Location = New System.Drawing.Point(8, 128)
        Me._ColorLabel_5.Name = "_ColorLabel_5"
        Me._ColorLabel_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_5.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_5.TabIndex = 7
        '
        '_ColorLabel_6
        '
        Me._ColorLabel_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me._ColorLabel_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_6.Location = New System.Drawing.Point(8, 136)
        Me._ColorLabel_6.Name = "_ColorLabel_6"
        Me._ColorLabel_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_6.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_6.TabIndex = 6
        '
        '_ColorLabel_0
        '
        Me._ColorLabel_0.BackColor = System.Drawing.Color.Black
        Me._ColorLabel_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_0.Location = New System.Drawing.Point(8, 24)
        Me._ColorLabel_0.Name = "_ColorLabel_0"
        Me._ColorLabel_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_0.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_0.TabIndex = 5
        Me._ColorLabel_0.Text = "Label7"
        '
        '_ColorLabel_7
        '
        Me._ColorLabel_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._ColorLabel_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_7.Location = New System.Drawing.Point(8, 152)
        Me._ColorLabel_7.Name = "_ColorLabel_7"
        Me._ColorLabel_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_7.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_7.TabIndex = 4
        '
        '_ColorLabel_8
        '
        Me._ColorLabel_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._ColorLabel_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_8.Location = New System.Drawing.Point(8, 160)
        Me._ColorLabel_8.Name = "_ColorLabel_8"
        Me._ColorLabel_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_8.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_8.TabIndex = 3
        '
        '_ColorLabel_9
        '
        Me._ColorLabel_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._ColorLabel_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._ColorLabel_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._ColorLabel_9.Location = New System.Drawing.Point(8, 168)
        Me._ColorLabel_9.Name = "_ColorLabel_9"
        Me._ColorLabel_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ColorLabel_9.Size = New System.Drawing.Size(105, 8)
        Me._ColorLabel_9.TabIndex = 2
        '
        'InitialColorLabel
        '
        Me.InitialColorLabel.BackColor = System.Drawing.SystemColors.Control
        Me.InitialColorLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.InitialColorLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InitialColorLabel.Location = New System.Drawing.Point(8, 8)
        Me.InitialColorLabel.Name = "InitialColorLabel"
        Me.InitialColorLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.InitialColorLabel.Size = New System.Drawing.Size(105, 9)
        Me.InitialColorLabel.TabIndex = 1
        '
        'fSimpleColorPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(121, 236)
        Me.ControlBox = False
        Me.Controls.Add(Me.NoColorButton)
        Me.Controls.Add(Me.MoreColorsButton)
        Me.Controls.Add(Me._ColorLabel_19)
        Me.Controls.Add(Me._ColorLabel_18)
        Me.Controls.Add(Me._ColorLabel_17)
        Me.Controls.Add(Me._ColorLabel_16)
        Me.Controls.Add(Me._ColorLabel_15)
        Me.Controls.Add(Me._ColorLabel_14)
        Me.Controls.Add(Me._ColorLabel_13)
        Me.Controls.Add(Me._ColorLabel_12)
        Me.Controls.Add(Me._ColorLabel_11)
        Me.Controls.Add(Me._ColorLabel_10)
        Me.Controls.Add(Me._ColorLabel_1)
        Me.Controls.Add(Me._ColorLabel_2)
        Me.Controls.Add(Me._ColorLabel_3)
        Me.Controls.Add(Me._ColorLabel_4)
        Me.Controls.Add(Me._ColorLabel_5)
        Me.Controls.Add(Me._ColorLabel_6)
        Me.Controls.Add(Me._ColorLabel_0)
        Me.Controls.Add(Me._ColorLabel_7)
        Me.Controls.Add(Me._ColorLabel_8)
        Me.Controls.Add(Me._ColorLabel_9)
        Me.Controls.Add(Me.InitialColorLabel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fSimpleColorPicker"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub
#End Region 
End Class