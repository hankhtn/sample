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
Imports DevExpress.Xpf.WindowsUI
Imports DevExpress.Xpf.WindowsUI.Navigation

Namespace WindowsUIDemo.Modules.Views



    Partial Public Class SlideViewEmployeesDetailsView
        Inherits NavigationPage

        Public Shared ReadOnly ParameterProperty As DependencyProperty = DependencyProperty.Register("Parameter", GetType(Object), GetType(SlideViewEmployeesDetailsView), Nothing)
        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(ByVal e As DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs)
            MyBase.OnNavigatedTo(e)
            Parameter = e.Parameter
        End Sub
        Private Sub DataLayoutControl_AutoGeneratingItem(ByVal sender As Object, ByVal e As DevExpress.Xpf.LayoutControl.DataLayoutControlAutoGeneratingItemEventArgs)
            If e.PropertyName = "Photo" Then
                e.Cancel = True
            End If
        End Sub
        Public Property Parameter() As Object
            Get
                Return DirectCast(GetValue(ParameterProperty), Object)
            End Get
            Set(ByVal value As Object)
                SetValue(ParameterProperty, value)
            End Set
        End Property
    End Class
End Namespace
