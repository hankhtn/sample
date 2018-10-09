Imports System
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace CommonChartsDemo
    <CodeFile("Modules/DataBinding/DataGridCharting.xaml"), CodeFile("Modules/DataBinding/DataGridCharting.xaml.(cs)"), CodeFile("ViewModels/SalesProductData.(cs)")>
    Partial Public Class DataGridCharting
        Inherits CommonChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = unitsChart
        End Sub
    End Class

    Public Enum AggregateType
        Average = 1
        Min = 2
        Max = 3
        Sum = 4
    End Enum
End Namespace
