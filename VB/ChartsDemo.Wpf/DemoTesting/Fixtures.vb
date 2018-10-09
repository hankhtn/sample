Imports System
Imports System.Linq
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace ChartsDemo.Tests
    Public Class ChartsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Private skipMemoryLeaksCheckModules() As Type = { }

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not skipMemoryLeaksCheckModules.Contains(moduleType)
        End Function
    End Class
End Namespace
