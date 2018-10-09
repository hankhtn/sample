Imports DevExpress.Mvvm.Native
Imports System
Imports System.Collections.Generic
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm

Namespace DockingDemo.ViewModels
    Public Class DockLayoutManagerEventsViewModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Private ReadOnly Property EventsService() As IDockLayoutManagerEventsService
            Get
                Return ServiceContainer.GetService(Of IDockLayoutManagerEventsService)()
            End Get
        End Property
        Public Sub New()
        End Sub

        Private clearLogCommand_Renamed As ICommand
        Public ReadOnly Property ClearLogCommand() As ICommand
            Get
                If clearLogCommand_Renamed Is Nothing Then
                    clearLogCommand_Renamed = DelegateCommandFactory.Create(AddressOf ClearLog)
                End If
                Return clearLogCommand_Renamed
            End Get
        End Property
        Private Sub ClearLog()
            EventsService.ClearEventsLog()
        End Sub
    End Class
End Namespace
