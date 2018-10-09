Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo
    <CodeFile("Modules/Interactivity/NumericDataAggregation.xaml"), CodeFile("Modules/Interactivity/NumericDataAggregation.xaml.(cs)"), CodeFile("ViewModels/NumericPointData.(cs)")>
    Partial Public Class NumericDataAggregation
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
