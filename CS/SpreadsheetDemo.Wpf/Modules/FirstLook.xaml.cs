using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Office;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Spreadsheet;
using DevExpress.Xpf.Core;

namespace SpreadsheetDemo {
    public partial class FirstLook : SpreadsheetDemoModule {

        public FirstLook() {
            InitializeComponent();
        }

        private void spreadsheetControl1_InvalidFormatException(object sender, SpreadsheetInvalidFormatExceptionEventArgs e) {
            DXMessageBox.Show(string.Format("Cannot open the file '{0}' because the file format or file extension is not valid.\n" +
                "Verify that file has not been corrupted and that the file extension matches the format of the file.", e.SourceUri),
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void spreadsheetControl1_DocumentClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (spreadsheetControl1.Modified) {
                string currentFileName = spreadsheetControl1.Options.Save.CurrentFileName;
                string message = !string.IsNullOrEmpty(currentFileName) ?
                    string.Format("Do you want to save the changes you made for '{0}'?", currentFileName) :
                    "Do you want to save the changes?";
                MessageBoxResult result = DXMessageBox.Show(message, "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                    spreadsheetControl1.SaveDocument();
                e.Cancel = result == MessageBoxResult.Cancel;
            }
        }

        private void spreadsheetControl1_SelectionChanged(object sender, EventArgs e) {
            int count = 0;
            double sum = 0.0;
            int numericCount = 0;
            Worksheet sheet = spreadsheetControl1.ActiveWorksheet;
            DevExpress.Spreadsheet.Range selectedCells = sheet.Selection.Intersect(sheet.GetDataRange());
            if (selectedCells != null) {
                foreach (Cell cell in selectedCells.ExistingCells) {
                    count++;
                    if (cell.Value.IsNumeric) {
                        numericCount++;
                        sum += cell.Value.NumericValue;
                    }
                }
            }
            if (count <= 1)
                lblSelection.Content = string.Empty;
            else if (numericCount == 0)
                lblSelection.Content = string.Format("Count: {0}", count);
            else {
                double average = sum / numericCount;
                lblSelection.Content = string.Format(spreadsheetControl1.Options.Culture,
                    "Average: {0:#0.######}  Count: {1}  Sum: {2}", average, count, sum);
            }
        }
    }
}
