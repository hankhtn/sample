Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Spreadsheet
Imports DevExpress.Spreadsheet.Demos
Imports DevExpress.Xpf.Editors
Imports DevExpress.Mvvm
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraSpreadsheet
Imports Microsoft.Win32
Imports System.Diagnostics

Namespace SpreadsheetDemo
    Partial Public Class Encryption
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeEncryptionOptions()
        End Sub

        Private Sub InitializeEncryptionOptions()
            passwordEdit.Text = "test"

            Dim array() As String = System.Enum.GetNames(GetType(EncryptionType))
            typeEdit.ItemsSource = array
            typeEdit.SelectedItem = EncryptionType.Strong.ToString()
        End Sub

        Private Sub passwordChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            spreadsheetControl1.Document.DocumentSettings.Encryption.Password = passwordEdit.Text
        End Sub

        Private Sub typeChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            spreadsheetControl1.Document.DocumentSettings.Encryption.Type = DirectCast(System.Enum.Parse(GetType(EncryptionType), typeEdit.Text), EncryptionType)
        End Sub

        Private Sub SaveAsXlsx(ByVal sender As Object, ByVal e As RoutedEventArgs)
            OnBtnClick("Excel Workbook files(*.xlsx)|*.xlsx", "Document.xlsx")
        End Sub

        Private Sub SaveAsXls(ByVal sender As Object, ByVal e As RoutedEventArgs)
            OnBtnClick("Excel 97-2003 Workbook files(*.xls)|*.xls", "Document.xls")
        End Sub

        Private Sub OnBtnClick(ByVal filter As String, ByVal defaultName As String)
            Dim sfDialog As New SaveFileDialog()
            sfDialog.Filter = filter
            sfDialog.FileName = defaultName
            Dim result? As Boolean = sfDialog.ShowDialog()
            If (Not result.HasValue) OrElse (Not result.Value) Then
                Return
            End If

            Dim fileName As String = sfDialog.FileName
            spreadsheetControl1.SaveDocument(fileName)

            If openFileCheckEditBox.IsChecked.HasValue AndAlso openFileCheckEditBox.IsChecked.Value Then
                Process.Start(fileName)
            End If
        End Sub
    End Class
End Namespace
