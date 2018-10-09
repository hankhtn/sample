Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Grid
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace GridDemo



    Partial Public Class NewItemRow
        Inherits GridDemoModule

        Private newRowID As Integer = 10000
        Public Sub New()
            InitializeComponent()

            Dim dataLoader As NWindDataLoader = (CType(Resources("NWindDataLoader"), NWindDataLoader))
            grid.ItemsSource = New ObservableCollection(Of OrderDetail)(DirectCast(dataLoader.OrderDetails, IEnumerable(Of OrderDetail)))
        End Sub

        Protected Overrides Sub Show()
            MyBase.Show()
            view.ScrollIntoView(view.FocusedRowHandle)
        End Sub
        Private Sub view_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.InitNewRowEventArgs)
            grid.SetCellValue(e.RowHandle, colQuantity, 1)
            grid.SetCellValue(e.RowHandle, colUnitPrice, 100)
            grid.SetCellValue(e.RowHandle, colDiscount, 0)
            grid.SetCellValue(e.RowHandle, colOrderID, newRowID)
            newRowID += 1
        End Sub
        Private Sub newItemRowPositionChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            If view.NewItemRowPosition <> NewItemRowPosition.None Then
                view.FocusedRowHandle = GridControl.NewItemRowHandle
                view.ScrollIntoView(view.FocusedRowHandle)
            End If
        End Sub
    End Class
End Namespace
