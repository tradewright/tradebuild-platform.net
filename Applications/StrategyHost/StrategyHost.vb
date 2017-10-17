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
Imports TWUtilities40

Module StrategyHost

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

#End Region

#Region "Member variables"

    Friend TWUtilities As New TWUtilities40.TWUtilities

    Friend BarUtils As New BarUtils27.BarUtils
    Friend ChartSkil As New ChartSkil27.ChartSkil
    Friend StudyUtils As New StudyUtils27.StudyUtils
    Friend TimeframeUtils As New TimeframeUtils27.TimeframeUtils
    Friend TradeBuild As New TradeBuild27.TradeBuild
    Friend TradingDO As New TradingDO27.TradingDO
    Friend TWControls As New TWControls40.TWControls

#End Region

#Region "Constructors"

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Public Sub ApplyTheme(pTheme As ITheme, pControls As Control.ControlCollection)
        If pTheme Is Nothing Then Exit Sub

        Dim lControl As Control
        For Each lControl In pControls
            ApplyThemeToControl(pTheme, lControl)
            ApplyTheme(pTheme, lControl.Controls)
        Next
    End Sub

    Public Sub ApplyThemeToControl(pTheme As ITheme, pControl As Control)
        If TypeOf pControl Is Label Then
            Dim lLabel = DirectCast(pControl, Label)
            lLabel.BorderStyle = getDotNetBorderStyle(pTheme.Appearance, pTheme.BorderStyle)
            lLabel.BackColor = ColorTranslator.FromOle(CInt(pTheme.BackColor))
            lLabel.ForeColor = ColorTranslator.FromOle(CInt(pTheme.ForeColor))
        ElseIf TypeOf pControl Is CheckBox Or
            TypeOf pControl Is GroupBox Or
            TypeOf pControl Is RadioButton _
        Then
            pControl.BackColor = ColorTranslator.FromOle(CInt(pTheme.BackColor))
            pControl.ForeColor = ColorTranslator.FromOle(CInt(pTheme.ForeColor))
        ElseIf TypeOf pControl Is PictureBox Then
            Dim lPicture = DirectCast(pControl, PictureBox)
            lPicture.BorderStyle = getDotNetBorderStyle(pTheme.Appearance, pTheme.BorderStyle)
            lPicture.BackColor = ColorTranslator.FromOle(CInt(pTheme.BackColor))
            lPicture.ForeColor = ColorTranslator.FromOle(CInt(pTheme.ForeColor))
        ElseIf TypeOf pControl Is TextBox Then
            Dim lText = DirectCast(pControl, TextBox)
            lText.BorderStyle = getDotNetBorderStyle(pTheme.Appearance, pTheme.BorderStyle)
            lText.BackColor = ColorTranslator.FromOle(CInt(pTheme.TextBackColor))
            lText.ForeColor = ColorTranslator.FromOle(CInt(pTheme.TextForeColor))
            If Not pTheme.TextFont Is Nothing Then
                pControl.Font = getDotNetFont(pTheme.TextFont)
            ElseIf Not pTheme.BaseFont Is Nothing Then
                pControl.Font = getDotNetFont(pTheme.BaseFont)
            End If
        ElseIf TypeOf pControl Is ComboBox Or
            TypeOf pControl Is ListBox _
        Then

            pControl.BackColor = ColorTranslator.FromOle(CInt(pTheme.TextBackColor))
            pControl.ForeColor = ColorTranslator.FromOle(CInt(pTheme.TextForeColor))
            If Not pTheme.ComboFont Is Nothing Then
                pControl.Font = getDotNetFont(pTheme.ComboFont)
            ElseIf Not pTheme.BaseFont Is Nothing Then
                pControl.Font = getDotNetFont(pTheme.BaseFont)
            End If
        ElseIf TypeOf pControl Is ListView Then
            Dim lListView = DirectCast(pControl, ListView)
            lListView.BackColor = ColorTranslator.FromOle(CInt(pTheme.GridBackColorFixed))
            lListView.Font = getDotNetFont(pTheme.GridFont)
        ElseIf TypeOf pControl Is TabPage Then
            Dim lTabPage = DirectCast(pControl, TabPage)
            lTabPage.BackColor = ColorTranslator.FromOle(CInt(pTheme.GridBackColorFixed))
        ElseIf TypeOf pControl Is Button Then
            ' nothing for these
            'ElseIf TypeOf pControl Is ToolBar Then
            '    pControl.Appearance = pTheme.Appearance
            '    pControl.BorderStyle = pTheme.BorderStyle

            '    If pControl.Style = tbrStandard Then
            '        Dim lDoneFirstStandardToolbar As Boolean
            '        If Not lDoneFirstStandardToolbar Then
            '            lDoneFirstStandardToolbar = True
            '            SetToolbarColor(pControl, pTheme.ToolbarBackColor)
            '        End If
            '    Else
            '        Dim lDoneFirstFlatToolbar As Boolean
            '        If Not lDoneFirstFlatToolbar Then
            '            lDoneFirstFlatToolbar = True
            '            SetToolbarColor(pControl, pTheme.ToolbarBackColor)
            '        End If
            '    End If
            '    pControl.Refresh()
        ElseIf TypeOf pControl Is TabControl Then
            pControl.BackColor = ColorTranslator.FromOle(CInt(pTheme.TabstripBackColor))
            pControl.ForeColor = ColorTranslator.FromOle(CInt(pTheme.TabstripForeColor))
        ElseIf TypeOf pControl Is AxHost Then
            Dim lCOMControl = DirectCast(pControl, AxHost).GetOcx()
            If TypeOf lCOMControl Is IThemeable Then
                Dim lThemeable = DirectCast(lCOMControl, IThemeable)
                lThemeable.Theme = pTheme
            End If
        End If
    End Sub

    Public Function CreateChartStyle() As ChartStyle
        TWUtilities.LogMessage("Creating chart style ""Black""")

        Dim lCrosshairsLineStyle As LineStyle
        lCrosshairsLineStyle = New LineStyle
        lCrosshairsLineStyle.Color = 128
        lCrosshairsLineStyle.LineStyle = LineStyles.LineSolid
        lCrosshairsLineStyle.Thickness = 1

        Dim lDefaultRegionStyle As ChartRegionStyle
        lDefaultRegionStyle = ChartSkil.GetDefaultChartDataRegionStyle.clone
        lDefaultRegionStyle.IntegerYScale = False
        lDefaultRegionStyle.YScaleQuantum = 0.015625
        lDefaultRegionStyle.YGridlineSpacing = 1.8
        lDefaultRegionStyle.MinimumHeight = 0.015625
        lDefaultRegionStyle.CursorSnapsToTickBoundaries = True
        lDefaultRegionStyle.BackGradientFillColors = {2105376, 2105376}

        lDefaultRegionStyle.XGridLineStyle = ChartSkil.GetDefaultLineStyle.clone
        lDefaultRegionStyle.XGridLineStyle.Color = 3158064
        lDefaultRegionStyle.YGridLineStyle = lDefaultRegionStyle.XGridLineStyle
        lDefaultRegionStyle.SessionEndGridLineStyle = lDefaultRegionStyle.XGridLineStyle
        lDefaultRegionStyle.SessionStartGridLineStyle = lDefaultRegionStyle.SessionEndGridLineStyle.clone
        lDefaultRegionStyle.SessionStartGridLineStyle.Thickness = 3

        Dim lXAxisStyle As ChartRegionStyle
        lXAxisStyle = ChartSkil.GetDefaultChartXAxisRegionStyle.clone
        lXAxisStyle.CursorTextPosition = CursorTextPositions.CursorTextPositionBelowLeftCursor
        lXAxisStyle.XGridTextPosition = XGridTextPositions.XGridTextPositionBottom
        lXAxisStyle.XGridTextStyle.Box = True
        lXAxisStyle.XGridTextStyle.BoxFillWithBackgroundColor = True
        lXAxisStyle.XGridTextStyle.BoxStyle = LineStyles.LineInvisible
        lXAxisStyle.XGridTextStyle.Color = 13684944
        lXAxisStyle.YScaleQuantum = 0.0001
        lXAxisStyle.BackGradientFillColors = {0, 0}

        lXAxisStyle.XCursorTextStyle.BoxFillWithBackgroundColor = True
        lXAxisStyle.XCursorTextStyle.BoxStyle = LineStyles.LineInvisible
        lXAxisStyle.XCursorTextStyle.BoxThickness = 0
        lXAxisStyle.XCursorTextStyle.Color = 255

        Dim lFont As New stdole.StdFont
        lFont.Name = "Courier New"
        lFont.Bold = True
        lFont.Italic = False
        lFont.Size = 8.25@
        lFont.Strikethrough = False
        lFont.Underline = False
        lXAxisStyle.XCursorTextStyle.Font = lFont

        Dim lYAxisStyle As ChartRegionStyle
        lYAxisStyle = ChartSkil.GetDefaultChartYAxisRegionStyle.clone
        lYAxisStyle.BackGradientFillColors = {0, 0}
        lYAxisStyle.YCursorTextStyle.Font = lFont
        lYAxisStyle.YGridTextPosition = YGridTextPositions.YGridTextPositionLeft
        lYAxisStyle.YGridTextStyle.Box = True
        lYAxisStyle.YGridTextStyle.BoxFillWithBackgroundColor = True
        lYAxisStyle.YGridTextStyle.BoxStyle = LineStyles.LineInvisible
        lYAxisStyle.YGridTextStyle.Color = 13684944
        lYAxisStyle.YCursorTextStyle.BoxFillWithBackgroundColor = True
        lYAxisStyle.YCursorTextStyle.BoxStyle = LineStyles.LineInvisible
        lYAxisStyle.YCursorTextStyle.BoxThickness = 0
        lYAxisStyle.YCursorTextStyle.Color = 255

        CreateChartStyle = ChartSkil.ChartStylesManager.Add("Black",
                                ChartSkil.ChartStylesManager.DefaultStyle,
                                lDefaultRegionStyle,
                                lXAxisStyle,
                                lYAxisStyle.clone,
                                lCrosshairsLineStyle,
                                pTemporary:=True)

        CreateChartStyle.Autoscrolling = True
        CreateChartStyle.ChartBackColor = 2105376
        CreateChartStyle.HorizontalMouseScrollingAllowed = True
        CreateChartStyle.HorizontalScrollBarVisible = False
        CreateChartStyle.VerticalMouseScrollingAllowed = True
        CreateChartStyle.XAxisVisible = True
        CreateChartStyle.YAxisVisible = True
    End Function

    Public Sub Log(ByRef pMsg As String, ByRef pModName As String, ByRef pProcName As String, Optional ByRef pMsgQualifier As String = vbNullString, Optional pLogLevel As TWUtilities40.LogLevels = TWUtilities40.LogLevels.LogLevelNormal)
        Static sLogger As TWUtilities40.FormattingLogger
        If sLogger Is Nothing Then sLogger = TWUtilities.CreateFormattingLogger("strategyhost", NameOf(StrategyHost))

        sLogger.Log(pMsg, pProcName, pModName, pLogLevel, pMsgQualifier)
    End Sub

#End Region

#Region "Helper Functions"

    Private Function getDotNetBorderStyle(a As AppearanceSettings, b As BorderStyleSettings) As BorderStyle
        If a = AppearanceSettings.Appearance3d Then
            If b = BorderStyleSettings.BorderStyleNone Then Return BorderStyle.None
            If b = BorderStyleSettings.BorderStyleSingle Then Return BorderStyle.Fixed3D
        Else
            If b = BorderStyleSettings.BorderStyleNone Then Return BorderStyle.None
            If b = BorderStyleSettings.BorderStyleSingle Then Return BorderStyle.FixedSingle
        End If
    End Function

    Private Function getDotNetFont(font As stdole.Font) As Font
        If font Is Nothing Then Return Nothing

        Dim style = New FontStyle
        If font.Bold Then style = FontStyle.Bold
        If font.Italic Then style = style Or FontStyle.Italic
        If font.Strikethrough Then style = style Or FontStyle.Strikeout
        If font.Underline Then style = style Or FontStyle.Underline

        Return New Font(New FontFamily(font.Name), font.Size, style, GraphicsUnit.Point)
    End Function

#End Region

End Module