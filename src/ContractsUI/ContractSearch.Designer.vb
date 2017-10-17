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
Partial Class ContractSearch
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
    '<System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MessageLabel = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.ActionButton = New System.Windows.Forms.Button()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.ContractSpecBuilder1 = New TradeWright.Trading.UI.Contracts.ContractSpecBuilder()
        Me.ContractSelector1 = New TradeWright.Trading.UI.Contracts.ContractSelector()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 287)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(260, 1)
        Me.Label1.TabIndex = 2
        '
        'MessageLabel
        '
        Me.MessageLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MessageLabel.Location = New System.Drawing.Point(0, 295)
        Me.MessageLabel.Name = "MessageLabel"
        Me.MessageLabel.Size = New System.Drawing.Size(260, 16)
        Me.MessageLabel.TabIndex = 5
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.ActionButton)
        Me.FlowLayoutPanel1.Controls.Add(Me.ClearButton)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 318)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(260, 25)
        Me.FlowLayoutPanel1.TabIndex = 6
        '
        'ActionButton
        '
        Me.ActionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ActionButton.Enabled = False
        Me.ActionButton.Location = New System.Drawing.Point(198, 3)
        Me.ActionButton.Name = "ActionButton"
        Me.ActionButton.Size = New System.Drawing.Size(59, 21)
        Me.ActionButton.TabIndex = 2
        Me.ActionButton.Text = "Button1"
        Me.ActionButton.UseVisualStyleBackColor = True
        '
        'ClearButton
        '
        Me.ClearButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClearButton.Location = New System.Drawing.Point(133, 3)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(59, 21)
        Me.ClearButton.TabIndex = 3
        Me.ClearButton.Text = "Clear"
        Me.ClearButton.UseVisualStyleBackColor = True
        Me.ClearButton.Visible = False
        '
        'ContractSpecBuilder1
        '
        Me.ContractSpecBuilder1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContractSpecBuilder1.ContractSpecifier = Nothing
        Me.ContractSpecBuilder1.FieldsBackColor = System.Drawing.SystemColors.Window
        Me.ContractSpecBuilder1.FieldsForeColor = System.Drawing.SystemColors.WindowText
        Me.ContractSpecBuilder1.Location = New System.Drawing.Point(0, 0)
        Me.ContractSpecBuilder1.ModeAdvanced = False
        Me.ContractSpecBuilder1.Name = "ContractSpecBuilder1"
        Me.ContractSpecBuilder1.Size = New System.Drawing.Size(257, 275)
        Me.ContractSpecBuilder1.TabIndex = 0
        '
        'ContractSelector1
        '
        Me.ContractSelector1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContractSelector1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ContractSelector1.IncludeHistoricalContracts = False
        Me.ContractSelector1.Location = New System.Drawing.Point(0, 0)
        Me.ContractSelector1.MultiSelect = True
        Me.ContractSelector1.Name = "ContractSelector1"
        Me.ContractSelector1.RowBackColorEven = System.Drawing.SystemColors.Window
        Me.ContractSelector1.RowBackColorOdd = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ContractSelector1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ContractSelector1.Size = New System.Drawing.Size(257, 275)
        Me.ContractSelector1.TabIndex = 3
        '
        'ContractSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.ContractSpecBuilder1)
        Me.Controls.Add(Me.ContractSelector1)
        Me.Controls.Add(Me.MessageLabel)
        Me.Controls.Add(Me.Label1)
        Me.MinimumSize = New System.Drawing.Size(130, 303)
        Me.Name = "ContractSearch"
        Me.Size = New System.Drawing.Size(260, 343)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MessageLabel As System.Windows.Forms.Label
    Friend WithEvents ContractSelector1 As TradeWright.Trading.UI.Contracts.ContractSelector
    Friend WithEvents ContractSpecBuilder1 As TradeWright.Trading.UI.Contracts.ContractSpecBuilder
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents ActionButton As System.Windows.Forms.Button
    Friend WithEvents ClearButton As System.Windows.Forms.Button

End Class
