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
Partial Class StudyConfigurerParameterLine
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
        Me.ParameterValueCheck = New System.Windows.Forms.CheckBox
        Me.ParameterValueCombo = New System.Windows.Forms.ComboBox
        Me.ParameterValueText = New System.Windows.Forms.TextBox
        Me.ParameterNameLabel = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ParameterValueUpDown = New System.Windows.Forms.NumericUpDown
        CType(Me.ParameterValueUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ParameterValueCheck
        '
        Me.ParameterValueCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ParameterValueCheck.AutoSize = True
        Me.ParameterValueCheck.Location = New System.Drawing.Point(142, 4)
        Me.ParameterValueCheck.Name = "ParameterValueCheck"
        Me.ParameterValueCheck.Size = New System.Drawing.Size(15, 14)
        Me.ParameterValueCheck.TabIndex = 9
        Me.ParameterValueCheck.UseVisualStyleBackColor = True
        '
        'ParameterValueCombo
        '
        Me.ParameterValueCombo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ParameterValueCombo.FormattingEnabled = True
        Me.ParameterValueCombo.Location = New System.Drawing.Point(116, 0)
        Me.ParameterValueCombo.Name = "ParameterValueCombo"
        Me.ParameterValueCombo.Size = New System.Drawing.Size(58, 21)
        Me.ParameterValueCombo.TabIndex = 8
        '
        'ParameterValueText
        '
        Me.ParameterValueText.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ParameterValueText.Location = New System.Drawing.Point(114, 0)
        Me.ParameterValueText.Name = "ParameterValueText"
        Me.ParameterValueText.Size = New System.Drawing.Size(43, 20)
        Me.ParameterValueText.TabIndex = 6
        '
        'ParameterNameLabel
        '
        Me.ParameterNameLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ParameterNameLabel.Location = New System.Drawing.Point(0, 3)
        Me.ParameterNameLabel.Name = "ParameterNameLabel"
        Me.ParameterNameLabel.Size = New System.Drawing.Size(105, 17)
        Me.ParameterNameLabel.TabIndex = 5
        Me.ParameterNameLabel.Text = "Label4"
        '
        'ParameterValueUpDown
        '
        Me.ParameterValueUpDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ParameterValueUpDown.Location = New System.Drawing.Point(114, 0)
        Me.ParameterValueUpDown.Name = "ParameterValueUpDown"
        Me.ParameterValueUpDown.Size = New System.Drawing.Size(60, 20)
        Me.ParameterValueUpDown.TabIndex = 10
        '
        'StudyConfigurerParameterLine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ParameterValueUpDown)
        Me.Controls.Add(Me.ParameterValueCheck)
        Me.Controls.Add(Me.ParameterValueCombo)
        Me.Controls.Add(Me.ParameterValueText)
        Me.Controls.Add(Me.ParameterNameLabel)
        Me.Name = "StudyConfigurerParameterLine"
        Me.Size = New System.Drawing.Size(177, 23)
        CType(Me.ParameterValueUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ParameterValueCheck As System.Windows.Forms.CheckBox
    Friend WithEvents ParameterValueCombo As System.Windows.Forms.ComboBox
    Friend WithEvents ParameterValueText As System.Windows.Forms.TextBox
    Friend WithEvents ParameterNameLabel As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ParameterValueUpDown As System.Windows.Forms.NumericUpDown

End Class
