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

Imports System.Drawing
Imports System.Windows.Forms

Imports TradeWright.Trading.Utils.Contracts

Public Class ContractSpecBuilder

#Region "Interfaces"

#End Region

#Region "Events"

    Event NotReady(sender As Object, ev As EventArgs)
    Event Ready(sender As Object, ev As EventArgs)

#End Region

#Region "Constants"

#End Region

#Region "Enums"

#End Region

#Region "Types"

#End Region

#Region "Member variables"

    Private mReady As Boolean

    Private mModeAdvanced As Boolean = True

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region

#Region "Constructors"

    Protected Overrides Sub OnGotFocus(e As System.EventArgs)
        If Not mModeAdvanced Then
            SymbolText.Focus()
        ElseIf ShortNameText.Text <> "" Then
            ShortNameText.Focus()
        ElseIf SymbolText.Text <> "" Then
            SymbolText.Focus()
        Else
            ShortNameText.Focus()
        End If
        MyBase.OnGotFocus(e)
    End Sub

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        mReady = False

        RaiseEvent NotReady(Me, EventArgs.Empty)

        TypeCombo.Items.Add(Contract.SecTypeToString(SecurityType.Stock))
        TypeCombo.Items.Add(Contract.SecTypeToString(SecurityType.Future))
        TypeCombo.Items.Add(Contract.SecTypeToString(SecurityType.Option))
        TypeCombo.Items.Add(Contract.SecTypeToString(SecurityType.FuturesOption))
        TypeCombo.Items.Add(Contract.SecTypeToString(SecurityType.Cash))
        TypeCombo.Items.Add(Contract.SecTypeToString(SecurityType.Index))

        RightCombo.Items.Add(Contract.OptionRightToString(OptionRight.Call))
        RightCombo.Items.Add(Contract.OptionRightToString(OptionRight.Put))

        Dim exchangeCodes = Exchange.GetExchangeCodes()

        ExchangeCombo.Items.Add("")
        For Each exchange In exchangeCodes
            ExchangeCombo.Items.Add(exchange)
        Next

        Dim currDescs = GetCurrencyDescriptors()

        CurrencyCombo.Items.Add("")
        For Each currDesc In currDescs
            CurrencyCombo.Items.Add(currDesc)
        Next

        MyBase.OnLoad(e)
    End Sub

#End Region

#Region "Control Event Handlers"

    Private Sub AdvancedButton_Click(sender As Object, e As System.EventArgs) Handles AdvancedButton.Click
        ModeAdvanced = Not ModeAdvanced
        If Not ModeAdvanced Then
            ShortNameText.Text = ""
            TypeCombo.Text = ""
            ExpiryText.Text = ""
            ExchangeCombo.Text = ""
            CurrencyCombo.Text = ""
            MultiplierText.Text = ""
            StrikePriceText.Text = ""
            RightCombo.Text = ""
        End If
    End Sub

    Private Sub Combo_GotFocus(sender As Object, e As System.EventArgs) Handles CurrencyCombo.GotFocus, ExchangeCombo.GotFocus, RightCombo.GotFocus, TypeCombo.GotFocus
        Dim combo = CType(sender, ComboBox)
        combo.SelectAll()
    End Sub

    Private Sub CurrencyCombo_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles CurrencyCombo.SelectionChangeCommitted
        handleCurrencyComboChange()
    End Sub

    Private Sub ExchangeCombo_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles ExchangeCombo.SelectionChangeCommitted
        checkIfValid()
    End Sub

    Private Sub RightCombo_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles RightCombo.SelectionChangeCommitted
        checkIfValid()
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles SymbolText.TextChanged, StrikePriceText.TextChanged, ShortNameText.TextChanged, MultiplierText.TextChanged, ExpiryText.TextChanged
        Dim t = DirectCast(sender, TextBox)
        Dim s = t.SelectionStart
        Dim l = t.SelectionLength
        t.Text = t.Text.ToUpper()
        t.SelectionStart = s
        t.SelectionLength = l
        checkIfValid()
    End Sub

    Private Sub TextBox_GotFocus(sender As Object, e As System.EventArgs) Handles ShortNameText.GotFocus, SymbolText.GotFocus, ExpiryText.GotFocus, MultiplierText.GotFocus, StrikePriceText.GotFocus
        Dim txtbox = CType(sender, TextBox)
        txtbox.SelectAll()
    End Sub

    Private Sub TypeCombo_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles TypeCombo.SelectionChangeCommitted
        handleTypeComboChange()
    End Sub

#End Region

#Region "XXXX Interface Members"

#End Region

#Region "XXXX Event Handlers"

#End Region

#Region "Properties"

    Public Property FieldsBackColor() As Color
        Get
            Return ShortNameText.BackColor
        End Get
        Set
            ShortNameText.BackColor = Value
            SymbolText.BackColor = Value
            TypeCombo.BackColor = Value
            ExpiryText.BackColor = Value
            ExchangeCombo.BackColor = Value
            CurrencyCombo.BackColor = Value
            MultiplierText.BackColor = Value
            StrikePriceText.BackColor = Value
            RightCombo.BackColor = Value
        End Set
    End Property

    Public WriteOnly Property ContractFilter() As ContractFilter
        Set
            Clear()
            If Value IsNot Nothing Then
                CurrencyCombo.Text = Value.Currency
                If Value.Multiplier <> 0.0 Then MultiplierText.Text = CStr(Value.Multiplier)
                ExchangeCombo.Text = Value.Exchange
                ExpiryText.Text = Value.Expiry
                RightCombo.Text = Contract.OptionRightToString(Value.Right)
                TypeCombo.Text = Contract.SecTypeToString(Value.SecType)
                If Value.StrikePrice <> 0.0 Then StrikePriceText.Text = CStr(Value.StrikePrice)
                SymbolText.Text = Value.Symbol
            End If
        End Set
    End Property

    Public Property ContractSpecifier() As IContractSpecifier
        Get
            If Not mReady Then
                Return Nothing
            Else
                Dim lMultiplier As Double
                If MultiplierText.Text = "" Then
                    lMultiplier = 1.0#
                Else
                    lMultiplier = CDbl(MultiplierText.Text)
                End If

                Return New ContractSpecifier(ShortNameText.Text,
                                                SymbolText.Text,
                                                ExchangeCombo.Text,
                                                Contract.SecTypeFromString(TypeCombo.Text),
                                                CurrencyCombo.Text,
                                                ExpiryText.Text,
                                                lMultiplier,
                                                CDbl(IIf(StrikePriceText.Text = "", 0, StrikePriceText.Text)),
                                                Contract.OptionRightFromString(RightCombo.Text))
            End If
        End Get
        Set
            If Value Is Nothing Then
                Clear()
                Exit Property
            End If

            ShortNameText.Text = Value.LocalSymbol
            SymbolText.Text = Value.Symbol
            ExchangeCombo.Text = Value.Exchange
            TypeCombo.Text = Contract.SecTypeToString(Value.SecType)
            CurrencyCombo.Text = Value.CurrencyCode
            ExpiryText.Text = Value.Expiry
            If Value.Multiplier = 1 Then
                MultiplierText.Text = ""
            Else
                MultiplierText.Text = CStr(Value.Multiplier)
            End If
            If Value.Strike <> 0 Then
                StrikePriceText.Text = CStr(Value.Strike)
            Else
                StrikePriceText.Text = ""
            End If
            RightCombo.Text = Contract.OptionRightToString(Value.Right)
        End Set
    End Property

    Public Property FieldsForeColor() As Color
        Get
            Return ShortNameText.ForeColor
        End Get
        Set
            ShortNameText.ForeColor = Value
            SymbolText.ForeColor = Value
            TypeCombo.ForeColor = Value
            ExpiryText.ForeColor = Value
            ExchangeCombo.ForeColor = Value
            CurrencyCombo.ForeColor = Value
            MultiplierText.BackColor = Value
            StrikePriceText.ForeColor = Value
            RightCombo.ForeColor = Value
        End Set
    End Property

    Public ReadOnly Property isReady() As Boolean
        Get
            Return mReady
        End Get
    End Property

    Public Property ModeAdvanced() As Boolean
        Get
            Return mModeAdvanced
        End Get
        Set
            mModeAdvanced = Value
            If mModeAdvanced Then
                ShortNameLabel.Visible = True
                ShortNameText.Visible = True

                SymbolLabel.Visible = True
                SymbolLabel.Top = ShortNameLabel.Top + 27
                SymbolText.Top = ShortNameText.Top + 27
                SymbolText.Visible = True

                TypeLabel.Visible = True
                TypeCombo.Visible = True

                ExpiryLabel.Visible = True
                ExpiryText.Visible = True

                ExchangeLabel.Visible = True
                ExchangeCombo.Visible = True

                CurrencyLabel.Visible = True
                CurrencyCombo.Visible = True

                MultiplierLabel.Visible = True
                MultiplierText.Visible = True

                StrikePriceLabel.Visible = True
                StrikePriceText.Visible = True

                RightLabel.Visible = True
                RightCombo.Visible = True

                'AdvancedButton.Anchor = AnchorStyles.None
                AdvancedButton.Top = RightCombo.Top + 27
                AdvancedButton.Left = RightCombo.Right - AdvancedButton.Width
                'AdvancedButton.Anchor = CType(AnchorStyles.Top + AnchorStyles.Right, AnchorStyles)
                AdvancedButton.Text = "Advanced <<"

            Else
                ShortNameLabel.Visible = False
                ShortNameText.Visible = False

                'AdvancedButton.Anchor = AnchorStyles.None
                AdvancedButton.Top = SymbolText.Top
                AdvancedButton.Left = SymbolText.Right - AdvancedButton.Width
                'AdvancedButton.Anchor = CType(AnchorStyles.Top + AnchorStyles.Right, AnchorStyles)
                AdvancedButton.Text = "Advanced >>"

                SymbolLabel.Visible = True
                SymbolLabel.Top = ShortNameLabel.Top
                SymbolText.Top = ShortNameText.Top

                TypeLabel.Visible = False
                TypeCombo.Visible = False

                ExpiryLabel.Visible = False
                ExpiryText.Visible = False

                ExchangeLabel.Visible = False
                ExchangeCombo.Visible = False

                CurrencyLabel.Visible = False
                CurrencyCombo.Visible = False

                MultiplierLabel.Visible = False
                MultiplierText.Visible = False

                StrikePriceLabel.Visible = False
                StrikePriceText.Visible = False

                RightLabel.Visible = False
                RightCombo.Visible = False

            End If

            SymbolText.Focus()
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub Clear()
        ShortNameText.Text = ""
        SymbolText.Text = ""
        ExchangeCombo.Text = ""
        TypeCombo.Text = ""
        CurrencyCombo.Text = ""
        ExpiryText.Text = ""
        MultiplierText.Text = ""
        StrikePriceText.Text = ""
        RightCombo.Text = ""
        RaiseEvent NotReady(Me, EventArgs.Empty)
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub checkIfValid()
        mReady = False
        If ShortNameText.Text = "" And SymbolText.Text = "" Then
            RaiseEvent NotReady(Me, EventArgs.Empty)
            Exit Sub
        End If

        If ExpiryText.Text <> "" Then
            If Not Contract.IsValidExpiry(ExpiryText.Text) Then
                RaiseEvent NotReady(Me, EventArgs.Empty)
                Exit Sub
            End If
        End If

        If CStr(ExchangeCombo.SelectedItem) <> "" Then
            If Not IsValidExchangeCode(ExchangeCombo.Text) Then
                RaiseEvent NotReady(Me, EventArgs.Empty)
                Exit Sub
            End If
        End If

        If CStr(CurrencyCombo.SelectedItem) <> "" Then
            If Not IsValidCurrencyCode(CurrencyCombo.Text) Then
                RaiseEvent NotReady(Me, EventArgs.Empty)
                Exit Sub
            End If
        End If

        If MultiplierText.Text <> "" Then
            Dim lMultiplier As Double
            lMultiplier = CDbl(MultiplierText.Text)
            If lMultiplier <= 0# Then
                RaiseEvent NotReady(Me, EventArgs.Empty)
                Exit Sub
            End If
        End If

        If StrikePriceText.Text <> "" Then
            If Not IsNumeric(StrikePriceText.Text) Then
                RaiseEvent NotReady(Me, EventArgs.Empty)
                Exit Sub
            End If
            If CDbl(StrikePriceText.Text) <= 0 Then
                RaiseEvent NotReady(Me, EventArgs.Empty)
                Exit Sub
            End If
        End If

        mReady = True
        RaiseEvent Ready(Me, EventArgs.Empty)
    End Sub

    Private Sub handleCurrencyComboChange()
        checkIfValid()
        ToolTip1.SetToolTip(CurrencyCombo, "")
        If CurrencyCombo.Text <> "" Then
            If IsValidCurrencyCode(CurrencyCombo.Text) Then
                Dim currDesc = GetCurrencyDescriptor(CurrencyCombo.Text)
                ToolTip1.SetToolTip(CurrencyCombo, currDesc.description)
            End If
        End If
    End Sub

    Private Sub handleTypeComboChange()
        Select Case Contract.SecTypeFromString(CStr(TypeCombo.SelectedItem))
            Case SecurityType.None
                ExpiryText.Enabled = True
                StrikePriceText.Enabled = True
                RightCombo.Enabled = True
            Case SecurityType.Future
                ExpiryText.Enabled = True
                StrikePriceText.Enabled = False
                RightCombo.Enabled = False
            Case SecurityType.Stock
                ExpiryText.Enabled = False
                StrikePriceText.Enabled = False
                RightCombo.Enabled = False
            Case SecurityType.Option
                ExpiryText.Enabled = True
                StrikePriceText.Enabled = True
                RightCombo.Enabled = True
            Case SecurityType.FuturesOption
                ExpiryText.Enabled = True
                StrikePriceText.Enabled = True
                RightCombo.Enabled = True
            Case SecurityType.Cash
                ExpiryText.Enabled = False
                StrikePriceText.Enabled = False
                RightCombo.Enabled = False
            Case SecurityType.Index
                ExpiryText.Enabled = False
                StrikePriceText.Enabled = False
                RightCombo.Enabled = False
                'Case SecurityType.Bag
                '    ExpiryText.Enabled = False
                '    StrikePriceText.Enabled = False
                '    RightCombo.Enabled = False
        End Select

        checkIfValid()
    End Sub

#End Region

End Class
