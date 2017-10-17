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

Public Class ChartSpecifier
    Implements ICloneable


    Private Const ConfigSettingFromTime As String = "&FromTime"
    Private Const ConfigSettingToTime As String = "&ToTime"
    Private Const ConfigSettingIncludeBarsOutsideSession As String = "&IncludeBarsOutsideSession"
    Private Const ConfigSettingInitialNumberOfBars As String = "&InitialNumberOfBars"

    Private mInitialNumberOfBars As Integer = 200
    Private mIncludeBarsOutsideSession As Boolean = False
    Private mFromTime As Date
    Private mToTime As Date
    
    Private mConfig As ConfigurationSection

    Private Sub New()
        ' only used when loading from config
    End Sub

    Public Sub New( _
                initialNumberOfBars As Integer, _
                includeBarsOutsideSession As Boolean, _
                fromTime As Date, _
                totime As Date)
        mInitialNumberOfBars = initialNumberOfBars
        mIncludeBarsOutsideSession = includeBarsOutsideSession
        mFromTime = fromTime
        mToTime = totime
    End Sub

    Public Sub New( _
                initialNumberOfBars As Integer, _
                includeBarsOutsideSession As Boolean)
        Me.New(initialNumberOfBars, includeBarsOutsideSession, Date.MinValue, Date.MinValue)
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return Me.MemberwiseClone
    End Function

    Public WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If value Is mConfig Then Exit Property
            mConfig = value
            If mConfig Is Nothing Then Exit Property

            mConfig.SetSetting(ConfigSettingFromTime, Format(mFromTime, "yyyy-MM-dd HH:mm:ss"))
            mConfig.SetSetting(ConfigSettingToTime, Format(mToTime, "yyyy-MM-dd HH:mm:ss"))
            mConfig.SetSetting(ConfigSettingInitialNumberOfBars, CStr(mInitialNumberOfBars))
            mConfig.SetBooleanSetting(ConfigSettingIncludeBarsOutsideSession, mIncludeBarsOutsideSession)
        End Set
    End Property

    Public ReadOnly Property FromTime() As Date
        Get
            Return mFromTime
        End Get
    End Property

    Public ReadOnly Property ToTime() As Date
        Get
            Return mToTime
        End Get
    End Property

    Public Property InitialNumberOfBars() As Integer
        Get
            Return mInitialNumberOfBars
        End Get
        Set
            mInitialNumberOfBars = value
            If Not mConfig Is Nothing Then mConfig.SetSetting(ConfigSettingInitialNumberOfBars, CStr(mInitialNumberOfBars))
        End Set
    End Property

    Public ReadOnly Property IncludeBarsOutsideSession() As Boolean
        Get
            Return mIncludeBarsOutsideSession
        End Get
    End Property

    Friend Shared Function LoadFromConfiguration(config As ConfigurationSection) As ChartSpecifier
        Dim chartSpec = New ChartSpecifier
        chartSpec.LoadFromConfig(config)
        Return chartSpec
    End Function

#Region "Helper Functions"

    Private Sub LoadFromConfig(config As ConfigurationSection)
        mConfig = config
        If mConfig Is Nothing Then Exit Sub

        mInitialNumberOfBars = CInt(mConfig.GetSetting(ConfigSettingInitialNumberOfBars, "500"))
        mIncludeBarsOutsideSession = CBool(mConfig.GetSetting(ConfigSettingIncludeBarsOutsideSession, "False"))
        mFromTime = CDate(mConfig.GetSetting(ConfigSettingFromTime, CStr(Date.MinValue)))
        mToTime = CDate(mConfig.GetSetting(ConfigSettingToTime, CStr(Date.MinValue)))
    End Sub

#End Region

End Class
