Imports DevExpress.DemoData.Utils
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Mvvm.POCO
Imports System.Reflection

Namespace PdfViewerDemo
    <CodeFile("ViewModels/MainViewModel.(cs)")>
    Partial Public Class MainDemoModule
        Inherits PdfViewerDemoModule

        Public Sub New()
            InitializeComponent()
            Dim currentAssembly = System.Reflection.Assembly.GetExecutingAssembly()
            DataContext = ViewModelSource.Create(Function() New MainViewModel With {.PdfStream = AssemblyHelper.GetResourceStream(currentAssembly, "Data/Demo.pdf", True)})
        End Sub
    End Class
End Namespace
