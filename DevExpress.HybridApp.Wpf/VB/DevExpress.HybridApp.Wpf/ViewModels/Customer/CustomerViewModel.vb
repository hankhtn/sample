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
  ''' Represents the single Customer object view model.
  ''' </summary>
  Public Partial Class CustomerViewModel
    Inherits SingleObjectViewModel(Of Customer, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of CustomerViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As CustomerViewModel
      Return ViewModelSource.Create(Function() New CustomerViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the CustomerViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the CustomerViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Customers, Function(ByVal x) x.Name)
    End Sub    
    ''' <summary>
    ''' The view model that contains a look-up collection of CustomerEmployees for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpCustomerEmployees As IEntitiesViewModel(Of CustomerEmployee)
      Get
        Return GetLookUpEntitiesViewModel(Of CustomerViewModel, CustomerEmployee, long)(propertyExpression := Function(ByVal x As CustomerViewModel) x.LookUpCustomerEmployees, getRepositoryFunc := Function(ByVal x) x.CustomerEmployees)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of CustomerStores for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpCustomerStores As IEntitiesViewModel(Of CustomerStore)
      Get
        Return GetLookUpEntitiesViewModel(Of CustomerViewModel, CustomerStore, long)(propertyExpression := Function(ByVal x As CustomerViewModel) x.LookUpCustomerStores, getRepositoryFunc := Function(ByVal x) x.CustomerStores)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Orders for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpOrders As IEntitiesViewModel(Of Order)
      Get
        Return GetLookUpEntitiesViewModel(Of CustomerViewModel, Order, long)(propertyExpression := Function(ByVal x As CustomerViewModel) x.LookUpOrders, getRepositoryFunc := Function(ByVal x) x.Orders)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Quotes for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpQuotes As IEntitiesViewModel(Of Quote)
      Get
        Return GetLookUpEntitiesViewModel(Of CustomerViewModel, Quote, long)(propertyExpression := Function(ByVal x As CustomerViewModel) x.LookUpQuotes, getRepositoryFunc := Function(ByVal x) x.Quotes)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the CustomerEmployees detail collection.
    ''' </summary>
    Public ReadOnly Property CustomerEmployeesDetails As CollectionViewModelBase(Of CustomerEmployee, CustomerEmployee, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of CustomerViewModel, CustomerEmployee, long, long?)(propertyExpression := Function(ByVal x As CustomerViewModel) x.CustomerEmployeesDetails, getRepositoryFunc := Function(ByVal x) x.CustomerEmployees, foreignKeyExpression := Function(ByVal x) x.CustomerId, navigationExpression := Function(ByVal x) x.Customer)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the CustomerCustomerStores detail collection.
    ''' </summary>
    Public ReadOnly Property CustomerCustomerStoresDetails As CollectionViewModelBase(Of CustomerStore, CustomerStore, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of CustomerViewModel, CustomerStore, long, long?)(propertyExpression := Function(ByVal x As CustomerViewModel) x.CustomerCustomerStoresDetails, getRepositoryFunc := Function(ByVal x) x.CustomerStores, foreignKeyExpression := Function(ByVal x) x.CustomerId, navigationExpression := Function(ByVal x) x.Customer)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the CustomerOrders detail collection.
    ''' </summary>
    Public ReadOnly Property CustomerOrdersDetails As CollectionViewModelBase(Of Order, Order, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of CustomerViewModel, Order, long, long?)(propertyExpression := Function(ByVal x As CustomerViewModel) x.CustomerOrdersDetails, getRepositoryFunc := Function(ByVal x) x.Orders, foreignKeyExpression := Function(ByVal x) x.CustomerId, navigationExpression := Function(ByVal x) x.Customer)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the CustomerQuotes detail collection.
    ''' </summary>
    Public ReadOnly Property CustomerQuotesDetails As CollectionViewModelBase(Of Quote, Quote, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of CustomerViewModel, Quote, long, long?)(propertyExpression := Function(ByVal x As CustomerViewModel) x.CustomerQuotesDetails, getRepositoryFunc := Function(ByVal x) x.Quotes, foreignKeyExpression := Function(ByVal x) x.CustomerId, navigationExpression := Function(ByVal x) x.Customer)
      End Get
    End Property
  End Class
End Namespace
