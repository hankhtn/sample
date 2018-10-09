Imports DevExpress.Mvvm

Namespace MVVMDemo.ViewModelsInteraction
    Public Class MessengerViewModel
        Private messageIndex As Integer
        Public Sub SendMessage()
            Messenger.Default.Send(New Message("Message " & messageIndex))
            messageIndex += 1
        End Sub
    End Class
    Public Class ReceiverViewModel
        Public Overridable Property Status() As String
        Protected Sub New()
            Messenger.Default.Register(Of Message)(Me, AddressOf OnMessage)
        End Sub
        Private Sub OnMessage(ByVal message As Message)
            Status = "Received: " & message.Content
        End Sub
    End Class
    Public Class Message
        Public Sub New(ByVal content As String)
            Me.Content = content
        End Sub
        Private privateContent As String
        Public Property Content() As String
            Get
                Return privateContent
            End Get
            Private Set(ByVal value As String)
                privateContent = value
            End Set
        End Property
    End Class
End Namespace
