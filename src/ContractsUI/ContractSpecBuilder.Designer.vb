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
Partial Class ContractSpecBuilder
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
        Me.components = New System.ComponentModel.Container()
        Me.ShortNameLabel = New System.Windows.Forms.Label()
        Me.ShortNameText = New System.Windows.Forms.TextBox()
        Me.SymbolText = New System.Windows.Forms.TextBox()
        Me.SymbolLabel = New System.Windows.Forms.Label()
        Me.TypeLabel = New System.Windows.Forms.Label()
        Me.TypeCombo = New System.Windows.Forms.ComboBox()
        Me.ExpiryText = New System.Windows.Forms.TextBox()
        Me.ExpiryLabel = New System.Windows.Forms.Label()
        Me.ExchangeCombo = New System.Windows.Forms.ComboBox()
        Me.ExchangeLabel = New System.Windows.Forms.Label()
        Me.CurrencyCombo = New System.Windows.Forms.ComboBox()
        Me.CurrencyLabel = New System.Windows.Forms.Label()
        Me.StrikePriceText = New System.Windows.Forms.TextBox()
        Me.StrikePriceLabel = New System.Windows.Forms.Label()
        Me.RightCombo = New System.Windows.Forms.ComboBox()
        Me.RightLabel = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AdvancedButton = New System.Windows.Forms.Button()
        Me.MultiplierText = New System.Windows.Forms.TextBox()
        Me.MultiplierLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ShortNameLabel
        '
        Me.ShortNameLabel.AutoSize = True
        Me.ShortNameLabel.Location = New System.Drawing.Point(0, 4)
        Me.ShortNameLabel.Name = "ShortNameLabel"
        Me.ShortNameLabel.Size = New System.Drawing.Size(61, 13)
        Me.ShortNameLabel.TabIndex = 0
        Me.ShortNameLabel.Text = "Short name"
        '
        'ShortNameText
        '
        Me.ShortNameText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShortNameText.Location = New System.Drawing.Point(63, 1)
        Me.ShortNameText.Name = "ShortNameText"
        Me.ShortNameText.Size = New System.Drawing.Size(96, 20)
        Me.ShortNameText.TabIndex = 1
        '
        'SymbolText
        '
        Me.SymbolText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SymbolText.Location = New System.Drawing.Point(63, 28)
        Me.SymbolText.Name = "SymbolText"
        Me.SymbolText.Size = New System.Drawing.Size(96, 20)
        Me.SymbolText.TabIndex = 3
        '
        'SymbolLabel
        '
        Me.SymbolLabel.AutoSize = True
        Me.SymbolLabel.Location = New System.Drawing.Point(0, 31)
        Me.SymbolLabel.Name = "SymbolLabel"
        Me.SymbolLabel.Size = New System.Drawing.Size(41, 13)
        Me.SymbolLabel.TabIndex = 2
        Me.SymbolLabel.Text = "Symbol"
        '
        'TypeLabel
        '
        Me.TypeLabel.AutoSize = True
        Me.TypeLabel.Location = New System.Drawing.Point(0, 58)
        Me.TypeLabel.Name = "TypeLabel"
        Me.TypeLabel.Size = New System.Drawing.Size(31, 13)
        Me.TypeLabel.TabIndex = 4
        Me.TypeLabel.Text = "Type"
        '
        'TypeCombo
        '
        Me.TypeCombo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TypeCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.TypeCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TypeCombo.FormattingEnabled = True
        Me.TypeCombo.Location = New System.Drawing.Point(63, 55)
        Me.TypeCombo.Name = "TypeCombo"
        Me.TypeCombo.Size = New System.Drawing.Size(96, 21)
        Me.TypeCombo.TabIndex = 5
        '
        'ExpiryText
        '
        Me.ExpiryText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExpiryText.Location = New System.Drawing.Point(63, 82)
        Me.ExpiryText.Name = "ExpiryText"
        Me.ExpiryText.Size = New System.Drawing.Size(96, 20)
        Me.ExpiryText.TabIndex = 7
        '
        'ExpiryLabel
        '
        Me.ExpiryLabel.AutoSize = True
        Me.ExpiryLabel.Location = New System.Drawing.Point(0, 85)
        Me.ExpiryLabel.Name = "ExpiryLabel"
        Me.ExpiryLabel.Size = New System.Drawing.Size(35, 13)
        Me.ExpiryLabel.TabIndex = 6
        Me.ExpiryLabel.Text = "Expiry"
        '
        'ExchangeCombo
        '
        Me.ExchangeCombo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExchangeCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.ExchangeCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ExchangeCombo.FormattingEnabled = True
        Me.ExchangeCombo.Location = New System.Drawing.Point(63, 109)
        Me.ExchangeCombo.Name = "ExchangeCombo"
        Me.ExchangeCombo.Size = New System.Drawing.Size(96, 21)
        Me.ExchangeCombo.TabIndex = 9
        '
        'ExchangeLabel
        '
        Me.ExchangeLabel.AutoSize = True
        Me.ExchangeLabel.Location = New System.Drawing.Point(0, 112)
        Me.ExchangeLabel.Name = "ExchangeLabel"
        Me.ExchangeLabel.Size = New System.Drawing.Size(55, 13)
        Me.ExchangeLabel.TabIndex = 8
        Me.ExchangeLabel.Text = "Exchange"
        '
        'CurrencyCombo
        '
        Me.CurrencyCombo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CurrencyCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CurrencyCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CurrencyCombo.FormattingEnabled = True
        Me.CurrencyCombo.Location = New System.Drawing.Point(63, 136)
        Me.CurrencyCombo.Name = "CurrencyCombo"
        Me.CurrencyCombo.Size = New System.Drawing.Size(96, 21)
        Me.CurrencyCombo.TabIndex = 11
        '
        'CurrencyLabel
        '
        Me.CurrencyLabel.AutoSize = True
        Me.CurrencyLabel.Location = New System.Drawing.Point(0, 139)
        Me.CurrencyLabel.Name = "CurrencyLabel"
        Me.CurrencyLabel.Size = New System.Drawing.Size(49, 13)
        Me.CurrencyLabel.TabIndex = 10
        Me.CurrencyLabel.Text = "Currency"
        '
        'StrikePriceText
        '
        Me.StrikePriceText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StrikePriceText.Location = New System.Drawing.Point(63, 189)
        Me.StrikePriceText.Name = "StrikePriceText"
        Me.StrikePriceText.Size = New System.Drawing.Size(96, 20)
        Me.StrikePriceText.TabIndex = 13
        '
        'StrikePriceLabel
        '
        Me.StrikePriceLabel.AutoSize = True
        Me.StrikePriceLabel.Location = New System.Drawing.Point(0, 192)
        Me.StrikePriceLabel.Name = "StrikePriceLabel"
        Me.StrikePriceLabel.Size = New System.Drawing.Size(60, 13)
        Me.StrikePriceLabel.TabIndex = 12
        Me.StrikePriceLabel.Text = "Strike price"
        '
        'RightCombo
        '
        Me.RightCombo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RightCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.RightCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RightCombo.FormattingEnabled = True
        Me.RightCombo.Location = New System.Drawing.Point(63, 215)
        Me.RightCombo.Name = "RightCombo"
        Me.RightCombo.Size = New System.Drawing.Size(96, 21)
        Me.RightCombo.TabIndex = 15
        '
        'RightLabel
        '
        Me.RightLabel.AutoSize = True
        Me.RightLabel.Location = New System.Drawing.Point(0, 218)
        Me.RightLabel.Name = "RightLabel"
        Me.RightLabel.Size = New System.Drawing.Size(32, 13)
        Me.RightLabel.TabIndex = 14
        Me.RightLabel.Text = "Right"
        '
        'AdvancedButton
        '
        Me.AdvancedButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AdvancedButton.Location = New System.Drawing.Point(79, 242)
        Me.AdvancedButton.Name = "AdvancedButton"
        Me.AdvancedButton.Size = New System.Drawing.Size(80, 20)
        Me.AdvancedButton.TabIndex = 18
        Me.AdvancedButton.Text = "Advanced <<"
        Me.AdvancedButton.UseVisualStyleBackColor = True
        '
        'MultiplierText
        '
        Me.MultiplierText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MultiplierText.Location = New System.Drawing.Point(63, 163)
        Me.MultiplierText.Name = "MultiplierText"
        Me.MultiplierText.Size = New System.Drawing.Size(96, 20)
        Me.MultiplierText.TabIndex = 20
        '
        'MultiplierLabel
        '
        Me.MultiplierLabel.AutoSize = True
        Me.MultiplierLabel.Location = New System.Drawing.Point(0, 166)
        Me.MultiplierLabel.Name = "MultiplierLabel"
        Me.MultiplierLabel.Size = New System.Drawing.Size(48, 13)
        Me.MultiplierLabel.TabIndex = 19
        Me.MultiplierLabel.Text = "Multiplier"
        '
        'ContractSpecBuilder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MultiplierText)
        Me.Controls.Add(Me.MultiplierLabel)
        Me.Controls.Add(Me.AdvancedButton)
        Me.Controls.Add(Me.RightCombo)
        Me.Controls.Add(Me.RightLabel)
        Me.Controls.Add(Me.StrikePriceText)
        Me.Controls.Add(Me.StrikePriceLabel)
        Me.Controls.Add(Me.CurrencyCombo)
        Me.Controls.Add(Me.CurrencyLabel)
        Me.Controls.Add(Me.ExchangeCombo)
        Me.Controls.Add(Me.ExchangeLabel)
        Me.Controls.Add(Me.ExpiryText)
        Me.Controls.Add(Me.ExpiryLabel)
        Me.Controls.Add(Me.TypeCombo)
        Me.Controls.Add(Me.TypeLabel)
        Me.Controls.Add(Me.SymbolText)
        Me.Controls.Add(Me.SymbolLabel)
        Me.Controls.Add(Me.ShortNameText)
        Me.Controls.Add(Me.ShortNameLabel)
        Me.Name = "ContractSpecBuilder"
        Me.Size = New System.Drawing.Size(159, 262)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ShortNameLabel As System.Windows.Forms.Label
    Friend WithEvents ShortNameText As System.Windows.Forms.TextBox
    Friend WithEvents SymbolText As System.Windows.Forms.TextBox
    Friend WithEvents SymbolLabel As System.Windows.Forms.Label
    Friend WithEvents TypeLabel As System.Windows.Forms.Label
    Friend WithEvents TypeCombo As System.Windows.Forms.ComboBox
    Friend WithEvents ExpiryText As System.Windows.Forms.TextBox
    Friend WithEvents ExpiryLabel As System.Windows.Forms.Label
    Friend WithEvents ExchangeCombo As System.Windows.Forms.ComboBox
    Friend WithEvents ExchangeLabel As System.Windows.Forms.Label
    Friend WithEvents CurrencyCombo As System.Windows.Forms.ComboBox
    Friend WithEvents CurrencyLabel As System.Windows.Forms.Label
    Friend WithEvents StrikePriceText As System.Windows.Forms.TextBox
    Friend WithEvents StrikePriceLabel As System.Windows.Forms.Label
    Friend WithEvents RightCombo As System.Windows.Forms.ComboBox
    Friend WithEvents RightLabel As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents AdvancedButton As System.Windows.Forms.Button
    Friend WithEvents MultiplierText As System.Windows.Forms.TextBox
    Friend WithEvents MultiplierLabel As System.Windows.Forms.Label
End Class
