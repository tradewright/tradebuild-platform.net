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

Imports System.Collections.Generic

Namespace Contracts
    Public Module Exchange

        Private mExchangeCodes As List(Of String) = New List(Of String)(31)

        Sub New()
            mExchangeCodes.Add("ACE")
            mExchangeCodes.Add("AEB")
            mExchangeCodes.Add("AMEX")
            mExchangeCodes.Add("ARCA")
            mExchangeCodes.Add("ASX")

            mExchangeCodes.Add("BATS")
            mExchangeCodes.Add("BELFOX")
            mExchangeCodes.Add("BEX")
            mExchangeCodes.Add("BOX")
            mExchangeCodes.Add("BRUT")
            mExchangeCodes.Add("BTRADE")
            mExchangeCodes.Add("BVME")
            mExchangeCodes.Add("BYX")

            mExchangeCodes.Add("CAES")
            mExchangeCodes.Add("CBOE")
            mExchangeCodes.Add("CBOE2")
            mExchangeCodes.Add("CBOT")
            mExchangeCodes.Add("CBSX")
            mExchangeCodes.Add("CDE")
            mExchangeCodes.Add("CFE")
            mExchangeCodes.Add("CHX")
            mExchangeCodes.Add("CSFBALGO")

            mExchangeCodes.Add("DRCTEDGE")
            mExchangeCodes.Add("DTB")

            mExchangeCodes.Add("EBS")
            mExchangeCodes.Add("ECBOT")
            mExchangeCodes.Add("EDGEA")
            mExchangeCodes.Add("EUREX")
            mExchangeCodes.Add("EUREXUS")

            mExchangeCodes.Add("FTA")
            mExchangeCodes.Add("FWB")

            mExchangeCodes.Add("GLOBEX")

            mExchangeCodes.Add("HKFE")

            mExchangeCodes.Add("IBIS")
            mExchangeCodes.Add("IDEAL")
            mExchangeCodes.Add("IDEALPRO")
            mExchangeCodes.Add("IDEM")
            mExchangeCodes.Add("INET")
            mExchangeCodes.Add("INSTINET")
            mExchangeCodes.Add("ISE")
            mExchangeCodes.Add("ISLAND")

            mExchangeCodes.Add("JEFFALGO")

            mExchangeCodes.Add("LAVA")
            mExchangeCodes.Add("LIFFE")
            mExchangeCodes.Add("LIFFE_NF")
            mExchangeCodes.Add("LSE")
            mExchangeCodes.Add("LSEIOB1")
            mExchangeCodes.Add("LSSF")

            mExchangeCodes.Add("MATIF")
            mExchangeCodes.Add("MEFF")
            mExchangeCodes.Add("MEFFRV")
            mExchangeCodes.Add("MEXI")
            mExchangeCodes.Add("MONEP")
            mExchangeCodes.Add("MXT")

            mExchangeCodes.Add("NASDAQ")
            mExchangeCodes.Add("NASDAQBX")
            mExchangeCodes.Add("NASDAQOM")
            mExchangeCodes.Add("NQLX")
            mExchangeCodes.Add("NSE")
            mExchangeCodes.Add("NSX")
            mExchangeCodes.Add("NYBOT")
            mExchangeCodes.Add("NYMEX")
            mExchangeCodes.Add("NYSE")

            mExchangeCodes.Add("OMS")
            mExchangeCodes.Add("ONE")
            mExchangeCodes.Add("OSE.JPN")

            mExchangeCodes.Add("PHLX")
            mExchangeCodes.Add("PINK")
            mExchangeCodes.Add("PSE")
            mExchangeCodes.Add("PSX")

            mExchangeCodes.Add("RDBK")

            mExchangeCodes.Add("SBF")
            mExchangeCodes.Add("SFB")
            mExchangeCodes.Add("SGX")
            mExchangeCodes.Add("SMART")
            mExchangeCodes.Add("SNFE")
            mExchangeCodes.Add("SOFFEX")
            mExchangeCodes.Add("SWB")
            mExchangeCodes.Add("SWX")

            mExchangeCodes.Add("TRACKECN")
            mExchangeCodes.Add("TRQXEN")
            mExchangeCodes.Add("TSE")
            mExchangeCodes.Add("TSE.JPN")

            mExchangeCodes.Add("VENTURE")
            mExchangeCodes.Add("VIRTX")
            mExchangeCodes.Add("VWAP")

            mExchangeCodes.Sort()
        End Sub


        Public Function GetExchangeCodes() As List(Of String)
            Return mExchangeCodes
        End Function

        Public Function IsValidExchangeCode(code As String) As Boolean
            Return mExchangeCodes.BinarySearch(code) >= 0
        End Function

    End Module
End Namespace

