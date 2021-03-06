Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.XtraRichEdit
Imports DevExpress.Xpf.RichEdit

Namespace RichEditDemo.Tests
    Public Class RichEditDemoModuleAccessor
        Inherits DemoModulesAccessor(Of RichEditDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
        Public ReadOnly Property RichEditControl() As RichEditControl
            Get
                Return DemoModule.RichEditControl
            End Get
        End Property
    End Class
    Public MustInherit Class BaseRichEditTestingFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As RichEditDemoModuleAccessor
        Public Sub New()
            Me.modulesAccessor = New RichEditDemoModuleAccessor(Me)
        End Sub
        Public ReadOnly Property RichEditControl() As RichEditControl
            Get
                Return Me.modulesAccessor.RichEditControl
            End Get
        End Property
    End Class
    Public Class FakeModuleFixture
        Inherits BaseRichEditTestingFixture

    End Class
    Public Class RichEditCheckAllDemosFixture
        Inherits CheckAllDemosFixture

    End Class
End Namespace
