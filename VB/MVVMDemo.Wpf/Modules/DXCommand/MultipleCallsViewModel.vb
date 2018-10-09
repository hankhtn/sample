Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.DXCommandDemo
    Public Class MultipleCallsViewModel
        Inherits BindableBase

        Public Property IsReadonly() As Boolean
            Get
                Return GetProperty(Function() IsReadonly)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() IsReadonly, value)
            End Set
        End Property

        Public Property FileName() As String
            Get
                Return GetProperty(Function() FileName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() FileName, value)
            End Set
        End Property

        Public Sub Open()
            MessageBox.Show(String.Format("Action: Open {0}", FileName))
        End Sub
        Public Function CanOpen() As Boolean
            Return Not String.IsNullOrEmpty(FileName)
        End Function

        Public Sub Clear()
            FileName = Nothing
        End Sub
    End Class
End Namespace
