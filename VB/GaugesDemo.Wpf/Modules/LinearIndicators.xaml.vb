Imports System
Imports System.Windows.Controls
Imports System.Windows.Threading
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo
    Partial Public Class LinearIndicators
        Inherits GaugesDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ShowIndicatorListBoxEdit_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            For Each scale As LinearScale In linearGaugeControl.Scales
                scale.LevelBars(0).Visible = CStr(showIndicatorListBoxEdit.SelectedItem) = "Level Bar"
                scale.Markers(0).Visible = CStr(showIndicatorListBoxEdit.SelectedItem) = "Marker"
                scale.RangeBars(0).Visible = CStr(showIndicatorListBoxEdit.SelectedItem) = "Range Bar"
            Next scale
        End Sub
        Private Sub LbeShowPercent_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            For Each scale As LinearScale In linearGaugeControl.Scales
                If scale.LabelOptions IsNot Nothing Then
                    scale.LabelOptions.Multiplier = If(CStr(lbeShowPercent.SelectedItem) = "Values", 1, 100 / scale.EndValue)
                End If
            Next scale
        End Sub
    End Class
End Namespace
