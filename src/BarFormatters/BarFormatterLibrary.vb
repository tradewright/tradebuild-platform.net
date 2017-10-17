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

Public MustInherit Class BarFormatterLibrary

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

    Private Structure FactoryEntry
        Dim Name As String
        Dim FactoryType As Type
    End Structure

#End Region

#Region "Constants"

#End Region

#Region "Member variables"

    Private mName As String

    Private mFactories As New List(Of FactoryEntry)

#End Region

#Region "Constructors"

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Property Name() As String
        Get
            Return mName
        End Get
        Set
            mName = Value
        End Set
    End Property

#End Region

#Region "Methods"

    Public Function CreateFactory(name As String) As IBarFormatterFactory
        For Each factory In mFactories
            If factory.Name = name Then
                Return DirectCast(Activator.CreateInstance(factory.FactoryType), IBarFormatterFactory)
            End If
        Next
        Throw New ArgumentException("Invalid BarFormatterFactory name")
    End Function

    Function GetFactoryNames() As IEnumerable(Of String)
        Return From e In mFactories Select e.Name
    End Function

    Protected Sub RegisterFactory(name As String, type As Type)
        mFactories.Add(New FactoryEntry With {.Name = name, .FactoryType = type})
    End Sub

#End Region

#Region "Helper Functions"

#End Region

End Class
