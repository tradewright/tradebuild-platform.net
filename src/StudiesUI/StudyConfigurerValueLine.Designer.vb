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
Partial Class StudyConfigurerValueLine
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
        Me.components = New System.ComponentModel.Container
        Me.AdvancedButton = New System.Windows.Forms.Button
        Me.ValueNameLabel = New System.Windows.Forms.Label
        Me.FontButton = New System.Windows.Forms.Button
        Me.StyleCombo = New System.Windows.Forms.ComboBox
        Me.ThicknessUpDown = New System.Windows.Forms.NumericUpDown
        Me.DisplayModeCombo = New System.Windows.Forms.ComboBox
        Me.AutoscaleCheck = New System.Windows.Forms.CheckBox
        Me.IncludeCheck = New System.Windows.Forms.CheckBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ColorPicker = New TradeWright.Trading.UI.Studies.ColorPicker
        Me.UpColorPicker = New TradeWright.Trading.UI.Studies.ColorPicker
        Me.DownColorPicker = New TradeWright.Trading.UI.Studies.ColorPicker
        CType(Me.ThicknessUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AdvancedButton
        '
        Me.AdvancedButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AdvancedButton.Location = New System.Drawing.Point(427, 0)
        Me.AdvancedButton.Name = "AdvancedButton"
        Me.AdvancedButton.Size = New System.Drawing.Size(28, 20)
        Me.AdvancedButton.TabIndex = 31
        Me.AdvancedButton.Text = "..."
        Me.AdvancedButton.UseVisualStyleBackColor = True
        '
        'ValueNameLabel
        '
        Me.ValueNameLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValueNameLabel.Location = New System.Drawing.Point(0, 4)
        Me.ValueNameLabel.Name = "ValueNameLabel"
        Me.ValueNameLabel.Size = New System.Drawing.Size(82, 19)
        Me.ValueNameLabel.TabIndex = 30
        Me.ValueNameLabel.Text = "Label11"
        '
        'FontButton
        '
        Me.FontButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FontButton.Location = New System.Drawing.Point(357, 0)
        Me.FontButton.Name = "FontButton"
        Me.FontButton.Size = New System.Drawing.Size(44, 21)
        Me.FontButton.TabIndex = 29
        Me.FontButton.Text = "Font..."
        Me.FontButton.UseVisualStyleBackColor = True
        '
        'StyleCombo
        '
        Me.StyleCombo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StyleCombo.FormattingEnabled = True
        Me.StyleCombo.Location = New System.Drawing.Point(339, 0)
        Me.StyleCombo.Name = "StyleCombo"
        Me.StyleCombo.Size = New System.Drawing.Size(82, 21)
        Me.StyleCombo.TabIndex = 28
        '
        'ThicknessUpDown
        '
        Me.ThicknessUpDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ThicknessUpDown.Location = New System.Drawing.Point(293, 0)
        Me.ThicknessUpDown.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.ThicknessUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.ThicknessUpDown.Name = "ThicknessUpDown"
        Me.ThicknessUpDown.Size = New System.Drawing.Size(40, 20)
        Me.ThicknessUpDown.TabIndex = 27
        Me.ThicknessUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'DisplayModeCombo
        '
        Me.DisplayModeCombo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayModeCombo.FormattingEnabled = True
        Me.DisplayModeCombo.Location = New System.Drawing.Point(213, 0)
        Me.DisplayModeCombo.Name = "DisplayModeCombo"
        Me.DisplayModeCombo.Size = New System.Drawing.Size(74, 21)
        Me.DisplayModeCombo.TabIndex = 25
        '
        'AutoscaleCheck
        '
        Me.AutoscaleCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AutoscaleCheck.AutoSize = True
        Me.AutoscaleCheck.Location = New System.Drawing.Point(109, 3)
        Me.AutoscaleCheck.Name = "AutoscaleCheck"
        Me.AutoscaleCheck.Size = New System.Drawing.Size(15, 14)
        Me.AutoscaleCheck.TabIndex = 21
        Me.AutoscaleCheck.UseVisualStyleBackColor = True
        '
        'IncludeCheck
        '
        Me.IncludeCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IncludeCheck.AutoSize = True
        Me.IncludeCheck.Location = New System.Drawing.Point(88, 3)
        Me.IncludeCheck.Name = "IncludeCheck"
        Me.IncludeCheck.Size = New System.Drawing.Size(15, 14)
        Me.IncludeCheck.TabIndex = 20
        Me.IncludeCheck.UseVisualStyleBackColor = True
        '
        'ColorPicker
        '
        Me.ColorPicker.AllowNull = False
        Me.ColorPicker.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ColorPicker.Color = System.Drawing.Color.Blue
        Me.ColorPicker.Location = New System.Drawing.Point(134, 0)
        Me.ColorPicker.Name = "ColorPicker"
        Me.ColorPicker.OleColor = 16711680
        Me.ColorPicker.Size = New System.Drawing.Size(21, 21)
        Me.ColorPicker.TabIndex = 32
        '
        'UpColorPicker
        '
        Me.UpColorPicker.AllowNull = False
        Me.UpColorPicker.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UpColorPicker.Color = System.Drawing.Color.Blue
        Me.UpColorPicker.Location = New System.Drawing.Point(160, 0)
        Me.UpColorPicker.Name = "UpColorPicker"
        Me.UpColorPicker.OleColor = 16711680
        Me.UpColorPicker.Size = New System.Drawing.Size(21, 21)
        Me.UpColorPicker.TabIndex = 33
        '
        'DownColorPicker
        '
        Me.DownColorPicker.AllowNull = False
        Me.DownColorPicker.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DownColorPicker.Color = System.Drawing.Color.Blue
        Me.DownColorPicker.Location = New System.Drawing.Point(186, 0)
        Me.DownColorPicker.Name = "DownColorPicker"
        Me.DownColorPicker.OleColor = 16711680
        Me.DownColorPicker.Size = New System.Drawing.Size(21, 21)
        Me.DownColorPicker.TabIndex = 34
        '
        'StudyConfigurerValueLine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DownColorPicker)
        Me.Controls.Add(Me.UpColorPicker)
        Me.Controls.Add(Me.ColorPicker)
        Me.Controls.Add(Me.AdvancedButton)
        Me.Controls.Add(Me.ValueNameLabel)
        Me.Controls.Add(Me.FontButton)
        Me.Controls.Add(Me.StyleCombo)
        Me.Controls.Add(Me.ThicknessUpDown)
        Me.Controls.Add(Me.DisplayModeCombo)
        Me.Controls.Add(Me.AutoscaleCheck)
        Me.Controls.Add(Me.IncludeCheck)
        Me.MinimumSize = New System.Drawing.Size(407, 22)
        Me.Name = "StudyConfigurerValueLine"
        Me.Size = New System.Drawing.Size(459, 22)
        CType(Me.ThicknessUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AdvancedButton As System.Windows.Forms.Button
    Friend WithEvents ValueNameLabel As System.Windows.Forms.Label
    Friend WithEvents FontButton As System.Windows.Forms.Button
    Friend WithEvents StyleCombo As System.Windows.Forms.ComboBox
    Friend WithEvents ThicknessUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents DisplayModeCombo As System.Windows.Forms.ComboBox
    Friend WithEvents AutoscaleCheck As System.Windows.Forms.CheckBox
    Friend WithEvents IncludeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ColorPicker As ColorPicker
    Friend WithEvents UpColorPicker As ColorPicker
    Friend WithEvents DownColorPicker As ColorPicker

End Class
