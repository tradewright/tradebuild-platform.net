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
Imports TradeWright.Trading.Utils.Charts.BarFormatters
Imports TWUtilities40

Public Class DunniganFactory
    Implements IBarFormatterFactory

    Public Sub New()
    End Sub

    Public WriteOnly Property ConfigurationSection() As ConfigurationSection Implements IBarFormatterFactory.ConfigurationSection
        Set

        End Set
    End Property

    Private Function CreateBarFormatter(barsFuture As _IFuture) As IBarFormatter Implements IBarFormatterFactory.CreateBarFormatter
        Return New BarFormatter(barsFuture)
    End Function

    Public Sub LoadFromConfig(config As ConfigurationSection) Implements IBarFormatterFactory.LoadFromConfig

    End Sub

    Public Class BarFormatter
        Implements IBarFormatter

        Private mBars As Bars
        Private WithEvents mFutureWaiter As FutureWaiter = New FutureWaiter

        Sub New(barsFuture As _IFuture)
            mFutureWaiter.Add(barsFuture)
        End Sub

        Private Sub mFutureWaiter_WaitCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitCompleted
            Try
                If ev.Future.IsAvailable Then mBars = DirectCast(ev.Future.Value, Bars)
            Catch e As Exception
                NotifyUnhandledError(e, NameOf(mFutureWaiter_WaitCompleted), NameOf(DunniganFactory))
            End Try
        End Sub

        Public Sub FormatBar(sourceBar As BarUtils27.Bar, chartBar As ChartSkil27.Bar) Implements IBarFormatter.FormatBar
            Try
                If sourceBar.BarNumber <= 2 Or mBars.Count < 2 Then Exit Sub

                If sourceBar.HighValue > mBars.Bar(sourceBar.BarNumber - 1).HighValue And
                    sourceBar.LowValue >= mBars.Bar(sourceBar.BarNumber - 1).LowValue _
                Then
                    chartBar.Color = &H1D9311
                    chartBar.UpColor = &H1D9311
                    chartBar.DownColor = &H1D9311
                ElseIf sourceBar.HighValue <= mBars.Bar(sourceBar.BarNumber - 1).HighValue And
                    sourceBar.LowValue < mBars.Bar(sourceBar.BarNumber - 1).LowValue _
                Then
                    chartBar.Color = &H43FC2
                    chartBar.UpColor = &H43FC2
                    chartBar.DownColor = &H43FC2
                ElseIf sourceBar.HighValue <= mBars.Bar(sourceBar.BarNumber - 1).HighValue And
                    sourceBar.LowValue >= mBars.Bar(sourceBar.BarNumber - 1).LowValue _
                Then
                    chartBar.Color = &HFF00FF    ' magenta
                    chartBar.UpColor = &HFF00FF
                    chartBar.DownColor = &HFF00FF
                ElseIf sourceBar.HighValue >= mBars.Bar(sourceBar.BarNumber - 1).HighValue And
                    sourceBar.LowValue <= mBars.Bar(sourceBar.BarNumber - 1).LowValue _
                Then
                    chartBar.Color = &HFF0000    ' blue
                    chartBar.UpColor = &HFF0000
                    chartBar.DownColor = &HFF0000
                End If
            Catch ex As Exception
                BarFormatters.ErrorLogger.Log(LogLevels.LogLevelSevere, ex.ToString)
            End Try
        End Sub

    End Class


End Class
