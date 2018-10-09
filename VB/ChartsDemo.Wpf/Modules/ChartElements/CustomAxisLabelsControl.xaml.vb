Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Linq

Namespace ChartsDemo
    <CodeFile("Modules/ChartElements/CustomAxisLabelsControl.xaml"), CodeFile("Modules/ChartElements/CustomAxisLabelsControl.xaml.(cs)")>
    Partial Public Class CustomAxisLabelsControl
        Inherits ChartsDemoModule

        Public Shared ReadOnly CustomLabels As List(Of TimeSpan) = Enumerable.Range(0, 9).Select(Function(x) TimeSpan.FromHours(x * 2)).Reverse().ToList()

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub chartToolTipControler_ToolTipOpening(ByVal sender As Object, ByVal e As ChartToolTipEventArgs)
            e.Hint = TimeSpan.FromMinutes(e.SeriesPoint.Value).ToString("hh\:mm")
        End Sub
    End Class
End Namespace
