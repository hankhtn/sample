Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/Interactivity/ScrollingZoomingControl.xaml"), CodeFile("Modules/Interactivity/ScrollingZoomingControl.xaml.(cs)"), CodeFile("ViewModels/PricesModel.(cs)")>
    Partial Public Class ScrollingZoomingControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
