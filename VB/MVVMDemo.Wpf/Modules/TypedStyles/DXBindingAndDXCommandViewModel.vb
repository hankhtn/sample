Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.TypedStylesDemo
    Public Class DXBindingAndDXCommandViewModel
        Inherits BindableBase

        Public Property FirstName() As String
            Get
                Return GetProperty(Function() FirstName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() FirstName, value)
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return GetProperty(Function() LastName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() LastName, value)
            End Set
        End Property
        Public Sub Register()
            MessageBox.Show("Registered")
        End Sub
        Public Sub New()
            FirstName = "Gregory"
            LastName = "Price"
        End Sub
    End Class
End Namespace
