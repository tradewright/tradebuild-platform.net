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

Imports TWUtilities40

Public Class StudyHorizontalRule

    '

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

    Private Const ConfigSettingY As String = "&Y"
    Private Const ConfigSettingColor As String = "&Color"
    Private Const ConfigSettingThickness As String = "&Thickness"
    Private Const ConfigSettingStyle As String = "&Style"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mY As Double
    Private mColor As Integer
    Private mThickness As Integer
    Private mStyle As ChartSkil27.LineStyles

    Private mConfig As ConfigurationSection

#End Region

#Region "Constructors"

    Friend Sub New()
        MyBase.New()
        mColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
        mThickness = 1
        mStyle = ChartSkil27.LineStyles.LineSolid
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"


    Public Property color() As Integer
        Get
            color = mColor
        End Get
        Set
            mColor = Value
            storeSettings()
        End Set
    End Property


    Friend WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If Value Is mConfig Then Exit Property
            If mConfig IsNot Nothing Then mConfig.Remove()
            mConfig = Value
            If mConfig Is Nothing Then Return
            storeSettings()
        End Set
    End Property

    Public Property style() As ChartSkil27.LineStyles
        Get
            style = mStyle
        End Get
        Set
            mStyle = Value
            storeSettings()
        End Set
    End Property


    Public Property thickness() As Integer
        Get
            thickness = mThickness
        End Get
        Set
            mThickness = Value
            storeSettings()
        End Set
    End Property


    Public Property y() As Double
        Get
            y = mY
        End Get
        Set
            mY = Value
            storeSettings()
        End Set
    End Property

#End Region

#Region "Methods"

    Friend Sub LoadFromConfig(
                config As ConfigurationSection)
        mConfig = config
        mY = CType(mConfig.GetSetting(ConfigSettingY), Double)
        mColor = CType(mConfig.GetSetting(ConfigSettingColor), Integer)
        mThickness = CType(mConfig.GetSetting(ConfigSettingThickness), Integer)
        mStyle = CType(mConfig.GetSetting(ConfigSettingStyle), ChartSkil27.LineStyles)
    End Sub

    Friend Sub RemoveFromConfig()
        If Not mConfig Is Nothing Then mConfig.Remove()
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub storeSettings()
        If mConfig Is Nothing Then Exit Sub

        mConfig.SetSetting(ConfigSettingY, CType(mY, String))
        mConfig.SetSetting(ConfigSettingColor, CType(mColor, String))
        mConfig.SetSetting(ConfigSettingThickness, CType(mThickness, String))
        mConfig.SetSetting(ConfigSettingStyle, CType(mStyle, String))
    End Sub

#End Region

End Class