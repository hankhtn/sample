Imports DevExpress.Mvvm.ViewModel
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.DevAV.Common
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.POCO
Namespace DevExpress.DevAV.ViewModels
  ''' <summary>
  ''' Represents the single EmployeeTask object view model.
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
  End Class
End Namespace
