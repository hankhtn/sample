Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System.Windows

Namespace MVVMDemo.Commands
    Public Class AutoCommandsChangeNotificationsViewModel
        Inherits ViewModelBase

        <Command(UseCommandManager := False)>
        Public Sub Save()
            MessageBox.Show("Action: Save")
        End Sub
        Public Function CanSave() As Boolean
            Return CanExecuteSaveCommand
        End Function

        Public Property CanExecuteSaveCommand() As Boolean
            Get
                Return GetProperty(Function() CanExecuteSaveCommand)
            End Get
            Set(ByVal value As Boolean)
'                #Region "!"
                If SetProperty(Function() CanExecuteSaveCommand, value) Then
                    RaiseCanExecuteChanged(Sub() Save())
                End If
'                #End Region '            }
            End Set
        End Property

        Public Sub New()
            CanExecuteSaveCommand = True
        End Sub
    End Class
End Namespace
