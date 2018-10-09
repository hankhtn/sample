Imports System
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Docking
Imports Microsoft.Win32

Namespace VisualStudioDocking
    Public Interface IDockingSerializationDialogService
        Sub SaveLayout()
        Sub LoadLayout()
    End Interface
    Public Class DockingSerializationDialogService
        Inherits ServiceBase
        Implements IDockingSerializationDialogService

        Private Const filter As String = "Configuration (*.xml)|*.xml|All files (*.*)|*.*"
        Public Property DockLayoutManager() As DockLayoutManager
        Public Sub LoadLayout() Implements IDockingSerializationDialogService.LoadLayout
            Dim openFileDialog As New OpenFileDialog() With {.Filter = filter}
            Dim openResult = openFileDialog.ShowDialog()
            If openResult.HasValue AndAlso openResult.Value Then
                DockLayoutManager.RestoreLayoutFromXml(openFileDialog.FileName)
            End If
        End Sub
        Public Sub SaveLayout() Implements IDockingSerializationDialogService.SaveLayout
            Dim saveFileDialog As New SaveFileDialog() With {.Filter = filter}
            Dim saveResult = saveFileDialog.ShowDialog()
            If saveResult.HasValue AndAlso saveResult.Value Then
                DockLayoutManager.SaveLayoutToXml(saveFileDialog.FileName)
            End If
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            DockLayoutManager = TryCast(AssociatedObject, DockLayoutManager)
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            DockLayoutManager = Nothing
        End Sub
    End Class
End Namespace
