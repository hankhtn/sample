Namespace MapDemo
    Public Class PhotoGalleryItemControl
        Inherits VisibleControl

        Public Sub New()
            DefaultStyleKey = GetType(PhotoGalleryItemControl)
            AddHandler MouseLeftButtonUp, AddressOf OnMouseButtonUp
        End Sub
        Private Sub OnMouseButtonUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            e.Handled = True
        End Sub
    End Class
End Namespace
