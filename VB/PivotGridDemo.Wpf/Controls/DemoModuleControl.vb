Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo
    Public Class PivotGridDemoModule
        Inherits DemoModule

        Shared Sub New()
            NWindContext.Preload()
        End Sub
        Protected Overridable ReadOnly Property NeedChangeEditorsTheme() As Boolean
            Get
                Return False
            End Get
        End Property
        Protected Overrides Sub HidePopupContent()
            Dim pivotGrid = If(TryCast(Content, PivotGridControl), LayoutTreeHelper.GetVisualChildren(CType(Content, DependencyObject)).OfType(Of PivotGridControl)().FirstOrDefault())
            If pivotGrid IsNot Nothing Then
                pivotGrid.HideFieldList()
            End If
            MyBase.HidePopupContent()
        End Sub
    End Class
End Namespace
