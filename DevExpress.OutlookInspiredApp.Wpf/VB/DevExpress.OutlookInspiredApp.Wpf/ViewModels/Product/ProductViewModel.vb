Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common
Imports DevExpress.DevAV
Namespace DevExpress.DevAV.ViewModels
  ''' <summary>
  ''' Represents the single Product object view model.
  ''' </summary>
  Public Partial Class ProductViewModel
    Inherits SingleObjectViewModel(Of Product, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of ProductViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As ProductViewModel
      Return ViewModelSource.Create(Function() New ProductViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the ProductViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the ProductViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Products, Function(ByVal x) x.Name)
    End Sub    
    ''' <summary>
    ''' The view model that contains a look-up collection of OrderItems for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpOrderItems As IEntitiesViewModel(Of OrderItem)
      Get
        Return GetLookUpEntitiesViewModel(Of ProductViewModel, OrderItem, long)(propertyExpression := Function(ByVal x As ProductViewModel) x.LookUpOrderItems, getRepositoryFunc := Function(ByVal x) x.OrderItems)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpEmployees As IEntitiesViewModel(Of Employee)
      Get
        Return GetLookUpEntitiesViewModel(Of ProductViewModel, Employee, long)(propertyExpression := Function(ByVal x As ProductViewModel) x.LookUpEmployees, getRepositoryFunc := Function(ByVal x) x.Employees)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Pictures for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpPictures As IEntitiesViewModel(Of Picture)
      Get
        Return GetLookUpEntitiesViewModel(Of ProductViewModel, Picture, long)(propertyExpression := Function(ByVal x As ProductViewModel) x.LookUpPictures, getRepositoryFunc := Function(ByVal x) x.Pictures)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of ProductCatalogs for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpProductCatalogs As IEntitiesViewModel(Of ProductCatalog)
      Get
        Return GetLookUpEntitiesViewModel(Of ProductViewModel, ProductCatalog, long)(propertyExpression := Function(ByVal x As ProductViewModel) x.LookUpProductCatalogs, getRepositoryFunc := Function(ByVal x) x.ProductCatalogs)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of ProductImages for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpProductImages As IEntitiesViewModel(Of ProductImage)
      Get
        Return GetLookUpEntitiesViewModel(Of ProductViewModel, ProductImage, long)(propertyExpression := Function(ByVal x As ProductViewModel) x.LookUpProductImages, getRepositoryFunc := Function(ByVal x) x.ProductImages)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of QuoteItems for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpQuoteItems As IEntitiesViewModel(Of QuoteItem)
      Get
        Return GetLookUpEntitiesViewModel(Of ProductViewModel, QuoteItem, long)(propertyExpression := Function(ByVal x As ProductViewModel) x.LookUpQuoteItems, getRepositoryFunc := Function(ByVal x) x.QuoteItems)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the ProductOrderItems detail collection.
    ''' </summary>
    Public ReadOnly Property ProductOrderItemsDetails As CollectionViewModelBase(Of OrderItem, OrderItem, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of ProductViewModel, OrderItem, long, long?)(propertyExpression := Function(ByVal x As ProductViewModel) x.ProductOrderItemsDetails, getRepositoryFunc := Function(ByVal x) x.OrderItems, foreignKeyExpression := Function(ByVal x) x.ProductId, navigationExpression := Function(ByVal x) x.Product)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the ProductCatalog detail collection.
    ''' </summary>
    Public ReadOnly Property ProductCatalogDetails As CollectionViewModelBase(Of ProductCatalog, ProductCatalog, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of ProductViewModel, ProductCatalog, long, long?)(propertyExpression := Function(ByVal x As ProductViewModel) x.ProductCatalogDetails, getRepositoryFunc := Function(ByVal x) x.ProductCatalogs, foreignKeyExpression := Function(ByVal x) x.ProductId, navigationExpression := Function(ByVal x) x.Product)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the ProductImages detail collection.
    ''' </summary>
    Public ReadOnly Property ProductImagesDetails As CollectionViewModelBase(Of ProductImage, ProductImage, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of ProductViewModel, ProductImage, long, long?)(propertyExpression := Function(ByVal x As ProductViewModel) x.ProductImagesDetails, getRepositoryFunc := Function(ByVal x) x.ProductImages, foreignKeyExpression := Function(ByVal x) x.ProductId, navigationExpression := Function(ByVal x) x.Product)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the ProductQuoteItems detail collection.
    ''' </summary>
    Public ReadOnly Property ProductQuoteItemsDetails As CollectionViewModelBase(Of QuoteItem, QuoteItem, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of ProductViewModel, QuoteItem, long, long?)(propertyExpression := Function(ByVal x As ProductViewModel) x.ProductQuoteItemsDetails, getRepositoryFunc := Function(ByVal x) x.QuoteItems, foreignKeyExpression := Function(ByVal x) x.ProductId, navigationExpression := Function(ByVal x) x.Product)
      End Get
    End Property
  End Class
End Namespace
