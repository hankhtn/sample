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
  ''' Represents the Quotes collection view model.
  ''' </summary>
  Public Partial Class QuoteCollectionViewModel
    Inherits CollectionViewModel(Of Quote, QuoteInfo, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of QuoteCollectionViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing, Optional ByVal unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual) As QuoteCollectionViewModel
      Return ViewModelSource.Create(Function() New QuoteCollectionViewModel(unitOfWorkFactory, unitOfWorkPolicy))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the QuoteCollectionViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the QuoteCollectionViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing, Optional ByVal unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Quotes, Function(ByVal x) QueriesHelper.GetQuoteInfo(x))
    End Sub
  End Class
End Namespace
