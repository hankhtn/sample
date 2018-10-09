Imports System
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports System.Windows
Imports System.Windows.Media.Animation
Imports DevExpress.Images
Imports DevExpress.Internal
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors

Namespace DevExpress.DevAV
    Partial Public Class App
        Inherits Application

        Private Shared singleInstanceApplicationGuard As IDisposable

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            ExceptionHelper.Initialize()
            ClearAutomationEventsHelper.IsEnabled = False
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf OnCurrentDomainAssemblyResolve
            DevAVDataDirectoryHelper.LocalPrefix = "WpfHybridApp"
            ImagesAssemblyLoader.Load()
            Timeline.DesiredFrameRateProperty.OverrideMetadata(GetType(Timeline), New FrameworkPropertyMetadata(200))
            LoadPlugins()
            MyBase.OnStartup(e)
            ViewLocator.Default = New ViewLocator(GetType(App).Assembly)
            Dim [exit] As Boolean = Nothing
            singleInstanceApplicationGuard = DevAVDataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfHybridApp", [exit])
            If [exit] Then
                Shutdown()
                Return
            End If
            ApplicationThemeHelper.ApplicationThemeName = Theme.HybridApp.Name
            SetCultureInfo()
            SetLocalization()
        End Sub
        Private Shared Sub SetCultureInfo()
            Dim demoCI As CultureInfo = DirectCast(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
            demoCI.NumberFormat.CurrencySymbol = "$"
            demoCI.DateTimeFormat = New DateTimeFormatInfo()
            Thread.CurrentThread.CurrentCulture = demoCI
            Dim demoUI As CultureInfo = DirectCast(Thread.CurrentThread.CurrentUICulture.Clone(), CultureInfo)
            demoUI.NumberFormat.CurrencySymbol = "$"
            demoUI.DateTimeFormat = New DateTimeFormatInfo()
            Thread.CurrentThread.CurrentUICulture = demoUI
        End Sub
        Private Shared Sub SetLocalization()
            EditorLocalizer.Active = New HybridAppEditorLocalizer()
        End Sub
        Private Shared Function OnCurrentDomainAssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As System.Reflection.Assembly
            Dim partialName As String = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower()
            If partialName = "entityframework" OrElse partialName = "system.data.sqlite" OrElse partialName = "system.data.sqlite.ef6" Then
                Dim path As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(GetType(App).Assembly.Location), partialName & ".dll")
                Return System.Reflection.Assembly.LoadFrom(path)
            End If
            Return Nothing
        End Function
        #Region "LoadPlugins"
        Private Shared Sub LoadPlugins()
            For Each file As String In Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "DevExpress.HybridApp.Wpf.Plugins.*.exe")
                System.Reflection.Assembly.LoadFrom(file).With(Function(x) x.GetType("Global.Program")).With(Function(x) x.GetMethod("Start", BindingFlags.Static Or BindingFlags.Public, Nothing, New Type() { }, Nothing)).Do(Function(x) x.Invoke(Nothing, New Object() { }))
            Next file
        End Sub
        #End Region
    End Class
    Public Class HybridAppEditorLocalizer
        Inherits EditorLocalizer

        Protected Overrides Sub PopulateStringTable()
            MyBase.PopulateStringTable()
            AddString(EditorStringId.LookUpSearch, "Enter text to search...")
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
                Dim app = New DevExpress.DevAV.App()
                app.InitializeComponent()
                GetType(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic).SetValue(app, Nothing)
                AddHandler app.Startup, Sub(s, e)
                    Dim window = New DevExpress.DevAV.MainWindow()
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
