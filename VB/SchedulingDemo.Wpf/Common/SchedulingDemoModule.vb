Imports System
Imports System.Windows
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Scheduling

Namespace SchedulingDemo
    Public Class SchedulingDemoModule
        Inherits DemoModule

        Shared Sub New()
            NWindContext.Preload()
        End Sub


        Private Shared commonResources_Renamed As ResourceDictionary
        Private Shared ReadOnly Property CommonResources() As ResourceDictionary
            Get
                If commonResources_Renamed IsNot Nothing Then
                    Return commonResources_Renamed
                Else
                    commonResources_Renamed = New ResourceDictionary() With {.Source = New Uri("/SchedulingDemo;component/Themes/Common.xaml", UriKind.RelativeOrAbsolute)}
                    Return commonResources_Renamed
                End If
            End Get
        End Property

        Private Shared outlookInspiredDemoResources_Renamed As ResourceDictionary
        Private Shared ReadOnly Property OutlookInspiredDemoResources() As ResourceDictionary
            Get
                If outlookInspiredDemoResources_Renamed IsNot Nothing Then
                    Return outlookInspiredDemoResources_Renamed
                Else
                    outlookInspiredDemoResources_Renamed = New ResourceDictionary() With {.Source = New Uri("/SchedulingDemo;component/Themes/OutlookInspired.xaml", UriKind.RelativeOrAbsolute)}
                    Return outlookInspiredDemoResources_Renamed
                End If
            End Get
        End Property

        Private addCommonResources As Boolean
        Private addOutlookInspiredDemoResources As Boolean
        Public Sub New(Optional ByVal addCommonResources As Boolean = True, Optional ByVal addOutlookInspiredDemoResources As Boolean =False)
            Me.addCommonResources = addCommonResources
            Me.addOutlookInspiredDemoResources = addOutlookInspiredDemoResources
            If addCommonResources Then
                Resources.MergedDictionaries.Add(CommonResources)
            End If
            If addOutlookInspiredDemoResources Then
                Resources.MergedDictionaries.Add(OutlookInspiredDemoResources)
            End If
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme() As Boolean
            Get
                Return False
            End Get
        End Property
        Private Property Scheduler() As SchedulerControl

        Protected Overridable Sub LoadDemoData()
        End Sub

        Protected Overrides Sub Clear()
            MyBase.Clear()
            Scheduler.DataSource = Nothing
        End Sub

        Protected Overrides Sub OnInitialized(ByVal e As EventArgs)
            MyBase.OnInitialized(e)
            If Me.addCommonResources AndAlso (Not Resources.MergedDictionaries.Contains(CommonResources)) Then
                Throw New InvalidOperationException()
            End If
            If Me.addOutlookInspiredDemoResources AndAlso (Not Resources.MergedDictionaries.Contains(OutlookInspiredDemoResources)) Then
                Throw New InvalidOperationException()
            End If
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Scheduler = LayoutHelper.FindElementByType(Of SchedulerControl)(Me)
        End Sub
    End Class
End Namespace
