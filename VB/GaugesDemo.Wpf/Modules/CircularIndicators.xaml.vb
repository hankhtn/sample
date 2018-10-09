Imports System
Imports System.Windows.Controls
Imports System.Windows.Threading
Imports DevExpress.Xpf.DemoBase

Namespace GaugesDemo
    Partial Public Class CircularIndicators
        Inherits GaugesDemoModule

        Public Sub New()
            InitializeComponent()
            Dim dataGenerator As New GaugeRandomDataGenerator(-100, 100, 1500)
            gauge.DataContext = dataGenerator
            dataGenerator.Start()
        End Sub
    End Class
End Namespace
