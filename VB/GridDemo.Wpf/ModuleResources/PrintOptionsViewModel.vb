Imports System
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Printing

Namespace GridDemo
    <POCOViewModel>
    Public Class PrintOptionsViewModel
        Inherits PrintViewModelBase

        Protected Sub New()
        End Sub
        Public Overridable Property UseCustomPrintStyles() As Boolean

        Public Overrides Sub OnLoaded(ByVal printableControl As IPrintableControl)
            MyBase.OnLoaded(printableControl)
            ShowPreviewInNewTab()
            UseCustomPrintStyles = True
            ShowPreviewInNewTab()
            UseCustomPrintStyles = False
        End Sub
        Protected Overrides Function GetTitle() As String
            Return (If(UseCustomPrintStyles, "Custom", "Default")) & " Style Print Preview"
        End Function

    End Class
End Namespace
