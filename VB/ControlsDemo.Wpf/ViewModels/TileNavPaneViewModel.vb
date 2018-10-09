Imports System
Imports System.Linq
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native

Namespace ControlsDemo
    Public Class TileNavPaneViewModel
        Inherits TileNavBaseViewModel

        Public Sub New()
            Messenger.Default.Register(Of NotificationMessage)(Me, AddressOf OnNotificationMessage)
            ShowNotificationCommand = DelegateCommandFactory.Create(Of String)(AddressOf OnShowNotificationCommand)
        End Sub

        Private privateShowNotificationCommand As ICommand
        Public Property ShowNotificationCommand() As ICommand
            Get
                Return privateShowNotificationCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateShowNotificationCommand = value
            End Set
        End Property
        Private ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return ServiceContainer.GetService(Of IMessageBoxService)()
            End Get
        End Property

        Protected Overridable Sub OnNotificationMessage(ByVal message As NotificationMessage)
            OnShowNotificationCommand(message.Source)
        End Sub
        Protected Overrides Sub OnViewUnloaded()
            MyBase.OnViewUnloaded()
            Messenger.Default.Unregister(Of NotificationMessage)(Me, AddressOf OnNotificationMessage)
        End Sub
        Private Sub OnShowNotificationCommand(ByVal message As String)
            MessageBoxService.ShowMessage(message)
        End Sub
    End Class
End Namespace
