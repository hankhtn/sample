using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;
using DevExpress.Xpf.Editors;
using DevExpress.Mvvm;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraSpreadsheet;
using Microsoft.Win32;
using System.Diagnostics;

namespace SpreadsheetDemo {
    public partial class Encryption : SpreadsheetDemoModule {

        public Encryption() {
            InitializeComponent();
            InitializeEncryptionOptions();
        }

        void InitializeEncryptionOptions() {
            passwordEdit.Text = "test";

            string[] array = Enum.GetNames(typeof(EncryptionType));
            typeEdit.ItemsSource = array;
            typeEdit.SelectedItem = EncryptionType.Strong.ToString();
        }

        void passwordChanged(object sender, EditValueChangedEventArgs e) {
            spreadsheetControl1.Document.DocumentSettings.Encryption.Password = passwordEdit.Text;
        }

        void typeChanged(object sender, EditValueChangedEventArgs e) {
            spreadsheetControl1.Document.DocumentSettings.Encryption.Type = (EncryptionType)Enum.Parse(typeof(EncryptionType), typeEdit.Text);
        }

        void SaveAsXlsx(object sender, RoutedEventArgs e) {
            OnBtnClick("Excel Workbook files(*.xlsx)|*.xlsx", "Document.xlsx");
        }

        void SaveAsXls(object sender, RoutedEventArgs e) {
            OnBtnClick("Excel 97-2003 Workbook files(*.xls)|*.xls", "Document.xls");
        }

        void OnBtnClick(string filter, string defaultName) {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Filter = filter;
            sfDialog.FileName = defaultName;
            bool? result = sfDialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            string fileName = sfDialog.FileName;
            spreadsheetControl1.SaveDocument(fileName);

            if (openFileCheckEditBox.IsChecked.HasValue && openFileCheckEditBox.IsChecked.Value)
                Process.Start(fileName);
        }
    }
}
