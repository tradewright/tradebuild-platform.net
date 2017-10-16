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

Imports System.Runtime.InteropServices
Imports TWUtilities40

Namespace Sessions
    <ComSourceInterfaces(GetType(SessionUtils27.__Session))>
    Public Class Session

#Region "Interfaces"
        Implements SessionUtils27._Session
        Implements SessionUtils27.__Session_Event

#End Region

#Region "Events"
        Event DateChanged(sender As Object, e As SessionEventArgs)

        Event Started(sender As Object, e As SessionEventArgs)

        Event Ended(sender As Object, e As SessionEventArgs)

        Event SessionDateChanged(ByRef ev As SessionUtils27.SessionEventData) Implements SessionUtils27.__Session_Event.SessionDateChanged

        Event SessionStarted(ByRef ev As SessionUtils27.SessionEventData) Implements SessionUtils27.__Session_Event.SessionStarted

        Event SessionEnded(ByRef ev As SessionUtils27.SessionEventData) Implements SessionUtils27.__Session_Event.SessionEnded

#End Region

#Region "Constants"
#End Region

#Region "Enums"
#End Region

#Region "Types"
#End Region

#Region "Member variables"
        ' All times are in this timezone
        Private mTimezone As TimeZone

        Private mSessionStartTime As TimeSpan
        Private mSessionEndTime As TimeSpan

        Private mCurrentSessionTimes As SessionTimes

        Private mBuilder As SessionBuilder

        Private mTimestamp As Date

        Private WithEvents mComSession As SessionUtils27.Session

#End Region

#Region "Constructors"
        Friend Sub New(pSessionBuilder As SessionBuilder)
            mBuilder = pSessionBuilder
            mTimezone = TW.GetTimeZone("")
            mCurrentSessionTimes.EndTime = TW.MinDate
            mCurrentSessionTimes.StartTime = TW.MinDate
        End Sub

        Public Shared Function FromCOM(pComSession As SessionUtils27.Session) As Session
            Dim builder = New SessionBuilder(
                pComSession.SessionEndTime - StartOfDayAsDate,
                pComSession.SessionStartTime - StartOfDayAsDate,
                pComSession.TimeZone
            )
            builder.Session.ComSession = pComSession
            builder.SetSessionCurrentTime(pComSession.SessionCurrentTime)
            Return builder.Session
        End Function

#End Region

#Region "SessionUtils27._Session Interface Members"
        Private Function _Session_GetSessionTimes(Timestamp As Date) As SessionUtils27.SessionTimes Implements SessionUtils27._Session.GetSessionTimes
            Dim sessionTimes = GetSessionTimes(Timestamp)
            Return New SessionUtils27.SessionTimes With {.StartTime = sessionTimes.StartTime, .EndTime = sessionTimes.EndTime}
        End Function

        Private Function _Session_GetOffsetSessionTimes(Timestamp As Date, offset As Integer) As SessionUtils27.SessionTimes Implements SessionUtils27._Session.GetOffsetSessionTimes
            Dim sessionTimes = GetOffsetSessionTimes(Timestamp, offset)
            Return New SessionUtils27.SessionTimes With {.StartTime = sessionTimes.StartTime, .EndTime = sessionTimes.EndTime}
        End Function

        Private Sub Initialise(pSessionBuilder As SessionUtils27.SessionBuilder) Implements SessionUtils27._Session.Initialise
            Throw New NotImplementedException()
        End Sub

        Private Function _Session_IsTimeInFirstSessionForWeek(pTimestamp As Date) As Boolean Implements SessionUtils27._Session.IsTimeInFirstSessionForWeek
            Return IsTimeInFirstSessionForWeek(pTimestamp)
        End Function

        Private Function _Session_IsTimeInSession(Timestamp As Date) As Boolean Implements SessionUtils27._Session.IsTimeInSession
            Return IsTimeInSession(Timestamp)
        End Function

        Public Sub LinkToSession(pSessionToLinkTo As SessionUtils27.Session) Implements SessionUtils27._Session.LinkToSession
            Throw New NotImplementedException()
        End Sub

        Private ReadOnly Property _Session_CurrentSessionEndTime As Date Implements SessionUtils27._Session.CurrentSessionEndTime
            Get
                Return CurrentSessionEndTime
            End Get
        End Property

        Private ReadOnly Property _Session_CurrentSessionStartTime As Date Implements SessionUtils27._Session.CurrentSessionStartTime
            Get
                Return CurrentSessionStartTime
            End Get
        End Property

        Private ReadOnly Property _Session_SessionCurrentTime As Date Implements SessionUtils27._Session.SessionCurrentTime
            Get
                Return SessionCurrentTime
            End Get
        End Property

        Private ReadOnly Property _Session_SessionEndTime As Date Implements SessionUtils27._Session.SessionEndTime
            Get
                Return StartOfDayAsDate + SessionEndTime
            End Get
        End Property

        Private ReadOnly Property _Session_SessionStartTime As Date Implements SessionUtils27._Session.SessionStartTime
            Get
                Return StartOfDayAsDate + SessionStartTime
            End Get
        End Property

        Private ReadOnly Property _Session_TimeZone As TimeZone Implements SessionUtils27._Session.TimeZone
            Get
                Return TimeZone
            End Get
        End Property

#End Region

#Region "ComSession Event Handlers"
        Private Sub ComSession_DateChanged(ByRef e As SessionUtils27.SessionEventData) Handles mComSession.SessionDateChanged
            OnDateChanged(NewSessionEventArgs(e))
        End Sub

        Private Sub ComSession_SessionEnded(ByRef e As SessionUtils27.SessionEventData) Handles mComSession.SessionEnded
            OnEnded(NewSessionEventArgs(e))
        End Sub

        Private Sub ComSession_SessionStarted(ByRef e As SessionUtils27.SessionEventData) Handles mComSession.SessionStarted
            OnStarted(NewSessionEventArgs(e))
        End Sub

#End Region

#Region "Properties"
        Friend WriteOnly Property ComSession As SessionUtils27.Session
            Set
                mComSession = Value
            End Set
        End Property

        Public ReadOnly Property CurrentSessionEndTime() As Date
            Get
                If mComSession Is Nothing Then
                    Return mCurrentSessionTimes.EndTime
                Else
                    Return mComSession.CurrentSessionEndTime
                End If
            End Get
        End Property

        Public ReadOnly Property CurrentSessionStartTime() As Date
            Get
                If mComSession Is Nothing Then
                    Return mCurrentSessionTimes.StartTime
                Else
                    Return mComSession.CurrentSessionStartTime
                End If
            End Get
        End Property

        Friend ReadOnly Property SessionBuilder() As SessionBuilder
            Get
                Return mBuilder
            End Get
        End Property

        Public ReadOnly Property SessionCurrentTime() As Date
            Get
                If mComSession Is Nothing Then
                    Return mTimestamp
                Else
                    Return mComSession.SessionCurrentTime
                End If
            End Get
        End Property

        Public Property SessionEndTime() As TimeSpan
            Get
                Return mSessionEndTime
            End Get
            Friend Set
                mSessionEndTime = NormaliseSessionEndTime(Value)
            End Set
        End Property

        Public Property SessionStartTime() As TimeSpan
            Get
                SessionStartTime = mSessionStartTime
            End Get
            Friend Set
                mSessionStartTime = NormaliseSessionStartTime(Value)
            End Set
        End Property

        Public Property TimeZone() As TimeZone
            Get
                TimeZone = mTimezone
            End Get
            Friend Set(Value As TimeZone)
                If Value Is Nothing Then
                    mTimezone = TW.GetTimeZone("")
                Else
                    mTimezone = Value
                End If
            End Set
        End Property

#End Region

#Region "Methods"
        Public Function GetSessionTimes(Timestamp As Date) As SessionTimes
            Return SessionUtils.GetSessionTimes(Timestamp, mSessionStartTime, mSessionEndTime)
        End Function

        Public Function GetOffsetSessionTimes(Timestamp As Date, offset As Integer) As SessionTimes
            Return SessionUtils.GetOffsetSessionTimes(Timestamp, offset, mSessionStartTime, mSessionEndTime)
        End Function

        Public Function IsTimeInFirstSessionForWeek(pTimestamp As Date) As Boolean
            Dim lSessionTimes = GetSessionTimes(pTimestamp)
            If lSessionTimes.StartTime.TimeOfDay >= Midday Then Return (Weekday(lSessionTimes.StartTime, FirstDayOfWeek.Sunday) = 1)
            Return (Weekday(lSessionTimes.StartTime, FirstDayOfWeek.Monday) = 1)
        End Function

        Public Function IsTimeInSession(Timestamp As Date) As Boolean
            Return Timestamp >= mCurrentSessionTimes.StartTime And Timestamp < mCurrentSessionTimes.EndTime
        End Function

        ''' <summary>
        ''' Links this <c>Session</c> object to another <c>Session</c>
        ''' object so that both will have identical properties.
        ''' </summary>
        ''' 
        ''' <param name="pSessionToLinkTo">
        '''  the <code>Session</code> object which this <code>Session</code> 
        '''  object is to be linked to.
        ''' </param>
        Public Sub LinkToSession(pSessionToLinkTo As Session)
            pSessionToLinkTo.SessionBuilder.LinkSession(Me)
        End Sub

        Public Overridable Sub OnDateChanged(e As SessionEventArgs)
            RaiseEvent DateChanged(Me, e)
            Dim ed = NewSessionEventData(e)
            RaiseEvent SessionDateChanged(ed)
        End Sub

        Public Overridable Sub OnStarted(e As SessionEventArgs)
            RaiseEvent Started(Me, e)
            Dim ed = NewSessionEventData(e)
            RaiseEvent SessionStarted(ed)
        End Sub

        Public Overridable Sub OnEnded(e As SessionEventArgs)
            RaiseEvent Ended(Me, e)
            Dim ed = NewSessionEventData(e)
            RaiseEvent SessionEnded(ed)
        End Sub

        Friend Function SetSessionCurrentTime(pTimestamp As Date) As SessionEventData
            Static sInitialised As Boolean
            Static sSessionEndNotified As Boolean
            Static sNextSessionTimes As SessionTimes
            Static sPrevTimeNotified As Date

            Dim ev As SessionEventData

            ev.Source = Me
            ev.changeType = SessionChangeType.None

            mTimestamp = pTimestamp

            If pTimestamp.Date > sPrevTimeNotified.Date Then
                ev.changeType = SessionChangeType.DateChange
                ev.Timestamp = pTimestamp.Date
                OnDateChanged(New SessionEventArgs(SessionChangeType.DateChange, pTimestamp))
            End If

            If Not sInitialised Then
                sInitialised = True
                mCurrentSessionTimes = SessionUtils.GetSessionTimes(pTimestamp, mSessionStartTime, mSessionEndTime)
                sNextSessionTimes = SessionUtils.GetOffsetSessionTimes(pTimestamp, 1, mSessionStartTime, mSessionEndTime, True)
                ev.changeType = SessionChangeType.Start
                ev.Timestamp = mCurrentSessionTimes.StartTime
                OnStarted(New SessionEventArgs(SessionChangeType.Start, pTimestamp))
            End If

            If pTimestamp >= mCurrentSessionTimes.EndTime And Not sSessionEndNotified Then
                sSessionEndNotified = True
                ev.changeType = SessionChangeType.End
                ev.Timestamp = mCurrentSessionTimes.EndTime
                OnEnded(New SessionEventArgs(SessionChangeType.End, pTimestamp))
                sSessionEndNotified = True
            End If

            If pTimestamp >= sNextSessionTimes.StartTime Then
                mCurrentSessionTimes = SessionUtils.GetSessionTimes(pTimestamp, mSessionStartTime, mSessionEndTime)
                sNextSessionTimes = SessionUtils.GetOffsetSessionTimes(pTimestamp, 1, mSessionStartTime, mSessionEndTime, True)
                sSessionEndNotified = False
                ev.changeType = SessionChangeType.Start
                ev.Timestamp = mCurrentSessionTimes.StartTime
                OnStarted(New SessionEventArgs(SessionChangeType.Start, pTimestamp))
            End If

            SetSessionCurrentTime = ev
            sPrevTimeNotified = pTimestamp
        End Function

#End Region

#Region "Helper Functions"
        Private Shared Function NewSessionEventArgs(e As SessionUtils27.SessionEventData) As SessionEventArgs
            Return New SessionEventArgs(CType(e.changeType, SessionChangeType), e.Timestamp)
        End Function

        Private Function NewSessionEventData(e As SessionEventArgs) As SessionUtils27.SessionEventData
            Return New SessionUtils27.SessionEventData With {
                .changeType = CType(e.ChangeType, SessionUtils27.SessionChangeTypes),
                .Source = Me,
                .Timestamp = e.Timestamp
            }
        End Function

#End Region

    End Class
End Namespace
