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

Imports System.Drawing
Imports System.Windows.Forms

Imports TradeWright.Trading.Utils.Contracts

Public Class ContractSelector
    Implements ITask

    Event SelectionChanged(sender As Object, args As EventArgs)
    Event SelectionCleared(sender As Object, args As EventArgs)

#Region "Enums"

    Private Enum ContractsGridColumns
        SecType
        LocalSymbol = SecType
        Exchange
        Expiry = Exchange
        CurrencyCode
        Strike = CurrencyCode
        OptionRight
        Filler
        MaxColumn = Filler
    End Enum

#End Region

#Region "Types"

#End Region

#Region "Constants"

    Private Const ModuleName = "ContractSelector"

#End Region

#Region "Member variables"

    Private mContracts As IContracts

    Private mAllowMultipleSelection As Boolean = True

    Private mIncludeHistoricalContracts As Boolean = False

    Private mSortKeys() As ContractSortKeyId = {ContractSortKeyId.SecType,
                                                ContractSortKeyId.Symbol,
                                                ContractSortKeyId.Currency,
                                                ContractSortKeyId.Exchange,
                                                ContractSortKeyId.Mutiplier,
                                                ContractSortKeyId.LocalSymbol,
                                                ContractSortKeyId.Expiry,
                                                ContractSortKeyId.Strike,
                                                ContractSortKeyId.Right}


    Private mCurrSectype As SecurityType
    Private mCurrCurrency As String
    Private mCurrExchange As String

    Private mControlDown As Boolean
    Private mShiftDown As Boolean
    Private mAltDown As Boolean

    Private mLoading As Boolean

    Private mSelectedRows As Dictionary(Of Integer, Integer)

    Private mCount As Integer

    Private mTaskContext As TaskContext
    Private mFirstTime As Boolean

    Private mPriceFormatter As New PriceFormatter()

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region

#Region "Constructors"

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        setupGrid()
        MyBase.OnLoad(e)
    End Sub

#End Region

#Region "ITask Interface Members"

    Private Sub ITask_Cancel() Implements _ITask.Cancel
        ContractsGrid.Rows.Clear()
        'ContractsGrid.Redraw = True
        mTaskContext.Finish(Nothing, True)
    End Sub

    Private Sub ITask_Run() Implements _ITask.Run
        Const ProcName = "ITask_Run"
        If mTaskContext.CancelPending Then
            ContractsGrid.Rows.Clear()
            'TWGrid1.Redraw = True
            mTaskContext.Finish(Nothing, True)
            Exit Sub
        End If

        Static et As ElapsedTimer
        Static sCounter As Long

        If mFirstTime Then
            mFirstTime = False
            sCounter = 0
            et = New ElapsedTimer
            et.StartTiming()
        End If

        If processNextContract Then
            sCounter = sCounter + 1
            If sCounter Mod 250 = 0 Then
                'ContractsGrid.Redraw = True
                'ContractsGrid.Redraw = False
            End If
        Else
            mLoading = False
            ' ContractsGrid.Redraw = True

            gLogger.Log("Loaded " & CStr(mContracts.Count) & " contracts: elapsed time (millisecs)", ProcName, ModuleName, LogLevels.LogLevelDetail, CStr(Int(et.ElapsedTimeMicroseconds / 1000.0#)))
            et = Nothing
            mTaskContext.Finish(mContracts, False)
        End If
    End Sub

    Private ReadOnly Property ITask_TaskName As String Implements _ITask.TaskName
        Get
            Return mTaskContext.Name
        End Get
    End Property

    Private WriteOnly Property ITask_TaskContext As TaskContext Implements _ITask.TaskContext
        Set
            mTaskContext = Value
            mFirstTime = True
        End Set
    End Property

#End Region

#Region "Control Event Handlers"

    Private Sub ContractsGrid_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ContractsGrid.CellClick
        If Not mControlDown Or Not mAllowMultipleSelection Then
            deselectSelectedContracts()
            selectContract(e.RowIndex)
        Else
            toggleRowSelection(e.RowIndex)
        End If

        If mSelectedRows.Count > 0 Then
            OnSelectionChanged(EventArgs.Empty)
        Else
            OnSelectionCleared(EventArgs.Empty)
        End If
        OnClick(EventArgs.Empty)
    End Sub

    Private Sub ContractsGrid_DoubleClick(sender As Object, e As System.EventArgs) Handles ContractsGrid.DoubleClick
        OnDoubleClick(e)
    End Sub

    Private Sub ContractsGrid_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ContractsGrid.KeyDown
        mShiftDown = e.Shift
        mControlDown = e.Control
        mAltDown = e.Alt
        OnKeyDown(e)
    End Sub

    Private Sub ContractsGrid_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles ContractsGrid.KeyPress
        OnKeyPress(e)
    End Sub

    Private Sub ContractsGrid_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ContractsGrid.KeyUp
        mShiftDown = e.Shift
        mControlDown = e.Control
        mAltDown = e.Alt
        OnKeyUp(e)
    End Sub

    Private Sub ContractsGrid_SelectionChanged(sender As Object, e As System.EventArgs) Handles ContractsGrid.SelectionChanged
        If mLoading Then Exit Sub
        For Each row As DataGridViewRow In ContractsGrid.SelectedRows
            If row.Tag Is Nothing Then row.Selected = False
        Next
    End Sub

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public ReadOnly Property Count() As Integer
        Get
            Return mCount
        End Get
    End Property

    Public Property IncludeHistoricalContracts() As Boolean
        Get
            Return mIncludeHistoricalContracts
        End Get
        Set
            mIncludeHistoricalContracts = Value
        End Set
    End Property

    Public Property MultiSelect() As Boolean
        Get
            Return ContractsGrid.MultiSelect
        End Get
        Set
            ContractsGrid.MultiSelect = Value
        End Set
    End Property

    Public Property RowBackColorEven() As Color
        Get
            Return ContractsGrid.DefaultCellStyle.BackColor
        End Get
        Set
            ContractsGrid.RowsDefaultCellStyle.BackColor = Value
        End Set
    End Property

    Public Property RowBackColorOdd() As Color
        Get
            Return ContractsGrid.AlternatingRowsDefaultCellStyle.BackColor
        End Get
        Set
            ContractsGrid.AlternatingRowsDefaultCellStyle.BackColor = Value
        End Set
    End Property

    Public Property ScrollBars() As ScrollBars
        Get
            Return ContractsGrid.ScrollBars
        End Get
        Set
            ContractsGrid.ScrollBars = Value
        End Set
    End Property

    Public ReadOnly Property SelectedContracts() As IContracts
        Get
            Dim scb = DirectCast(New ContractsBuilder, IContractsBuilder)

            For Each row As DataGridViewRow In ContractsGrid.SelectedRows
                scb.Add(DirectCast(row.Tag, IContract))
            Next

            Return scb.Contracts
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub Clear()
        ContractsGrid.Rows.Clear()
    End Sub

    Public Sub Initialise(
                    pContracts As IContracts,
                    pAllowMultipleSelection As Boolean,
                    Optional pPriceFormatter As PriceFormatter = Nothing)
        doInitialise(False, pContracts, pAllowMultipleSelection, pPriceFormatter, Nothing)
    End Sub

    Public Function InitialiseAsync(
                    pContracts As IContracts,
                    pAllowMultipleSelection As Boolean,
                    pCookie As Object,
                    Optional pPriceFormatter As PriceFormatter = Nothing) As TaskController
        Return doInitialise(True, pContracts, pAllowMultipleSelection, pPriceFormatter, pCookie)
    End Function

    Public Sub OnSelectionChanged(e As EventArgs)
        RaiseEvent SelectionChanged(Me, e)
    End Sub

    Public Sub OnSelectionCleared(e As EventArgs)
        RaiseEvent SelectionCleared(Me, e)
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub deselectContract(Row As Integer)
        mSelectedRows.Remove(Row)
    End Sub

    Private Sub deselectSelectedContracts()
        mSelectedRows.Clear()
    End Sub

    Private Function doInitialise(
                pAsync As Boolean,
                pContracts As IContracts,
                pAllowMultipleSelection As Boolean,
                pPriceFormatter As PriceFormatter,
                pCookie As Object) As TaskController

        Const ProcName = "doInitialise"
        mLoading = True
        mCount = 0
        mAllowMultipleSelection = pAllowMultipleSelection
        mPriceFormatter = pPriceFormatter

        ContractsGrid.Rows.Clear()
        'ContractsGrid.Redraw = False

        mContracts = pContracts
        mContracts.SortKeys = mSortKeys

        mSelectedRows = New Dictionary(Of Integer, Integer)

        If pAsync Then Return TWUtilities.StartTask(Me, TaskPriorities.PriorityHigh, , pCookie)

        Dim et = New ElapsedTimer
        et.StartTiming()

        Do While processNextContract() : Loop
        mLoading = False
        'ContractsGrid.Redraw = True

        gLogger.Log("Loaded " & CStr(mContracts.Count) & " contracts: elapsed time (millisecs)", ProcName, ModuleName, LogLevels.LogLevelDetail, CStr(Int(et.ElapsedTimeMicroseconds / 1000.0#)))
        Return Nothing
    End Function

    Private Function isFullHeadingSecType(secType As SecurityType) As Boolean
        Return secType = SecurityType.Future Or
            secType = SecurityType.FuturesOption Or
            secType = SecurityType.Option
    End Function

    Private Function isHeadingWithoutExchangeSecType(secType As SecurityType) As Boolean
        Return secType = SecurityType.Stock Or
            secType = SecurityType.Index
    End Function

    Private Function isHeadingWithoutCurrencySecType(secType As SecurityType) As Boolean
        Return secType = SecurityType.Stock Or
            secType = SecurityType.Cash Or
            secType = SecurityType.Index
    End Function

    Private Function isRowSelected(Row As Integer) As Boolean
        isRowSelected = mSelectedRows.ContainsKey(Row)
    End Function

    Private Function needFullHeadingRow(contractSpec As IContractSpecifier) As Boolean
        Return (contractSpec.SecType <> mCurrSectype Or
            contractSpec.CurrencyCode <> mCurrCurrency Or
            contractSpec.Exchange <> mCurrExchange) And
            isFullHeadingSecType(contractSpec.SecType)
    End Function

    Private Function needHeadingRow(contractSpec As IContractSpecifier) As Boolean
        Return needFullHeadingRow(contractSpec) Or
            needHeadingRowWithoutExchange(contractSpec) Or
            needHeadingRowWithoutCurrency(contractSpec) Or
            needHeadingRowWithSectypeOnly(contractSpec)
    End Function

    Private Function needHeadingRowWithoutExchange(contractSpec As IContractSpecifier) As Boolean
        Return (contractSpec.SecType <> mCurrSectype Or
            contractSpec.CurrencyCode <> mCurrCurrency) And
            isHeadingWithoutExchangeSecType(contractSpec.SecType) And
            (Not isHeadingWithoutExchangeSecType(contractSpec.SecType))
    End Function

    Private Function needHeadingRowWithoutCurrency(contractSpec As IContractSpecifier) As Boolean
        Return (contractSpec.SecType <> mCurrSectype Or
            contractSpec.Exchange <> mCurrExchange) And
            isHeadingWithoutCurrencySecType(contractSpec.SecType) And
            (Not isHeadingWithoutExchangeSecType(contractSpec.SecType))
    End Function

    Private Function needHeadingRowWithSectypeOnly(contractSpec As IContractSpecifier) As Boolean
        Return contractSpec.SecType <> mCurrSectype And
            isHeadingWithoutExchangeSecType(contractSpec.SecType) And
            isHeadingWithoutCurrencySecType(contractSpec.SecType)
    End Function

    Private Function processNextContract() As Boolean
        Static en As IEnumerator(Of IContract)
        If en Is Nothing Then en = mContracts.GetEnumerator

        If Not en.MoveNext Then
            en = Nothing
            mCurrSectype = SecurityType.None
            mCurrCurrency = ""
            mCurrExchange = ""

            Return False
        End If

        Dim lContract = DirectCast(en.Current, IContract)

        Dim lContractSpec = lContract.Specifier

        If IncludeHistoricalContracts Or Not Contract.IsContractExpired(lContract) Then
            If needHeadingRow(lContractSpec) Then writeHeadingRow(lContractSpec)
            writeRow(lContract)

            mCurrSectype = lContractSpec.SecType
            mCurrCurrency = lContractSpec.CurrencyCode
            mCurrExchange = lContractSpec.Exchange
        End If

        Return True
    End Function

    Private Sub selectContract(
                ByVal Row As Integer)
        mSelectedRows.Add(Row, Row)
        'highlightRow (Row)
    End Sub

    Private Sub setupGrid()
        ContractsGrid.AutoResizeColumns()
        ContractsGrid.RowHeadersVisible = False
        ContractsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        ContractsGrid.BackgroundColor = SystemColors.Window
        ContractsGrid.ColumnHeadersVisible = False


        ContractsGrid.RowsDefaultCellStyle.BackColor = Color.FromArgb(&HEE, &HEE, &HEE)
        ContractsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(&HF8, &HF8, &HF8)
    End Sub

    Private Sub toggleRowSelection(Row As Integer)
        If isRowSelected(Row) Then
            deselectContract(Row)
        Else
            selectContract(Row)
        End If
    End Sub

    Private Sub writeHeadingRow(contractSpec As IContractSpecifier)

        Dim cols(ContractsGridColumns.MaxColumn) As String

        cols(ContractsGridColumns.SecType) = Contract.SecTypeToString(contractSpec.SecType)
        cols(ContractsGridColumns.Exchange) = CStr(IIf(isHeadingWithoutExchangeSecType(contractSpec.SecType), "", contractSpec.Exchange))
        cols(ContractsGridColumns.CurrencyCode) = CStr(IIf(isHeadingWithoutCurrencySecType(contractSpec.SecType), "", contractSpec.CurrencyCode))

        Dim row = ContractsGrid.Rows(ContractsGrid.Rows.Add(cols))

        Select Case contractSpec.SecType
            Case SecurityType.Stock
                row.DefaultCellStyle.BackColor = Color.FromArgb(&H51, &H9F, &H35)
            Case SecurityType.Future
                row.DefaultCellStyle.BackColor = Color.FromArgb(&HA0, &H5D, &H34)
            Case SecurityType.Option
                row.DefaultCellStyle.BackColor = Color.FromArgb(&H50, &HB6, &HB4)
            Case SecurityType.FuturesOption
                row.DefaultCellStyle.BackColor = Color.FromArgb(&HAB, &H5B, &H8B)
            Case SecurityType.Cash
                row.DefaultCellStyle.BackColor = Color.FromArgb(&H44, &H90, &H98)
            Case SecurityType.Combo
                row.DefaultCellStyle.BackColor = Color.FromArgb(&H98, &H7E, &H6A)
            Case SecurityType.Index
                row.DefaultCellStyle.BackColor = Color.FromArgb(&H50, &H4A, &HC8)
        End Select
        row.DefaultCellStyle.Font = New System.Drawing.Font(ContractsGrid.Font, FontStyle.Bold)
        row.DefaultCellStyle.ForeColor = Color.White

    End Sub

    Private Sub writeRow(
                    pContract As IContract)
        Dim contractSpec = pContract.Specifier

        Dim cols(ContractsGridColumns.MaxColumn) As String

        cols(ContractsGridColumns.LocalSymbol) = contractSpec.LocalSymbol

        If isFullHeadingSecType(contractSpec.SecType) Then
        Else
            If isHeadingWithoutExchangeSecType(contractSpec.SecType) Then
                cols(ContractsGridColumns.Exchange) = contractSpec.Exchange
            End If
            If isHeadingWithoutCurrencySecType(contractSpec.SecType) Then
                cols(ContractsGridColumns.CurrencyCode) = contractSpec.CurrencyCode
            End If
        End If

        Select Case contractSpec.SecType
            Case SecurityType.Future
                cols(ContractsGridColumns.Expiry) = FormatDateTime(pContract.ExpiryDate, vbShortDate)
            Case SecurityType.Option, SecurityType.FuturesOption
                cols(ContractsGridColumns.Expiry) = FormatDateTime(pContract.ExpiryDate, vbShortDate)

                cols(ContractsGridColumns.OptionRight) = Contract.OptionRightToString(contractSpec.Right)

                cols(ContractsGridColumns.Strike) = mPriceFormatter.FormatPrice(contractSpec.Strike, contractSpec.SecType, pContract.TickSize)
        End Select

        Dim row = ContractsGrid.Rows(ContractsGrid.Rows.Add(cols))

        row.Tag = pContract

        If contractSpec.SecType = SecurityType.Option Or contractSpec.SecType = SecurityType.FuturesOption Then
            row.Cells(ContractsGridColumns.Strike).Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

    End Sub

#End Region

End Class
