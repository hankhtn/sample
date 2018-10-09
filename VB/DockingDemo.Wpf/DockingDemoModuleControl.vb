Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Docking

Namespace DockingDemo
    Public Class DockingDemoModule
        Inherits DemoModule

        Public Shared ReadOnly ShouldRestoreOnActivateProperty As DependencyProperty = DependencyProperty.RegisterAttached("ShouldRestoreOnActivate", GetType(Boolean), GetType(DockingDemoModule))

        Public Sub New()
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler ModuleLoaded, AddressOf OnModuleLoaded
        End Sub
        Shared Sub New()
            NWindContext.Preload()
        End Sub

        Private privateIsModuleLoaded As Boolean
        Protected Property IsModuleLoaded() As Boolean
            Get
                Return privateIsModuleLoaded
            End Get
            Private Set(ByVal value As Boolean)
                privateIsModuleLoaded = value
            End Set
        End Property

        Public Shared Function GetShouldRestoreOnActivate(ByVal target As DependencyObject) As Boolean
            Return DirectCast(target.GetValue(ShouldRestoreOnActivateProperty), Boolean)
        End Function
        Public Shared Sub SetShouldRestoreOnActivate(ByVal target As DependencyObject, ByVal value As Boolean)
            target.SetValue(ShouldRestoreOnActivateProperty, value)
        End Sub
        Protected Overrides Sub HidePopupContent()
            Dim containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(Me).OfType(Of DockLayoutManager)()
            For Each contianer In containerList
                HideFloatGroups(contianer)
            Next contianer
            MyBase.HidePopupContent()
        End Sub
        Protected Sub NavigateHomePage()
            System.Diagnostics.Process.Start("http://www.devexpress.com")
        End Sub
        Protected Overridable Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If IsModuleLoaded Then
                Return
            End If
            Dim containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(Me).OfType(Of DockLayoutManager)()
            For Each contianer In containerList
                HideFloatGroups(contianer)
            Next contianer
        End Sub
        Protected Sub ShowAbout()
            Dim platformName As String = "WPF"
            Dim ati As New AboutToolInfo()
            ati.LicenseState = LicenseState.Licensed
            ati.ProductEULAUri = "http://www.devexpress.com/"
            ati.ProductName = "DXDocking for " & platformName
            ati.Version = AssemblyInfo.MarketingVersion

            Dim tAbout As DevExpress.Xpf.ToolAbout = New ToolAbout(ati)
            Dim aWindow As New AboutWindow()
            aWindow.Content = tAbout
            aWindow.ShowDialog()
        End Sub
        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            Dim containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(Me).OfType(Of DockLayoutManager)()
            For Each contianer In containerList
                ShowFloatGroups(contianer)
            Next contianer
        End Sub
        Private Sub HideFloatGroups(ByVal dockLayoutManager As DockLayoutManager)
            For Each floatGroup As FloatGroup In dockLayoutManager.FloatGroups
                If floatGroup.IsOpen Then
                    SetShouldRestoreOnActivate(floatGroup, True)
                    floatGroup.IsOpen = False
                End If
            Next floatGroup
        End Sub
        Private Sub OnModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            IsModuleLoaded = True
        End Sub
        Private Sub ShowFloatGroups(ByVal dockLayoutManager As DockLayoutManager)
            For Each floatGroup As FloatGroup In dockLayoutManager.FloatGroups
                If GetShouldRestoreOnActivate(floatGroup) Then
                    SetShouldRestoreOnActivate(floatGroup, False)
                    If Not dockLayoutManager.IsVisible Then
                        floatGroup.ShouldRestoreOnActivate = True
                    Else
                        floatGroup.IsOpen = True
                    End If
                End If
            Next floatGroup
        End Sub
    End Class
End Namespace
