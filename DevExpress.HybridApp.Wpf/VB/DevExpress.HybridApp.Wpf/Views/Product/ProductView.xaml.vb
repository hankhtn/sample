Imports System
Imports System.Linq
Imports System.Windows.Controls
Imports System.Collections.Generic

Namespace DevExpress.DevAV.Views
    Partial Public Class ProductView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub PdfViewerControl_ManipulationBoundaryFeedback(ByVal sender As Object, ByVal e As System.Windows.Input.ManipulationBoundaryFeedbackEventArgs)
            e.Handled = True
        End Sub
    End Class
End Namespace
