Imports System
Imports DevExpress.Xpf.DemoBase
Imports System.IO
Imports DevExpress.Internal
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Markup
Imports System.Windows.Media
Imports DevExpress.Utils
Imports System.Windows.Media.Imaging

Namespace MVVMDemo
    Public MustInherit Class MVVMDemoModule
        Inherits ShowcaseBrowserModule

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme() As Boolean
            Get
                Return False
            End Get
        End Property
        Protected Overridable Sub LoadDemoData()
        End Sub
    End Class
End Namespace
