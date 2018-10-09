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

Namespace DevExpress.DevAV.Views
    Partial Public Class StaticFiltersPanel
        Inherits UserControl

        Public Property Title() As String
            Get
                Return DirectCast(GetValue(TitleProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property
        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(StaticFiltersPanel), New PropertyMetadata(Nothing))

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
