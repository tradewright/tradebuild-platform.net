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
    Public NotInheritable Class ContractFetcher

        Private mPrimaryContractStore As IContractStore
        Private mSecondaryContractStore As IContractStore

        Public Sub New(primaryContractStore As IContractStore, Optional secondaryContractStore As IContractStore = Nothing)
            If primaryContractStore Is Nothing Then Throw New ArgumentException("pPrimaryContractStore cannot be Nothing")
            mPrimaryContractStore = primaryContractStore
            mSecondaryContractStore = secondaryContractStore
        End Sub

        Public Function FetchContract(pContractSpec As IContractSpecifier, Optional pPriority As TaskPriorities = TaskPriorities.PriorityNormal, Optional pTaskName As String = "", Optional pCookie As Object = Nothing) As IFuture
            If IsNothing(pCookie) Then pCookie = Guid.NewGuid.ToString

            Dim t As New ContractFetchTask
            t.Initialise(pContractSpec, mPrimaryContractStore, mSecondaryContractStore, pCookie, Nothing, True)
            TW.StartTask(t, pPriority, pTaskName)

            Return t.ContractFuture
        End Function

        Public Function FetchContracts(pContractSpec As IContractSpecifier, Optional pListener As IContractFetchListener = Nothing, Optional pPriority As TaskPriorities = TaskPriorities.PriorityNormal, Optional pTaskName As String = "", Optional pCookie As Object = Nothing) As IFuture
            If IsNothing(pCookie) Then pCookie = Guid.NewGuid.ToString

            Dim t As New ContractFetchTask
            t.Initialise(pContractSpec, mPrimaryContractStore, mSecondaryContractStore, pCookie, pListener, False)
            TW.StartTask(t, pPriority, pTaskName)

            Return t.ContractsFuture
        End Function

        Private Class ContractFetchTask

            Implements IContractFetchListener
            Implements ITask

#Region "Interfaces"


#End Region

#Region "Events"
            
#End Region

#Region "Enums"
            
#End Region

#Region "Types"
            
#End Region

#Region "Constants"

            Private Const ModuleName As String = "ContractFetchTask"

#End Region

#Region "Member variables"

            Private mPrimaryContractStore As IContractStore
            Private mSecondaryContractStore As IContractStore

            Private mContractSpec As IContractSpecifier

            Private mTaskContext As TaskContext

            Private mUsedSecondaryContractStore As Boolean

            Private WithEvents mContractsFutureBuilder As FutureBuilder
            Private WithEvents mContractFutureBuilder As FutureBuilder

            Private mSingleContractOnly As Boolean

            Private mListener As IContractFetchListener

            Private mCookie As Object

            Private mFetchFuture As _IFuture

            Private mContractsBuilder As New ContractsBuilder

#End Region

#Region "Constructors"
            
#End Region

#Region "IContractFetchListener Interface Members"

            Private Sub IContractFetchListener_FetchCancelled(pCookie As Object) Implements IContractFetchListener.FetchCancelled
                If Not futureIsPending() Then Exit Sub

                If mSingleContractOnly Then
                    mContractFutureBuilder.Cancel()
                Else
                    mContractsFutureBuilder.Cancel()
                End If

                If Not mListener Is Nothing Then mListener.FetchCancelled(pCookie)
                mTaskContext.Finish(Nothing, False)
            End Sub

            Private Sub IContractFetchListener_FetchCompleted(pCookie As Object) Implements IContractFetchListener.FetchCompleted
                If Not futureIsPending() Then Exit Sub

                Dim lContracts As IContracts
                lContracts = CType(mFetchFuture.Value, IContracts)
                If lContracts.Count = 0 And Not mUsedSecondaryContractStore Then If trySecondaryContractSP() Then Exit Sub

                If mSingleContractOnly Then
                    If lContracts.Count = 0 Then
                        mContractFutureBuilder.Fail(ErrorCodes.ErrIllegalArgumentException, "No such contract", "")
                    Else
                        mContractFutureBuilder.Value = lContracts.ItemAtIndex(0)
                        mContractFutureBuilder.Complete()
                    End If
                Else
                    mContractsFutureBuilder.Value = mContractsBuilder.Contracts
                    mContractsFutureBuilder.Complete()
                End If

                If Not mListener Is Nothing Then mListener.FetchCompleted(pCookie)
                mTaskContext.Finish(Nothing, False)
            End Sub

            Private Sub IContractFetchListener_FetchFailed(pCookie As Object, pErrorCode As Integer, pErrorMessage As String, pErrorSource As String) Implements IContractFetchListener.FetchFailed
                If Not futureIsPending() Then Exit Sub

                If mSingleContractOnly Then
                    mContractFutureBuilder.Fail(pErrorCode, pErrorMessage, pErrorSource)
                Else
                    mContractsFutureBuilder.Fail(pErrorCode, pErrorMessage, pErrorSource)
                End If

                If Not mListener Is Nothing Then mListener.FetchFailed(pCookie, pErrorCode, pErrorMessage, pErrorSource)
                mTaskContext.Finish(Nothing, False)
            End Sub

            Private Sub IContractFetchListener_NotifyContract(pCookie As Object, pContract As IContract) Implements IContractFetchListener.NotifyContract
                If Not futureIsPending() Then Exit Sub

                If mSingleContractOnly Then
                    If mContractsBuilder.Contracts.Count = 1 Then
                        mContractFutureBuilder.Fail(ErrorCodes.ErrIllegalArgumentException, "Contract is not uniquely specified", "")
                        mFetchFuture.Cancel()
                        mTaskContext.Finish(Nothing, False)
                        Exit Sub
                    End If
                End If

                mContractsBuilder.Add(pContract)

                If Not mListener Is Nothing Then mListener.NotifyContract(pCookie, pContract)
            End Sub

#End Region

#Region "ITask Interface Members"

            Private Sub Task_Cancel() Implements ITask.Cancel
            End Sub

            Private Sub Task_Run() Implements ITask.Run
                Dim lContractStore As IContractStore
                lContractStore = mPrimaryContractStore
                If lContractStore Is Nothing Then
                    lContractStore = mSecondaryContractStore
                    mUsedSecondaryContractStore = True
                End If

                mFetchFuture = lContractStore.FetchContracts(mContractSpec, Me, mCookie)
                mTaskContext.Suspend(-1)
            End Sub

            Private WriteOnly Property Task_TaskContext() As TaskContext Implements ITask.TaskContext
                Set(Value As TaskContext)
                    mTaskContext = Value
                End Set
            End Property

            Private ReadOnly Property Task_TaskName() As String Implements ITask.TaskName
                Get
                    Task_TaskName = mTaskContext.Name
                End Get
            End Property

#End Region

#Region "Properties"

            Friend ReadOnly Property ContractFuture() As IFuture
                Get
                    ContractFuture = CType(mContractFutureBuilder.Future, IFuture)
                End Get
            End Property

            Friend ReadOnly Property ContractsFuture() As IFuture
                Get
                    ContractsFuture = CType(mContractsFutureBuilder.Future, IFuture)
                End Get
            End Property

#End Region

#Region "mContractFutureBuilder Handlers"

            Private Sub mContractFutureBuilder_Cancelled(ByRef ev As CancelledEventData) Handles mContractFutureBuilder.Cancelled
                If Not mFetchFuture Is Nothing Then mFetchFuture.Cancel()
                If Not mListener Is Nothing Then mListener.FetchCancelled(mContractFutureBuilder.Cookie)
                mTaskContext.Finish(Nothing, True)
            End Sub

#End Region

#Region "mContractsFutureBuilder Handlers"

            Private Sub mContractsFutureBuilder_Cancelled(ByRef ev As CancelledEventData) Handles mContractsFutureBuilder.Cancelled
                If Not mFetchFuture Is Nothing Then mFetchFuture.Cancel()
                If Not mListener Is Nothing Then mListener.FetchCancelled(mContractsFutureBuilder.Cookie)
                mTaskContext.Finish(Nothing, True)
            End Sub

#End Region

#Region "Methods"

            Friend Sub Initialise(pContractSpec As IContractSpecifier, pPrimaryContractStore As IContractStore, pSecondaryContractStore As IContractStore, pCookie As Object, pListener As IContractFetchListener, pSingleContractOnly As Boolean)
                mContractSpec = pContractSpec
                mPrimaryContractStore = pPrimaryContractStore
                mSecondaryContractStore = pSecondaryContractStore

                mCookie = pCookie

                mListener = pListener

                mSingleContractOnly = pSingleContractOnly
                If mSingleContractOnly Then
                    mContractFutureBuilder = New FutureBuilder
                    mContractFutureBuilder.Cookie = mCookie
                Else
                    mContractsFutureBuilder = New FutureBuilder
                    mContractsFutureBuilder.Cookie = mCookie
                End If
            End Sub

#End Region

#Region "Helper Functions"

            Private Function futureIsPending() As Boolean
                If mSingleContractOnly Then
                    futureIsPending = mContractFutureBuilder.Future.IsPending
                Else
                    futureIsPending = mContractsFutureBuilder.Future.IsPending
                End If
            End Function

            Private Function trySecondaryContractSP() As Boolean
                mUsedSecondaryContractStore = True
                If mSecondaryContractStore Is Nothing Then
                    trySecondaryContractSP = False
                Else
                    mFetchFuture = mSecondaryContractStore.FetchContracts(mContractSpec, Me, mCookie)
                    trySecondaryContractSP = True
                End If
            End Function
#End Region

End Class

    End Class

End Namespace
