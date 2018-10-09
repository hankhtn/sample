Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Utils
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input

Namespace DXDemo.Controls
    Partial Public Class CollectionViewNavigator
        Inherits UserControl

        Public Shared ReadOnly IsSynchronizedWithCurrentItemProperty As DependencyProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItem", GetType(Boolean), GetType(CollectionViewNavigator), New UIPropertyMetadata(True))
        Public Shared ReadOnly CollectionViewProperty As DependencyProperty = DependencyPropertyManager.Register("CollectionView", GetType(ICollectionView), GetType(CollectionViewNavigator), New UIPropertyMetadata(Nothing))
        Public Shared ReadOnly IsSynchronizedWithCurrentItemEditorVisibilityProperty As DependencyProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItemEditorVisibility", GetType(Visibility), GetType(CollectionViewNavigator), New UIPropertyMetadata(Visibility.Visible))
        Public Shared ReadOnly EditableCollectionViewVisibilityProperty As DependencyProperty = DependencyPropertyManager.Register("EditableCollectionViewVisibility", GetType(Visibility), GetType(CollectionViewNavigator), New UIPropertyMetadata(Visibility.Visible))


        Private directions_Renamed As IList = New List(Of ListSortDirection)() From {ListSortDirection.Ascending, ListSortDirection.Descending}

        Private fields_Renamed As IList = New List(Of String)() From {"JobTitle", "FirstName", "LastName", "BirthDate"}
        Public ReadOnly Property Directions() As IList
            Get
                Return directions_Renamed
            End Get
        End Property
        Public ReadOnly Property Fields() As IList
            Get
                Return fields_Renamed
            End Get
        End Property

        Public Property IsSynchronizedWithCurrentItem() As Boolean
            Get
                Return DirectCast(GetValue(IsSynchronizedWithCurrentItemProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsSynchronizedWithCurrentItemProperty, value)
            End Set
        End Property
        Public Property CollectionView() As ICollectionView
            Get
                Return DirectCast(GetValue(CollectionViewProperty), ICollectionView)
            End Get
            Set(ByVal value As ICollectionView)
                SetValue(CollectionViewProperty, value)
            End Set
        End Property
        Public Property IsSynchronizedWithCurrentItemEditorVisibility() As Visibility
            Get
                Return DirectCast(GetValue(IsSynchronizedWithCurrentItemEditorVisibilityProperty), Visibility)
            End Get
            Set(ByVal value As Visibility)
                SetValue(IsSynchronizedWithCurrentItemEditorVisibilityProperty, value)
            End Set
        End Property
        Public Property EditableCollectionViewVisibility() As Visibility
            Get
                Return DirectCast(GetValue(EditableCollectionViewVisibilityProperty), Visibility)
            End Get
            Set(ByVal value As Visibility)
                SetValue(EditableCollectionViewVisibilityProperty, value)
            End Set
        End Property

        Public Property CurrentSortDescription() As Integer
        Public Property CurrentGroupDescription() As Integer
        Public Property CurrentGroupFieldName() As String
        Public Property CurrentSortFieldName() As String
        Public Property CurrentSortDirection() As ListSortDirection

        Private privateDeleteGroup As ICommand
        Public Property DeleteGroup() As ICommand
            Get
                Return privateDeleteGroup
            End Get
            Private Set(ByVal value As ICommand)
                privateDeleteGroup = value
            End Set
        End Property
        Private privateDeleteSort As ICommand
        Public Property DeleteSort() As ICommand
            Get
                Return privateDeleteSort
            End Get
            Private Set(ByVal value As ICommand)
                privateDeleteSort = value
            End Set
        End Property
        Private privateAddGroup As ICommand
        Public Property AddGroup() As ICommand
            Get
                Return privateAddGroup
            End Get
            Private Set(ByVal value As ICommand)
                privateAddGroup = value
            End Set
        End Property
        Private privateAddSort As ICommand
        Public Property AddSort() As ICommand
            Get
                Return privateAddSort
            End Get
            Private Set(ByVal value As ICommand)
                privateAddSort = value
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            root.DataContext = Me
            DeleteGroup = New DelegateCommand(Of Object)(AddressOf OnDeleteGroup, AddressOf CanDeleteGroup)
            DeleteSort = New DelegateCommand(Of Object)(AddressOf OnDeleteSort, AddressOf CanDeleteSort)
            AddGroup = New DelegateCommand(Of Object)(AddressOf OnAddGroup)
            AddSort = New DelegateCommand(Of Object)(AddressOf OnAddSort)
        End Sub
        Private Sub OnDeleteGroup(ByVal parameter As Object)
            If CurrentGroupDescription >= 0 Then
                CollectionView.GroupDescriptions.RemoveAt(CurrentGroupDescription)
            End If
        End Sub
        Private Sub OnDeleteSort(ByVal parameter As Object)
            If CurrentSortDescription >= 0 Then
                CollectionView.GroupDescriptions.Remove(FindGroupDescription(CurrentSortDescription))
                CollectionView.SortDescriptions.RemoveAt(CurrentSortDescription)
            End If
        End Sub
        Private Sub OnAddGroup(ByVal parameter As Object)
            If ContainsGroupDescription(CurrentGroupFieldName) Then
                Return
            End If
            CollectionView.GroupDescriptions.Add(New PropertyGroupDescription(CurrentGroupFieldName))
            If Not ContainsSortDescription(CurrentGroupFieldName) Then
                CollectionView.SortDescriptions.Add(New SortDescription(CurrentGroupFieldName, ListSortDirection.Ascending))
            End If
        End Sub
        Private Sub OnAddSort(ByVal parameter As Object)
            If ContainsSortDescription(CurrentSortFieldName) Then
                Return
            End If
            CollectionView.SortDescriptions.Add(New SortDescription(CurrentSortFieldName, CurrentSortDirection))
        End Sub
        Public Function CanDeleteGroup(ByVal parameter As Object) As Boolean
            Return CurrentGroupDescription >= 0
        End Function
        Public Function CanDeleteSort(ByVal parameter As Object) As Boolean
            Return CurrentSortDescription >= 0
        End Function
        Private Function ContainsGroupDescription(ByVal fieldName As String) As Boolean
            For Each desc As PropertyGroupDescription In CollectionView.GroupDescriptions
                If desc.PropertyName = fieldName Then
                    Return True
                End If
            Next desc
            Return False
        End Function
        Private Function FindGroupDescription(ByVal index As Integer) As PropertyGroupDescription

            Dim name_Renamed As String = CollectionView.SortDescriptions(CurrentSortDescription).PropertyName
            For Each desc As PropertyGroupDescription In CollectionView.GroupDescriptions
                If desc.PropertyName = name_Renamed Then
                    Return desc
                End If
            Next desc
            Return Nothing
        End Function
        Private Function ContainsSortDescription(ByVal fieldName As String) As Boolean
            For Each desc As SortDescription In CollectionView.SortDescriptions
                If desc.PropertyName = fieldName Then
                    Return True
                End If
            Next desc
            Return False
        End Function

    End Class
End Namespace
