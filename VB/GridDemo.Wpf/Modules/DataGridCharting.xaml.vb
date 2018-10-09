Imports System
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace CommonChartsDemo
    <CodeFile("ModuleResources/DataGridCharting/SalesProductData.(cs)")>
    Partial Public Class DataGridCharting
        Inherits CommonChartsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Enum AggregateType
        Average = 1
        Min = 2
        Max = 3
        Sum = 4
    End Enum
End Namespace
