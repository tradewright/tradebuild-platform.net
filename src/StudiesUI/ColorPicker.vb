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

Public Class ColorPicker

    Private Shared gSimpleColorPicker As New fSimpleColorPicker

    Friend Shared NullColor As Color = System.Drawing.SystemColors.AppWorkspace

    Private mAllowNull As Boolean

    Private Sub ColorLabel_Click(sender As Object, e As System.EventArgs) Handles ColorLabel.Click
        gSimpleColorPicker.initialColor = Me.Color
        If AllowNull Then gSimpleColorPicker.NoColorButton.Enabled = True
        gSimpleColorPicker.Visible = True
        gSimpleColorPicker.DesktopLocation = Cursor.Position    ' getLocation()
        gSimpleColorPicker.Visible = False
        gSimpleColorPicker.ShowDialog()
        Me.Color = gSimpleColorPicker.selectedColor
        MyBase.OnClick(e)
    End Sub

    Public Property AllowNull() As Boolean
        Get
            Return mAllowNull
        End Get
        Set
            mAllowNull = Value
        End Set
    End Property

    Public Property Color() As Color
        Get
            Return ColorLabel.BackColor
        End Get
        Set
            ColorLabel.BackColor = Value
        End Set
    End Property

    Public Property OleColor() As Integer
        Get
            If IsColorNull Then
                Return -1
            Else
                Return ColorTranslator.ToOle(ColorLabel.BackColor)
            End If
        End Get
        Set
            If Value = -1 Then
                SetNullColor()
            Else
                ColorLabel.BackColor = ColorTranslator.FromOle(Value)
            End If
        End Set
    End Property

    Public ReadOnly Property IsColorNull() As Boolean
        Get
            Return (Color = NullColor)
        End Get
    End Property

    Public Sub SetNullColor()
        Color = NullColor
    End Sub

    Private Function getLocation() As System.Drawing.Point
        Dim pt = Me.Location

        Dim parent = Me.Parent

        gLogger.Log(TWUtilities40.LogLevels.LogLevelDetail, "Me " & Me.Name & " location=" & Me.Location.ToString)
        Do
            gLogger.Log(TWUtilities40.LogLevels.LogLevelDetail, "Parent " & parent.Name & " location=" & parent.Location.ToString)
            pt = Point.op_Addition(pt, Point.op_Explicit(parent.Location))
            parent = parent.Parent
        Loop Until parent Is Me.TopLevelControl

        gLogger.Log(TWUtilities40.LogLevels.LogLevelDetail, "getLocation=" & Point.op_Addition(pt, New Size(CInt(Me.Width / 2), CInt(Me.Height / 2))).ToString)
        Return Point.op_Addition(Point.op_Addition(pt, New Size(CInt(Me.Width / 2), CInt(Me.Height / 2))), Point.op_Explicit(Me.FindForm.DesktopLocation))
    End Function

End Class
