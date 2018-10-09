Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Utils.Filtering
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.LayoutControl

Namespace CommonChartsDemo
    <CodeFile("Modules/DataAnalysis/SalesOverview.xaml"), CodeFile("Modules/DataAnalysis/SalesOverview.xaml.(cs)"), CodeFile("ViewModels/SalesProductData.(cs)")>
    Partial Public Class SalesOverview
        Inherits CommonChartsDemoModule


        Private seriesBrushes_Renamed As Dictionary(Of String, SolidColorBrush)
        Private ReadOnly Property SeriesBrushes() As Dictionary(Of String, SolidColorBrush)
            Get
                If seriesBrushes_Renamed Is Nothing Then
                    seriesBrushes_Renamed = CreateBrushes()
                End If
                Return seriesBrushes_Renamed
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            DataContext = New BicyclesDataViewModel(SeriesBrushes)
        End Sub

        Private Function CreateBrushes() As Dictionary(Of String, SolidColorBrush)
            Dim result As New Dictionary(Of String, SolidColorBrush)()
            For i As Integer = 0 To SalesProductData.BikeCategories.Count - 1
                result.Add(SalesProductData.BikeCategories(i), New SolidColorBrush(chart.Palette(i)))
            Next i
            Return result
        End Function
        Private Sub DataLayoutControl_AutoGeneratingItem(ByVal sender As Object, ByVal e As DataLayoutControlAutoGeneratingItemEventArgs)
            Dim group As LayoutGroup = TryCast(e.Item.Content, LayoutGroup)
            If group IsNot Nothing Then
                BarManager.SetDXContextMenu(group, Nothing)
                If e.PropertyName = "ReportDate" Then
                    e.Item.Visibility = Visibility.Hidden
                End If
                If e.PropertyName = "Category" Then
                    Dim edit As ListBoxEdit = TryCast(group.Children(1), ListBoxEdit)
                    edit.ItemTemplate = TryCast(Resources("CategoryItemTemplate"), DataTemplate)
                End If
            End If
        End Sub
        Private Sub chart_BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each series As Series In chart.Diagram.Series
                CType(series, XYSeries).Brush = SeriesBrushes(series.DisplayName)
            Next series
        End Sub
    End Class

    Public Class BicyclesSaleFilteringViewModel
        <FilterLookup("Categories", UseBlanks := False, UseSelectAll := False, ValueMember := "Text"), Display(Name := "Bicycles Category")>
        Public Property Category() As String
        <FilterRange("MinRevenue", "MaxRevenue"), Display(Name := "Weekly Revenue, USD"), DataType(DataType.Currency)>
        Public Property Revenue() As Decimal
        <Display(Name := "Units Sold"), FilterRange("MinSold", "MaxSold", EditorType := RangeUIEditorType.Range)>
        Public Property UnitsSold() As Integer
        <FilterRange("StartSelectionDate", "EndSelectionDate"), Display(Name := "Report Date")>
        Public Property ReportDate() As Date
    End Class

    <POCOViewModel>
    Public Class BicyclesDataViewModel
        Public Class CategoryInfo
            Public Property Text() As String
            Public Property Brush() As SolidColorBrush
        End Class
        Public Overridable Property Categories() As IEnumerable(Of CategoryInfo)
        Public Overridable Property Data() As List(Of BikeReportItem)
        Public Overridable Property RangeData() As List(Of BikeReportRangeItem)
        Public Overridable Property MinRevenue() As Decimal
        Public Overridable Property MaxRevenue() As Decimal
        Public Overridable Property MinSold() As Integer
        Public Overridable Property MaxSold() As Integer
        Public Overridable Property EndSelectionDate() As Date
        Public Overridable Property StartSelectionDate() As Date

        Public Sub New(ByVal seriesBrushes As Dictionary(Of String, SolidColorBrush))

            Dim data_Renamed As List(Of BikeReportItem) = SalesProductData.BicyclesReport.BicyclesData
            Me.Data = data_Renamed
            Me.Categories = seriesBrushes.Select(Function(item) New CategoryInfo() With {.Text = item.Key, .Brush = item.Value})
            Me.MinSold = data_Renamed.Min(Function(x) x.UnitsSold)
            Me.MaxSold = data_Renamed.Max(Function(x) x.UnitsSold)
            Me.MinRevenue = data_Renamed.Min(Function(x) x.Revenue)
            Me.MaxRevenue = data_Renamed.Max(Function(x) x.Revenue)
            Me.RangeData = SalesProductData.BicyclesReport.BicycleRangesData
            Me.StartSelectionDate = data_Renamed.Min(Function(x) x.ReportDate)
            Me.EndSelectionDate = data_Renamed.Max(Function(x) x.ReportDate)
        End Sub
    End Class
End Namespace
