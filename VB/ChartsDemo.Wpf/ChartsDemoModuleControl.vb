Imports System
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    Public Class ChartsDemoModule
        Inherits DemoModule

        Private Shared ReadOnly ActualChartPropertyKey As DependencyPropertyKey = DependencyProperty.RegisterReadOnly("ActualChart", GetType(ChartControlBase), GetType(ChartsDemoModule), New PropertyMetadata(Nothing))
        Public Shared ReadOnly ActualChartProperty As DependencyProperty = ActualChartPropertyKey.DependencyProperty
        Public Property ActualChart() As ChartControlBase
            Get
                Return CType(GetValue(ActualChartProperty), ChartControlBase)
            End Get
            Protected Set(ByVal value As ChartControlBase)
                SetValue(ActualChartPropertyKey, value)
            End Set
        End Property

        Public Sub New()

        End Sub

        Protected Overrides Sub Show()
            MyBase.Show()
            If ActualChart IsNot Nothing AndAlso ActualChart.Palette IsNot Nothing Then
                ActualChart.Palette.ColorCycleLength = 10
            End If
        End Sub

    End Class
End Namespace

Namespace CommonChartsDemo
    Public Class CommonChartsDemoModule
        Inherits ChartsDemo.ChartsDemoModule

    End Class
End Namespace
