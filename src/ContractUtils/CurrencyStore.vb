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


Namespace Contracts
    Public Structure CurrencyDescriptor
        Implements IComparable(Of CurrencyDescriptor)
        Implements IComparable

        Dim code As String
        Dim description As String

        Public Function CompareTo(descriptor As CurrencyDescriptor) As Integer Implements IComparable(Of CurrencyDescriptor).CompareTo
            Return StrComp(code, descriptor.code)
        End Function

        Private Function CompareTo1(obj As Object) As Integer Implements IComparable.CompareTo
            If Not TypeOf obj Is CurrencyDescriptor Then Throw New ArgumentException("Invalid object type")
            Return CompareTo(CType(obj, CurrencyDescriptor))
        End Function
    End Structure

    Public Module CurrencyStore

        Private mCurrencyDescs As List(Of CurrencyDescriptor) = New List(Of CurrencyDescriptor)(127)

        Sub New()
            addCurrencyDesc("AED", "United Arab Emirates, Dirhams")
            addCurrencyDesc("AFN", "Afghanistan, Afghanis")
            addCurrencyDesc("ALL", "Albania, Leke")
            addCurrencyDesc("AMD", "Armenia, Drams")
            addCurrencyDesc("ANG", "Netherlands Antilles, Guilders")
            addCurrencyDesc("AOA", "Angola, Kwanza")
            addCurrencyDesc("ARS", "Argentina, Pesos")
            addCurrencyDesc("AUD", "Australia, Dollars")
            addCurrencyDesc("AWG", "Aruba, Guilders")
            addCurrencyDesc("AZN", "Azerbaijan, New Manats")
            addCurrencyDesc("BAM", "Bosnia and Herzegovina, Convertible Marka")
            addCurrencyDesc("BBD", "Barbados, Dollars")
            addCurrencyDesc("BDT", "Bangladesh, Taka")
            addCurrencyDesc("BGN", "Bulgaria, Leva")
            addCurrencyDesc("BHD", "Bahrain, Dinars")
            addCurrencyDesc("BIF", "Burundi, Francs")
            addCurrencyDesc("BMD", "Bermuda, Dollars")
            addCurrencyDesc("BND", "Brunei Darussalam, Dollars")
            addCurrencyDesc("BOB", "Bolivia, Bolivianos")
            addCurrencyDesc("BRL", "Brazil, Brazil Real")
            addCurrencyDesc("BSD", "Bahamas, Dollars")
            addCurrencyDesc("BTN", "Bhutan, Ngultrum")
            addCurrencyDesc("BWP", "Botswana, Pulas")
            addCurrencyDesc("BYR", "Belarus, Rubles")
            addCurrencyDesc("BZD", "Belize, Dollars")
            addCurrencyDesc("CAD", "Canada, Dollars")
            addCurrencyDesc("CDF", "Congo/Kinshasa, Congolese Francs")
            addCurrencyDesc("CHF", "Switzerland, Francs")
            addCurrencyDesc("CLP", "Chile, Pesos")
            addCurrencyDesc("CNY", "China, Yuan Renminbi")
            addCurrencyDesc("COP", "Colombia, Pesos")
            addCurrencyDesc("CRC", "Costa Rica, Colones")
            addCurrencyDesc("CUP", "Cuba, Pesos")
            addCurrencyDesc("CVE", "Cape Verde, Escudos")
            addCurrencyDesc("CYP", "Cyprus, Pounds")
            addCurrencyDesc("CZK", "Czech Republic, Koruny")
            addCurrencyDesc("DJF", "Djibouti, Francs")
            addCurrencyDesc("DKK", "Denmark, Kroner")
            addCurrencyDesc("DOP", "Dominican Republic, Pesos")
            addCurrencyDesc("DZD", "Algeria, Algeria Dinars")
            addCurrencyDesc("EEK", "Estonia, Krooni")
            addCurrencyDesc("EGP", "Egypt, Pounds")
            addCurrencyDesc("ERN", "Eritrea, Nakfa")
            addCurrencyDesc("ETB", "Ethiopia, Birr")
            addCurrencyDesc("EUR", "Euro Member Countries, Euro")
            addCurrencyDesc("FJD", "Fiji, Dollars")
            addCurrencyDesc("FKP", "Falkland Islands (Malvinas), Pounds")
            addCurrencyDesc("GBP", "United Kingdom, Pounds")
            addCurrencyDesc("GEL", "Georgia, Lari")
            addCurrencyDesc("GGP", "Guernsey, Pounds")
            addCurrencyDesc("GHC", "Ghana, Cedis")
            addCurrencyDesc("GHS", "Ghana, Cedis")
            addCurrencyDesc("GIP", "Gibraltar, Pounds")
            addCurrencyDesc("GMD", "Gambia, Dalasi")
            addCurrencyDesc("GNF", "Guinea, Francs")
            addCurrencyDesc("GTQ", "Guatemala, Quetzales")
            addCurrencyDesc("GYD", "Guyana, Dollars")
            addCurrencyDesc("HKD", "Hong Kong, Dollars")
            addCurrencyDesc("HNL", "Honduras, Lempiras")
            addCurrencyDesc("HRK", "Croatia, Kuna")
            addCurrencyDesc("HTG", "Haiti, Gourdes")
            addCurrencyDesc("HUF", "Hungary, Forint")
            addCurrencyDesc("IDR", "Indonesia, Rupiahs")
            addCurrencyDesc("ILS", "Israel, New Shekels")
            addCurrencyDesc("IMP", "Isle of Man, Pounds")
            addCurrencyDesc("INR", "India, Rupees")
            addCurrencyDesc("IQD", "Iraq, Dinars")
            addCurrencyDesc("IRR", "Iran, Rials")
            addCurrencyDesc("ISK", "Iceland, Kronur")
            addCurrencyDesc("JEP", "Jersey, Pounds")
            addCurrencyDesc("JMD", "Jamaica, Dollars")
            addCurrencyDesc("JOD", "Jordan, Dinars")
            addCurrencyDesc("JPY", "Japan, Yen")
            addCurrencyDesc("KES", "Kenya, Shillings")
            addCurrencyDesc("KGS", "Kyrgyzstan, Soms")
            addCurrencyDesc("KHR", "Cambodia, Riels")
            addCurrencyDesc("KMF", "Comoros, Francs")
            addCurrencyDesc("KPW", "Korea (North), Won")
            addCurrencyDesc("KRW", "Korea (South), Won")
            addCurrencyDesc("KWD", "Kuwait, Dinars")
            addCurrencyDesc("KYD", "Cayman Islands, Dollars")
            addCurrencyDesc("KZT", "Kazakhstan, Tenge")
            addCurrencyDesc("LAK", "Laos, Kips")
            addCurrencyDesc("LBP", "Lebanon, Pounds")
            addCurrencyDesc("LKR", "Sri Lanka, Rupees")
            addCurrencyDesc("LRD", "Liberia, Dollars")
            addCurrencyDesc("LSL", "Lesotho, Maloti")
            addCurrencyDesc("LTL", "Lithuania, Litai")
            addCurrencyDesc("LVL", "Latvia, Lati")
            addCurrencyDesc("LYD", "Libya, Dinars")
            addCurrencyDesc("MAD", "Morocco, Dirhams")
            addCurrencyDesc("MDL", "Moldova, Lei")
            addCurrencyDesc("MGA", "Madagascar, Ariary")
            addCurrencyDesc("MKD", "Macedonia, Denars")
            addCurrencyDesc("MMK", "Myanmar (Burma), Kyats")
            addCurrencyDesc("MNT", "Mongolia, Tugriks")
            addCurrencyDesc("MOP", "Macau, Patacas")
            addCurrencyDesc("MRO", "Mauritania, Ouguiyas")
            addCurrencyDesc("MTL", "Malta, Liri")
            addCurrencyDesc("MUR", "Mauritius, Rupees")
            addCurrencyDesc("MVR", "Maldives (Maldive Islands), Rufiyaa")
            addCurrencyDesc("MWK", "Malawi, Kwachas")
            addCurrencyDesc("MXN", "Mexico, Pesos")
            addCurrencyDesc("MYR", "Malaysia, Ringgits")
            addCurrencyDesc("MZN", "Mozambique, Meticais")
            addCurrencyDesc("NAD", "Namibia, Dollars")
            addCurrencyDesc("NGN", "Nigeria, Nairas")
            addCurrencyDesc("NIO", "Nicaragua, Cordobas")
            addCurrencyDesc("NOK", "Norway, Krone")
            addCurrencyDesc("NPR", "Nepal, Nepal Rupees")
            addCurrencyDesc("NZD", "New Zealand, Dollars")
            addCurrencyDesc("OMR", "Oman, Rials")
            addCurrencyDesc("PAB", "Panama, Balboa")
            addCurrencyDesc("PEN", "Peru, Nuevos Soles")
            addCurrencyDesc("PGK", "Papua New Guinea, Kina")
            addCurrencyDesc("PHP", "Philippines, Pesos")
            addCurrencyDesc("PKR", "Pakistan, Rupees")
            addCurrencyDesc("PLN", "Poland, Zlotych")
            addCurrencyDesc("PYG", "Paraguay, Guarani")
            addCurrencyDesc("QAR", "Qatar, Rials")
            addCurrencyDesc("RON", "Romania, New Lei")
            addCurrencyDesc("RSD", "Serbia, Dinars")
            addCurrencyDesc("RUB", "Russia, Rubles")
            addCurrencyDesc("RWF", "Rwanda, Rwanda Francs")
            addCurrencyDesc("SAR", "Saudi Arabia, Riyals")
            addCurrencyDesc("SBD", "Solomon Islands, Dollars")
            addCurrencyDesc("SCR", "Seychelles, Rupees")
            addCurrencyDesc("SDG", "Sudan, Pounds")
            addCurrencyDesc("SEK", "Sweden, Kronor")
            addCurrencyDesc("SGD", "Singapore, Dollars")
            addCurrencyDesc("SHP", "Saint Helena, Pounds")
            addCurrencyDesc("SKK", "Slovakia, Koruny")
            addCurrencyDesc("SLL", "Sierra Leone, Leones")
            addCurrencyDesc("SOS", "Somalia, Shillings")
            addCurrencyDesc("SPL", "Seborga, Luigini")
            addCurrencyDesc("SRD", "Suriname, Dollars")
            addCurrencyDesc("STD", "São Tome and Principe, Dobras")
            addCurrencyDesc("SVC", "El Salvador, Colones")
            addCurrencyDesc("SYP", "Syria, Pounds")
            addCurrencyDesc("SZL", "Swaziland, Emalangeni")
            addCurrencyDesc("THB", "Thailand, Baht")
            addCurrencyDesc("TJS", "Tajikistan, Somoni")
            addCurrencyDesc("TMM", "Turkmenistan, Manats")
            addCurrencyDesc("TND", "Tunisia, Dinars")
            addCurrencyDesc("TOP", "Tonga, Pa'anga")
            addCurrencyDesc("TRY", "Turkey, New Lira")
            addCurrencyDesc("TTD", "Trinidad and Tobago, Dollars")
            addCurrencyDesc("TVD", "Tuvalu, Tuvalu Dollars")
            addCurrencyDesc("TWD", "Taiwan, New Dollars")
            addCurrencyDesc("TZS", "Tanzania, Shillings")
            addCurrencyDesc("UAH", "Ukraine, Hryvnia")
            addCurrencyDesc("UGX", "Uganda, Shillings")
            addCurrencyDesc("USD", "United States of America, Dollars")
            addCurrencyDesc("UYU", "Uruguay, Pesos")
            addCurrencyDesc("UZS", "Uzbekistan, Sums")
            addCurrencyDesc("VEB", "Venezuela, Bolivares")
            addCurrencyDesc("VND", "Viet Nam, Dong")
            addCurrencyDesc("VUV", "Vanuatu, Vatu")
            addCurrencyDesc("WST", "Samoa, Tala")
            addCurrencyDesc("XAF", "Communauté Financière Africaine BEAC, Francs")
            addCurrencyDesc("XAG", "Silver, Ounces")
            addCurrencyDesc("XAU", "Gold, Ounces")
            addCurrencyDesc("XCD", "East Caribbean Dollars")
            addCurrencyDesc("XOF", "Communauté Financière Africaine BCEAO, Francs")
            addCurrencyDesc("XPD", "Palladium Ounces")
            addCurrencyDesc("XPF", "Comptoirs Français du Pacifique Francs")
            addCurrencyDesc("XPT", "Platinum, Ounces")
            addCurrencyDesc("YER", "Yemen, Rials")
            addCurrencyDesc("ZAR", "South Africa, Rand")
            addCurrencyDesc("ZMK", "Zambia, Kwacha")
            addCurrencyDesc("ZWD", "Zimbabwe, Zimbabwe Dollars")

            mCurrencyDescs.Sort(Function(d1, d2) StrComp(d1.code, d2.code))
        End Sub

        Public Function GetCurrencyDescriptor(code As String) As CurrencyDescriptor
            Dim index = getIndex(code)
            If Not index >= 0 Then Throw New ArgumentException("Invalid currency code")
            Return mCurrencyDescs(index)
        End Function

        Public Function GetCurrencyDescriptors() As List(Of CurrencyDescriptor)
            Return mCurrencyDescs
        End Function

        Public Function IsValidCurrencyCode(code As String) As Boolean
            Return (getIndex(code) >= 0)
        End Function

        Private Sub addCurrencyDesc(code As String, description As String)
            Dim descriptor As CurrencyDescriptor
            descriptor.code = code
            descriptor.description = description
            mCurrencyDescs.Add(descriptor)
        End Sub

        Private Function getIndex(code As String) As Integer
            Dim descriptor As New CurrencyDescriptor
            descriptor.code = code
            Return mCurrencyDescs.BinarySearch(descriptor, System.Collections.Generic.Comparer(Of CurrencyDescriptor).Create(Function(d1, d2) StrComp(d1.code, d2.code)))
        End Function

    End Module
End Namespace
