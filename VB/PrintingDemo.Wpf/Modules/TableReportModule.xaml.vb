Imports System
Imports System.Linq
Imports System.IO
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Demos.DataSources
Imports DevExpress.Utils
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraPrinting
Imports CreateAreaEventArgs = DevExpress.Xpf.Printing.CreateAreaEventArgs

Namespace PrintingDemo
    Partial Public Class TableReportModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()

            Dim resources = TryCast(Me.Resources("detailTextStyle"), Style)

            Dim borderDashStyleBinding As New Binding()
            borderDashStyleBinding.Source = BorderStyleService.Instance
            Dim path As New PropertyPath(BorderStyleService.BorderDashStyleProperty)
            borderDashStyleBinding.Path = path

            resources.Setters.Add(New Setter(ExportSettings.BorderDashStyleProperty, borderDashStyleBinding))
        End Sub
    End Class

    Public Class BorderStyleService
        Inherits DependencyObject

        Public Property BorderDashStyle() As BorderDashStyle
            Get
                Return DirectCast(GetValue(BorderDashStyleProperty), BorderDashStyle)
            End Get
            Set(ByVal value As BorderDashStyle)
                SetValue(BorderDashStyleProperty, value)
            End Set
        End Property
        Public Shared ReadOnly BorderDashStyleProperty As DependencyProperty = DependencyProperty.Register("BorderDashStyle", GetType(BorderDashStyle), GetType(BorderStyleService), New PropertyMetadata(BorderDashStyle.Solid))


        Private ReadOnly Shared instance_Renamed As New BorderStyleService()
        Public Shared ReadOnly Property Instance() As BorderStyleService
            Get
                Return instance_Renamed
            End Get
        End Property
    End Class

    Public Class TableReportModuleViewModel
        Inherits ModuleViewModelBase


        Private ReadOnly fishes_Renamed As New Lazy(Of Fishes)(AddressOf GetFishes)
        Private ReadOnly Property Fishes() As Fishes
            Get
                Return fishes_Renamed.Value
            End Get
        End Property

        Private ReadOnly borderDashStyles As New Lazy(Of Array)(AddressOf GetBorderDashStyles)
        Public ReadOnly Property BorderDashStyleValues() As Array
            Get
                Return borderDashStyles.Value
            End Get
        End Property

        Public Property BorderDashStyle() As BorderDashStyle
            Get
                Return BorderStyleService.Instance.BorderDashStyle
            End Get
            Set(ByVal value As BorderDashStyle)
                If System.Enum.Equals(BorderStyleService.Instance.BorderDashStyle, value) Then
                    Return
                End If
                BorderStyleService.Instance.BorderDashStyle = value
                RaisePropertyChanged("BorderDashStyle")
                CreateDocument()
            End Set
        End Property

        Protected Overrides Function CreateLink() As TemplatedLink

            Dim link_Renamed As New SimpleLink()
            link_Renamed.PageHeaderTemplate = PageHeaderTemplate
            link_Renamed.DetailTemplate = DetailTemplate
            link_Renamed.PageFooterTemplate = PageFooterTemplate
            link_Renamed.DetailCount = Me.Fishes.Count
            AddHandler link_Renamed.CreateDetail, AddressOf link_CreateDetail
            link_Renamed.DocumentName = "Fishes"
            Return link_Renamed

        End Function

        Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
            e.Data = Fishes(e.DetailIndex)
        End Sub

        Private Shared Function GetBorderDashStyles() As Array
            Return System.Enum.GetValues(GetType(BorderDashStyle)).Cast(Of BorderDashStyle)().Where(Function(x) x <> DevExpress.XtraPrinting.BorderDashStyle.Double).ToArray()
        End Function

        Private Shared Function GetFishes() As Fishes
            Dim stream As Stream = AssemblyHelper.GetResourceStream(GetType(TableReportModuleViewModel).Assembly, "Data/biolife.txt", True)
            Return New Fishes(stream)
        End Function
    End Class
End Namespace
