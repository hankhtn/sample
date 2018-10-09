Imports System
Imports System.Windows
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Utils.Themes

Namespace PropertyGridDemo
    Public Class PropertyGridDemoModule
        Inherits DemoModule

        Shared Sub New()
            Dim ownerType As Type = GetType(PropertyGridDemoModule)
        End Sub
        Protected Overridable ReadOnly Property NeedChangeEditorsTheme() As Boolean
            Get
                Return False
            End Get
        End Property
    End Class
End Namespace
