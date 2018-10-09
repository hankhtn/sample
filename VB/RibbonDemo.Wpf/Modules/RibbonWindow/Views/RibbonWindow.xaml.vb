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
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace RibbonDemo
    <CodeFiles("Modules/RibbonWindow/Views/PainterWindow.xaml;" & ControlChars.CrLf & "                 Modules/RibbonWindow/Views/PainterWindow.xaml.(cs);" & ControlChars.CrLf & "                 Modules/RibbonWindow/ViewModels/PainterWindowViewModel.(cs);" & ControlChars.CrLf & "                 Modules/RibbonWindow/Services/CloseWindowService.(cs);" & ControlChars.CrLf & "                 Modules/RibbonWindow/Views/RibbonWindow.xaml;" & ControlChars.CrLf & "                 Modules/RibbonWindow/Views/RibbonWindow.xaml.(cs)")>
    Partial Public Class RibbonWindow
        Inherits RibbonDemoModule

        Public Sub New()
            DevExpress.Xpf.Bars.BarManager.CheckBarItemNames = False
            InitializeComponent()
        End Sub

        Private Sub ShowPainterWindow()
            Dim Window As New PainterWindow()
            Window.Width = Me.ActualWidth
            Window.Height = Me.ActualHeight
            Dim pos As Point = Me.PointToScreen(New Point())
            Window.Left = pos.X
            Window.Top = pos.Y
            Window.ShowDialog()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowPainterWindow()
        End Sub
    End Class
End Namespace
