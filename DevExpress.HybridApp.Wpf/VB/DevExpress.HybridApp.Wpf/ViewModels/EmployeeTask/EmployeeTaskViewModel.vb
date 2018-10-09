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
  ''' Represents the single EmployeeTask object view model.
  ''' </summary>
  Public Partial Class EmployeeTaskViewModel
    Inherits SingleObjectViewModel(Of EmployeeTask, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of EmployeeTaskViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeTaskViewModel
      Return ViewModelSource.Create(Function() New EmployeeTaskViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the EmployeeTaskViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the EmployeeTaskViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Tasks, Function(ByVal x) x.Subject)
    End Sub
    Protected Overrides Sub RefreshLookUpCollections(ByVal raisePropertyChanged As Boolean)
      MyBase.RefreshLookUpCollections(raisePropertyChanged)
      AssignedEmployeesDetailEntities = CreateAddRemoveDetailEntitiesViewModel(Function(ByVal x) x.Employees, Function(ByVal x) x.AssignedEmployees)
    End Sub    
    ''' <summary>
    ''' The view model that contains a look-up collection of AttachedFiles for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpAttachedFiles As IEntitiesViewModel(Of TaskAttachedFile)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeTaskViewModel, TaskAttachedFile, long)(propertyExpression := Function(ByVal x As EmployeeTaskViewModel) x.LookUpAttachedFiles, getRepositoryFunc := Function(ByVal x) x.AttachedFiles)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpEmployees As IEntitiesViewModel(Of Employee)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeTaskViewModel, Employee, long)(propertyExpression := Function(ByVal x As EmployeeTaskViewModel) x.LookUpEmployees, getRepositoryFunc := Function(ByVal x) x.Employees)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of CustomerEmployees for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpCustomerEmployees As IEntitiesViewModel(Of CustomerEmployee)
      Get
        Return GetLookUpEntitiesViewModel(Of EmployeeTaskViewModel, CustomerEmployee, long)(propertyExpression := Function(ByVal x As EmployeeTaskViewModel) x.LookUpCustomerEmployees, getRepositoryFunc := Function(ByVal x) x.CustomerEmployees)
      End Get
    End Property
    Public Overridable Property AssignedEmployeesDetailEntities As AddRemoveDetailEntitiesViewModel(Of EmployeeTask, Int64, Employee, Int64, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' The view model for the EmployeeTaskAttachedFiles detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeTaskAttachedFilesDetails As CollectionViewModelBase(Of TaskAttachedFile, TaskAttachedFile, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Of EmployeeTaskViewModel, TaskAttachedFile, long, long?)(propertyExpression := Function(ByVal x As EmployeeTaskViewModel) x.EmployeeTaskAttachedFilesDetails, getRepositoryFunc := Function(ByVal x) x.AttachedFiles, foreignKeyExpression := Function(ByVal x) x.EmployeeTaskId, navigationExpression := Function(ByVal x) x.EmployeeTask)
      End Get
    End Property
  End Class
End Namespace
