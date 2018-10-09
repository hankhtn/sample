Imports System.Windows
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.XtraReports
Imports DevExpress.Mvvm.UI

Namespace SchedulingDemo
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
    Public Class CustomBackstageDocumentPreview
        Inherits BackstageDocumentPreview

        Public Shared ReadOnly DocumentSourceProperty As DependencyProperty = DependencyProperty.Register("DocumentSource", GetType(IReport), GetType(CustomBackstageDocumentPreview), New PropertyMetadata(Nothing))
        Public Property DocumentSource() As IReport
            Get
                Return DirectCast(GetValue(DocumentSourceProperty), IReport)
            End Get
            Set(ByVal value As IReport)
                SetValue(DocumentSourceProperty, value)
            End Set
        End Property

        Public Sub New()
            ExportFormats = New String() { "Pdf", "Htm", "Image" }
        End Sub
    End Class
End Namespace
