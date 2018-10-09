Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm

Namespace NavigationDemo
    Public Class MailViewModel
        Implements ISupportParameter

        Public Overridable Property ItemsSource() As ObservableCollection(Of MailItem)
        Public Overridable Property Parameter() As Object

        Public Overridable Property SentGroupIndex() As Integer
        Public Overridable Property ReceiveGroupIndex() As Integer

        Public Sub New()
            SentGroupIndex = -1
            ReceiveGroupIndex = -1
        End Sub
        Protected Overridable Sub OnParameterChanged()
            Dim id = DirectCast(Parameter, NavigationId)
            Select Case id
                Case NavigationId.Inbox
                    SentGroupIndex = -1
                    ReceiveGroupIndex = 0
                Case NavigationId.Outbox
                    SentGroupIndex = 0
                    ReceiveGroupIndex = -1
                Case NavigationId.SentItems
                    SentGroupIndex = 0
                    ReceiveGroupIndex = -1
                Case NavigationId.DeletedItems
                    SentGroupIndex = -1
                    ReceiveGroupIndex = 0
                Case NavigationId.Drafts
                    SentGroupIndex = 0
                    ReceiveGroupIndex = -1
            End Select
            ItemsSource = NavigationPaneData.MailData(id)
        End Sub
        #Region "ISupportParameter"
        Private Property ISupportParameter_Parameter() As Object Implements ISupportParameter.Parameter
            Get
                Return Parameter
            End Get
            Set(ByVal value As Object)
                Parameter = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
