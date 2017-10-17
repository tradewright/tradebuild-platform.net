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


Public NotInheritable Class NotificationRequest

    Private mEventSink As IBracketOrderEventSink
    Private mResourceContext As ResourceContext

#Region "Constructor"

    Friend Sub New(pEventSink As IBracketOrderEventSink, pResourceContext As ResourceContext)
        mEventSink = pEventSink
        mResourceContext = pResourceContext
    End Sub

#End Region

#Region "Properties"

    Friend ReadOnly Property EventSink() As IBracketOrderEventSink
        Get
            EventSink = mEventSink
        End Get
    End Property

    Friend ReadOnly Property ResourceContext() As ResourceContext
        Get
            ResourceContext = mResourceContext
        End Get
    End Property

#End Region

#Region "Methods"

#End Region

#Region "Helper Functions"
#End Region

End Class