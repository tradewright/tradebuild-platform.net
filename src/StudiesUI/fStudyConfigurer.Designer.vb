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
Partial Class fStudyConfigurer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.AddButton = New System.Windows.Forms.Button()
        Me.CanclButton = New System.Windows.Forms.Button()
        Me.SetDefaultButton = New System.Windows.Forms.Button()
        Me.StudyConfigurer1 = New TradeWright.Trading.UI.Studies.StudyConfigurer()
        Me.SuspendLayout()
        '
        'AddButton
        '
        Me.AddButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddButton.Location = New System.Drawing.Point(875, 14)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.Size = New System.Drawing.Size(75, 34)
        Me.AddButton.TabIndex = 0
        Me.AddButton.Text = "Add to chart"
        Me.AddButton.UseVisualStyleBackColor = True
        '
        'CanclButton
        '
        Me.CanclButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CanclButton.Location = New System.Drawing.Point(878, 61)
        Me.CanclButton.Name = "CanclButton"
        Me.CanclButton.Size = New System.Drawing.Size(71, 33)
        Me.CanclButton.TabIndex = 1
        Me.CanclButton.Text = "Cancel"
        Me.CanclButton.UseVisualStyleBackColor = True
        '
        'SetDefaultButton
        '
        Me.SetDefaultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SetDefaultButton.Location = New System.Drawing.Point(878, 200)
        Me.SetDefaultButton.Name = "SetDefaultButton"
        Me.SetDefaultButton.Size = New System.Drawing.Size(71, 34)
        Me.SetDefaultButton.TabIndex = 2
        Me.SetDefaultButton.Text = "Set as default"
        Me.SetDefaultButton.UseVisualStyleBackColor = True
        '
        'StudyConfigurer1
        '
        Me.StudyConfigurer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StudyConfigurer1.Location = New System.Drawing.Point(5, 5)
        Me.StudyConfigurer1.Name = "StudyConfigurer1"
        Me.StudyConfigurer1.Size = New System.Drawing.Size(859, 396)
        Me.StudyConfigurer1.TabIndex = 3
        '
        'fStudyConfigurer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 406)
        Me.Controls.Add(Me.StudyConfigurer1)
        Me.Controls.Add(Me.SetDefaultButton)
        Me.Controls.Add(Me.CanclButton)
        Me.Controls.Add(Me.AddButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "fStudyConfigurer"
        Me.Text = "fStudyConfigurer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents CanclButton As System.Windows.Forms.Button
    Friend WithEvents SetDefaultButton As System.Windows.Forms.Button
    Friend WithEvents StudyConfigurer1 As StudyConfigurer
End Class
