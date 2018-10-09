Imports DevExpress.SalesDemo.Model
Imports DevExpress.SalesDemo.Wpf
Imports DevExpress.Xpf.Core
Imports System.Windows

Namespace DevExpress.SalesDemo.Wpf
    Partial Public Class MainWindow
        Inherits ThemedWindow

        Public Sub New()
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, Me)
            InitializeComponent()
        End Sub
    End Class
End Namespace
