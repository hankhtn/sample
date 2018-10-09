Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Utils
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Data

Namespace GridDemo
    Public MustInherit Class BindingCollection
        Inherits CollectionBase
        Implements IBindingList

        Public Overridable Sub OnListChanged(ByVal item As Object)
        End Sub
        Public Function AddNew() As Object Implements IBindingList.AddNew
            Return Nothing
        End Function
        Public ReadOnly Property AllowEdit() As Boolean Implements IBindingList.AllowEdit
            Get
                Return True
            End Get
        End Property
        Public ReadOnly Property AllowNew() As Boolean Implements IBindingList.AllowNew
            Get
                Return False
            End Get
        End Property
        Public ReadOnly Property AllowRemove() As Boolean Implements IBindingList.AllowRemove
            Get
                Return True
            End Get
        End Property

        Private listChangedHandler As ListChangedEventHandler
        Public Custom Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged
            AddHandler(ByVal value As ListChangedEventHandler)
                listChangedHandler = DirectCast(System.Delegate.Combine(listChangedHandler, value), ListChangedEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As ListChangedEventHandler)
                listChangedHandler = DirectCast(System.Delegate.Remove(listChangedHandler, value), ListChangedEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
                If listChangedHandler IsNot Nothing Then
                    For Each d As ListChangedEventHandler In listChangedHandler.GetInvocationList()
                        d.Invoke(sender, e)
                    Next d
                End If
            End RaiseEvent
        End Event
        Friend Sub OnListChanged(ByVal args As ListChangedEventArgs)
            If listChangedHandler IsNot Nothing Then
                listChangedHandler(Me, args)
            End If
        End Sub
        Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemDeleted, index))
        End Sub
        Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemAdded, index))
        End Sub

        Public Sub AddIndex(ByVal pd As PropertyDescriptor) Implements IBindingList.AddIndex
            Throw New NotSupportedException()
        End Sub
        Public Sub ApplySort(ByVal pd As PropertyDescriptor, ByVal dir As ListSortDirection) Implements IBindingList.ApplySort
            Throw New NotSupportedException()
        End Sub
        Public Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
            Throw New NotSupportedException()
        End Function
        Public ReadOnly Property IsSorted() As Boolean Implements IBindingList.IsSorted
            Get
                Return False
            End Get
        End Property
        Public Sub RemoveIndex(ByVal pd As PropertyDescriptor) Implements IBindingList.RemoveIndex
            Throw New NotSupportedException()
        End Sub
        Public Sub RemoveSort() Implements IBindingList.RemoveSort
            Throw New NotSupportedException()
        End Sub
        Public ReadOnly Property SortDirection() As ListSortDirection Implements IBindingList.SortDirection
            Get
                Throw New NotSupportedException()
            End Get
        End Property
        Public ReadOnly Property SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty
            Get
                Throw New NotSupportedException()
            End Get
        End Property
        Public ReadOnly Property SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification
            Get
                Return True
            End Get
        End Property
        Public ReadOnly Property SupportsSearching() As Boolean Implements IBindingList.SupportsSearching
            Get
                Return False
            End Get
        End Property
        Public ReadOnly Property SupportsSorting() As Boolean Implements IBindingList.SupportsSorting
            Get
                Return False
            End Get
        End Property
    End Class
    <POCOViewModel>
    Public Class Task
        Inherits BindableBase

        Private fID As Integer

        Private fRelationCollection As BindingCollection
        Public Sub New(ByVal relationCollection As BindingCollection, ByVal id As Integer, ByVal name As String, ByVal [date] As Date)
            Me.fRelationCollection = relationCollection
            Me.fID = id
            Me.TaskName = name
            Me.DueDate = [date]
            PercentComplete = 0
            Complete = False
            Note = ""
        End Sub

        Public ReadOnly Property ID() As Integer
            Get
                Return fID
            End Get
        End Property

        <BindableProperty(OnPropertyChangedMethodName := "OnListChanged")>
        Public Overridable Property TaskName() As String

        <BindableProperty(OnPropertyChangedMethodName := "OnListChanged")>
        Public Overridable Property DueDate() As Date

        Public Overridable Property Complete() As Boolean

        Protected Sub OnCompleteChanged()
            OnListChanged()
            PercentComplete = If(Complete, 100, 0)
        End Sub

        Public Property PercentComplete() As Integer
            Get
                Return GetProperty(Function() PercentComplete)
            End Get
            Set(ByVal value As Integer)
                Dim coercedValue As Integer = value
                If coercedValue < 0 Then
                    coercedValue = 0
                End If
                If coercedValue > 100 Then
                    coercedValue = 100
                End If
                If SetProperty(Function() PercentComplete, coercedValue, AddressOf OnListChanged) Then
                    Complete = PercentComplete = 100
                End If
            End Set
        End Property

        <BindableProperty(OnPropertyChangedMethodName := "OnListChanged")>
        Public Overridable Property Note() As String

        Public ReadOnly Property CategoryName() As String
            Get
                Return GetCategoryByTask(DirectCast(fRelationCollection, TasksWithCategories), Me)
            End Get
        End Property

        Private Shared Function GetCategoryByTask(ByVal collection As TasksWithCategories, ByVal task As Task) As String
            Dim ret As String = ""
            For i As Integer = 0 To collection.fCategories.Count - 1
                If collection.HasCategory(task, collection.fCategories(i)) Then
                    ret &= String.Format("{0}{1}", (If(ret = "", "", ", ")), collection.fCategories(i).CategoryName)
                End If
            Next i
            If ret = "" Then
                ret = "<None>"
            End If
            Return ret
        End Function

        Protected Sub OnListChanged()
            fRelationCollection.OnListChanged(Me)
        End Sub
    End Class
    Public Class Category
        Private fID As Integer
        Private fName As String
        Public Sub New(ByVal id As Integer, ByVal name As String)
            Me.fID = id
            Me.fName = name
        End Sub

        Public ReadOnly Property ID() As Integer
            Get
                Return fID
            End Get
        End Property

        Public Property CategoryName() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                fName = value
            End Set
        End Property
    End Class
    Public Class Relation
        Inherits FrameworkElement

        Public Shared ReadOnly CompleteProperty As DependencyProperty = DependencyPropertyManager.Register("Complete", GetType(Boolean), GetType(Relation), New PropertyMetadata(Nothing))

        Public Shared ReadOnly PercentCompleteProperty As DependencyProperty = DependencyPropertyManager.Register("PercentComplete", GetType(Integer), GetType(Relation), New PropertyMetadata(Nothing))

        Public Property Complete() As Boolean
            Get
                Return DirectCast(GetValue(CompleteProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(CompleteProperty, value)
            End Set
        End Property

        Public Property PercentComplete() As Integer
            Get
                Return DirectCast(GetValue(PercentCompleteProperty), Integer)
            End Get
            Set(ByVal value As Integer)
                SetValue(PercentCompleteProperty, value)
            End Set
        End Property

        Friend fTask As Task
        Friend fCategory As Category
        Public Sub New(ByVal task As Task, ByVal category As Category)
            Me.fTask = task
            Me.fCategory = category

            Dim completeBinding As New Binding("Complete")
            completeBinding.Source = fTask
            completeBinding.Mode = BindingMode.TwoWay
            Me.SetBinding(CompleteProperty, completeBinding)

            Dim percentCompleteBinding As New Binding("PercentComplete")
            percentCompleteBinding.Source = fTask
            percentCompleteBinding.Mode = BindingMode.TwoWay
            Me.SetBinding(PercentCompleteProperty, percentCompleteBinding)
        End Sub
        Public Property TaskName() As String
            Get
                Return fTask.TaskName
            End Get
            Set(ByVal value As String)
                fTask.TaskName = value
            End Set
        End Property
        Public Property DueDate() As Date
            Get
                Return fTask.DueDate
            End Get
            Set(ByVal value As Date)
                fTask.DueDate = value
            End Set
        End Property
        Public ReadOnly Property CategoryName() As String
            Get
                Return fCategory.CategoryName
            End Get
        End Property

        Public Property Note() As String
            Get
                Return fTask.Note
            End Get
            Set(ByVal value As String)
                fTask.Note = value
            End Set
        End Property
    End Class
    Public Class Tasks
        Inherits BindingCollection

        Public Shared MaxTasks As Integer = 7
        Public Shared Function GetTasks(ByVal collection As TasksWithCategories) As Tasks
            Dim ret As New Tasks()
            Dim rnd As New Random()
            For i As Integer = 0 To MaxTasks - 1
                Dim index As Integer = i + 1
                ret.List.Add(ViewModelSource.Create(Function() New Task(collection, index, "Task" & index, Date.Today.AddDays(rnd.Next(5)))))
                If i = 2 Then
                    ret(i).PercentComplete = 50
                End If
                If i = 6 Then
                    ret(i).PercentComplete = 100
                End If
            Next i
            Return ret
        End Function
        Default Public Property Item(ByVal index As Integer) As Task
            Get
                Return DirectCast(List(index), Task)
            End Get
            Set(ByVal value As Task)
                List(index) = value
            End Set
        End Property
    End Class
    Public Class Categories
        Inherits BindingCollection

        Public Shared MaxCategories As Integer = 6
        Public Shared Function GetCategories(ByVal collection As TasksWithCategories) As Categories
            Dim ret As New Categories()
            Dim names() As String = { "Business", "Competitor", "Favorites", "Gifts", "Goals", "Holiday", "Ideas", "International", "Personal" }
            For i As Integer = 0 To names.Length - 1
                ret.List.Add(New Category(i + 1, names(i)))
            Next i
            Return ret
        End Function
        Default Public Property Item(ByVal index As Integer) As Category
            Get
                Return DirectCast(List(index), Category)
            End Get
            Set(ByVal value As Category)
                List(index) = value
            End Set
        End Property
    End Class
    Public Class TasksWithCategories
        Inherits BindingCollection

        Friend fTasks As Tasks
        Friend fCategories As Categories
        Public Sub New()
            fTasks = Tasks.GetTasks(Me)
            fCategories = Categories.GetCategories(Me)
        End Sub
        Public Shared Function GetTasksWithCategories() As TasksWithCategories
            Dim ret As New TasksWithCategories()
            Dim rnd As New Random()
            For i As Integer = 0 To Tasks.MaxTasks - 1
                For j As Integer = 0 To (1 + rnd.Next(Categories.MaxCategories)) - 1
                    Dim cat As Category = ret.fCategories(rnd.Next(ret.fCategories.Count))
                    If Not ret.HasCategory(ret.fTasks(i), cat) Then
                        ret.List.Add(New Relation(ret.fTasks(i), cat))
                    End If
                Next j
            Next i
            Return ret
        End Function
        Default Public Property Item(ByVal index As Integer) As Relation
            Get
                Return DirectCast(List(index), Relation)
            End Get
            Set(ByVal value As Relation)
                List(index) = value
            End Set
        End Property
        Public Overrides Sub OnListChanged(ByVal item As Object)
            If item Is Nothing Then
                Return
            End If
            For i As Integer = 0 To Me.Count - 1
                If item.Equals(Me(i).fTask) Then
                    Me.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, i))
                End If
            Next i
        End Sub
        Public Function HasCategory(ByVal task As Task, ByVal category As Category) As Boolean
            For i As Integer = 0 To Me.Count - 1
                If Me(i).fCategory Is category AndAlso Me(i).fTask.Equals(task) Then
                    Return True
                End If
            Next i
            Return False
        End Function
    End Class
    Public Class GroupingControllerTasksWithCategories
        Private fGrid As GridControl
        Private fTasks As TasksWithCategories
        Public Event AfterGrouping As EventHandler
        Public Sub New(ByVal grid As GridControl)
            Me.fGrid = grid
            Me.fTasks = TasksWithCategories.GetTasksWithCategories()
            AddHandler grid.EndGrouping, AddressOf Grid_Grouping
            SetDataSource()
        End Sub
        Private Sub Grid_Grouping(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetDataSource()
            RaiseEvent AfterGrouping(Me, EventArgs.Empty)
        End Sub

        Public ReadOnly Property CategoryColumn() As GridColumn
            Get
                Return fGrid.Columns("CategoryName")
            End Get
        End Property

        Public ReadOnly Property IsCategoryGrouping() As Boolean
            Get
                If CategoryColumn Is Nothing Then
                    Return False
                End If
                Return CategoryColumn.IsGrouped
            End Get
        End Property

        Public Sub SetDataSource()
            If IsCategoryGrouping Then
                fGrid.ItemsSource = fTasks
            Else
                fGrid.ItemsSource = fTasks.fTasks
            End If
        End Sub
    End Class
End Namespace
