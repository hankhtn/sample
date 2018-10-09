Imports System.Windows
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Diagram.Core
Imports DevExpress.Diagram.Demos
Imports DevExpress.Diagram.Core.Routing

Namespace DiagramDemo
    Partial Public Class MindMapTreeLayoutModule
        Inherits LayoutModuleBase

        Public Sub New()
            InitializeComponent()
            diagramControl.LoadDemoData("MindMapTreeLayoutDiagram.xml")
            diagramControl.RegisterRoutingStrategy(ConnectorType.Curved, New OrgChartRoutingStrategy())
        End Sub
        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplyMindMapTreeLayout()
            diagramControl.FitToPage()
        End Sub
        Private Sub CreateChild(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim parent = TryCast(diagramControl.PrimarySelection, DiagramItem)
            Dim size = MindMapHelpers.GetSize(OrgChartHelpers.GetItemLevel(parent) + 1)
            Dim child = New DiagramShape() With {.Shape = BasicShapes.Ellipse, .Width = size.Width, .Height = size.Height, .FontSize = MindMapHelpers.GetFontSize(OrgChartHelpers.GetItemLevel(parent) + 1), .Content = "New Item"}
            child.ThemeStyleId = MindMapHelpers.GetMindMapStyle(child, parent)
            diagramControl.Items.Add(child)
            diagramControl.Items.Add(New DiagramConnector() With {.Type = ConnectorType.Curved, .ThemeStyleId = child.ThemeStyleId, .StrokeThickness = 3, .BeginItem = parent, .EndItem = child})
            RelayoutDiagramCore()
        End Sub
    End Class
End Namespace
