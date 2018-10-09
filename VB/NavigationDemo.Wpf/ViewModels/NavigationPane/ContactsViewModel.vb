Imports DevExpress.Mvvm

Namespace NavigationDemo
    Public Class ContactsViewModel
        Implements ISupportParameter

        Public Overridable Property Parameter() As Object

        Public Overridable Property FirstName() As String
        Public Overridable Property LastName() As String
        Public Overridable Property Email() As String

        Public Overridable Sub AddContact()
            Dim viewModel = DirectCast(Parameter, ContactsNavigationViewModel)
            viewModel.Contacts.Add(New ContactItem() With {.FirstName = FirstName, .LastName = LastName, .Email = Email})
            FirstName = String.Empty
            LastName = String.Empty
            Email = String.Empty
        End Sub
        Public Overridable Function CanAddContact() As Boolean
            Return Not(String.IsNullOrEmpty(FirstName) OrElse String.IsNullOrEmpty(FirstName))
        End Function

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
