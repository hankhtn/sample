Imports System
Imports System.Collections.Generic
Imports System.Globalization
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
Imports DevExpress.Xpf
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Mvvm.Native

Namespace ProductsDemo
    Partial Public Class InfoView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class AboutInfoToControlAboutConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim control As New ControlAbout(DirectCast(value, AboutInfo))
            AddHandler control.SizeChanged, AddressOf control_SizeChanged
            Return control
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
        Private Sub control_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            Dim control As ControlAbout = DirectCast(sender, ControlAbout)
            LayoutHelper.FindElementByName(control, "CloseButton").Do(Sub(x) x.Visibility = Visibility.Collapsed)
        End Sub
    End Class
End Namespace
