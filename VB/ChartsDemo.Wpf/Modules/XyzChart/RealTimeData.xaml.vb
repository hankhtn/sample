Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System
Imports System.Windows

Namespace ChartsDemo
    <CodeFile("Modules/XyzChart/RealTimeData.xaml"), CodeFile("Modules/XyzChart/RealTimeData.xaml.(cs)"), CodeFile("ViewModels/RealTimeSurfaceViewModel.(cs)")>
    Partial Public Class RealTimeData
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
