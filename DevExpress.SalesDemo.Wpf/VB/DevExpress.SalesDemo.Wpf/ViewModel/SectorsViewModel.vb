Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel
    Public Class SectorsViewModel
        Inherits PageViewModel

        Public Shared Function Create() As SectorsViewModel
            Return ViewModelSource.Create(Function() New SectorsViewModel())
        End Function
        Protected Sub New()
        End Sub

        Public Overridable Property DailySalesBySectorViewModel() As PerformanceBarChartViewModel
        Public Overridable Property UnitSalesBySectorViewModel() As PerformanceBarChartViewModel
        Public Overridable Property PieGaugeRangeViewModel() As PieBarRangeViewModel
        Protected Overrides Sub Initialize()
            DailySalesBySectorViewModel = PerformanceBarChartViewModel.Create(Modules.Sectors, PerformanceViewMode.Daily)

            UnitSalesBySectorViewModel = PerformanceBarChartViewModel.Create(Modules.Sectors, PerformanceViewMode.Monthly)

            PieGaugeRangeViewModel = PieBarRangeViewModel.Create(Modules.Sectors)
        End Sub
    End Class
End Namespace
