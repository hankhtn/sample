Imports System
Imports System.Windows.Threading
Imports DevExpress.Xpf.DemoBase

Namespace GaugesDemo
    Partial Public Class CircularModels
        Inherits GaugesDemoModule

        Public Sub New()
            InitializeComponent()
            lbModel.SelectedIndex = 0
            Dim dataGenerator As New GaugeRandomDataGenerator(0, 100, 1500)
            gauge.DataContext = dataGenerator
            gaugeHalfTop.DataContext = dataGenerator
            gaugeQuarterTopLeft.DataContext = dataGenerator
            gaugeThreeQuarters.DataContext = dataGenerator
            dataGenerator.Start()
        End Sub
    End Class
End Namespace
