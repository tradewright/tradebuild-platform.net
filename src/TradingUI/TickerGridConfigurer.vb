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

Imports AxTradingUI27
Imports System.ComponentModel
Imports TWControls40
Imports TWUtilities40

Public Class TickerGridConfigurer

    Private Const ConfigSettingAllowUserReordering As String = "&AllowUserReordering"
    Private Const ConfigSettingAllowUserResizing As String = "&AllowUserResizing"
    Private Const ConfigSettingBackColorBkg As String = "&BackColorBkg"
    Private Const ConfigSettingBackColorFixed As String = "&BackColorFixed"
    Private Const ConfigSettingDecreasedValueColor As String = "&DecreasedValueColor"
    Private Const ConfigSettingFont As String = "&Font"
    Private Const ConfigSettingFontFixed As String = "&FontFixed"
    Private Const ConfigSettingForeColor As String = "&ForeColor"
    Private Const ConfigSettingForeColorFixed As String = "&ForeColorFixed"
    Private Const ConfigSettingGridColor As String = "&GridColor"
    Private Const ConfigSettingGridColorFixed As String = "&GridColorFixed"
    Private Const ConfigSettingGridLineWidth As String = "&GridLineWidth"
    Private Const ConfigSettingIncreasedValueColor As String = "&IncreasedValueColor"
    Private Const ConfigSettingNegativeChangeBackColor As String = "&NegativeChangeBackColor"
    Private Const ConfigSettingNegativeChangeForeColor As String = "&NegativeChangeForeColor"
    Private Const ConfigSettingPositiveChangeBackColor As String = "&PositiveChangeBackColor"
    Private Const ConfigSettingPositiveChangeForeColor As String = "&PositiveChangeForeColor"
    Private Const ConfigSettingRowBackColorOdd As String = "&RowBackColorOdd"
    Private Const ConfigSettingRowBackColorEven As String = "&RowBackColorEven"
    Private Const ConfigSettingRowHeightMin As String = "&RowHeightMin"
    Private Const ConfigSettingRowSizingMode As String = "&RowSizingMode"
    Private Const ConfigSettingScrollbars As String = "&Scrollbars"

    Private mTickerGrid As AxTickerGrid
    Private mConfig As ConfigurationSection

    Public Sub New(tickerGrid As AxTickerGrid)
        mTickerGrid = tickerGrid
    End Sub

    <Browsable(False)>
    Public WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            mConfig = value
            storeSettings()
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether scroll bars are to be provided.")>
    Public Property ScrollBars() As ScrollBarsSettings
        Get
            Return mTickerGrid.ScrollBars
        End Get
        Set
            mTickerGrid.ScrollBars = Value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies whether resizing a row affects only that row or all rows.")>
    Public Property RowSizingMode() As RowSizingSettings
        Get
            Return mTickerGrid.RowSizingMode
        End Get
        Set
            mTickerGrid.RowSizingMode = Value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Specifies the minimum height to which a row can be resized by the user.")>
    Public Property RowHeightMin() As Integer
        Get
            Return mTickerGrid.RowHeightMin
        End Get
        Set
            mTickerGrid.RowHeightMin = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the background color of the even-numbered non-fixed rows.")>
    Public Property RowBackColorEven() As System.Drawing.Color
        Get
            Return mTickerGrid.RowBackColorEven
        End Get
        Set
            mTickerGrid.RowBackColorEven = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the background color of the odd-numbered non-fixed rows.")>
    Public Property RowBackColorOdd() As System.Drawing.Color
        Get
            Return mTickerGrid.RowBackColorOdd
        End Get
        Set
            mTickerGrid.RowBackColorOdd = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the background color for price change cells when the price has increased.")>
    Public Property PositiveChangeBackColor() As System.Drawing.Color
        Get
            Return mTickerGrid.PositiveChangeBackColor
        End Get
        Set
            mTickerGrid.PositiveChangeBackColor = Value
        End Set
    End Property


    <Category("Appearance"), Browsable(True),
    Description("Specifies the foreground color for price change cells when the price has increased.")>
    Public Property PositiveChangeForeColor() As System.Drawing.Color
        Get
            Return mTickerGrid.PositiveChangeForeColor
        End Get
        Set
            mTickerGrid.PositiveChangeForeColor = Value
        End Set
    End Property


    <Category("Appearance"), Browsable(True),
    Description("Specifies the background color for price change cells when the price has decreased.")>
    Public Property NegativeChangeBackColor() As System.Drawing.Color
        Get
            Return mTickerGrid.NegativeChangeBackColor
        End Get
        Set
            mTickerGrid.NegativeChangeBackColor = Value
        End Set
    End Property


    <Category("Appearance"), Browsable(True),
    Description("Specifies the foreground color for price change cells when the price has decreased.")>
    Public Property NegativeChangeForeColor() As System.Drawing.Color
        Get
            Return mTickerGrid.NegativeChangeForeColor
        End Get
        Set
            mTickerGrid.NegativeChangeForeColor = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the foreground color for price cells that have increased in value.")>
    Public Property IncreasedValueColor() As System.Drawing.Color
        Get
            Return mTickerGrid.IncreasedValueColor
        End Get
        Set
            mTickerGrid.IncreasedValueColor = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the thickness of the grid lines.")>
    Public Property GridLineWidth() As Integer
        Get
            Return mTickerGrid.GridLineWidth
        End Get
        Set
            mTickerGrid.GridLineWidth = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the color of the header grid lines.")>
    Public Property GridColorFixed() As System.Drawing.Color
        Get
            Return mTickerGrid.GridColorFixed
        End Get
        Set
            mTickerGrid.GridColorFixed = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the color of the grid lines.")>
    Public Property GridColor() As System.Drawing.Color
        Get
            Return mTickerGrid.GridColor
        End Get
        Set
            mTickerGrid.GridColor = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the foreground color for header cells.")>
    Public Property ForeColorFixed() As System.Drawing.Color
        Get
            Return mTickerGrid.ForeColorFixed
        End Get
        Set
            mTickerGrid.ForeColorFixed = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the foreground color for non-header cells.")>
    Public Property ForeColor() As System.Drawing.Color
        Get
            Return mTickerGrid.ForeColor
        End Get
        Set
            mTickerGrid.ForeColor = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the font to be used for header cells.")>
    Public Property FontFixed() As System.Drawing.Font
        Get
            Return mTickerGrid.FontFixed
        End Get
        Set
            mTickerGrid.FontFixed = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the font to be used for non-header cells.")>
    Public Property Font() As System.Drawing.Font
        Get
            Return mTickerGrid.Font
        End Get
        Set
            mTickerGrid.Font = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the foreground color for price cells that have decreased in value.")>
    Public Property DecreasedValueColor() As System.Drawing.Color
        Get
            Return mTickerGrid.DecreasedValueColor
        End Get
        Set
            mTickerGrid.DecreasedValueColor = Value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the background color of the fixed cells (ie row and column headers).")>
    Public Property BackColorFixed() As System.Drawing.Color
        Get
            Return mTickerGrid.BackColorFixed
        End Get
        Set
            mTickerGrid.BackColorFixed = value
        End Set
    End Property

    <Category("Appearance"), Browsable(True),
    Description("Specifies the color of the area behind the grid cells.")>
    Public Property BackColorBkg() As System.Drawing.Color
        Get
            Return mTickerGrid.BackColorBkg
        End Get
        Set
            mTickerGrid.BackColorBkg = value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Indicates whether the user can change the size of the rows and/or columns.")>
    Public Property AllowUserResizing() As AllowUserResizeSettings
        Get
            Return mTickerGrid.AllowUserResizing
        End Get
        Set
            mTickerGrid.AllowUserResizing = Value
        End Set
    End Property

    <Category("Behavior"), Browsable(True),
    Description("Indicates whether the user can change the order of the rows and/or columns.")>
    Public Property AllowUserReordering() As AllowUserReorderSettings
        Get
            Return mTickerGrid.AllowUserReordering
        End Get
        Set
            mTickerGrid.AllowUserReordering = value
        End Set
    End Property

    Public Sub LoadFromConfig(config As ConfigurationSection)
        mConfig = config
        mTickerGrid.Redraw = False

        If mConfig.GetSetting(ConfigSettingAllowUserReordering) <> "" Then mTickerGrid.AllowUserReordering = CType(mConfig.GetSetting(ConfigSettingAllowUserReordering), TWControls40.AllowUserReorderSettings)
        If mConfig.GetSetting(ConfigSettingAllowUserResizing) <> "" Then mTickerGrid.AllowUserResizing = CType(mConfig.GetSetting(ConfigSettingAllowUserResizing), TWControls40.AllowUserResizeSettings)
        If mConfig.GetSetting(ConfigSettingBackColorBkg) <> "" Then mTickerGrid.BackColorBkg = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingBackColorBkg)), Color)
        If mConfig.GetSetting(ConfigSettingBackColorFixed) <> "" Then mTickerGrid.BackColorFixed = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingBackColorFixed)), Color)
        If mConfig.GetSetting(ConfigSettingDecreasedValueColor) <> "" Then mTickerGrid.DecreasedValueColor = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingDecreasedValueColor)), Color)
        If mConfig.GetSetting(ConfigSettingFont) <> "" Then mTickerGrid.Font = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Font)).ConvertFromString(mConfig.GetSetting(ConfigSettingFont)), Font)
        If mConfig.GetSetting(ConfigSettingFontFixed) <> "" Then mTickerGrid.FontFixed = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Font)).ConvertFromString(mConfig.GetSetting(ConfigSettingFontFixed)), Font)
        If mConfig.GetSetting(ConfigSettingForeColor) <> "" Then mTickerGrid.ForeColor = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingForeColor)), Color)
        If mConfig.GetSetting(ConfigSettingForeColorFixed) <> "" Then mTickerGrid.ForeColorFixed = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingForeColorFixed)), Color)
        If mConfig.GetSetting(ConfigSettingGridColor) <> "" Then mTickerGrid.GridColor = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingGridColor)), Color)
        If mConfig.GetSetting(ConfigSettingGridColorFixed) <> "" Then mTickerGrid.GridColorFixed = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingGridColorFixed)), Color)
        If mConfig.GetSetting(ConfigSettingGridLineWidth) <> "" Then mTickerGrid.GridLineWidth = CInt(mConfig.GetSetting(ConfigSettingGridLineWidth))
        If mConfig.GetSetting(ConfigSettingIncreasedValueColor) <> "" Then mTickerGrid.IncreasedValueColor = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingIncreasedValueColor)), Color)
        If mConfig.GetSetting(ConfigSettingNegativeChangeBackColor) <> "" Then mTickerGrid.NegativeChangeBackColor = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingNegativeChangeBackColor)), Color)
        If mConfig.GetSetting(ConfigSettingNegativeChangeForeColor) <> "" Then mTickerGrid.NegativeChangeForeColor = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingNegativeChangeForeColor)), Color)
        If mConfig.GetSetting(ConfigSettingPositiveChangeBackColor) <> "" Then mTickerGrid.PositiveChangeBackColor = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingPositiveChangeBackColor)), Color)
        If mConfig.GetSetting(ConfigSettingPositiveChangeForeColor) <> "" Then mTickerGrid.PositiveChangeForeColor = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingPositiveChangeForeColor)), Color)
        If mConfig.GetSetting(ConfigSettingRowBackColorOdd) <> "" Then mTickerGrid.RowBackColorOdd = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingRowBackColorOdd)), Color)
        If mConfig.GetSetting(ConfigSettingRowBackColorEven) <> "" Then mTickerGrid.RowBackColorEven = CType(TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(mConfig.GetSetting(ConfigSettingRowBackColorEven)), Color)
        If mConfig.GetSetting(ConfigSettingRowHeightMin) <> "" Then mTickerGrid.RowHeightMin = CInt(mConfig.GetSetting(ConfigSettingRowHeightMin))
        If mConfig.GetSetting(ConfigSettingRowSizingMode) <> "" Then mTickerGrid.RowSizingMode = CType(mConfig.GetSetting(ConfigSettingRowSizingMode), TWControls40.RowSizingSettings)
        If mConfig.GetSetting(ConfigSettingScrollbars) <> "" Then mTickerGrid.ScrollBars = CType(mConfig.GetSetting(ConfigSettingScrollbars), TWControls40.ScrollBarsSettings)

        mTickerGrid.Redraw = True
    End Sub

    Public Sub RemoveFromConfig()
        mConfig.remove()
        mConfig = Nothing
    End Sub

    Private Sub storeSettings()
        mConfig.SetSetting(ConfigSettingAllowUserReordering, CStr(mTickerGrid.AllowUserReordering))
        mConfig.SetSetting(ConfigSettingAllowUserResizing, CStr(mTickerGrid.AllowUserResizing))
        mConfig.SetSetting(ConfigSettingBackColorBkg, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.BackColorBkg))
        mConfig.SetSetting(ConfigSettingBackColorFixed, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.BackColorFixed))
        mConfig.SetSetting(ConfigSettingDecreasedValueColor, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.DecreasedValueColor))
        mConfig.SetSetting(ConfigSettingFont, TypeDescriptor.GetConverter(GetType(System.Drawing.Font)).ConvertToString(mTickerGrid.Font))
        mConfig.SetSetting(ConfigSettingFontFixed, TypeDescriptor.GetConverter(GetType(System.Drawing.Font)).ConvertToString(mTickerGrid.FontFixed))
        mConfig.SetSetting(ConfigSettingForeColor, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.ForeColor))
        mConfig.SetSetting(ConfigSettingForeColorFixed, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.ForeColorFixed))
        mConfig.SetSetting(ConfigSettingGridColor, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.GridColor))
        mConfig.SetSetting(ConfigSettingGridColorFixed, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.GridColorFixed))
        mConfig.SetSetting(ConfigSettingGridLineWidth, CStr(mTickerGrid.GridLineWidth))
        mConfig.SetSetting(ConfigSettingIncreasedValueColor, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.IncreasedValueColor))
        mConfig.SetSetting(ConfigSettingNegativeChangeBackColor, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.NegativeChangeBackColor))
        mConfig.SetSetting(ConfigSettingNegativeChangeForeColor, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.NegativeChangeForeColor))
        mConfig.SetSetting(ConfigSettingPositiveChangeBackColor, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.PositiveChangeBackColor))
        mConfig.SetSetting(ConfigSettingPositiveChangeForeColor, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.PositiveChangeForeColor))
        mConfig.SetSetting(ConfigSettingRowBackColorOdd, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.RowBackColorOdd))
        mConfig.SetSetting(ConfigSettingRowBackColorEven, TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertToString(mTickerGrid.RowBackColorEven))
        mConfig.SetSetting(ConfigSettingRowHeightMin, CStr(mTickerGrid.RowHeightMin))
        mConfig.SetSetting(ConfigSettingRowSizingMode, CStr(mTickerGrid.RowSizingMode))
        mConfig.SetSetting(ConfigSettingScrollbars, CStr(mTickerGrid.ScrollBars))
    End Sub
End Class
