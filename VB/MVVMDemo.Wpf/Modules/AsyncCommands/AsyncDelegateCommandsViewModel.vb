Imports DevExpress.Mvvm
Imports System
Imports System.Threading
Imports System.Threading.Tasks

Namespace MVVMDemo.AsyncCommands
    Public Class AsyncDelegateCommandsViewModel
        Inherits ViewModelBase

        Private privateCalculateCommand As AsyncCommand
        Public Property CalculateCommand() As AsyncCommand
            Get
                Return privateCalculateCommand
            End Get
            Private Set(ByVal value As AsyncCommand)
                privateCalculateCommand = value
            End Set
        End Property

        Private Function Calculate() As Task
            Return Task.Factory.StartNew(AddressOf CaclulateCore)
        End Function
        Private Sub CaclulateCore()
            For i As Integer = 0 To 100
                If CalculateCommand.IsCancellationRequested Then
                    Progress = 0
                    Return
                End If
                Progress = i
                Thread.Sleep(TimeSpan.FromMilliseconds(20))
            Next i
        End Sub

        Private _Progress As Integer
        Public Property Progress() As Integer
            Get
                Return _Progress
            End Get
            Set(ByVal value As Integer)
                SetProperty(_Progress, value, Function() Progress)
            End Set
        End Property

        Public Sub New()
            CalculateCommand = New AsyncCommand(AddressOf Calculate)
        End Sub

    End Class
End Namespace
