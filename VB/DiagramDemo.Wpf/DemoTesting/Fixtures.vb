Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System.Threading
Imports System.Windows.Threading
Imports System
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Helpers
Imports System.Windows
Imports System.Globalization
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports System.Linq

Namespace DiagramDemo.Tests
    Public Class DiagramCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return theme IsNot Theme.HybridApp
        End Function
    End Class
    Public Class DiagramDemoModulesAccessor
        Inherits DemoModulesAccessor(Of DiagramDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class
    Public MustInherit Class BaseDiagramDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private privateModuleAccessor As DiagramDemoModulesAccessor
        Protected Property ModuleAccessor() As DiagramDemoModulesAccessor
            Get
                Return privateModuleAccessor
            End Get
            Private Set(ByVal value As DiagramDemoModulesAccessor)
                privateModuleAccessor = value
            End Set
        End Property
        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub
        Protected MustOverride Function GetModulesAccessor() As DiagramDemoModulesAccessor
    End Class
End Namespace
