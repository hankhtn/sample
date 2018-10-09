Imports DevExpress.Xpf.Printing
Imports System.Windows
Imports System.Windows.Controls

Namespace GridDemo
    Partial Public Class TabHeaderPrintInfoControl
        Inherits UserControl

        Public Property Link() As LinkBase

        Public Property TabName() As String
            Get
                Return DirectCast(GetValue(TabNameProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(TabNameProperty, value)
            End Set
        End Property
        Public Shared ReadOnly TabNameProperty As DependencyProperty = DependencyProperty.Register("TabName", GetType(String), GetType(TabHeaderPrintInfoControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnTabNameChanged)))
        Private Shared Sub OnTabNameChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, TabHeaderPrintInfoControl).OnTabNameChanged()
        End Sub
        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub OnTabNameChanged()
            tabNameTextBlock.Text = TabName
        End Sub
    End Class
End Namespace
