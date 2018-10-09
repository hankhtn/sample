Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports System.Linq
Imports System.IO
Imports System.Text
Imports System.ComponentModel

Namespace ChartsDemo
    <CodeFile("Modules/BarSeries/SideBySideRangeBar2DControl.xaml"), CodeFile("Modules/BarSeries/SideBySideRangeBar2DControl.xaml.(cs)"), CodeFile("ViewModels/OilPrices.(cs)")>
    Partial Public Class SideBySideRangeBar2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
