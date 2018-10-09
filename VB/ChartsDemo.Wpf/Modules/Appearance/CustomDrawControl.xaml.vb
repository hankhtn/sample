Imports System
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic
Imports System.Linq

Namespace ChartsDemo
    <CodeFile("Modules/Appearance/CustomDrawControl.xaml"), CodeFile("Modules/Appearance/CustomDrawControl.xaml.(cs)")>
    Partial Public Class CustomDrawControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub

        Private Sub chart_CustomDrawSeriesPoint(ByVal sender As Object, ByVal e As CustomDrawSeriesPointEventArgs)
            If CBool(chbCustomDraw.IsChecked) AndAlso e.DrawOptions IsNot Nothing Then
                Dim drawOptions = GetDrawOptions(e.SeriesPoint.Value)
                If Not String.IsNullOrEmpty(drawOptions.Item1) Then
                    e.LabelText = drawOptions.Item1 & ": " & e.LabelText
                End If
                e.DrawOptions.Color = drawOptions.Item2
            End If
        End Sub
        Private Shared Function GetDrawOptions(ByVal val As Double) As Tuple(Of String, Color)
            If val < 1 Then
                Return New Tuple(Of String, Color)("Green", Color.FromArgb(&HFF, &H51, &H89, &H3))
            End If
            If val < 2 Then
                Return New Tuple(Of String, Color)("Yellow", Color.FromArgb(&HFF, &HF9, &HAA, &HF))
            End If
            Return New Tuple(Of String, Color)("Red", Color.FromArgb(&HFF, &HC7, &H39, &HC))
        End Function
    End Class

    Public Class CustomDrawViewModel
        Public Overridable Property Data() As List(Of NumericPoint)
        Public Overridable ReadOnly Property ChartAnimationService() As IChartAnimationService
            Get
                Return Nothing
            End Get
        End Property

        Public Sub New()
            UpdateData()
        End Sub

        Public Sub UpdateData()
            Data = CreateData()
            If ChartAnimationService IsNot Nothing Then
                ChartAnimationService.Animate()
            End If
        End Sub
        Public Sub OnModuleLoaded()
            If ChartAnimationService IsNot Nothing Then
                ChartAnimationService.Animate()
            End If
        End Sub

        Private Shared Function CreateData() As List(Of NumericPoint)
            Dim random = New Random()
            Return Enumerable.Range(0, 20).Select(Function(x)
                Dim value = Math.Max(Math.Round(random.NextDouble() * 3, 1), 0.1)
                Return New NumericPoint(x, value)
            End Function).ToList()
        End Function
    End Class
End Namespace
