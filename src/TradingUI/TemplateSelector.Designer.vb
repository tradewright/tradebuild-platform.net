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
Partial Class TemplateSelector
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
        Me.TemplateListBox = New System.Windows.Forms.ListBox()
        Me.NameTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NotesTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CashCheckBox = New System.Windows.Forms.CheckBox()
        Me.ComboCheckBox = New System.Windows.Forms.CheckBox()
        Me.FuturesCheckBox = New System.Windows.Forms.CheckBox()
        Me.FuturesOptionsCheckBox = New System.Windows.Forms.CheckBox()
        Me.AllCheckBox = New System.Windows.Forms.CheckBox()
        Me.IndexesCheckBox = New System.Windows.Forms.CheckBox()
        Me.OptionsCheckBox = New System.Windows.Forms.CheckBox()
        Me.StocksCheckBox = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'TemplateListBox
        '
        Me.TemplateListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TemplateListBox.FormattingEnabled = True
        Me.TemplateListBox.IntegralHeight = False
        Me.TemplateListBox.Location = New System.Drawing.Point(0, 0)
        Me.TemplateListBox.MultiColumn = True
        Me.TemplateListBox.Name = "TemplateListBox"
        Me.TemplateListBox.Size = New System.Drawing.Size(197, 56)
        Me.TemplateListBox.TabIndex = 0
        '
        'NameTextBox
        '
        Me.NameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NameTextBox.Location = New System.Drawing.Point(0, 75)
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.Size = New System.Drawing.Size(197, 20)
        Me.NameTextBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.LightBlue
        Me.Label1.Location = New System.Drawing.Point(0, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(197, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Name"
        '
        'NotesTextBox
        '
        Me.NotesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NotesTextBox.Location = New System.Drawing.Point(0, 114)
        Me.NotesTextBox.Multiline = True
        Me.NotesTextBox.Name = "NotesTextBox"
        Me.NotesTextBox.Size = New System.Drawing.Size(197, 92)
        Me.NotesTextBox.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.LightBlue
        Me.Label2.Location = New System.Drawing.Point(0, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(197, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Description"
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.LightBlue
        Me.Label3.Location = New System.Drawing.Point(0, 209)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(197, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Default for"
        '
        'CashCheckBox
        '
        Me.CashCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CashCheckBox.AutoSize = True
        Me.CashCheckBox.Location = New System.Drawing.Point(19, 250)
        Me.CashCheckBox.Name = "CashCheckBox"
        Me.CashCheckBox.Size = New System.Drawing.Size(50, 17)
        Me.CashCheckBox.TabIndex = 7
        Me.CashCheckBox.Text = "Cash"
        Me.CashCheckBox.UseVisualStyleBackColor = True
        '
        'ComboCheckBox
        '
        Me.ComboCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboCheckBox.AutoSize = True
        Me.ComboCheckBox.Location = New System.Drawing.Point(19, 271)
        Me.ComboCheckBox.Name = "ComboCheckBox"
        Me.ComboCheckBox.Size = New System.Drawing.Size(59, 17)
        Me.ComboCheckBox.TabIndex = 8
        Me.ComboCheckBox.Text = "Combo"
        Me.ComboCheckBox.UseVisualStyleBackColor = True
        '
        'FuturesCheckBox
        '
        Me.FuturesCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FuturesCheckBox.AutoSize = True
        Me.FuturesCheckBox.Location = New System.Drawing.Point(19, 292)
        Me.FuturesCheckBox.Name = "FuturesCheckBox"
        Me.FuturesCheckBox.Size = New System.Drawing.Size(61, 17)
        Me.FuturesCheckBox.TabIndex = 9
        Me.FuturesCheckBox.Text = "Futures"
        Me.FuturesCheckBox.UseVisualStyleBackColor = True
        '
        'FuturesOptionsCheckBox
        '
        Me.FuturesOptionsCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FuturesOptionsCheckBox.AutoSize = True
        Me.FuturesOptionsCheckBox.Location = New System.Drawing.Point(96, 229)
        Me.FuturesOptionsCheckBox.Name = "FuturesOptionsCheckBox"
        Me.FuturesOptionsCheckBox.Size = New System.Drawing.Size(98, 17)
        Me.FuturesOptionsCheckBox.TabIndex = 10
        Me.FuturesOptionsCheckBox.Text = "Futures options"
        Me.FuturesOptionsCheckBox.UseVisualStyleBackColor = True
        '
        'AllCheckBox
        '
        Me.AllCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AllCheckBox.AutoSize = True
        Me.AllCheckBox.Location = New System.Drawing.Point(19, 229)
        Me.AllCheckBox.Name = "AllCheckBox"
        Me.AllCheckBox.Size = New System.Drawing.Size(37, 17)
        Me.AllCheckBox.TabIndex = 11
        Me.AllCheckBox.Text = "All"
        Me.AllCheckBox.UseVisualStyleBackColor = True
        '
        'IndexesCheckBox
        '
        Me.IndexesCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IndexesCheckBox.AutoSize = True
        Me.IndexesCheckBox.Location = New System.Drawing.Point(96, 250)
        Me.IndexesCheckBox.Name = "IndexesCheckBox"
        Me.IndexesCheckBox.Size = New System.Drawing.Size(63, 17)
        Me.IndexesCheckBox.TabIndex = 12
        Me.IndexesCheckBox.Text = "Indexes"
        Me.IndexesCheckBox.UseVisualStyleBackColor = True
        '
        'OptionsCheckBox
        '
        Me.OptionsCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.OptionsCheckBox.AutoSize = True
        Me.OptionsCheckBox.Location = New System.Drawing.Point(96, 271)
        Me.OptionsCheckBox.Name = "OptionsCheckBox"
        Me.OptionsCheckBox.Size = New System.Drawing.Size(62, 17)
        Me.OptionsCheckBox.TabIndex = 13
        Me.OptionsCheckBox.Text = "Options"
        Me.OptionsCheckBox.UseVisualStyleBackColor = True
        '
        'StocksCheckBox
        '
        Me.StocksCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.StocksCheckBox.AutoSize = True
        Me.StocksCheckBox.Location = New System.Drawing.Point(97, 292)
        Me.StocksCheckBox.Name = "StocksCheckBox"
        Me.StocksCheckBox.Size = New System.Drawing.Size(59, 17)
        Me.StocksCheckBox.TabIndex = 14
        Me.StocksCheckBox.Text = "Stocks"
        Me.StocksCheckBox.UseVisualStyleBackColor = True
        '
        'TemplateSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.NotesTextBox)
        Me.Controls.Add(Me.StocksCheckBox)
        Me.Controls.Add(Me.OptionsCheckBox)
        Me.Controls.Add(Me.IndexesCheckBox)
        Me.Controls.Add(Me.AllCheckBox)
        Me.Controls.Add(Me.FuturesOptionsCheckBox)
        Me.Controls.Add(Me.FuturesCheckBox)
        Me.Controls.Add(Me.ComboCheckBox)
        Me.Controls.Add(Me.CashCheckBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NameTextBox)
        Me.Controls.Add(Me.TemplateListBox)
        Me.MinimumSize = New System.Drawing.Size(197, 307)
        Me.Name = "TemplateSelector"
        Me.Size = New System.Drawing.Size(197, 307)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TemplateListBox As System.Windows.Forms.ListBox
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NotesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CashCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ComboCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents FuturesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents FuturesOptionsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AllCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents IndexesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents OptionsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents StocksCheckBox As System.Windows.Forms.CheckBox

End Class
