Imports System
Imports System.Windows
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core

Namespace DevExpress.DevAV
    Partial Public Class MainWindow
        Inherits ThemedWindow

        Public Sub New()
            InitializeComponent()
            If Height > SystemParameters.VirtualScreenHeight OrElse Width > SystemParameters.VirtualScreenWidth Then
                WindowState = WindowState.Maximized
            End If
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, Me)
            EventManager.RegisterClassHandler(GetType(BarItem), BarItem.ItemClickEvent, New ItemClickEventHandler(AddressOf OnBarItemClick), True)
            Xpf.Core.ThemeManager.AddThemeChangingHandler(Me, Sub(s, e) Xpf.DemoBase.Helpers.Logger.Log(String.Format("OutlookInspiredApp: Change Theme: {0}", Xpf.Core.ApplicationThemeHelper.ApplicationThemeName)))
        End Sub

        Private Sub MainWindowLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Left < 0 OrElse Top < 0 Then
                WindowState = WindowState.Maximized
            End If
        End Sub
        Private Sub OnBarItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            Dim barItem = DirectCast(sender, BarItem)
            Dim content = If(barItem.Content, barItem.ActualCustomizationContent)
            If content IsNot Nothing Then
                Xpf.DemoBase.Helpers.Logger.Log(String.Format("OutlookInspiredApp: BarItemClick: {0}", content.ToString()))
            End If
        End Sub
    End Class
End Namespace
