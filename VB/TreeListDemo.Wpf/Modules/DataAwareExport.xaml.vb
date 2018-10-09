Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports GridDemo

Namespace TreeListDemo



    Partial Public Class DataAwareExport
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub ExportButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DemoModuleExportHelper.ExportToXlsx(view)
        End Sub
    End Class
End Namespace
