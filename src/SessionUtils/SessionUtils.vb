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

Imports TWUtilities40

Imports TradeWright.Utilities.Time

Namespace Sessions

    Public Enum SessionChangeType
        None
        [End]
        Start
        DateChange
    End Enum

    Public Structure SessionEventData
        Dim Source As Object
        Dim Timestamp As Date
        Dim changeType As SessionChangeType
    End Structure

    Public Structure SessionTimes
        Dim StartTime As Date
        Dim EndTime As Date
    End Structure

    Public Module SessionUtils

#Region "Member variables"

        Public StartOfDayAsDate As Date = Date.FromOADate(0.0)

        Public StartOfDay As TimeSpan = TimeSpan.Zero
        Public Midday As TimeSpan = TimeSpan.FromHours(12)
        Public Midnight As TimeSpan = TimeSpan.FromHours(24)

        Friend TW As New TWUtilities40.TWUtilities

#End Region

#Region "Constructors"


#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

        Public Function CreateSessionBuilderFuture(pSessionFuture As IFuture) As _IFuture
            Dim lBuilder As New SessionBuilderFutureBuilder
            lBuilder.Initialise(pSessionFuture)
            Return lBuilder.Future
        End Function

        Public Function CreateSessionFuture(pSessionBuilderFuture As IFuture) As _IFuture
            Dim lBuilder As New SessionFutureBuilder
            lBuilder.Initialise(pSessionBuilderFuture)
            Return lBuilder.Future
        End Function

        Public Function GetOffsetSessionTimes(timestamp As Date, offset As Integer, Optional ignoreWeekends As Boolean = False) As SessionTimes
            GetOffsetSessionTimes(timestamp, offset, StartOfDay, Midnight, ignoreWeekends)
        End Function

        Public Function GetOffsetSessionTimes(timestamp As Date, offset As Integer, startTime As TimeSpan, endTime As TimeSpan, Optional ignoreWeekends As Boolean = False) As SessionTimes
            startTime = NormaliseSessionStartTime(startTime)
            endTime = NormaliseSessionEndTime(endTime)

            Dim lDatumSessionTimes As SessionTimes

            Dim lTargetDate As Date

            If ignoreWeekends Then
                lDatumSessionTimes = GetSessionTimesIgnoringWeekend(timestamp, startTime, endTime)
                lTargetDate = lDatumSessionTimes.StartTime.Date.AddDays(offset)
            Else
                lDatumSessionTimes = GetSessionTimes(timestamp, startTime, endTime)

                Dim lBasedate = lDatumSessionTimes.EndTime.AddDays(If(endTime = Midnight, -1, 0))

                Dim lTargetWorkingDayNum = Time.GetWorkingDayNumber(lBasedate) + offset

                lTargetDate = Time.GetWorkingDayDate(lTargetWorkingDayNum, lBasedate.Date)
                If sessionSpansMidnight(startTime, endTime) Then lTargetDate = lTargetDate.AddDays(-1)
            End If

            With GetOffsetSessionTimes
                .StartTime = lTargetDate.Add(startTime)
                .EndTime = lTargetDate.Add(endTime)
                If sessionSpansMidnight(startTime, endTime) Then .EndTime = .EndTime.AddDays(1)
            End With
        End Function

        Public Function GetSessionTimes(Timestamp As Date) As SessionTimes
            GetSessionTimes(Timestamp, StartOfDay, Midnight)
        End Function

        ' !!!!!!!!!!!!!!!!!!!!!!!!!!!
        ' getSessionTimes needs to be amended
        ' to take acCount of holidays
        Public Function GetSessionTimes(Timestamp As Date, StartTime As TimeSpan, EndTime As TimeSpan) As SessionTimes
            StartTime = NormaliseSessionStartTime(StartTime)
            EndTime = NormaliseSessionEndTime(EndTime)
            GetSessionTimes = GetSessionTimesIgnoringWeekend(Timestamp, StartTime, EndTime)

            Dim lWeekday = GetSessionTimes.StartTime.DayOfWeek
            If sessionSpansMidnight(StartTime, EndTime) Then
                ' session DOES span midnight
                If lWeekday = DayOfWeek.Friday Then
                    GetSessionTimes.StartTime = GetSessionTimes.StartTime.AddDays(-1)
                    GetSessionTimes.EndTime = GetSessionTimes.EndTime.AddDays(-1)
                ElseIf lWeekday = DayOfWeek.Saturday Then
                    GetSessionTimes.StartTime = GetSessionTimes.StartTime.AddDays(-2)
                    GetSessionTimes.EndTime = GetSessionTimes.EndTime.AddDays(-2)
                End If
            Else
                ' session doesn't span midnight or 24-hour session or no session times known
                If lWeekday = DayOfWeek.Sunday Then
                    GetSessionTimes.StartTime = GetSessionTimes.StartTime.AddDays(-2)
                    GetSessionTimes.EndTime = GetSessionTimes.EndTime.AddDays(-2)
                ElseIf lWeekday = DayOfWeek.Saturday Then
                    GetSessionTimes.StartTime = GetSessionTimes.StartTime.AddDays(-1)
                    GetSessionTimes.EndTime = GetSessionTimes.EndTime.AddDays(-1)
                End If
            End If
        End Function

        Public Function GetSessionTimesIgnoringWeekend(pTimestamp As Date, pSessionStartTime As TimeSpan, pSessionEndTime As TimeSpan) As SessionTimes
            pSessionStartTime = NormaliseSessionStartTime(pSessionStartTime)
            pSessionEndTime = NormaliseSessionEndTime(pSessionEndTime
                                          )
            Dim referenceDate As Date
            referenceDate = pTimestamp.Date

            Dim referenceTime = pTimestamp.TimeOfDay

            If referenceTime = StartOfDay Then
                If pSessionStartTime >= Midday Then referenceDate = referenceDate.AddDays(-1)
            ElseIf referenceTime < pSessionStartTime Then
                referenceDate = referenceDate.AddDays(-1)
            End If

            GetSessionTimesIgnoringWeekend.StartTime = referenceDate + pSessionStartTime
            If pSessionEndTime > pSessionStartTime Then
                GetSessionTimesIgnoringWeekend.EndTime = referenceDate + pSessionEndTime
            Else
                GetSessionTimesIgnoringWeekend.EndTime = referenceDate.AddDays(1) + pSessionEndTime
            End If
        End Function

        Friend Function NormaliseSessionEndTime(time As TimeSpan) As TimeSpan
            If time.Days <> 0 Then time = New TimeSpan(time.Hours, time.Minutes, 0)
            If time = TimeSpan.Zero Then Return Midnight
            Return time
        End Function

        Friend Function NormaliseSessionStartTime(time As TimeSpan) As TimeSpan
            If time.Days = 0 Then Return time
            Return New TimeSpan(time.Hours, time.Minutes, 0)
        End Function

#End Region

#Region "Helper Functions"

        Private Function sessionSpansMidnight(pStartTime As TimeSpan, pEndTime As TimeSpan) As Boolean
            sessionSpansMidnight = (pStartTime > pEndTime)
        End Function

#End Region

    End Module
End Namespace
