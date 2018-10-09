Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Grid
Imports System.Collections

Namespace TreeListDemo
    Partial Public Class BuildTreeViaChildNodesSelector
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class ChildNodesSelectorViewModel
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

            Dim stage21 As New Stage() With {.Name = "Information Gathering"}
            stage21.Tasks.Add(New Task() With {.Name = "Market research", .StartDate = New Date(2011, 8, 1), .EndDate = New Date(2011, 8, 5), .State = States.DataSource(2)})
            stage21.Tasks.Add(New Task() With {.Name = "Making specification", .StartDate = New Date(2011, 8, 5), .EndDate = New Date(2011, 8, 10), .State = States.DataSource(1)})

            Dim stage22 As New Stage() With {.Name = "Planning"}
            stage22.Tasks.Add(New Task() With {.Name = "Documentation", .StartDate = New Date(2011, 9, 15), .EndDate = New Date(2011, 9, 16), .State = States.DataSource(0)})

            Dim stage23 As New Stage() With {.Name = "Design"}
            stage23.Tasks.Add(New Task() With {.Name = "Design of a web pages", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)})
            stage23.Tasks.Add(New Task() With {.Name = "Pages layout", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)})

            Dim stage24 As New Stage() With {.Name = "Development"}
            stage24.Tasks.Add(New Task() With {.Name = "Design", .StartDate = New Date(2011, 10, 27), .EndDate = New Date(2011, 10, 28), .State = States.DataSource(0)})
            stage24.Tasks.Add(New Task() With {.Name = "Coding", .StartDate = New Date(2011, 10, 29), .EndDate = New Date(2011, 10, 30), .State = States.DataSource(0)})

            Dim stage25 As New Stage() With {.Name = "Testing and Delivery"}
            stage25.Tasks.Add(New Task() With {.Name = "Testing", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)})
            stage25.Tasks.Add(New Task() With {.Name = "Content", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)})

            betaronProject.Stages.Add(stage21)
            betaronProject.Stages.Add(stage22)
            betaronProject.Stages.Add(stage23)
            betaronProject.Stages.Add(stage24)
            betaronProject.Stages.Add(stage25)
        End Sub

        Private Sub InitStantoneProjectData(ByVal stantoneProject As Project)

            Dim stage11 As New Stage() With {.Name = "Information Gathering"}
            stage11.Tasks.Add(New Task() With {.Name = "Market research", .StartDate = New Date(2011, 7, 1), .EndDate = New Date(2011, 7, 5), .State = States.DataSource(2)})
            stage11.Tasks.Add(New Task() With {.Name = "Making specification", .StartDate = New Date(2011, 7, 5), .EndDate = New Date(2011, 7, 10), .State = States.DataSource(2)})

            Dim stage12 As New Stage() With {.Name = "Planning"}
            stage12.Tasks.Add(New Task() With {.Name = "Documentation", .StartDate = New Date(2011, 8, 13), .EndDate = New Date(2011, 8, 14), .State = States.DataSource(2)})

            Dim stage13 As New Stage() With {.Name = "Design"}
            stage13.Tasks.Add(New Task() With {.Name = "Design of a web pages", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(1)})
            stage13.Tasks.Add(New Task() With {.Name = "Pages layout", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(1)})

            Dim stage14 As New Stage() With {.Name = "Development"}
            stage14.Tasks.Add(New Task() With {.Name = "Design", .StartDate = New Date(2011, 10, 23), .EndDate = New Date(2011, 10, 24), .State = States.DataSource(1)})
            stage14.Tasks.Add(New Task() With {.Name = "Coding", .StartDate = New Date(2011, 10, 25), .EndDate = New Date(2011, 10, 26), .State = States.DataSource(0)})

            Dim stage15 As New Stage() With {.Name = "Testing and Delivery"}
            stage15.Tasks.Add(New Task() With {.Name = "Testing", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)})
            stage15.Tasks.Add(New Task() With {.Name = "Content", .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)})

            stantoneProject.Stages.Add(stage11)
            stantoneProject.Stages.Add(stage12)
            stantoneProject.Stages.Add(stage13)
            stantoneProject.Stages.Add(stage14)
            stantoneProject.Stages.Add(stage15)
        End Sub
    End Class

    Public Class DemoChildSelector
        Implements IChildNodesSelector

        Private Function IChildNodesSelector_SelectChildren(ByVal item As Object) As IEnumerable Implements IChildNodesSelector.SelectChildren
            If TypeOf item Is Project Then
                Return (TryCast(item, Project)).Stages
            End If
            If TypeOf item Is Stage Then
                Return (TryCast(item, Stage)).Tasks
            End If
            Return Nothing
        End Function
    End Class
End Namespace
