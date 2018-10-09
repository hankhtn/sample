Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.iCalendar
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native

Namespace SchedulingDemo
    Public Class ReportTemplateInfo
        Public Sub New()
        End Sub
        Public Sub New(ByVal displayName As String, ByVal reportTemplatePath As String)
            Me.DisplayName = displayName
            Me.ReportTemplatePath = reportTemplatePath
        End Sub

        Public Property DisplayName() As String
        Public Property ReportTemplatePath() As String
    End Class
    Public Class Utils
        Public Shared Function GetReportTemplateCollection() As List(Of ReportTemplateInfo)
            Dim reportTemplateCollection As New List(Of ReportTemplateInfo)()
            Dim reportTemplatesDirectory As String = DataFilesHelper.FindFile("SchedulerReportTemplates", DataFilesHelper.DataPath)
            Dim directoryInfo As New DirectoryInfo(reportTemplatesDirectory)
            For Each fileInfo As FileInfo In directoryInfo.GetFiles()
                reportTemplateCollection.Add(New ReportTemplateInfo(fileInfo.Name, fileInfo.FullName))
            Next fileInfo
            Return reportTemplateCollection
        End Function

        Public Shared Sub SendMail(ByVal scheduler As SchedulerControl, ByVal appointments As ObservableCollection(Of AppointmentItem))
            If appointments.Count = 0 Then
                Return
            End If
            Dim tempdirInfos As New List(Of DirectoryInfo)(appointments.Count)
            Try
                Dim files(appointments.Count - 1) As String
                Dim i As Integer = 0
                For Each appointment As AppointmentItem In appointments
                    Dim tempDirectoryName As String = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())
                    Dim tempdirInfo As New DirectoryInfo(tempDirectoryName)
                    If tempdirInfo.Exists Then
                        Return
                    End If
                    tempdirInfo.Create()
                    tempdirInfos.Add(tempdirInfo)
                    Dim items As New AppointmentItemCollection()
                    items.Add(If(appointment.Type = DevExpress.XtraScheduler.AppointmentType.Occurrence, appointment.RecurrencePattern, appointment))
                    Dim exporter As New ICalendarExporter(scheduler, items)
                    Dim filePath As String = String.Format("{0}/{1}.ics", tempDirectoryName, GetFileName(appointment))
                    Using output As FileStream = File.OpenWrite(filePath)
                        exporter.Export(output)
                        files(i) = filePath
                        i += 1
                        output.Flush()
                    End Using
                Next appointment
                Dim recipients As New RecipientCollection()
                recipients.Add(New Recipient())
                MAPI.SendMail(IntPtr.Zero, files,If(appointments.Count = 1, String.Format("FW:{0}", GetFileName(appointments(0))), ""), "", recipients)
            Finally
                For Each tempdirInfo As DirectoryInfo In tempdirInfos
                    tempdirInfo.Delete(True)
                Next tempdirInfo
            End Try
        End Sub

        Private Shared Function GetFileName(ByVal appointment As AppointmentItem) As String
            Return String.Format("{0}.ics",If(String.IsNullOrEmpty(appointment.Subject), "Untitled", appointment.Subject))
        End Function
    End Class
End Namespace
