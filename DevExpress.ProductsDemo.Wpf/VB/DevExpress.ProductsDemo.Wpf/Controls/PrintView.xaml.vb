Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace ProductsDemo
    Partial Public Class PrintView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            AddHandler IsVisibleChanged, AddressOf PrintView_IsVisibleChanged
        End Sub

        Private ReadOnly Property PrintViewModel() As PrintViewModel
            Get
                Return TryCast(DataContext, PrintViewModel)
            End Get
        End Property

        Private Sub PrintView_IsVisibleChanged(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)
            If IsVisible AndAlso PrintViewModel IsNot Nothing Then
                PrintViewModel.UpdatePrintableControlLink()
            End If
        End Sub

    End Class
End Namespace
