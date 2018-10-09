Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views
    Partial Public Class DevAVDbView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub OnNavButtonCloseClick(ByVal sender As Object, ByVal e As EventArgs)
            Application.Current.MainWindow.Close()
        End Sub
    End Class
End Namespace
