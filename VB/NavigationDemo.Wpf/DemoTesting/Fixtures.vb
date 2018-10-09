Imports System
Imports System.Collections
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Editors

Namespace NavigationDemo.Tests
    Public Class AccordionCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Private skipMemoryLeaksCheckModules() As Type = { GetType(FileSearchDemoModule) }
        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not skipMemoryLeaksCheckModules.Contains(moduleType)
        End Function
        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return theme IsNot Theme.HybridApp
        End Function
    End Class
    Public Class AccordionDemoModulesAccessor
        Inherits DemoModulesAccessor(Of AccordionDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class
    Public MustInherit Class BaseAccordionDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private privateModuleAccessor As AccordionDemoModulesAccessor
        Protected Property ModuleAccessor() As AccordionDemoModulesAccessor
            Get
                Return privateModuleAccessor
            End Get
            Private Set(ByVal value As AccordionDemoModulesAccessor)
                privateModuleAccessor = value
            End Set
        End Property
        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub
        Protected MustOverride Function GetModulesAccessor() As AccordionDemoModulesAccessor
    End Class
End Namespace
