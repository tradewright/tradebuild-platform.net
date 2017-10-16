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

Namespace Sessions
    Public Class SessionBuilder

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Constants"

        Private Const ModuleName As String = "SessionBuilder"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

        Private mSession As New Session(Me)
        Private mLatestTimeNotified As Date
        Private mLinkedSessions As New HashSet(Of Session)

#End Region

#Region "Class Event Handlers"

        Private Sub New()
        End Sub

        Public Sub New(pSessionStartTime As TimeSpan, pSessionEndTime As TimeSpan, pTimezone As TWUtilities40.TimeZone)
            mSession.SessionStartTime = pSessionStartTime
            mSession.SessionEndTime = pSessionEndTime
            mSession.TimeZone = pTimezone
        End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

        Public ReadOnly Property Session() As Session
            Get
                Session = mSession
            End Get
        End Property

        Friend WriteOnly Property SessionStartTime() As TimeSpan
            Set
                mSession.SessionStartTime = Value
            End Set
        End Property

        Friend WriteOnly Property SessionEndTime() As TimeSpan
            Set
                mSession.SessionEndTime = Value
            End Set
        End Property

        Friend WriteOnly Property TimeZone() As TWUtilities40.TimeZone
            Set
                If Not Not Value Is Nothing Then Throw New ArgumentException("Value is Nothing")
                mSession.TimeZone = Value
            End Set
        End Property

#End Region

#Region "Methods"

        ''
        ' Links another <code>Session</code> object to this <code>SessionBuilder</code>
        ' object so that both will have identical properties.
        '
        ' @param pSessionToLink the <code>Session</code> object which is to be linked to this
        '               <code>SessionBuilder</code> object.
        ' @see UnLinkSession
        '
        '@/
        Public Sub LinkSession(pSessionToLink As Session)
            pSessionToLink.SessionStartTime = mSession.SessionStartTime
            pSessionToLink.SessionEndTime = mSession.SessionEndTime
            pSessionToLink.SetSessionCurrentTime(mLatestTimeNotified)
            mLinkedSessions.Add(pSessionToLink)
        End Sub

        Public Function SetSessionCurrentTime(Timestamp As Date) As SessionEventData
            SetSessionCurrentTime = mSession.SetSessionCurrentTime(Timestamp)
            For Each sess In mLinkedSessions
                sess.SetSessionCurrentTime(Timestamp)
            Next sess

            mLatestTimeNotified = Timestamp
        End Function

        ''
        ' Unlinks a previously linked <code>Session</code> object from this
        ' <code>SessionBuilder</code> object.
        '
        ' @param objectToUnlink the <code>Session</code> object which is to be unlinked
        '               from this <code>SessionBuilder</code> object.
        ' @see LinkSession
        '
        '@/
        Public Sub UnLinkSession(objectToUnlink As Session)
            mLinkedSessions.Remove(objectToUnlink)
        End Sub

#End Region

#Region "Helper Functions"
#End Region

    End Class
End Namespace
