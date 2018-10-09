Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.Charts
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity

Namespace ChartsDemo
    Public Class ChartAnimationService
        Inherits ServiceBase
        Implements IChartAnimationService

        Private ReadOnly Property Chart() As ChartControl
            Get
                Return CType(AssociatedObject, ChartControl)
            End Get
        End Property
        Public Sub Animate() Implements IChartAnimationService.Animate
            Chart.Dispatcher.BeginInvoke(New Action(AddressOf Chart.Animate))
        End Sub
    End Class
End Namespace
