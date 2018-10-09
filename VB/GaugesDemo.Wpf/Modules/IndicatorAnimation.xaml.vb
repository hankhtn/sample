Imports System
Imports System.Windows.Threading
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors

Namespace GaugesDemo
    Partial Public Class IndicatorAnimation
        Inherits GaugesDemoModule

        Private rand As New Random()
        Private timer As New DispatcherTimer()

        Public Sub New()
            InitializeComponent()
            timer.Interval = New TimeSpan(0, 0, 3)
            AddHandler timer.Tick, AddressOf Timer_Tick
            timer.Start()
        End Sub
        Private Sub Timer_Tick(ByVal source As Object, ByVal e As EventArgs)
            voltmeterScaleNeedle.Value = rand.Next(1, CInt((voltmeterScale.EndValue)))
            Dim nextRand As Double = rand.Next(3, CInt((ampermeterScale.EndValue)) * 10)
            ampermeterScaleNeedle.Value = nextRand / 10.0
            wattmeterScaleNeedle.Value = voltmeterScaleNeedle.Value * ampermeterScaleNeedle.Value
        End Sub
    End Class
End Namespace
