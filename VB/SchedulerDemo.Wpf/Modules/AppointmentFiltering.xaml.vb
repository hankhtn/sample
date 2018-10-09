Imports System
Imports System.Windows
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports System.Collections.Generic
Imports SchedulerDemo.Internal

Namespace SchedulerDemo
    Partial Public Class AppointmentFiltering
        Inherits SchedulerDemoModule

        Public Shared Start As New Date(2016, 09, 15, 10, 0, 0)
        Private resourceColorSchemas As New Dictionary(Of Object, SchedulerColorSchema)()

        Public Sub New()
            InitializeComponent()
            FillSportsComboBox()
            AddSportChanels()
            PrepareResourceColorSchemas()
            SetStart()
            FillData()
        End Sub

        Private Sub SetStart()
            Me.scheduler.Start = Start
            Dim selectedInterval As TimeInterval = Me.scheduler.TimelineView.SelectedInterval
            Dim selectedResource As Resource = Me.scheduler.TimelineView.SelectedResource
            Me.scheduler.TimelineView.SetSelection(New TimeInterval(Start, selectedInterval.Duration), selectedResource)
        End Sub
        Private Sub AddSportChanels()
            scheduler.Storage.ResourceStorage.BeginUpdate()
            AddResource(0, "SPORT TV 1")
            AddResource(1, "SPORT TV 2")
            AddResource(2, "SPORT TV 3")
            AddResource(3, "SPORT TV 4")
            AddResource(4, "TV 5")
            AddResource(5, "TV 6")
            AddResource(6, "TV 7")
            AddResource(7, "TV 8")
            scheduler.Storage.ResourceStorage.EndUpdate()
        End Sub
        Private Sub AddResource(ByVal index As Integer, ByVal caption As String)
            Dim r As Resource = scheduler.Storage.CreateResource(index.ToString())
            r.Caption = caption
            Dim imageName As String = String.Format("tv{0}.png", index + 1)
            Dim bitmapImage As BitmapImage = SchedulerDataHelper.GetImageFromResource(imageName)

            r.SetImage(bitmapImage)
            scheduler.Storage.ResourceStorage.Add(r)
        End Sub

        Private Sub FillData()
            scheduler.Storage.AppointmentStorage.DataSource = SchedulerXmlHelper.LoadFromXml(Of SportItem)("Sports.xml")
        End Sub

        Private Sub FillSportsComboBox()
            cbSports.BeginInit()
            For i As Integer = 0 To scheduler.Storage.AppointmentStorage.Labels.Count - 1
                Dim lab As IAppointmentLabel = scheduler.Storage.AppointmentStorage.Labels.GetByIndex(i)
                Dim index As Integer = cbSports.Items.Add(lab.DisplayName)
                cbSports.SelectedItems.Add(cbSports.Items(index))
            Next i
            cbSports.EndInit()
        End Sub

        Private Sub SchedulerStorage_FilterAppointment(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
            Dim apt As Appointment = CType(e.Object, Appointment)
            e.Cancel = Not IsAppointmentVisible(CInt((apt.LabelKey)))
        End Sub
        Private Function IsAppointmentVisible(ByVal labelId As Integer) As Boolean
            Dim count As Integer = cbSports.SelectedItems.Count
            For i As Integer = 0 To count - 1
                Dim selectedIndex As Integer = cbSports.Items.IndexOf(cbSports.SelectedItems(i))
                If selectedIndex = labelId Then
                    Return True
                End If
            Next i
            Return False
        End Function
        Private Sub cbSports_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            scheduler.ActiveView.LayoutChanged()
        End Sub
        Private Sub PrepareResourceColorSchemas()
            Dim count As Integer = scheduler.Storage.ResourceStorage.Count
            Dim currentColorchemas As SchedulerColorSchemaCollection = scheduler.GetResourceColorSchemasCopy()
            Dim schemaCount As Integer = currentColorchemas.Count
            For i As Integer = 0 To count - 1
                Dim resource As Resource = scheduler.Storage.ResourceStorage(i)
                resourceColorSchemas.Add(resource.Id, currentColorchemas(i Mod schemaCount))
            Next i
        End Sub
        Private Sub OnQueryResourceColorSchema(ByVal sender As Object, ByVal e As QueryResourceColorSchemaEventArgs)
            Dim key As Object = e.Resource.Id
            If Me.resourceColorSchemas.ContainsKey(key) Then
                e.ResourceColorSchema = Me.resourceColorSchemas(key)
            End If
        End Sub
    End Class
End Namespace
