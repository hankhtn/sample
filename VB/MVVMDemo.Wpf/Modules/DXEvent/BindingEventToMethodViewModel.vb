Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.DXEventDemo
    Public Class BindingEventToMethodViewModel
        Inherits BindableBase

        Public Property State() As String
            Get
                Return GetProperty(Function() State)
            End Get
            Private Set(ByVal value As String)
                SetProperty(Function() State, value)
            End Set
        End Property
        Public Sub Initialize()
            State = "Initialized"
        End Sub
    End Class
End Namespace
