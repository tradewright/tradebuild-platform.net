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
Imports WorkspaceUtils27

Imports System.Drawing
Imports System.Windows.Forms

Imports TradeWright.Trading.Utils.Contracts

Public Class ContractSearch
    Implements IButtonControl
    Implements IContractFetchListener

#Region "Interfaces"

#End Region

#Region "Events"

    Event DoAction(sender As Object, ev As EventArgs)

    Event Cancelled(sender As Object, ev As EventArgs)

    Event Cleared(sender As Object, ev As EventArgs)

    Event ContractsLoaded(sender As Object, ev As ContractsLoadedEventArgs)

    Event FetchError(sender As Object, ev As FetchErrorEventArgs)

    Event NoContracts(sender As Object, ev As EventArgs)

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const CancelCaption As String = "Cancel"
    Private Const ClearCaption As String = "Clear"

#End Region

#Region "Member variables"

    Private mContractFetcher As ContractFetcher

    Private mContractsBuilder As IContractsBuilder
    Private mSortKeys() As ContractSortKeyId = {ContractSortKeyId.SecType,
                                                ContractSortKeyId.Symbol,
                                                ContractSortKeyId.Exchange,
                                                ContractSortKeyId.Currency,
                                                ContractSortKeyId.Expiry,
                                                ContractSortKeyId.Strike,
                                                ContractSortKeyId.Right}

    Private mLoadingContracts As Boolean

    Private mAllowMultipleSelection As Boolean

    Private WithEvents mFutureWaiter As New FutureWaiter

    Private mCookie As Object

    Private mTheme As ITheme

    Private WithEvents mContractSelectorInitialisationTC As TaskController

    Private mCancelInitiatedByUser As Boolean

    Private mSingleContracts As IContracts

    Private mHistoricalContractsFound As Boolean

    Private mDialogResult As DialogResult

    Private mAction As Action(Of IContracts, WorkSpace)
    Private mWorkSpace As WorkSpace

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ContractSpecBuilder1.Visible = True
        ContractSelector1.Visible = False
        IncludeHistoricalContracts = False
    End Sub

#End Region

#Region "Constructors"

    Protected Sub OnDoAction(ev As EventArgs)
        If mAction IsNot Nothing Then mAction(SelectedContracts, mWorkSpace)
        RaiseEvent DoAction(Me, ev)
    End Sub

    Protected Sub OnCancelled(ev As EventArgs)
        RaiseEvent Cancelled(Me, ev)
    End Sub

    Protected Sub OnCleared(ev As EventArgs)
        RaiseEvent Cleared(Me, ev)
    End Sub

    Protected Sub OnContractsLoaded(ev As ContractsLoadedEventArgs)
        RaiseEvent ContractsLoaded(Me, ev)
    End Sub

    Protected Overrides Sub OnEnter(e As System.EventArgs)
        If ContractSpecBuilder1.Visible Then
            ContractSpecBuilder1.Focus()
        Else
            ContractSelector1.Focus()
        End If
        MyBase.OnEnter(e)
    End Sub

    Protected Sub OnFetchError(ev As FetchErrorEventArgs)
        RaiseEvent FetchError(Me, ev)
    End Sub

    Protected Sub OnNoContracts(ev As EventArgs)
        RaiseEvent NoContracts(Me, ev)
    End Sub

#End Region

#Region "IButtonControl Interface Members"

    Public Property DialogResult() As DialogResult Implements IButtonControl.DialogResult
        Get
            Return mDialogResult
        End Get
        Set
            mDialogResult = Value
        End Set
    End Property

    Public Sub NotifyDefault(value As Boolean) Implements IButtonControl.NotifyDefault
        ' we should make ActionButton a subclass of Button so we can set its IsDefault property
    End Sub

    Public Sub PerformClick() Implements System.Windows.Forms.IButtonControl.PerformClick
        ActionButton.PerformClick()
    End Sub

#End Region

#Region "IContractFetchListener Interface Members"

    Public Sub FetchCancelled(pCookie As Object) Implements IContractFetchListener.FetchCancelled

    End Sub

    Public Sub FetchCompleted(pCookie As Object) Implements IContractFetchListener.FetchCompleted

    End Sub

    Public Sub FetchFailed(pCookie As Object, pErrorCode As Integer, pErrorMessage As String, pErrorSource As String) Implements IContractFetchListener.FetchFailed

    End Sub

    Public Sub NotifyContract(pCookie As Object, pContract As IContract) Implements IContractFetchListener.NotifyContract
        If Not IncludeHistoricalContracts And Contract.IsContractExpired(pContract) Then
            mHistoricalContractsFound = True
            Exit Sub
        End If

        If mContractsBuilder Is Nothing Then
            mContractsBuilder = DirectCast(New ContractsBuilder, IContractsBuilder)
            mContractsBuilder.Contracts.SortKeys = mSortKeys
            ActionButton.Enabled = False
            ClearButton.Visible = True
            ClearButton.Text = CancelCaption
            ClearButton.Enabled = True
            ContractSelector1.Clear()
            ContractSelector1.Visible = True
            ContractSpecBuilder1.Visible = False
        End If

        mContractsBuilder.Add(pContract)

        If mContractsBuilder.Contracts.Count Mod 100 = 0 Then
            MessageLabel.Text = getContractsCountMessage(mContractsBuilder.Contracts.Count)
            MessageLabel.Refresh()
        End If
    End Sub

#End Region

#Region "Control Event Handlers"

    Private Sub ActionButton_Click(sender As System.Object, e As System.EventArgs) Handles ActionButton.Click
        If ContractSelector1.Visible Then
            OnDoAction(EventArgs.Empty)
            mCookie = Nothing
        Else
            mSingleContracts = Nothing
            mHistoricalContractsFound = False
            mFutureWaiter.Add(mContractFetcher.FetchContracts(ContractSpecBuilder1.ContractSpecifier, Me))
            mLoadingContracts = True
            UseWaitCursor = True
            MessageLabel.Text = "Searching..."
            ActionButton.Enabled = False
        End If
    End Sub

    Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click
        If ClearButton.Text = CancelCaption Then
            mCancelInitiatedByUser = True
            CancelSearch()
        Else
            Clear()
            OnCleared(EventArgs.Empty)
        End If
    End Sub

    Private Sub ContractSelector1_SelectionChanged(sender As Object, args As EventArgs) Handles ContractSelector1.SelectionChanged
        ActionButton.Enabled = True
    End Sub

    Private Sub ContractSelector1_SelectionCleared(sender As Object, args As EventArgs) Handles ContractSelector1.SelectionCleared
        ActionButton.Enabled = False
    End Sub

    Private Sub ContractSpecBuilder1_NotReady(sender As Object, ev As EventArgs) Handles ContractSpecBuilder1.NotReady
        If Not mLoadingContracts Then ActionButton.Enabled = False
    End Sub

    Private Sub ContractSpecBuilder1_Ready(sender As Object, ev As System.EventArgs) Handles ContractSpecBuilder1.Ready
        If Not mLoadingContracts Then
            ActionButton.Enabled = True
        End If
    End Sub

#End Region

#Region "mContractSelectorInitialisationTC Event Handlers"

    Private Sub mContractSelectorInitialisationTC_Completed(ByRef ev As TaskCompletionEventData) Handles mContractSelectorInitialisationTC.Completed
        Try
            If CBool(ev.Cancelled) Then
                Clear()
                MessageLabel.Text = "Cancelled"
                If mCancelInitiatedByUser Then
                    OnCancelled(EventArgs.Empty)
                    mCancelInitiatedByUser = False
                End If
            ElseIf ev.ErrorNumber <> 0 Then
                Clear()

                Dim lEv As ErrorEventData
                lEv.Source = Me
                lEv.ErrorCode = ev.ErrorNumber
                lEv.ErrorMessage = ev.ErrorMessage
                lEv.ErrorSource = ev.ErrorSource
                OnFetchError(New FetchErrorEventArgs(ev.ErrorNumber, ev.ErrorMessage, ev.ErrorSource))
            Else
                contractSelectorCompletion()
                Dim lContractsSuppliedByCaller As Boolean
                lContractsSuppliedByCaller = CBool(ev.Cookie)
                If Not lContractsSuppliedByCaller Then OnContractsLoaded(New ContractsLoadedEventArgs(DirectCast(ev.Result, IContracts)))
            End If

            UseWaitCursor = False
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mContractSelectorInitialisationTC_Completed), NameOf(ContractSearch))
        End Try
    End Sub

#End Region

#Region "mFutureWaiter Event Handlers"

    Private Sub mFutureWaiter_WaitCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitCompleted
        Try
            If ev.Future.IsPending Then Exit Sub

            UseWaitCursor = False

            If ev.Future.IsCancelled Then
                Clear()

                If mCancelInitiatedByUser Then
                    OnCancelled(EventArgs.Empty)
                    mCancelInitiatedByUser = False
                End If
            ElseIf ev.Future.IsFaulted Then
                Clear()

                Dim lEv As ErrorEventData
                lEv.Source = Me
                lEv.ErrorCode = ev.Future.ErrorNumber
                lEv.ErrorMessage = ev.Future.ErrorMessage
                lEv.ErrorSource = ev.Future.ErrorSource
                OnFetchError(New FetchErrorEventArgs(ev.Future.ErrorNumber, ev.Future.ErrorMessage, ev.Future.ErrorSource))
            Else
                If mContractsBuilder Is Nothing Then
                    If mHistoricalContractsFound Then
                        MessageLabel.Text = "No unexpired contracts"
                    Else
                        MessageLabel.Text = "No contracts"
                    End If
                    UseWaitCursor = False
                    OnNoContracts(EventArgs.Empty)
                Else
                    MessageLabel.Text = getContractsCountMessage(mContractsBuilder.Contracts.Count)
                    mContractSelectorInitialisationTC = handleContractsLoaded(mContractsBuilder.Contracts, False)
                    If mContractSelectorInitialisationTC Is Nothing Then
                        ClearButton.Text = ClearCaption
                        UseWaitCursor = False
                        OnContractsLoaded(New ContractsLoadedEventArgs(mContractsBuilder.Contracts))
                    End If
                End If
            End If

            'System.Runtime.InteropServices.Marshal.ReleaseComObject(mContractsBuilder)
            mContractsBuilder = Nothing
        Catch e As Exception
            NotifyUnhandledError(e, NameOf(mFutureWaiter_WaitCompleted), NameOf(ContractSearch))
        End Try
    End Sub

#End Region

#Region "Properties"

    Public WriteOnly Property Action As Action(Of IContracts, WorkSpace)
        Set
            mAction = Value
        End Set
    End Property

    Public Property ActionButtonCaption() As String
        Get
            Return ActionButton.Text
        End Get
        Set
            ActionButton.Text = Value
        End Set
    End Property

    Public Property AllowMultipleSelection() As Boolean
        Get
            Return mAllowMultipleSelection
        End Get
        Set
            mAllowMultipleSelection = Value
        End Set
    End Property

    Public WriteOnly Property ContractFilter() As ContractFilter
        Set
            ContractSpecBuilder1.ContractFilter = Value
        End Set
    End Property

    Public ReadOnly Property Cookie() As Object
        Get
            Return mCookie
        End Get
    End Property

    Public Property IncludeHistoricalContracts() As Boolean
        Get
            Return ContractSelector1.IncludeHistoricalContracts
        End Get
        Set
            ContractSelector1.IncludeHistoricalContracts = Value
        End Set
    End Property

    Public Property RowBackColorEven() As Color
        Get
            Return ContractSelector1.RowBackColorEven
        End Get
        Set
            ContractSelector1.RowBackColorEven = Value
        End Set
    End Property

    Public Property RowBackColorOdd() As Color
        Get
            Return ContractSelector1.RowBackColorOdd
        End Get
        Set
            ContractSelector1.RowBackColorOdd = Value
        End Set
    End Property

    Public ReadOnly Property SelectedContracts() As IContracts
        Get
            If Not mSingleContracts Is Nothing Then
                Return mSingleContracts
            Else
                Return ContractSelector1.SelectedContracts
            End If
        End Get
    End Property

    Public WriteOnly Property Workspace() As WorkSpace
        Set
            If Value IsNot mWorkSpace Then Clear()
            mWorkSpace = Value
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub CancelSearch()
        If Not mContractsBuilder Is Nothing Then
            mFutureWaiter.Cancel()
        ElseIf Not mContractSelectorInitialisationTC Is Nothing Then
            mContractSelectorInitialisationTC.CancelTask()
        End If
    End Sub

    Public Sub Clear()
        mSingleContracts = Nothing
        MessageLabel.Text = ""
        ContractSpecBuilder1.Visible = True
        ContractSelector1.Visible = False
        ContractSelector1.Clear()
        ClearButton.Text = ClearCaption
        ClearButton.Visible = False
        ContractSpecBuilder1.Focus()
        ClearButton.Visible = False

        setActionButton()
    End Sub

    Public Sub Initialise(
                pContractFetcher As ContractFetcher)
        mContractFetcher = pContractFetcher
    End Sub

    Public Sub LoadContracts(pContracts As IContracts, Optional pCookie As Object = Nothing)
        If pContracts Is Nothing Then Exit Sub
        If pContracts.Count = 1 Then Exit Sub   ' to prevent Action event firing

        ActionButton.Enabled = True
        Me.ActiveControl = ActionButton

        mCookie = pCookie

        UseWaitCursor = False
        ClearButton.Text = CancelCaption
        ClearButton.Visible = True
        ClearButton.Enabled = True

        ContractSelector1.Visible = True
        ContractSpecBuilder1.Visible = False

        mContractSelectorInitialisationTC = handleContractsLoaded(pContracts, True)
        If mContractSelectorInitialisationTC Is Nothing Then
            ClearButton.Text = ClearCaption
            UseWaitCursor = False
        End If
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub contractSelectorCompletion()
        mLoadingContracts = False
        MessageLabel.Text = getContractsCountMessage(ContractSelector1.Count)
        ActionButton.Enabled = False
        ClearButton.Text = ClearCaption

        ContractSelector1.Focus()
    End Sub

    Private Function getContractsCountMessage(pCount As Long) As String
        getContractsCountMessage = pCount & CStr(IIf(pCount = 1, " contract", " contracts"))
    End Function

    Private Function handleContractsLoaded(contracts As IContracts, ContractsSuppliedByCaller As Boolean) As TaskController
        If contracts.Count = 1 Then
            mSingleContracts = contracts
            OnDoAction(EventArgs.Empty)
            Clear()
            mCookie = Nothing
            Return Nothing
        Else
            Return setupContractSelector(contracts, mAllowMultipleSelection, ContractsSuppliedByCaller)
        End If
    End Function

    Private Sub setActionButton()
        If ContractSpecBuilder1.isReady Then
            ActionButton.Enabled = True
            Me.ActiveControl = ActionButton
        Else
            ActionButton.Enabled = False
            Me.ActiveControl = Nothing
        End If
    End Sub

    Private Function setupContractSelector(
                pContracts As IContracts,
                pAllowMultipleSelection As Boolean,
                pContractsSuppliedByCaller As Boolean) As TaskController
        If pContracts.Count <= 20 Then
            ContractSelector1.Initialise(DirectCast(pContracts, IContracts), pAllowMultipleSelection)
            contractSelectorCompletion()
            Return Nothing
        Else
            Return ContractSelector1.InitialiseAsync(DirectCast(pContracts, IContracts), pAllowMultipleSelection, pContractsSuppliedByCaller)
        End If
    End Function

#End Region

End Class

Public Class ContractsLoadedEventArgs
    Inherits System.EventArgs

    Private _Contracts As IContracts

    Friend Sub New(contracts As IContracts)
        Me._Contracts = contracts
    End Sub

    Public ReadOnly Property Contracts As IContracts
        Get
            Return Me._Contracts
        End Get
    End Property

End Class

Public Class FetchErrorEventArgs
    Inherits EventArgs

    Private _ErrorNumber As Integer
    Private _ErrorMessage As String
    Private _ErrorSource As String

    Sub New(errorNumber As Integer, errorMessage As String, errorSource As String)
        _ErrorNumber = errorNumber
        _ErrorMessage = errorMessage
        _ErrorSource = errorSource
    End Sub

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return _ErrorMessage
        End Get
    End Property

    Public ReadOnly Property ErrorNumber() As Integer
        Get
            Return _ErrorNumber
        End Get
    End Property

    Public ReadOnly Property ErrorSource() As String
        Get
            Return _ErrorSource
        End Get
    End Property

End Class

