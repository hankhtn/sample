Imports System.Windows.Media.Media3D
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingFor3DPieSeries.xaml"), CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingFor3DPieSeries.xaml.(cs)")>
    Partial Public Class ResolveLabelsOverlappingFor3DPieSeries
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            simpleDiagram3D.ContentTransform = New MatrixTransform3D(New Matrix3D(-0.719, -0.414, 0.558, 0, 0.693, -0.389, 0.605, 0, -0.032, 0.822, 0.567, 0, 0.000, 0.000, 0.000, 1))
        End Sub
    End Class
End Namespace
