Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Threading
Imports System.Threading.Tasks

Namespace MVVMDemo.AsyncCommands
    Public Class AsyncPOCOCommandsViewModel
        Public Overridable Property Progress() As Integer

        Public Function Calculate() As Task
            Return Task.Factory.StartNew(AddressOf CaclulateCore)
        End Function
        Private Sub CaclulateCore()
            For i As Integer = 0 To 100
                If Me.GetAsyncCommand(Function(x) x.Calculate()).IsCancellationRequested Then
                    Progress = 0
                    Return
                End If
                Progress = i
                Thread.Sleep(TimeSpan.FromMilliseconds(20))
            Next i
        End Sub
    End Class
End Namespace
