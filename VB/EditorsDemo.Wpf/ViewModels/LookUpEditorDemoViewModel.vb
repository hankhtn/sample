Imports System.Collections.Generic
Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace EditorsDemo
    Public Class LookUpEditorDemoViewModel

        Public Sub New()
            CarsSource = CarsData.DataSource
            CollectionViewSource = New CollectionViewViewModel()
            DataLoader = New NWindDataLoader()
            ProductsSource = DirectCast(DataLoader.Products, IList(Of Product))
            SelectedCars = New List(Of Object)() From {CarsSource(0), CarsSource(2), CarsSource(3), CarsSource(5), CarsSource(8)}
        End Sub

        Private privateCarsSource As List(Of Cars)
        Public Property CarsSource() As List(Of Cars)
            Get
                Return privateCarsSource
            End Get
            Private Set(ByVal value As List(Of Cars))
                privateCarsSource = value
            End Set
        End Property
        Private privateCollectionViewSource As Object
        Public Property CollectionViewSource() As Object
            Get
                Return privateCollectionViewSource
            End Get
            Private Set(ByVal value As Object)
                privateCollectionViewSource = value
            End Set
        End Property
        Private privateProductsSource As IList(Of Product)
        Public Property ProductsSource() As IList(Of Product)
            Get
                Return privateProductsSource
            End Get
            Private Set(ByVal value As IList(Of Product))
                privateProductsSource = value
            End Set
        End Property
        Public Property SelectedCars() As IList(Of Object)

        Private Property DataLoader() As NWindDataLoader

    End Class
End Namespace
