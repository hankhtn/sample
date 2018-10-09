Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System

Namespace ChartsDemo
    <CodeFile("Modules/Interactivity/SelectionControl.xaml"), CodeFile("Modules/Interactivity/SelectionControl.xaml.(cs)"), CodeFile("ViewModels/DashboardViewModel.(cs)"), CodeFile("Utils/MapControlUtils.(cs)")>
    Partial Public Class SelectionControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = pieChart
        End Sub
    End Class
End Namespace
