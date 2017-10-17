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

Imports System.Runtime.CompilerServices

Module FontExtensions
    <Extension()>
    Public Function ToFont(stdFont As stdole.StdFont) As Font
        Dim FontStyle As FontStyle = FontStyle.Regular
        If stdFont.Bold Then FontStyle = FontStyle Or FontStyle.Bold
        If stdFont.Italic Then FontStyle = FontStyle Or FontStyle.Italic
        If stdFont.Strikethrough Then FontStyle = FontStyle Or FontStyle.Strikeout
        If stdFont.Underline Then FontStyle = FontStyle Or FontStyle.Underline

        Return New System.Drawing.Font(stdFont.Name, stdFont.Size, FontStyle)
    End Function

    <Extension()>
    Public Function ToStdFont(font As Font) As stdole.StdFont
        Dim stdFont As New stdole.StdFont
        stdFont.Name = font.FontFamily.Name
        stdFont.Bold = font.Bold
        stdFont.Italic = font.Italic
        stdFont.Size = CType(font.Size, Decimal)
        stdFont.Strikethrough = font.Strikeout
        stdFont.Underline = font.Underline
        Return stdFont
    End Function

End Module
