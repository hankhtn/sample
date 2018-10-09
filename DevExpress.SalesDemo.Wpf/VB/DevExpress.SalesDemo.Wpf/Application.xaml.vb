Imports System
Imports System.Windows
Imports DevExpress.Mvvm.ModuleInjection
Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.SalesDemo.Model
Imports DevExpress.SalesDemo.Wpf.Helpers
Imports DevExpress.SalesDemo.Wpf.View
Imports DevExpress.SalesDemo.Wpf.View.Common
Imports DevExpress.SalesDemo.Wpf.ViewModel
Imports DevExpress.Xpf.Core

Namespace DevExpress.SalesDemo.Wpf
    Partial Public Class App
        Inherits Application

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            ExceptionHelper.Initialize()
            ViewModelLocator.Default = New ViewModelLocator(GetType(App).Assembly)
            ViewLocator.Default = New ViewLocator(GetType(App).Assembly)
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013Name
            Using DataSource.Progress.Subscribe(New DataGenerationProgress())
                DataSource.EnsureDataProvider()
            End Using
            MyBase.OnStartup(e)
            InitModules()
        End Sub
        Private Shared Sub InitModules()
            ModuleManager.DefaultManager.Register(Regions.Navigation, New Mvvm.ModuleInjection.Module(Modules.Dashboard, Function() NavigationItemViewModel.Create(Modules.Dashboard, "Sales", "Revenue" & Environment.NewLine & "Snapshots", ResourceImageHelper.GetResourceImage("Sales.png")), GetType(NavigationItemView)))
            ModuleManager.DefaultManager.Register(Regions.Navigation, New Mvvm.ModuleInjection.Module(Modules.Products, Function() NavigationItemViewModel.Create(Modules.Products, "Products", "Revenue" & Environment.NewLine & "by Products", ResourceImageHelper.GetResourceImage("Products.png")), GetType(NavigationItemView)))
            ModuleManager.DefaultManager.Register(Regions.Navigation, New Mvvm.ModuleInjection.Module(Modules.Sectors, Function() NavigationItemViewModel.Create(Modules.Sectors, "Sectors", "Revenue" & Environment.NewLine & "by Sectors", ResourceImageHelper.GetResourceImage("Sectors.png")), GetType(NavigationItemView)))
            ModuleManager.DefaultManager.Register(Regions.Navigation, New Mvvm.ModuleInjection.Module(Modules.Regions, Function() NavigationItemViewModel.Create(Modules.Regions, "Regions", "Revenue" & Environment.NewLine & "by Regions", ResourceImageHelper.GetResourceImage("Regions.png")), GetType(NavigationItemView)))
            ModuleManager.DefaultManager.Register(Regions.Navigation, New Mvvm.ModuleInjection.Module(Modules.Channels, Function() NavigationItemViewModel.Create(Modules.Channels, "Channels", "Revenue" & Environment.NewLine & "by Sales Channels", ResourceImageHelper.GetResourceImage("Channels.png")), GetType(NavigationItemView)))

            ModuleManager.DefaultManager.Register(Regions.Main, New Mvvm.ModuleInjection.Module(Modules.Dashboard, Function() SalesDashboardViewModel.Create(), GetType(SalesDashboard)))
            ModuleManager.DefaultManager.Register(Regions.Main, New Mvvm.ModuleInjection.Module(Modules.Products, Function() ProductsViewModel.Create(), GetType(ProductsView)))
            ModuleManager.DefaultManager.Register(Regions.Main, New Mvvm.ModuleInjection.Module(Modules.Sectors, Function() SectorsViewModel.Create(), GetType(SectorsView)))
            ModuleManager.DefaultManager.Register(Regions.Main, New Mvvm.ModuleInjection.Module(Modules.Regions, Function() RegionsViewModel.Create(), GetType(RegionsView)))
            ModuleManager.DefaultManager.Register(Regions.Main, New Mvvm.ModuleInjection.Module(Modules.Channels, Function() ChannelsViewModel.Create(), GetType(ChannelsView)))

            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Dashboard)
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Products)
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Sectors)
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Regions)
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Channels)

            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Dashboard)
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Products)
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Sectors)
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Regions)
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Channels)

            ModuleManager.DefaultManager.Navigate(Regions.Navigation, Modules.Dashboard)
        End Sub
    End Class
    Friend Class DataGenerationProgress
        Implements IObserver(Of Integer)

        Private Sub IObserverGeneric_OnCompleted() Implements IObserver(Of Integer).OnCompleted
            DXSplashScreen.Close()
        End Sub
        Private Sub IObserverGeneric_OnNext(ByVal value As Integer) Implements IObserver(Of Integer).OnNext
            If Not DXSplashScreen.IsActive Then
                DXSplashScreen.Show(GetType(ProgressSplashScreenView))
                DXSplashScreen.SetState(ProgressSplashScreenViewModel.Create("Generating Sales...", AssemblyInfo.AssemblyCopyright))
            End If
            DXSplashScreen.Progress(CDbl(value))
        End Sub
        Private Sub IObserverGeneric_OnError(ByVal [error] As Exception) Implements IObserver(Of Integer).OnError
            Throw [error]
        End Sub
    End Class
End Namespace
#If CLICKONCE Then
Namespace DevExpress.Internal.DemoLauncher
    Public NotInheritable Class ClickOnceEntryPoint

        Private Sub New()
        End Sub

        Public Shared Function Run() As Tuple(Of Action, System.Threading.Tasks.Task(Of Window))
            Dim done = New System.Threading.Tasks.TaskCompletionSource(Of Window)()

            Dim run_Renamed As Action = Sub()
                Dim app = New DevExpress.SalesDemo.Wpf.App()
                app.InitializeComponent()
                GetType(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic).SetValue(app, Nothing)
                AddHandler app.Startup, Sub(s, e)
                    Dim window = New DevExpress.SalesDemo.Wpf.MainWindow()
                    window.Show()
                    app.MainWindow = window
                    done.SetResult(window)
                End Sub
                app.Run()
            End Sub
            Return Tuple.Create(run_Renamed, done.Task)
        End Function
    End Class
End Namespace
#End If
