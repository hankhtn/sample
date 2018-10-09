Imports System
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/Appearance/CustomBar2DControl.xaml"), CodeFile("Modules/Appearance/CustomBar2DControl.xaml.(cs)")>
    Partial Public Class CustomBar2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            series.ToolTipPointPattern = "Argument: {A}" & ControlChars.Lf & "Value: {V:0.0}"
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class

    Public Class CustomBar2DAnimation
        Inherits Bar2DDropInAnimation

        Public Overrides Function CreateAnimatedBarBounds(ByVal barBounds As Rect, ByVal viewport As Rect, ByVal isNegativeBar As Boolean, ByVal axisXReverse As Boolean, ByVal axisYReverse As Boolean, ByVal diagramRotated As Boolean, ByVal progress As Double) As Rect
            Dim bounds As Rect = MyBase.CreateAnimatedBarBounds(barBounds, viewport, isNegativeBar, axisXReverse, axisYReverse, diagramRotated, progress)
            bounds.X += Math.Sin(progress * Math.PI * 4) * viewport.Width / 12
            Return bounds
        End Function
    End Class
End Namespace
