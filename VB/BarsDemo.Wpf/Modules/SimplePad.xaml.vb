Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports System.Windows
Namespace BarsDemo
    <CodeFiles("Modules/SimplePad.xaml", "Modules/SimplePad.xaml.(cs)", "ViewModels/SimplePadViewModel.(cs)")>
    Partial Public Class SimplePad
        Inherits BarsDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler ModuleLoaded, AddressOf OnModuleLoaded
        End Sub

        Private Sub OnModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not Me.IsInDesignTool() Then
                richControl.SetFocus()
            End If
        End Sub
    End Class
End Namespace
