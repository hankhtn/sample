Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Data

Namespace EditorsDemo

    Public Class CollectionViewViewModel

        Private employees_Renamed As IList = New ObservableCollection(Of Object)(EmployeesWithPhotoData.DataSource)
        Public ReadOnly Property Employees() As IList
            Get
                Return employees_Renamed
            End Get
        End Property
        Private privateCollectionView As ICollectionView
        Public Property CollectionView() As ICollectionView
            Get
                Return privateCollectionView
            End Get
            Private Set(ByVal value As ICollectionView)
                privateCollectionView = value
            End Set
        End Property
        Public Sub New()
            CollectionView = New CollectionViewSource() With {.Source = Employees}.View
            CollectionView.GroupDescriptions.Add(New PropertyGroupDescription("JobTitle"))
            CollectionView.SortDescriptions.Add(New SortDescription("JobTitle", ListSortDirection.Ascending))
        End Sub
    End Class
End Namespace
