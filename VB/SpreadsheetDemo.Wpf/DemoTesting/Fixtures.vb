Imports System
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.DemoBase
Imports System.Linq

Namespace SpreadsheetDemo.Tests
    Public Class SpreadsheetCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
            If moduleType Is GetType(BindingToDataSource) Then
                Return False
            End If
            Return MyBase.CanRunModule(moduleType)
        End Function
    End Class
    Public Class SpreadsheetDemoModulesAccessor
        Inherits DemoModulesAccessor(Of SpreadsheetDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class
    Public MustInherit Class BaseSpreadsheetDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private privateModuleAccessor As SpreadsheetDemoModulesAccessor
        Protected Property ModuleAccessor() As SpreadsheetDemoModulesAccessor
            Get
                Return privateModuleAccessor
            End Get
            Private Set(ByVal value As SpreadsheetDemoModulesAccessor)
                privateModuleAccessor = value
            End Set
        End Property
        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub
        Protected MustOverride Function GetModulesAccessor() As SpreadsheetDemoModulesAccessor
    End Class
End Namespace
