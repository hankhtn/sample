Imports System
Imports System.Windows
Imports SchedulerDemo
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler
Imports System.Windows.Input
Imports DevExpress.Xpf.Scheduler.UI
Imports System.ComponentModel
Imports DevExpress.Mvvm

Namespace SchedulerDemo
    Partial Public Class CustomMenu
        Inherits SchedulerDemoModule


        Private commandsCollection_Renamed As CustomMenuDemoAppointmentCommandsCollection

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
            AddHandler scheduler.PopupMenuShowing, AddressOf PopupMenuShowing
            CustomMenuModule.DataContext = Me
            commandsCollection_Renamed = New CustomMenuDemoAppointmentCommandsCollection(scheduler)
        End Sub

        Public ReadOnly Property CommandsCollection() As CustomMenuDemoAppointmentCommandsCollection
            Get
                Return commandsCollection_Renamed
            End Get
        End Property

        Private Sub PopupMenuShowing(ByVal sender As Object, ByVal e As SchedulerMenuEventArgs)
            If e.Menu.Name = SchedulerMenuItemName.AppointmentMenu Then
                e.Customizations.Add(changeAppointment)
            End If
        End Sub
    End Class

    Public MustInherit Class AppointmentCommandBase
        Inherits BindableBase
        Implements ICommand

        Private controlCore As SchedulerControl = Nothing
        Private subjectCore As String
        Private labelIdCore As Integer

        Public Sub New(ByVal control As SchedulerControl, ByVal subject As String, ByVal labelId As Integer)
            Me.Control = control
            subjectCore = subject
            labelIdCore = labelId
        End Sub
        Public Sub New()
            Me.New(Nothing, String.Empty, 0)
        End Sub

        Public Property Control() As SchedulerControl
            Get
                Return controlCore
            End Get
            Set(ByVal value As SchedulerControl)
                SetProperty(controlCore, value, "Control")
            End Set
        End Property
        Public Property Subject() As String
            Get
                Return subjectCore
            End Get
            Set(ByVal value As String)
                subjectCore = value
            End Set
        End Property
        Public Property LabelId() As Integer
            Get
                Return labelIdCore
            End Get
            Set(ByVal value As Integer)
                labelIdCore = value
            End Set
        End Property

        #Region "ICommand Members"
        Private Function ICommand_CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function
        Private Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(ByVal value As EventHandler)
                AddHandler _canExecuteChanged, value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                RemoveHandler _canExecuteChanged, value
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
                RaiseEvent _canExecuteChanged(sender, e)
            End RaiseEvent
        End Event
        Private Event _canExecuteChanged As EventHandler
        Protected Sub FakeMethod()
            RaiseEvent _canExecuteChanged(Me, EventArgs.Empty)
        End Sub
        Private Sub ICommand_Execute(ByVal parameter As Object) Implements ICommand.Execute
            Execute(parameter)
        End Sub
        #End Region
        Public MustOverride Sub Execute(ByVal parameter As Object)
    End Class
    Public Class CreateAppointmentCommand
        Inherits AppointmentCommandBase

        Public Sub New(ByVal control As SchedulerControl, ByVal subject As String, ByVal labelId As Integer)
            MyBase.New(control, subject, labelId)
        End Sub
        Public Sub New()
            Me.New(Nothing, String.Empty, 0)
        End Sub

        Public Overrides Sub Execute(ByVal parameter As Object)
            Dim apt As Appointment = Control.Storage.CreateAppointment(AppointmentType.Normal)
            apt.Subject = Subject
            apt.Start = Control.ActiveView.SelectedInterval.Start
            apt.End = Control.ActiveView.SelectedInterval.End
            apt.ResourceId = Control.ActiveView.SelectedResource.Id
            apt.StatusKey = 1
            apt.LabelKey = LabelId
            Control.Storage.AppointmentStorage.Add(apt)
        End Sub
    End Class
    Public Class ChangeAppointmentCommand
        Inherits AppointmentCommandBase

        Public Sub New(ByVal control As SchedulerControl, ByVal subject As String, ByVal labelId As Integer)
            MyBase.New(control, subject, labelId)
        End Sub
        Public Sub New()
            Me.New(Nothing, String.Empty, 0)
        End Sub

        Public Overrides Sub Execute(ByVal parameter As Object)
            Dim appointments As AppointmentBaseCollection = Control.SelectedAppointments
            For i As Integer = 0 To appointments.Count - 1
                Dim apt As Appointment = appointments(i)
                apt.Subject = Subject
                apt.StatusKey = 1
                apt.LabelKey = LabelId
            Next i
        End Sub
    End Class

    Public Class CustomMenuDemoAppointmentCommandsCollection
        Inherits BindableBase

        Public Sub New(ByVal control As SchedulerControl)
            ChangeCheckEngineOilCommand = New ChangeAppointmentCommand(control, "Check engine oil", 1)
            CreateCheckEngineOilCommand = New CreateAppointmentCommand(control, "Check engine oil", 1)
            ChangeWashTheCarCommand = New ChangeAppointmentCommand(control, "Wash the car", 2)
            CreateWashTheCarCommand = New CreateAppointmentCommand(control, "Wash the car", 2)
            ChangeWaxTheCarCommand = New ChangeAppointmentCommand(control, "Wax the car", 3)
            CreateWaxTheCarCommand = New CreateAppointmentCommand(control, "Wax the car", 3)
            ChangeCheckTransmissionFluidCommand = New ChangeAppointmentCommand(control, "Check transmission fluid", 4)
            CreateCheckTransmissionFluidCommand = New CreateAppointmentCommand(control, "Check transmission fluid", 4)
            ChangeInspectByMechanicCommand = New ChangeAppointmentCommand(control, "Inspect by mechanic", 5)
            CreateInspectByMechanicCommand = New CreateAppointmentCommand(control, "Inspect by mechanic", 5)
        End Sub

        Private privateChangeCheckEngineOilCommand As ChangeAppointmentCommand
        Public Property ChangeCheckEngineOilCommand() As ChangeAppointmentCommand
            Get
                Return privateChangeCheckEngineOilCommand
            End Get
            Private Set(ByVal value As ChangeAppointmentCommand)
                privateChangeCheckEngineOilCommand = value
            End Set
        End Property
        Private privateCreateCheckEngineOilCommand As CreateAppointmentCommand
        Public Property CreateCheckEngineOilCommand() As CreateAppointmentCommand
            Get
                Return privateCreateCheckEngineOilCommand
            End Get
            Private Set(ByVal value As CreateAppointmentCommand)
                privateCreateCheckEngineOilCommand = value
            End Set
        End Property
        Private privateChangeWashTheCarCommand As ChangeAppointmentCommand
        Public Property ChangeWashTheCarCommand() As ChangeAppointmentCommand
            Get
                Return privateChangeWashTheCarCommand
            End Get
            Private Set(ByVal value As ChangeAppointmentCommand)
                privateChangeWashTheCarCommand = value
            End Set
        End Property
        Private privateCreateWashTheCarCommand As CreateAppointmentCommand
        Public Property CreateWashTheCarCommand() As CreateAppointmentCommand
            Get
                Return privateCreateWashTheCarCommand
            End Get
            Private Set(ByVal value As CreateAppointmentCommand)
                privateCreateWashTheCarCommand = value
            End Set
        End Property
        Private privateChangeWaxTheCarCommand As ChangeAppointmentCommand
        Public Property ChangeWaxTheCarCommand() As ChangeAppointmentCommand
            Get
                Return privateChangeWaxTheCarCommand
            End Get
            Private Set(ByVal value As ChangeAppointmentCommand)
                privateChangeWaxTheCarCommand = value
            End Set
        End Property
        Private privateCreateWaxTheCarCommand As CreateAppointmentCommand
        Public Property CreateWaxTheCarCommand() As CreateAppointmentCommand
            Get
                Return privateCreateWaxTheCarCommand
            End Get
            Private Set(ByVal value As CreateAppointmentCommand)
                privateCreateWaxTheCarCommand = value
            End Set
        End Property
        Private privateChangeCheckTransmissionFluidCommand As ChangeAppointmentCommand
        Public Property ChangeCheckTransmissionFluidCommand() As ChangeAppointmentCommand
            Get
                Return privateChangeCheckTransmissionFluidCommand
            End Get
            Private Set(ByVal value As ChangeAppointmentCommand)
                privateChangeCheckTransmissionFluidCommand = value
            End Set
        End Property
        Private privateCreateCheckTransmissionFluidCommand As CreateAppointmentCommand
        Public Property CreateCheckTransmissionFluidCommand() As CreateAppointmentCommand
            Get
                Return privateCreateCheckTransmissionFluidCommand
            End Get
            Private Set(ByVal value As CreateAppointmentCommand)
                privateCreateCheckTransmissionFluidCommand = value
            End Set
        End Property
        Private privateChangeInspectByMechanicCommand As ChangeAppointmentCommand
        Public Property ChangeInspectByMechanicCommand() As ChangeAppointmentCommand
            Get
                Return privateChangeInspectByMechanicCommand
            End Get
            Private Set(ByVal value As ChangeAppointmentCommand)
                privateChangeInspectByMechanicCommand = value
            End Set
        End Property
        Private privateCreateInspectByMechanicCommand As CreateAppointmentCommand
        Public Property CreateInspectByMechanicCommand() As CreateAppointmentCommand
            Get
                Return privateCreateInspectByMechanicCommand
            End Get
            Private Set(ByVal value As CreateAppointmentCommand)
                privateCreateInspectByMechanicCommand = value
            End Set
        End Property
    End Class
End Namespace
