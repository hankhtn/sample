Imports System

Namespace DockingDemo
    Partial Public Class FloatingPanels
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler DesktopContainer.Loaded, AddressOf OnDesktopContainerLoaded
        End Sub
        Private Sub OnDesktopContainerLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            RemoveHandler DesktopContainer.Loaded, AddressOf OnDesktopContainerLoaded
            ShowWindowModeContainer()
        End Sub
        Private isWindow As Boolean
        Private Sub OnFloatingModeButtonlick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            If isWindow Then
                ShowDesktopModeContainer()
            Else
                ShowWindowModeContainer()
            End If
        End Sub
        Private Sub ShowWindowModeContainer()
            ModeSwitchButton.Content = "Show in Desktop Mode"
            WindowContainer.Visibility = System.Windows.Visibility.Visible
            DesktopContainer.Visibility = System.Windows.Visibility.Collapsed
            isWindow = True
        End Sub
        Private Sub ShowDesktopModeContainer()
            ModeSwitchButton.Content = "Show in Window Mode"
            DesktopContainer.Visibility = System.Windows.Visibility.Visible
            WindowContainer.Visibility = System.Windows.Visibility.Collapsed
            isWindow = False
        End Sub
    End Class
End Namespace
