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
Imports StudyUtils27
Imports TWUtilities40

Module Globals

#Region "Constants"

    Public Const ProjectName As String = "TradeBuildUI"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Global object references"

    Friend ChartSkil As New ChartSkilClass
    Friend StudyUtils As New StudyUtils
    Friend TWUtilities As New TWUtilities
    'Friend ContractUtils As New ContractUtils

#End Region

#Region "External function declarations"

#End Region

#Region "Variables"

    Private mErrorLogger As Logger = TWUtilities.GetLogger("error")
    Private mDiagLogger As FormattingLogger = TWUtilities.CreateFormattingLogger("diag.TradeWright.Trading.UI.Trading", ProjectName)

    Private mStartOfDay As Date

#End Region

#Region "mStudyPickerForm Event Handlers"

#End Region

#Region "Properties"

    Public ReadOnly Property gDiagLogger() As FormattingLogger
        Get
            Return mDiagLogger
        End Get
    End Property

    Public ReadOnly Property gErrorLogger() As Logger
        Get
            Return mErrorLogger
        End Get
    End Property

    Public ReadOnly Property COMStartOfDay As Date
        Get
            Static sComStartOfDay As Date = Date.FromOADate(0.0)
            Return sComStartOfDay
        End Get
    End Property

    Public ReadOnly Property StartOfDay As TimeSpan
        Get
            Static sStartOfDay As TimeSpan = TimeSpan.Zero
            Return sStartOfDay
        End Get
    End Property

#End Region

#Region "Methods"

    Friend Sub NotifyUnhandledError(e As Exception, procName As String, moduleName As String)
        TWUtilities.UnhandledErrorHandler.Notify(procName,
                                                moduleName,
                                                "TradingUI",
                                                pErrorNumber:=e.HResult,
                                                pErrorDesc:=e.Message,
                                                pErrorSource:=e.StackTrace)
    End Sub

#End Region

#Region "Helper Functions"

#End Region

End Module