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

Imports ExtProps40
Imports System.ComponentModel

<TypeConverter(GetType(ExtendedPropertyWrapperConverter))> _
Public NotInheritable Class ExtendedPropertyValueConfigurer(Of T)
    Inherits ExtendedPropertyWrapper

    Private mFromFunc As Func(Of Object, T)
    Private mToFunc As Func(Of T, Object)

    Public Sub New(host As ExtendedPropertyHost, prop As ExtendedProperty)
        Me.New(host, prop, Nothing, Nothing)
    End Sub

    Public Sub New(host As ExtendedPropertyHost, prop As ExtendedProperty, fromFunc As Func(Of Object, T), toFunc As Func(Of T, Object))
        MyBase.New(host, prop)
        If prop.ValueIsObject Then Throw New ArgumentException("Extended property must not have a value that is an object")
        mFromFunc = fromFunc
        mToFunc = toFunc
    End Sub

    <Category("Appearance"), Browsable(True), _
    Description("Specifies the value for this setting. If this value is inherited, you cannot change it.")> _
    Public Property Value As T
        Get
            If mFromFunc Is Nothing Then
                Return CType(mHost.GetValue(mProperty), T)
            Else
                Return mFromFunc(mHost.GetValue(mProperty))
            End If
        End Get
        Set
            If Inherited Then Return
            If mToFunc Is Nothing Then
                mHost.SetValue(mProperty, CType(Value, T))
            Else
                mHost.SetValue(mProperty, mToFunc(Value))
            End If
        End Set
    End Property

    Protected Overrides Sub setLocalValue()
        Dim inheritedValue As T
        If mFromFunc Is Nothing Then
            inheritedValue = CType(mHost.GetValue(mProperty), T)
        Else
            inheritedValue = mFromFunc(mHost.GetValue(mProperty))
        End If

        If mToFunc Is Nothing Then
            mHost.SetValue(mProperty, inheritedValue)
        Else
            mHost.SetValue(mProperty, mToFunc(inheritedValue))
        End If
    End Sub
End Class
