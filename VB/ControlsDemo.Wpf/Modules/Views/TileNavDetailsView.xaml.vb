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

Namespace ControlsDemo.Modules.Views



    Partial Public Class TileNavDetailsView
        Inherits NavigationPage

        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(TileNavDetailsView), Nothing)
        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(ByVal e As DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs)
            MyBase.OnNavigatedTo(e)
            Title = String.Format("{0} Details", e.Parameter)
            DataContext = e.Parameter
        End Sub
        Public Property Title() As String
            Get
                Return CStr(GetValue(TitleProperty))
            End Get
            Set(ByVal value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property
    End Class
End Namespace
