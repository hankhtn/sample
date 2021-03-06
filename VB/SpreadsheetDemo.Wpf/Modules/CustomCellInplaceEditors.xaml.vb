Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Editors.Settings

Namespace SpreadsheetDemo
    Partial Public Class CustomCellInplaceEditors
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            BindCustomEditors()
        End Sub

        Private Sub BindCustomEditors()
            Dim sheet As Worksheet = spreadsheetControl1.Document.Worksheets("Sales report")

            Dim dateEditRange As Range = sheet("Table[Order Date]")
            sheet.CustomCellInplaceEditors.Add(dateEditRange, CustomCellInplaceEditorType.DateEdit)

            Dim comboBoxRange As Range = sheet("Table[Category]")
            sheet.CustomCellInplaceEditors.Add(comboBoxRange, CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(sheet("J3:J9")))

            Dim checkBoxRange As Range = sheet("Table[Discount]")
            sheet.CustomCellInplaceEditors.Add(checkBoxRange, CustomCellInplaceEditorType.CheckBox)

            Dim customRange As Range = sheet("Table[Qty]")
            sheet.CustomCellInplaceEditors.Add(customRange, CustomCellInplaceEditorType.Custom, "MySpinEdit")
        End Sub

        Private Sub spreadsheetControl1_CustomCellEdit(ByVal sender As Object, ByVal e As DevExpress.Xpf.Spreadsheet.SpreadsheetCustomCellEditEventArgs)
            If e.ValueObject.IsText AndAlso e.ValueObject.TextValue = "MySpinEdit" Then
                Dim settings As New SpinEditSettings()
                settings.MinValue = 1
                settings.MaxValue = 1000
                settings.IsFloatValue = False
                e.EditSettings = settings
            End If
        End Sub
    End Class
End Namespace
