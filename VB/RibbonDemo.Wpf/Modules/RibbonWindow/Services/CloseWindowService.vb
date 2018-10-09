Imports DevExpress.Mvvm.UI
Imports System.Windows
Namespace RibbonDemo
    Public Interface ICloseWindowService
        Sub Close()
    End Interface
    Public Class CloseWindowService
        Inherits ServiceBase
        Implements ICloseWindowService

        Public Sub Close() Implements ICloseWindowService.Close
            If (TypeOf AssociatedObject Is Window) Then
                CType(AssociatedObject, Window).Close()
            End If
        End Sub
    End Class
End Namespace
