Imports System
Imports System.Windows
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerDemo
    Partial Public Class VerticalResourceHeaderStyles
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()
            ApplyVerticalResourceHeaderStyle()
        End Sub
        Private Sub ApplyVerticalResourceHeaderStyle()
            Dim verticalResourceHeaderStyle As Style = CType(Me.FindResource("VerticalResourceHeaderStyle"), Style)
            For Each view As SchedulerViewBase In Views
                view.VerticalResourceHeaderStyle = verticalResourceHeaderStyle
            Next view
        End Sub
        Private Sub ClearVerticalResourceHeaderStyle()
            For Each view As SchedulerViewBase In Views
                view.ClearValue(SchedulerViewBase.VerticalResourceHeaderStyleProperty)
            Next view
        End Sub
        Private Sub cheVerticalResourceHeader_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyVerticalResourceHeaderStyle()
        End Sub
        Private Sub cheVerticalResourceHeader_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ClearVerticalResourceHeaderStyle()
        End Sub
    End Class
End Namespace
