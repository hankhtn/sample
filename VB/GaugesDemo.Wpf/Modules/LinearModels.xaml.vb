Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo
    Partial Public Class LinearModels
        Inherits GaugesDemoModule

        Public Sub New()
            InitializeComponent()
            lbModel.SelectedIndex = 0
            Dim dataGenerator As New GaugeRandomDataGenerator(0, 100)
            gauge1.DataContext = dataGenerator
            gauge2.DataContext = dataGenerator
            dataGenerator.Start()
        End Sub
    End Class
End Namespace
