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

Imports System.ComponentModel

Imports TWUtilities40

Imports TradeWright.Trading.Utils.Contracts

<defaultpropertyattribute("SecType")>
Public Class ContractFilter

    <BrowsableAttribute(False)> _
    Event Changed(sender As Object, ev As EventArgs)

    Private Const ConfigSettingCurrency As String = "&Currency"
    Private Const ConfigSettingExchange As String = "&Exchange"
    Private Const ConfigSettingExpiry As String = "&Expiry"
    Private Const ConfigSettingRight As String = "&Right"
    Private Const ConfigSettingMultiplier As String = "&Multiplier"
    Private Const ConfigSettingSecType As String = "&SecType"
    Private Const ConfigSettingStrikePrice As String = "&StrikePrice"
    Private Const ConfigSettingSymbol As String = "&Symbol"

    Private mSymbol As String
    Private mSecType As SecurityType
    Private mExchange As String
    Private mCurrency As String
    Private mExpiry As String
    Private mStrikePrice As Double
    Private mRight As OptionRight
    Private mMultiplier As Double

    Private mDirty As Boolean

    Private mConfig As ConfigurationSection

    <BrowsableAttribute(False)> _
    Public ReadOnly Property Dirty() As Boolean
        Get
            Return mDirty
        End Get
    End Property

    <BrowsableAttribute(False)> _
    Public WriteOnly Property ConfigurationSection() As ConfigurationSection
        Set
            If value Is mConfig Then Exit Property
            mConfig = value
            storeSettings()
        End Set
    End Property

    <TypeConverter(GetType(CurrencyCodeConverter)),
    CategoryAttribute("Behavior"),
    DescriptionAttribute("Specifies the currency code for contracts that satisfy this filter.")> _
    Public Property Currency() As String
        Get
            Return mCurrency
        End Get
        Set
            If mCurrency <> value Then
                mCurrency = value
                change()
            End If
        End Set
    End Property

    <TypeConverter(GetType(ExchangeConverter)),
    CategoryAttribute("Behavior"),
    DescriptionAttribute("Specifies the exchange for contracts that satisfy this filter.")> _
    Public Property Exchange() As String
        Get
            Return mExchange
        End Get
        Set
            If mExchange <> value Then
                mExchange = value
                change()
            End If
        End Set
    End Property

    <CategoryAttribute("Behavior"),
    DescriptionAttribute("Specifies the expiry date for contracts that satisfy this filter (format YYYYMM or YYYYMMDD).")> _
    Public Property Expiry() As String
        Get
            Return mExpiry
        End Get
        Set
            If mExpiry <> value Then
                If Not Contract.IsValidExpiry(Value) Then Throw New ArgumentException("Expiry format must be YYYYMM or YYYYMMDD")
                mExpiry = value
                change()
            End If
        End Set
    End Property

    <CategoryAttribute("Behavior"),
    DescriptionAttribute("Specifies the multiplier for contracts that satisfy this filter."),
    DisplayNameAttribute("Multiplier")>
    Public Property Multiplier() As Double
        Get
            Return mMultiplier
        End Get
        Set
            If mMultiplier <> value Then
                mMultiplier = value
                change()
            End If
        End Set
    End Property

    <TypeConverter(GetType(OptionRightConverter)),
    CategoryAttribute("Behavior"),
    DescriptionAttribute("Specifies the option right for contracts that satisfy this filter.")>
    Public Property Right() As OptionRight
        Get
            Return mRight
        End Get
        Set
            If mRight <> value Then
                mRight = value
                change()
            End If
        End Set
    End Property

    <TypeConverter(GetType(SecTypeConverter)),
    CategoryAttribute("Behavior"),
    DescriptionAttribute("Specifies the security type for contracts that satisfy this filter."),
    DisplayNameAttribute("Security Type")>
    Public Property SecType() As SecurityType
        Get
            Return mSecType
        End Get
        Set
            If mSecType <> value Then
                mSecType = value
                change()
            End If
        End Set
    End Property

    <CategoryAttribute("Behavior"),
    DescriptionAttribute("Specifies the strike price for contracts that satisfy this filter."),
    DisplayNameAttribute("Strike Price")>
    Public Property StrikePrice() As Double
        Get
            Return mStrikePrice
        End Get
        Set
            If mStrikePrice <> value Then
                mStrikePrice = value
                change()
            End If
        End Set
    End Property

    <CategoryAttribute("Behavior"),
    DescriptionAttribute("Specifies the underlying symbol for contracts that satisfy this filter.")> _
    Public Property Symbol() As String
        Get
            Return mSymbol
        End Get
        Set
            If mSymbol <> value Then
                mSymbol = value
                change()
            End If
        End Set
    End Property

    Public Function Clone() As ContractFilter
        Return DirectCast(Me.MemberwiseClone, ContractFilter)
    End Function

    Public Sub LoadFromConfig( _
                    config As ConfigurationSection)
        mConfig = config
        If mConfig Is Nothing Then Exit Sub

        mCurrency = mConfig.GetSetting(ConfigSettingCurrency)
        mExchange = mConfig.GetSetting(ConfigSettingExchange)
        mExpiry = mConfig.GetSetting(ConfigSettingExpiry)
        mRight = Contract.OptionRightFromString(mConfig.GetSetting(ConfigSettingRight))
        mSecType = Contract.SecTypeFromString(mConfig.GetSetting(ConfigSettingSecType))
        mStrikePrice = CDbl(mConfig.GetSetting(ConfigSettingStrikePrice))
        mSymbol = mConfig.GetSetting(ConfigSettingSymbol)
    End Sub

    Public Sub RemoveFromConfig()
        If Not mConfig Is Nothing Then mConfig.Remove()
    End Sub

    Private Sub change()
        mDirty = True
        storeSettings()
        RaiseEvent Changed(Me, EventArgs.Empty)
    End Sub

    Private Sub storeSettings()
        If mConfig Is Nothing Then Exit Sub
        mConfig.SetSetting(ConfigSettingCurrency, mCurrency)
        mConfig.SetSetting(ConfigSettingExchange, mExchange)
        mConfig.SetSetting(ConfigSettingExpiry, mExpiry)
        mConfig.SetSetting(ConfigSettingRight, Contract.OptionRightToString(mRight))
        mConfig.SetSetting(ConfigSettingSecType, Contract.SecTypeToShortString(mSecType))
        mConfig.SetSetting(ConfigSettingStrikePrice, CStr(mStrikePrice))
        mConfig.SetSetting(ConfigSettingSymbol, mSymbol)
    End Sub


End Class
