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

Imports TradeWright.Trading.Utils.Charts
Imports TWUtilities40

Public Class MultiChartStylePickerHandler

    Private WithEvents mMarketChart As MarketChart
    Private WithEvents mMultiChart As MultiChart
    Private WithEvents mStylePicker As ChartStylePicker

    Public Sub New(stylePicker As ChartStylePicker)
        mStylePicker = stylePicker
    End Sub

    Private Sub mMarketChart_StateChange(ev As StateChangeEventData) Handles mMarketChart.StateChange
        Try
            setStylePicker(mMarketChart)
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mMarketChart_StateChange), NameOf(MultiChartStylePickerHandler))
        End Try
    End Sub

    Private Sub mMultiChart_Change(sender As Object, e As MultichartChangeEventArgs) Handles mMultiChart.Change
        setStylePicker(mMultiChart.CurrentChart)
    End Sub

    Private Sub mMultiChart_ChartStateChanged(sender As Object, e As ChartStateChangedEventArgs) Handles mMultiChart.ChartStateChanged
        setStylePicker(mMultiChart.CurrentChart)
    End Sub

    Public ReadOnly Property MarketChart() As MarketChart
        Get
            Return mMarketChart
        End Get
    End Property

    Public ReadOnly Property MultiChart() As MultiChart
        Get
            Return mMultiChart
        End Get
    End Property

    Public Sub Initialise(marketChart As MarketChart)
        mMultiChart = Nothing
        mMarketChart = marketChart
        setStylePicker(mMarketChart)
    End Sub

    Public Sub Initialise(multiChart As MultiChart)
        mMarketChart = Nothing
        mMultiChart = multiChart
        setStylePicker(mMultiChart.CurrentChart)
    End Sub

    Private Sub setStylePicker(chart As MarketChart)
        If chart Is Nothing OrElse chart.State <> MarketChart.ChartStates.ChartStateLoaded Then
            mStylePicker.SelectItem(Nothing)
            mStylePicker.Enabled = False
        Else
            mStylePicker.SelectItem(chart.Style)
            mStylePicker.Enabled = True
        End If
    End Sub

    Private Sub mStylePicker_SelectedEntryChanged(sender As Object, e As System.EventArgs) Handles mStylePicker.SelectedEntryChanged
        Dim chart As MarketChart
        If mMultiChart.CurrentChart IsNot Nothing Then
            If mMultiChart.CurrentChart.State <> MarketChart.ChartStates.ChartStateLoaded Then Exit Sub
            chart = mMultiChart.CurrentChart
        ElseIf mMarketChart IsNot Nothing Then
            If mMarketChart.State <> MarketChart.ChartStates.ChartStateLoaded Then Exit Sub
            chart = mMarketChart
        Else
            Exit Sub
        End If

        Dim style = mStylePicker.SelectedEntry
        If IsNothing(style) Then
            chart.Style = Nothing
        Else
            chart.Style = style
        End If
    End Sub

End Class
