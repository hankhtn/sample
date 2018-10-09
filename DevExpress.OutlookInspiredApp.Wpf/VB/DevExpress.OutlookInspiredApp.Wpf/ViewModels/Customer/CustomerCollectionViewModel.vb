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
  ''' Represents the Customers collection view model.
  ''' </summary>
  Public Partial Class CustomerCollectionViewModel
    Inherits CollectionViewModel(Of Customer, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of CustomerCollectionViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing, Optional ByVal unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual) As CustomerCollectionViewModel
      Return ViewModelSource.Create(Function() New CustomerCollectionViewModel(unitOfWorkFactory, unitOfWorkPolicy))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the CustomerCollectionViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the CustomerCollectionViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing, Optional ByVal unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Customers, unitOfWorkPolicy := unitOfWorkPolicy)
    End Sub
  End Class
End Namespace
