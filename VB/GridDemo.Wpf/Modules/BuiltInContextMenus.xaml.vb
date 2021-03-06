Imports DevExpress.Data
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Utils
Imports System
Imports System.Windows
Imports System.Windows.Input

Namespace GridDemo



    Partial Public Class BuiltInContextMenus
        Inherits GridDemoModule

        Public Shared ReadOnly CellMenuInfoProperty As DependencyProperty = DependencyPropertyManager.Register("CellMenuInfo", GetType(GridCellMenuInfo), GetType(BuiltInContextMenus), New FrameworkPropertyMetadata(Nothing))

        Private privateDeleteRow As ICommand
        Public Property DeleteRow() As ICommand
            Get
                Return privateDeleteRow
            End Get
            Private Set(ByVal value As ICommand)
                privateDeleteRow = value
            End Set
        End Property
        Private privateCopyCellInfo As ICommand
        Public Property CopyCellInfo() As ICommand
            Get
                Return privateCopyCellInfo
            End Get
            Private Set(ByVal value As ICommand)
                privateCopyCellInfo = value
            End Set
        End Property
        Private privateCopyRowInfo As ICommand
        Public Property CopyRowInfo() As ICommand
            Get
                Return privateCopyRowInfo
            End Get
            Private Set(ByVal value As ICommand)
                privateCopyRowInfo = value
            End Set
        End Property

        Public Property CellMenuInfo() As GridCellMenuInfo
            Get
                Return CType(GetValue(CellMenuInfoProperty), GridCellMenuInfo)
            End Get
            Set(ByVal value As GridCellMenuInfo)
                SetValue(CellMenuInfoProperty, value)
            End Set
        End Property

        Public Sub New()
            DeleteRow = New DelegateCommand(Of Object)(AddressOf OnDeleteRow)
            CopyCellInfo = New DelegateCommand(Of Object)(AddressOf OnCopyCellInfo)
            CopyRowInfo = New DelegateCommand(Of Object)(AddressOf OnCopyRowInfo)
            InitializeComponent()
            grid.GroupBy(colCountry)
        End Sub
        Private Sub TableView_ShowGridMenu(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.GridMenuEventArgs)
            Select Case e.MenuType
                Case GridMenuType.Column
                    If columnMenuRemoveItemCheck.IsChecked.Value Then
                        e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNames.GroupBox})
                    End If
                    If Not columnMenuAddItemCheck.IsChecked.Value Then
                        e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = "allowSortingItem"})
                        e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = "allowGroupingItem"})
                    End If
                Case GridMenuType.GroupPanel
                    If groupPanelMenuRemoveItemCheck.IsChecked.Value Then
                        e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNames.ClearGrouping})
                    End If
                    If Not groupPanelMenuAddItemCheck.IsChecked.Value Then
                        e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = "allowAllSortingItem"})
                        e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = "allowAllGroupingItem"})
                    End If
                Case GridMenuType.TotalSummary
                    If totalMenuRemoveItemCheck.IsChecked.Value Then
                        e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultSummaryMenuItemNames.Customize})
                    End If
                    If Not Object.ReferenceEquals(e.MenuInfo.Column, colUnitPrice) Then
                        e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = "customSummaryMenuItem"})
                    End If

            End Select
            CellMenuInfo = If(e.MenuType = GridMenuType.RowCell, CType(e.MenuInfo, GridCellMenuInfo), Nothing)
        End Sub
        Private Sub OnDeleteRow(ByVal parameter As Object)
            If TypeOf parameter Is Integer Then
                view.DeleteRow(Convert.ToInt32(parameter))
            End If
        End Sub
        Private Sub OnCopyCellInfo(ByVal parameter As Object)
            Dim menuInfo As GridCellMenuInfo = TryCast(parameter, GridCellMenuInfo)
            If menuInfo IsNot Nothing AndAlso menuInfo.Row IsNot Nothing Then
                Dim text As String = GetCellText(menuInfo.Row.RowHandle.Value, menuInfo.Column)
                SetClibboardText(text)
            End If
        End Sub
        Private Sub OnCopyRowInfo(ByVal parameter As Object)
            If TypeOf parameter Is Integer Then
                grid.ClipboardCopyMode = ClipboardCopyMode.ExcludeHeader
                grid.CopyRowsToClipboard(New Integer() { Convert.ToInt32(parameter) })
                grid.ClipboardCopyMode = ClipboardCopyMode.IncludeHeader
            End If
        End Sub
        Private max, min As Decimal
        Private Sub grid_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
            If Object.Equals(e.SummaryProcess, CustomSummaryProcess.Start) Then
                min = Decimal.MinValue
            End If
            If e.SummaryProcess = CustomSummaryProcess.Calculate Then
                Dim value As Decimal = CDec(e.FieldValue)
                If min = Decimal.MinValue Then
                    max = value
                    min = max
                Else
                    max = Math.Max(max, value)
                    min = Math.Min(min, value)
                End If
            End If
            If e.SummaryProcess = CustomSummaryProcess.Finalize Then
                e.TotalValue = (min + max) / 2
            End If
        End Sub

        Private customItemCore As GridSummaryItem
        Private ReadOnly Property CustomItem() As GridSummaryItem
            Get
                If customItemCore Is Nothing Then
                    customItemCore = GetCustomItem()
                End If
                Return customItemCore
            End Get
        End Property
        Private Function GetCustomItem() As GridSummaryItem
            For Each item As GridSummaryItem In grid.TotalSummary
                If item.Tag IsNot Nothing AndAlso item.Tag.ToString() = "customItem" Then
                    Return item
                End If
            Next item
            Return Nothing
        End Function

        Private Sub grid_CustomSummaryExists(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryExistEventArgs)
            If (grid IsNot Nothing) AndAlso (Object.Equals(e.Item, CustomItem)) Then
                e.Exists = (TryCast(view.TotalSummaryMenuCustomizations(0), BarCheckItem)).IsChecked.Value
            End If
        End Sub
        Private Sub customSummaryMenuItem_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If grid IsNot Nothing Then
                CustomItem.SummaryType = If((TryCast(sender, BarCheckItem)).IsChecked.Value, SummaryItemType.Custom, SummaryItemType.None)
            End If
        End Sub
        Private Sub SetClibboardText(ByVal text As String)
            Try
                Clipboard.SetText(text)
            Catch
            End Try
        End Sub

        Private Function GetCellText(ByVal rowHandle As Integer, ByVal column As ColumnBase) As String
            Return Convert.ToString(grid.GetCellValue(rowHandle, CType(column, GridColumn)))
        End Function
    End Class
End Namespace
