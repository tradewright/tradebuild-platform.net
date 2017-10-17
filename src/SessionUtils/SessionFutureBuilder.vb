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

Namespace Sessions
    Friend Class SessionFutureBuilder

#Region "Interfaces"
        
#End Region

#Region "Events"
        
#End Region

#Region "Enums"
        
#End Region

#Region "Types"
        
#End Region

#Region "Constants"

        Private Const ModuleName As String = "SessionFutureBuilder"

#End Region

#Region "Member variables"

        Private WithEvents mFutureBuilder As FutureBuilder
        Private WithEvents mFutureWaiter As FutureWaiter

        Private mSelfRef As Object

#End Region

#Region "Constructors"

        'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Private Sub Class_Initialize_Renamed()
            mFutureBuilder = New FutureBuilder
        End Sub
        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub

#End Region

#Region "mFutureBuilder Event Handlers"

        Private Sub mFutureBuilder_Cancelled(ByRef ev As CancelledEventData) Handles mFutureBuilder.Cancelled
            mFutureWaiter.Clear()
            mFutureBuilder.Cancel()

            'UPGRADE_NOTE: Object mSelfRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mSelfRef = Nothing
        End Sub

#End Region

#Region "mFutureWaiter Event Handlers"

        Private Sub mFutureWaiter_WaitCompleted(ByRef ev As FutureWaitCompletedEventData) Handles mFutureWaiter.WaitCompleted
            If ev.Future.IsAvailable Then
                setupSession(DirectCast(ev.Future.Value, SessionBuilder))
                'UPGRADE_NOTE: Object mSelfRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                mSelfRef = Nothing
            End If
        End Sub

#End Region

#Region "Properties"

        Friend ReadOnly Property Future() As _IFuture
            Get
                Future = mFutureBuilder.Future
            End Get
        End Property

#End Region

#Region "Methods"

        Friend Sub Initialise(pSessionBuilderFuture As IFuture)
            If pSessionBuilderFuture.IsAvailable Then
                setupSession(DirectCast(pSessionBuilderFuture.Value, SessionBuilder))
            Else
                mSelfRef = Me
                mFutureWaiter = New FutureWaiter
                mFutureWaiter.Add(pSessionBuilderFuture)
            End If
        End Sub

#End Region

#Region "Helper Functions"

        Private Sub setupSession(pSessionBuilder As SessionBuilder)
            mFutureBuilder.Value = pSessionBuilder.Session
            mFutureBuilder.Complete()
        End Sub
#End Region

End Class
End Namespace
