Imports System
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Printing

Namespace GridDemo
    <POCOViewModel>
    Public Class PrintMasterDetailViewModel
        Inherits PrintViewModelBase

        Protected Sub New()
        End Sub

        Public Overrides Sub OnLoaded(ByVal printableControl As IPrintableControl)
            MyBase.OnLoaded(printableControl)
            ShowPreviewInNewTab()
        End Sub

        Protected Overrides Function GetTitle() As String
            Return "Print Preview"
        End Function
    End Class
End Namespace
