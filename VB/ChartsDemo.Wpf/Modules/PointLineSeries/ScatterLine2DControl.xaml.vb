Imports System
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Collections.Generic

Namespace ChartsDemo
    <CodeFile("Modules/PointLineSeries/ScatterLine2DControl.xaml"), CodeFile("Modules/PointLineSeries/ScatterLine2DControl.xaml.(cs)"), CodeFile("ViewModels/ChartDataSourceViewModel.(cs)")>
    Partial Public Class ScatterLine2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
