Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Diagram.Core
Imports DevExpress.Diagram.Demos
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo
    <CodeFile("Resources/ContentItemsStyles.xaml")>
    Partial Public Class CustomShapesModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeStencils()
            AddHandler diagramControl.DocumentLoaded, Sub(o, e) diagramControl.Items.Where(Function(item) item.Tag IsNot Nothing).ForEach(Sub(item) item.ToolTip = New TextBlock With {.Text = item.Tag.ToString(), .TextAlignment = TextAlignment.Left})
            diagramControl.DocumentSource = DiagramDemoHelper.GetDataFilePath("CustomShapesDocument_WPF.xml")
        End Sub
        Private svgStencil As DiagramStencil
        Private customShapesStencil As DiagramStencil
        Private contentItemsStencil As DiagramStencil

        Private Sub InitializeStencils()
            Me.customShapesStencil = DemoHelper.CreateStencilFromFile(DiagramDemoHelper.GetDataFilePath("CustomShapes.xml"), "CustomShapes", "Custom Shapes")
            Me.svgStencil = DemoHelper.CreatePredefinedSvgStencil("SvgShapes", "Svg Shapes")
            Me.contentItemsStencil = CreateContentItemsStencil()
            Me.diagramControl.Stencils = DemoHelper.CreateExtendedStencilCollection(svgStencil, customShapesStencil)
        End Sub
        Private Function CreateContentItemsStencil() As DiagramStencil
            Dim stencil As New DiagramStencil("CustomTools", "Content Item Tools", True)
            stencil.RegisterTool(New FactoryItemTool("Text", Function() "Text", Function(diagram) New DiagramContentItem() With {.CustomStyleId = "formattedTextContentItem"}, New Size(230, 110), True))
            stencil.RegisterTool(New FactoryItemTool("Action", Function() "Button", Function(diagram) New DiagramContentItem() With {.CustomStyleId = "buttonContentItem"}, New Size(230, 80), True))
            Return stencil
        End Function

        Private Sub diagramControl_ItemInitializing(ByVal sender As Object, ByVal e As DiagramItemInitializingEventArgs)
            DemoHelper.InitializeSvgShape(svgStencil, TryCast(e.Item, IDiagramShape))
        End Sub
    End Class
End Namespace
