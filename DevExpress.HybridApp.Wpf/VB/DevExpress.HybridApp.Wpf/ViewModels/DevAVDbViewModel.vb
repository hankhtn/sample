Imports System
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel
Namespace DevExpress.DevAV.ViewModels
  Public Partial Class DevAVDbViewModel
    Inherits DocumentsViewModel(Of DevAVDbModuleDescription, IDevAVDbUnitOfWork)
    Private Const _MyWorldGroup As String = "My World"
    Private Const _OperationsGroup As String = "Operations"
    Public Sub New()
      MyBase.New(UnitOfWorkSource.GetUnitOfWorkFactory())
      IsTablet = True
    End Sub
    Protected Overrides Function CreateModules() As DevAVDbModuleDescription()
      Dim modules = New DevAVDbModuleDescription() { New DevAVDbModuleDescription("Dashboard", "DashboardView", _MyWorldGroup, FiltersSettings.GetDashboardFilterTree(Me)),New DevAVDbModuleDescription("Tasks", "TaskCollectionView", _MyWorldGroup, FiltersSettings.GetTasksFilterTree(Me)),New DevAVDbModuleDescription("Employees", "EmployeeCollectionView", _MyWorldGroup, FiltersSettings.GetEmployeesFilterTree(Me)),New DevAVDbModuleDescription("Products", "ProductCollectionView", _OperationsGroup, FiltersSettings.GetProductsFilterTree(Me)),New DevAVDbModuleDescription("Customers", "CustomerCollectionView", _OperationsGroup, FiltersSettings.GetCustomersFilterTree(Me)),New DevAVDbModuleDescription("Sales", "OrderCollectionView", _OperationsGroup, FiltersSettings.GetSalesFilterTree(Me)),New DevAVDbModuleDescription("Opportunities", "QuoteCollectionView", _OperationsGroup, FiltersSettings.GetOpportunitiesFilterTree(Me)) }
      For Each [module] In modules
        Dim moduleRef As DevAVDbModuleDescription = [module]
        [module].FilterTreeViewModel.NavigateAction = (Sub()
          Show(moduleRef)
        End Sub)
      Next
      Return modules
    End Function
    Protected Overrides Sub OnActiveModuleChanged(ByVal oldModule As DevAVDbModuleDescription)
      MyBase.OnActiveModuleChanged(oldModule)
      If ActiveModule Is Nothing Then
        Return
      End If
      If ActiveModule.FilterTreeViewModel IsNot Nothing Then
        ActiveModule.FilterTreeViewModel.SetViewModel(DocumentManagerService.ActiveDocument.Content)
      End If
      Xpf.DemoBase.Helpers.Logger.Log(String.Format("HybridApp: Select module: {0}", ActiveModule.ModuleTitle.ToUpper()))
    End Sub
    Protected Overrides Function GetModuleTitle(ByVal [module] As DevAVDbModuleDescription) As String
      Return MyBase.GetModuleTitle([module]) + " - DevAV"
    End Function
    Public Overrides Sub OnLoaded(ByVal [module] As DevAVDbModuleDescription)
      MyBase.OnLoaded([module])
      IsTablet = DeviceDetector.IsTablet
      RegisterJumpList()
    End Sub
    Public Overrides Sub SaveLogicalLayout()
    End Sub
    Public Overrides Function RestoreLogicalLayout() As Boolean
      Return False
    End Function
    Private _linksViewModel As LinksViewModel
    Public ReadOnly Property LinksViewModel As LinksViewModel
      Get
        If _linksViewModel Is Nothing Then
          _linksViewModel = LinksViewModel.Create()
        End If
        Return _linksViewModel
      End Get
    End Property
    Public Overrides ReadOnly Property DefaultModule As DevAVDbModuleDescription
      Get
        Return Modules(2)
      End Get
    End Property
    Public Overridable Property IsTablet As Boolean
    Private Sub RegisterJumpList()
      Dim jumpListService As IApplicationJumpListService = Me.GetService(Of IApplicationJumpListService)()
      jumpListService.Items.AddOrReplace("Become a UI Superhero", "Explore DevExpress Universal", UniversalIcon, Sub() LinksViewModel.UniversalSubscription())
      jumpListService.Items.AddOrReplace("Online Tutorials", "Explore DevExpress Universal", TutorialsIcon, Sub() LinksViewModel.GettingStarted())
      jumpListService.Items.AddOrReplace("Buy Now", "Explore DevExpress Universal", BuyIcon, Sub() LinksViewModel.BuyNow())
      jumpListService.Apply()
    End Sub
    Private ReadOnly Property UniversalIcon As ImageSource
      Get
        Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Universal.ico"))
      End Get
    End Property
    Private ReadOnly Property TutorialsIcon As ImageSource
      Get
        Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/WPF.ico"))
      End Get
    End Property
    Private ReadOnly Property BuyIcon As ImageSource
      Get
        Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/DevExpress.ico"))
      End Get
    End Property
  End Class
  Public Partial Class DevAVDbModuleDescription
    Inherits ModuleDescription(Of DevAVDbModuleDescription)
    Private _FilterTreeViewModel As IFilterTreeViewModel
    Private _ImageSource As Uri
    Public Sub New(ByVal title As String, ByVal documentType As String, ByVal group As String, ByVal filterTreeViewModel As IFilterTreeViewModel)
      MyBase.New(title, documentType, group, Nothing)
      _ImageSource = New Uri(String.Format("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Menu/{0}.png", title))
      Me._FilterTreeViewModel = filterTreeViewModel
    End Sub
    Public ReadOnly Property ImageSource As Uri
      Get
        Return _ImageSource
      End Get
    End Property
    Public ReadOnly Property FilterTreeViewModel As IFilterTreeViewModel
      Get
        Return _FilterTreeViewModel
      End Get
    End Property
  End Class
End Namespace
