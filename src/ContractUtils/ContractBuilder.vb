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

Namespace Contracts
    Public NotInheritable Class ContractBuilder

        '@================================================================================
        ' Interfaces
        '@================================================================================

        '@================================================================================
        ' Events
        '@================================================================================

        '@================================================================================
        ' Enums
        '@================================================================================

        '@================================================================================
        ' Types
        '@================================================================================

        '@================================================================================
        ' Constants
        '@================================================================================

        Private Const ModuleName As String = "ContractBuilder"

        '@================================================================================
        ' Member variables
        '@================================================================================

        Private mContract As Contract

        '@================================================================================
        ' Constructors
        '@================================================================================

        Private Sub New()
        End Sub

        Public Sub New(specifier As IContractSpecifier)
            specifier = specifier
            TimezoneName = "Eastern Standard Time"
            DaysBeforeExpiryToSwitch = 1
        End Sub

        Public Sub New(contract As IContract)
            Dim builder = New ContractBuilder(contract.Specifier)
            builder.BuildFrom(contract)
        End Sub

        '@================================================================================
        ' XXXX Interface Members
        '@================================================================================

        '@================================================================================
        ' XXXX Event Handlers
        '@================================================================================

        '@================================================================================
        ' Properties
        '@================================================================================

        Public ReadOnly Property Contract() As IContract
            Get
                Contract = mContract
            End Get
        End Property

        Public WriteOnly Property DaysBeforeExpiryToSwitch() As Integer
            Set(Value As Integer)
                mContract.DaysBeforeExpiryToSwitch = Value
            End Set
        End Property

        Public WriteOnly Property Description() As String
            Set(Value As String)
                mContract.Description = Value
            End Set
        End Property

        Public WriteOnly Property ExpiryDate() As Date
            Set(Value As Date)
                mContract.ExpiryDate = Value
            End Set
        End Property

        Public WriteOnly Property ProviderIDs() As Parameters
            Set(Value As Parameters)
                mContract.ProviderIDs = Value
            End Set
        End Property

        Public WriteOnly Property SessionStartTime() As TimeSpan
            Set
                mContract.SessionStartTime = Value
            End Set
        End Property

        Public WriteOnly Property SessionEndTime() As TimeSpan
            Set
                mContract.SessionEndTime = Value
            End Set
        End Property

        Public WriteOnly Property TickSize() As Double
            Set(Value As Double)
                mContract.TickSize = Value
            End Set
        End Property

        Public WriteOnly Property TimezoneName() As String
            Set(Value As String)
                mContract.TimezoneName = Value
            End Set
        End Property

        '@================================================================================
        ' Methods
        '@================================================================================

        Friend Sub BuildFrom(pContract As IContract)
            With mContract
                .DaysBeforeExpiryToSwitch = pContract.DaysBeforeExpiryToSwitch
                .Description = pContract.Description
                .ExpiryDate = pContract.ExpiryDate
                '.ProviderIDs = pContract.ProviderIDs
                .SessionEndTime = pContract.SessionEndTime
                .SessionStartTime = pContract.SessionStartTime
                .Specifier = pContract.Specifier
                .TickSize = pContract.TickSize
                .TimezoneName = pContract.TimezoneName
            End With
        End Sub

        Public Sub LoadFromConfig(pConfig As ConfigurationSection)
            mContract = New Contract(pConfig)
        End Sub

        '@================================================================================
        ' Helper Functions
        '@================================================================================
    End Class
End Namespace
