Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Media
Imports System.Xml.Linq
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/ChartElements/ConstantLinesControl.xaml"), CodeFile("Modules/ChartElements/ConstantLinesControl.xaml.(cs)")>
    Partial Public Class ConstantLinesControl
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
