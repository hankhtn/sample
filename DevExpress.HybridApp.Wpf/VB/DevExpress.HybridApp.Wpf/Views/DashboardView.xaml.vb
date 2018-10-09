Imports System
Imports System.Linq
Imports System.Windows.Controls
Imports System.Collections.Generic
Imports System.Windows.Data
Imports System.Windows
Imports DevExpress.Xpf.Charts

Namespace DevExpress.DevAV.Views
    Partial Public Class DashboardView
        Inherits UserControl

        Private Const highTopSpacing As Integer = 10
        Private Const lowTopSpacing As Integer = -15
        Private Const highBottomSpacing As Integer = 5
        Private Const lowBottomSpacing As Integer = 0
        Private Const heightThreshold As Integer = 150

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub goodsSold_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            Dim legend As Legend = DirectCast(sender, ChartControl).Legend
            If e.NewSize.Height < heightThreshold AndAlso (CInt((legend.Margin.Top)) <> lowTopSpacing OrElse CInt((legend.Margin.Bottom)) <> lowBottomSpacing) Then
                legend.Margin = New Thickness With {.Top = lowTopSpacing, .Bottom = lowBottomSpacing}
            End If
            If e.NewSize.Height >= heightThreshold AndAlso (CInt((legend.Margin.Top)) <> highTopSpacing OrElse CInt((legend.Margin.Bottom)) <> highBottomSpacing) Then
                legend.Margin = New Thickness With {.Top = highTopSpacing, .Bottom = highBottomSpacing}
            End If
        End Sub
    End Class
End Namespace
