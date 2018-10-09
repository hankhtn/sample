Imports CommonChartsDemo
Imports DevExpress.Data.Filtering
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.ExpressionEditor
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.Native
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls.Primitives
Imports System.Windows.Media

Namespace GridDemo.Tests
    Public Class GridCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
            Return moduleType IsNot GetType(ClipboardFormats) AndAlso moduleType IsNot GetType(InfiniteSourceModule) AndAlso moduleType IsNot GetType(PagingModule) AndAlso moduleType IsNot GetType(DataGridCharting) AndAlso moduleType IsNot GetType(DragDropCustomData)
        End Function
        Protected Overrides Function SwitchAllThemes(ByVal moduleType As Type) As Boolean
            Return False
        End Function
    End Class
    #Region "GridDemoModulesAccessor"
    Public Class GridDemoModulesAccessor
        Inherits DemoModulesAccessor(Of GridDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
        Public ReadOnly Property GridControl() As DevExpress.Xpf.Grid.GridControl
            Get
                Return DemoModule.GridControl
            End Get
        End Property
        Public ReadOnly Property View() As DevExpress.Xpf.Grid.GridViewBase
            Get
                Return CType(GridControl.View, DevExpress.Xpf.Grid.GridViewBase)
            End Get
        End Property
        Public ReadOnly Property TableView() As DevExpress.Xpf.Grid.TableView
            Get
                Return CType(View, DevExpress.Xpf.Grid.TableView)
            End Get
        End Property
        Public ReadOnly Property CardView() As DevExpress.Xpf.Grid.CardView
            Get
                Return CType(View, DevExpress.Xpf.Grid.CardView)
            End Get
        End Property
        Public ReadOnly Property TableViewModule() As StandardTableView
            Get
                Return CType(DemoModule, StandardTableView)
            End Get
        End Property
        Public ReadOnly Property MultiSelectionModule() As MultiRowSelection
            Get
                Return CType(DemoModule, MultiRowSelection)
            End Get
        End Property
        Public ReadOnly Property CopyPasteModule() As CopyPasteOperations
            Get
                Return CType(DemoModule, CopyPasteOperations)
            End Get
        End Property
        Public ReadOnly Property LargeDataSetModule() As LargeDataSource
            Get
                Return CType(DemoModule, LargeDataSource)
            End Get
        End Property
        Public ReadOnly Property DragDropModule() As GridDemo.DragDrop
            Get
                Return CType(DemoModule, GridDemo.DragDrop)
            End Get
        End Property
        Public ReadOnly Property FilterControlModule() As GridDemo.FilterControl
            Get
                Return CType(DemoModule, GridDemo.FilterControl)
            End Get
        End Property
        Public ReadOnly Property HitTestModule() As GridDemo.HitTesting
            Get
                Return CType(DemoModule, GridDemo.HitTesting)
            End Get
        End Property
        Public ReadOnly Property FilteringModule() As GridDemo.Filtering
            Get
                Return CType(DemoModule, GridDemo.Filtering)
            End Get
        End Property
        Public ReadOnly Property UnboundColumnsModule() As UnboundColumns
            Get
                Return CType(DemoModule, UnboundColumns)
            End Get
        End Property
    End Class
    #End Region
    Public MustInherit Class BaseGridDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As GridDemoModulesAccessor
        Public Sub New()
            modulesAccessor = New GridDemoModulesAccessor(Me)
        End Sub
        Public ReadOnly Property GridControl() As DevExpress.Xpf.Grid.GridControl
            Get
                Return modulesAccessor.GridControl
            End Get
        End Property
        Public ReadOnly Property View() As DevExpress.Xpf.Grid.GridViewBase
            Get
                Return modulesAccessor.View
            End Get
        End Property
        Public ReadOnly Property TableView() As DevExpress.Xpf.Grid.TableView
            Get
                Return modulesAccessor.TableView
            End Get
        End Property
        Public ReadOnly Property CardView() As DevExpress.Xpf.Grid.CardView
            Get
                Return modulesAccessor.CardView
            End Get
        End Property
        Public ReadOnly Property TableViewModule() As StandardTableView
            Get
                Return modulesAccessor.TableViewModule
            End Get
        End Property
        Public ReadOnly Property MultiSelectionModule() As MultiRowSelection
            Get
                Return modulesAccessor.MultiSelectionModule
            End Get
        End Property
        Public ReadOnly Property CopyPasteModule() As CopyPasteOperations
            Get
                Return modulesAccessor.CopyPasteModule
            End Get
        End Property
        Public ReadOnly Property LargeDataSetModule() As LargeDataSource
            Get
                Return modulesAccessor.LargeDataSetModule
            End Get
        End Property
        Public ReadOnly Property DragDropModule() As GridDemo.DragDrop
            Get
                Return modulesAccessor.DragDropModule
            End Get
        End Property
        Public ReadOnly Property FilterControlModule() As GridDemo.FilterControl
            Get
                Return modulesAccessor.FilterControlModule
            End Get
        End Property
        Public ReadOnly Property HitTestModule() As GridDemo.HitTesting
            Get
                Return modulesAccessor.HitTestModule
            End Get
        End Property
        Public ReadOnly Property FilteringModule() As GridDemo.Filtering
            Get
                Return modulesAccessor.FilteringModule
            End Get
        End Property
        Public ReadOnly Property UnboundColumnsModule() As UnboundColumns
            Get
                Return modulesAccessor.UnboundColumnsModule
            End Get
        End Property
    End Class
    Public Class CheckDemoOptionsFixture
        Inherits BaseGridDemoTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            CreateCheckFilterControlDemoActions()
            CreateCheckHitTestDemoActions()

            CreateCheckUnboundColumnsDemoActions()
            CreateCheckFilteringDemoActions()
            CreateCheckMultiSelectionDemoActions()
            CreateCheckCopyPasteDemoActions()
            CreateCheckTableViewDemoActions()
            CreateCheckFixedColumnsDemoActions()
            CreateSetCurrentDemoActions(Nothing, False)
        End Sub
        Private Function IsListBox(ByVal element As FrameworkElement) As Boolean
            Return TypeOf element Is System.Windows.Controls.ListBox
        End Function
        Private Function GetListBoxEditCount(ByVal listBoxEdit As ListBoxEdit) As Integer
            Return DirectCast(listBoxEdit.ItemsSource, System.Collections.IList).Count
        End Function
        #Region "UnboundColumns"
        Private Sub CreateCheckUnboundColumnsDemoActions()
            AddLoadModuleActions(GetType(GridDemo.UnboundColumns))
            Dispatch(Sub()
                Assert.AreEqual(0, GridControl.Columns("OrderID").GroupIndex)
                Assert.AreEqual(1, GridControl.GroupCount)
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-1))
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-2))
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-3))
                Assert.IsTrue(UnboundColumnsModule.showExpressionEditorButton.IsEnabled)
                Assert.IsTrue(GridControl.Columns("Total").AllowUnboundExpressionEditor)
                Assert.IsTrue(GridControl.Columns("DiscountAmount").AllowUnboundExpressionEditor)
                Assert.AreEqual(2, UnboundColumnsModule.columnsList.Items.Count)
                Dim listBox As System.Windows.Controls.ListBox
                listBox = CType(LayoutHelper.FindElement(UnboundColumnsModule.columnsList, AddressOf IsListBox), System.Windows.Controls.ListBox)
                Assert.AreEqual("Discount Amount", CType(listBox.ItemContainerGenerator.ContainerFromIndex(0), System.Windows.Controls.ListBoxItem).Content)
                Assert.AreEqual("Total", CType(listBox.ItemContainerGenerator.ContainerFromIndex(1), System.Windows.Controls.ListBoxItem).Content)
                Assert.AreEqual("DiscountAmount", CType(listBox.ItemContainerGenerator.ContainerFromIndex(0), System.Windows.Controls.ListBoxItem).Tag)
                Assert.AreEqual("Total", CType(listBox.ItemContainerGenerator.ContainerFromIndex(1), System.Windows.Controls.ListBoxItem).Tag)
                Assert.AreEqual(CDec(0.0), GridControl.GetCellValue(0, GridControl.Columns("DiscountAmount")))
                Assert.AreEqual(CDec(174.0), GridControl.GetCellValue(0, GridControl.Columns("Total")))
                Assert.IsTrue(Math.Abs(CDbl(38.0) - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns("DiscountAmount")))) < 0.001)
                Assert.IsTrue(Math.Abs(CDbl(214.2) - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns("Total")))) < 0.001)
                UnboundColumnsModule.columnsList.SelectedIndex = 0
                AssertShowExpressionEditor("Round([UnitPrice] * [Quantity] - [Total])")
                UpdateLayoutAndDoEvents()
                UnboundColumnsModule.columnsList.SelectedIndex = 1
                AssertShowExpressionEditor("[UnitPrice] * [Quantity] * (1 - [Discount])")
                UpdateLayoutAndDoEvents()
                GridControl.Columns("DiscountAmount").UnboundExpression = "[UnitPrice] * [Quantity] * (1 - [Discount])+[Discount][[Discount]]"
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(DevExpress.Data.UnboundErrorObject.Value, GridControl.GetCellValue(0, GridControl.Columns("DiscountAmount")))
            End Sub)
        End Sub

        Private _expectedExpression As String
        Private expressionEditorControl As ExpressionEditorControl = Nothing

        Private Sub OnExpressionEditorLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim _dw As DXDialogWindow = TryCast(expressionEditorControl.Parent, DXDialogWindow)
            Assert.IsNotNull(_dw, "2")
            RemoveHandler expressionEditorControl.Loaded, AddressOf OnExpressionEditorLoaded
            Assert.IsTrue(expressionEditorControl.IsVisible)
            Assert.AreEqual(_expectedExpression, DirectCast(expressionEditorControl, DevExpress.Xpf.Editors.ExpressionEditor.Native.ISupportExpressionString).GetExpressionString(Nothing))
            _dw.DialogResult = True
            _dw.Close()
        End Sub

        Private Sub OnExpressionEditorCreated(ByVal sender As Object, ByVal e As UnboundExpressionEditorEventArgs)
            expressionEditorControl = e.ExpressionEditorControl
            Assert.IsNotNull(expressionEditorControl, "1")
            AddHandler expressionEditorControl.Loaded, AddressOf OnExpressionEditorLoaded
            RemoveHandler View.UnboundExpressionEditorCreated, AddressOf OnExpressionEditorCreated
        End Sub

        Private Sub AssertShowExpressionEditor(ByVal expectedExpression As String)
            _expectedExpression = expectedExpression
            AddHandler View.UnboundExpressionEditorCreated, AddressOf OnExpressionEditorCreated
            UIAutomationActions.ClickButton(UnboundColumnsModule.showExpressionEditorButton)
            UpdateLayoutAndDoEvents()
        End Sub
        #End Region
        #Region "hit test"
        Private Function IsGroupGridRow(ByVal element As FrameworkElement) As Boolean
            Return TypeOf element Is DevExpress.Xpf.Grid.GroupRowControl
        End Function
        Private Sub CreateCheckHitTestDemoActions()
            AddLoadModuleActions(GetType(GridDemo.HitTesting))
            Dispatch(Sub()
                Assert.IsTrue(HitTestModule.showHitInfoCheckEdit.IsChecked.Value)
                Assert.AreEqual(0, HitTestModule.viewsListBox.SelectedIndex)
                Assert.AreEqual(GridControl, HitTestModule.hitInfoPopup.PlacementTarget)
                Assert.AreEqual(PlacementMode.Mouse, HitTestModule.hitInfoPopup.Placement)
                MouseActions.MouseMove(GridControl, -5, -5)
                Assert.IsFalse(HitTestModule.hitInfoPopup.IsOpen)
                MouseActions.MouseMove(GridControl, 5, 5)
                Assert.IsTrue(HitTestModule.hitInfoPopup.IsOpen)
                Assert.AreEqual(0R, HitTestModule.hitInfoPopup.HorizontalOffset)
                Assert.AreEqual(0R, HitTestModule.hitInfoPopup.VerticalOffset)
                EditorsActions.ToggleCheckEdit(HitTestModule.showHitInfoCheckEdit)
                Assert.IsFalse(HitTestModule.hitInfoPopup.IsOpen)
                EditorsActions.ToggleCheckEdit(HitTestModule.showHitInfoCheckEdit)
                Assert.IsTrue(HitTestModule.hitInfoPopup.IsOpen)
                MouseActions.LeftMouseDown(GridControlHelper.GetColumnHeaderElements(GridControl, GridControl.Columns(0))(0), 5, 5)
                MouseActions.MouseMove(GridControlHelper.GetColumnHeaderElements(GridControl, GridControl.Columns(0))(0), 25, 5)
                Assert.IsFalse(HitTestModule.hitInfoPopup.IsOpen)
                MouseActions.LeftMouseUp(GridControlHelper.GetColumnHeaderElements(GridControl, GridControl.Columns(0))(0), 5, 5)
                Assert.IsTrue(HitTestModule.hitInfoPopup.IsOpen)
                TableView.ShowColumnChooser()
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(HitTestModule.hitInfoPopup.IsOpen)
                TableView.HideColumnChooser()
                UpdateLayoutAndDoEvents()
                Assert.IsTrue(HitTestModule.hitInfoPopup.IsOpen)
                Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count)
                Assert.AreEqual("HitTest", GetHitInfoNameTextControl(0).NameValue)
                Assert.AreEqual("ColumnHeader", GetHitInfoNameTextControl(0).TextValue)
                Assert.AreEqual("Column", GetHitInfoNameTextControl(1).NameValue)
                Assert.AreEqual("ID", GetHitInfoNameTextControl(1).TextValue)
                Assert.AreEqual("RowHandle", GetHitInfoNameTextControl(2).NameValue)
                Assert.AreEqual("No row", GetHitInfoNameTextControl(2).TextValue)
                Assert.AreEqual("CellValue", GetHitInfoNameTextControl(3).NameValue)
                Assert.AreEqual("", GetHitInfoNameTextControl(3).TextValue)
                MouseActions.MouseMove(LayoutHelper.FindElementByName(GridControl, "PART_NewItemRow"), 35, 5)
                Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count)
                Assert.AreEqual("New Item Row", GetHitInfoNameTextControl(2).TextValue)
                MouseActions.MouseMove(LayoutHelper.FindElementByName(GridControl, "PART_FilterRow"), 35, 5)
                Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count)
                Assert.AreEqual("Auto Filter Row", GetHitInfoNameTextControl(2).TextValue)
                MouseActions.MouseMove(View.GetCellElementByRowHandleAndColumn(0, GridControl.Columns(0)), 35, 5)
                Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count)
                Assert.AreEqual("0 (data row)", GetHitInfoNameTextControl(2).TextValue)
                Assert.AreEqual("10248", GetHitInfoNameTextControl(3).TextValue)
                AssertAdditionalElements()
                MouseActions.MouseMove(LayoutHelper.FindElement(LayoutHelper.FindElement(GridControl, AddressOf IsGroupGridRow), Function(e) TypeOf e Is DevExpress.Xpf.Grid.GroupRowIndicator), 5, 5)
                Assert.AreEqual(5, HitTestModule.hitIfoItemsControl.Items.Count)
                Assert.AreEqual("RowIndicatorState", GetHitInfoNameTextControl(4).NameValue)
                Assert.AreEqual("Focused", GetHitInfoNameTextControl(4).TextValue)
                HitTestModule.viewsListBox.SelectedIndex = 1
                CardView.CardLayout = DevExpress.Xpf.Grid.CardLayout.Rows
                UpdateLayoutAndDoEvents()
                Assert.IsNotNull(CardView)
                AssertAdditionalElements()
            End Sub)
        End Sub

        Private Sub AssertAdditionalElements()
            GridControl.Columns(0).GroupIndex = 0
            UpdateLayoutAndDoEvents()
            MouseActions.MouseMove(View.GetRowElementByRowHandle(-1), View.GetRowElementByRowHandle(-1).ActualWidth / 2, View.GetRowElementByRowHandle(-1).ActualHeight / 2)
            Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count)
            Assert.AreEqual("-1 (group row)", GetHitInfoNameTextControl(2).TextValue)
            Assert.AreEqual(Nothing, GetHitInfoNameTextControl(3).TextValue)

            MouseActions.MouseMove(LayoutHelper.FindElement(View.GetRowElementByRowHandle(-1), Function(element) CStr(element.GetValue(System.Windows.Controls.TextBlock.TextProperty)) = "10248"), 5, 5)
            Assert.AreEqual(5, HitTestModule.hitIfoItemsControl.Items.Count)
            Assert.AreEqual("GroupValue", GetHitInfoNameTextControl(4).NameValue)
            Assert.AreEqual("OrderID: 10248", GetHitInfoNameTextControl(4).TextValue)

            MouseActions.MouseMove(LayoutHelper.FindElement(View.GetRowElementByRowHandle(-1), Function(element) CStr(element.GetValue(System.Windows.Controls.TextBlock.TextProperty)) = "Count=3"), 5, 5)
            Assert.AreEqual(5, HitTestModule.hitIfoItemsControl.Items.Count)
            Assert.AreEqual("GroupSummary", GetHitInfoNameTextControl(4).NameValue)
            Assert.AreEqual("Count=3", GetHitInfoNameTextControl(4).TextValue)

            GridControl.ClearGrouping()
            UpdateLayoutAndDoEvents()
            MouseActions.MouseMove(LayoutHelper.FindElement(View, Function(element) CStr(element.GetValue(System.Windows.Controls.TextBlock.TextProperty)) = "Count=2155"), 5, 5)
            Assert.AreEqual(4, HitTestModule.hitIfoItemsControl.Items.Count)
            Assert.AreEqual("FixedTotalSummary", GetHitInfoNameTextControl(3).NameValue)
            Assert.AreEqual("Count=2155", GetHitInfoNameTextControl(3).TextValue)
        End Sub
        Private Function GetHitInfoNameTextControl(ByVal intdex As Integer) As NameTextControl
            Return CType(VisualTreeHelper.GetChild(HitTestModule.hitIfoItemsControl.ItemContainerGenerator.ContainerFromIndex(intdex), 0), NameTextControl)
        End Function
        #End Region

        Private Function GetElementByName(Of T As FrameworkElement)(ByVal root As FrameworkElement, ByVal name As String) As T
            Return CType(LayoutHelper.FindElementByName(root, name), T)
        End Function

        #Region "table view"
        Private Sub CreateCheckTableViewDemoActions()
            AddLoadModuleActions(GetType(StandardTableView))
            Dispatch(Sub()
                Assert.IsTrue(TableView.AllowSorting)
                Assert.IsTrue(TableView.AllowGrouping)
                Assert.IsTrue(TableView.AllowColumnMoving)
                Assert.IsTrue(TableView.AllowResizing)
                Assert.IsTrue(TableView.AllowBestFit)
                Assert.IsTrue(TableView.ShowHorizontalLines)
                Assert.IsTrue(TableView.ShowVerticalLines)
                Dim hasStarColumn As Boolean = False
                For Each column In GridControl.Columns
                    hasStarColumn = hasStarColumn OrElse column.Width.IsStar
                    Select Case column.FieldName
                        Case "Phone"
                            Assert.IsFalse(column.AllowSorting.ToBoolean(False))
                            Assert.IsFalse(column.ActualAllowGrouping)
                        Case Else
                            Assert.IsTrue(column.ActualAllowSorting)
                            Assert.IsTrue(column.ActualAllowGrouping)
                    End Select
                Next column
                Assert.IsTrue(hasStarColumn)
                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowSorting)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowSorting)
                    Assert.IsFalse(column.ActualAllowGrouping)
                Next column
                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit)
                EditorsActions.ToggleCheckEdit(TableViewModule.allowGroupingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowGrouping)
                For Each column In GridControl.Columns
                    Select Case column.FieldName
                        Case "Phone"
                            Assert.IsFalse(column.AllowSorting.ToBoolean(False))
                            Assert.IsFalse(column.ActualAllowGrouping)
                        Case Else
                            Assert.IsTrue(column.ActualAllowSorting)
                            Assert.IsFalse(column.ActualAllowGrouping)
                    End Select
                Next column
                EditorsActions.ToggleCheckEdit(TableViewModule.allowMovingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowColumnMoving)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowMoving)
                Next column
                EditorsActions.ToggleCheckEdit(TableViewModule.allowResizingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowResizing)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowResizing)
                Next column
                EditorsActions.ToggleCheckEdit(TableViewModule.allowBestFitCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowBestFit)
                EditorsActions.ToggleCheckEdit(TableViewModule.showHorizontalLinesCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.ShowHorizontalLines)
                EditorsActions.ToggleCheckEdit(TableViewModule.showVerticalLinesCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.ShowVerticalLines)
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, View.NavigationStyle)
                TableViewModule.NavigationStyleComboBox.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
                TableViewModule.NavigationStyleComboBox.SelectedIndex = 2
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.None, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
            End Sub)
        End Sub
        #End Region
        #Region "Multi-Selection"
        Private Sub CreateCheckMultiSelectionDemoActions()
            AddLoadModuleActions(GetType(MultiRowSelection))
            Dispatch(Sub()
                Assert.AreEqual(GridControl.View, TableView)
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell, TableView.NavigationStyle)
                AssertMuliSelection(TableView)
                MultiSelectionModule.viewsListBox.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(GridControl.View, CardView)
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, CardView.NavigationStyle)
                GridControl.SelectItem(0)
                AssertMuliSelection(CardView)
            End Sub)
        End Sub
        Private Sub AssertMuliSelection(ByVal gridView As DevExpress.Xpf.Grid.GridViewBase)
            AssertMultiSelectMode(gridView, True)

            MultiSelectionModule.selectionModeListBox.EditValue = DevExpress.Xpf.Grid.MultiSelectMode.None
            UpdateLayoutAndDoEvents()
            AssertMultiSelectMode(gridView, False)
            Assert.IsFalse(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled)
            Assert.IsFalse(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled)
            MultiSelectionModule.selectionModeListBox.EditValue = DevExpress.Xpf.Grid.MultiSelectMode.Row
            UpdateLayoutAndDoEvents()
            AssertMultiSelectMode(gridView, True)
            Assert.IsTrue(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled)
            Assert.IsTrue(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled)

            Assert.AreEqual(77, (DirectCast(CType(MultiSelectionModule.ProductsMultiSelectionOptionsControl.comboBoxControl, ComboBoxEdit).ItemsSource, IEnumerable(Of Product))).Count())
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count)

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.SelectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(39, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(39, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.UnselectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(1, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.ReselectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(38, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(38, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))

            gridView.Grid.UnselectAll()
            gridView.Grid.SelectRange(0, 3)

            Assert.AreEqual("Grand Total=${0:N}", gridView.VisibleColumns(4).TotalSummaries(0).Item.DisplayFormat)
            Assert.AreEqual(Convert.ToString(607.4, System.Globalization.CultureInfo.CurrentCulture), gridView.VisibleColumns(4).TotalSummaries(0).Value.ToString())
            GridControl.UnselectAll()

            Dim count As Integer = (TryCast((MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.ItemsSource), System.Collections.Generic.List(Of Range))).Count
            Assert.AreEqual(9, count)
            For i As Integer = 0 To count - 1
                MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.SelectedIndex = i
                UIAutomationActions.ClickButton(MultiSelectionModule.PriceMultiSelectionOptionsControl.SelectButton)
                UpdateLayoutAndDoEvents()
            Next i
            Assert.AreEqual(GridControl.VisibleRowCount, gridView.Grid.SelectedItems.Count)
            GridControl.UnselectAll()

            GridControl.GroupBy("OrderID")
            UpdateLayoutAndDoEvents()
            GridControl.SelectItem(0)
            GridControl.SelectItem(10)
            Assert.AreEqual("Grand Total=${0:N}", CType(gridView.RootRowsContainer.Items(0), DevExpress.Xpf.Grid.GroupRowData).GroupSummaryData(0).SummaryItem.DisplayFormat)
            Assert.AreEqual(168, Convert.ToInt32(CType(gridView.RootRowsContainer.Items(0), DevExpress.Xpf.Grid.GroupRowData).GroupSummaryData(0).SummaryValue))
            Assert.AreEqual(336, Convert.ToInt32(CType(gridView.RootRowsContainer.Items(3), DevExpress.Xpf.Grid.GroupRowData).GroupSummaryData(0).SummaryValue))
            Assert.AreEqual(504, Convert.ToInt32(gridView.VisibleColumns(4).TotalSummaries(0).Value))
            GridControl.ClearGrouping()
            GridControl.UnselectAll()
        End Sub
        Private Sub AssertMultiSelectMode(ByVal view As DevExpress.Xpf.Grid.GridViewBase, ByVal isMultiSelect As Boolean)
            Assert.AreEqual(If(isMultiSelect, DevExpress.Xpf.Grid.MultiSelectMode.Row, DevExpress.Xpf.Grid.MultiSelectMode.None), view.DataControl.SelectionMode)
        End Sub
        #End Region
        #Region "Copy Paste"
        Private Sub CreateCheckCopyPasteDemoActions()
            AddLoadModuleActions(GetType(CopyPasteOperations))
            Dispatch(Sub()
                Assert.IsTrue(CopyPasteModule.firstGrid.ClipboardCopyMode <> DevExpress.Xpf.Grid.ClipboardCopyMode.None)
                Assert.IsTrue(CopyPasteModule.secondGrid.ClipboardCopyMode <> DevExpress.Xpf.Grid.ClipboardCopyMode.None)
                EditorsActions.ToggleCheckEdit(CopyPasteModule.allowCopyingtoClipboardCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsTrue(CopyPasteModule.firstGrid.ClipboardCopyMode = DevExpress.Xpf.Grid.ClipboardCopyMode.None)
                Assert.IsTrue(CopyPasteModule.secondGrid.ClipboardCopyMode = DevExpress.Xpf.Grid.ClipboardCopyMode.None)
                EditorsActions.ToggleCheckEdit(CopyPasteModule.allowCopyingtoClipboardCheckEdit)
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.None, False, False, False, False)
                Assert.AreEqual(10, CopyPasteModule.firstGrid.VisibleRowCount)
                Assert.AreEqual(0, CopyPasteModule.secondGrid.VisibleRowCount)
                Assert.AreEqual(0, CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex)
                ClickOnGrid(CopyPasteModule.firstGrid)
                CopyPasteModule.firstGrid.SelectRange(1, 4)
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.First, True, True, False, True)
                Assert.AreEqual(0, CopyPasteModule.PasteRule.SelectedIndex)
                UIAutomationActions.ClickButton(CopyPasteModule.CopyButton)
                UpdateLayoutAndDoEvents()
                ClickOnGrid(CopyPasteModule.secondGrid)
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.Second, False, False, True, False)
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
            End Sub)
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, Function() DemoBaseTesting.CurrentDemoModule, Nothing, Nothing)
            Dispatch(Sub()
                Assert.AreEqual(5, CopyPasteModule.secondGrid.VisibleRowCount)
                ClickOnGrid(CopyPasteModule.firstGrid)
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.First, True, True, True, True)
                UIAutomationActions.ClickButton(CopyPasteModule.DeleteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(5, CopyPasteModule.firstGrid.VisibleRowCount)
                CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.First, False, False, True, False)
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40))
                CopyPasteModule.textEdit.Select(0, 8)
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.First, True, True, True, True)
                UIAutomationActions.ClickButton(CopyPasteModule.CutButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 32))
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.CaretIndex = 0
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40))
                CopyPasteModule.textEdit.SelectAll()
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.CopyButton)
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.DeleteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("", CopyPasteModule.textEdit.Text)
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40))
                CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex = 0
                UpdateLayoutAndDoEvents()
                CopyPasteModule.secondGrid.UnselectAll()
                CopyPasteModule.secondGrid.SelectItem(2)
                UpdateLayoutAndDoEvents()
                CType(CType(CopyPasteModule.secondGrid.View, DevExpress.Xpf.Grid.GridViewBase).Grid.SelectedItems(0), GridDemo.CopyPasteOutlookData).From = "QWERTY"
                ClickOnGrid(CopyPasteModule.secondGrid)
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.CutButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(4, CopyPasteModule.secondGrid.VisibleRowCount)
                CopyPasteModule.firstGrid.View.FocusedRowHandle = 2
                ClickOnGrid(CopyPasteModule.firstGrid)
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
            End Sub)
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, Function() DemoBaseTesting.CurrentDemoModule, Nothing, Nothing)
            Dispatch(Sub()
                Assert.AreEqual(6, CopyPasteModule.firstGrid.VisibleRowCount)
                Assert.AreEqual("QWERTY", CType(CopyPasteModule.firstGrid.GetRow(3), GridDemo.CopyPasteOutlookData).From)
                CopyPasteModule.PasteRule.SelectedIndex = 1
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
            End Sub)
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, Function() DemoBaseTesting.CurrentDemoModule, Nothing, Nothing)
            Dispatch(Sub()
                Assert.AreEqual(7, CopyPasteModule.firstGrid.VisibleRowCount)
                Assert.AreEqual("QWERTY", CType(CopyPasteModule.firstGrid.GetRow(6), GridDemo.CopyPasteOutlookData).From)
            End Sub)
        End Sub
        Private Sub ClickOnGrid(ByVal grid As DevExpress.Xpf.Grid.GridControl)
            MouseActions.LeftMouseDown(grid.View, 2, 2)
            MouseActions.LeftMouseUp(grid.View, 2, 2)
        End Sub
        Private Sub AssertGridsAndButtonState(ByVal focusedGrid As FocusedGrid, ByVal copyBtn As Boolean, ByVal cutBtn As Boolean, ByVal pasteBtn As Boolean, ByVal delBtn As Boolean)
            Assert.AreEqual(focusedGrid, CopyPasteModule.FocusedGrid)
            Assert.AreEqual(copyBtn, CopyPasteModule.CopyButton.IsEnabled)
            Assert.AreEqual(cutBtn, CopyPasteModule.CutButton.IsEnabled)
            Assert.AreEqual(pasteBtn, CopyPasteModule.PasteButton.IsEnabled)
            Assert.AreEqual(delBtn, CopyPasteModule.DeleteButton.IsEnabled)
        End Sub
        #End Region
        #Region "Filter Control"
        Private Sub CreateCheckFilterControlDemoActions()
            AddLoadModuleActions(GetType(FilterControl))
            Dispatch(Sub()
                Assert.AreEqual(DefaultBoolean.Default, View.AllowFilterEditor)
                Dim filterCriteria As CriteriaOperator = New BinaryOperator("OrderID", 10248, BinaryOperatorType.Equal)
                Assert.AreEqual(filterCriteria, FilterControlModule.filterEditor.FilterCriteria)
                Assert.AreEqual(filterCriteria, GridControl.FilterCriteria)
                Assert.AreEqual(False, FilterControlModule.showGroupCommandsIcon.IsChecked)
                Assert.AreEqual(False, FilterControlModule.showOperandTypeIcon.IsChecked)
                Assert.AreEqual(True, FilterControlModule.showToolTips.IsChecked)
                Assert.IsFalse(FilterControlModule.filterEditor.ShowGroupCommandsIcon)
                Assert.IsFalse(FilterControlModule.filterEditor.ShowOperandTypeIcon)
                Assert.IsTrue(FilterControlModule.filterEditor.ShowToolTips)
                EditorsActions.ToggleCheckEdit(FilterControlModule.showGroupCommandsIcon)
                UpdateLayoutAndDoEvents()
                Assert.IsTrue(FilterControlModule.filterEditor.ShowGroupCommandsIcon)
                EditorsActions.ToggleCheckEdit(FilterControlModule.showOperandTypeIcon)
                UpdateLayoutAndDoEvents()
                Assert.IsTrue(FilterControlModule.filterEditor.ShowOperandTypeIcon)
                EditorsActions.ToggleCheckEdit(FilterControlModule.showToolTips)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(FilterControlModule.filterEditor.ShowToolTips)
                filterCriteria = New BinaryOperator("OrderID", 10249L, BinaryOperatorType.Equal)
                FilterControlModule.filterEditor.FilterCriteria = filterCriteria
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(FilterControlModule.ApplyFilterButton)
                Assert.AreEqual(filterCriteria, GridControl.FilterCriteria)
                filterCriteria = New BinaryOperator("OrderID", 10250L, BinaryOperatorType.Equal)
                GridControl.FilterCriteria = filterCriteria
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(filterCriteria, FilterControlModule.filterEditor.FilterCriteria)
                AddHandler View.FilterEditorCreated, AddressOf View_FilterEditorCreated
                View.ShowFilterEditor(Nothing)
                UpdateLayoutAndDoEvents()
                Assert.IsNotNull(FilterEditorFromGrid)
                Assert.IsTrue(FilterEditorFromGrid.ShowGroupCommandsIcon)
                Assert.IsTrue(FilterEditorFromGrid.ShowOperandTypeIcon)
                Assert.IsFalse(FilterEditorFromGrid.ShowToolTips)
                Dim filterDialogControl As DialogControl = LayoutHelper.FindParentObject(Of DialogControl)(FilterEditorFromGrid)
                Assert.IsNotNull(filterDialogControl)
                UIAutomationActions.ClickButton(filterDialogControl.CancelButton)
            End Sub)
        End Sub
        Private FilterEditorFromGrid As DevExpress.Xpf.Editors.Filtering.FilterControl = Nothing
        Private Sub View_FilterEditorCreated(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.FilterEditorEventArgs)
            FilterEditorFromGrid = e.FilterControl
        End Sub
        #End Region
        #Region "Filtering"
        Private Sub CreateCheckFilteringDemoActions()
            AddLoadModuleActions(GetType(Filtering))
            Dispatch(Sub()
                Assert.IsTrue(CBool(FilteringModule.allowFilterEditor.IsChecked))
                Assert.IsTrue(CBool(FilteringModule.allowFilteringCheckEdit.IsChecked))
                Assert.AreEqual(0, FilteringModule.viewsListBox.SelectedIndex)
                Assert.AreEqual(DevExpress.Xpf.Grid.ShowFilterPanelMode.Default, View.ShowFilterPanelMode)
                Dim column As DevExpress.Xpf.Grid.GridColumn = FilteringModule.colCountry
                Assert.AreEqual(DefaultBoolean.Default, column.AllowColumnFiltering)
                Assert.AreEqual(DefaultBoolean.Default, column.AllowColumnFiltering)
                Dim updateImmediatelyCheckBox As CheckEdit = FilteringModule.immediateUpdateCountryColumnFilterCheckEdit
                Assert.AreEqual(True, updateImmediatelyCheckBox.IsChecked)
                Assert.AreEqual(True, column.ImmediateUpdateColumnFilter)
                EditorsActions.ToggleCheckEdit(updateImmediatelyCheckBox)
                Assert.AreEqual(False, column.ImmediateUpdateColumnFilter)
                EditorsActions.ToggleCheckEdit(updateImmediatelyCheckBox)
                Assert.AreEqual(True, column.ImmediateUpdateColumnFilter)
                FilteringModule.viewsListBox.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(GetType(DevExpress.Xpf.Grid.CardView), CardView.GetType())
            End Sub)
        End Sub
        #End Region

        Private Sub CreateCheckFixedColumnsDemoActions()
            AddLoadModuleActions(GetType(FixedDataColumns))
        End Sub
    End Class
    Public Class BugFixesFixture
        Inherits BaseGridDemoTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            Create_B144178_Actions()
            CreateSetCurrentDemoActions(Nothing, False)
        End Sub
        #Region "B144178"
        Private Sub Create_B144178_Actions()
            AddLoadModuleActions(GetType(LargeDataSource))
            Dispatch(Sub() AssertPrioritySorting("Priority6"))
            AddLoadModuleActions(GetType(CellEditors))
            Dispatch(Sub() AssertPrioritySorting("Priority"))
            AddLoadModuleActions(GetType(AutoFilterRow))
            Dispatch(Sub() AssertPrioritySorting("Priority"))
        End Sub

        Private Sub AssertPrioritySorting(ByVal fieldName As String)
            GridControl.SortBy(fieldName)
            For i As Integer = 0 To 9
                Assert.AreEqual(Priority.Low, CType(GridControl.GetCellValue(i, fieldName), Priority), fieldName)
                Assert.AreEqual(Priority.High, CType(GridControl.GetCellValue(GridControl.VisibleRowCount - 1 - i, fieldName), Priority))
            Next i
        End Sub
        #End Region
    End Class
End Namespace
