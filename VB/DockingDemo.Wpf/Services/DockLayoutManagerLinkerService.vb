Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Bars.Native
Imports DevExpress.Xpf.Docking
Imports System

Namespace DockingDemo
    Public Class DockLayoutManagerLinkerService
        Inherits ServiceBase

        <ThreadStatic>
        Private Shared managers As New WeakList(Of DockLayoutManager)()
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Dim manager As DockLayoutManager = TryCast(AssociatedObject, DockLayoutManager)
            If manager IsNot Nothing AndAlso (Not managers.Contains(manager)) Then
                SyncLock managers
                    For Each m In managers
                        DockLayoutManagerLinker.Link(manager, m)
                    Next m
                    managers.Add(manager)
                End SyncLock
            End If
        End Sub
        Protected Overrides Sub OnDetaching()
            Dim manager As DockLayoutManager = TryCast(AssociatedObject, DockLayoutManager)
            If manager IsNot Nothing AndAlso managers.Contains(manager) Then
                SyncLock managers
                    For Each m In managers
                        DockLayoutManagerLinker.Unlink(manager, m)
                    Next m
                    managers.Remove(manager)
                End SyncLock
            End If
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
