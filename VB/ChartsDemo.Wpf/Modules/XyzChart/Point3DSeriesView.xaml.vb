Imports System
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/XyzChart/Point3DSeriesView.xaml"), CodeFile("Modules/XyzChart/Point3DSeriesView.xaml.(cs)"), CodeFile("ViewModels/StarsData.(cs)")>
    Partial Public Class Point3DSeriesView
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
