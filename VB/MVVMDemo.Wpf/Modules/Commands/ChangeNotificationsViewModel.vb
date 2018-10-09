Imports DevExpress.Mvvm
Imports System.Windows
Imports System.Windows.Input

Namespace MVVMDemo.Commands
    Public Class ChangeNotificationsViewModel
        Inherits ViewModelBase

        Private privateSaveCommand As DelegateCommand
        Public Property SaveCommand() As DelegateCommand
            Get
                Return privateSaveCommand
            End Get
            Private Set(ByVal value As DelegateCommand)
                privateSaveCommand = value
            End Set
        End Property

        Public Property CanExecuteSaveCommand() As Boolean
            Get
                Return GetProperty(Function() CanExecuteSaveCommand)
            End Get
            Set(ByVal value As Boolean)
'                #Region "!"
                If SetProperty(Function() CanExecuteSaveCommand, value) Then
                    SaveCommand.RaiseCanExecuteChanged()
                End If
'                #End Region
            End Set
        End Property

        Public Sub New()
            SaveCommand = New DelegateCommand(Function() MessageBox.Show("Action: Save"), Function() CanExecuteSaveCommand, useCommandManager:= False)

            CanExecuteSaveCommand = True
        End Sub
    End Class
End Namespace
