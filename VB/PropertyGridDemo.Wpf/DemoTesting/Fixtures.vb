Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System.Threading
Imports System.Windows.Threading
Imports System
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Helpers
Imports System.Windows
Imports System.Globalization
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports System.Linq

Namespace PropertyGridDemo.Tests
    Public Class PropertyGridCheckAllDemosFixture
        Inherits CheckAllDemosFixture

    End Class
    Public Class PropertyGridDemoModulesAccessor
        Inherits DemoModulesAccessor(Of PropertyGridDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class
    Public MustInherit Class BasePropertyGridDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private privateModuleAccessor As PropertyGridDemoModulesAccessor
        Protected Property ModuleAccessor() As PropertyGridDemoModulesAccessor
            Get
                Return privateModuleAccessor
            End Get
            Private Set(ByVal value As PropertyGridDemoModulesAccessor)
                privateModuleAccessor = value
            End Set
        End Property
        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub
        Protected MustOverride Function GetModulesAccessor() As PropertyGridDemoModulesAccessor
    End Class
End Namespace
