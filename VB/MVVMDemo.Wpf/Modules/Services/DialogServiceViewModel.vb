Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Windows

Namespace MVVMDemo.Services
    Public Class DialogServiceViewModel
        Public Sub ShowDetail(ByVal serviceName As String)
            Dim detailViewModel As DialogServiceDetailViewModel = DialogServiceDetailViewModel.Create()

            Dim registerCommand As New UICommand() With {.Caption = "Register", .IsDefault = True, .Command = New DelegateCommand(Sub()
                End Sub, Function() (Not String.IsNullOrEmpty(detailViewModel.CustomerName)))}
            Dim cancelCommand As New UICommand() With {.Caption = "Cancel", .IsCancel = True}

            Dim service As IDialogService = Me.GetService(Of IDialogService)(serviceName)
            Dim result As UICommand = service.ShowDialog(dialogCommands:= { registerCommand, cancelCommand }, title:= "Registration Dialog", viewModel:= detailViewModel)

            If result Is registerCommand Then
                MessageBox.Show("Registered: " & detailViewModel.CustomerName)
            End If
        End Sub
    End Class
End Namespace
