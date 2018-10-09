Imports System
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.DemoData.Utils
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.PdfViewer
Imports DevExpress.Xpf.PdfViewer.Helpers

Namespace ProductsDemo.Modules



    Partial Public Class PdfViewerDemo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            Dim source As Object = Utils.GetRelativePath("Demo.pdf")
            DataContext = ViewModelSource.Create(Function() New MainViewModel With {.PdfSource = source})
        End Sub
    End Class
    Public Class MainViewModel
        Public Overridable Property PdfSource() As Object
        Public Sub ShowNewDocument()
        End Sub
    End Class
    Public Class PdfDocumentAttachedBehavior
        Inherits Behavior(Of PdfViewerControl)

        Private ReadOnly Property Model() As MainViewModel
            Get
                Return TryCast(AssociatedObject.DataContext, MainViewModel)
            End Get
        End Property
    End Class
End Namespace
