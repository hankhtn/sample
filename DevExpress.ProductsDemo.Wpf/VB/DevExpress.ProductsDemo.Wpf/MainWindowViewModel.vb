Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media.Imaging
Imports DevExpress.Images
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI
Imports DevExpress.Utils
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports ProductsDemo.Modules
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.DevAV

Namespace ProductsDemo
    Public Class MainWindowViewModel

        Public Sub New()
            SplashScreenType = GetType(SplashScreenWindow)
            Modules = New List(Of ModuleInfo)() From {ViewModelSource.Create(Function() New ModuleInfo("GridTasksModule", Me, "Grid: Tasks")).SetIcon("GridTasks"), ViewModelSource.Create(Function() New ModuleInfo("GridContactsModule", Me, "Grid: Contacts")).SetIcon("GridContacts"), ViewModelSource.Create(Function() New ModuleInfo("SpreadsheetModule", Me, "Spreadsheet")).SetIcon("Spreadsheet"), ViewModelSource.Create(Function() New ModuleInfo("RichEditModule", Me, "Word Processing")).SetIcon("WordProcessing"), ViewModelSource.Create(Function() New ModuleInfo("ReportsModule", Me, "Banded Reports")).SetIcon("BandedReports"), ViewModelSource.Create(Function() New ModuleInfo("PivotGridModule", Me, "Pivot Table")).SetIcon("Pivot"), ViewModelSource.Create(Function() New ModuleInfo("SalesDashboard", Me, "Analytics")).SetIcon("Analytics"), ViewModelSource.Create(Function() New ModuleInfo("PhotoGallery", Me, "Photo Gallery Map")).SetIcon("WeatherMap"), ViewModelSource.Create(Function() New ModuleInfo("SchedulerModule", Me, "Scheduler")).SetIcon("Scheduler"), ViewModelSource.Create(Function() New ModuleInfo("PdfViewerDemo", Me, "Pdf Viewer")).SetIcon("PDFViewer")}
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of PrefixEnumWithExternalMetadata)()
        End Sub
        Public Overridable Property Modules() As IEnumerable(Of ModuleInfo)

        Public Overridable Property SelectedModuleInfo() As ModuleInfo
        Public Overridable Property SplashScreenType() As Type
        Public Overridable Property DefaultBackstatgeIndex() As Integer
        Public Overridable Property HasPrinting() As Boolean
        Public Overridable Property IsBackstageOpen() As Boolean
        Public Sub [Exit]()
            CurrentWindowService.Close()
        End Sub
        Public Sub OnModulesLoaded()
            If SelectedModuleInfo Is Nothing Then
                SelectedModuleInfo = Modules.First()
                SelectedModuleInfo.IsSelected = True
                SelectedModuleInfo.Show()
            End If
            SplashScreenType = GetType(ProgressWindow)
            ApplicationJumpListService.Items.AddOrReplace("New Task", NewTaskIcon, AddressOf ShowGridTasksModuleNewItemWindow)
            ApplicationJumpListService.Items.AddOrReplace("New Contact", NewContactIcon, AddressOf ShowGridContactsModuleNewItemWindow)
            ApplicationJumpListService.Apply()
        End Sub
        <Required>
        Protected Overridable ReadOnly Property CurrentWindowService() As ICurrentWindowService
            Get
                Return Nothing
            End Get
        End Property
        <Required>
        Protected Overridable ReadOnly Property ApplicationJumpListService() As IApplicationJumpListService
            Get
                Return Nothing
            End Get
        End Property
        <Required>
        Protected Overridable ReadOnly Property NavigationService() As INavigationService
            Get
                Return Nothing
            End Get
        End Property
        Protected Overridable Sub OnSelectedModuleInfoChanged()
            If Not allowSelectedModuleInfoChanged Then
                Return
            End If
            PrintingService.PrintableControlLink = Nothing
            SelectedModuleInfo.IsSelected = True
            SelectedModuleInfo.Show()
        End Sub
        Protected Overridable Sub OnIsBackstageOpenChanged()
            HasPrinting = PrintingService.HasPrinting
            If (Not HasPrinting) AndAlso DefaultBackstatgeIndex = 1 Then
                DefaultBackstatgeIndex = 0
            End If
        End Sub
        Private ReadOnly Property NewTaskIcon() As BitmapImage
            Get
                Return New BitmapImage(AssemblyHelper.GetResourceUri(GetType(DXImages).Assembly, "Images/Tasks/NewTask_16x16.png"))
            End Get
        End Property
        Private ReadOnly Property NewContactIcon() As BitmapImage
            Get
                Return New BitmapImage(AssemblyHelper.GetResourceUri(GetType(DXImages).Assembly, "Images/Mail/NewContact_16x16.png"))
            End Get
        End Property
        Private allowSelectedModuleInfoChanged As Boolean = True
        Private Sub ShowGridModuleNewItemWindow(Of T As Class)(ByVal moduleType As String)
            If Application.Current.Windows.Count <> 1 Then
                Return
            End If
            Dim viewModel As GridViewModelBase(Of T) = TryCast(ViewHelper.GetViewModelFromView(NavigationService.Current), GridViewModelBase(Of T))
            If viewModel IsNot Nothing Then
                viewModel.ShowNewItemWindow()
            Else
                Dim moduleInfo = Modules.Single(Function(m) m.Type = moduleType)
                allowSelectedModuleInfoChanged = False
                SelectedModuleInfo = moduleInfo
                allowSelectedModuleInfoChanged = True
                moduleInfo.Show(GridModuleNavigationParameter.NewItem)
            End If
        End Sub
        Private Sub ShowGridTasksModuleNewItemWindow()
            ShowGridModuleNewItemWindow(Of Task)("GridTasksModule")
        End Sub
        Private Sub ShowGridContactsModuleNewItemWindow()
            ShowGridModuleNewItemWindow(Of Contact)("GridContactsModule")
        End Sub
    End Class
    Public Class ModuleInfo
        Private parent As ISupportServices

        Public Sub New(ByVal _type As String, ByVal parent As Object, ByVal _title As String)
            Type = _type
            Me.parent = DirectCast(parent, ISupportServices)
            Title = _title
        End Sub
        Private privateType As String
        Public Property Type() As String
            Get
                Return privateType
            End Get
            Private Set(ByVal value As String)
                privateType = value
            End Set
        End Property
        Public Overridable Property IsSelected() As Boolean
        Private privateTitle As String
        Public Property Title() As String
            Get
                Return privateTitle
            End Get
            Private Set(ByVal value As String)
                privateTitle = value
            End Set
        End Property
        Public Overridable Property Icon() As Uri
        Public Function SetIcon(ByVal icon As String) As ModuleInfo
            Me.Icon = AssemblyHelper.GetResourceUri(GetType(ModuleInfo).Assembly, String.Format("Images/{0}.png", icon))
            Return Me
        End Function
        Public Sub Show(Optional ByVal parameter As Object = Nothing)
            Dim navigationService As INavigationService = parent.ServiceContainer.GetRequiredService(Of INavigationService)()
            navigationService.Navigate(Type, parameter, parent)
        End Sub
    End Class
    Public Class PrefixEnumWithExternalMetadata
        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of PersonPrefix))
            builder.Member(PersonPrefix.Dr).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Doctor.png").EndMember().Member(PersonPrefix.Mr).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Mr.png").EndMember().Member(PersonPrefix.Ms).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Ms.png").EndMember().Member(PersonPrefix.Miss).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Miss.png").EndMember().Member(PersonPrefix.Mrs).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Mrs.png").EndMember()
        End Sub
    End Class
End Namespace
