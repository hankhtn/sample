Imports System
Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic

Namespace ChartsDemo
    <CodeFile("Modules/Appearance/CustomFinancial2DControl.xaml"), CodeFile("Modules/Appearance/CustomFinancial2DControl.xaml.(cs)")>
    Partial Public Class CustomFinancial2DControl
        Inherits ChartsDemoModule

        Public Shared ReadOnly Property Data() As List(Of FinancialPoint)
            Get

                Dim data_Renamed = New List(Of FinancialPoint) From {FinancialPoint.Create(1, 1.1, 2.9, 2.7, 1.6), FinancialPoint.Create(2, 1.9, 2.6, 2.4, 2.1), FinancialPoint.Create(3, 0.7, 2.4, 1.3, 2.1), FinancialPoint.Create(4, 1.8, 2.8, 2.4, 1.9), FinancialPoint.Create(5, 2.1, 3.4, 2.3, 3.1), FinancialPoint.Create(6, 2.2, 3.2, 3.0, 2.6), FinancialPoint.Create(7, 1.4, 2.7, 2.3, 2.5), FinancialPoint.Create(8, 2.1, 3.6, 3.2, 2.7), FinancialPoint.Create(9, 1.2, 3.1, 1.6, 2.6), FinancialPoint.Create(10, 2.7, 4.1, 3.4, 4.0)}
                Return data_Renamed
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
