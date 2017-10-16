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

Imports System.Drawing
Imports BarUtils27
Imports TWUtilities40

Public Class PBFactory
    Implements IBarFormatterFactory

    Private Shared mBFF As PBFactory

    Public Shared ReadOnly Property Instance() As PBFactory
        Get
            If mBFF Is Nothing Then mBFF = New PBFactory
            Return mBFF
        End Get
    End Property

    Public Sub New()
    End Sub

    Public WriteOnly Property ConfigurationSection() As ConfigurationSection Implements IBarFormatterFactory.ConfigurationSection
        Set

        End Set
    End Property

    Public Function CreateBarFormatter(barsFuture As _IFuture) As IBarFormatter Implements IBarFormatterFactory.CreateBarFormatter
        Return New BarFormatter(barsFuture)
    End Function

    Public Sub LoadFromConfig(config As ConfigurationSection) Implements IBarFormatterFactory.LoadFromConfig

    End Sub

    Public Class BarFormatter
        Implements IBarFormatter

        Private mBars As Bars
        Private WithEvents mFutureWaiter As New FutureWaiter

        Sub New(barsFuture As _IFuture)
            mFutureWaiter.Add(barsFuture)
        End Sub

        Private Sub mFutureWaiter_WaitCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitCompleted
            Try
                If ev.Future.IsAvailable Then mBars = DirectCast(ev.Future.Value, Bars)
            Catch e As Exception
                NotifyUnhandledError(e, NameOf(mFutureWaiter_WaitCompleted), NameOf(PBFactory))
            End Try
        End Sub

        Public Sub FormatBar(sourceBar As BarUtils27.Bar, chartBar As ChartSkil27.Bar) Implements IBarFormatter.FormatBar
            Try
                chartBar.UpColor = System.Drawing.ColorTranslator.ToOle(Color.White)
                chartBar.DownColor = System.Drawing.ColorTranslator.ToOle(Color.Gray)
                chartBar.SolidUpBody = True
                chartBar.Color = System.Drawing.ColorTranslator.ToOle(Color.Gray)
                chartBar.OutlineThickness = 1
                If sourceBar.BarNumber <= 2 Or mBars.Count < 2 Then Exit Sub
                If sourceBar.HighValue <= mBars.Bar(sourceBar.BarNumber - 1).HighValue And
                    sourceBar.CloseValue > mBars.Bar(sourceBar.BarNumber - 1).LowValue And
                    sourceBar.LowValue < mBars.Bar(sourceBar.BarNumber - 1).LowValue Then
                    chartBar.Color = System.Drawing.ColorTranslator.ToOle(Color.Blue)
                    chartBar.DownColor = System.Drawing.ColorTranslator.ToOle(Color.Blue)
                End If
            Catch ex As Exception
                BarFormatters.ErrorLogger.Log(LogLevels.LogLevelSevere, ex.ToString)
            End Try
        End Sub
    End Class


End Class
