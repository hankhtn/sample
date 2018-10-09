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
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports Microsoft.Win32
Imports System.IO
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.Docking
Imports RibbonDemo
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DemoBase

Namespace RibbonDemo
    <CodeFiles("Modules/RibbonMerging/Views/RibbonMergingUserControl.xaml;" & ControlChars.CrLf & "                 Modules/RibbonMerging/Views/RibbonMergingUserControl.xaml.(cs);" & ControlChars.CrLf & "                 Modules/RibbonMerging/Views/PaintUserControl.xaml;" & ControlChars.CrLf & "                 Modules/RibbonMerging/Views/PaintUserControl.xaml.(cs);" & ControlChars.CrLf & "                 Modules/RibbonMerging/Views/TextUserControl.xaml;" & ControlChars.CrLf & "                 Modules/RibbonMerging/Views/TextUserControl.xaml.(cs);                 " & ControlChars.CrLf & "                 Modules/RibbonMerging/ViewModels/PaintPanelViewModel.(cs);" & ControlChars.CrLf & "                 Modules/RibbonMerging/ViewModels/TextPanelViewModel.(cs);" & ControlChars.CrLf & "                 Modules/RibbonMerging/ViewModels/PanelViewModel.(cs);" & ControlChars.CrLf & "                 Modules/RibbonMerging/ViewModels/RibbonMergingViewModel.(cs);")>
    Partial Public Class RibbonMergingUserControl
        Inherits RibbonDemoModule

        Public Sub New()
            InitializeComponent()
            Ribbon = mainRibbon
        End Sub

        Protected Overrides Sub Hide()
            mainRibbon.CloseApplicationMenu()
            MyBase.Hide()
        End Sub
    End Class
End Namespace
