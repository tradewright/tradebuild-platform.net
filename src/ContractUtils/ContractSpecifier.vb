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

Imports System.Runtime.Serialization

Imports ContractUtils27
Imports TWUtilities40

Namespace Contracts
    <DataContract>
    Public NotInheritable Class ContractSpecifier
        Implements IContractSpecifier
        Implements ContractUtils27.IContractSpecifier

#Region "Description"
        
        '
#End Region

#Region "Amendment history"
        
        '
        '
        '

#End Region

#Region "Interfaces"


#End Region

#Region "Events"
        
#End Region

#Region "Constants"

        Friend Const ConfigSettingContractSpecCurrency As String = "&Currency"
        Friend Const ConfigSettingContractSpecExpiry As String = "&Expiry"
        Friend Const ConfigSettingContractSpecExchange As String = "&Exchange"
        Friend Const ConfigSettingContractSpecLocalSymbol As String = "&LocalSymbol"
        Friend Const ConfigSettingContractSpecMultiplier As String = "&Multiplier"
        Friend Const ConfigSettingContractSpecRight As String = "&Right"
        Friend Const ConfigSettingContractSpecSecType As String = "&SecType"
        Friend Const ConfigSettingContractSpecStrikePrice As String = "&StrikePrice"
        Friend Const ConfigSettingContractSpecSymbol As String = "&Symbol"

#End Region

#Region "Enums"
        
#End Region

#Region "Types"
        
#End Region

#Region "Member variables"

        Private mSymbol As String
        Private mSecType As SecurityType
        Private mExpiry As String
        Private mMultiplier As Double
        Private mStrike As Double
        Private mRight As OptionRight
        Private mExchange As String
        Private mCurrency As String
        Private mLocalSymbol As String
        Private mComboLegs As IComboLegs

        'Private mComContractSpec As ContractUtils27._IContractSpecifier

#End Region

#Region "Constructors"

        ' need to allow the ComboContractSpecBuilder to create an empty ContractSpecifier
        Friend Sub New()
        End Sub

        Public Sub New(Optional localSymbol As String = "",
                       Optional symbol As String = "",
                       Optional exchange As String = "",
                       Optional secType As SecurityType = SecurityType.None,
                       Optional currencyCode As String = "",
                       Optional expiry As String = "",
                       Optional multiplier As Double = 1.0,
                       Optional strike As Double = 0,
                       Optional right As OptionRight = OptionRight.None)
            If localSymbol = "" And symbol = "" Then Throw New ArgumentException("Symbol must be supplied if LocalSymbol is not supplied")
            If exchange <> "" AndAlso Not Utils.Contracts.Exchange.IsValidExchangeCode(exchange) Then Throw New ArgumentException(String.Format($"'{exchange}' is not a valid Exchange code"))
            If expiry <> "" AndAlso Not Contract.IsValidExpiry(expiry) Then Throw New ArgumentException($"'{expiry}' is not a valid Expiry format")

            Select Case secType
                Case 0 ' ie not supplied
                Case SecurityType.Stock
                Case SecurityType.Future
                Case SecurityType.Option, SecurityType.FuturesOption
                    If Not strike >= 0 Then Throw New ArgumentException("Strike must be >= 0")
                    Select Case right
                        Case OptionRight.Call
                        Case OptionRight.Put
                        Case OptionRight.None
                        Case Else
                            Throw New ArgumentException($"'{right}' is not a valid option Right")
                    End Select
                Case SecurityType.Cash
                Case SecurityType.Combo
                    Throw New ArgumentException("Can't create a combo contract specifier with this constructor")
                Case SecurityType.Index
                Case Else
                    Throw New ArgumentException($"'{secType}' is not a valid secType")
            End Select

            mLocalSymbol = localSymbol
            mSymbol = symbol
            mExchange = exchange
            mSecType = secType
            mCurrency = currencyCode
            mExpiry = expiry
            mMultiplier = multiplier
            mStrike = strike
            mRight = right
        End Sub

        Public Sub New(specifierElement As XElement)
            Me.New(specifierElement.@localsymbol,
                specifierElement.@symbol,
                specifierElement.@exchange,
                CType([Enum].Parse(GetType(SecurityType), specifierElement.@sectype), SecurityType),
                specifierElement.@currencycode,
                specifierElement.@expiry,
                Double.Parse(specifierElement.@strike),
                CType([Enum].Parse(GetType(OptionRight), specifierElement.@right), OptionRight))
        End Sub

        Public Sub New(pConfig As ConfigurationSection)
            Me.New(pConfig.GetSetting(ConfigSettingContractSpecLocalSymbol, ""),
                   pConfig.GetSetting(ConfigSettingContractSpecSymbol, ""),
                   pConfig.GetSetting(ConfigSettingContractSpecExchange, ""),
                   Contract.SecTypeFromString(pConfig.GetSetting(ConfigSettingContractSpecSecType, "")),
                   pConfig.GetSetting(ConfigSettingContractSpecCurrency, ""),
                   pConfig.GetSetting(ConfigSettingContractSpecExpiry, ""),
                   CDbl(pConfig.GetSetting(ConfigSettingContractSpecStrikePrice, "0.0")),
                   Contract.OptionRightFromString(pConfig.GetSetting(ConfigSettingContractSpecRight, "")))
        End Sub

        Public Shared Function FromCOM(comContractSpec As ContractUtils27._IContractSpecifier) As ContractSpecifier
            Dim specifier = New ContractSpecifier()
            'specifier.ComContractSpecifier = comContractSpec
            With comContractSpec
                specifier.LocalSymbol = .LocalSymbol
                specifier.Symbol = .Symbol
                specifier.Exchange = .Exchange
                specifier.SecType = CType(.SecType, SecurityType)
                specifier.CurrencyCode = .CurrencyCode
                specifier.Expiry = .Expiry
                specifier.Multiplier = .Multiplier
                specifier.Strike = .Strike
                specifier.Right = CType(.Right, OptionRight)
            End With
            Return specifier
        End Function

#End Region

#Region "Properties"

        <DataMember>
        Public Property ComboLegs() As IComboLegs Implements IContractSpecifier.ComboLegs
            Get
                Return mComboLegs
            End Get
            Friend Set(Value As IComboLegs)
                mComboLegs = Value
            End Set
        End Property

        'Friend ReadOnly Property ComContractSpecifier As ContractUtils27._IContractSpecifier
        '    Get
        '        If mComContractSpec IsNot Nothing Then
        '        ElseIf mComboLegs Is Nothing Then
        '            mComContractSpec = DirectCast(ComContractUtils.CreateContractSpecifier(mLocalSymbol,
        '                                                                       mSymbol,
        '                                                                       mExchange,
        '                                                                       CType(mSecType, SecurityTypes),
        '                                                                       mCurrency,
        '                                                                       mExpiry,
        '                                                                       mMultiplier,
        '                                                                       mStrike,
        '                                                                       CType(mRight, OptionRights)),
        '                                         ContractUtils27.IContractSpecifier)
        '        Else
        '            Dim lBuilder As New ContractUtils27.ComboContractSpecBldrClass()
        '            For Each leg In mComboLegs
        '                lBuilder.AddLeg(CType(leg.ContractSpec, _IContractSpecifier), leg.IsBuyLeg, leg.Ratio)
        '            Next
        '            mComContractSpec = DirectCast(lBuilder.ContractSpecifier, ContractUtils27.IContractSpecifier)
        '        End If
        '        Return mComContractSpec
        '    End Get
        'End Property

        <DataMember>
        Public Property CurrencyCode() As String Implements IContractSpecifier.CurrencyCode
            Get
                CurrencyCode = mCurrency
            End Get
            Friend Set(Value As String)
                mCurrency = UCase(Value)
            End Set
        End Property

        <DataMember>
        Public Property Exchange() As String Implements IContractSpecifier.Exchange
            Get
                Return mExchange
            End Get
            Friend Set(Value As String)
                mExchange = UCase(Value)
            End Set
        End Property

        <DataMember>
        Public Property Expiry() As String Implements IContractSpecifier.Expiry
            Get
                Return mExpiry
            End Get
            Friend Set(Value As String)
                If String.IsNullOrEmpty(Value) And Not Contract.IsValidExpiry(Value) Then Throw New ArgumentException("Invalid Expiry")
                mExpiry = Value
            End Set
        End Property

        Public ReadOnly Property Key() As String Implements IContractSpecifier.Key
            Get
                Return GetContractSpecKey(Me)
            End Get
        End Property

        <DataMember>
        Public Property LocalSymbol() As String Implements IContractSpecifier.LocalSymbol
            Get
                Return mLocalSymbol
            End Get
            Friend Set(Value As String)
                mLocalSymbol = UCase(Value)
            End Set
        End Property

        <DataMember>
        Public Property Multiplier() As Double Implements IContractSpecifier.Multiplier
            Get
                Multiplier = mMultiplier
            End Get
            Set(Value As Double)
                mMultiplier = Value
            End Set
        End Property

        <DataMember>
        Public Property Right() As OptionRight Implements IContractSpecifier.Right
            Get
                Return mRight
            End Get
            Friend Set(Value As OptionRight)
                mRight = Value
            End Set
        End Property

        <DataMember>
        Public Property SecType() As SecurityType Implements IContractSpecifier.SecType
            Get
                Return mSecType
            End Get
            Friend Set(Value As SecurityType)
                mSecType = Value
            End Set
        End Property

        <DataMember>
        Public Property Strike() As Double Implements IContractSpecifier.Strike
            Get
                Return mStrike
            End Get
            Friend Set(Value As Double)
                mStrike = Value
            End Set
        End Property

        <DataMember>
        Public Property Symbol() As String Implements IContractSpecifier.Symbol
            Get
                Symbol = mSymbol
            End Get
            Friend Set(Value As String)
                mSymbol = UCase(Value)
            End Set
        End Property

#End Region

#Region "Methods"

        Public Shared Function ContractSpecsEqual(pContractSpec1 As IContractSpecifier, pContractSpec2 As IContractSpecifier) As Boolean
            If pContractSpec1 Is Nothing Then Return False
            If pContractSpec2 Is Nothing Then Return False
            If pContractSpec1 Is pContractSpec2 Then Return True

            If pContractSpec1.CurrencyCode <> pContractSpec2.CurrencyCode Then Return False
            If pContractSpec1.Exchange <> pContractSpec2.Exchange Then Return False
            If pContractSpec1.Expiry <> pContractSpec2.Expiry Then Return False
            If pContractSpec1.LocalSymbol <> pContractSpec2.LocalSymbol Then Return False
            If pContractSpec1.Multiplier <> pContractSpec2.Multiplier Then Return False
            If pContractSpec1.Right <> pContractSpec2.Right Then Return False
            If pContractSpec1.SecType <> pContractSpec2.SecType Then Return False
            If pContractSpec1.Strike <> pContractSpec2.Strike Then Return False
            If pContractSpec1.Symbol <> pContractSpec2.Symbol Then Return False
            Return True
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Return ContractSpecsEqual(Me, CType(obj, IContractSpecifier))
        End Function

        Friend Shared Function GetContractSpecKey(pSpec As IContractSpecifier) As String
            Return $"{pSpec.LocalSymbol}|{CStr(pSpec.SecType)}|{pSpec.Symbol}|{pSpec.Expiry}|{pSpec.Strike}|{CStr(pSpec.Right)}|{pSpec.Exchange}|{pSpec.CurrencyCode}|{pSpec.Multiplier}"
        End Function

        Public Shared Sub SaveToConfig(pSpecifier As IContractSpecifier, pConfig As ConfigurationSection)
            With pConfig
                .SetSetting(ConfigSettingContractSpecLocalSymbol, pSpecifier.LocalSymbol)
                .SetSetting(ConfigSettingContractSpecSymbol, pSpecifier.Symbol)
                .SetSetting(ConfigSettingContractSpecExchange, pSpecifier.Exchange)
                .SetSetting(ConfigSettingContractSpecSecType, Contract.SecTypeToString(pSpecifier.SecType))
                .SetSetting(ConfigSettingContractSpecCurrency, pSpecifier.CurrencyCode)
                .SetSetting(ConfigSettingContractSpecExpiry, pSpecifier.Expiry)
                .SetSetting(ConfigSettingContractSpecMultiplier, CStr(pSpecifier.Multiplier))
                .SetSetting(ConfigSettingContractSpecStrikePrice, CStr(pSpecifier.Strike))
                .SetSetting(ConfigSettingContractSpecRight, Contract.OptionRightToString(pSpecifier.Right))
            End With
        End Sub

        Public Overrides Function ToString() As String
            Dim s = If(mLocalSymbol <> "", $"localsymbol={mLocalSymbol}; ", "")
            s = s & If(mSymbol <> "", $"symbol={mSymbol}; ", "")
            s = s & If(mSecType <> 0, $"sectype={Contract.SecTypeToString(mSecType)}; ", "")
            s = s & If(mExpiry <> "", $"expiry={mExpiry}; ", "")
            s = s & If(mExchange <> "", $"exchange={0}; mExchange", "")
            s = s & If(mCurrency <> "", $"currencycode={mCurrency}; ", "")
            s = s & If(mMultiplier <> 0, $"currencycode={mMultiplier}; ", "")
            Select Case mRight
                Case OptionRight.Call, OptionRight.Put
                    s = s & $"right={Contract.OptionRightToString(mRight)}; "
                    s = s & $"strike={mStrike}; "
            End Select

            If Not mComboLegs Is Nothing Then s = s & $"comboLegs={mComboLegs.ToString}"

            Return s
        End Function

        Private Function Eqquals(pContractSpecifier As _IContractSpecifier) As Boolean Implements _IContractSpecifier.Equals
            Return Equals(DirectCast(pContractSpecifier, IContractSpecifier))
        End Function

        Private Function _IContractSpecifier_ToString() As String Implements _IContractSpecifier.ToString
            Throw New NotImplementedException()
        End Function

        Private ReadOnly Property _IContractSpecifier_ComboLegs As _IComboLegs Implements _IContractSpecifier.ComboLegs
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_CurrencyCode As String Implements _IContractSpecifier.CurrencyCode
            Get
                Return mCurrency
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_Exchange As String Implements _IContractSpecifier.Exchange
            Get
                Return mExchange
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_Expiry As String Implements _IContractSpecifier.Expiry
            Get
                Return mExpiry
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_Key As String Implements _IContractSpecifier.Key
            Get
                Return Key
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_LocalSymbol As String Implements _IContractSpecifier.LocalSymbol
            Get
                Return mLocalSymbol
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_Multiplier As Double Implements _IContractSpecifier.Multiplier
            Get
                Return mMultiplier
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_Right As OptionRights Implements _IContractSpecifier.Right
            Get
                Return CType(mRight, OptionRights)
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_SecType As SecurityTypes Implements _IContractSpecifier.SecType
            Get
                Return CType(mSecType, SecurityTypes)
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_Strike As Double Implements _IContractSpecifier.Strike
            Get
                Return mStrike
            End Get
        End Property

        Private ReadOnly Property _IContractSpecifier_Symbol As String Implements _IContractSpecifier.Symbol
            Get
                Return mSymbol
            End Get
        End Property

#End Region

#Region "Helper Functions"

#End Region

End Class
End Namespace
