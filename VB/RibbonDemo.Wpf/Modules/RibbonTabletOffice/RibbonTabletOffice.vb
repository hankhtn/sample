Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Ribbon

Namespace RibbonDemo
    <NoAutogeneratedCodeFiles>
    Public Class RibbonTabletOffice
        Inherits RibbonSimplePad

        Public Sub New()
        End Sub

        Protected Overrides Function GetTitleBarVisibility() As RibbonTitleBarVisibility
            Return RibbonTitleBarVisibility.Collapsed
        End Function
        Protected Overrides Function GetRibbonStyle() As RibbonStyle
            Return RibbonStyle.TabletOffice
        End Function
    End Class
End Namespace