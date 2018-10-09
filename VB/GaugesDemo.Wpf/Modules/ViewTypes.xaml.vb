Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo
    Partial Public Class ViewTypes
        Inherits GaugesDemoModule

        Private timer As New DispatcherTimer()

        Public Sub New()
            InitializeComponent()
            UpdateTime()
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Interval = New TimeSpan(0, 0, 1)
            timer.Start()
        End Sub
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateTime()
        End Sub
        Private Sub UpdateTime()
            time7Segment.Text = String.Format("{0:H:mm:ss}", Date.Now)
            time14Segment.Text = String.Format("{0:H:mm:ss}", Date.Now)
            timeMatrix5x8.Text = String.Format("{0:H:mm:ss}", Date.Now)
            timeMatrix8x14.Text = String.Format("{0:H:mm:ss}", Date.Now)
        End Sub
    End Class
End Namespace
