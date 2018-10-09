using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DevExpress.Utils;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.Charts;
using System.Windows.Threading;

namespace SpreadsheetDemo {
    public partial class RangeAsDataSource : SpreadsheetDemoModule {
        bool updateLocked = false;
        DispatcherOperation operation = null;

        public RangeAsDataSource() {
            InitializeComponent();
            BindChartToWorksheetRange();
        }

        void BindChartToWorksheetRange() {
            IWorkbook workbook = this.spreadsheetControl1.Document;
            Worksheet sheet = workbook.Worksheets[0];

            this.chartControl1.DataSource = sheet["B3:D103"].GetDataSource();
            Series series = this.chartControl1.Diagram.Series[0];
            series.ArgumentDataMember = "Column 0";
            series.ValueDataMember = "Column 1";
            series = this.chartControl1.Diagram.Series[1];
            series.ArgumentDataMember = "Column 0";
            series.ValueDataMember = "Column 2";
        }

        private void trbMean_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if (!updateLocked)
                UpdateValueAsync(UpdateMean);
        }

        private void trbStdDev_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if (!updateLocked)
                UpdateValueAsync(UpdateStdDev);
        }

        private void spreadsheetControl1_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e) {
            updateLocked = true;
            try {
                if (e.Cell.GetReferenceA1() == "F3")
                    trbMean.Value = e.Cell.Value.NumericValue;
                else if (e.Cell.GetReferenceA1() == "F6")
                    trbStdDev.Value = e.Cell.Value.NumericValue * 100;
            }
            finally {
                updateLocked = false;
            }
        }

        void UpdateValueAsync(Action action) {
            if (operation == null || operation.Status == DispatcherOperationStatus.Aborted || operation.Status == DispatcherOperationStatus.Completed)
                operation = Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, action);
        }

        void UpdateMean() {
            spreadsheetControl1.ActiveWorksheet["F3"].Value = trbMean.Value;
        }

        void UpdateStdDev() {
            spreadsheetControl1.ActiveWorksheet["F6"].Value = trbStdDev.Value / 100.0;
        }
    }
}
