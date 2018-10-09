Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo
    <POCOViewModel>
    Public Class TopValuesViewModel
        <BindableProperty(OnPropertyChangedMethodName := "UpdateTopValueCount")>
        Public Overridable Property TopValueCount() As Decimal
        <BindableProperty(OnPropertyChangedMethodName := "UpdateTopValueCount")>
        Public Overridable Property TopValueShowOthers() As Boolean

        Private privateFields As IEnumerable(Of FieldViewModel)
        Public Property Fields() As IEnumerable(Of FieldViewModel)
            Get
                Return privateFields
            End Get
            Private Set(ByVal value As IEnumerable(Of FieldViewModel))
                privateFields = value
            End Set
        End Property
        Private privateRowsAndColumns As IEnumerable(Of FieldViewModel)
        Public Property RowsAndColumns() As IEnumerable(Of FieldViewModel)
            Get
                Return privateRowsAndColumns
            End Get
            Private Set(ByVal value As IEnumerable(Of FieldViewModel))
                privateRowsAndColumns = value
            End Set
        End Property
        Public Sub New()
            Fields = New List(Of FieldViewModel) From {FieldViewModel.Create("Order ID", "OrderID", FieldArea.RowArea, 0), FieldViewModel.Create("Order Amount", "ExtendedPrice", FieldArea.DataArea, 0), FieldViewModel.Create("Category Name", "CategoryName", FieldArea.RowArea, 1), FieldViewModel.Create("Sales Person", "FullName", FieldArea.RowArea, 2), FieldViewModel.Create("Product Name", "ProductName", FieldArea.RowArea, 3)}
            RowsAndColumns = Fields.Where(Function(x) x.Area <> FieldArea.DataArea)
            SelectedField = Fields.Last()
            TopValueCount = 5
        End Sub
        <BindableProperty(OnPropertyChangedMethodName := "OnSelectedFieldChanged")>
        Public Overridable Property SelectedField() As FieldViewModel
        Protected Sub OnSelectedFieldChanged()

            Dim fields_Renamed = RowsAndColumns.Except(SelectedField.Yield())
            fields_Renamed.ForEach(Sub(x) x.Visible = False)
            SelectedField.Visible = True
            SelectedField.SortOrder = FieldSortOrder.Descending
            SelectedField.SortByFieldName = "ExtendedPrice"
            UpdateTopValueCount()
        End Sub
        Protected Sub UpdateTopValueCount()
            SelectedField.TopValueCount = TopValueCount
            SelectedField.TopValueShowOthers = TopValueShowOthers
        End Sub
    End Class
End Namespace
