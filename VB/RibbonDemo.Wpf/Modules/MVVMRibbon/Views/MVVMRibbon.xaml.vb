Imports DevExpress.Xpf.DemoBase

Namespace RibbonDemo
    <CodeFiles("Modules/MVVMRibbon/Views/MVVMRibbon.xaml;" & ControlChars.CrLf & "                 Modules/MVVMRibbon/Views/MVVMRibbon.xaml.(cs);" & ControlChars.CrLf & "                 Modules/MVVMRibbon/ViewModels/MVVMRibbonCommands.(cs);" & ControlChars.CrLf & "                 Modules/MVVMRibbon/ViewModels/MVVMRibbonViewModel.(cs)")>
    Partial Public Class MVVMRibbon
        Inherits RibbonDemoModule

        Public Sub New()
            InitializeComponent()
            Ribbon = ribbonControl
        End Sub
    End Class
End Namespace
