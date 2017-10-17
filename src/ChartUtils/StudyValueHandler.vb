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

Imports BarUtils27
Imports ChartSkil27
Imports StudyUtils27
Imports TWUtilities40

Imports System.Runtime.InteropServices.Marshal
Imports TradeWright.Trading.Utils.Charts.BarFormatters

Public NotInheritable Class StudyValueHandler
    Implements IStudyValueListener

#Region "Interfaces"


#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const ModuleName As String = "StudyValueHandler"

#End Region

#Region "Enums"

    Public Structure ConditionalAction
        Dim [Operator] As ConditionalOperators
        Dim [Not] As Boolean
        Dim Value1 As Object
        Dim IsSpecial1 As Boolean
        Dim IsSpecial2 As Boolean
        Dim Value2 As Object
        Dim Action As StudyRenderingActions
        Dim ActionValue As Object
        Dim StopIfTrue As Boolean ' if set, then if this condition is true,
        ' then no further conditional actions are performed
    End Structure

#End Region

#Region "Types"

    Enum ConditionalOperators
        OpTrue
        OpLessThan
        OpEqual
        OpGreaterThan
        OpBetween
        OpLessThanPrevious
        OpEqualPrevious
        OpGreaterThanPrevious
        OpStartsWith
        OpContains
        OpEndsWith
        OpAnd
        OpOr
        OpXor
    End Enum

    Enum SpecialValues
        SVCurrentSessionStartTime
        SVCurrentSessionEndTime
        SVHighPrice
        SVLowPrice
        SVPreviousClosePrice
    End Enum

    Enum StudyRenderingActions
        SRActSetPeriodBackgroundColor
        SRActSetValueColor
    End Enum

#End Region

#Region "Member variables"

    Private mChartManager As ChartManager

    Private mStudy As _IStudy

    Private mValueMode As StudyValueModes
    Private mValueType As StudyValueTypes

    Private mCurrentDataPointValue As SValue
    Private mPrevDataPointValue As SValue
    Private mCurrentStringValue As SValue
    Private mCurrentStudyText As StudyText
    Private mCurrentBar As BarUtils27.Bar
    Private mCurrentStudyLine As StudyLine

    Private mDataSeries As DataPointSeries
    Private mBarSeries As BarSeries
    Private mLineSeries As LineSeries
    Private mTextSeries As TextSeries

    Private mValueSeries As _IGraphicObjectSeries

    Private mTextOffset As ChartSkil27.Size

    Private mConditionalActions() As ConditionalAction
    Private mConditionalActionsIndex As Integer
    Private mRegion As ChartRegion
    Private mPeriods As Periods

    Private mUpdatePerTick As Boolean

    Private mBarFormatter As IBarFormatter

    Private mValueName As String

#End Region

#Region "Constructors"

    Friend Sub New()
        MyBase.New()
        ReDim mConditionalActions(10)
        mConditionalActionsIndex = -1
    End Sub

#End Region

#Region "StudyValueListener Interface Members"

    Private Sub StudyValueListener_notify(ByRef ev As StudyValueEventData) Implements IStudyValueListener.Notify
        Try
            If IsNothing(ev.sVal.Value) Then Exit Sub

            Select Case mValueMode
                Case StudyValueModes.ValueModeNone
                    Select Case mValueType
                        Case StudyValueTypes.ValueTypeInteger
                            processDataPoint(ev)
                        Case StudyValueTypes.ValueTypeReal
                            processDataPoint(ev)
                        Case StudyValueTypes.ValueTypeString
                            processString(ev)
                        Case StudyValueTypes.ValueTypeDate

                        Case StudyValueTypes.ValueTypeBoolean

                    End Select
                Case StudyValueModes.ValueModeLine
                    processLine(ev)
                Case StudyValueModes.ValueModeBar
                    processBar(ev)
                Case StudyValueModes.ValueModeText
                    processText(ev)
            End Select
        Catch ex As Exception
            ex.Source = ex.StackTrace
            Throw
        End Try
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public ReadOnly Property Region() As ChartRegion
        Get
            Return mRegion
        End Get
    End Property

    Public WriteOnly Property UpdatePerTick() As Boolean
        Set
            mUpdatePerTick = Value
        End Set
    End Property

    Public ReadOnly Property ValueName() As String
        Get
            Return mValueName
        End Get
    End Property

    Public ReadOnly Property ValueSeries() As _IGraphicObjectSeries
        Get
            Return mValueSeries
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub AddConditionalAction(ByRef pConditionalAction As ConditionalAction)
        If mConditionalActionsIndex >= UBound(mConditionalActions) Then
            ReDim Preserve mConditionalActions(UBound(mConditionalActions) + 10)
        End If
        mConditionalActionsIndex = mConditionalActionsIndex + 1

        mConditionalActions(mConditionalActionsIndex) = pConditionalAction
    End Sub

    Friend Sub Finish()
        If mDataSeries IsNot Nothing Then
            'ReleaseComObject(mDataSeries)
            mDataSeries = Nothing
        End If
        If mBarSeries IsNot Nothing Then
            'ReleaseComObject(mBarSeries)
            mBarSeries = Nothing
        End If
        If mLineSeries IsNot Nothing Then
            'ReleaseComObject(mLineSeries)
            mLineSeries = Nothing
        End If
        If mTextSeries IsNot Nothing Then
            'ReleaseComObject(mTextSeries)
            mTextSeries = Nothing
        End If
        If mValueSeries IsNot Nothing Then
            'ReleaseComObject(mValueSeries)
            mValueSeries = Nothing
        End If
        If mRegion IsNot Nothing Then
            'ReleaseComObject(mRegion)
            mRegion = Nothing
        End If
    End Sub

    Friend Sub Initialise(chartMgr As ChartManager, region As ChartRegion, study As _IStudy, studyValueConfig As StudyValueConfiguration, studyValueDef As StudyValueDefinition, updatePerTick As Boolean)
        mChartManager = chartMgr
        mRegion = region
        mPeriods = mChartManager.Chart.Periods
        mStudy = study

        mUpdatePerTick = updatePerTick
        mValueName = studyValueConfig.ValueName

        If Not IsNothing(studyValueDef.MaximumValue) Or Not IsNothing(studyValueDef.MinimumValue) Then
            region.Autoscaling = False
            region.SetVerticalScale(CSng(studyValueDef.MinimumValue), CSng(studyValueDef.MaximumValue))
        End If

        If mStudy.GetValueTicksize(studyValueDef.Name) <> 0.0 Then
            If mStudy.GetValueTicksize(studyValueDef.Name) < mRegion.YScaleQuantum Then mRegion.YScaleQuantum = mStudy.GetValueTicksize(studyValueDef.Name)
        End If

        mValueMode = studyValueDef.ValueMode
        mValueType = studyValueDef.ValueType

        Select Case mValueMode
            Case StudyValueModes.ValueModeNone
                Select Case mValueType
                    Case StudyValueTypes.ValueTypeInteger
                        setupDataSeries(studyValueConfig, studyValueDef)
                    Case StudyValueTypes.ValueTypeReal
                        setupDataSeries(studyValueConfig, studyValueDef)
                    Case StudyValueTypes.ValueTypeString
                        setupTextSeries(studyValueConfig, studyValueDef)
                    Case StudyValueTypes.ValueTypeDate

                    Case StudyValueTypes.ValueTypeBoolean
                End Select
            Case StudyValueModes.ValueModeLine
                setupLineSeries(studyValueConfig, studyValueDef)
            Case StudyValueModes.ValueModeBar
                setupBarSeries(studyValueConfig, studyValueDef)
            Case StudyValueModes.ValueModeText
                setupTextSeries(studyValueConfig, studyValueDef)
        End Select
    End Sub

    Friend Sub Update()
        If mUpdatePerTick Then Exit Sub

        If Not mCurrentBar Is Nothing Then updateCurrentBar(mCurrentBar)
        If Not IsNothing(mCurrentDataPointValue.Value) Then updateCurrentDatapoint(mCurrentDataPointValue)
        If Not mCurrentStudyLine Is Nothing Then updateCurrentLine(mCurrentStudyLine)
        If Not IsNothing(mCurrentStringValue.Value) Then updateCurrentString(mCurrentStringValue)
        If Not mCurrentStudyText Is Nothing Then updateCurrentText(mCurrentStudyText)
    End Sub

#End Region

#Region "Helper Functions"

    Private Function addBarToChart(timestamp As Date) As ChartSkil27.Bar
        Return mBarSeries.Add(timestamp)
    End Function

    Private Function addDataPointToChart(timestamp As Date) As ChartSkil27.DataPoint
        Return mDataSeries.Add(timestamp)
    End Function

    Private Function addLineToChart() As ChartSkil27.Line
        Return mLineSeries.Add
    End Function

    Private Function addTextToChart() As ChartSkil27.Text
        Return mTextSeries.Add
    End Function

    Private Sub processBar(ByRef ev As StudyValueEventData)
        'If ev.sVal.Value Is Nothing Then Exit Sub

        Static sCurrentBarNumber As Long

        Dim lBar = DirectCast(ev.sVal.Value, BarUtils27.Bar)

        If mUpdatePerTick Then
            Static sCurrentChartBar As ChartSkil27.Bar
            If sCurrentChartBar Is Nothing Or ev.sVal.BarNumber <> sCurrentBarNumber Then
                sCurrentChartBar = addBarToChart(lBar.Timestamp)
                If Not lBar.Blank Then
                    ' we may be processing a historical bar here, so make sure open, high
                    ' low and close are all set

                    sCurrentChartBar.Tick(lBar.OpenValue)
                    sCurrentChartBar.Tick(lBar.HighValue)
                    sCurrentChartBar.Tick(lBar.LowValue)
                    sCurrentChartBar.Tick(lBar.CloseValue)
                End If
            Else
                If Not lBar.Blank Then sCurrentChartBar.Tick(lBar.CloseValue)
            End If

            If lBar.Blank Then
                If Not mBarFormatter Is Nothing Then ChartUtils.Logger.Log("Bar number " & ev.sVal.BarNumber & " is blank: no bar formatting performed", NameOf(processBar), NameOf(StudyValueHandler))
            Else
                If mConditionalActionsIndex >= 0 Then processConditionalActions(lBar.Timestamp, sCurrentChartBar)
                If Not mBarFormatter Is Nothing Then mBarFormatter.FormatBar(lBar, sCurrentChartBar)
            End If
        Else
            If ev.sVal.BarNumber <> sCurrentBarNumber Then
                updateCurrentBar(mCurrentBar)
                If Not mPeriods.Contains(lBar.Timestamp) Then mPeriods.Add(lBar.Timestamp)
            End If
            mCurrentBar = lBar
        End If

        sCurrentBarNumber = ev.sVal.BarNumber
    End Sub

    Private Function processConditionalAction(ByRef conditionalAction As ConditionalAction, timestamp As Date, target As Object) As Boolean
        Dim value1 As Object
        Dim value2 As Object

        If conditionalAction.IsSpecial1 Then
            value1 = mChartManager.GetSpecialValue(CType(conditionalAction.Value1, SpecialValues))
        Else
            value1 = conditionalAction.Value1
        End If

        If conditionalAction.IsSpecial2 Then
            value2 = mChartManager.GetSpecialValue(CType(conditionalAction.Value2, SpecialValues))
        Else
            value2 = conditionalAction.Value2
        End If

        Dim conditionSatisfied As Boolean
        Select Case conditionalAction.Operator
            Case ConditionalOperators.OpAnd
                conditionSatisfied = (CBool(mCurrentDataPointValue.Value) And CBool(value1))
            Case ConditionalOperators.OpBetween
                conditionSatisfied = (CDbl(mCurrentDataPointValue.Value) >= CDbl(value1) And CDbl(mCurrentDataPointValue.Value) <= CDbl(value2))
            Case ConditionalOperators.OpContains
                conditionSatisfied = (InStr(1, CStr(mCurrentDataPointValue.Value), CStr(value1)) <> 0)
            Case ConditionalOperators.OpEndsWith
                conditionSatisfied = (Right(CStr(mCurrentDataPointValue.Value), Len(value1)) = CStr(value1))
            Case ConditionalOperators.OpEqual
                conditionSatisfied = (CDbl(mCurrentDataPointValue.Value) = CDbl(value1))
            Case ConditionalOperators.OpEqualPrevious
                If Not mPrevDataPointValue.Value Is Nothing Then
                    conditionSatisfied = (CDbl(mCurrentDataPointValue.Value) = CDbl(mPrevDataPointValue.Value))
                End If
            Case ConditionalOperators.OpGreaterThan
                conditionSatisfied = (CDbl(mCurrentDataPointValue.Value) > CDbl(value1))
            Case ConditionalOperators.OpGreaterThanPrevious
                If Not mPrevDataPointValue.Value Is Nothing Then
                    conditionSatisfied = (CDbl(mCurrentDataPointValue.Value) > CDbl(mPrevDataPointValue.Value))
                End If
            Case ConditionalOperators.OpLessThan
                conditionSatisfied = (CDbl(mCurrentDataPointValue.Value) < CDbl(value1))
            Case ConditionalOperators.OpLessThanPrevious
                If Not mPrevDataPointValue.Value Is Nothing Then
                    conditionSatisfied = (CDbl(mCurrentDataPointValue.Value) < CDbl(mPrevDataPointValue.Value))
                End If
            Case ConditionalOperators.OpOr
                conditionSatisfied = (CBool(mCurrentDataPointValue.Value) Or CBool(value1))
            Case ConditionalOperators.OpStartsWith
                conditionSatisfied = (Left(CStr(mCurrentDataPointValue.Value), Len(value1)) = CStr(value1))
            Case ConditionalOperators.OpTrue
                conditionSatisfied = CBool(mCurrentDataPointValue.Value)
            Case ConditionalOperators.OpXor
                conditionSatisfied = (CBool(mCurrentDataPointValue.Value) Xor CBool(value1))
        End Select

        If conditionalAction.Not Then conditionSatisfied = (Not conditionSatisfied)

        If conditionSatisfied Then
            Select Case conditionalAction.Action
                Case StudyRenderingActions.SRActSetPeriodBackgroundColor
                    mRegion.SetPeriodBackgroundColor(mChartManager.GetPeriod(timestamp).PeriodNumber, CInt(conditionalAction.ActionValue))
                Case StudyRenderingActions.SRActSetValueColor
                    If TypeOf target Is DataPoint Then
                        Dim lDataPoint As DataPoint
                        lDataPoint = DirectCast(target, DataPoint)
                        lDataPoint.UpColor = CInt(conditionalAction.ActionValue)
                        lDataPoint.DownColor = CInt(conditionalAction.ActionValue)
                    End If
            End Select
        End If
        processConditionalAction = conditionSatisfied
    End Function

    Private Sub processConditionalActions(timestamp As Date, target As Object)
        For i = 0 To mConditionalActionsIndex
            If processConditionalAction(mConditionalActions(i), timestamp, target) And mConditionalActions(i).StopIfTrue Then
                Exit For
            End If
        Next
    End Sub

    Private Sub processDataPoint(ByRef ev As StudyValueEventData)
        Static sCurrentBarNumber As Long
        Static sCOMZeroDate As Date = Date.FromOADate(0#)

        If mUpdatePerTick Then
            Static sCurrentDataPoint As DataPoint
            If sCurrentDataPoint Is Nothing Or ev.sVal.BarNumber <> sCurrentBarNumber Then
                If ev.sVal.BarStartTime = sCOMZeroDate Then
                    sCurrentDataPoint = addDataPointToChart(ev.sVal.Timestamp)
                Else
                    sCurrentDataPoint = addDataPointToChart(ev.sVal.BarStartTime)
                End If
            End If
            sCurrentDataPoint.DataValue = CDbl(ev.sVal.Value)

            mPrevDataPointValue = mCurrentDataPointValue
            mCurrentDataPointValue = ev.sVal

            If mConditionalActionsIndex >= 0 Then processConditionalActions(mCurrentDataPointValue.Timestamp, sCurrentDataPoint)
        Else
            If ev.sVal.BarNumber <> sCurrentBarNumber Then updateCurrentDatapoint(mCurrentDataPointValue)
            mPrevDataPointValue = mCurrentDataPointValue
            mCurrentDataPointValue = ev.sVal
        End If

        sCurrentBarNumber = ev.sVal.BarNumber
    End Sub

    Private Sub processLine(ByRef ev As StudyValueEventData)
        If ev.sVal.Value Is Nothing Then Exit Sub

        Static sCurrentBarNumber As Long

        Dim lStudyLine As StudyLine
        lStudyLine = DirectCast(ev.sVal.Value, StudyLine)

        If mUpdatePerTick Then
            Static sCurrentChartLine As ChartSkil27.Line
            If sCurrentChartLine Is Nothing Or ev.sVal.BarNumber <> sCurrentBarNumber Then
                sCurrentChartLine = addLineToChart()
            End If

            sCurrentChartLine.SetPosition(ChartUtils.ChartSkil.NewPoint(
                                                        mChartManager.GetXFromTimestamp(lStudyLine.Point1.X),
                                                        lStudyLine.Point1.Y),
                                            ChartUtils.ChartSkil.NewPoint(
                                                        mChartManager.GetXFromTimestamp(lStudyLine.Point2.X),
                                                        lStudyLine.Point2.Y))
            If mConditionalActionsIndex >= 0 Then processConditionalActions(mChartManager.GetPeriod(lStudyLine.Point1.X).Timestamp, sCurrentChartLine)
        Else
            If ev.sVal.BarNumber <> sCurrentBarNumber Then updateCurrentLine(mCurrentStudyLine)
            mCurrentStudyLine = lStudyLine
        End If

        sCurrentBarNumber = ev.sVal.BarNumber
    End Sub

    Private Sub processString(ByRef ev As StudyValueEventData)
        Static sCurrentBarNumber As Long

        If mUpdatePerTick Then
            Static sCurrentText As ChartSkil27.Text
            If sCurrentText Is Nothing Or ev.sVal.BarNumber <> sCurrentBarNumber Then sCurrentText = addTextToChart()

            sCurrentText.Position = ChartUtils.ChartSkil.NewPoint(
                                                        mChartManager.GetXFromTimestamp(ev.sVal.Timestamp),
                                                        0, CoordinateSystems.CoordsCounterDistance, CoordinateSystems.CoordsRelative)
            sCurrentText.Text = CStr(ev.sVal.Value)
            If mConditionalActionsIndex >= 0 Then processConditionalActions(ev.sVal.Timestamp, sCurrentText)
        Else
            If ev.sVal.BarNumber <> sCurrentBarNumber Then updateCurrentString(mCurrentStringValue)
            mCurrentStringValue = ev.sVal
        End If

        sCurrentBarNumber = ev.sVal.BarNumber
    End Sub

    Private Sub processText(ByRef ev As StudyValueEventData)
        Static sCurrentBarNumber As Long

        Dim lStudyText As StudyText
        lStudyText = DirectCast(ev.sVal.Value, StudyText)

        If mUpdatePerTick Then
            Static sCurrentText As ChartSkil27.Text
            If sCurrentText Is Nothing Or ev.sVal.BarNumber <> sCurrentBarNumber Then sCurrentText = addTextToChart()

            sCurrentText.Position = ChartUtils.ChartSkil.NewPoint(
                                                        mChartManager.GetXFromTimestamp(lStudyText.Position.X),
                                                        lStudyText.Position.Y)
            sCurrentText.Offset = mTextOffset
            sCurrentText.Text = lStudyText.Text
            If mConditionalActionsIndex >= 0 Then processConditionalActions(mChartManager.GetPeriod(lStudyText.Position.X).Timestamp, sCurrentText)
        Else
            If ev.sVal.BarNumber <> sCurrentBarNumber Then updateCurrentText(mCurrentStudyText)
            mCurrentStudyText = lStudyText
        End If

        sCurrentBarNumber = ev.sVal.BarNumber
    End Sub

    Private Sub setupBarSeries(studyValueConfig As StudyValueConfiguration, studyValueDef As StudyValueDefinition)
        mValueSeries = mRegion.AddGraphicObjectSeries(DirectCast(New BarSeries, IGraphicObjectSeries), studyValueConfig.Layer)
        mBarSeries = DirectCast(mValueSeries, BarSeries)
        If studyValueConfig.BarStyle Is Nothing Then studyValueConfig.BarStyle = DirectCast(studyValueDef.ValueStyle, BarStyle).clone
        mBarSeries.Style = studyValueConfig.BarStyle

        If studyValueConfig.BarFormatterFactoryName <> "" Then
            Dim lBarStudy = DirectCast(mStudy, IBarStudy)
            mBarFormatter = BarFormatters.CreateBarFormatterFactory(studyValueConfig.BarFormatterFactoryName,
                                                        studyValueConfig.BarFormatterLibraryName).CreateBarFormatter(lBarStudy.BarsFuture)
        End If
    End Sub

    Private Sub setupDataSeries(studyValueConfig As StudyValueConfiguration, studyValueDef As StudyValueDefinition)
        mValueSeries = mRegion.AddGraphicObjectSeries(DirectCast(New DataPointSeries, IGraphicObjectSeries), studyValueConfig.Layer)
        mDataSeries = DirectCast(mValueSeries, DataPointSeries)
        If studyValueConfig.DataPointStyle Is Nothing Then studyValueConfig.DataPointStyle = DirectCast(studyValueDef.ValueStyle, DataPointStyle).clone
        mDataSeries.Style = studyValueConfig.DataPointStyle
    End Sub

    Private Sub setupLineSeries(studyValueConfig As StudyValueConfiguration, studyValueDef As StudyValueDefinition)
        mValueSeries = mRegion.AddGraphicObjectSeries(DirectCast(New LineSeries, IGraphicObjectSeries), studyValueConfig.Layer)
        mLineSeries = DirectCast(mValueSeries, LineSeries)
        If studyValueConfig.LineStyle Is Nothing Then studyValueConfig.LineStyle = DirectCast(studyValueDef.ValueStyle, LineStyle).clone
        mLineSeries.Style = studyValueConfig.LineStyle
    End Sub

    Private Sub setupTextSeries(studyValueConfig As StudyValueConfiguration, studyValueDef As StudyValueDefinition)
        mValueSeries = mRegion.AddGraphicObjectSeries(DirectCast(New TextSeries, IGraphicObjectSeries), studyValueConfig.Layer)
        mTextSeries = DirectCast(mValueSeries, TextSeries)
        If studyValueConfig.TextStyle Is Nothing Then studyValueConfig.TextStyle = DirectCast(studyValueDef.ValueStyle, TextStyle).clone
        mTextSeries.Style = studyValueConfig.TextStyle
        mTextOffset = ChartUtils.ChartSkil.NewSize(studyValueConfig.OffsetX, studyValueConfig.OffsetY)
    End Sub

    Private Sub updateCurrentBar(ByVal pCurrentBar As BarUtils27.Bar)
        If pCurrentBar Is Nothing Then Exit Sub
        If pCurrentBar.Blank Then Exit Sub

        Dim lChartBar = addBarToChart(pCurrentBar.Timestamp)

        lChartBar.Tick(pCurrentBar.OpenValue)
        lChartBar.Tick(pCurrentBar.HighValue)
        lChartBar.Tick(pCurrentBar.LowValue)
        lChartBar.Tick(pCurrentBar.CloseValue)
        If mConditionalActionsIndex >= 0 Then processConditionalActions(pCurrentBar.Timestamp, lChartBar)
        If Not mBarFormatter Is Nothing Then mBarFormatter.FormatBar(pCurrentBar, lChartBar)
    End Sub

    Private Sub updateCurrentDatapoint(ByRef pCurrentDatapointValue As SValue)
        If IsNothing(pCurrentDatapointValue.Value) Then Exit Sub

        Dim lDataPoint As DataPoint
        If pCurrentDatapointValue.BarStartTime = COMZeroDate Then
            lDataPoint = addDataPointToChart(pCurrentDatapointValue.Timestamp)
        Else
            lDataPoint = addDataPointToChart(pCurrentDatapointValue.BarStartTime)
        End If

        lDataPoint.DataValue = CDbl(pCurrentDatapointValue.Value)
        If mConditionalActionsIndex >= 0 Then processConditionalActions(pCurrentDatapointValue.Timestamp, lDataPoint)
    End Sub

    Private Sub updateCurrentLine(ByVal pCurrentStudyLine As StudyLine)
        If pCurrentStudyLine Is Nothing Then Exit Sub
        If pCurrentStudyLine.Point1 Is Nothing Or pCurrentStudyLine.Point2 Is Nothing Then Exit Sub

        Dim lChartLine = addLineToChart()

        lChartLine.SetPosition(ChartUtils.ChartSkil.NewPoint(
                                                            mChartManager.GetXFromTimestamp(pCurrentStudyLine.Point1.X),
                                                            pCurrentStudyLine.Point1.Y),
                                        ChartUtils.ChartSkil.NewPoint(
                                                            mChartManager.GetXFromTimestamp(pCurrentStudyLine.Point2.X),
                                                            pCurrentStudyLine.Point2.Y))
        If mConditionalActionsIndex >= 0 Then processConditionalActions(mChartManager.GetPeriod(pCurrentStudyLine.Point1.X).Timestamp, lChartLine)
    End Sub

    Private Sub updateCurrentString(ByRef pCurrentStringValue As SValue)
        If IsNothing(pCurrentStringValue.Value) Then Exit Sub

        Dim lText = addTextToChart()

        lText.Position = ChartUtils.ChartSkil.NewPoint(
                                                    mChartManager.GetXFromTimestamp(pCurrentStringValue.Timestamp),
                                                    0, CoordinateSystems.CoordsCounterDistance, CoordinateSystems.CoordsRelative)
        lText.Text = CStr(pCurrentStringValue.Value)
        If mConditionalActionsIndex >= 0 Then processConditionalActions(pCurrentStringValue.Timestamp, lText)
    End Sub

    Private Sub updateCurrentText(ByVal pCurrentStudyText As StudyText)
        If pCurrentStudyText Is Nothing Then Exit Sub

        Dim lText = addTextToChart()

        lText.Position = ChartUtils.ChartSkil.NewPoint(
                                                mChartManager.GetXFromTimestamp(pCurrentStudyText.Position.X),
                                                pCurrentStudyText.Position.Y)
        lText.Offset = mTextOffset
        lText.Text = pCurrentStudyText.Text
        If mConditionalActionsIndex >= 0 Then processConditionalActions(mChartManager.GetPeriod(pCurrentStudyText.Position.X).Timestamp, lText)
    End Sub
#End Region

End Class