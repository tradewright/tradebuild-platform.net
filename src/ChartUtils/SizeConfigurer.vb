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
Imports System.ComponentModel

<TypeConverter(GetType(SizeConfigurerConverter))>
Public Class SizeConfigurer

    Private Structure SizeInfo
        Private sCoordinateSystemHeight As CoordinateSystems
        Private sCoordinateSystemWidth As CoordinateSystems
        Private sHeight As String
        Private sWidth As String

        Sub New(coordinateSystemWidth As CoordinateSystems, coordinateSystemHeight As CoordinateSystems, width As String, height As String)
            sCoordinateSystemHeight = coordinateSystemHeight
            sCoordinateSystemWidth = coordinateSystemWidth
            sHeight = height
            sWidth = width
        End Sub

        ReadOnly Property CoordinateSystemHeight As CoordinateSystems
            Get
                Return sCoordinateSystemHeight
            End Get
        End Property

        ReadOnly Property CoordinateSystemWidth As CoordinateSystems
            Get
                Return sCoordinateSystemWidth
            End Get
        End Property

        ReadOnly Property Height As String
            Get
                Return sHeight
            End Get
        End Property

        ReadOnly Property Width As String
            Get
                Return sWidth
            End Get
        End Property

    End Structure

    Public Event ValueChanged(sender As Object, e As EventArgs)

    Private Shared _ChartSkil As ChartSkil27.ChartSkil = New ChartSkil27.ChartSkil
    Friend Shared _NullSize As ChartSkil27.Size = _ChartSkil.NewSize(0, 0, CoordinateSystems.CoordsDistance, CoordinateSystems.CoordsDistance)

    Private mSize As ChartSkil27.Size

    Private mSizeInfo As SizeInfo

    Public Sub New(size As ChartSkil27.Size)
        mSize = size
        mSizeInfo = getSizeInfo(size)
    End Sub

    <Category("Dimensions"), Browsable(True),
    DefaultValue(CoordinateSystems.CoordsDistance),
    Description("Specifies the coordinate system for the height.")>
    Public Property CoordinateSystemHeight() As CoordinateSystems
        Get
            Return mSizeInfo.CoordinateSystemHeight
        End Get
        Set
            mSizeInfo = New SizeInfo(mSizeInfo.CoordinateSystemWidth, value, mSizeInfo.Width, mSizeInfo.Height)
            setNewValue()
        End Set
    End Property

    <Category("Dimensions"), Browsable(True),
    DefaultValue(CoordinateSystems.CoordsDistance),
    Description("Specifies the coordinate system for the width.")>
    Public Property CoordinateSystemWidth() As CoordinateSystems
        Get
            Return mSizeInfo.CoordinateSystemWidth
        End Get
        Set
            mSizeInfo = New SizeInfo(value, mSizeInfo.CoordinateSystemHeight, mSizeInfo.Width, mSizeInfo.Height)
            setNewValue()
        End Set
    End Property

    <Category("Dimensions"), Browsable(True),
    DefaultValue("0"),
    Description("Specifies the height.")>
    Public Property Height() As String
        Get
            Return mSizeInfo.Height
        End Get
        Set
            mSizeInfo = New SizeInfo(mSizeInfo.CoordinateSystemWidth, mSizeInfo.CoordinateSystemHeight, mSizeInfo.Width, value)
            setNewValue()
        End Set
    End Property

    <Category("Dimensions"), Browsable(True),
    DefaultValue(False),
    Description("Specifies that no size is to be set.")>
    Public Property None() As Boolean
        Get
            Return mSize Is Nothing
        End Get
        Set
            If value Then
                setValue(Nothing)
            Else
                setValue(_NullSize)
            End If
        End Set
    End Property

    <Category("Dimensions"), Browsable(True),
    DefaultValue("0"),
    Description("Specifies the width.")>
    Public Property Width() As String
        Get
            Return mSizeInfo.Width
        End Get
        Set
            mSizeInfo = New SizeInfo(mSizeInfo.CoordinateSystemWidth, mSizeInfo.CoordinateSystemHeight, value, mSizeInfo.Height)
            setNewValue()
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property Value As ChartSkil27.Size
        Get
            Return mSize
        End Get
    End Property

    Private Function getSizeInfo(size As ChartSkil27.Size) As SizeInfo
        If mSize IsNot Nothing Then
            Return New SizeInfo(mSize.CoordinateSystemWidth, mSize.CoordinateSystemHeight, CStr(mSize.Width), CStr(mSize.Height))
        Else
            Return New SizeInfo(CoordinateSystems.CoordsDistance, CoordinateSystems.CoordsDistance, "", "")
        End If
    End Function

    Private Sub setNewValue()
        Dim width As Double
        Dim height As Double
        If Not Double.TryParse(mSizeInfo.Width, width) Then Exit Sub
        If Not Double.TryParse(mSizeInfo.Height, height) Then Exit Sub
        setValue(_ChartSkil.NewSize(width, height, mSizeInfo.CoordinateSystemWidth, mSizeInfo.CoordinateSystemHeight))
    End Sub

    Private Sub setValue(size As ChartSkil27.Size)
        mSize = size
        RaiseEvent ValueChanged(Me, New EventArgs)
    End Sub

End Class

Friend NotInheritable Class SizeConfigurerConverter
    Inherits ExpandableObjectConverter

    Private Shared _ChartSkil As ChartSkil27.ChartSkil = New ChartSkil27.ChartSkil

    Public Overrides Function CanConvertFrom(context As System.ComponentModel.ITypeDescriptorContext, sourceType As System.Type) As Boolean
        If sourceType Is GetType(System.String) Then Return True
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overrides Function ConvertFrom(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object) As Object
        If TypeOf value Is System.String Then
            Dim s = CStr(value)
            If String.IsNullOrEmpty(s) OrElse String.Compare(s, "(None)", True) = 0 OrElse String.Compare(s, "None", True) = 0 Then Return Nothing
            Dim tokens() = s.Split({","c})
            Return New SizeConfigurer(_ChartSkil.NewSize(CDbl(tokens(0)),
                                      CDbl(tokens(1)),
                                      parseCoordinate(tokens(2)),
                                      parseCoordinate(tokens(3))))
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overrides Function ConvertTo(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object, destinationType As System.Type) As Object
        If destinationType Is GetType(System.String) Then
            If TypeOf value Is SizeConfigurer Then
                If CType(value, SizeConfigurer).None Then Return "(None)"
                Dim size = CType(value, SizeConfigurer).Value
                If size Is Nothing Then Return "(None)"
                Return size.Width & "," & size.Height & "," & size.CoordinateSystemWidth.ToString & "," & size.CoordinateSystemHeight.ToString
            End If
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

    Public Overrides Function CreateInstance(context As System.ComponentModel.ITypeDescriptorContext, propertyValues As System.Collections.IDictionary) As Object
        Return _ChartSkil.NewSize(CDbl(propertyValues.Item("Width")),
                                  CDbl(propertyValues.Item("Height")),
                                  CType(propertyValues.Item("CoordinateSystemWidth"), CoordinateSystems),
                                  CType(propertyValues.Item("CoordinateSystemHeight"), CoordinateSystems))
    End Function

    Public Overrides Function GetProperties(context As System.ComponentModel.ITypeDescriptorContext, value As Object, attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
        Return MyBase.GetProperties(context, value, attributes).Sort({"None", "Width", "Height", "CoordinateSystemWidth", "CoordinateSystemHeight"})
    End Function

    Public Overrides Function GetPropertiesSupported(context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Private Function parseCoordinate(source As String) As CoordinateSystems
        Dim value As CoordinateSystems
        If Not [Enum].TryParse(Of CoordinateSystems)(source, True, value) Then Throw New ArgumentException
        If Not [Enum].IsDefined(GetType(CoordinateSystems), value) Then Throw New ArgumentException
    End Function

End Class
