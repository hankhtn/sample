Imports DevExpress.Data
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Windows

Namespace GridDemo
    <DevExpress.Xpf.DemoBase.CodeFile("Controls/MiltiSelectionOptionsControl.xaml.(cs)"), DevExpress.Xpf.DemoBase.CodeFile("Controls/MiltiSelectionOptionsControl.xaml"), DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiSelectionClasses.(cs)"), DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiSelectionTemplates.xaml")>
    Partial Public Class MultiRowSelection
        Inherits GridDemoModule

        Private ReadOnly Property View() As GridViewBase
            Get
                Return CType(grid.View, GridViewBase)
            End Get
        End Property
        Public Sub New()
            InitializeComponent()
            AddHandler viewsListBox.EditValueChanged, AddressOf viewsListBox_SelectionChanged
            AddHandler View.ShowGridMenu, AddressOf View_ShowGridMenu
            FillComboBoxes()
            AddHandler ProductsMultiSelectionOptionsControl.SelectButtonClick, AddressOf SelectProductsButtonClick
            AddHandler ProductsMultiSelectionOptionsControl.UnselectButtonClick, AddressOf UnselectProductsButtonClick
            AddHandler ProductsMultiSelectionOptionsControl.ReselectButtonClick, AddressOf ReselectProductsButtonClick
            AddHandler PriceMultiSelectionOptionsControl.SelectButtonClick, AddressOf SelectPriceButtonClick
            AddHandler PriceMultiSelectionOptionsControl.UnselectButtonClick, AddressOf UnselectPriceButtonClick
            AddHandler PriceMultiSelectionOptionsControl.ReselectButtonClick, AddressOf ReselectPriceButtonClick
        End Sub

        Private Sub View_ShowGridMenu(ByVal sender As Object, ByVal e As GridMenuEventArgs)
            If e.MenuType = GridMenuType.Column Then
                e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNames.SortBySummary})
            End If
        End Sub
        Private Sub viewsListBox_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            grid.View = CType(FindResource(If(viewsListBox.SelectedIndex = 0, "tableView", "cardView")), GridViewBase)
        End Sub


        Private dataTableFromGrid_Renamed As List(Of Invoice) = Nothing
        Private ReadOnly Property DataTableFromGrid() As List(Of Invoice)
            Get
                If dataTableFromGrid_Renamed Is Nothing Then
                    dataTableFromGrid_Renamed = CType(grid.ItemsSource, List(Of Invoice))
                End If
                Return dataTableFromGrid_Renamed
            End Get
        End Property
        Private Sub FillComboBoxes()
            Dim listRanges As New List(Of Range)()
            Const lastRangeMinLimit As Integer = 240
            Const rangeInList As Integer = 30
            For i As Integer = 0 To lastRangeMinLimit Step rangeInList
                listRanges.Add(New Range() With {.Text = ("$" & Convert.ToString(i) & " - $" & Convert.ToString(i + rangeInList)), .Min = i, .Max = (i + rangeInList)})
            Next i
            PriceMultiSelectionOptionsControl.ComboBox.ItemsSource = listRanges
            PriceMultiSelectionOptionsControl.ComboBox.SelectedIndex = 0
            ProductsMultiSelectionOptionsControl.ComboBox.SelectedIndex = 0
        End Sub
        Private Sub RunAction(ByVal FilterDelegate As RowFilter, ByVal actionForRows As Action)
            Try
                grid.BeginSelection()
                Dim rows As IEnumerable(Of Invoice) = DataTableFromGrid
                For Each row In rows
                    If FilterDelegate(row) Then
                        actionForRows(grid.GetRowHandleByListIndex(DataTableFromGrid.IndexOf(row)))
                    End If
                Next row
            Finally
                grid.EndSelection()
            End Try
        End Sub
        Private Delegate Function RowFilter(ByVal row As Invoice) As Boolean
        Private Function SelectProductsFilter(ByVal row As Invoice) As Boolean
            Return Object.Equals(row.ProductID, CLng((ProductsMultiSelectionOptionsControl.ComboBox.EditValue)))
        End Function
        Private Function SelectRangeFilter(ByVal row As Invoice) As Boolean
            Dim range As Range = CType(PriceMultiSelectionOptionsControl.ComboBox.SelectedItem, Range)
            Try
                Dim unitPrice As Integer = Convert.ToInt32(row.UnitPrice)
                Return unitPrice >= range.Min AndAlso unitPrice <= range.Max
            Catch e1 As OverflowException
                Return False
            End Try
        End Function
        Private Delegate Sub Action(ByVal rowHandle As Integer)
        Private Sub SelectAction(ByVal rowHandle As Integer)
            grid.SelectItem(rowHandle)
        End Sub
        Private Sub UnselectAction(ByVal rowHandle As Integer)
            grid.UnselectItem(rowHandle)
        End Sub
        Private Sub SelectProductsButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            RunAction(AddressOf SelectProductsFilter, AddressOf SelectAction)
        End Sub
        Private Sub UnselectProductsButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            RunAction(AddressOf SelectProductsFilter, AddressOf UnselectAction)
        End Sub
        Private Sub ReselectProductsButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            grid.UnselectAll()
            RunAction(AddressOf SelectProductsFilter, AddressOf SelectAction)
        End Sub
        Private Sub SelectPriceButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            RunAction(AddressOf SelectRangeFilter, AddressOf SelectAction)
        End Sub
        Private Sub UnselectPriceButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            RunAction(AddressOf SelectRangeFilter, AddressOf UnselectAction)
        End Sub
        Private Sub ReselectPriceButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            grid.UnselectAll()
            RunAction(AddressOf SelectRangeFilter, AddressOf SelectAction)
        End Sub
        Private sum As Decimal = 0
        Private Sub grid_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
            If Object.Equals(e.SummaryProcess, CustomSummaryProcess.Start) Then
                sum = 0
            End If
            If e.SummaryProcess = CustomSummaryProcess.Calculate Then
                If (grid IsNot Nothing) AndAlso (grid.View IsNot Nothing) Then
                    If grid.View.IsRowSelected(e.RowHandle) Then
                        sum += CDec(e.FieldValue)
                    End If
                End If
            End If
            If e.SummaryProcess = CustomSummaryProcess.Finalize Then
                e.TotalValue = sum
            End If
        End Sub
        Private Sub UpdateSummary()
            If grid IsNot Nothing Then
                grid.UpdateTotalSummary()
                grid.UpdateGroupSummary()
            End If
        End Sub
        Private Sub gridView_SelectionChanged(ByVal sender As Object, ByVal e As GridSelectionChangedEventArgs)
            UpdateSummary()
        End Sub
        Private Sub SetMultiSelectMode(ByVal enabled As Boolean)
            ProductsMultiSelectionOptionsControl.IsEnabled = enabled
            PriceMultiSelectionOptionsControl.IsEnabled = enabled
        End Sub

        Private Sub grid_CurrentItemChanged(ByVal sender As Object, ByVal e As CurrentItemChangedEventArgs)
            If grid.SelectionMode <> MultiSelectMode.None Then
                UpdateSummary()
            End If
        End Sub

        Private Sub selectionModeListBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetMultiSelectMode(grid.SelectionMode <> MultiSelectMode.None)
        End Sub
    End Class
End Namespace
