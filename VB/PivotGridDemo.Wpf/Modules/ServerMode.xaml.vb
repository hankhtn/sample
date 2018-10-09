Imports System
Imports System.Diagnostics
Imports System.Windows
Imports DevExpress.Xpf.DemoBase

Namespace PivotGridDemo
    <CodeFile("Helpers/ServerMode/ServerModeViewModelBase.(cs)"), CodeFile("Helpers/ServerMode/ServerModeViewModel.(cs)"), CodeFile("Helpers/ServerMode/OrderDataContext.(cs)")>
    Partial Public Class ServerMode
        Inherits PivotGridDemoModule

        Private ReadOnly timer As New Stopwatch()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub pivotGrid_AsyncOperationStarting(ByVal sender As Object, ByVal e As RoutedEventArgs)
            tbTimeTaken.Text = "..."
            timer.Restart()
        End Sub

        Private Sub pivotGrid_AsyncOperationCompleted(ByVal sender As Object, ByVal e As RoutedEventArgs)
            timer.Stop()
            tbTimeTaken.Text = timer.ElapsedMilliseconds.ToString()
        End Sub
    End Class
End Namespace
