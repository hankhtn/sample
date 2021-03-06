Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.DXCommandDemo
    Public Class CommandParameterViewModel
        Inherits BindableBase

        Public Property FileName() As String
            Get
                Return GetProperty(Function() FileName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() FileName, value)
            End Set
        End Property

        Public Sub Open(ByVal fileName As String)
            MessageBox.Show(String.Format("Action: Open {0}", fileName))
        End Sub
        Public Function CanOpen(ByVal fileName As String) As Boolean
            Return Not String.IsNullOrEmpty(fileName)
        End Function
    End Class
End Namespace
