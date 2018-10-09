Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.WindowsUI
Imports DevExpress.Xpf.WindowsUI.Navigation

Namespace ProductsDemo
    Partial Public Class MainWindow
        Inherits ThemedWindow

        Public Sub New()
            InitializeComponent()
            If Height > SystemParameters.VirtualScreenHeight OrElse Width > SystemParameters.VirtualScreenWidth Then
                WindowState = WindowState.Maximized
            End If
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, Me)
        End Sub
        Private Sub OnDocumentFrameNavigating(ByVal sender As Object, ByVal e As NavigatingEventArgs)
            If e.Cancel Then
                Return
            End If
            Dim frame As NavigationFrame = DirectCast(sender, NavigationFrame)
            Dim oldContent As FrameworkElement = CType(frame.Content, FrameworkElement)
            If oldContent IsNot Nothing Then
                RibbonMergingHelper.SetMergeWith(oldContent, Nothing)
                RibbonMergingHelper.SetMergeStatusBarWith(oldContent, Nothing)
            End If
        End Sub
        Private Sub OnDocumentFrameNavigated(ByVal sender As Object, ByVal e As NavigationEventArgs)
            Dim newContent As FrameworkElement = CType(e.Content, FrameworkElement)
            If newContent IsNot Nothing Then
                RibbonMergingHelper.SetMergeWith(newContent, ribbon)
                RibbonMergingHelper.SetMergeStatusBarWith(newContent, statusBar)
            End If
        End Sub
    End Class
End Namespace
