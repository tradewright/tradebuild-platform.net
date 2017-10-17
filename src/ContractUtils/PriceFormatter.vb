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

Imports System.Globalization
Imports System.Text.RegularExpressions

Namespace Contracts
    Public NotInheritable Class PriceFormatter

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

        Private Structure TickSizePatternEntry
            Dim TickSize As Double
            Dim Pattern As String
        End Structure

#End Region

#Region "Constants"

        Public Const OneTenth As Double = 0.01
        Public Const OneHalf As Double = 0.5
        Public Const OneQuarter As Double = 0.25
        Public Const OneEigth As Double = 0.125
        Public Const OneSixteenth As Double = 0.0625
        Public Const OneThirtySecond As Double = 0.03125
        Public Const OneSixtyFourth As Double = 0.015625
        Public Const OneHundredTwentyEighth As Double = 0.0078125

#End Region

#Region "Member variables"

        Private mPriceFormatStrings As New List(Of TickSizePatternEntry)

        Private mParsePriceAs32ndsPattern As String
        Private mParsePriceAs32ndsAndFractionsPattern As String
        Private mParsePriceAs64thsPattern As String
        Private mParsePriceAs64thsAndFractionsPattern As String

        Private mParsePriceAsDecimalsPatterns As New List(Of TickSizePatternEntry)

        Private mThirtySecondsSeparators() As String = {"'"}
        Private mThirtySecondsTerminators() As String = {"", "/32"}

        Private mThirtySecondsAndFractionsSeparators() As String = {"'"}
        Private mThirtySecondsAndFractionsTerminators() As String = {"", "/32"}

        Private mSixtyFourthsSeparators() As String = {"", "''"}
        Private mSixtyFourthsTerminators() As String = {"", "/64"}

        Private mSixtyFourthsAndFractionsSeparators() As String = {"''"}
        Private mSixtyFourthsAndFractionsTerminators() As String = {"", "/64"}

        Private mExactThirtySecondIndicators() As String = {"", "0"}
        Private mQuarterThirtySecondIndicators() As String = {"¼", "2"}
        Private mHalfThirtySecondIndicators() As String = {"+", "5"}
        Private mThreeQuarterThirtySecondIndicators() As String = {"¾", "7"}

        Private mExactSixtyFourthIndicators() As String = {"", "''"}
        Private mHalfSixtyFourthIndicators() As String = {"+", "5"}

#End Region

#Region "Constructors"

        Public Sub New()
            generate32ndsAndFractionsPattern(mThirtySecondsAndFractionsSeparators, mThirtySecondsAndFractionsTerminators, mExactThirtySecondIndicators, mQuarterThirtySecondIndicators, mHalfThirtySecondIndicators, mThreeQuarterThirtySecondIndicators)
            generate32ndsPattern(mThirtySecondsSeparators, mThirtySecondsTerminators)
            generate64thsAndFractionsPattern(mSixtyFourthsAndFractionsSeparators, mSixtyFourthsAndFractionsTerminators, mExactSixtyFourthIndicators, mHalfSixtyFourthIndicators)
            generate64thsPattern(mSixtyFourthsSeparators, mSixtyFourthsTerminators)
        End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

        Public ReadOnly Property DefaultExactSixtyFourthIndicator() As String
            Get
                Return mExactSixtyFourthIndicators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultExactThirtySecondIndicator() As String
            Get
                Return mExactThirtySecondIndicators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultHalfSixtyFourthIndicator() As String
            Get
                Return mHalfSixtyFourthIndicators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultHalfThirtySecondIndicator() As String
            Get
                Return mHalfThirtySecondIndicators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultQuarterThirtySecondIndicator() As String
            Get
                Return mQuarterThirtySecondIndicators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultSixtyFourthsAndFractionsSeparator() As String
            Get
                Return mSixtyFourthsAndFractionsSeparators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultSixtyFourthsAndFractionsTerminator() As String
            Get
                Return mSixtyFourthsAndFractionsTerminators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultSixtyFourthsSeparator() As String
            Get
                Return mSixtyFourthsSeparators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultSixtyFourthsTerminator() As String
            Get
                Return mSixtyFourthsTerminators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultThirtySecondsAndFractionsSeparator() As String
            Get
                Return mThirtySecondsAndFractionsSeparators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultThirtySecondsAndFractionsTerminator() As String
            Get
                Return mThirtySecondsAndFractionsTerminators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultThirtySecondsSeparator() As String
            Get
                Return mThirtySecondsSeparators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultThirtySecondsTerminator() As String
            Get
                Return mThirtySecondsTerminators(0)
            End Get
        End Property

        Public ReadOnly Property DefaultThreeQuarterThirtySecondIndicator() As String
            Get
                Return mThreeQuarterThirtySecondIndicators(0)
            End Get
        End Property

        Public Property ExactSixtyFourthIndicators() As String()
            Get
                Return mExactSixtyFourthIndicators
            End Get
            Set(Value As String())
                mExactSixtyFourthIndicators = Value
                generate64thsAndFractionsPattern(mSixtyFourthsAndFractionsSeparators, mSixtyFourthsAndFractionsTerminators, mExactSixtyFourthIndicators, mHalfSixtyFourthIndicators)
            End Set
        End Property

        Public Property ExactThirtySecondIndicators() As String()
            Get
                Return mExactThirtySecondIndicators
            End Get
            Set(Value As String())
                mExactThirtySecondIndicators = Value
                generate32ndsAndFractionsPattern(mThirtySecondsAndFractionsSeparators, mThirtySecondsAndFractionsTerminators, mExactThirtySecondIndicators, mQuarterThirtySecondIndicators, mHalfThirtySecondIndicators, mThreeQuarterThirtySecondIndicators)
            End Set
        End Property

        Public Property HalfSixtyFourthIndicators() As String()
            Get
                Return mHalfSixtyFourthIndicators
            End Get
            Set(Value As String())
                mHalfSixtyFourthIndicators = Value
                generate64thsAndFractionsPattern(mSixtyFourthsAndFractionsSeparators, mSixtyFourthsAndFractionsTerminators, mExactSixtyFourthIndicators, mHalfSixtyFourthIndicators)
            End Set
        End Property

        Public Property HalfThirtySecondIndicators() As String()
            Get
                Return mHalfThirtySecondIndicators
            End Get
            Set(Value As String())
                mHalfThirtySecondIndicators = Value
                generate32ndsAndFractionsPattern(mThirtySecondsAndFractionsSeparators, mThirtySecondsAndFractionsTerminators, mExactThirtySecondIndicators, mQuarterThirtySecondIndicators, mHalfThirtySecondIndicators, mThreeQuarterThirtySecondIndicators)
            End Set
        End Property

        Public ReadOnly Property ParsePriceAs32ndsPattern() As String
            Get
                Return mParsePriceAs32ndsPattern
            End Get
        End Property

        Public ReadOnly Property ParsePriceAs32ndsAndFractionsPattern() As String
            Get
                Return mParsePriceAs32ndsAndFractionsPattern
            End Get
        End Property

        Public ReadOnly Property ParsePriceAs64thsPattern() As String
            Get
                Return mParsePriceAs64thsPattern
            End Get
        End Property

        Public ReadOnly Property ParsePriceAs64thsAndFractionsPattern() As String
            Get
                Return mParsePriceAs64thsAndFractionsPattern
            End Get
        End Property

        Public Property QuarterThirtySecondIndicators() As String()
            Get
                Return mQuarterThirtySecondIndicators
            End Get
            Set(Value As String())
                mQuarterThirtySecondIndicators = Value
                generate32ndsAndFractionsPattern(mThirtySecondsAndFractionsSeparators, mThirtySecondsAndFractionsTerminators, mExactThirtySecondIndicators, mQuarterThirtySecondIndicators, mHalfThirtySecondIndicators, mThreeQuarterThirtySecondIndicators)
            End Set
        End Property

        Public Property SixtyFourthsAndFractionsSeparators() As String()
            Get
                Return mSixtyFourthsAndFractionsSeparators
            End Get
            Set(Value As String())
                mSixtyFourthsAndFractionsSeparators = Value
                generate64thsAndFractionsPattern(mSixtyFourthsAndFractionsSeparators, mSixtyFourthsAndFractionsTerminators, mExactSixtyFourthIndicators, mHalfSixtyFourthIndicators)
            End Set
        End Property

        Public Property SixtyFourthsAndFractionsTerminators() As String()
            Get
                Return mSixtyFourthsAndFractionsTerminators
            End Get
            Set(Value As String())
                mSixtyFourthsAndFractionsTerminators = Value
                generate64thsAndFractionsPattern(mSixtyFourthsAndFractionsSeparators, mSixtyFourthsAndFractionsTerminators, mExactSixtyFourthIndicators, mHalfSixtyFourthIndicators)
            End Set
        End Property

        Public Property SixtyFourthsSeparators() As String()
            Get
                Return mSixtyFourthsSeparators
            End Get
            Set(Value As String())
                mSixtyFourthsSeparators = Value
                generate64thsAndFractionsPattern(mSixtyFourthsAndFractionsSeparators, mSixtyFourthsAndFractionsTerminators, mExactSixtyFourthIndicators, mHalfSixtyFourthIndicators)
            End Set
        End Property

        Public Property SixtyFourthsTerminators() As String()
            Get
                Return mSixtyFourthsTerminators
            End Get
            Set(Value As String())
                mSixtyFourthsTerminators = Value
                generate64thsAndFractionsPattern(mSixtyFourthsAndFractionsSeparators, mSixtyFourthsAndFractionsTerminators, mExactSixtyFourthIndicators, mHalfSixtyFourthIndicators)
            End Set
        End Property

        Public Property ThirtySecondsAndFractionsSeparators() As String()
            Get
                Return mThirtySecondsAndFractionsSeparators
            End Get
            Set(Value As String())
                mThirtySecondsAndFractionsSeparators = Value
                generate32ndsAndFractionsPattern(mThirtySecondsAndFractionsSeparators, mThirtySecondsAndFractionsTerminators, mExactThirtySecondIndicators, mQuarterThirtySecondIndicators, mHalfThirtySecondIndicators, mThreeQuarterThirtySecondIndicators)
            End Set
        End Property

        Public Property ThirtySecondsAndFractionsTerminators() As String()
            Get
                Return mThirtySecondsAndFractionsTerminators
            End Get
            Set(Value As String())
                mThirtySecondsAndFractionsTerminators = Value
                generate32ndsAndFractionsPattern(mThirtySecondsAndFractionsSeparators, mThirtySecondsAndFractionsTerminators, mExactThirtySecondIndicators, mQuarterThirtySecondIndicators, mHalfThirtySecondIndicators, mThreeQuarterThirtySecondIndicators)
            End Set
        End Property

        Public Property ThirtySecondsSeparators() As String()
            Get
                Return mThirtySecondsSeparators
            End Get
            Set(Value As String())
                mThirtySecondsSeparators = Value
                generate32ndsPattern(mThirtySecondsSeparators, mThirtySecondsTerminators)
            End Set
        End Property

        Public Property ThirtySecondsTerminators() As String()
            Get
                Return mThirtySecondsTerminators
            End Get
            Set(Value As String())
                mThirtySecondsTerminators = Value
                generate32ndsPattern(mThirtySecondsSeparators, mThirtySecondsTerminators)
            End Set
        End Property

        Public Property ThreeQuarterThirtySecondIndicators() As String()
            Get
                Return mThreeQuarterThirtySecondIndicators
            End Get
            Set(Value As String())
                mThreeQuarterThirtySecondIndicators = Value
                generate32ndsAndFractionsPattern(mThirtySecondsAndFractionsSeparators, mThirtySecondsAndFractionsTerminators, mExactThirtySecondIndicators, mQuarterThirtySecondIndicators, mHalfThirtySecondIndicators, mThreeQuarterThirtySecondIndicators)
            End Set
        End Property

#End Region

#Region "Methods"

        Public Function FormatPrice(pPrice As Double, pSecType As SecurityType, pTickSize As Double) As String
            ' see http://www.cmegroup.com/trading/interest-rates/files/TreasuryFuturesPriceRoundingConventions_Mar_24_Final.pdf
            ' for details of price presentation, especially sections (2) and (7)

            If pTickSize = OneThirtySecond Then Return FormatPriceAs32nds(pPrice)

            If pTickSize = OneSixtyFourth Then
                If pSecType = SecurityType.Future Then Return FormatPriceAs32ndsAndFractions(pPrice)
                Return FormatPriceAs64ths(pPrice)
            End If

            If pTickSize = OneHundredTwentyEighth Then
                If pSecType = SecurityType.Future Then Return FormatPriceAs32ndsAndFractions(pPrice)
                Return FormatPriceAs64thsAndFractions(pPrice)
            End If

            Return FormatPriceAsDecimals(pPrice, pTickSize)
        End Function

        Public Function FormatPriceAs32nds(pPrice As Double) As String
            Dim priceInt = Int(pPrice)
            Dim fract = pPrice - priceInt
            Dim numerator = fract * 32
            Return String.Format("{0:d}" & DefaultThirtySecondsSeparator & "{1:00}" & DefaultThirtySecondsTerminator, CInt(priceInt), numerator)
        End Function

        Public Function FormatPriceAs32ndsAndFractions(pPrice As Double) As String
            Dim priceInt = Int(pPrice)
            Dim fract = pPrice - priceInt
            Dim numerator = CInt(Int(fract * 128))
            Dim priceString = String.Format("{0:d}" & DefaultThirtySecondsAndFractionsSeparator & "{1:00}", CInt(priceInt), CInt(Int(numerator \ 4)))
            Select Case numerator Mod 4
                Case 0
                    priceString = priceString & DefaultExactThirtySecondIndicator
                Case 1
                    priceString = priceString & DefaultQuarterThirtySecondIndicator
                Case 2
                    priceString = priceString & DefaultHalfThirtySecondIndicator
                Case 3
                    priceString = priceString & DefaultThreeQuarterThirtySecondIndicator
            End Select

            Return priceString & DefaultThirtySecondsAndFractionsTerminator
        End Function

        Public Function FormatPriceAs64ths(pPrice As Double) As String
            Dim priceInt = Int(pPrice)
            Dim fract = pPrice - priceInt
            Dim numerator = CInt(Int(fract * 64))
            Return String.Format("{0:d}" & DefaultSixtyFourthsSeparator & "{1:00}" & DefaultSixtyFourthsTerminator, CInt(priceInt), numerator)
        End Function

        Public Function FormatPriceAs64thsAndFractions(pPrice As Double) As String
            Dim priceInt = Int(pPrice)
            Dim fract = pPrice - priceInt
            Dim numerator = CInt(Int(fract * 128))
            Dim priceString = String.Format("{0:d}" & DefaultSixtyFourthsAndFractionsSeparator & "{1:00}", CInt(priceInt), CInt(Int(numerator \ 2)))
            Select Case numerator Mod 2
                Case 0
                    priceString = priceString & DefaultExactSixtyFourthIndicator
                Case 1
                    priceString = priceString & DefaultHalfSixtyFourthIndicator
            End Select

            Return priceString & DefaultSixtyFourthsAndFractionsTerminator
        End Function

        Public Function FormatPriceAsDecimals(pPrice As Double, pTickSize As Double) As String
            FormatPriceAsDecimals = pPrice.ToString(getPriceFormatString(pTickSize))
        End Function

        Public Function ParsePrice(pPriceString As String, pSecType As SecurityType, pTickSize As Double, ByRef pPrice As Double) As Boolean
            pPriceString = Trim(pPriceString)

            If pTickSize = OneThirtySecond Then Return ParsePriceAs32nds(pPriceString, pPrice)

            If pTickSize = OneSixtyFourth Then
                If pSecType = SecurityType.Future Then Return ParsePriceAs32ndsAndFractions(pPriceString, pPrice)
                Return ParsePriceAs64ths(pPriceString, pPrice)
            End If

            If pTickSize = OneHundredTwentyEighth Then
                If pSecType = SecurityType.Future Then Return ParsePriceAs32ndsAndFractions(pPriceString, pPrice)
                Return ParsePriceAs64thsAndFractions(pPriceString, pPrice)
            End If

            Return ParsePriceAsDecimals(pPriceString, pTickSize, pPrice)
        End Function

        Public Function ParsePriceAs32nds(pPriceString As String, ByRef pPrice As Double) As Boolean
            Dim matches = Regex.Matches(pPriceString, ParsePriceAs32ndsPattern)
            If matches.Count = 0 Then Return False

            pPrice = Double.Parse(matches(0).Value)
            If matches.Count = 4 Then pPrice = pPrice + Double.Parse(matches(3).Value) / 32.0

            Return True
        End Function

        Public Function ParsePriceAs32ndsAndFractions(pPriceString As String, ByRef pPrice As Double) As Boolean
            Dim matches = Regex.Matches(pPriceString, ParsePriceAs32ndsAndFractionsPattern)
            If matches.Count = 0 Then Return False

            pPrice = Double.Parse(matches(0).Value)
            If matches.Count = 4 Then pPrice = pPrice + Double.Parse(matches(3).Value) / 32.0

            If matches.Count = 5 Then
                If memberOf(matches(4).Value, QuarterThirtySecondIndicators) Then
                    pPrice = pPrice + 1 / 128
                ElseIf memberOf(matches(4).Value, HalfThirtySecondIndicators) Then
                    pPrice = pPrice + 1 / 64
                ElseIf memberOf(matches(4).Value, ThreeQuarterThirtySecondIndicators) Then
                    pPrice = pPrice + 3 * 3 / 128
                End If
            End If

            Return True
        End Function

        Public Function ParsePriceAs64ths(pPriceString As String, ByRef pPrice As Double) As Boolean
            Dim matches = Regex.Matches(pPriceString, ParsePriceAs64thsPattern)
            If matches.Count = 0 Then Return False

            pPrice = Double.Parse(matches(0).Value)
            If matches.Count = 4 Then pPrice = pPrice + Double.Parse(matches(3).Value) / 64.0

            Return True
        End Function

        Public Function ParsePriceAs64thsAndFractions(pPriceString As String, ByRef pPrice As Double) As Boolean
            Dim matches = Regex.Matches(pPriceString, ParsePriceAs64thsAndFractionsPattern)
            If matches.Count = 0 Then Return False

            pPrice = Double.Parse(matches(0).Value)
            If matches.Count = 4 Then pPrice = pPrice + Double.Parse(matches(3).Value) / 64.0

            If matches.Count = 5 Then
                If memberOf(matches(4).Value, HalfSixtyFourthIndicators) Then
                    pPrice = pPrice + OneHundredTwentyEighth
                End If
            End If

            Return True
        End Function

        Public Function ParsePriceAsDecimals(pPriceString As String, pTickSize As Double, ByRef pPrice As Double) As Boolean
            If Regex.IsMatch(pPriceString, getParsePriceAsDecimalsPattern(pTickSize)) Then

                ' don't use CDBL here as we don't want to follow locale conventions (ie decimal point
                ' must be a period here)
                pPrice = Double.Parse(pPriceString, CultureInfo.InvariantCulture)
                Return True
            End If
            Return False
        End Function

#End Region

#Region "Helper Functions"

        Private Function generatePriceFormatString(pTickSize As Double) As String
            Dim lNumberOfDecimals = pTickSize.ToString("0.##############").Length - 2

            If lNumberOfDecimals = 0 Then
                Return "0"
            Else
                Return "0." & New String("0"c, lNumberOfDecimals)
            End If
        End Function

        Private Function getPriceFormatString(pTickSize As Double) As String
            For i = 0 To mPriceFormatStrings.Count - 1
                If mPriceFormatStrings(i).TickSize = pTickSize Then Return mPriceFormatStrings(i).Pattern
            Next

            Dim pattern = generatePriceFormatString(pTickSize)
            mPriceFormatStrings.Add(New TickSizePatternEntry With {.TickSize = pTickSize, .Pattern = pattern})
            Return pattern
        End Function

        Private Function convertToRegexpChoices(ByRef choiceStrings() As String) As String
            Dim s = ""

            For i = 0 To UBound(choiceStrings)
                If i <> 0 Then s = s & "|"
                s = s & escapeRegexSpecialChar(choiceStrings(i))
            Next

            convertToRegexpChoices = s
        End Function

        Private Function escapeRegexSpecialChar(ByRef inString As String) As String
            Dim s = ""
            For i = 1 To Len(inString)
                Dim ch = Mid(inString, i, 1)
                Select Case ch
                    Case "*", "+", "?", "^", "$", "[", "]", "{", "}", "(", ")", "|", "/", "\"
                        s = s & "\" & ch
                    Case Else
                        s = s & ch
                End Select
            Next

            escapeRegexSpecialChar = s
        End Function

        Private Sub generate32ndsPattern(separators() As String, terminators() As String)
            mParsePriceAs32ndsPattern =
                            "^(\d+)" &
                            "(" &
                                "($" &
                                    "|" & convertToRegexpChoices(separators) &
                                ")" &
                                "([0-2][0-9]|30|31)" &
                                "(" &
                                      convertToRegexpChoices(terminators) &
                                ")$" &
                            ")"
        End Sub

        Private Sub generate32ndsAndFractionsPattern(separators() As String, terminators() As String, exactIndicators() As String, quarterIndicators() As String, halfIndicators() As String, threeQuarterIndicators() As String)
            mParsePriceAs32ndsAndFractionsPattern =
                            "^(\d+)" &
                            "(" &
                                "($" &
                                    "|" & convertToRegexpChoices(separators) &
                                ")" &
                                "([0-2][0-9]|30|31)" &
                                "(" &
                                    convertToRegexpChoices(exactIndicators) &
                                    "|" & convertToRegexpChoices(quarterIndicators) &
                                    "|" & convertToRegexpChoices(halfIndicators) &
                                    "|" & convertToRegexpChoices(threeQuarterIndicators) &
                                ")" &
                                "(" &
                                    convertToRegexpChoices(terminators) &
                                ")$" &
                            ")"
        End Sub

        Private Sub generate64thsPattern(separators() As String, terminators() As String)
            mParsePriceAs64thsPattern =
                            "^(\d+)" &
                            "(" &
                                "($" &
                                    "|" & convertToRegexpChoices(separators) &
                                ")" &
                                "([0-5][0-9]|60|61|62|63)" &
                                "(" &
                                    convertToRegexpChoices(terminators) &
                                ")$" &
                            ")"
        End Sub

        Private Sub generate64thsAndFractionsPattern(separators() As String, terminators() As String, exactIndicators() As String, halfIndicators() As String)
            mParsePriceAs64thsAndFractionsPattern =
                            "^(\d+)" &
                            "(" &
                                "($" &
                                    "|" & convertToRegexpChoices(separators) &
                                ")" &
                                "([0-5][0-9]|60|61|62|63)" &
                                "(" &
                                    convertToRegexpChoices(exactIndicators) &
                                    "|" & convertToRegexpChoices(halfIndicators) &
                                ")" &
                                "(" &
                                    "|" & convertToRegexpChoices(terminators) &
                                ")$" &
                            ")"
        End Sub

        Private Function generateDecimalsPattern(pTickSize As Double) As String
            Dim minTickString = String.Format("0.##############", pTickSize)

            Dim numberOfDecimals = Len(minTickString) - 2

            Return "^\d+($" & "|\.\d{1," & numberOfDecimals & "}$)"
        End Function

        Private Function getParsePriceAsDecimalsPattern(pTickSize As Double) As String
            For i = 0 To mParsePriceAsDecimalsPatterns.Count - 1
                If mParsePriceAsDecimalsPatterns(i).TickSize = pTickSize Then
                    Return mParsePriceAsDecimalsPatterns(i).Pattern
                    Exit Function
                End If
            Next

            Dim pattern = generateDecimalsPattern(pTickSize)
            mParsePriceAsDecimalsPatterns.Add(New TickSizePatternEntry With {.TickSize = pTickSize, .Pattern = pattern})
            Return pattern
        End Function

        Private Function memberOf(pInstring As String, ByRef pChoices() As String) As Boolean
            For i = 0 To UBound(pChoices)
                If pChoices(i) = pInstring Then Return True
            Next
            Return False
        End Function

#End Region

End Class
End Namespace
