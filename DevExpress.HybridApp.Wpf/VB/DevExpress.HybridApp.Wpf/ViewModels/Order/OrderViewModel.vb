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
  ''' Represents the single Order object view model.
  ''' </summary>
  Public Partial Class OrderViewModel
    Inherits SingleObjectViewModel(Of Order, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of OrderViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As OrderViewModel
      Return ViewModelSource.Create(Function() New OrderViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the OrderViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the OrderViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Orders, Function(ByVal x) x.InvoiceNumber)
    End Sub    
    ''' <summary>
    ''' The view model that contains a look-up collection of Customers for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpCustomers As IEntitiesViewModel(Of Customer)
      Get
        Return GetLookUpEntitiesViewModel(Of OrderViewModel, Customer, long)(propertyExpression := Function(ByVal x As OrderViewModel) x.LookUpCustomers, getRepositoryFunc := Function(ByVal x) x.Customers)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpEmployees As IEntitiesViewModel(Of Employee)
      Get
        Return GetLookUpEntitiesViewModel(Of OrderViewModel, Employee, long)(propertyExpression := Function(ByVal x As OrderViewModel) x.LookUpEmployees, getRepositoryFunc := Function(ByVal x) x.Employees)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of CustomerStores for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpCustomerStores As IEntitiesViewModel(Of CustomerStore)
      Get
        Return GetLookUpEntitiesViewModel(Of OrderViewModel, CustomerStore, long)(propertyExpression := Function(ByVal x As OrderViewModel) x.LookUpCustomerStores, getRepositoryFunc := Function(ByVal x) x.CustomerStores)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of OrderItems for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpOrderItems As IEntitiesViewModel(Of OrderItem)
      Get
        Return GetLookUpEntitiesViewModel(Of OrderViewModel, OrderItem, long)(propertyExpression := Function(ByVal x As OrderViewModel) x.LookUpOrderItems, getRepositoryFunc := Function(ByVal x) x.OrderItems)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the OrderOrderItems detail collection.
    ''' </summary>
    Public ReadOnly Property OrderOrderItemsDetails As CollectionViewModelBase(Of OrderItem, OrderItem, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of OrderViewModel, OrderItem, long, long?)(propertyExpression := Function(ByVal x As OrderViewModel) x.OrderOrderItemsDetails, getRepositoryFunc := Function(ByVal x) x.OrderItems, foreignKeyExpression := Function(ByVal x) x.OrderId, navigationExpression := Function(ByVal x) x.Order)
      End Get
    End Property
  End Class
End Namespace
