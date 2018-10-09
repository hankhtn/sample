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
  ''' Represents the single Employee object view model.
  ''' </summary>
  Public Partial Class EmployeeViewModel
    Inherits SingleObjectViewModel(Of Employee, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of EmployeeViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeViewModel
      Return ViewModelSource.Create(Function() New EmployeeViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the EmployeeViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the EmployeeViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Employees, Function(ByVal x) x.FullName)
    End Sub
    Protected Overrides Sub RefreshLookUpCollections(ByVal raisePropertyChanged As Boolean)
      MyBase.RefreshLookUpCollections(raisePropertyChanged)
      AssignedEmployeeTasksDetailEntities = CreateAddRemoveDetailEntitiesViewModel(Function(ByVal x) x.Tasks, Function(ByVal x) x.AssignedEmployeeTasks)
    End Sub    
    ''' <summary>
    ''' The view model that contains a look-up collection of Tasks for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpTasks As IEntitiesViewModel(Of EmployeeTask)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeViewModel, EmployeeTask, long)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.LookUpTasks, getRepositoryFunc := Function(ByVal x) x.Tasks)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Pictures for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpPictures As IEntitiesViewModel(Of Picture)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeViewModel, Picture, long)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.LookUpPictures, getRepositoryFunc := Function(ByVal x) x.Pictures)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Probations for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpProbations As IEntitiesViewModel(Of Probation)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeViewModel, Probation, long)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.LookUpProbations, getRepositoryFunc := Function(ByVal x) x.Probations)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Communications for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpCommunications As IEntitiesViewModel(Of CustomerCommunication)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeViewModel, CustomerCommunication, long)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.LookUpCommunications, getRepositoryFunc := Function(ByVal x) x.Communications)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Orders for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpOrders As IEntitiesViewModel(Of Order)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeViewModel, Order, long)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.LookUpOrders, getRepositoryFunc := Function(ByVal x) x.Orders)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Products for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpProducts As IEntitiesViewModel(Of Product)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeViewModel, Product, long)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.LookUpProducts, getRepositoryFunc := Function(ByVal x) x.Products)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Quotes for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpQuotes As IEntitiesViewModel(Of Quote)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeViewModel, Quote, long)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.LookUpQuotes, getRepositoryFunc := Function(ByVal x) x.Quotes)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Evaluations for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpEvaluations As IEntitiesViewModel(Of Evaluation)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeViewModel, Evaluation, long)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.LookUpEvaluations, getRepositoryFunc := Function(ByVal x) x.Evaluations)
      End Get
    End Property
    Public Overridable Property AssignedEmployeeTasksDetailEntities As AddRemoveDetailEntitiesViewModel(Of Employee, Int64, EmployeeTask, Int64, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' The view model for the EmployeeOwnedTasks detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeOwnedTasksDetails As CollectionViewModelBase(Of EmployeeTask, EmployeeTask, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeViewModel, EmployeeTask, long, long?)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.EmployeeOwnedTasksDetails, getRepositoryFunc := Function(ByVal x) x.Tasks, foreignKeyExpression := Function(ByVal x) x.OwnerId, navigationExpression := Function(ByVal x) x.Owner)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeEmployees detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeEmployeesDetails As CollectionViewModelBase(Of CustomerCommunication, CustomerCommunication, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeViewModel, CustomerCommunication, long, long?)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.EmployeeEmployeesDetails, getRepositoryFunc := Function(ByVal x) x.Communications, foreignKeyExpression := Function(ByVal x) x.EmployeeId, navigationExpression := Function(ByVal x) x.Employee)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeOrders detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeOrdersDetails As CollectionViewModelBase(Of Order, Order, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeViewModel, Order, long, long?)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.EmployeeOrdersDetails, getRepositoryFunc := Function(ByVal x) x.Orders, foreignKeyExpression := Function(ByVal x) x.EmployeeId, navigationExpression := Function(ByVal x) x.Employee)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeProducts detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeProductsDetails As CollectionViewModelBase(Of Product, Product, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeViewModel, Product, long, long?)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.EmployeeProductsDetails, getRepositoryFunc := Function(ByVal x) x.Products, foreignKeyExpression := Function(ByVal x) x.EngineerId, navigationExpression := Function(ByVal x) x.Engineer)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeSupportedProducts detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeSupportedProductsDetails As CollectionViewModelBase(Of Product, Product, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeViewModel, Product, long, long?)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.EmployeeSupportedProductsDetails, getRepositoryFunc := Function(ByVal x) x.Products, foreignKeyExpression := Function(ByVal x) x.SupportId, navigationExpression := Function(ByVal x) x.Support)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeQuotes detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeQuotesDetails As CollectionViewModelBase(Of Quote, Quote, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeViewModel, Quote, long, long?)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.EmployeeQuotesDetails, getRepositoryFunc := Function(ByVal x) x.Quotes, foreignKeyExpression := Function(ByVal x) x.EmployeeId, navigationExpression := Function(ByVal x) x.Employee)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeEvaluationsCreatedBy detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeEvaluationsCreatedByDetails As CollectionViewModelBase(Of Evaluation, Evaluation, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeViewModel, Evaluation, long, long?)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.EmployeeEvaluationsCreatedByDetails, getRepositoryFunc := Function(ByVal x) x.Evaluations, foreignKeyExpression := Function(ByVal x) x.CreatedById, navigationExpression := Function(ByVal x) x.CreatedBy)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeEvaluations detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeEvaluationsDetails As CollectionViewModelBase(Of Evaluation, Evaluation, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeViewModel, Evaluation, long, long?)(propertyExpression := Function(ByVal x As EmployeeViewModel) x.EmployeeEvaluationsDetails, getRepositoryFunc := Function(ByVal x) x.Evaluations, foreignKeyExpression := Function(ByVal x) x.EmployeeId, navigationExpression := Function(ByVal x) x.Employee)
      End Get
    End Property
  End Class
End Namespace
