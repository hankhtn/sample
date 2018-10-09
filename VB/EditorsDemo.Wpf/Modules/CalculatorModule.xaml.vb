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
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core
Imports System.Windows.Threading
Imports DevExpress.Xpf.Core.Native

Namespace EditorsDemo



    Partial Public Class CalculatorModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub CalculatorCustomErrorText(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.CalculatorCustomErrorTextEventArgs)
            If cbCustomErrorText.IsChecked = True Then
                e.ErrorText &= " !!!"
            End If
        End Sub
        Private Sub ShowCalculatorWindow(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim calculator As New Calculator() With {.ShowBorder = False}
            Dim container As FloatingContainer = FloatingContainer.ShowDialog(calculator, editor, New Size(1, 1), New FloatingContainerParameters() With {.Title = "Calculator"})
            container.SizeToContent = SizeToContent.WidthAndHeight
            container.ContainerStartupLocation = WindowStartupLocation.CenterOwner
            calculator.Focus()
        End Sub
        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            calculator.ClearHistory()
        End Sub
    End Class

    #Region "Move to Utils"
    Public Class ObjectList
        Inherits List(Of Object)

    End Class
    #End Region
End Namespace
