Imports DevExpress.Mvvm
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace ProductsDemo.Modules
    Public Enum TaskStatus
        NotStarted
        InProgress
        Completed
        WaitingOnSomeoneElse
        Deferred
    End Enum
    Public Enum TaskCategory
        HouseChores
        Shopping
        Work
    End Enum
    Public Enum FlagStatus
        Today
        Tomorrow
        ThisWeek
        NextWeek
        NoDate
        Custom
        Completed
    End Enum
    Public Enum TaskPriority
        Low = 0
        Medium = 1
        High = 2
    End Enum

    Public Class Task
        Inherits BindableBase
        Implements IDXDataErrorInfo


        Private priority_Renamed As TaskPriority = TaskPriority.Medium

        Private percentComplete_Renamed As Integer = 0

        Private createdDate_Renamed As Date



        Private startDate_Renamed? As Date = Nothing, dueDate_Renamed? As Date = Nothing, completedDate_Renamed? As Date = Nothing


        Private subject_Renamed, description_Renamed As String

        Private status_Renamed As TaskStatus = TaskStatus.NotStarted

        Private category_Renamed As TaskCategory

        Private assignTo_Renamed As Contact = Nothing

        Private changeCompleteCommand_Renamed As DelegateCommand

        Public Sub New(ByVal subject As String, ByVal category As TaskCategory)
            Me.New(subject, category, Date.Now)
        End Sub
        Friend Sub New(ByVal subject As String, ByVal category As TaskCategory, ByVal [date] As Date)
            Me.subject_Renamed = subject
            Me.category_Renamed = category
            Me.createdDate_Renamed = [date]
        End Sub
        Public Property Priority() As TaskPriority
            Get
                Return priority_Renamed
            End Get
            Set(ByVal value As TaskPriority)
                SetProperty(priority_Renamed, value, "Priority")
            End Set
        End Property
        Public Property PercentComplete() As Integer
            Get
                Return percentComplete_Renamed
            End Get
            Set(ByVal value As Integer)
                SetProperty(percentComplete_Renamed,If(value < 0, 0, If(value > 100, 100, value)), "PercentComplete", AddressOf OnPercentCompleteChanged)
            End Set
        End Property
        Private Sub OnPercentCompleteChanged()
            If percentComplete_Renamed = 100 Then
                Status = TaskStatus.Completed
            ElseIf percentComplete_Renamed > 0 Then
                Status = TaskStatus.InProgress
            End If
        End Sub
        Public ReadOnly Property CreatedDate() As Date
            Get
                Return createdDate_Renamed
            End Get
        End Property
        Public Property StartDate() As Date?
            Get
                Return startDate_Renamed
            End Get
            Set(ByVal value? As Date)
                SetProperty(startDate_Renamed, value, "StartDate")
                RaisePropertyChanged("Overdue")
            End Set
        End Property
        Public Property DueDate() As Date?
            Get
                Return dueDate_Renamed
            End Get
            Set(ByVal value? As Date)
                SetProperty(dueDate_Renamed, value, "DueDate")
                RaisePropertyChanged("Overdue")
            End Set
        End Property
        Public Property CompletedDate() As Date?
            Get
                Return completedDate_Renamed
            End Get
            Set(ByVal value? As Date)
                SetProperty(completedDate_Renamed, value, "CompletedDate")
            End Set
        End Property
        Public Property Subject() As String
            Get
                Return subject_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(subject_Renamed, value, "Subject")
            End Set
        End Property
        Public Property Description() As String
            Get
                Return description_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(description_Renamed, value, "Description")
            End Set
        End Property
        Public Property Category() As TaskCategory
            Get
                Return category_Renamed
            End Get
            Set(ByVal value As TaskCategory)
                SetProperty(category_Renamed, value, "Category")
            End Set
        End Property
        Public Property Status() As TaskStatus
            Get
                Return status_Renamed
            End Get
            Set(ByVal value As TaskStatus)
                SetProperty(status_Renamed, value, "Status", AddressOf OnStatusChanged)
            End Set
        End Property
        Private Sub OnStatusChanged()
            If Status = TaskStatus.Completed Then
                PercentComplete = 100
                CompletedDate = Date.Now
            Else
                CompletedDate = Nothing
            End If
            If Status = TaskStatus.NotStarted Then
                PercentComplete = 0
            End If
            If Status = TaskStatus.InProgress AndAlso PercentComplete = 100 Then
                PercentComplete = 75
            End If
            If Status = TaskStatus.Deferred OrElse Status = TaskStatus.WaitingOnSomeoneElse Then
                DueDate = Nothing
            End If
            RaisePropertyChanged("Complete")
        End Sub
        Public Property AssignTo() As Contact
            Get
                Return assignTo_Renamed
            End Get
            Set(ByVal value As Contact)
                assignTo_Renamed = value
            End Set
        End Property
        Friend ReadOnly Property TimeDiff() As TimeSpan
            Get
                Return (Date.Now.Subtract(CreatedDate))
            End Get
        End Property
        Public ReadOnly Property Overdue() As Boolean
            Get
                If Status = TaskStatus.Completed OrElse (Not DueDate.HasValue) Then
                    Return False
                End If
                Dim dDate As Date = DueDate.Value.Date.AddDays(1)
                If Date.Now >= dDate Then
                    Return True
                End If
                Return False
            End Get
        End Property
        Public Property Complete() As Boolean
            Get
                Return Status = TaskStatus.Completed
            End Get
            Set(ByVal value As Boolean)
                Status = If(value, TaskStatus.Completed, TaskStatus.NotStarted)
                RaisePropertiesChanged( { "Complete", "FlagStatus" })
            End Set
        End Property
        Public ReadOnly Property ChangeCompleteCommand() As DelegateCommand
            Get
                If changeCompleteCommand_Renamed Is Nothing Then
                    changeCompleteCommand_Renamed = New DelegateCommand(AddressOf ChangeComplete)
                End If
                Return changeCompleteCommand_Renamed
            End Get
        End Property
        Private Sub ChangeComplete()
            Complete = Not Complete
        End Sub
        Public ReadOnly Property Icon() As Integer
            Get
                Return If(Complete, 0, 1)
            End Get
        End Property
        Public ReadOnly Property FlagStatus() As FlagStatus
            Get
                Dim today As Date = Date.Today
                If Complete Then
                    Return FlagStatus.Completed
                End If
                If Not DueDate.HasValue Then
                    Return FlagStatus.NoDate
                End If
                If DueDate.Value.Date.Equals(today) Then
                    Return FlagStatus.Today
                End If
                If DueDate.Value.Date.Equals(today.AddDays(1)) Then
                    Return FlagStatus.Tomorrow
                End If
                Dim thisWeekStart As Date = DevExpress.Data.Filtering.Helpers.EvalHelpers.GetWeekStart(today)
                If DueDate.Value.Date >= thisWeekStart AndAlso DueDate.Value.Date < thisWeekStart.AddDays(7) Then
                    Return FlagStatus.ThisWeek
                End If
                If DueDate.Value.Date >= thisWeekStart.AddDays(7) AndAlso DueDate.Value.Date < thisWeekStart.AddDays(14) Then
                    Return FlagStatus.NextWeek
                End If
                Return FlagStatus.Custom
            End Get
        End Property
        Public Sub Assign(ByVal task As Task)
            Me.subject_Renamed = task.Subject
            Me.priority_Renamed = task.Priority
            Me.percentComplete_Renamed = task.PercentComplete
            Me.createdDate_Renamed = task.CreatedDate
            Me.startDate_Renamed = task.StartDate
            Me.dueDate_Renamed = task.DueDate
            Me.completedDate_Renamed = task.CompletedDate
            Me.description_Renamed = task.Description
            Me.category_Renamed = task.Category
            Me.status_Renamed = task.Status
            Me.assignTo_Renamed = task.AssignTo
            RaisePropertiesChanged(String.Empty)
        End Sub
        Public Function Clone() As Task
            Dim task As New Task(Me.Subject, Me.Category)
            task.Assign(Me)
            Return task
        End Function
        #Region "IDXDataErrorInfo Members"
        Private Sub IDXDataErrorInfo_GetError(ByVal info As DevExpress.XtraEditors.DXErrorProvider.ErrorInfo) Implements IDXDataErrorInfo.GetError
        End Sub

        Private Sub IDXDataErrorInfo_GetPropertyError(ByVal propertyName As String, ByVal info As DevExpress.XtraEditors.DXErrorProvider.ErrorInfo) Implements IDXDataErrorInfo.GetPropertyError
            If propertyName = "DueDate" Then
                If (DueDate.HasValue AndAlso StartDate.HasValue) AndAlso DueDate < StartDate Then
                    SetErrorInfo(info, My.Resources.DueDateError, ErrorType.Critical)
                End If
                If (Not DueDate.HasValue) AndAlso Status = TaskStatus.InProgress Then
                    SetErrorInfo(info, My.Resources.DueDateWarning, ErrorType.Warning)
                End If
            End If
        End Sub
        Private Sub SetErrorInfo(ByVal info As DevExpress.XtraEditors.DXErrorProvider.ErrorInfo, ByVal errorText As String, ByVal errorType As ErrorType)
            info.ErrorText = errorText
            info.ErrorType = errorType
        End Sub
        #End Region
    End Class

End Namespace
