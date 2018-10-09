Imports System
Imports DevExpress.Xpf.Ribbon
Imports System.Windows.Controls
Imports System.Globalization

Namespace ProductsDemo.Modules
    Partial Public Class SpreadsheetModule
        Inherits UserControl

        Private Const FileName As String = "LoanCalculator.xlsx"

        Public Sub New()
            InitializeComponent()
            Dim filePath As String = Utils.GetRelativePath(FileName)
            If String.IsNullOrEmpty(filePath) Then
                Return
            End If
            Me.spreadsheetControl.LoadDocument(filePath)
        End Sub
    End Class


End Namespace
