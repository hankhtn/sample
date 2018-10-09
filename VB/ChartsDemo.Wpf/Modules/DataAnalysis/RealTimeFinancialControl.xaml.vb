Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Threading
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Linq

Namespace ChartsDemo
    <CodeFile("Modules/DataAnalysis/RealTimeFinancialControl.xaml"), CodeFile("Modules/DataAnalysis/RealTimeFinancialControl.xaml.(cs)"), CodeFile("ViewModels/FinancialDataViewModel.(cs)")>
    Partial Public Class RealTimeFinancialControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub

        Private Sub UpdateIndicators()
            priceSeries.Indicators.Clear()
            Dim existSeparatePaneIndicators As Boolean = False
            For Each item As ComboBoxEditItem In indicatorsBox.SelectedItems
                Dim indicator As Indicator = DirectCast(Activator.CreateInstance(CType(item.Tag, Type)), Indicator)
                Dim separatePaneIndicator As SeparatePaneIndicator = TryCast(indicator, SeparatePaneIndicator)
                If separatePaneIndicator IsNot Nothing Then
                    separatePaneIndicator.Pane = indicatorsPane
                    separatePaneIndicator.AxisY = indicatorsAxis
                    existSeparatePaneIndicators = True
                    indicator.Legend = indicatorsLegend
                Else
                    indicator.Legend = priceLegend
                End If
                indicator.LegendText = CStr(item.Content)
                indicator.ShowInLegend = True
                priceSeries.Indicators.Add(indicator)
            Next item
            indicatorsAxis.Visible = existSeparatePaneIndicators
            indicatorsPane.Visibility = If(existSeparatePaneIndicators, Visibility.Visible, Visibility.Hidden)
            indicatorsAxis.VisibilityInPanes(0).Visible = existSeparatePaneIndicators
            dateAxis.VisibilityInPanes(1).Visible = Not existSeparatePaneIndicators
            volumePane.AxisXScrollBarOptions.Visible = Not existSeparatePaneIndicators
        End Sub
        Private Sub ChartLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim maxRange = priceSeries.Points.Last().DateTimeArgument
            Dim minRange = priceSeries.Points(priceSeries.Points.Count \ 2).DateTimeArgument
            dateAxis.VisualRange.SetMinMaxValues(minRange, maxRange)
            UpdateIndicators()
        End Sub
        Private Sub QueryChartCursor(ByVal sender As Object, ByVal e As QueryChartCursorEventArgs)
            Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.Position)
            If hitInfo IsNot Nothing AndAlso hitInfo.Pane IsNot Nothing Then
                e.Cursor = Cursors.Hand
            End If
        End Sub
        Private Sub IndicatorsComboBoxCustomDisplayText(ByVal sender As Object, ByVal e As CustomDisplayTextEventArgs)
            If String.IsNullOrEmpty(e.DisplayText) Then
                e.DisplayText = "None"
                e.Handled = True
            End If
        End Sub
        Private Sub IndicatorsComboBoxEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateIndicators()
        End Sub
    End Class
End Namespace
