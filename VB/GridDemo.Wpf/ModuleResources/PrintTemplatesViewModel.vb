Imports System
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Printing

Namespace GridDemo
    <POCOViewModel>
    Public Class PrintTemplatesViewModel
        Inherits PrintViewModelBase

        Protected Sub New()
        End Sub
        Public Overridable Property Mode() As PrintTemplatesMode

        Public Overrides Sub OnLoaded(ByVal printableControl As IPrintableControl)
            MyBase.OnLoaded(printableControl)
            Mode = PrintTemplatesMode.Default
            ShowPreviewInNewTab()
            Mode = PrintTemplatesMode.MailMerge
            ShowPreviewInNewTab()
            Mode = PrintTemplatesMode.Detail
            ShowPreviewInNewTab()
        End Sub
        Protected Overrides Function GetTitle() As String
            Return Mode.ToString() & " Print Preview"
        End Function
    End Class
    Public Enum PrintTemplatesMode
        Detail
        MailMerge
        [Default]
    End Enum
End Namespace
