using System;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Editors.Settings;

namespace SpreadsheetDemo {
    public partial class CustomCellInplaceEditors : SpreadsheetDemoModule {
        public CustomCellInplaceEditors() {
            InitializeComponent();
            BindCustomEditors();
        }

        void BindCustomEditors() {
            Worksheet sheet = spreadsheetControl1.Document.Worksheets["Sales report"];

            Range dateEditRange = sheet["Table[Order Date]"];
            sheet.CustomCellInplaceEditors.Add(dateEditRange, CustomCellInplaceEditorType.DateEdit);

            Range comboBoxRange = sheet["Table[Category]"];
            sheet.CustomCellInplaceEditors.Add(comboBoxRange, CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(sheet["J3:J9"]));

            Range checkBoxRange = sheet["Table[Discount]"];
            sheet.CustomCellInplaceEditors.Add(checkBoxRange, CustomCellInplaceEditorType.CheckBox);

            Range customRange = sheet["Table[Qty]"];
            sheet.CustomCellInplaceEditors.Add(customRange, CustomCellInplaceEditorType.Custom, "MySpinEdit");
        }

        void spreadsheetControl1_CustomCellEdit(object sender, DevExpress.Xpf.Spreadsheet.SpreadsheetCustomCellEditEventArgs e) {
            if (e.ValueObject.IsText && e.ValueObject.TextValue == "MySpinEdit") {
                SpinEditSettings settings = new SpinEditSettings();
                settings.MinValue = 1;
                settings.MaxValue = 1000;
                settings.IsFloatValue = false;
                e.EditSettings = settings;
            }
        }
    }
}
