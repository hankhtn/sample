Imports System.Globalization
Imports System.Windows
Imports System.Windows.Markup
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/FinancialSeries/CandleStick2DControl.xaml"), CodeFile("Modules/FinancialSeries/CandleStick2DControl.xaml.(cs)")>
    Partial Public Class CandleStick2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            Me.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name)
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class
End Namespace
