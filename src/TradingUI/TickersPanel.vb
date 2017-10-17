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

Imports System.Windows.Forms

Imports AxTradingUI27
Imports MarketDataUtils27
Imports TWUtilities40
Imports WorkspaceUtils27

Imports TradeWright.Trading.UI.Contracts
Imports TradeWright.Trading.GeneralUtils

Public NotInheritable Class TickersPanel

    Event TickerPageNameChanged(s As Object, e As EventArgs)
    Event TickerSymbolEntered(s As Object, e As TickerSymbolEnteredEventArgs)

    Private Const ConfigSectionContractFilter As String = "ContractFilter"
    Private Const ConfigSectionTickerGrid As String = "TickerGrid"
    Private Const ConfigSectionTickerGridDefaults As String = "TickerGridDefaults"

    Private Const ConfigSettingTickerPageName As String = "&Name"
    Private Const ConfigSettingTickerPageWorkspaceName As String = "&WorkspaceName"

    Private Enum VB6MouseButtonConstants
        LeftButton = 1
        RightButton = 2
        MiddleButton = 4
    End Enum

    Private Enum VB6ShiftConstants
        AltMask = 4
        CtrlMask = 2
        ShiftMask = 1
    End Enum

    Private Shared _Index As Integer

    Private mTickerPageName As String

    Private mContractFilter As New ContractFilter

    Private mConfig As ConfigurationSection

    Private mWorkspaces As WorkSpaces
    Private mWorkspaceName As String
    Private mWorkspace As WorkSpace

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Index += 1
        Me.Name = "Tickers " & _Index
        mTickerPageName = Me.Name

        AddHandler TickerGrid1.ClickEvent, Sub(s, e) OnClick(New EventArgs)
        AddHandler TickerGrid1.DblClick, Sub(s, e) OnDoubleClick(New EventArgs)
        AddHandler TickerGrid1.KeyDownEvent, Sub(s, e) OnKeyDown(New KeyEventArgs(getKeyValue(e.keyCode, e.shift)))
        AddHandler TickerGrid1.KeyPressEvent, Sub(s, e) OnKeyPress(New KeyPressEventArgs(Microsoft.VisualBasic.ChrW(e.keyAscii)))
        AddHandler TickerGrid1.KeyUpEvent, Sub(s, e) OnKeyUp(New KeyEventArgs(getKeyValue(e.keyCode, e.shift)))
        AddHandler TickerGrid1.MouseDownEvent, Sub(s, e) OnMouseDown(New MouseEventArgs(getMouseButton(DirectCast(CInt(e.button), VB6MouseButtonConstants)), 1, CInt(e.x), CInt(e.y), 0))
        AddHandler TickerGrid1.MouseMoveEvent, Sub(s, e) OnMouseMove(New MouseEventArgs(getMouseButton(DirectCast(CInt(e.button), VB6MouseButtonConstants)), 1, CInt(e.x), CInt(e.y), 0))
        AddHandler TickerGrid1.MouseUpEvent, Sub(s, e) OnMouseUp(New MouseEventArgs(getMouseButton(DirectCast(CInt(e.button), VB6MouseButtonConstants)), 1, CInt(e.x), CInt(e.y), 0))

        AddHandler TickerGrid1.TickerSymbolEntered, Sub(s, e) OnTickerSymbolEntered(New TickerSymbolEnteredEventArgs(e.pSymbol, e.pPreferredRow))
    End Sub

    Public Sub Initialise(workspaces As WorkSpaces, marketDataManager As _IMarketDataManager)
        mWorkspaces = workspaces
        mWorkspaceName = TWUtilities.GenerateGUIDString

        TickerGrid1.Initialise(marketDataManager)
    End Sub

    Public Sub OnTickerSymbolEntered(e As TickerSymbolEnteredEventArgs)
        RaiseEvent TickerSymbolEntered(Me, e)
    End Sub

    Public WriteOnly Property ConfigurationSection As ConfigurationSection
        Set
            If value Is mConfig Then Exit Property
            mConfig = value
            If mConfig Is Nothing Then Exit Property
            TickerGrid1.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionTickerGrid)
            storeSettings()
        End Set
    End Property

    Public Property ContractFilter() As ContractFilter
        Get
            Return mContractFilter
        End Get
        Set
            If value Is Nothing Then
                If mContractFilter IsNot Nothing Then mContractFilter.RemoveFromConfig()
                mContractFilter = Nothing
            Else
                mContractFilter = value
                storeSettings()
            End If
        End Set
    End Property

    Public ReadOnly Property TickerGrid() As AxTickerGrid
        Get
            Return TickerGrid1
        End Get
    End Property

    Public ReadOnly Property TickerPageName() As String
        Get
            Return mTickerPageName
        End Get
    End Property

    Public ReadOnly Property Workspace As WorkSpace
        Get
            If mWorkspace Is Nothing Then
                If mWorkspaces.Contains(mWorkspaceName) Then
                    mWorkspace = mWorkspaces.Item(mWorkspaceName)
                Else
                    mWorkspace = mWorkspaces.Add(mWorkspaceName)
                End If
            End If
            Return mWorkspace
        End Get
    End Property

    Public Sub LoadFromConfig(config As ConfigurationSection)
        If config Is Nothing Then Throw New ArgumentException("config")

        mConfig = config
        mTickerPageName = mConfig.GetSetting(ConfigSettingTickerPageName)
        mWorkspaceName = mConfig.GetSetting(ConfigSettingTickerPageWorkspaceName)
        TickerGrid1.LoadFromConfig(mConfig.AddConfigurationSection(ConfigSectionTickerGrid))

        mContractFilter = New ContractFilter
        Dim cs = mConfig.GetConfigurationSection(ConfigSectionContractFilter)
        If cs IsNot Nothing Then
            mContractFilter.LoadFromConfig(cs)
        Else
            cs = mConfig.AddConfigurationSection(ConfigSectionContractFilter)
            mContractFilter.ConfigurationSection = cs
        End If

    End Sub

    Public Sub RemoveFromConfig()
        If Not mConfig Is Nothing Then mConfig.Remove()
    End Sub

    Public Sub SetTickerPageName(names As INameSet, name As String)
        If name = mTickerPageName Then Exit Sub
        names.Remove(mTickerPageName)
        names.Add(name)
        mTickerPageName = name
        storeSettings()
        RaiseEvent TickerPageNameChanged(Me, EventArgs.Empty)
    End Sub

    Private Function getKeyValue(keyCode As Short, shift As Short) As System.Windows.Forms.Keys
        Dim ctrlPressed = (shift And VB6ShiftConstants.CtrlMask) = VB6ShiftConstants.CtrlMask
        Dim shiftPressed = (shift And VB6ShiftConstants.ShiftMask) = VB6ShiftConstants.ShiftMask
        Dim altPressed = (shift And VB6ShiftConstants.AltMask) = VB6ShiftConstants.AltMask
        Dim keyVal As System.Windows.Forms.Keys = DirectCast(CInt(keyCode), System.Windows.Forms.Keys)
        If ctrlPressed Then keyVal = keyVal Or System.Windows.Forms.Keys.Control
        If shiftPressed Then keyVal = keyVal Or System.Windows.Forms.Keys.Shift
        If altPressed Then keyVal = keyVal Or System.Windows.Forms.Keys.Alt
        Return keyVal
    End Function

    Private Function getMouseButton(VB6button As VB6MouseButtonConstants) As System.Windows.Forms.MouseButtons
        Select Case VB6button
            Case VB6MouseButtonConstants.LeftButton
                Return MouseButtons.Left
            Case VB6MouseButtonConstants.MiddleButton
                Return MouseButtons.Middle
            Case VB6MouseButtonConstants.RightButton
                Return MouseButtons.Right
            Case Else
                Return MouseButtons.None
        End Select
    End Function

    Private Sub storeSettings()
        If mConfig Is Nothing Then Exit Sub
        mConfig.SetSetting(ConfigSettingTickerPageName, mTickerPageName)
        mConfig.SetSetting(ConfigSettingTickerPageWorkspaceName, mWorkspaceName)
        mContractFilter.ConfigurationSection = mConfig.AddConfigurationSection(ConfigSectionContractFilter)
    End Sub

End Class

Public Class TickerSymbolEnteredEventArgs
    Inherits EventArgs

    Private mSymbol As String
    Private mPreferredRow As Integer

    Public Sub New(symbol As String, preferredRow As Integer)
        mSymbol = symbol
        mPreferredRow = preferredRow
    End Sub

    Public ReadOnly Property PreferredRow As Integer
        Get
            Return mPreferredRow
        End Get
    End Property

    Public ReadOnly Property Symbol As String
        Get
            Return mSymbol
        End Get
    End Property

End Class
