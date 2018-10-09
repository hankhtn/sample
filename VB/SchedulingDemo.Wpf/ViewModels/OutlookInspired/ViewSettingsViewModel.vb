Imports System.Windows.Controls
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel

Namespace SchedulingDemo.ViewModels
    Public Class ViewSettingsViewModel
        Public Shared Function Create() As ViewSettingsViewModel
            Return ViewModelSource.Create(Function() New ViewSettingsViewModel())
        End Function

        Protected Sub New()
            ResetView()
        End Sub

        Public Overridable Property Orientation() As Orientation
        Public Overridable Property IsDataPaneVisible() As Boolean
        Public Sub ResetView()
            Orientation = Orientation.Horizontal
            IsDataPaneVisible = False
        End Sub

        Public Sub DataPaneRight()
            Orientation = Orientation.Horizontal
            IsDataPaneVisible = True
        End Sub
        Public Function CanDataPaneRight() As Boolean
            Return Orientation <> Orientation.Horizontal OrElse Not IsDataPaneVisible
        End Function
        Public Sub DataPaneBottom()
            Orientation = Orientation.Vertical
            IsDataPaneVisible = True
        End Sub
        Public Function CanDataPaneBottom() As Boolean
            Return Orientation <> Orientation.Vertical OrElse Not IsDataPaneVisible
        End Function
        Public Sub DataPaneOff()
            IsDataPaneVisible = False
        End Sub
        Public Function CanDataPaneOff() As Boolean
            Return IsDataPaneVisible
        End Function
    End Class
End Namespace
