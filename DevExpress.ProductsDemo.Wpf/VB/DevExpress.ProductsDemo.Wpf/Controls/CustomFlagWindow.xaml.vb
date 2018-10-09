Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Ribbon
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes

Namespace ProductsDemo.Controls



    Partial Public Class CustomFlagWindow
        Inherits ThemedWindow

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub btn_Ok_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = True
            Close()
        End Sub

        Private Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = False
            Close()
        End Sub
    End Class
End Namespace
