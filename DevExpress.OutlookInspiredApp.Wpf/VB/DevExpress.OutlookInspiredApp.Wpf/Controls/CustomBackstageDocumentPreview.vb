Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.Controls
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
    End Class
End Namespace
