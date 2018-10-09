Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.TreeList
Imports System.Windows.Controls
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Mvvm

Namespace TreeListDemo
    Partial Public Class BuildTreeViaHierarchicalDataTemplate
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Enum Priority
        Low
        Normal
        High
    End Enum

    Public Class BaseObject
        Inherits BindableBase

        Public Sub New()
            ExecutorObj = GetRandomEmployee()
            Executor = ExecutorObj.ToString()
        End Sub
        Private Shared ReadOnly Random As New Random(Date.Now.Second)
        Public Shared Function GetRandomEmployee() As Employee
            If EmployeesData.DataSource Is Nothing Then
                Return Nothing
            End If
            Return EmployeesData.DataSource(Random.Next(EmployeesData.DataSource.Count))
        End Function
        Private nameCore As String
        Private executorCore As String
        Private ownerCore As ProgressingObject

        Public Property Name() As String
            Get
                Return nameCore
            End Get
            Set(ByVal value As String)
                SetProperty(nameCore, value, "Name")
            End Set
        End Property

        Public Property Executor() As String
            Get
                Return executorCore
            End Get
            Set(ByVal value As String)
                SetProperty(executorCore, value, "Executor")
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Property Owner() As ProgressingObject
            Get
                Return ownerCore
            End Get
            Set(ByVal value As ProgressingObject)
                SetProperty(ownerCore, value, "Owner")
            End Set
        End Property

        Public Property ExecutorObj() As Employee
    End Class

    Public Class ProgressingObject
        Inherits BaseObject

        Private progressCore As Integer

        Public Overridable Sub UpdateProgress()
        End Sub

        Public Property Progress() As Integer
            Get
                Return progressCore
            End Get
            Set(ByVal value As Integer)
                If SetProperty(progressCore, value, "Progress") Then
                    If Owner IsNot Nothing Then
                        Owner.UpdateProgress()
                    End If
                End If
            End Set
        End Property
    End Class

    Public Class Project
        Inherits ProgressingObject

        Protected Shared StaticImage As BitmapImage
        Shared Sub New()
            StaticImage = New BitmapImage(New Uri("/TreeListDemo;component/Images/Object_Project.png", UriKind.Relative))
        End Sub
        Public Sub New()
            Stages = New ObservableCollection(Of Stage)()
            AddHandler Stages.CollectionChanged, Sub(s, e) Me.UpdateProgress()
        End Sub
        Public Property Stages() As ObservableCollection(Of Stage)
        Public Overrides Sub UpdateProgress()
            Dim completed As Integer = 0, sum As Integer = 0

            If Stages IsNot Nothing Then

                For Each stage_Renamed As Stage In Stages
                    sum += 1
                    If stage_Renamed.Progress >= 100 Then
                        completed += 1
                    End If
                Next stage_Renamed
            End If
            If sum = 0 Then
                Progress = 100
            Else
                Progress = completed * 100 \ sum
            End If
        End Sub

        Public ReadOnly Property Image() As BitmapImage
            Get
                Return StaticImage
            End Get
        End Property
    End Class

    Public Class Stage
        Inherits ProgressingObject

        Protected Shared StaticImage As BitmapImage
        Shared Sub New()
            StaticImage = New BitmapImage(New Uri("/TreeListDemo;component/Images/Object_Stage.png", UriKind.Relative))
        End Sub
        Public Sub New()
            Tasks = New ObservableCollection(Of Task)()
            AddHandler Tasks.CollectionChanged, Sub(s, e) Me.UpdateProgress()
        End Sub
        Public Overrides Sub UpdateProgress()
            Dim completed As Integer = 0, sum As Integer = 0

            If Tasks IsNot Nothing Then

                For Each task_Renamed As Task In Tasks
                    sum += 1
                    If task_Renamed.State Is Nothing OrElse task_Renamed.State.StateValue = 2 Then
                        completed += 1
                    End If
                Next task_Renamed
            End If
            If sum = 0 Then
                Progress = 100
            Else
                Progress = completed * 100 \ sum
            End If
        End Sub
        Public Property Tasks() As ObservableCollection(Of Task)

        Public ReadOnly Property Image() As BitmapImage
            Get
                Return StaticImage
            End Get
        End Property
    End Class

    Public Class Task
        Inherits BaseObject

        Protected Shared StaticImage As BitmapImage
        Shared Sub New()
            StaticImage = New BitmapImage(New Uri("/TreeListDemo;component/Images/Object_Task.png", UriKind.Relative))
        End Sub
        Private startDateCore As Date
        Private endDateCore As Date
        Private stateCore As State
        Private priorityCore As Priority
        Public alertCore As Boolean

        Public Property StartDate() As Date
            Get
                Return startDateCore
            End Get
            Set(ByVal value As Date)
                SetProperty(startDateCore, value, "StartDate")
            End Set
        End Property

        Public Property EndDate() As Date
            Get
                Return endDateCore
            End Get
            Set(ByVal value As Date)
                If SetProperty(endDateCore, value, "EndDate") Then
                    UpdateAlertState()
                End If
            End Set
        End Property

        Public Property State() As State
            Get
                Return stateCore
            End Get
            Set(ByVal value As State)
                If SetProperty(stateCore, value, "State") Then
                    If Owner IsNot Nothing Then
                        Owner.UpdateProgress()
                    End If
                    UpdateAlertState()
                End If
            End Set
        End Property


        Public Property Priority() As Priority
            Get
                Return priorityCore
            End Get
            Set(ByVal value As Priority)
                SetProperty(priorityCore, value, "Priority")
            End Set
        End Property


        Public Property Alert() As Boolean
            Get
                Return alertCore
            End Get
            Set(ByVal value As Boolean)
                SetProperty(alertCore, value, "Alert")
            End Set
        End Property

        Private Sub UpdateAlertState()
            If stateCore IsNot Nothing Then
                Alert = EndDate < Date.Now AndAlso State.StateValue <> 2
            End If
        End Sub

        Public ReadOnly Property Image() As BitmapImage
            Get
                Return StaticImage
            End Get
        End Property
    End Class

    Public Class HierarchicalViewModel
        Public Property DataItems() As ObservableCollection(Of Project)

        Public Sub New()
            DataItems = InitData()
        End Sub

        Private Function InitData() As ObservableCollection(Of Project)
            Dim projects As New ObservableCollection(Of Project)()
            Dim betaronProject As New Project() With {.Name = "Project: Betaron"}
            Dim stantoneProject As New Project() With {.Name = "Project: Stanton"}

            InitBetaronProjectData(betaronProject)
            InitStantoneProjectData(stantoneProject)

            projects.Add(betaronProject)
            projects.Add(stantoneProject)

            Return projects
        End Function

        Private Sub InitBetaronProjectData(ByVal betaronProject As Project)

            Dim stage21 As New Stage() With {.Name = "Information Gathering", .Owner = betaronProject}
            stage21.Tasks.Add(New Task() With {.Name = "Market research", .StartDate = New Date(2011, 8, 1), .EndDate = New Date(2011, 8, 5), .State = States.DataSource(2), .Owner = stage21, .Priority = Priority.Normal})
            stage21.Tasks.Add(New Task() With {.Name = "Making specification", .StartDate = New Date(2011, 8, 5), .EndDate = New Date(2011, 8, 10), .State = States.DataSource(1), .Owner = stage21, .Priority = Priority.High})

            Dim stage22 As New Stage() With {.Name = "Planning", .Owner = betaronProject}
            stage22.Tasks.Add(New Task() With {.Name = "Documentation", .StartDate = New Date(2011, 9, 15), .EndDate = New Date(2011, 9, 16), .State = States.DataSource(0), .Owner = stage22, .Priority = Priority.High})

            Dim stage23 As New Stage() With {.Name = "Design", .Owner = betaronProject}
            stage23.Tasks.Add(New Task() With {.Name = "Design of a web pages", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0), .Owner = stage23, .Priority = Priority.Normal})
            stage23.Tasks.Add(New Task() With {.Name = "Pages layout", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0), .Owner = stage23, .Priority = Priority.Low})

            Dim stage24 As New Stage() With {.Name = "Development", .Owner = betaronProject}
            stage24.Tasks.Add(New Task() With {.Name = "Design", .StartDate = New Date(2011, 10, 27), .EndDate = New Date(2011, 10, 28), .State = States.DataSource(0), .Owner = stage24, .Priority = Priority.Normal})
            stage24.Tasks.Add(New Task() With {.Name = "Coding", .StartDate = New Date(2011, 10, 29), .EndDate = New Date(2011, 10, 30), .State = States.DataSource(0), .Owner = stage24, .Priority = Priority.Normal})

            Dim stage25 As New Stage() With {.Name = "Testing and Delivery", .Owner = betaronProject}
            stage25.Tasks.Add(New Task() With {.Name = "Testing", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0), .Owner = stage25, .Priority = Priority.Low})
            stage25.Tasks.Add(New Task() With {.Name = "Content", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0), .Owner = stage25, .Priority = Priority.Normal})

            betaronProject.Stages.Add(stage21)
            betaronProject.Stages.Add(stage22)
            betaronProject.Stages.Add(stage23)
            betaronProject.Stages.Add(stage24)
            betaronProject.Stages.Add(stage25)
        End Sub

        Private Sub InitStantoneProjectData(ByVal stantoneProject As Project)

            Dim stage11 As New Stage() With {.Name = "Information Gathering", .Owner = stantoneProject}
            stage11.Tasks.Add(New Task() With {.Name = "Market research", .StartDate = New Date(2011, 7, 1), .EndDate = New Date(2011, 7, 5), .State = States.DataSource(2), .Owner = stage11, .Priority = Priority.Normal})
            stage11.Tasks.Add(New Task() With {.Name = "Making specification", .StartDate = New Date(2011, 7, 5), .EndDate = New Date(2011, 7, 10), .State = States.DataSource(2), .Owner = stage11, .Priority = Priority.High})

            Dim stage12 As New Stage() With {.Name = "Planning", .Owner = stantoneProject}
            stage12.Tasks.Add(New Task() With {.Name = "Documentation", .StartDate = New Date(2011, 8, 13), .EndDate = New Date(2011, 8, 14), .State = States.DataSource(2), .Owner = stage12, .Priority = Priority.Normal})

            Dim stage13 As New Stage() With {.Name = "Design", .Owner = stantoneProject}
            stage13.Tasks.Add(New Task() With {.Name = "Design of a web pages", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(1), .Owner = stage13, .Priority = Priority.Normal})
            stage13.Tasks.Add(New Task() With {.Name = "Pages layout", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(1), .Owner = stage13, .Priority = Priority.Normal})

            Dim stage14 As New Stage() With {.Name = "Development", .Owner = stantoneProject}
            stage14.Tasks.Add(New Task() With {.Name = "Design", .StartDate = New Date(2011, 10, 23), .EndDate = New Date(2011, 10, 24), .State = States.DataSource(1), .Owner = stage14, .Priority = Priority.Low})
            stage14.Tasks.Add(New Task() With {.Name = "Coding", .StartDate = New Date(2011, 10, 25), .EndDate = New Date(2011, 10, 26), .State = States.DataSource(0), .Owner = stage14, .Priority = Priority.Normal})

            Dim stage15 As New Stage() With {.Name = "Testing and Delivery", .Owner = stantoneProject}
            stage15.Tasks.Add(New Task() With {.Name = "Testing", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0), .Owner = stage15})
            stage15.Tasks.Add(New Task() With {.Name = "Content", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0), .Owner = stage15, .Priority = Priority.High})

            stantoneProject.Stages.Add(stage11)
            stantoneProject.Stages.Add(stage12)
            stantoneProject.Stages.Add(stage13)
            stantoneProject.Stages.Add(stage14)
            stantoneProject.Stages.Add(stage15)
        End Sub
    End Class

    Public Class ObjectTemplateSelector
        Inherits DataTemplateSelector

        Public Property ProjectTemplate() As DataTemplate
        Public Property StageTemplate() As DataTemplate
        Public Property TaskTemplate() As DataTemplate
        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As System.Windows.DependencyObject) As System.Windows.DataTemplate
            Dim rowData As TreeListRowData = TryCast(item, TreeListRowData)
            If rowData IsNot Nothing Then
                If TypeOf rowData.Row Is Project Then
                    Return ProjectTemplate
                End If
                If TypeOf rowData.Row Is Stage Then
                    Return StageTemplate
                End If
                If TypeOf rowData.Row Is Task Then
                    Return TaskTemplate
                End If
            End If
            Return Nothing
        End Function
    End Class
End Namespace
