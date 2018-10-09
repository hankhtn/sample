Imports DevExpress.Xpf.DemoBase

Namespace DockingDemo
    <CodeFile("ViewModels/DockLayoutManagerEventsViewModel.(cs)"), CodeFile("Services/DockLayoutManagerEventsService.(cs)")>
    Partial Public Class DockLayoutManagerEvents
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub bHome_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
            NavigateHomePage()
        End Sub
        Private Sub bAbout_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
            ShowAbout()
        End Sub
    End Class
End Namespace
