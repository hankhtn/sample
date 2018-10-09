Imports DevExpress.Xpf.Map

Namespace MapDemo
    Partial Public Class WebMapService
        Inherits MapDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub OnResponseCapabilities(ByVal sender As Object, ByVal e As CapabilitiesResponsedEventArgs)
            lbWmsLayers.ItemsSource = e.Layers
        End Sub
    End Class
End Namespace
