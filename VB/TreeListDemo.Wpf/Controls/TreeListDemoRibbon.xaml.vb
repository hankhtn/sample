Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid

Namespace TreeListDemo
    Partial Public Class TreeListDemoRibbon
        Inherits UserControl

       Public Shared ReadOnly ExportTreeListProperty As DependencyProperty = DependencyProperty.Register("ExportTreeList", GetType(TreeListControl), GetType(TreeListDemoRibbon), New PropertyMetadata(Nothing))
        Public Property ExportTreeList() As TreeListControl
            Get
                Return DirectCast(GetValue(ExportTreeListProperty), TreeListControl)
            End Get
            Set(ByVal value As TreeListControl)
                SetValue(ExportTreeListProperty, value)
            End Set
        End Property
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
