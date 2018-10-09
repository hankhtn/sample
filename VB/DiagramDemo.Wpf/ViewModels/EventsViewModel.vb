Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Text
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Mvvm.POCO
Imports System.Diagnostics

Namespace DiagramDemo
    <POCOViewModel>
    Public Class EventsViewModel
        Protected Sub New()
        End Sub

        Private ReadOnly log_Renamed As New ObservableCollection(Of LogEntry)()
        Private Const LogEntriesMaxCount As Integer = 100
        Public ReadOnly Property Log() As IEnumerable(Of LogEntry)
            Get
                Return log_Renamed
            End Get
        End Property

        Private Sub AddToLog(ByVal eventName As String, ByVal argsFormat As String, ParamArray ByVal args() As Object)
            log_Renamed.Add(LogEntry.Create(eventName, String.Format(argsFormat, args)))
            If log_Renamed.Count > LogEntriesMaxCount Then
                log_Renamed.RemoveAt(0)
            End If
            Dim scrollService = Me.GetService(Of IScrollToEndService)()
            If scrollService IsNot Nothing Then
                scrollService.ScrollToEnd()
            End If
        End Sub
        Public Sub ClearLog()
            log_Renamed.Clear()
        End Sub
        Public Overridable Property EventsInfo() As IEnumerable(Of DiagramEventNode)
        Public Sub InitializeEventsInfo(ByVal eventsOwner As DiagramControl)
            EventsInfo = (New DiagramEventsInfo(eventsOwner, AddressOf AddToLog)).Initialize()
        End Sub
    End Class
    <POCOViewModel>
    Public Class LogEntry
        Public Shared Function Create(ByVal eventName As String, ByVal eventArgs As String) As LogEntry
            Return ViewModelSource.Create(Function() New LogEntry(eventName, eventArgs))
        End Function
        Protected Sub New(ByVal eventName As String, ByVal eventArgs As String)
            Me.EventName = eventName
            Me.EventArgs = eventArgs
        End Sub
        Public Overridable Property EventName() As String
        Public Overridable Property EventArgs() As String
        Public Sub ShowHelp()
            Process.Start("https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl." & EventName & ".event")
        End Sub
    End Class
End Namespace
