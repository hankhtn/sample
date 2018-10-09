Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/DataBinding/BindingUsingSeriesTemplate.xaml"), CodeFile("Modules/DataBinding/BindingUsingSeriesTemplate.xaml.(cs)"), CodeFile("ViewModels/BindingUsingSeriesTemplateViewModel.(cs)"), CodeFile("ViewModels/G7Data.(cs)"), CodeFile("ViewModels/IChartAnimationService.(cs)"), CodeFile("Utils/ChartAnimationService.(cs)")>
    Partial Public Class BindingUsingSeriesTemplate
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
