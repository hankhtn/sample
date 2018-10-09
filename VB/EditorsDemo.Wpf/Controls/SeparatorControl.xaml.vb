Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace EditorsDemo
    Partial Public Class SeparatorControl
        Inherits UserControl

        Public Shared ReadOnly HeaderProperty As DependencyProperty

        Shared Sub New()
            HeaderProperty = DependencyProperty.Register("Header", GetType(String), GetType(SeparatorControl))
        End Sub

        Public Sub New()
            InitializeComponent()
            DataContext = Me
        End Sub

        Public Property Header() As String
            Get
                Return DirectCast(GetValue(HeaderProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(HeaderProperty, value)
            End Set
        End Property
    End Class
End Namespace
