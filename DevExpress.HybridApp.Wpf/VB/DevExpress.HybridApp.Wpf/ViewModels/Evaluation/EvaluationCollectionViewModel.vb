Imports System
Imports System.Linq
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common
Imports DevExpress.DevAV
Namespace DevExpress.DevAV.ViewModels
  ''' <summary>
  ''' Represents the Evaluations collection view model.
  ''' </summary>
  Public Partial Class EvaluationCollectionViewModel
    Inherits CollectionViewModel(Of Evaluation, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of EvaluationCollectionViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing, Optional ByVal unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual) As EvaluationCollectionViewModel
      Return ViewModelSource.Create(Function() New EvaluationCollectionViewModel(unitOfWorkFactory, unitOfWorkPolicy))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the EvaluationCollectionViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the EvaluationCollectionViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing, Optional ByVal unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Evaluations, unitOfWorkPolicy := unitOfWorkPolicy)
    End Sub
  End Class
End Namespace
