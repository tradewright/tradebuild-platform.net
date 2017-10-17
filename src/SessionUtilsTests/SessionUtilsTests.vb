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

Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports TradeWright.Trading.Utils.Sessions
Imports TradeWright.Utilities.Time

<TestClass()> Public Class SessionUtilsTests

#Region "Setup Functions"

#End Region

#Region "Test Methods"

    <TestClass()> Public Class TestGetOffsetSessionTimes

        <TestMethod()> Public Sub GetOffsetSessionTimes_0100()
            Assert.AreEqual("2016-01-18 08:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("15/1/16 08:00:00"),
                                1,
                                New TimeSpan(8, 0, 0),
                                New TimeSpan(17, 30, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0110()
            Assert.AreEqual("2016-01-18 17:30:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("15/1/16 08:00:00"),
                                1,
                                New TimeSpan(8, 0, 0),
                                New TimeSpan(17, 30, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0200()
            Assert.AreEqual("2016-01-18 08:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("16/1/16 08:00:00"),
                                1,
                                New TimeSpan(8, 0, 0),
                                New TimeSpan(17, 30, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0210()
            Assert.AreEqual("2016-01-18 17:30:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("16/1/16 08:00:00"),
                                1,
                                New TimeSpan(8, 0, 0),
                                New TimeSpan(17, 30, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0300()
            Assert.AreEqual("2016-01-19 08:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("18/1/16 08:00:00"),
                                1,
                                New TimeSpan(8, 0, 0),
                                New TimeSpan(17, 30, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0310()
            Assert.AreEqual("2016-01-19 17:30:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("18/1/16 08:00:00"),
                                1,
                                New TimeSpan(8, 0, 0),
                                New TimeSpan(17, 30, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0400()
            Assert.AreEqual("2016-01-22 08:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("21/1/16 08:00:00"),
                                1,
                                New TimeSpan(8, 0, 0),
                                New TimeSpan(17, 30, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0410()
            Assert.AreEqual("2016-01-22 17:30:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("21/1/16 08:00:00"),
                                1,
                                New TimeSpan(8, 0, 0),
                                New TimeSpan(17, 30, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub




        <TestMethod()> Public Sub GetOffsetSessionTimes_0500()
            Assert.AreEqual("2016-01-17 16:30:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("15/1/16 08:00:00"),
                                1,
                                New TimeSpan(16, 30, 0),
                                New TimeSpan(16, 15, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0510()
            Assert.AreEqual("2016-01-18 16:15:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("15/1/16 08:00:00"),
                                1,
                                New TimeSpan(16, 30, 0),
                                New TimeSpan(16, 15, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0600()
            Assert.AreEqual("2016-01-17 16:30:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("16/1/16 08:00:00"),
                                1,
                                New TimeSpan(16, 30, 0),
                                New TimeSpan(16, 15, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0610()
            Assert.AreEqual("2016-01-18 16:15:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("16/1/16 08:00:00"),
                                1,
                                New TimeSpan(16, 30, 0),
                                New TimeSpan(16, 15, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0700()
            Assert.AreEqual("2016-01-18 16:30:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("18/1/16 08:00:00"),
                                1,
                                New TimeSpan(16, 30, 0),
                                New TimeSpan(16, 15, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0710()
            Assert.AreEqual("2016-01-19 16:15:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("18/1/16 08:00:00"),
                                1,
                                New TimeSpan(16, 30, 0),
                                New TimeSpan(16, 15, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0800()
            Assert.AreEqual("2016-01-21 16:30:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("21/1/16 08:00:00"),
                                1,
                                New TimeSpan(16, 30, 0),
                                New TimeSpan(16, 15, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_0810()
            Assert.AreEqual("2016-01-22 16:15:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("21/1/16 08:00:00"),
                                1,
                                New TimeSpan(16, 30, 0),
                                New TimeSpan(16, 15, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub





        <TestMethod()> Public Sub GetOffsetSessionTimes_1100()
            Assert.AreEqual("2016-01-18 00:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("15/1/16 08:00:00"),
                                1,
                                New TimeSpan(0, 0, 0),
                                New TimeSpan(0, 0, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_1110()
            Assert.AreEqual("2016-01-19 00:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("15/1/16 08:00:00"),
                                1,
                                New TimeSpan(0, 0, 0),
                                New TimeSpan(0, 0, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_1200()
            Assert.AreEqual("2016-01-18 00:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("16/1/16 08:00:00"),
                                1,
                                New TimeSpan(0, 0, 0),
                                New TimeSpan(0, 0, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_1210()
            Assert.AreEqual("2016-01-19 00:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("16/1/16 08:00:00"),
                                1,
                                New TimeSpan(0, 0, 0),
                                New TimeSpan(0, 0, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_1300()
            Assert.AreEqual("2016-01-19 00:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("18/1/16 08:00:00"),
                                1,
                                New TimeSpan(0, 0, 0),
                                New TimeSpan(0, 0, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_1310()
            Assert.AreEqual("2016-01-20 00:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("18/1/16 08:00:00"),
                                1,
                                New TimeSpan(0, 0, 0),
                                New TimeSpan(0, 0, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_1400()
            Assert.AreEqual("2016-01-22 00:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("21/1/16 08:00:00"),
                                1,
                                New TimeSpan(0, 0, 0),
                                New TimeSpan(0, 0, 0)).StartTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

        <TestMethod()> Public Sub GetOffsetSessionTimes_1410()
            Assert.AreEqual("2016-01-23 00:00:00.000",
                FormatTimestamp(
                    GetOffsetSessionTimes(CDate("21/1/16 08:00:00"),
                                1,
                                New TimeSpan(0, 0, 0),
                                New TimeSpan(0, 0, 0)).EndTime,
                    TimestampFormats.DateAndTimeISO8601))
        End Sub

#End Region

    End Class

    <TestClass()> Public Class TestFromCOM
        Private WithEvents comSession As SessionUtils27.Session
        Private comSessionEnded As Boolean
        Private comSessionStarted As Boolean

        <TestMethod()> Public Sub FromCom()
            Dim SU As New SessionUtils27.SessionUtils
            Dim builder = SU.CreateSessionBuilder(StartOfDayAsDate + New TimeSpan(8, 0, 0),
                                                  StartOfDayAsDate + New TimeSpan(17, 30, 0))
            comSession = builder.Session
            Dim sess = Trading.Utils.Sessions.Session.FromCOM(comSession)

            Dim sessionEnded As Boolean
            Dim sessionStarted As Boolean
            AddHandler sess.Ended, Sub(s, e) sessionEnded = True
            AddHandler sess.Started, Sub(s, e) sessionStarted = True

            builder.SetSessionCurrentTime(#2017/09/21 08:10:00#)
            Assert.IsTrue(comSessionStarted, "Session started")
            Assert.IsTrue(comSession.SessionCurrentTime = #2017/09/21 08:10:00#)
            Assert.IsTrue(comSession.CurrentSessionStartTime = #2017/09/21 08:00:00#)
            Assert.IsTrue(comSession.CurrentSessionEndTime = #2017/09/21 17:30:00#)

        End Sub

        Private Sub comSession_SessionStarted(ByRef ev As SessionUtils27.SessionEventData) Handles comSession.SessionStarted
            comSessionStarted = True
        End Sub

        Private Sub comSession_SessionEnded(ByRef ev As SessionUtils27.SessionEventData) Handles comSession.SessionEnded
            comSessionEnded = True
        End Sub
    End Class


#Region "Helper Functions"





#End Region

End Class