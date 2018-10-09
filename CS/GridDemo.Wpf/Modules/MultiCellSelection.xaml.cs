using DevExpress.Data;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace GridDemo {
    public partial class MultiCellSelection : GridDemoModule {
        public MultiCellSelection() {
            InitializeComponent();
            AssignDataSource();

            SelectCells();
        }
        static readonly string[] dxLogo = new[] {
"dd ",
"dd ",
"ddd",
"d d",
"d d",
"d d",
"d d",
"d d",
"ddd",
"dd ",
"dd ",
"   ",
"x x",
"x x",
"xxx",
"xxx",
" x ",
" x ",
" x ",
"xxx",
"xxx",
"x x",
"x x",
};
        void SelectCells() {
            grid.BeginSelection();
            var points = dxLogo.SelectMany((s, y) => {
                return s.Select((c, x) => c != ' ' ? new Point(x, y) : (Point?)null);
            });
            foreach(var point in points.Where(x => x != null)) {
                view.SelectCell(point.Value.Y, view.VisibleColumns[point.Value.X + 1]);
            }
            grid.EndSelection();
        }

        void AssignDataSource() {
            grid.ItemsSource = SalesByYearData.Data;
            grid.Columns["Date"].Visible = false;
            grid.Columns["Date"].ShowInColumnChooser = false;
            foreach(GridColumn column in view.VisibleColumns) {
                grid.TotalSummary.Add(new GridSummaryItem() { FieldName = column.FieldName, SummaryType = DevExpress.Data.SummaryItemType.Custom, DisplayFormat = "${0:N}" });
                column.EditSettings = new SpinEditSettings() { MaskType = MaskType.Numeric, MaskUseAsDisplayFormat = true, Mask = "c", MaskCulture = new CultureInfo("en-US") };
            }
        }

        int sum = 0;
        void grid_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e) {
            if(object.Equals(e.SummaryProcess, CustomSummaryProcess.Start)) {
                sum = 0;
            }
            if(e.SummaryProcess == CustomSummaryProcess.Calculate) {
                if(grid.View != null) {
                    if(!checkEdit.IsChecked.Value || view.IsCellSelected(e.RowHandle, grid.Columns[((GridSummaryItem)e.Item).FieldName])) {
                        if(e.FieldValue != DBNull.Value && e.FieldValue != null)
                            sum += (int)e.FieldValue;
                    }
                }
            }
            if(e.SummaryProcess == CustomSummaryProcess.Finalize)
                e.TotalValue = sum;
        }

        void TableView_SelectionChanged(object sender, DevExpress.Xpf.Grid.GridSelectionChangedEventArgs e) {
            grid.UpdateTotalSummary();
        }

        void CheckEdit_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            grid.UpdateTotalSummary();
        }

        void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
            SelectCells(true);
        }
        void Button_Click_1(object sender, System.Windows.RoutedEventArgs e) {
            SelectCells(false);
        }

        void SelectCells(bool shouldSelectTopValues) {
            List<KeyValuePair<GridCell, int>> list = new List<KeyValuePair<GridCell, int>>();
            for(int i = 0; i < grid.VisibleRowCount; i++)
                for(int j = 0; j < view.VisibleColumns.Count; j++)
                    list.Add(new KeyValuePair<GridCell, int>(new GridCell(i, view.VisibleColumns[j]), (int)grid.GetCellValue(i, view.VisibleColumns[j])));
            list.Sort(delegate(KeyValuePair<GridCell, int> x, KeyValuePair<GridCell, int> y) {
                return Compare(x, y, shouldSelectTopValues);
            });

            grid.BeginSelection();
            view.DataControl.UnselectAll();
            for(int i = 0; i < Math.Min(20, list.Count); i++) {
                view.SelectCell(list[list.Count - i - 1].Key.RowHandle, list[list.Count - i - 1].Key.Column);
            }
            grid.EndSelection();
        }

        private static int Compare(KeyValuePair<GridCell, int> x, KeyValuePair<GridCell, int> y, bool shouldSelectTopValues) {
            if(shouldSelectTopValues)
                return Comparer<int>.Default.Compare(x.Value, y.Value);
            else
                return Comparer<int>.Default.Compare(y.Value, x.Value);
        }
    }
}
