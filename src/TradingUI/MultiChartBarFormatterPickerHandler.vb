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
Imports TradeWright.Trading.Utils.Charts.BarFormatters
Imports TWUtilities40

Public Class MultiChartBarFormatterPickerHandler

    Private mBarFormatterLibManager As BarFormatterLibManager
    Private mBarFormatters() As BarFormatterFactoryListEntry

    Private WithEvents mMarketChart As MarketChart
    Private WithEvents mMultiChart As MultiChart
    Private WithEvents mCurrentChartManager As ChartManager
    Private WithEvents mBarFormatterPicker As BarFormatterPicker

    Public Sub New(barFormatterPicker As BarFormatterPicker)
        mBarFormatterPicker = barFormatterPicker
    End Sub

    Private Sub mCurrentChartManager_BaseStudyConfigurationChanged(sender As Object, studyConfig As Utils.Charts.StudyConfiguration) Handles mCurrentChartManager.BaseStudyConfigurationChanged
        If mMultiChart IsNot Nothing Then
            setBarFormatterPicker(mMultiChart.CurrentChart)
        Else
            setBarFormatterPicker(mMarketChart)
        End If
    End Sub

    Private Sub mMarketChart_StateChange(ev As StateChangeEventData) Handles mMarketChart.StateChange
        Try
            setBarFormatterPicker(mMarketChart)
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mMarketChart_StateChange), NameOf(MultiChartBarFormatterPickerHandler))
        End Try
    End Sub

    Private Sub mMultiChart_Change(sender As Object, e As MultichartChangeEventArgs) Handles mMultiChart.Change
        setBarFormatterPicker(mMultiChart.CurrentChart)
    End Sub

    Private Sub mMultiChart_ChartStateChanged(sender As Object, e As ChartStateChangedEventArgs) Handles mMultiChart.ChartStateChanged
        setBarFormatterPicker(mMultiChart.CurrentChart)
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
        setBarFormatterPicker(mMarketChart)
    End Sub

    Public Sub Initialise(multiChart As MultiChart)
        mMarketChart = Nothing
        mMultiChart = multiChart
        setBarFormatterPicker(mMultiChart.CurrentChart)
    End Sub

    Private Sub setBarFormatterPicker(chart As MarketChart)
        If chart Is Nothing OrElse chart.State <> MarketChart.ChartStates.ChartStateLoaded Then
            mCurrentChartManager = Nothing
            mBarFormatterPicker.SelectItem("", "")
            mBarFormatterPicker.Enabled = False
        Else
            mCurrentChartManager = chart.ChartManager
            Dim lBarsValueConfig = mCurrentChartManager.BaseStudyConfiguration.StudyValueConfigurations.Item("Bar")
            mBarFormatterPicker.SelectItem(lBarsValueConfig.BarFormatterFactoryName, lBarsValueConfig.BarFormatterLibraryName)
            mBarFormatterPicker.Enabled = True
        End If
    End Sub

    Private Sub mBarFormatterPicker_SelectedEntryChanged(sender As Object, e As System.EventArgs) Handles mBarFormatterPicker.SelectedEntryChanged
        If mMultiChart.CurrentChart IsNot Nothing Then
            If mMultiChart.CurrentChart.State <> MarketChart.ChartStates.ChartStateLoaded Then Exit Sub
        ElseIf mMarketChart IsNot Nothing Then
            If mMarketChart.State <> MarketChart.ChartStates.ChartStateLoaded Then Exit Sub
        Else
            Exit Sub
        End If

        Dim lBaseStudyConfig = mCurrentChartManager.BaseStudyConfiguration
        Dim lBarsValueConfig = lBaseStudyConfig.StudyValueConfigurations.Item("Bar")

        Dim lEntry = mBarFormatterPicker.SelectedEntry
        If IsNothing(lEntry) Then
            lBarsValueConfig.BarFormatterFactoryName = ""
            lBarsValueConfig.BarFormatterLibraryName = ""
        Else
            lBarsValueConfig.BarFormatterFactoryName = lEntry.FactoryName
            lBarsValueConfig.BarFormatterLibraryName = lEntry.LibraryName
        End If

        mCurrentChartManager.SetBaseStudyConfiguration(lBaseStudyConfig)
    End Sub

End Class
