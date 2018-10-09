Imports System
Imports System.Collections.Generic
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.TreeList

Namespace TreeListDemo
    Partial Public Class UnboundMode
        Inherits TreeListDemoModule

        Private Const StateFieldName As String = "State"
        Private Const StartDateFieldName As String = "StartDate"
        Private Const EndDateFieldName As String = "EndDate"

        Private Shared ReadOnly Random As New Random(Date.Now.Second)
        Public Shared Function GetRandomEmployee() As Employee
            If EmployeesData.DataSource Is Nothing Then
                Return Nothing
            End If
            Return EmployeesData.DataSource(Random.Next(EmployeesData.DataSource.Count))
        End Function

        Public Sub New()
            InitializeComponent()
            InitData()
            view.ExpandAllNodes()
        End Sub
        Private Sub InitData()
            Dim stantoneProject As TreeListNode = view.Nodes(0)
            InitStantoneProjectData(stantoneProject)
            Dim betaronProject As TreeListNode = view.Nodes(1)
            InitBetaronProjectData(betaronProject)
        End Sub
        Private Sub CellValueChanging(ByVal sender As Object, ByVal e As TreeListCellValueChangedEventArgs)
            If e.Column.FieldName = StartDateFieldName OrElse e.Column.FieldName = EndDateFieldName OrElse e.Column.FieldName = StateFieldName Then
                view.CommitEditing(True)
            End If
        End Sub
        Private Sub GetColumnData(ByVal sender As Object, ByVal e As TreeListUnboundColumnDataEventArgs)
            If e.IsSetData Then
                SetUnboundCellData(sender, e)
            Else
                GetUnboundCellData(sender, e)
            End If
        End Sub
        Private Sub GetUnboundCellData(ByVal sender As Object, ByVal e As TreeListUnboundColumnDataEventArgs)
            Select Case e.Column.FieldName
                Case StartDateFieldName
                    e.Value = GetUnboundStartDate(e, e.Node)
                Case EndDateFieldName
                    e.Value = GetUnboundEndDate(e, e.Node)
                Case StateFieldName
                    GetUnboundState(e, e.Node)
                Case Else
            End Select
        End Sub
        Private Sub SetUnboundCellData(ByVal sender As Object, ByVal e As TreeListUnboundColumnDataEventArgs)

            Dim task_Renamed As TaskObject = TryCast(e.Node.Content, TaskObject)
            Dim FieldName As String = e.Column.FieldName
            If task_Renamed IsNot Nothing Then
                Select Case FieldName
                    Case StartDateFieldName
                        task_Renamed.StartDate = CDate(If(e.Value, Date.MinValue))
                    Case EndDateFieldName
                        task_Renamed.EndDate = CDate(If(e.Value, Date.MinValue))
                    Case StateFieldName
                        Dim newState As State = CType(e.Value, State)
                        If task_Renamed.State IsNot newState Then
                            task_Renamed.State = newState
                            RecursiveNodeRefresh(e.Node.ParentNode)
                        End If
                    Case Else
                End Select
            End If
        End Sub
        Private Sub RecursiveNodeRefresh(ByVal node As TreeListNode)
            If node IsNot Nothing Then
                treeList.RefreshRow(node.RowHandle)
                RecursiveNodeRefresh(node.ParentNode)
            End If
        End Sub
        Private Sub EditorVisibility(ByVal sender As Object, ByVal e As TreeListShowingEditorEventArgs)
            Dim fieldName As String = e.Column.FieldName
            e.Cancel = (fieldName = StartDateFieldName OrElse fieldName = EndDateFieldName OrElse fieldName = StateFieldName) AndAlso Not(TypeOf e.Node.Content Is TaskObject)
        End Sub

        Private Sub CollectBoundStates(ByVal treeListNode As TreeListNode, ByVal states_Renamed As List(Of State))
            Dim [iterator] As New TreeListNodeIterator(treeListNode)
            For Each node As TreeListNode In [iterator]

                Dim task_Renamed As TaskObject = TryCast(node.Content, TaskObject)
                If task_Renamed IsNot Nothing Then
                    states_Renamed.Add(task_Renamed.State)
                End If
            Next node
        End Sub
        Private Sub GetUnboundState(ByVal e As TreeListUnboundColumnDataEventArgs, ByVal treeListNode As TreeListNode)

            Dim task_Renamed As TaskObject = TryCast(treeListNode.Content, TaskObject)
            If task_Renamed IsNot Nothing Then
                e.Value = task_Renamed.State
                Return
            End If
            Dim statesList As New List(Of State)()
            CollectBoundStates(e.Node, statesList)
            If statesList.Contains(States.DataSource(1)) OrElse (statesList.Contains(States.DataSource(0)) AndAlso statesList.Contains(States.DataSource(2))) Then
                e.Value = States.DataSource(1)
            ElseIf statesList.Contains(States.DataSource(0)) Then
                e.Value = States.DataSource(0)
            ElseIf statesList.Contains(States.DataSource(2)) Then
                e.Value = States.DataSource(2)
            End If
        End Sub
        Private Function GetUnboundStartDate(ByVal e As TreeListUnboundColumnDataEventArgs, ByVal treeListNode As TreeListNode) As Date

            Dim task_Renamed As TaskObject = TryCast(treeListNode.Content, TaskObject)
            Dim value As Date = Date.Now
            Dim tempValue As Date
            If task_Renamed IsNot Nothing Then
                value = task_Renamed.StartDate
            Else
                value = Date.MaxValue
                For Each item As TreeListNode In treeListNode.Nodes
                    tempValue = GetUnboundStartDate(e, item)
                    If tempValue < value Then
                        value = tempValue
                    End If
                Next item
            End If
            Return value
        End Function
        Private Function GetUnboundEndDate(ByVal e As TreeListUnboundColumnDataEventArgs, ByVal treeListNode As TreeListNode) As Date

            Dim task_Renamed As TaskObject = TryCast(treeListNode.Content, TaskObject)
            Dim value As Date = Date.Now
            Dim tempValue As Date
            If task_Renamed IsNot Nothing Then
                value = task_Renamed.EndDate
            Else
                value = Date.MinValue
                For Each item As TreeListNode In treeListNode.Nodes
                    tempValue = GetUnboundEndDate(e, item)
                    If tempValue > value Then
                        value = tempValue
                    End If
                Next item
            End If
            Return value
        End Function


        #Region "Unbound Data Initialization"
        Private Sub InitBetaronProjectData(ByVal betaronProject As TreeListNode)
            betaronProject.Image = ProjectObject.Image
            Dim stage21 As New TreeListNode(New StageObject() With {.NameValue = "Information Gathering", .Executor = GetRandomEmployee()})
            stage21.Image = StageObject.Image
            stage21.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Market research", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 1), .EndDate = New Date(2011, 10, 5), .State = States.DataSource(2)}) With {.Image = TaskObject.Image})
            stage21.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Making specification", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 5), .EndDate = New Date(2011, 10, 10), .State = States.DataSource(1)}) With {.Image = TaskObject.Image})
            Dim stage22 As New TreeListNode(New StageObject() With {.NameValue = "Planning", .Executor = GetRandomEmployee()})
            stage22.Image = StageObject.Image
            stage22.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Documentation", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 15), .EndDate = New Date(2011, 10, 16), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})
            Dim stage23 As New TreeListNode(New StageObject() With {.NameValue = "Design", .Executor = GetRandomEmployee()})
            stage23.Image = StageObject.Image
            stage23.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Design of a web pages", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})
            stage23.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Pages layout", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})
            Dim stage24 As New TreeListNode(New StageObject() With {.NameValue = "Development", .Executor = GetRandomEmployee()})
            stage24.Image = StageObject.Image
            stage24.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Design", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 27), .EndDate = New Date(2011, 10, 28), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})
            stage24.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Coding", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 29), .EndDate = New Date(2011, 10, 30), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})
            Dim stage25 As New TreeListNode(New StageObject() With {.NameValue = "Testing and Delivery", .Executor = GetRandomEmployee()})
            stage25.Image = StageObject.Image
            stage25.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Testing", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})
            stage25.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Content", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})

            betaronProject.Nodes.Add(stage21)
            betaronProject.Nodes.Add(stage22)
            betaronProject.Nodes.Add(stage23)
            betaronProject.Nodes.Add(stage24)
            betaronProject.Nodes.Add(stage25)
        End Sub

        Private Sub InitStantoneProjectData(ByVal stantoneProject As TreeListNode)
            stantoneProject.Image = ProjectObject.Image

            Dim stage11 As New TreeListNode(New StageObject() With {.NameValue = "Information Gathering", .Executor = GetRandomEmployee()})
            stage11.Image = StageObject.Image

            stage11.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Market research", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 1), .EndDate = New Date(2011, 10, 5), .State = States.DataSource(2)}) With {.Image = TaskObject.Image})
            stage11.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Making specification", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 5), .EndDate = New Date(2011, 10, 10), .State = States.DataSource(2)}) With {.Image = TaskObject.Image})
            Dim stage12 As New TreeListNode(New StageObject() With {.NameValue = "Planning", .Executor = GetRandomEmployee()})
            stage12.Image = StageObject.Image
            stage12.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Documentation", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(2)}) With {.Image = TaskObject.Image})

            Dim stage13 As New TreeListNode(New StageObject() With {.NameValue = "Design", .Executor = GetRandomEmployee()})
            stage13.Image = StageObject.Image
            stage13.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Design of a web pages", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(1)}) With {.Image = TaskObject.Image})
            stage13.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Pages layout", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(1)}) With {.Image = TaskObject.Image})
            Dim stage14 As New TreeListNode(New StageObject() With {.NameValue = "Development", .Executor = GetRandomEmployee()})
            stage14.Image = StageObject.Image
            stage14.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Design", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 23), .EndDate = New Date(2011, 10, 24), .State = States.DataSource(1)}) With {.Image = TaskObject.Image})
            stage14.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Coding", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 25), .EndDate = New Date(2011, 10, 26), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})
            Dim stage15 As New TreeListNode(New StageObject() With {.NameValue = "Testing and Delivery", .Executor = GetRandomEmployee()})
            stage15.Image = StageObject.Image
            stage15.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Testing", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})
            stage15.Nodes.Add(New TreeListNode(New TaskObject() With {.NameValue = "Content", .Executor = GetRandomEmployee(), .StartDate = New Date(2011, 10, 13), .EndDate = New Date(2011, 10, 14), .State = States.DataSource(0)}) With {.Image = TaskObject.Image})

            stantoneProject.Nodes.Add(stage11)
            stantoneProject.Nodes.Add(stage12)
            stantoneProject.Nodes.Add(stage13)
            stantoneProject.Nodes.Add(stage14)
            stantoneProject.Nodes.Add(stage15)
        End Sub
        #End Region
    End Class

    #Region "Classes"
    Public Class State
        Implements IComparable

        Public Property Image() As ImageSource
        Public Property TextValue() As String
        Public Property StateValue() As Integer
        Public Overrides Function ToString() As String
            Return TextValue
        End Function

        Public Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
            Return Comparer(Of Integer).Default.Compare(StateValue, DirectCast(obj, State).StateValue)
        End Function
    End Class
    Public Class TaskObject
        Public Property NameValue() As String
        Public Property StartDate() As Date
        Public Property EndDate() As Date
        Public Property Executor() As Employee
        Public Property State() As State
        Public Shared ReadOnly Property Image() As BitmapImage
            Get
                Return New BitmapImage(New Uri("/TreeListDemo;component/Images/Object_Task.png", UriKind.Relative))
            End Get
        End Property
    End Class
    Public Class States
        Inherits List(Of State)

        Private Shared src As List(Of State)
        Public Shared ReadOnly Property DataSource() As List(Of State)
            Get
                If src Is Nothing Then
                    src = New List(Of State)()
                    src.Add(New State() With {.TextValue = "Not started", .StateValue = 0, .Image = New BitmapImage(New Uri("/TreeListDemo;component/Images/State_NotStarted.png", UriKind.Relative))})
                    src.Add(New State() With {.TextValue = "In progress", .StateValue = 1, .Image = New BitmapImage(New Uri("/TreeListDemo;component/Images/State_InProgress.png", UriKind.Relative))})
                    src.Add(New State() With {.TextValue = "Completed", .StateValue = 2, .Image = New BitmapImage(New Uri("/TreeListDemo;component/Images/State_Completed.png", UriKind.Relative))})
                End If
                Return src
            End Get
        End Property
    End Class
    Public Class ProjectObject
        Public Property NameValue() As String

        Private executor_Renamed As Employee
        Public ReadOnly Property Executor() As Employee
            Get
                If executor_Renamed Is Nothing Then
                    executor_Renamed = UnboundMode.GetRandomEmployee()
                End If
                Return executor_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property Image() As BitmapImage
            Get
                Return New BitmapImage(New Uri("/TreeListDemo;component/Images/Object_Project.png", UriKind.Relative))
            End Get
        End Property
    End Class

    Public Class StageObject
        Public Property NameValue() As String
        Public Property Executor() As Employee
        Public Shared ReadOnly Property Image() As BitmapImage
            Get
                Return New BitmapImage(New Uri("/TreeListDemo;component/Images/Object_Stage.png", UriKind.Relative))
            End Get
        End Property
    End Class
    #End Region
End Namespace
