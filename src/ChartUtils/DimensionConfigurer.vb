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

<TypeConverter(GetType(DimensionConfigurerConverter))>
Public Class DimensionConfigurer

    Private Structure DimensionInfo
        Private sCoordinateSystem As CoordinateSystems
        Private sLength As String

        Sub New(coordinateSystem As CoordinateSystems, length As String)
            sCoordinateSystem = coordinateSystem
            sLength = length
        End Sub

        ReadOnly Property CoordinateSystem As CoordinateSystems
            Get
                Return sCoordinateSystem
            End Get
        End Property

        ReadOnly Property Length As String
            Get
                Return sLength
            End Get
        End Property

    End Structure

    Public Event ValueChanged(sender As Object, e As EventArgs)

    Private Shared _ChartSkil As ChartSkil27.ChartSkil = New ChartSkil27.ChartSkil
    Friend Shared _NullDimension As ChartSkil27.Dimension = _ChartSkil.NewDimension(0, CoordinateSystems.CoordsDistance)

    Private mDimension As ChartSkil27.Dimension

    Private mDimensionInfo As DimensionInfo

    Public Sub New(dimension As ChartSkil27.Dimension)
        mDimension = dimension
        mDimensionInfo = getSizeInfo(dimension)
    End Sub

    <Category("Dimensions"), Browsable(True),
    DefaultValue(CoordinateSystems.CoordsDistance),
    Description("Specifies the coordinate system.")>
    Public Property CoordinateSystem() As CoordinateSystems
        Get
            Return mDimensionInfo.CoordinateSystem
        End Get
        Set
            mDimensionInfo = New DimensionInfo(value, mDimensionInfo.Length)
            setNewValue()
        End Set
    End Property

    <Category("Dimensions"), Browsable(True),
    DefaultValue("0"),
    Description("Specifies the length.")>
    Public Property Length() As String
        Get
            Return mDimensionInfo.Length
        End Get
        Set
            mDimensionInfo = New DimensionInfo(mDimensionInfo.CoordinateSystem, value)
            setNewValue()
        End Set
    End Property

    <Category("Dimensions"), Browsable(True),
    DefaultValue(False),
    Description("Specifies that no dimension is to be set.")>
    Public Property None() As Boolean
        Get
            Return mDimension Is Nothing
        End Get
        Set
            If value Then
                setValue(Nothing)
            Else
                setValue(_NullDimension)
            End If
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property Value As ChartSkil27.Dimension
        Get
            Return mDimension
        End Get
    End Property

    Private Function getSizeInfo(size As ChartSkil27.Dimension) As DimensionInfo
        If mDimension IsNot Nothing Then
            Return New DimensionInfo(mDimension.CoordinateSystem, CStr(mDimension.Length))
        Else
            Return New DimensionInfo(CoordinateSystems.CoordsDistance, "")
        End If
    End Function

    Private Sub setNewValue()
        Dim length As Double
        If Not Double.TryParse(mDimensionInfo.Length, length) Then Exit Sub
        setValue(_ChartSkil.NewDimension(length, mDimensionInfo.CoordinateSystem))
    End Sub

    Private Sub setValue(dimension As ChartSkil27.Dimension)
        mDimension = dimension
        RaiseEvent ValueChanged(Me, New EventArgs)
    End Sub

End Class

Friend NotInheritable Class DimensionConfigurerConverter
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
            Return New DimensionConfigurer(_ChartSkil.NewDimension(CDbl(tokens(0)),
                                      parseCoordinate(tokens(1))))
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overrides Function ConvertTo(context As System.ComponentModel.ITypeDescriptorContext, culture As System.Globalization.CultureInfo, value As Object, destinationType As System.Type) As Object
        If destinationType Is GetType(System.String) Then
            If TypeOf value Is DimensionConfigurer Then
                If CType(value, DimensionConfigurer).None Then Return "(None)"
                Dim dimension = CType(value, DimensionConfigurer).Value
                If dimension Is Nothing Then Return "(None)"
                Return dimension.Length & "," & dimension.CoordinateSystem.ToString
            End If
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

    Public Overrides Function CreateInstance(context As System.ComponentModel.ITypeDescriptorContext, propertyValues As System.Collections.IDictionary) As Object
        Return _ChartSkil.NewDimension(CDbl(propertyValues.Item("Length")),
                                  CType(propertyValues.Item("CoordinateSystem"), CoordinateSystems))
    End Function

    Public Overrides Function GetProperties(context As System.ComponentModel.ITypeDescriptorContext, value As Object, attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
        Return MyBase.GetProperties(context, value, attributes).Sort({"None", "Length", "CoordinateSystem"})
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
