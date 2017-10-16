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

Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json

Imports TradeWright.Trading.Utils.Sessions

Imports ContractUtils27
Imports TWUtilities40

Namespace Contracts
    <DataContract>
    Public NotInheritable Class Contract
        Implements IContract
        Implements IJSONable
        Implements IStringable
        Implements ContractUtils27.IContract

        '@================================================================================
        ' Description
        '@================================================================================
        
        '
        '@================================================================================
        ' Amendment history
        '@================================================================================
        
        '
        '
        '

        '@================================================================================
        ' Interfaces
        '@================================================================================


        '@================================================================================
        ' Events
        '@================================================================================
        
        '@================================================================================
        ' Constants
        '@================================================================================

        Private Const ModuleName As String = "Contract"

        Friend Const ConfigSectionContractSpecifier As String = "Specifier"

        Friend Const ConfigSettingDaysBeforeExpiryToSwitch As String = "&DaysBeforeExpiryToSwitch"
        Friend Const ConfigSettingDescription As String = "&Description"
        Friend Const ConfigSettingExpiryDate As String = "&ExpiryDate"
        Friend Const ConfigSettingMultiplier As String = "&Multiplier"
        Friend Const ConfigSettingSessionEndTime As String = "&SessionEndTime"
        Friend Const ConfigSettingSessionStartTime As String = "&SessionStartTime"
        Friend Const ConfigSettingTickSize As String = "&TickSize"
        Friend Const ConfigSettingTimezoneName As String = "&Timezone"

        '@================================================================================
        ' Enums
        '@================================================================================
        
        '@================================================================================
        ' Types
        '@================================================================================
        
        '@================================================================================
        ' Member variables
        '@================================================================================

        Private mSpecifier As IContractSpecifier
        Private mTickSize As Double
        Private mNumberOfDecimals As Integer
        Private mDescription As String
        Private mSessionStartTime As TimeSpan
        Private mSessionEndTime As TimeSpan
        Private mExpiryDate As DateTime
        Private mDaysBeforeExpiryToSwitch As Integer
        Private mProviderIDs As Parameters
        Private mTimezoneName As String

        'Private mComContract As ContractUtils27._IContract

        '@================================================================================
        ' Constructors
        '@================================================================================

        Private Sub New()
        End Sub

        Public Sub New(xmlString As String)
            FromXML(xmlString)
        End Sub

        Public Sub New(pConfig As ConfigurationSection)
            mSpecifier = New ContractSpecifier(pConfig.AddConfigurationSection(ConfigSectionContractSpecifier))

            With pConfig
                DaysBeforeExpiryToSwitch = CInt(.GetSetting(ConfigSettingDaysBeforeExpiryToSwitch, "1"))
                Description = .GetSetting(ConfigSettingDescription, "")
                ExpiryDate = CDate(.GetSetting(ConfigSettingExpiryDate, ""))
                SessionEndTime = TimeSpan.Parse(.GetSetting(ConfigSettingSessionEndTime, "00:00:00"))
                SessionStartTime = TimeSpan.Parse(.GetSetting(ConfigSettingSessionStartTime, "00:00:00"))
                TickSize = CDbl(.GetSetting(ConfigSettingTickSize, "0.01"))
                TimezoneName = .GetSetting(ConfigSettingTimezoneName, "")
            End With
        End Sub

        Public Shared Function FromCOM(comContract As ContractUtils27._IContract) As IContract
            'mComContract = comContract
            Dim contract = New Contract
            With comContract
                contract.DaysBeforeExpiryToSwitch = .DaysBeforeExpiryToSwitch
                contract.Description = .Description
                contract.ExpiryDate = .ExpiryDate
                'ProviderIDs = .ProviderIDs
                contract.SessionEndTime = .SessionEndTime - StartOfDayAsDate
                contract.SessionStartTime = .SessionStartTime - StartOfDayAsDate
                contract.Specifier = ContractSpecifier.FromCOM(comContract.Specifier)
                contract.TickSize = .TickSize
                contract.TimezoneName = .TimezoneName
            End With
            Return contract
        End Function

        '@================================================================================
        ' Properties
        '@================================================================================

        'Friend ReadOnly Property ComContract As ContractUtils27._IContract
        '    Get
        '        If mComContract Is Nothing Then
        '            Dim lBuilder = ComContractUtils.CreateContractBuilder(CType(Specifier, ContractSpecifierClass))
        '            With lBuilder
        '                .DaysBeforeExpiryToSwitch = DaysBeforeExpiryToSwitch
        '                .Description = Description
        '                .ExpiryDate = ExpiryDate
        '                .ProviderIDs = ProviderIDs
        '                .SessionEndTime = StartOfDayAsDate + SessionEndTime
        '                .SessionStartTime = StartOfDayAsDate + SessionStartTime
        '                .TickSize = TickSize
        '                .TimezoneName = TimezoneName
        '            End With
        '        End If
        '        Return mComContract
        '    End Get
        'End Property

        <DataMember>
        Public Property DaysBeforeExpiryToSwitch() As Integer Implements IContract.DaysBeforeExpiryToSwitch
            Get
                DaysBeforeExpiryToSwitch = mDaysBeforeExpiryToSwitch
            End Get
            Friend Set(Value As Integer)
                mDaysBeforeExpiryToSwitch = Value
            End Set
        End Property

        <DataMember>
        Public Property Description() As String Implements IContract.Description
            Get
                Description = mDescription
            End Get
            Friend Set(Value As String)
                mDescription = Value
            End Set
        End Property

        <DataMember>
        Public Property ExpiryDate() As DateTime Implements IContract.ExpiryDate
            Get
                ExpiryDate = mExpiryDate
            End Get
            Friend Set(Value As DateTime)
                mExpiryDate = Value
            End Set
        End Property

        Public ReadOnly Property Key() As String
            Get
                Key = Specifier.Key
            End Get
        End Property

        Public ReadOnly Property NumberOfDecimals() As Integer Implements IContract.NumberOfDecimals
            Get
                NumberOfDecimals = mNumberOfDecimals
            End Get
        End Property

        Public ReadOnly Property ProviderID(providerKey As String) As String
            Get
                ProviderID = mProviderIDs.GetParameterValue(providerKey, mSpecifier.LocalSymbol)
            End Get
        End Property

        <DataMember>
        Public Property ProviderIDs() As Parameters
            Get
                ProviderIDs = mProviderIDs
            End Get
            Friend Set(Value As Parameters)
                mProviderIDs = Value
            End Set
        End Property

        <DataMember>
        Public Property SessionStartTime() As TimeSpan Implements IContract.SessionStartTime
            Get
                SessionStartTime = mSessionStartTime
            End Get
            Friend Set
                If Value.Days <> 0 Then Throw New ArgumentException("SessionStartTime must have a zero Days field")
                mSessionStartTime = Value
            End Set
        End Property

        <DataMember>
        Public Property SessionEndTime() As TimeSpan Implements IContract.SessionEndTime
            Get
                SessionEndTime = mSessionEndTime
            End Get
            Friend Set
                If Value.Days <> 0 Then Throw New ArgumentException("SessionEndTime must have a zero Days field")
                mSessionEndTime = Value
            End Set
        End Property

        <DataMember>
        Public Property Specifier() As IContractSpecifier Implements IContract.Specifier
            Get
                Specifier = mSpecifier
            End Get
            Friend Set(Value As IContractSpecifier)
                mSpecifier = Value
            End Set
        End Property

        <DataMember>
        Public Property TickSize() As Double Implements IContract.TickSize
            Get
                TickSize = mTickSize
            End Get
            Friend Set(Value As Double)
                mTickSize = Value

                Dim minTickString = mTickSize.ToString

                mNumberOfDecimals = Len(minTickString) - 2
            End Set
        End Property

        <DataMember>
        Public ReadOnly Property TickValue() As Double Implements IContract.TickValue
            Get
                TickValue = mTickSize * mSpecifier.Multiplier
            End Get
        End Property

        <DataMember>
        Public Property TimezoneName() As String Implements IContract.TimezoneName
            Get
                TimezoneName = mTimezoneName
            End Get
            Friend Set(Value As String)
                mTimezoneName = Value
            End Set
        End Property

        '@================================================================================
        ' IJSONable Interface Members
        '@================================================================================

        Private Function JSONable_ToJSON() As String Implements IJSONable.ToJSON
            JSONable_ToJSON = ToJSON()
        End Function

        '@================================================================================
        ' Stringable Interface Members
        '@================================================================================

        Private Function Stringable_ToString() As String Implements IStringable.ToString
            Return ToString()
        End Function

        '@================================================================================
        ' Methods
        '@================================================================================

        Public Shared Function ContractSpecsCompatible(pContractSpec1 As IContractSpecifier, pContractSpec2 As IContractSpecifier) As Boolean

            If pContractSpec1.LocalSymbol <> "" And pContractSpec1.LocalSymbol <> pContractSpec2.LocalSymbol Then Return False
            If pContractSpec1.Symbol <> "" And pContractSpec1.Symbol <> pContractSpec2.Symbol Then Return False
            If pContractSpec1.SecType <> SecurityType.None And pContractSpec1.SecType <> pContractSpec2.SecType Then Return False
            If pContractSpec1.Exchange <> "" And pContractSpec1.Exchange <> pContractSpec2.Exchange Then Return False
            If pContractSpec1.Expiry <> "" And pContractSpec1.Expiry <> Left(pContractSpec2.Expiry, Len(pContractSpec1.Expiry)) Then Return False
            If pContractSpec1.Exchange <> "" And pContractSpec1.Exchange <> pContractSpec2.Exchange Then Return False
            If pContractSpec1.CurrencyCode <> "" And pContractSpec1.CurrencyCode <> pContractSpec2.CurrencyCode Then Return False
            If pContractSpec1.Right <> OptionRight.None And pContractSpec1.Right <> pContractSpec2.Right Then Return False
            If pContractSpec1.Strike <> 0.0# And pContractSpec1.Strike <> pContractSpec2.Strike Then Return False

            Return True
        End Function

        Public Shared Function CompareContractSpecs(pContractSpec1 As IContractSpecifier, pContractSpec2 As IContractSpecifier, ByRef pSortkeys() As ContractSortKeyId) As Integer
            Dim compare = 0
            For i = 0 To UBound(pSortkeys)
                Select Case pSortkeys(i)
                    Case ContractSortKeyId.None
                        Return 0
                    Case ContractSortKeyId.LocalSymbol
                        compare = StrComp(pContractSpec1.LocalSymbol, pContractSpec2.LocalSymbol, CompareMethod.Text)
                    Case ContractSortKeyId.Symbol
                        compare = StrComp(pContractSpec1.Symbol, pContractSpec2.Symbol, CompareMethod.Text)
                    Case ContractSortKeyId.SecType
                        compare = StrComp(SecTypeToShortString(pContractSpec1.SecType), SecTypeToShortString(pContractSpec2.SecType), CompareMethod.Text)
                    Case ContractSortKeyId.Exchange
                        compare = StrComp(pContractSpec1.Exchange, pContractSpec2.Exchange, CompareMethod.Text)
                    Case ContractSortKeyId.Expiry
                        compare = StrComp(pContractSpec1.Expiry, pContractSpec2.Expiry, CompareMethod.Text)
                    Case ContractSortKeyId.Mutiplier
                        compare = If(pContractSpec1.Multiplier < pContractSpec2.Multiplier, -1, (If(pContractSpec1.Multiplier = pContractSpec2.Multiplier, 0, 1)))
                    Case ContractSortKeyId.Currency
                        compare = StrComp(pContractSpec1.CurrencyCode, pContractSpec2.CurrencyCode, CompareMethod.Text)
                    Case ContractSortKeyId.Right
                        compare = StrComp(OptionRightToString(pContractSpec1.Right), OptionRightToString(pContractSpec2.Right), CompareMethod.Text)
                    Case ContractSortKeyId.Strike
                        compare = System.Math.Sign(pContractSpec1.Strike - pContractSpec2.Strike)
                End Select
                If compare <> 0 Then Return compare
            Next
            Return 0
        End Function

        Public Shared Function CreateClockFuture(pContractFuture As IFuture, Optional pIsSimulated As Boolean = False, Optional pClockRate As Single = 1.0!) As IFuture
            Dim lClockFutureBuilder As New ClockFutureBuilder
            lClockFutureBuilder.Initialise(CType(pContractFuture, IFuture), pIsSimulated, pClockRate)
            Return lClockFutureBuilder.Future
        End Function

        Public Shared Function CreateSessionBuilderFutureFromContractFuture(pContractFuture As IFuture, pUseExchangeTimeZone As Boolean) As _IFuture
            Dim lBuilder As New SessionBuilderFutBldr
            lBuilder.Initialise(pContractFuture, pUseExchangeTimeZone)
            CreateSessionBuilderFutureFromContractFuture = lBuilder.Future
        End Function

        'Public Shared Narrowing Operator CType(comContract As ContractUtils27.ContractClass) As Contract
        '    Return New Contract(comContract)
        'End Operator

        'Public Shared Narrowing Operator CType(contract As Contract) As ContractUtils27.ContractClass
        '    Return contract.ComContract
        'End Operator

        Friend Sub FromXML(contractXML As String)
            Dim XMLdoc As XDocument
            Try
                XMLdoc = XDocument.Parse(contractXML)
            Catch ex As Exception
                Throw New ArgumentException(NameOf(contractXML), ex)
            End Try

            Dim contractEl = XMLdoc.<contract>(0)
            TickSize = Double.Parse(contractEl.@minimumtick)

            If String.IsNullOrEmpty(contractEl.@sessionstarttime) Then
                mSessionStartTime = StartOfDay
            Else
                mSessionStartTime = TimeSpan.Parse(contractEl.@sessionstarttime)
            End If

            If String.IsNullOrEmpty(contractEl.@sessionendtime) Then
                mSessionEndTime = StartOfDay
            Else
                mSessionEndTime = TimeSpan.Parse(contractEl.@sessionendtime)
            End If

            mDescription = contractEl.@description

            If String.IsNullOrEmpty(contractEl.@numberofdecimals) Then
                mNumberOfDecimals = 4
            Else
                mNumberOfDecimals = Integer.Parse(contractEl.@numberofdecimals)
            End If

            mTimezoneName = contractEl.@timezonename

            Dim specifierEl = contractEl.<specifier>(0)
            Dim specifierObj = New ContractSpecifier(specifierEl)
            Specifier = specifierObj

            Dim nodeList = specifierEl.<combolegs>(0).<comboleg>

            If nodeList.Count <> 0 Then
                Dim comboLegsObj = New ComboLegs
                specifierObj.ComboLegs = comboLegsObj
                For Each comboLegEl In nodeList
                    Dim comboSpecifierEl = comboLegEl.<specifier>(0)
                    Dim comboSpecifierObj = New ContractSpecifier(comboSpecifierEl)
                    Dim comboLegObj = New ComboLeg(comboSpecifierObj, CBool(comboLegEl.@isbuyleg), Integer.Parse(comboLegEl.@Ratio))
                    comboLegsObj.Add(comboLegObj)
                Next
            End If
        End Sub

        Public Shared Function IsContractExpired(contract As IContract) As Boolean
            Select Case contract.Specifier.SecType
                Case SecurityType.Future, SecurityType.Option, SecurityType.FuturesOption
                    Return CBool(IIf(contract.ExpiryDate.Date.CompareTo(System.TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.Date, contract.TimezoneName)) < 0, True, False))
            End Select
            Return False
        End Function

        Public Shared Function IsValidExpiry(value As String) As Boolean
            Dim d As Date

            If IsDate(value) Then
                d = CDate(value)
            ElseIf Len(value) = 8 Then
                Dim datestring = value.Substring(0, 4) & "/" & value.Substring(4, 2) & "/" & value.Substring(value.Length - 2)
                If IsDate(datestring) Then d = CDate(datestring)
            End If

            If d >= CDate((Year(Now) - 20) & "/01/01") And d <= CDate((Year(Now) + 10) & "/12/31") Then Return True

            If Len(value) = 6 Then
                If TW.IsInteger(value, (Year(Now) - 20) * 100 + 1, (Year(Now) + 10) * 100 + 12) Then
                    If CInt(value.Substring(value.Length - 2)) <= 12 Then
                        Return True
                    End If
                End If
            End If

            Return False
        End Function

        Public Shared Function IsValidPrice(pPrice As Double, pPrevValidPrice As Double, pSecType As SecurityType, pTickSize As Double) As Boolean
            If pTickSize = 0 Then Return True

            If pPrevValidPrice = 0 Or pPrevValidPrice = Double.MaxValue Then
                ' note that Z index has ticksize 0.01 so we need to allow plenty of room
                ' &H3FFFFF = 4194303
                If System.Math.Abs(pPrice) / pTickSize > &H3FFFFF Then Return False

                ' A first price of 0 is always considered invalid. Although some indexes can validly
                ' have zero values, it is unlikely on the very first price notified, and this check
                ' catches the occasional zero prices sent by IB when, for example, the Z ticker
                ' is started
                If pPrice = 0 Then Return False
            End If

            ' don't do the next check for indexes because some of them, such as TICK-NYSE, can have both
            ' positive and negative values - moreover the value can change dramatically from one
            ' tick to the next
            If pSecType = SecurityType.Index Then Return True

            If pPrice < 0 Then Return False
            If pPrice / pTickSize < 1 Then Return False ' catch occasional very small prices from IB

            Return True
        End Function

        Public Function IsValidPrice(pPrice As Double, pPrevValidPrice As Double) As Boolean
            IsValidPrice = IsValidPrice(pPrice, pPrevValidPrice, mSpecifier.SecType, mTickSize)
        End Function

        Public Shared Function OptionRightFromString(Value As String) As OptionRight
            Select Case UCase(Value)
                Case ""
                    Return OptionRight.None
                Case "CALL", "C"
                    Return OptionRight.Call
                Case "PUT", "P"
                    Return OptionRight.Put
                Case Else
                    Throw New ArgumentException(String.Format("Invalid Option Right value: {0}", Value))
            End Select
        End Function

        Public Shared Function OptionRightToString(Value As OptionRight) As String
            Return [Enum].Format(Value.GetType(), Value, "G")
        End Function

        Public Shared Sub SaveToConfig(pContract As IContract, pConfig As ConfigurationSection)
            ContractSpecifier.SaveToConfig(pContract.Specifier, pConfig.AddConfigurationSection(ConfigSectionContractSpecifier))

            With pConfig
                .SetSetting(ConfigSettingDaysBeforeExpiryToSwitch, CStr(pContract.DaysBeforeExpiryToSwitch))
                .SetSetting(ConfigSettingDescription, pContract.Description)
                .SetSetting(ConfigSettingExpiryDate, pContract.ExpiryDate.ToString("yyyy-MM-dd"))
                .SetSetting(ConfigSettingSessionEndTime, pContract.SessionEndTime.ToString("HH:mm:ss"))
                .SetSetting(ConfigSettingSessionStartTime, pContract.SessionStartTime.ToString("HH:mm:ss"))
                .SetSetting(ConfigSettingTickSize, CStr(pContract.TickSize))
                .SetSetting(ConfigSettingTimezoneName, pContract.TimezoneName)
            End With
        End Sub

        Public Shared Function SecTypeFromString(Value As String) As SecurityType
            Select Case UCase(Value)
                Case "STOCK", "STK"
                    Return SecurityType.Stock
                Case "FUTURE", "FUT"
                    Return SecurityType.Future
                Case "OPTION", "OPT"
                    Return SecurityType.Option
                Case "FUTURES OPTION", "FOP"
                    Return SecurityType.FuturesOption
                Case "CASH"
                    Return SecurityType.Cash
                Case "COMBO", "CMB"
                    Return SecurityType.Combo
                Case "INDEX", "IND"
                    Return SecurityType.Index
            End Select
            Throw New ArgumentException("Unsupported sec type identifier")
        End Function

        Public Shared Function SecTypeToString(Value As SecurityType) As String
            Select Case Value
                Case SecurityType.Stock
                    Return "Stock"
                Case SecurityType.Future
                    Return "Future"
                Case SecurityType.Option
                    Return "Option"
                Case SecurityType.FuturesOption
                    Return "Futures Option"
                Case SecurityType.Cash
                    Return "Cash"
                Case SecurityType.Combo
                    Return "Combo"
                Case SecurityType.Index
                    Return "Index"
            End Select
            Throw New ArgumentException("Unsupported sec type")
        End Function

        Public Shared Function SecTypeToShortString(Value As SecurityType) As String
            Select Case Value
                Case SecurityType.Stock
                    Return "STK"
                Case SecurityType.Future
                    Return "FUT"
                Case SecurityType.Option
                    Return "OPT"
                Case SecurityType.FuturesOption
                    Return "FOP"
                Case SecurityType.Cash
                    Return "CASH"
                Case SecurityType.Combo
                    Return "CMB"
                Case SecurityType.Index
                    Return "IND"
            End Select
            Throw New ArgumentException("Unsupported sec type")
        End Function

        Friend Function ToJSON() As String
            Dim stream = New MemoryStream()
            Dim ser = New DataContractJsonSerializer(Me.GetType)
            ser.WriteObject(stream, Me)
            stream.Position = 0
            Dim sr = New StreamReader(stream)
            Return sr.ReadToEnd()
        End Function

        Public Overrides Function ToString() As String
            Return $"Specifier=({mSpecifier.ToString()}); Description={mDescription}; Expiry date={mExpiryDate}; Tick size={mTickSize}; Session start={mSessionStartTime.ToString("HH:mm")}; Session end={mSessionEndTime.ToString("HH:mm")}; TimezoneName={mTimezoneName}"
        End Function

        Public Function ToXML() As String
            Dim XMLdoc As New XDocument()

            Dim contractElement = New XElement("contract")
            XMLdoc.Add(contractElement)
            contractElement.SetAttributeValue("xmlns", "urn:tradewright.com:tradebuild")
            contractElement.SetAttributeValue("minimumtick", TickSize)
            contractElement.SetAttributeValue("sessionstarttime", mSessionStartTime.ToString())
            contractElement.SetAttributeValue("sessionendtime", mSessionEndTime.ToString)
            contractElement.SetAttributeValue("Description", mDescription)
            contractElement.SetAttributeValue("numberofdecimals", mNumberOfDecimals)
            contractElement.SetAttributeValue("timezonename", mTimezoneName)

            Dim specifier = New XElement("specifier")
            contractElement.Add(specifier)
            specifier.SetAttributeValue("symbol", mSpecifier.Symbol)
            specifier.SetAttributeValue("sectype", mSpecifier.SecType)
            specifier.SetAttributeValue("expiry", mSpecifier.Expiry)
            specifier.SetAttributeValue("exchange", mSpecifier.Exchange)
            specifier.SetAttributeValue("currencycode", mSpecifier.CurrencyCode)
            specifier.SetAttributeValue("localsymbol", mSpecifier.LocalSymbol)
            specifier.SetAttributeValue("multiplier", mSpecifier.Multiplier)
            specifier.SetAttributeValue("right", mSpecifier.Right)
            specifier.SetAttributeValue("strike", mSpecifier.Strike)

            Dim comboLegs = New XElement("combolegs")
            specifier.Add(comboLegs)
            If Not mSpecifier.ComboLegs Is Nothing Then
                For Each comboLegObj In mSpecifier.ComboLegs
                    Dim comboLeg = New XElement("comboleg")
                    comboLegs.Add(comboLeg)

                    specifier = New XElement("specifier")
                    comboLeg.Add(specifier)
                    comboLeg.SetAttributeValue("isBuyLeg", comboLegObj.IsBuyLeg)
                    comboLeg.SetAttributeValue("Ratio", comboLegObj.Ratio)
                    specifier.SetAttributeValue("symbol", mSpecifier.Symbol)
                    specifier.SetAttributeValue("sectype", mSpecifier.SecType)
                    specifier.SetAttributeValue("expiry", mSpecifier.Expiry)
                    specifier.SetAttributeValue("exchange", mSpecifier.Exchange)
                    specifier.SetAttributeValue("currencycode", mSpecifier.CurrencyCode)
                    specifier.SetAttributeValue("localsymbol", mSpecifier.LocalSymbol)
                    specifier.SetAttributeValue("right", mSpecifier.Right)
                    specifier.SetAttributeValue("strike", mSpecifier.Strike)
                Next comboLegObj
            End If
            Return XMLdoc.ToString()
        End Function

        Private Function _IContract_ToString() As String Implements _IContract.ToString
            Return ToString()
        End Function

        Private ReadOnly Property _IContract_DaysBeforeExpiryToSwitch As Integer Implements _IContract.DaysBeforeExpiryToSwitch
            Get
                Return mDaysBeforeExpiryToSwitch
            End Get
        End Property

        Private ReadOnly Property _IContract_Description As String Implements _IContract.Description
            Get
                Return mDescription
            End Get
        End Property

        Private ReadOnly Property _IContract_ExpiryDate As Date Implements _IContract.ExpiryDate
            Get
                Return mExpiryDate
            End Get
        End Property

        Private ReadOnly Property _IContract_NumberOfDecimals As Integer Implements _IContract.NumberOfDecimals
            Get
                Return mNumberOfDecimals
            End Get
        End Property

        Private ReadOnly Property _IContract_SessionStartTime As Date Implements _IContract.SessionStartTime
            Get
                Return StartOfDayAsDate + mSessionStartTime
            End Get
        End Property

        Private ReadOnly Property _IContract_SessionEndTime As Date Implements _IContract.SessionEndTime
            Get
                Return StartOfDayAsDate + mSessionEndTime
            End Get
        End Property

        Private ReadOnly Property _IContract_Specifier As _IContractSpecifier Implements _IContract.Specifier
            Get
                Return DirectCast(mSpecifier, _IContractSpecifier)
            End Get
        End Property

        Private ReadOnly Property _IContract_TickSize As Double Implements _IContract.TickSize
            Get
                Return mTickSize
            End Get
        End Property

        Private ReadOnly Property _IContract_TickValue As Double Implements _IContract.TickValue
            Get
                Return mTickSize * mSpecifier.Multiplier
            End Get
        End Property

        Private ReadOnly Property _IContract_TimezoneName As String Implements _IContract.TimezoneName
            Get
                Return mTimezoneName
            End Get
        End Property

        '@================================================================================
        ' Helper Functions
        '@================================================================================
    End Class
End Namespace