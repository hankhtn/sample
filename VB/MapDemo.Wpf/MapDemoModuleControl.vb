Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Map

Namespace MapDemo
    Public Class MapDemoModule
        Inherits DemoModule

        Protected Overrides Sub Hide()
            For Each map As MapControl In DemoUtils.FindMap(Me)
                map.HideToolTip()
            Next map
            MyBase.Hide()
        End Sub
    End Class
End Namespace
