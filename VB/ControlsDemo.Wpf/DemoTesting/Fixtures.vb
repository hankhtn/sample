Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System
Imports System.Linq

Namespace ControlsDemo.Tests
    Public Class ControlsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Private skipMemoryLeaksCheckModules() As Type = { }
        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not skipMemoryLeaksCheckModules.Contains(moduleType)
        End Function
    End Class
End Namespace