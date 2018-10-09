Imports System
Imports System.Linq
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.DemoBase

Namespace WindowsUIDemo
    Public Class WindowsUIDemoModule
        Inherits DemoModule

        Shared Sub New()
            NWindContext.Preload()
        End Sub
    End Class
End Namespace
