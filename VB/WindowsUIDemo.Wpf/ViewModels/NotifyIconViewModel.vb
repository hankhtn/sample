Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.ObjectModel

Namespace WindowsUIDemo
    <POCOViewModel>
    Public Class NotifyIconViewModel
        Public Property EventsLog() As ObservableCollection(Of String)

        Protected Overridable ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property
        Protected Overridable ReadOnly Property NotifyIconService() As INotifyIconService
            Get
                Return Nothing
            End Get
        End Property

        Private Sub AddStringInLog(ByVal logString As String)
            EventsLog.Add(Date.Now.ToShortTimeString() & logString)
            If EventsLog.Count > 20 Then
                EventsLog.RemoveAt(0)
            End If
        End Sub

        Public Sub New()
            EventsLog = New ObservableCollection(Of String)()

            AddStringInLog(" - NotifyIcon is created")
        End Sub
        Public Sub ShowMessageBox()
            MessageBoxService.ShowMessage("hello")
        End Sub
        Public Sub SetStatusIcon(ByVal icon As Object)
            NotifyIconService.SetStatusIcon(icon)

            AddStringInLog(" - NotifyIcon state changed on: " & icon.ToString())
        End Sub
        Public Sub ResetStatusIcon()
            NotifyIconService.ResetStatusIcon()

            AddStringInLog(" - NotifyIcon state reseted on default state")
        End Sub
        Public Sub DoIconLeftAction()
            MessageBoxService.ShowMessage("NotifyIcon left mouse button click!")

            AddStringInLog(" - NotifyIcon left mouse button click")
        End Sub

        Public Sub PopupButtonClick(ByVal buttonName As Object)

            AddStringInLog(" - NotifyIcon context menu button '" & buttonName.ToString() & "' click")
        End Sub
    End Class
End Namespace
