Imports DevExpress.Xpf.DemoBase
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

Namespace PropertyGridDemo
    <CodeFile("ViewModels/TabbedViewModule/Model/Metadata/BorderOptions.(cs)"), CodeFile("ViewModels/TabbedViewModule/Model/Metadata/Effects.(cs)"), CodeFile("ViewModels/TabbedViewModule/Model/Metadata/FillOptions.(cs)"), CodeFile("ViewModels/TabbedViewModule/Model/Metadata/SeriesOptions.(cs)"), CodeFile("Resources/TabbedViewModuleResources/Resources.xaml"), CodeFile("Resources/TabbedViewModuleResources/Templates/Effects.xaml"), CodeFile("Resources/TabbedViewModuleResources/Templates/FillnLine.xaml"), CodeFile("Resources/TabbedViewModuleResources/Templates/Misc.xaml"), CodeFile("Resources/TabbedViewModuleResources/Templates/SeriesOptions.xaml")>
    Partial Public Class TabbedViewModule
        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub OnCustomExpand(ByVal sender As Object, ByVal args As DevExpress.Xpf.PropertyGrid.CustomExpandEventArgs)
            args.IsExpanded = True
        End Sub
    End Class
End Namespace
