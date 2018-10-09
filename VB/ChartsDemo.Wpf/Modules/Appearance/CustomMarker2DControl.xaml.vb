Imports System
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic

Namespace ChartsDemo
    <CodeFile("Modules/Appearance/CustomMarker2DControl.xaml"), CodeFile("Modules/Appearance/CustomMarker2DControl.xaml.(cs)")>
    Partial Public Class CustomMarker2DControl
        Inherits ChartsDemoModule

        Public Shared Function CreateData() As List(Of NumericPoint)
            Return New List(Of NumericPoint) From {
                New NumericPoint(1.0, 2.1, 1.0),
                New NumericPoint(2.0, 1.4, 2.0),
                New NumericPoint(3.0, 1.1, 4.0),
                New NumericPoint(4.0, 1.9, 3.0),
                New NumericPoint(5.0, 3.1, 2.5),
                New NumericPoint(6.0, 2.4, 1.7),
                New NumericPoint(7.0, 2.6, 3.9),
                New NumericPoint(8.0, 1.9, 2.8),
                New NumericPoint(9.0, 3.2, 2.1),
                New NumericPoint(10.0, 3.5, 1.3)
            }
        End Function

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
