Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace DockingDemo
    Partial Public Class IDEWorkspaces
        Inherits DockingDemoModule

        Private Manager As IWorkspaceManager
        Private workspaceNumber As Integer = 1
        Public Sub New()
            InitializeComponent()
            Manager = WorkspaceManager.GetWorkspaceManager(DemoDockContainer)
            Manager.TransitionEffect = TransitionEffect.Ripple
            LoadPredefinedWorkspaces()
        End Sub
        Private Sub LoadPredefinedWorkspaces()
            workspaces.Items.Clear()
            Dim thisExe As System.Reflection.Assembly = GetType(IDEWorkspaces).Assembly
            Dim i As Integer = 1
            Do While i <= 4
                Dim workspaceName As String = String.Format("workspace{0}", i)
                Dim fileName As String = String.Format("{0}.xml", workspaceName)
                Using resourceStream As Stream = AssemblyHelper.GetEmbeddedResourceStream(thisExe, DemoHelper.GetPath("Layouts/", thisExe) & fileName, True)
                    Manager.LoadWorkspace(workspaceName, resourceStream)
                End Using
                workspaces.Items.Add(workspaceName)
                i += 1
                workspaceNumber += 1
            Loop
        End Sub
        Private Sub captureWorkspace_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim name As String = String.Format("workspace{0}", workspaceNumber)
            workspaceNumber += 1
            Manager.CaptureWorkspace(name)
            workspaces.Items.Add(name)
            If workspaces.Visibility = Visibility.Collapsed Then
                workspaces.Visibility = Visibility.Visible
            End If
        End Sub
        Private Sub removeWorkspace_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim name As String = TryCast(workspaces.SelectedItem, String)
            If name Is Nothing Then
                Return
            End If
            Manager.RemoveWorkspace(name)
            workspaces.Items.Remove(name)
            If workspaces.Items.Count = 0 Then
                workspaces.SelectedIndex = -1
                workspaces.Visibility = Visibility.Collapsed
            End If
        End Sub
        Private Sub workspaces_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            Dim name As String = TryCast(workspaces.SelectedItem, String)
            If name IsNot Nothing Then
                Manager.ApplyWorkspace(name)
            End If
        End Sub
    End Class
End Namespace
