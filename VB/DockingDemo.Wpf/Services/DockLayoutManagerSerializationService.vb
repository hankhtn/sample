Imports System
Imports System.Linq
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core.Serialization
Imports DevExpress.Xpf.Docking

Namespace DockingDemo
    Public Interface IDockLayoutManagerSerializationService
        Sub Serialize(ByVal path As Object)
        Sub Deserialize(ByVal path As Object)
    End Interface
    Public Class DockLayoutManagerSerializationService
        Inherits ServiceBase
        Implements IDockLayoutManagerSerializationService

        Private ReadOnly Property DockLayoutManager() As DockLayoutManager
            Get
                Return TryCast(AssociatedObject, DockLayoutManager)
            End Get
        End Property

        Private Sub IDockLayoutManagerSerializationService_Deserialize(ByVal path As Object) Implements IDockLayoutManagerSerializationService.Deserialize
            DXSerializer.Deserialize(AssociatedObject, path, AssociatedObject.GetType().Name, Nothing)
        End Sub

        Private Sub IDockLayoutManagerSerializationService_Serialize(ByVal path As Object) Implements IDockLayoutManagerSerializationService.Serialize
            DXSerializer.Serialize(AssociatedObject, path, AssociatedObject.GetType().Name, Nothing)
        End Sub
    End Class
End Namespace
