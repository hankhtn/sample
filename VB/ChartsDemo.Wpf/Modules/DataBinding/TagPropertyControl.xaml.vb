Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/DataBinding/TagPropertyControl.xaml"), CodeFile("Modules/DataBinding/TagPropertyControl.xaml.(cs)"), CodeFile("ViewModels/CountryData.(cs)")>
    Partial Public Class TagPropertyControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub chart_CustomDrawSeriesPoint(ByVal sender As Object, ByVal e As CustomDrawSeriesPointEventArgs)
            e.LegendText = CType(e.SeriesPoint.Tag, Country).OfficialName
        End Sub
    End Class
End Namespace
