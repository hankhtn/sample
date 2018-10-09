Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Xml.Serialization
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Commands
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid


Namespace TreeListDemo
    Partial Public Class BandedLayout
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class BandedViewViewModel
        Public ReadOnly Property SpaceObjects() As IList(Of SpaceObjects)
            Get
                Return SpaceObjectData.DataSource
            End Get
        End Property
    End Class
End Namespace
