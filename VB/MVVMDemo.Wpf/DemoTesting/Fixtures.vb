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

Namespace MVVMDemo.Tests
    Public Class MVVMCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return theme IsNot Theme.HybridApp
        End Function
    End Class
    Public Class MVVMDemoModulesAccessor
        Inherits DemoModulesAccessor(Of MVVMDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class
    Public MustInherit Class BaseMVVMDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private privateModuleAccessor As MVVMDemoModulesAccessor
        Protected Property ModuleAccessor() As MVVMDemoModulesAccessor
            Get
                Return privateModuleAccessor
            End Get
            Private Set(ByVal value As MVVMDemoModulesAccessor)
                privateModuleAccessor = value
            End Set
        End Property
        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub
        Protected MustOverride Function GetModulesAccessor() As MVVMDemoModulesAccessor
    End Class
End Namespace
