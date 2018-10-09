Imports DevExpress.Xpf.Core
Imports System.Windows

Namespace RichEditDemo
    Partial Public Class RibbonUICustomization
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub About_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
            DXMessageBox.Show("This demo illustrates how to customize the WPF Rich Text Editor's integrated ribbon UI." & ControlChars.Lf & ControlChars.Lf & "Switch to the Code View to learn how to use the Rich Text Editor's RibbonActions collection to create, remove or modify ribbon elements.", "Rich Text Editor Ribbon Customization", MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub
    End Class
End Namespace
