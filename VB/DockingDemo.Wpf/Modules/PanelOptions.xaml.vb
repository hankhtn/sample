Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.DemoBase

Namespace DockingDemo
    Partial Public Class PanelOptions
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub bHome_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            NavigateHomePage()
        End Sub
        Private Sub bAbout_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            ShowAbout()
        End Sub
    End Class
End Namespace
