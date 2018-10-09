Imports System
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Core

Namespace SpreadsheetDemo
    Partial Public Class RibbonCustomization
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub About_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
            DXMessageBox.Show("This demo illustrates how to customize the WPF Spreadsheet's integrated ribbon UI." & ControlChars.Lf & ControlChars.Lf & "Switch to the Code View to learn how to use the Spreadsheet Control's RibbonActions collection to create, remove or modify ribbon elements.", "Spreadsheet Ribbon Customization", MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub
    End Class
End Namespace
