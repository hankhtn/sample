Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Layout.Core

Namespace DockingDemo
    Partial Public Class Serialization
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private memoryStream As Stream
        Protected Overridable Sub SaveLayout()
            Ref.Dispose(memoryStream)
            memoryStream = New MemoryStream()
            dockManager.SaveLayoutToStream(memoryStream)
            deserializeButton.IsEnabled = True
        End Sub
        Protected Overridable Sub RestoreLayout()
            If memoryStream Is Nothing Then
                Return
            End If
            memoryStream.Seek(0, SeekOrigin.Begin)
            dockManager.RestoreLayoutFromStream(memoryStream)
        End Sub
        Private Sub serializeButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SaveLayout()
        End Sub
        Private Sub deserializeButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RestoreLayout()
        End Sub
        Private Sub loadSampleLayoutButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim thisExe As System.Reflection.Assembly = GetType(Serialization).Assembly
            Dim name As String = String.Format("{0}.xml", layoutSampleName.SelectedItem.ToString().ToLower())
            Using resourceStream As Stream = AssemblyHelper.GetEmbeddedResourceStream(thisExe, DemoHelper.GetPath("Layouts/", thisExe) & name, True)
                dockManager.RestoreLayoutFromStream(resourceStream)
            End Using
        End Sub
    End Class
End Namespace
