Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo
    Partial Public Class SingleTotal
        Inherits PivotGridDemoModule

        Public Shared ReadOnly Property FieldSummaryTypes() As IEnumerable(Of FieldSummaryType)
            Get
                Return GetType(FieldSummaryType).GetEnumValues().Cast(Of FieldSummaryType)().Except(FieldSummaryType.Custom.Yield())
            End Get
        End Property
        Public Sub New()
            InitializeComponent()
            cbField.ItemsSource = pivotGrid.Fields.Where(Function(x) x.Area = FieldArea.DataArea AndAlso x.Visible)
        End Sub
    End Class
End Namespace
