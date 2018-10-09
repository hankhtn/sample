Imports System.Collections.ObjectModel
Imports System.Reflection
Imports System.Windows
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Scheduling

Namespace SchedulingDemo.ViewModels
    Public Class OutlookEventWindowViewModel
        Inherits AppointmentWindowViewModel

        Public Shared Function Create(ByVal scheduler As SchedulerControl, ByVal appointment As AppointmentItem) As OutlookEventWindowViewModel
            Return ViewModelSource.Create(Function() New OutlookEventWindowViewModel(scheduler, appointment))
        End Function
        Protected Sub New(ByVal scheduler As SchedulerControl, ByVal appointment As AppointmentItem)
            MyBase.New(appointment, scheduler)
            Dim priority As EventPriority = CType(appointment.CustomFields("Priority"), EventPriority)
            HighImportanceChecked = priority = EventPriority.HighImportance
            LowImportanceChecked = priority = EventPriority.LowImportance
            Categories = New ObservableCollection(Of CheckedCategory)()
            For Each label As AppointmentLabelItem In scheduler.LabelItems
                Categories.Add(CheckedCategory.Create(label, Object.Equals(Label.Id, label.Id)))
            Next label
        End Sub

        Public Overridable Property HighImportanceChecked() As Boolean
        Public Overridable Property LowImportanceChecked() As Boolean
        Public Overridable Property Categories() As ObservableCollection(Of CheckedCategory)

        Public Sub Send()
            If ArePropertiesChanged Then
                DXMessageBox.Show("This item must be saved before it can be forwarded.", System.Reflection.Assembly.GetEntryAssembly().GetName().Name, MessageBoxButton.OK, MessageBoxImage.Warning)
                Return
            End If
            Utils.SendMail(Scheduler, New ObservableCollection(Of AppointmentItem)() From {Appointment})
        End Sub
        Public Sub ChangeCategorize(ByVal label As AppointmentLabelItem)
            Label = label
        End Sub
        Public Sub SetHighPriority()
            Dim priority As EventPriority = CType(CustomFields("Priority"), EventPriority)
            CustomFields("Priority") = If(priority = EventPriority.HighImportance, EventPriority.None, EventPriority.HighImportance)
            If LowImportanceChecked Then
                LowImportanceChecked = Not HighImportanceChecked
            End If
        End Sub
        Public Sub SetLowPriority()
            Dim priority As EventPriority = CType(CustomFields("Priority"), EventPriority)
            CustomFields("Priority") = If(priority = EventPriority.LowImportance, EventPriority.None, EventPriority.LowImportance)
            If HighImportanceChecked Then
                HighImportanceChecked = Not LowImportanceChecked
            End If
        End Sub
    End Class
End Namespace
