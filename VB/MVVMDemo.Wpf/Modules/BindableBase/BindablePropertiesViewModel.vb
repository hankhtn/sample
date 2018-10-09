Imports DevExpress.Mvvm

Namespace MVVMDemo.BindableBaseDemo
    Public Class BindablePropertiesViewModel
        Inherits BindableBase

        Public Property FirstName() As String
            Get
                Return GetProperty(Function() FirstName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() FirstName, value, changedCallback:= AddressOf NotifyFullNameChanged)
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return GetProperty(Function() LastName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() LastName, value, changedCallback:= AddressOf NotifyFullNameChanged)
            End Set
        End Property

        Public ReadOnly Property FullName() As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property

        Private Sub NotifyFullNameChanged()
            RaisePropertyChanged(Function() FullName)
        End Sub

    End Class
End Namespace
