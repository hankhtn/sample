Imports DevExpress.Mvvm
Imports System.Windows
Imports System.Windows.Input

Namespace MVVMDemo.Commands
    Public Class DelegateCommandsViewModel
        Inherits ViewModelBase

        Private privateSaveCommand As ICommand
        Public Property SaveCommand() As ICommand
            Get
                Return privateSaveCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateSaveCommand = value
            End Set
        End Property
        Private privateOpenCommand As ICommand
        Public Property OpenCommand() As ICommand
            Get
                Return privateOpenCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateOpenCommand = value
            End Set
        End Property

        #Region "!"
        Public Sub New()
            CanExecuteSaveCommand = True

            SaveCommand = New DelegateCommand(Function() MessageBox.Show("Action: Save"), Function() CanExecuteSaveCommand)

            OpenCommand = New DelegateCommand(Of String)(Function(fileName) MessageBox.Show(String.Format("Action: Open {0}", FileName)), Function(fileName) (Not String.IsNullOrEmpty(fileName)))
        End Sub
        #End Region

        Public Property CanExecuteSaveCommand() As Boolean
            Get
                Return GetProperty(Function() CanExecuteSaveCommand)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Function() CanExecuteSaveCommand, value)
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
    End Class
End Namespace
