Imports DevExpress.Diagram.Core
Imports DevExpress.Diagram.Demos

Namespace DiagramDemo
    Partial Public Class FloorPlanModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeStencils()
            diagramControl.DocumentSource = DiagramDemoHelper.GetDataFilePath("OfficePlan.xml")
        End Sub
        Private homeObjectsStencil As DiagramStencil

        Private Sub InitializeStencils()
            Me.homeObjectsStencil = DemoHelper.CreatePredefinedSvgStencil("HomeObjects", "Home Objects", True)
            diagramControl.Stencils = DemoHelper.CreateExtendedStencilCollection(homeObjectsStencil)
        End Sub
        Private Sub diagramControl_ItemInitializing(ByVal sender As Object, ByVal e As DevExpress.Xpf.Diagram.DiagramItemInitializingEventArgs)
            DemoHelper.InitializeSvgShape(homeObjectsStencil, TryCast(e.Item, IDiagramShape))
        End Sub
    End Class
End Namespace
