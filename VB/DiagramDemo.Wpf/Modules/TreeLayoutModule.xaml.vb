Imports DevExpress.Diagram.Core
Imports DevExpress.Diagram.Core.Layout
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
Imports System.Xml.Serialization

Namespace DiagramDemo
    Partial Public Class TreeLayoutModule
        Inherits LayoutModuleBase

        Public Sub New()
            InitializeComponent()
            diagramControl.LoadDemoData("TreeLayoutDiagram.xml")
        End Sub
        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplyTreeLayout()
            diagramControl.FitToPage()
        End Sub
    End Class
End Namespace
