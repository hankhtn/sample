Imports System
Imports System.Windows.Threading
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo
    Partial Public Class DigitalModels
        Inherits GaugesDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub lbModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            FourteenSegmentsGauge.Text = CType(lbModel.SelectedItem, PredefinedElementKind).Name.ToUpper()
            Matrix8x14Gauge.Text = CType(lbModel.SelectedItem, PredefinedElementKind).Name
        End Sub
    End Class
End Namespace
