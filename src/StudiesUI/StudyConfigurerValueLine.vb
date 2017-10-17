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

Imports ChartSkil27
Imports StudyUtils27
Imports TradeWright.Trading.Utils.Charts

Friend Class StudyConfigurerValueLine

    Private mChart As AxChartSkil27.AxChart
    Private mStudyValueDef As StudyValueDefinition
    Private mStudyValueConfig As StudyValueConfiguration

    Private mFont As System.Drawing.Font

    Friend Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub AdvancedButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles AdvancedButton.Click
        notImplemented()
    End Sub

    Private Sub DisplayModeCombo_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles DisplayModeCombo.SelectionChangeCommitted
        Try
            Select Case mStudyValueDef.ValueMode
                Case StudyValueModes.ValueModeNone
                    Dim dpStyle As DataPointStyle = Nothing

                    If Not mStudyValueConfig Is Nothing Then
                        dpStyle = mStudyValueConfig.DataPointStyle
                    ElseIf Not mStudyValueDef.ValueStyle Is Nothing Then
                        dpStyle = DirectCast(mStudyValueDef.ValueStyle, DataPointStyle)
                    End If
                    If dpStyle Is Nothing Then dpStyle = ChartSkil.GetDefaultDataPointStyle.clone

                    Select Case DisplayModeCombo.SelectedItem.ToString
                        Case PointDisplayModeLine
                            initialiseLineStyleCombo(StyleCombo, dpStyle.LineStyle)
                        Case PointDisplayModePoint
                            initialisePointStyleCombo(StyleCombo, dpStyle.PointStyle)
                        Case PointDisplayModeSteppedLine
                            initialiseLineStyleCombo(StyleCombo, dpStyle.LineStyle)
                        Case PointDisplayModeHistogram
                            initialiseHistogramStyleCombo(StyleCombo, dpStyle.HistogramBarWidth)
                    End Select
                Case StudyValueModes.ValueModeLine

                Case StudyValueModes.ValueModeBar

                Case StudyValueModes.ValueModeText

            End Select

        Catch ex As Exception
            gLogger.log(TWUtilities40.LogLevels.LogLevelSevere, ex.ToString)
        End Try
    End Sub

    Private Sub DisplayModeCombo_Validating(eventSender As System.Object, eventArgs As System.ComponentModel.CancelEventArgs) Handles DisplayModeCombo.Validating
        eventArgs.Cancel = (DisplayModeCombo.SelectedItem Is Nothing)
    End Sub

    Private Sub FontButton_Click(eventSender As System.Object, eventArgs As System.EventArgs) Handles FontButton.Click
        Try
            Dim fd As New FontDialog() With {.Font = mFont, .Color = ColorPicker.Color, .ShowEffects = True, .ShowApply = True, .ShowColor = True}

            fd.ShowDialog()

            mFont = fd.Font

            ColorPicker.Color = fd.Color

        Catch ex As Exception
            gLogger.log(TWUtilities40.LogLevels.LogLevelSevere, ex.ToString)
        End Try
    End Sub

    Private Sub StyleCombo_Validating(eventSender As System.Object, eventArgs As System.ComponentModel.CancelEventArgs) Handles StyleCombo.Validating
        eventArgs.Cancel = (StyleCombo.SelectedItem Is Nothing)
    End Sub

    Friend Sub ApplyUpdates(studyValueConfig As StudyValueConfiguration)
        Try
            If Not mStudyValueConfig Is Nothing Then
                studyValueConfig.BarFormatterFactoryName = mStudyValueConfig.BarFormatterFactoryName
                studyValueConfig.BarFormatterLibraryName = mStudyValueConfig.BarFormatterLibraryName
            End If

            studyValueConfig.IncludeInChart = (IncludeCheck.CheckState = System.Windows.Forms.CheckState.Checked)

            studyValueConfig.ChartRegionName = getRegionName()

            Select Case mStudyValueDef.ValueMode
                Case StudyValueModes.ValueModeNone

                    Dim dpStyle = ChartSkil.GetDefaultDataPointStyle.clone

                    dpStyle.IncludeInAutoscale = AutoscaleCheck.Checked
                    dpStyle.Color = ColorTranslator.ToOle(ColorPicker.Color)
                    dpStyle.DownColor = CInt(IIf(DownColorPicker.IsColorNull, -1, ColorTranslator.ToOle(DownColorPicker.Color)))
                    dpStyle.UpColor = CInt(IIf(UpColorPicker.IsColorNull, -1, ColorTranslator.ToOle(UpColorPicker.Color)))

                    Select Case DisplayModeCombo.SelectedItem.ToString
                        Case PointDisplayModeLine
                            dpStyle.DisplayMode = DataPointDisplayModes.DataPointDisplayModeLine
                            Select Case StyleCombo.SelectedItem.ToString
                                Case LineStyleSolid
                                    dpStyle.LineStyle = LineStyles.LineSolid
                                Case LineStyleDash
                                    dpStyle.LineStyle = LineStyles.LineDash
                                Case LineStyleDot
                                    dpStyle.LineStyle = LineStyles.LineDot
                                Case LineStyleDashDot
                                    dpStyle.LineStyle = LineStyles.LineDashDot
                                Case LineStyleDashDotDot
                                    dpStyle.LineStyle = LineStyles.LineDashDotDot
                            End Select
                        Case PointDisplayModePoint
                            dpStyle.DisplayMode = DataPointDisplayModes.DataPointDisplayModePoint
                            Select Case StyleCombo.SelectedItem.ToString
                                Case PointStyleRound
                                    dpStyle.PointStyle = PointStyles.PointRound
                                Case PointStyleSquare
                                    dpStyle.PointStyle = PointStyles.PointSquare
                            End Select
                        Case PointDisplayModeSteppedLine
                            dpStyle.DisplayMode = DataPointDisplayModes.DataPointDisplayModeStep
                            Select Case StyleCombo.SelectedItem.ToString
                                Case LineStyleSolid
                                    dpStyle.LineStyle = LineStyles.LineSolid
                                Case LineStyleDash
                                    dpStyle.LineStyle = LineStyles.LineDash
                                Case LineStyleDot
                                    dpStyle.LineStyle = LineStyles.LineDot
                                Case LineStyleDashDot
                                    dpStyle.LineStyle = LineStyles.LineDashDot
                                Case LineStyleDashDotDot
                                    dpStyle.LineStyle = LineStyles.LineDashDotDot
                            End Select
                        Case PointDisplayModeHistogram
                            dpStyle.DisplayMode = DataPointDisplayModes.DataPointDisplayModeHistogram
                            Select Case StyleCombo.SelectedItem.ToString
                                Case HistogramStyleNarrow
                                    dpStyle.HistogramBarWidth = HistogramWidthNarrow
                                Case HistogramStyleMedium
                                    dpStyle.HistogramBarWidth = HistogramWidthMedium
                                Case HistogramStyleWide
                                    dpStyle.HistogramBarWidth = HistogramWidthWide
                                Case CustomStyle
                                    dpStyle.HistogramBarWidth = CType(StyleCombo.SelectedItem, CustomValue).Value
                            End Select
                    End Select

                    dpStyle.LineThickness = CInt(ThicknessUpDown.Value)

                    studyValueConfig.DataPointStyle = dpStyle
                Case StudyValueModes.ValueModeLine

                    Dim lnStyle = ChartSkil.GetDefaultLineStyle.clone

                    lnStyle.IncludeInAutoscale = AutoscaleCheck.Checked
                    lnStyle.Color = ColorTranslator.ToOle(ColorPicker.Color)
                    lnStyle.ArrowStartColor = ColorTranslator.ToOle(ColorPicker.Color)
                    lnStyle.ArrowEndColor = ColorTranslator.ToOle(ColorPicker.Color)
                    lnStyle.ArrowStartFillColor = CInt(IIf(UpColorPicker.IsColorNull, -1, ColorTranslator.ToOle(UpColorPicker.Color)))
                    lnStyle.ArrowEndFillColor = CInt(IIf(DownColorPicker.IsColorNull, -1, ColorTranslator.ToOle(DownColorPicker.Color)))

                    Select Case DisplayModeCombo.SelectedItem.ToString
                        Case LineDisplayModePlain
                            lnStyle.ArrowEndStyle = ArrowStyles.ArrowNone
                            lnStyle.ArrowStartStyle = ArrowStyles.ArrowNone
                        Case LineDisplayModeArrowEnd
                            lnStyle.ArrowEndStyle = ArrowStyles.ArrowClosed
                            lnStyle.ArrowStartStyle = ArrowStyles.ArrowNone
                        Case LineDisplayModeArrowStart
                            lnStyle.ArrowEndStyle = ArrowStyles.ArrowNone
                            lnStyle.ArrowStartStyle = ArrowStyles.ArrowClosed
                        Case LineDisplayModeArrowBoth
                            lnStyle.ArrowEndStyle = ArrowStyles.ArrowClosed
                            lnStyle.ArrowStartStyle = ArrowStyles.ArrowClosed
                    End Select

                    Select Case StyleCombo.SelectedItem.ToString
                        Case LineStyleSolid
                            lnStyle.LineStyle = LineStyles.LineSolid
                        Case LineStyleDash
                            lnStyle.LineStyle = LineStyles.LineDash
                        Case LineStyleDot
                            lnStyle.LineStyle = LineStyles.LineDot
                        Case LineStyleDashDot
                            lnStyle.LineStyle = LineStyles.LineDashDot
                        Case LineStyleDashDotDot
                            lnStyle.LineStyle = LineStyles.LineDashDotDot
                    End Select

                    lnStyle.Thickness = CInt(ThicknessUpDown.Value)
                    ' temporary fix until ChartSkil improves drawing of non-extended lines
                    lnStyle.Extended = True

                    studyValueConfig.LineStyle = lnStyle

                Case StudyValueModes.ValueModeBar

                    Dim brStyle = ChartSkil.GetDefaultBarStyle.clone

                    brStyle.IncludeInAutoscale = AutoscaleCheck.Checked
                    brStyle.Color = CInt(IIf(ColorPicker.IsColorNull, -1, ColorTranslator.ToOle(ColorPicker.Color)))
                    brStyle.DownColor = CInt(IIf(DownColorPicker.IsColorNull, -1, ColorTranslator.ToOle(DownColorPicker.Color)))
                    brStyle.UpColor = ColorTranslator.ToOle(UpColorPicker.Color)

                    Select Case DisplayModeCombo.SelectedItem.ToString
                        Case BarModeBar
                            brStyle.DisplayMode = BarDisplayModes.BarDisplayModeBar
                            brStyle.Thickness = CInt(ThicknessUpDown.Value)
                        Case BarModeCandle
                            brStyle.DisplayMode = BarDisplayModes.BarDisplayModeCandlestick
                            brStyle.SolidUpBody = False
                            brStyle.TailThickness = CInt(ThicknessUpDown.Value)
                        Case BarModeSolidCandle
                            brStyle.DisplayMode = BarDisplayModes.BarDisplayModeCandlestick
                            brStyle.SolidUpBody = True
                            brStyle.TailThickness = CInt(ThicknessUpDown.Value)
                        Case BarModeLine
                            brStyle.DisplayMode = BarDisplayModes.BarDisplayModeLine
                    End Select

                    Select Case StyleCombo.SelectedItem.ToString
                        Case BarStyleNarrow
                            brStyle.Width = BarWidthNarrow
                        Case BarStyleMedium
                            brStyle.Width = BarWidthMedium
                        Case BarStyleWide
                            brStyle.Width = BarWidthWide
                        Case CustomStyle
                            brStyle.Width = CType(StyleCombo.SelectedItem, CustomValue).Value
                    End Select

                    studyValueConfig.BarStyle = brStyle

                Case StudyValueModes.ValueModeText

                    Dim txStyle = ChartSkil.GetDefaultTextStyle.clone

                    txStyle.IncludeInAutoscale = AutoscaleCheck.Checked
                    txStyle.Color = ColorTranslator.ToOle(ColorPicker.Color)
                    If UpColorPicker.IsColorNull Then
                        txStyle.BoxFillWithBackgroundColor = True
                    Else
                        txStyle.BoxFillColor = ColorTranslator.ToOle(UpColorPicker.Color)
                    End If
                    txStyle.BoxColor = CInt(IIf(DownColorPicker.IsColorNull, -1, ColorTranslator.ToOle(DownColorPicker.Color)))

                    Select Case DisplayModeCombo.SelectedItem.ToString
                        Case TextDisplayModePlain
                            txStyle.Box = False
                        Case TextDisplayModeWIthBackground
                            txStyle.Box = True
                            txStyle.BoxStyle = LineStyles.LineInvisible
                            txStyle.BoxFillStyle = FillStyles.FillSolid
                        Case TextDisplayModeWithBox
                            txStyle.Box = True
                            txStyle.BoxStyle = LineStyles.LineInsideSolid
                            txStyle.BoxFillStyle = FillStyles.FillTransparent
                        Case TextDisplayModeWithFilledBox
                            txStyle.Box = True
                            txStyle.BoxStyle = LineStyles.LineInsideSolid
                            txStyle.BoxFillStyle = FillStyles.FillSolid
                    End Select

                    Dim aFont As New stdole.StdFont
                    aFont.Bold = mFont.Bold
                    aFont.Italic = mFont.Italic
                    aFont.Name = mFont.Name
                    aFont.Size = CDec(mFont.Size)
                    aFont.Strikethrough = mFont.Strikeout
                    aFont.Underline = mFont.Underline
                    txStyle.Font = aFont

                    txStyle.BoxThickness = CInt(ThicknessUpDown.Value)
                    ' temporary fix until ChartSkil improves drawing of non-extended texts
                    txStyle.Extended = True

                    studyValueConfig.TextStyle = txStyle


            End Select

        Catch ex As Exception
            gLogger.log(TWUtilities40.LogLevels.LogLevelSevere, ex.ToString)
        End Try

    End Sub

    Friend Sub Initialise(studyValueDef As StudyValueDefinition, studyValueConfig As StudyValueConfiguration, chart As AxChartSkil27.AxChart)
        Try
            DownColorPicker.Visible = False
            DisplayModeCombo.Visible = False
            FontButton.Visible = False
            StyleCombo.Visible = False
            ThicknessUpDown.Visible = False
            UpColorPicker.Visible = False

            mStudyValueDef = studyValueDef
            mStudyValueConfig = studyValueConfig
            mChart = chart

            AutoscaleCheck.CheckState = System.Windows.Forms.CheckState.Unchecked

            ValueNameLabel.Text = mStudyValueDef.Name
            ToolTip1.SetToolTip(ValueNameLabel, studyValueDef.Description)

            If Not mStudyValueConfig Is Nothing Then
                IncludeCheck.Checked = mStudyValueConfig.IncludeInChart
            Else
                IncludeCheck.Checked = mStudyValueDef.IncludeInChart
            End If

            Select Case studyValueDef.ValueMode
                Case StudyValueModes.ValueModeNone
                    Dim dpStyle As DataPointStyle = Nothing

                    ToolTip1.SetToolTip(ColorPicker, "Select the color for all values")

                    UpColorPicker.Visible = True
                    ToolTip1.SetToolTip(UpColorPicker, "Optionally, select the color for higher values")

                    DownColorPicker.Visible = True
                    ToolTip1.SetToolTip(DownColorPicker, "Optionally, select the color for lower values")

                    DisplayModeCombo.Visible = True
                    StyleCombo.Visible = True

                    If studyValueConfig IsNot Nothing Then
                        dpStyle = studyValueConfig.DataPointStyle
                    ElseIf studyValueDef.ValueStyle IsNot Nothing Then
                        dpStyle = CType(studyValueDef.ValueStyle, DataPointStyle)
                    End If
                    If dpStyle Is Nothing Then dpStyle = ChartSkil.GetDefaultDataPointStyle.clone

                    AutoscaleCheck.Checked = dpStyle.IncludeInAutoscale

                    ColorPicker.OleColor = dpStyle.Color
                    UpColorPicker.Visible = True
                    UpColorPicker.OleColor = dpStyle.UpColor
                    DownColorPicker.Visible = True
                    DownColorPicker.OleColor = dpStyle.DownColor

                    initialisePointDisplayModeCombo(DisplayModeCombo, dpStyle.DisplayMode)
                    Select Case dpStyle.DisplayMode
                        Case DataPointDisplayModes.DataPointDisplayModeLine
                            initialiseLineStyleCombo(StyleCombo, dpStyle.LineStyle)
                        Case DataPointDisplayModes.DataPointDisplayModePoint
                            initialisePointStyleCombo(StyleCombo, dpStyle.PointStyle)
                        Case DataPointDisplayModes.DataPointDisplayModeStep
                            initialiseLineStyleCombo(StyleCombo, dpStyle.LineStyle)
                        Case DataPointDisplayModes.DataPointDisplayModeHistogram
                            initialiseHistogramStyleCombo(StyleCombo, dpStyle.HistogramBarWidth)
                    End Select

                    ThicknessUpDown.Value = CDec(dpStyle.LineThickness)
                    ThicknessUpDown.Visible = True

                Case StudyValueModes.ValueModeLine
                    Dim lnStyle As LineStyle = Nothing

                    ToolTip1.SetToolTip(ColorPicker, "Select the color for the line")

                    UpColorPicker.Visible = True
                    ToolTip1.SetToolTip(UpColorPicker, "Optionally, select the color for the start arrowhead")

                    DownColorPicker.Visible = True
                    ToolTip1.SetToolTip(DownColorPicker, "Optionally, select the color for the end arrowhead")

                    DisplayModeCombo.Visible = True
                    StyleCombo.Visible = True

                    If studyValueConfig IsNot Nothing Then
                        lnStyle = studyValueConfig.LineStyle
                    ElseIf studyValueDef.ValueStyle IsNot Nothing Then
                        lnStyle = CType(studyValueDef.ValueStyle, LineStyle)
                    End If
                    If lnStyle Is Nothing Then lnStyle = ChartSkil.GetDefaultLineStyle.clone

                    AutoscaleCheck.Checked = lnStyle.IncludeInAutoscale
                    ColorPicker.OleColor = lnStyle.Color
                    UpColorPicker.Visible = True
                    UpColorPicker.OleColor = lnStyle.ArrowStartFillColor
                    DownColorPicker.Visible = True
                    DownColorPicker.OleColor = lnStyle.ArrowEndFillColor

                    initialiseLineDisplayModeCombo(DisplayModeCombo, (lnStyle.ArrowStartStyle <> ArrowStyles.ArrowNone), (lnStyle.ArrowEndStyle <> ArrowStyles.ArrowNone))

                    initialiseLineStyleCombo(StyleCombo, lnStyle.LineStyle)

                    ThicknessUpDown.Value = CDec(lnStyle.Thickness)
                    ThicknessUpDown.Visible = True

                Case StudyValueModes.ValueModeBar
                    Dim brStyle As BarStyle = Nothing

                    ToolTip1.SetToolTip(ColorPicker, "Optionally, select the color for the bar or the candlestick frame")

                    UpColorPicker.Visible = True
                    ToolTip1.SetToolTip(UpColorPicker, "Select the color for up bars")

                    DownColorPicker.Visible = True
                    ToolTip1.SetToolTip(DownColorPicker, "Optionally, select the color for down bars")

                    DisplayModeCombo.Visible = True
                    StyleCombo.Visible = True

                    If studyValueConfig IsNot Nothing Then
                        brStyle = studyValueConfig.BarStyle
                    ElseIf studyValueDef.ValueStyle IsNot Nothing Then
                        brStyle = CType(studyValueDef.ValueStyle, BarStyle)
                    End If
                    If brStyle Is Nothing Then brStyle = ChartSkil.GetDefaultBarStyle.clone

                    AutoscaleCheck.Checked = brStyle.IncludeInAutoscale
                    ColorPicker.OleColor = brStyle.Color
                    UpColorPicker.OleColor = brStyle.UpColor
                    DownColorPicker.OleColor = brStyle.DownColor

                    initialiseBarDisplayModeCombo(DisplayModeCombo, brStyle.DisplayMode, brStyle.SolidUpBody)

                    initialiseBarStyleCombo(StyleCombo, brStyle.Width)

                    Select Case DisplayModeCombo.SelectedItem.ToString
                        Case BarModeBar
                            ThicknessUpDown.Value = CDec(brStyle.Thickness)
                        Case BarModeCandle
                            ThicknessUpDown.Value = CDec(brStyle.TailThickness)
                        Case BarModeSolidCandle
                            ThicknessUpDown.Value = CDec(brStyle.TailThickness)
                        Case BarModeLine
                            ThicknessUpDown.Value = CDec(brStyle.Thickness)
                    End Select
                    ThicknessUpDown.Visible = True

                Case StudyValueModes.ValueModeText
                    Dim txStyle As TextStyle = Nothing

                    ToolTip1.SetToolTip(ColorPicker, "Select the color for the text")

                    UpColorPicker.Visible = True ' box fill color
                    ToolTip1.SetToolTip(UpColorPicker, "Optionally, select the color for the box fill")

                    DownColorPicker.Visible = True ' box outline color
                    ToolTip1.SetToolTip(UpColorPicker, "Optionally, select the color for the box outline")

                    DisplayModeCombo.Visible = True
                    StyleCombo.Visible = False
                    FontButton.Visible = True

                    If studyValueConfig IsNot Nothing Then
                        txStyle = studyValueConfig.TextStyle
                    ElseIf studyValueDef.ValueStyle IsNot Nothing Then
                        txStyle = CType(studyValueDef.ValueStyle, TextStyle)
                    End If
                    If txStyle Is Nothing Then txStyle = ChartSkil.GetDefaultTextStyle.clone

                    AutoscaleCheck.Checked = txStyle.IncludeInAutoscale
                    ColorPicker.OleColor = txStyle.Color
                    UpColorPicker.OleColor = txStyle.BoxFillColor
                    If txStyle.BoxFillWithBackgroundColor Then UpColorPicker.OleColor = -1
                    DownColorPicker.OleColor = txStyle.BoxColor

                    initialiseTextDisplayModeCombo(DisplayModeCombo, txStyle.Box, txStyle.BoxThickness, txStyle.BoxStyle, txStyle.BoxColor, txStyle.BoxFillStyle, txStyle.BoxFillColor)

                    ThicknessUpDown.Value = CDec(txStyle.BoxThickness)
                    ThicknessUpDown.Visible = True

                    Dim fs As FontStyle
                    If txStyle.Font.Bold Then fs = FontStyle.Bold
                    If txStyle.Font.Italic Then fs = fs Or FontStyle.Italic
                    If txStyle.Font.Strikethrough Then fs = fs Or FontStyle.Strikeout
                    If txStyle.Font.Underline Then fs = fs Or FontStyle.Underline
                    mFont = New Font(New FontFamily(txStyle.Font.Name), txStyle.Font.Size, fs)

            End Select
        Catch ex As Exception
            gLogger.Log(TWUtilities40.LogLevels.LogLevelSevere, ex.ToString)
        End Try

    End Sub

    Private Function getDefaultRegionName() As String
        Select Case mStudyValueDef.DefaultRegion
            Case StudyValueDefaultRegions.StudyValueDefaultRegionNone
                Return ChartUtils.ChartRegionNameDefault
            Case StudyValueDefaultRegions.StudyValueDefaultRegionDefault
                Return ChartUtils.ChartRegionNameDefault
            Case StudyValueDefaultRegions.StudyValueDefaultRegionCustom
                Return ChartUtils.ChartRegionNameCustom
            Case StudyValueDefaultRegions.StudyValueDefaultRegionUnderlying
                Return ChartUtils.ChartRegionNameUnderlying
            Case Else
                Throw New InvalidOperationException()
        End Select
    End Function

    Private Function getRegionName() As String
        If useCurrentRegionName Then
            getRegionName = mStudyValueConfig.ChartRegionName
        Else
            getRegionName = getDefaultRegionName
        End If
    End Function

    Private Sub initialiseBarDisplayModeCombo(combo As ComboBox, pDisplayMode As BarDisplayModes, pSolid As Boolean)
        combo.Items.Clear()

        combo.Items.Add(BarModeBar)
        If pDisplayMode = BarDisplayModes.BarDisplayModeBar Then combo.SelectedItem = BarModeBar

        combo.Items.Add(BarModeCandle)
        If pDisplayMode = BarDisplayModes.BarDisplayModeCandlestick And Not pSolid Then combo.SelectedItem = BarModeCandle

        combo.Items.Add(BarModeSolidCandle)
        If pDisplayMode = BarDisplayModes.BarDisplayModeCandlestick And pSolid Then combo.SelectedItem = BarModeSolidCandle

        combo.Items.Add(BarModeLine)
        If pDisplayMode = BarDisplayModes.BarDisplayModeLine Then combo.SelectedItem = BarModeLine

        ToolTip1.SetToolTip(combo, "Select the type of bar")

    End Sub

    Private Sub initialiseBarStyleCombo(combo As ComboBox, barWidth As Single)
        Dim selected As Boolean

        combo.Items.Clear()

        combo.Items.Add(BarStyleMedium)
        If barWidth = BarWidthMedium Then combo.SelectedItem = BarStyleMedium : selected = True

        combo.Items.Add(BarStyleNarrow)
        If barWidth = BarWidthNarrow Then combo.SelectedItem = BarStyleNarrow : selected = True

        combo.Items.Add(BarStyleWide)
        If barWidth = BarWidthWide Then combo.SelectedItem = BarStyleWide : selected = True

        If Not selected Then
            combo.Items.Insert(0, New CustomValue(barWidth))
            combo.SelectedIndex = 0
        End If

        ToolTip1.SetToolTip(combo, "Select the width of the bar")

    End Sub

    Private Sub initialiseHistogramStyleCombo(combo As ComboBox, histBarWidth As Single)
        Dim selected As Boolean

        combo.Items.Clear()

        combo.Items.Add(HistogramStyleMedium)
        If histBarWidth = HistogramWidthMedium Then combo.SelectedItem = HistogramStyleMedium : selected = True

        combo.Items.Add(HistogramStyleNarrow)
        If histBarWidth = HistogramWidthNarrow Then combo.SelectedItem = HistogramStyleNarrow : selected = True

        combo.Items.Add(HistogramStyleWide)
        If histBarWidth = HistogramWidthWide Then combo.SelectedItem = HistogramStyleWide : selected = True

        If Not selected Then
            combo.Items.Insert(0, New CustomValue(histBarWidth))
            combo.SelectedIndex = 0
        End If

        ToolTip1.SetToolTip(combo, "Select the width of the histogram")

    End Sub

    Private Sub initialiseLineDisplayModeCombo(combo As ComboBox, pArrowStart As Boolean, pArrowEnd As Boolean)
        combo.Items.Clear()

        combo.Items.Add(LineDisplayModePlain)
        If Not pArrowStart And Not pArrowEnd Then combo.SelectedItem = LineDisplayModePlain

        combo.Items.Add(LineDisplayModeArrowEnd)
        If Not pArrowStart And pArrowEnd Then combo.SelectedItem = LineDisplayModeArrowEnd

        combo.Items.Add(LineDisplayModeArrowStart)
        If pArrowStart And Not pArrowEnd Then combo.SelectedItem = LineDisplayModeArrowStart

        combo.Items.Add(LineDisplayModeArrowBoth)
        If pArrowStart And pArrowEnd Then combo.SelectedItem = LineDisplayModeArrowBoth

        ToolTip1.SetToolTip(combo, "Select the type of line")

    End Sub

    Private Sub initialiseLineStyleCombo(combo As ComboBox, pLineStyle As LineStyles)

        combo.Items.Clear()

        combo.Items.Add(LineStyleSolid)
        If pLineStyle = LineStyles.LineSolid Then combo.SelectedItem = LineStyleSolid

        combo.Items.Add(LineStyleDash)
        If pLineStyle = LineStyles.LineDash Then combo.SelectedItem = LineStyleDash

        combo.Items.Add(LineStyleDot)
        If pLineStyle = LineStyles.LineDot Then combo.SelectedItem = LineStyleDot

        combo.Items.Add(LineStyleDashDot)
        If pLineStyle = LineStyles.LineDashDot Then combo.SelectedItem = LineStyleDashDot

        combo.Items.Add(LineStyleDashDotDot)
        If pLineStyle = LineStyles.LineDashDotDot Then combo.SelectedItem = LineStyleDashDotDot

        ToolTip1.SetToolTip(combo, "Select the style of the line")

    End Sub

    Private Sub initialisePointDisplayModeCombo(combo As ComboBox, pDisplayMode As DataPointDisplayModes)

        combo.Items.Clear()

        combo.Items.Add(PointDisplayModeLine)
        If pDisplayMode = DataPointDisplayModes.DataPointDisplayModeLine Then combo.SelectedItem = PointDisplayModeLine

        combo.Items.Add(PointDisplayModePoint)
        If pDisplayMode = DataPointDisplayModes.DataPointDisplayModePoint Then combo.SelectedItem = PointDisplayModePoint

        combo.Items.Add(PointDisplayModeSteppedLine)
        If pDisplayMode = DataPointDisplayModes.DataPointDisplayModeStep Then combo.SelectedItem = PointDisplayModeSteppedLine

        combo.Items.Add(PointDisplayModeHistogram)
        If pDisplayMode = DataPointDisplayModes.DataPointDisplayModeHistogram Then combo.SelectedItem = PointDisplayModeHistogram

    End Sub

    Private Sub initialisePointStyleCombo(combo As ComboBox, pPointStyle As PointStyles)

        combo.Items.Clear()

        combo.Items.Add(PointStyleRound)
        If pPointStyle = PointStyles.PointRound Then combo.SelectedItem = PointStyleRound

        combo.Items.Add(PointStyleSquare)
        If pPointStyle = PointStyles.PointSquare Then combo.SelectedItem = PointStyleSquare

        ToolTip1.SetToolTip(combo, "Select the shape of the point")

    End Sub

    Private Sub initialiseTextDisplayModeCombo(combo As ComboBox, pBox As Boolean, pBoxThickness As Integer, pBoxStyle As LineStyles, pBoxColor As Integer, pBoxFillStyle As FillStyles, pBoxFillColor As Integer)
        Dim selected As Boolean

        combo.Items.Clear()

        combo.Items.Add(TextDisplayModePlain)
        If Not pBox Then combo.SelectedItem = TextDisplayModePlain : selected = True

        combo.Items.Add(TextDisplayModeWIthBackground)
        If pBox And (pBoxStyle = LineStyles.LineInvisible Or pBoxThickness = 0) And pBoxFillStyle = FillStyles.FillSolid Then combo.SelectedItem = TextDisplayModeWIthBackground : selected = True

        combo.Items.Add(TextDisplayModeWithBox)
        If pBox And pBoxStyle <> LineStyles.LineInvisible And pBoxThickness = 0 And pBoxFillStyle = FillStyles.FillTransparent Then combo.SelectedItem = TextDisplayModeWithBox : selected = True

        combo.Items.Add(TextDisplayModeWithFilledBox)
        If pBox And pBoxStyle <> LineStyles.LineInvisible And pBoxThickness = 0 And pBoxFillStyle = FillStyles.FillSolid Then combo.SelectedItem = TextDisplayModeWithFilledBox : selected = True

        If Not selected Then
            combo.Items.Add(CustomDisplayMode)
            combo.SelectedItem = CustomDisplayMode
        End If

        ToolTip1.SetToolTip(combo, "Select the type of text")

    End Sub

    Private Function useCurrentRegionName() As Boolean
        If mStudyValueConfig Is Nothing Then Exit Function
        If mStudyValueConfig.ChartRegionName <> "" Then useCurrentRegionName = True
    End Function

    Private Class CustomValue
        Private _value As Single

        Friend Sub New(value As Single)
            _value = value
        End Sub

        Friend ReadOnly Property Value() As Single
            Get
                Return _value
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return CustomStyle
        End Function
    End Class

End Class
