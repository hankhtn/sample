Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Linq

Namespace ChartsDemo
    <CodeFile("Modules/XyzChart/Bar3DSeriesView.xaml"), CodeFile("Modules/XyzChart/Bar3DSeriesView.xaml.(cs)"), CodeFile("ViewModels/Bar3DViewModel.(cs)")>
    Partial Public Class Bar3DSeriesView
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
