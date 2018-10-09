Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Ribbon
Imports System.Windows
Namespace RibbonDemo
    Public Interface IBackstageViewService
        Sub Close()
    End Interface
    Public Class BackstageViewService
        Inherits ServiceBase
        Implements IBackstageViewService

        Public Sub Close() Implements IBackstageViewService.Close
            If (TypeOf AssociatedObject Is RibbonControl) Then
                CType(AssociatedObject, RibbonControl).CloseApplicationMenu()
            End If
        End Sub
    End Class
End Namespace
