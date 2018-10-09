Imports System
Imports System.Linq
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.DevAV

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class EmployeeTaskViewModel
        Protected Overrides Function CreateEntity() As EmployeeTask
            Dim entity As EmployeeTask = MyBase.CreateEntity()
            entity.StartDate = Date.Now
            entity.DueDate = Date.Now + New TimeSpan(48, 0, 0)
            Return entity
        End Function
        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            Me.RaisePropertyChanged(Function(vm) vm.ReminderTime)
            If Entity IsNot Nothing Then
                Xpf.DemoBase.Helpers.Logger.Log(String.Format("HybridApp: Edit Task: {0}",If(String.IsNullOrEmpty(Entity.Subject), "<New>", Entity.Subject)))
            End If
        End Sub
        Public Property ReminderTime() As Date?
            Get
                If Me.Entity Is Nothing OrElse Me.Entity.ReminderDateTime Is Nothing Then
                    Return Nothing
                End If
                Return Me.Entity.ReminderDateTime.Value
            End Get
            Set(ByVal value? As Date)
                If Me.Entity Is Nothing OrElse value Is Nothing OrElse Me.Entity.ReminderDateTime Is Nothing Then
                    Return
                End If
                Dim reminderDateTime As Date = CDate(Me.Entity.ReminderDateTime)
                Me.Entity.ReminderDateTime = New Date(reminderDateTime.Year, reminderDateTime.Month, reminderDateTime.Day, CDate(value).Hour, CDate(value).Minute, reminderDateTime.Second)
            End Set
        End Property
        Protected Overrides Function GetTitle() As String
            Return If(Entity.Owner IsNot Nothing, Entity.Owner.FullName, String.Empty)
        End Function
    End Class
End Namespace
