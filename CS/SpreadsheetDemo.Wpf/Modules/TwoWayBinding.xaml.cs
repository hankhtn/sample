using DevExpress.Spreadsheet;
using System;

namespace SpreadsheetDemo {
    public partial class TwoWayBinding : SpreadsheetDemoModule {
        public TwoWayBinding() {
            InitializeComponent();
            BindGridToWorksheetRange();
        }

        void BindGridToWorksheetRange() {
            IWorkbook workbook = this.spreadsheet.Document;
            Worksheet sheet = workbook.Worksheets[0];
            Table expenses = sheet.Tables[0];
            RangeDataSourceOptions options = new RangeDataSourceOptions();
            options.PreserveFormulas = true;
            options.SkipHiddenRows = true;
            grid.ItemsSource = expenses.DataRange.GetDataSource(options);
        }
    }
}
