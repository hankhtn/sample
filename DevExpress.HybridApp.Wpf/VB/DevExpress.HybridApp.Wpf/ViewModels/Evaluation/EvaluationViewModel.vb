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
  ''' Represents the single Evaluation object view model.
  ''' </summary>
  Public Partial Class EvaluationViewModel
    Inherits SingleObjectViewModel(Of Evaluation, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of EvaluationViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EvaluationViewModel
      Return ViewModelSource.Create(Function() New EvaluationViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the EvaluationViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the EvaluationViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Evaluations, Function(ByVal x) x.Subject)
    End Sub    
    ''' <summary>
    ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpEmployees As IEntitiesViewModel(Of Employee)
      Get
        Return GetLookUpEntitiesViewModel(Of EvaluationViewModel, Employee, long)(propertyExpression := Function(ByVal x As EvaluationViewModel) x.LookUpEmployees, getRepositoryFunc := Function(ByVal x) x.Employees)
      End Get
    End Property
  End Class
End Namespace
