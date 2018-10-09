Imports System.Windows
Imports System.Windows.Controls

Namespace MVVMDemo.TypedStylesDemo
    Partial Public Class EventsView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnButtonLocalClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            MessageBox.Show("Local click")
        End Sub

        Private Sub OnContainerAttachedClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            MessageBox.Show("Container attached click")
        End Sub
    End Class
End Namespace
