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

Namespace EditorsDemo.Tests
    Public Class EditorsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

    End Class
    Public Class EditorsDemoModulesAccessor
        Inherits DemoModulesAccessor(Of EditorsDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class
    Public MustInherit Class BaseEditorsDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private privateModuleAccessor As EditorsDemoModulesAccessor
        Protected Property ModuleAccessor() As EditorsDemoModulesAccessor
            Get
                Return privateModuleAccessor
            End Get
            Private Set(ByVal value As EditorsDemoModulesAccessor)
                privateModuleAccessor = value
            End Set
        End Property
        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub
        Protected MustOverride Function GetModulesAccessor() As EditorsDemoModulesAccessor
    End Class
End Namespace
