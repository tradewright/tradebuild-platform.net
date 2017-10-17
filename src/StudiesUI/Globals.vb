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
Imports TradeWright.Trading.Utils.Charts
Imports TWUtilities40

Module Globals

    '

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

    Public Const MinDouble As Double = -(2 - 2 ^ -52) * 2 ^ 1023
    Public Const MaxDouble As Double = (2 - 2 ^ -52) * 2 ^ 1023

    Public Const LB_SETHORZEXTENT As Integer = &H194

    Public Const TaskTypeStartStudy As Integer = 1
    Public Const TaskTypeReplayBars As Integer = 2
    Public Const TaskTypeAddValueListener As Integer = 3

    Public Const ErroredFieldColor As Integer = &HD0CAFA

    Public Const PositiveChangeBackColor As Integer = &HB7E43
    Public Const NegativeChangebackColor As Integer = &H4444EB

    Public Const PositiveProfitColor As Integer = &HB7E43
    Public Const NegativeProfitColor As Integer = &H4444EB

    Public Const IncreasedValueColor As Integer = &HB7E43
    Public Const DecreasedValueColor As Integer = &H4444EB

    Public Const BarModeBar As String = "Bars"
    Public Const BarModeCandle As String = "Candles"
    Public Const BarModeSolidCandle As String = "Solid candles"
    Public Const BarModeLine As String = "Line"

    Public Const BarStyleNarrow As String = "Narrow"
    Public Const BarStyleMedium As String = "Medium"
    Public Const BarStyleWide As String = "Wide"

    Public Const BarWidthNarrow As Single = 0.3
    Public Const BarWidthMedium As Single = 0.6
    Public Const BarWidthWide As Single = 0.9

    Public Const HistogramStyleNarrow As String = "Narrow"
    Public Const HistogramStyleMedium As String = "Medium"
    Public Const HistogramStyleWide As String = "Wide"

    Public Const HistogramWidthNarrow As Single = 0.3
    Public Const HistogramWidthMedium As Single = 0.6
    Public Const HistogramWidthWide As Single = 0.9

    Public Const LineDisplayModePlain As String = "Plain"
    Public Const LineDisplayModeArrowEnd As String = "End arrow"
    Public Const LineDisplayModeArrowStart As String = "Start arrow"
    Public Const LineDisplayModeArrowBoth As String = "Both arrows"

    Public Const LineStyleSolid As String = "Solid"
    Public Const LineStyleDash As String = "Dash"
    Public Const LineStyleDot As String = "Dot"
    Public Const LineStyleDashDot As String = "Dash dot"
    Public Const LineStyleDashDotDot As String = "Dash dot dot"
    Public Const LineStyleInsideSolid As String = "Inside solid"
    Public Const LineStyleInvisible As String = "Invisible"

    Public Const PointDisplayModeLine As String = "Line"
    Public Const PointDisplayModePoint As String = "Point"
    Public Const PointDisplayModeSteppedLine As String = "Stepped line"
    Public Const PointDisplayModeHistogram As String = "Histogram"

    Public Const PointStyleRound As String = "Round"
    Public Const PointStyleSquare As String = "Square"

    Public Const TextDisplayModePlain As String = "Plain"
    Public Const TextDisplayModeWIthBackground As String = "With background"
    Public Const TextDisplayModeWithBox As String = "With box"
    Public Const TextDisplayModeWithFilledBox As String = "With filled box"

    Public Const CustomStyle As String = "(Custom)"
    Public Const CustomDisplayMode As String = "(Custom)"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Friend ChartSkil As New ChartSkil27.ChartSkil
    Friend TWUtilities As New TWUtilities40.TWUtilities
    Friend StudyUtils As New StudyUtils27.StudyUtils

    'Private mDefaultStudyConfigurations As Collection

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

    'Public Function chooseAColor(initialColor As Color, allowNull As Boolean, location As System.Drawing.Point) As Color
    '    Dim simpleColorPicker As New fSimpleColorPicker

    '    simpleColorPicker.Top = location.X
    '    simpleColorPicker.Left = location.Y
    '    simpleColorPicker.initialColor = initialColor
    '    If allowNull Then simpleColorPicker.NoColorButton.Enabled = True
    '    simpleColorPicker.ShowDialog()
    '    chooseAColor = simpleColorPicker.selectedColor
    '    simpleColorPicker.Close()
    'End Function

    'Public Function gGetBottomCentre(ctrl As Control) As System.Drawing.Point
    '    Return New System.Drawing.Point(ctrl.TopLevelControl.Location.X + ctrl.Location.X + ctrl.Size.Width / 2, ctrl.TopLevelControl.Location.Y + ctrl.Location.Y - ctrl.Size.Height / 2)
    'End Function

    Public Function gLineStyleToString(value As LineStyles) As String
        Select Case value
            Case LineStyles.LineSolid
                gLineStyleToString = LineStyleSolid
            Case LineStyles.LineDash
                gLineStyleToString = LineStyleDash
            Case LineStyles.LineDot
                gLineStyleToString = LineStyleDot
            Case LineStyles.LineDashDot
                gLineStyleToString = LineStyleDashDot
            Case LineStyles.LineDashDotDot
                gLineStyleToString = LineStyleDashDotDot
            Case LineStyles.LineInvisible
                gLineStyleToString = LineStyleInvisible
            Case LineStyles.LineInsideSolid
                gLineStyleToString = LineStyleInsideSolid
            Case Else
                gLineStyleToString = LineStyleSolid
        End Select
    End Function

    Public ReadOnly Property gLogger() As Logger
        Get
            Static lLogger As Logger
            If lLogger Is Nothing Then lLogger = TWUtilities.GetLogger("log")
            Return lLogger
        End Get
    End Property

    Public Function gPointStyleToString(value As PointStyles) As String
        Select Case value
            Case PointStyles.PointRound
                gPointStyleToString = PointStyleRound
            Case PointStyles.PointSquare
                gPointStyleToString = PointStyleSquare
            Case Else
                gPointStyleToString = PointStyleRound
        End Select
    End Function

    Public Sub notImplemented()
        MsgBox("This facility has not yet been implemented", , "Sorry")
    End Sub

#End Region

#Region "Helper Functions"

#End Region

End Module