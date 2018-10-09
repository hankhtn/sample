Imports System
Imports System.Windows
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerDemo
    Partial Public Class HorizontalResourceHeaderStyles
        Inherits SchedulerDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeScheduler()

            ApplyHorizontalResourceHeaderStyle()
        End Sub

        Private Sub ApplyHorizontalResourceHeaderStyle()
            Dim style As Style = CType(Me.FindResource("HorizontalResourceHeaderStyle"), Style)
            For Each view As SchedulerViewBase In Views
                view.HorizontalResourceHeaderStyle = style
            Next view
        End Sub
        Private Sub ClearHorizontalResourceHeaderStyle()
            For Each view As SchedulerViewBase In Views
                view.ClearValue(SchedulerViewBase.HorizontalResourceHeaderStyleProperty)
            Next view
        End Sub
        Private Sub cheHorizontalResourceHeader_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ApplyHorizontalResourceHeaderStyle()
        End Sub
        Private Sub cheHorizontalResourceHeader_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ClearHorizontalResourceHeaderStyle()
        End Sub
    End Class
End Namespace
