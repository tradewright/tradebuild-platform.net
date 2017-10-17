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

Option Strict Off

Imports TradeBuild27
Imports TradingDO27
Imports TWUtilities40

Imports TradeWright.Trading.TradeBuild.AutoTradingEnvironment
Imports TradeWright.Trading.Utils.Contracts

Module MainModule

#Region "Interfaces"

#End Region

#Region "Events"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

#End Region

#Region "Member variables"

    Private mForm As fStrategyHost

    Private WithEvents mUnhandledErrorHandler As UnhandledErrorHandler = TWUtilities.UnhandledErrorHandler

    Private mTB As TradeBuildAPI

    Private mModel As IStrategyHostModel

    Private mStrategyHost As DefaultStrategyHost

    Public gFinished As Boolean

#End Region

#Region "Constructors"

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "mUnhandledErrorHandler Event Handlers"

    Private Sub mUnhandledErrorHandler_UnhandledError(ByRef ev As ErrorEventData) Handles mUnhandledErrorHandler.UnhandledError
        NotifyFatalError()
        System.Environment.FailFast(
$"Error: 0x{Hex(ev.ErrorCode)} ({ev.ErrorCode})
{ev.ErrorMessage}
At:
{ev.ErrorSource}")
    End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

    Public Sub Main(args() As String)
        System.Windows.Forms.Application.EnableVisualStyles()

        Dim lClp = New Utilities.CommandLine.CommandLineParser(Command)

        If lClp.IsSwitchSet("?") Then
            MsgBox(vbCrLf & getUsageString(), , "Usage")
            Exit Sub
        End If

        If Not init() Then
            TWUtilities.TerminateTWUtilities()
            Exit Sub
        End If

        Dim lNoUI = lClp.IsSwitchSet("noui")

        Dim lRun = lClp.IsSwitchSet("run")

        Dim lLiveTrades = lClp.IsSwitchSet("livetrades")

        Dim lSymbol = lClp.Arg(0)
        If lSymbol = "" And lNoUI Then
            TWUtilities.LogMessage("No symbol supplied")
            If Not lNoUI And lRun Then MsgBox("Error - no symbol argument supplied: " & vbCrLf & getUsageString(), vbCritical, "Error")
            Exit Sub
        End If

        If lSymbol <> "" Then
            Dim lContractSpec = parseSymbol(lSymbol)
            If lContractSpec Is Nothing Then
                TWUtilities.LogMessage("Invalid symbol")
                If Not lNoUI And lRun Then MsgBox("Error - invalid symbol string supplied: " & vbCrLf & getUsageString(), vbCritical, "Error")
                Exit Sub
            End If
            mModel.Symbol = lContractSpec
        End If

        Dim lStrategyClassName = lClp.Arg(1)
        If lStrategyClassName = "" And lNoUI Then
            TWUtilities.LogMessage("No strategy supplied")
            If Not lNoUI And lRun Then MsgBox("Error - no strategy class name argument supplied: " & vbCrLf & getUsageString(), vbCritical, "Error")
            Exit Sub
        End If
        mModel.StrategyClassName = lStrategyClassName

        Dim lStopStrategyFactoryClassName = lClp.Arg(2)
        If lStopStrategyFactoryClassName = "" And lNoUI Then
            TWUtilities.LogMessage("No stop strategy factory supplied")
            If Not lNoUI And lRun Then MsgBox("Error - no stop strategy factory class name argument supplied: " & vbCrLf & getUsageString(), vbCritical, "Error")
            Exit Sub
        End If
        mModel.StopStrategyFactoryClassName = lStopStrategyFactoryClassName

        If Not setupServiceProviders(lClp, lLiveTrades, lNoUI) Then Exit Sub

        mModel.UseMoneyManagement = lClp.IsSwitchSet("umm") Or lClp.IsSwitchSet("UseMoneyManagement")

        If lClp.IsSwitchSet("ResultsPath") Then mModel.ResultsPath = lClp.SwitchValue("ResultsPath")

        If lNoUI Then
            '    Set mStrategyRunner = CreateStrategyRunner(Me)
            '    mStrategyRunner.UseMoneyManagement = lUseMoneyManagement
            '    mStrategyRunner.ResultsPath = lResultsPath
            '    mStrategyRunner.SetStrategy CreateObject(lStrategyClassName), Nothing
            '    mStrategyRunner.PrepareSymbol lSymbol
            '    Set mStrategyRunner = Nothing
        Else
            mForm = New fStrategyHost
            mForm.Show()

            Dim lFrameSize = SystemInformation.FixedFrameBorderSize
            mForm.Left = -lFrameSize.Width
            mForm.Top = -lFrameSize.Height
            mForm.Width = Screen.PrimaryScreen.WorkingArea.Width / 2 + 2 * lFrameSize.Width
            mForm.Height = Screen.PrimaryScreen.WorkingArea.Height + 2 * lFrameSize.Height

            Dim failpoint As String

            failpoint = "Dim lController As New DefaultStrategyHostCtlr"
            Dim lController As New DefaultStrategyHostController

            failpoint = "Set mStrategyHost = New DefaultStrategyHost"
            mStrategyHost = New DefaultStrategyHost

            failpoint = "mStrategyHost.Initialise mModel, mForm, lController"
            mStrategyHost.Initialise(mModel, mForm, lController)

            failpoint = "mForm.Initialise mModel, lController"
            mForm.Initialise(mModel, lController)

            If lRun Then
                mForm.Start()
            End If

            Do While Not gFinished
                TWUtilities.Wait(50)
            Loop

            mForm = Nothing

            TWUtilities.LogMessage("Removing all service providers")
            mTB.ServiceProviders.RemoveAll()

            TWUtilities.LogMessage("Application exiting")

            TWUtilities.TerminateTWUtilities()
        End If
    End Sub

    Public Sub NotifyFatalError()
        MsgBox(
$"A fatal error has occurred within {GetAppTitle()}. The program will close when you click the OK button.

Please email the log file located at

     {getLogfileName()}

to support@tradewright.com",
                vbCritical,
                "Fatal error")
    End Sub

    Public Function GetAppTitle() As String
        Dim strategyString = If(mModel?.StrategyClassName <> "", $" - {mModel?.StrategyClassName}", "")
        Dim contractString = If(mModel?.Contract?.Specifier?.LocalSymbol <> "", $" - {mModel?.Contract?.Specifier?.LocalSymbol}", "")
        Return $"TradeBuild Strategy Host{strategyString}{contractString}"
    End Function

#End Region

#Region "Helper Functions"

    Private Sub displayServiceProviderErrorMessage(providertype As String)
        MsgBox(
$"Error setting up {providertype} service provider(s) - see log at:

{getLogfileName()}

{getUsageString()}", vbCritical, "Error")
    End Sub

    Private Function getLogfileName() As String
        Static sName As String
        If sName = "" Then sName = TWUtilities.DefaultLogFileName(Command)
        Return sName
    End Function

    Private Function getUsageString() As String
        Return _
"strategyhost  [(/specifier[;/specifier]...)]
              [strategy Class name]
              [stop strategy factory Class name]
              [/tws:[Server],[Port],[ClientId]
              [/db:[server],[servertype],[database]
              [/livetrades]
              [/logpath:path]
              [/noUI]
              [/resultsPath:path]
              [/run]
              [/useMoneyManagement  |  /umm]

 where

   specifier := [ local[symbol]:<localsymbol>
                | symb[ol]:<symbol>
                | sec[type][ STK | FUT | FOP | CASH ]
                | exch[ange]:<exchangename>
                | curr[ency]:<currencycode>
                | exp[iry][yyyymm | yyyymmdd]
                | mult[iplier]:<multiplier>
                | str[ike]:<price>
                | right[ Call | PUT ]
                ]"
    End Function

    Private Function init() As Boolean
        TWUtilities.InitialiseTWUtilities()

        TWUtilities.ApplicationGroupName = "TradeWright"
        TWUtilities.ApplicationName = "StrategyHost.Net"

        Try
            TWUtilities.DefaultLogLevel = LogLevels.LogLevelDetail
            TWUtilities.SetupDefaultLogging(Command)
        Catch e As System.Runtime.InteropServices.COMException When e.ErrorCode = ErrorCodes.ErrSecurityException
            MsgBox(
$"You don't have write access to the log file:


{getLogfileName()}


The program will close",
                    vbCritical,
                    "Attention")
            Return False
        End Try

        mModel = New DefaultStrategyHostModel()
        mModel.LogParameters = True
        mModel.ShowChart = True
        Return True
    End Function

    Private Function parseSymbol(pSymbol As String) As IContractSpecifier
        If Not Left$(pSymbol, 1) = "(" Or Not Right$(pSymbol, 1) = ")" Then
            Return Nothing
            Exit Function
        End If

        pSymbol = Mid$(pSymbol, 2, Len(pSymbol) - 2)

        Dim lClp = New Utilities.CommandLine.CommandLineParser(pSymbol, ";")

        Dim lLocalSymbol = lClp.SwitchValue("localsymbol")
        If lLocalSymbol = "" Then lLocalSymbol = lClp.SwitchValue("local")

        Dim lSymbol = lClp.SwitchValue("symbol")
        If lSymbol = "" Then lSymbol = lClp.SwitchValue("symb")

        Dim lSectype = lClp.SwitchValue("sectype")
        If lSectype = "" Then lSectype = lClp.SwitchValue("sec")

        Dim lExchange = lClp.SwitchValue("exchange")
        If lExchange = "" Then lExchange = lClp.SwitchValue("exch")

        Dim lCurrency = lClp.SwitchValue("currency")
        If lCurrency = "" Then lCurrency = lClp.SwitchValue("curr")

        Dim lExpiry = lClp.SwitchValue("expiry")
        If lExpiry = "" Then lExpiry = lClp.SwitchValue("exp")

        Dim lMultiplier = lClp.SwitchValue("multiplier")
        If lMultiplier = "" Then lExpiry = lClp.SwitchValue("mult")
        If lMultiplier = "" Then lMultiplier = "1.0"

        Dim lstrike = lClp.SwitchValue("strike")
        If lstrike = "" Then lstrike = lClp.SwitchValue("str")
        If lstrike = "" Then lstrike = "0.0"

        Dim lRight = lClp.SwitchValue("right")

        Return New ContractSpecifier(lLocalSymbol,
                                                lSymbol,
                                                lExchange,
                                                Contract.SecTypeFromString(lSectype),
                                                lCurrency,
                                                lExpiry,
                                                CDbl(lMultiplier),
                                                CDbl(lstrike),
                                                Contract.OptionRightFromString(lRight))
    End Function

    Private Function setupServiceProviders(
                    pClp As Utilities.CommandLine.CommandLineParser,
                    pLiveTrades As Boolean,
                    pNoUI As Boolean) As Boolean
        Dim lPermittedSPRoles = ServiceProviderRoles.SPRoleContractDataPrimary +
                            ServiceProviderRoles.SPRoleHistoricalDataInput +
                            ServiceProviderRoles.SPRoleOrderSubmissionLive +
                            ServiceProviderRoles.SPRoleOrderSubmissionSimulated

        If Not pLiveTrades And Not pNoUI Then lPermittedSPRoles = lPermittedSPRoles + ServiceProviderRoles.SPRoleTickfileInput

        If pClp.IsSwitchSet("tws") Then lPermittedSPRoles = lPermittedSPRoles + ServiceProviderRoles.SPRoleRealtimeData

        mTB = TradeBuild.CreateTradeBuildAPI(, lPermittedSPRoles)

        If pClp.IsSwitchSet("tws") Then
            If Not setupTwsServiceProviders(pClp.SwitchValue("tws"), pLiveTrades) Then
                displayServiceProviderErrorMessage("Tws")
                Exit Function
            End If
        End If

        If pClp.IsSwitchSet("db") Then
            If Not setupDbServiceProviders(pClp.SwitchValue("db"), Not (pLiveTrades Or pNoUI)) Then
                displayServiceProviderErrorMessage("database")
                Exit Function
            End If
        Else
            MsgBox("/db not supplied: " & vbCrLf & getUsageString(), vbCritical, "Error")
            Exit Function
        End If

        If Not setupSimulateOrderServiceProviders(pLiveTrades) Then
            displayServiceProviderErrorMessage("simulated orders")
            Exit Function
        End If

        If Not mTB.StartServiceProviders Then
            MsgBox("One or more service providers failed to start: see logfile")
            Exit Function
        End If

        mTB.StudyLibraryManager.AddBuiltInStudyLibrary()

        mModel.ContractStorePrimary = New ContractStore(mTB.ContractStorePrimary)
        mModel.ContractStoreSecondary = New ContractStore(mTB.ContractStoreSecondary)
        mModel.HistoricalDataStoreInput = mTB.HistoricalDataStoreInput
        mModel.OrderSubmitterFactoryLive = mTB.OrderSubmitterFactoryLive
        mModel.OrderSubmitterFactorySimulated = mTB.OrderSubmitterFactorySimulated
        mModel.RealtimeTickers = mTB.Tickers
        mModel.StudyLibraryManager = mTB.StudyLibraryManager
        mModel.TickfileStoreInput = mTB.TickfileStoreInput

        setupServiceProviders = True
    End Function

    Private Function setupDbServiceProviders(
                    switchValue As String,
                    pAllowTickfiles As Boolean) As Boolean
        Dim clp = TWUtilities.CreateCommandLineParser(switchValue, ",")

        setupDbServiceProviders = True

        Dim server = String.Empty
        Dim dbtypeStr = String.Empty
        Dim database = String.Empty
        Dim username = String.Empty
        Dim password = String.Empty

        Try
            server = clp.Arg(0)
            dbtypeStr = clp.Arg(1)
            database = clp.Arg(2)
            username = clp.Arg(3)
            password = clp.Arg(4)
        Catch
        End Try

        Try
            Dim dbtype = TradingDO.DatabaseTypeFromString(dbtypeStr)
            If dbtype = DatabaseTypes.DbNone Then
                TWUtilities.LogMessage("Error: invalid dbtype")
                setupDbServiceProviders = False
            End If

            If username <> "" And password = "" Then
                TWUtilities.LogMessage("Password not supplied")
                setupDbServiceProviders = False
            End If

            If setupDbServiceProviders Then
                mTB.ServiceProviders.Add(
                                    progId:="TBInfoBase27.ContractInfoSrvcProvider",
                                    Enabled:=True,
                                    paramString:=$"Role=PRIMARY" &
                                                $";Database Name={database}" &
                                                $";Database Type={dbtypeStr}" &
                                                $";Server={server}" &
                                                $";User Name={username}" &
                                                $";Password={password}" &
                                                ";Use Synchronous Reads=True",
                                    Description:="Primary contract data")

                mTB.ServiceProviders.Add(
                                    progId:="TBInfoBase27.HistDataServiceProvider",
                                    Enabled:=True,
                                    paramString:=$"Role=INPUT" &
                                                $";Database Name={database}" &
                                                $";Database Type={dbtypeStr}" &
                                                $";Server={server}" &
                                                $";User Name={username}" &
                                                $";Password={password}" &
                                                ";Use Synchronous Reads=False",
                                    Description:="Historical data input")

                If pAllowTickfiles Then
                    mTB.ServiceProviders.Add(
                                        progId:="TBInfoBase27.TickfileServiceProvider",
                                        Enabled:=True,
                                        paramString:=$"Role=INPUT" &
                                                    $";Database Name={database}" &
                                                    $";Database Type={dbtypeStr}" &
                                                    $";Server={server}" &
                                                    $";User Name={username}" &
                                                    $";Password={password}" &
                                                    ";Use Synchronous Reads=false",
                                        Description:="Tickfile input")
                End If
            End If
        Catch e As System.Runtime.InteropServices.COMException
            TWUtilities.LogMessage(Err.Description, LogLevels.LogLevelSevere)
            setupDbServiceProviders = False
        End Try
    End Function

    Private Function setupSimulateOrderServiceProviders(pLiveTrades As Boolean) As Boolean
        Try
            If Not pLiveTrades Then
                mTB.ServiceProviders.Add(progId:="TradeBuild27.OrderSimulatorSP", Enabled:=True, Name:="TradeBuild Exchange Simulator for Main Orders", paramString:="Role=LIVE", Description:="Simulated order submission for main orders")
            End If

            mTB.ServiceProviders.Add(progId:="TradeBuild27.OrderSimulatorSP", Enabled:=True, Name:="TradeBuild Exchange Simulator for Dummy Orders", paramString:="Role=SIMULATED", Description:="Simulated order submission for dummy orders")
            Return True
        Catch e As System.Runtime.InteropServices.COMException
            TWUtilities.LogMessage(e.Message, LogLevels.LogLevelSevere)
            Return False
        End Try
    End Function

    Private Function setupTwsServiceProviders(switchValue As String, pAllowLiveTrades As Boolean) As Boolean
        Dim clp = TWUtilities.CreateCommandLineParser(switchValue, ",")

        setupTwsServiceProviders = True

        Dim Server = String.Empty
        Dim Port = String.Empty
        Dim ClientId = String.Empty

        Try
            Server = clp.Arg(0)
            Port = clp.Arg(1)
            ClientId = clp.Arg(2)
        Catch
        End Try

        If Port = "" Then
            Port = "7496"
        ElseIf Not TWUtilities.IsInteger(Port, 1) Then
            TWUtilities.LogMessage("Error: Port must be a positive integer > 0")
            setupTwsServiceProviders = False
        End If

        If ClientId = "" Then
            ClientId = "1215339864"
        ElseIf Not TWUtilities.IsInteger(ClientId, 0) Then
            TWUtilities.LogMessage("Error: ClientId must be an integer >= 0")
            setupTwsServiceProviders = False
        End If

        If setupTwsServiceProviders Then
            Try
                If setupTwsServiceProviders Then
                    mTB.ServiceProviders.Add(progId:="IBTWSSP27.RealtimeDataServiceProvider", Enabled:=True, paramString:="Role=PRIMARY" & ";Server=" & Server & ";Port=" & Port & ";Client Id=" & ClientId & ";Provider Key=IB;Keep Connection=True", Description:="Realtime data")

                    If pAllowLiveTrades Then
                        mTB.ServiceProviders.Add(progId:="IBTWSSP27.OrderSubmissionSrvcProvider", Enabled:=True, paramString:="Server=" & Server & ";Port=" & Port & ";Client Id=" & ClientId & ";Provider Key=IB;Keep Connection=True", Description:="Live order submission")
                    End If
                End If
            Catch e As System.Runtime.InteropServices.COMException
                setupTwsServiceProviders = False
            End Try
        End If
    End Function

#End Region

End Module