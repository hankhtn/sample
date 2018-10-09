Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Printing
Imports System
Namespace ProductsDemo
    Public Class PrintViewModel
        Inherits ViewModelBase

        Public Overridable Property PrintableControlLink() As PrintableControlLink

        Public Sub UpdatePrintableControlLink()
            If PrintingService.PrintableControlLink IsNot Nothing Then
                PrintableControlLink = PrintingService.PrintableControlLink
                PrintableControlLink.CreateDocument(True)
            End If
        End Sub
    End Class
    Public NotInheritable Class PrintingService

        Private Sub New()
        End Sub
        Public Shared PrintableControlLink As PrintableControlLink

        Public Shared ReadOnly Property HasPrinting() As Boolean
            Get
                Return PrintableControlLink IsNot Nothing
            End Get
        End Property
    End Class
End Namespace
