Imports System
Imports System.ComponentModel

Namespace SchedulerDemo
    Partial Public Class ObjectDataProvider
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class CustomEventProvider
        Public Shared Function GetCustomEvents() As BindingList(Of CustomEvent)
            Dim result As New BindingList(Of CustomEvent)()
            Dim count As Integer = DemoUtils.ResourceList.Count
            For i As Integer = 0 To count - 1
                Dim subjPrefix As String = DemoUtils.ResourceList(i) & "'s "
                result.Add(CreateEvent(subjPrefix & "meeting", i, 2, 5))
                result.Add(CreateEvent(subjPrefix & "travel", i, 3, 6))
                result.Add(CreateEvent(subjPrefix & "phone call", i, 0, 10))
            Next i
            Return result
        End Function
        Private Shared Function CreateEvent(ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As CustomEvent
            Dim apt As New CustomEvent()
            apt.Subject = subject
            apt.OwnerId = resourceId
            Dim rnd As Random = DemoUtils.RandomInstance
            Dim rangeInMinutes As Integer = 60 * 24
            apt.StartTime = Date.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime.Add(TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4)))
            apt.Status = status
            apt.Label = label
            Return apt
        End Function

    End Class
    Public Class CustomResourceProvider
        Public Shared Function GetCustomResources() As BindingList(Of CustomResource)
            Dim result As New BindingList(Of CustomResource)()
            Dim count As Integer = DemoUtils.ResourceList.Count
            For i As Integer = 0 To count - 1
                Dim customResource As New CustomResource()
                customResource.Caption = DemoUtils.ResourceList(i)
                customResource.Id = i
                result.Add(customResource)
            Next i
            Return result
        End Function
    End Class
End Namespace
