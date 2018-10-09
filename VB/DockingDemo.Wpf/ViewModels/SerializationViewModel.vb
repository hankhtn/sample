Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Reflection
Imports System.Xml.Serialization
Imports DevExpress.DemoData.Utils
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace DockingDemo.ViewModels
    Public Class SerializableViewModel
        Protected Sub New()
        End Sub

        Public Property Content() As Object
        Public Property DisplayName() As String
        Public Property Name() As String

        Public Shared Function Create() As SerializableViewModel
            Return ViewModelSource.Create(Function() New SerializableViewModel())
        End Function
        Public Shared Function Create(ByVal id As Integer) As SerializableViewModel
            Dim vm = ViewModelSource.Create(Function() New SerializableViewModel())
            vm.Name = "Model" & id
            vm.DisplayName = "Model " & id
            vm.Content = id
            Return vm
        End Function
        Public Shared Function Create(ByVal info As SerializationInfo) As SerializableViewModel
            Dim vm = Create()
            vm.ApplySerializationInfo(info)
            Return vm
        End Function
        Public Function GetSerializationInfo() As SerializationInfo
            Return New SerializationInfo() With {.Content = Content, .DisplayName = DisplayName, .Name = Name}
        End Function
        Private Sub ApplySerializationInfo(ByVal info As SerializationInfo)
            Content = info.Content
            DisplayName = info.DisplayName
            Name = info.Name
        End Sub

        Public Class SerializationInfo
            Public Sub New()
            End Sub

            Public Property Content() As Object
            Public Property DisplayName() As String
            Public Property Name() As String
        End Class
    End Class
    Public Class MVVMSerializationViewModel
        Inherits ViewModelBase

        Private _Items As IList(Of Object)
        Private layoutStream As Stream
        Private vmStream As Stream

        Public Sub New()
            _Items = GenerateItems(5)
        End Sub

        Public ReadOnly Property Items() As IList(Of Object)
            Get
                Return _Items
            End Get
        End Property
        Public Overridable ReadOnly Property SerializationService() As IDockLayoutManagerSerializationService
            Get
                Return Nothing
            End Get
        End Property

        Public Function CanRestore() As Boolean
            Return vmStream IsNot Nothing AndAlso layoutStream IsNot Nothing
        End Function
        Public Sub Restore()
            vmStream.Position = 0
            layoutStream.Position = 0
            RestoreFromStream(vmStream)
            SerializationService.Deserialize(layoutStream)
        End Sub
        Public Sub RestoreSample(ByVal index As Integer)
            Dim assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim vmName As String = String.Format("views{0}.xml", index + 1)
            Using resourceStream As Stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Layouts/MVVMSerialization/", assembly) & vmName, True)
                RestoreFromStream(resourceStream)
            End Using
            Dim name As String = String.Format("savedworkspace{0}.xml", index + 1)
            Using resourceStream As Stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Layouts/MVVMSerialization/", assembly) & name, True)
                SerializationService.Deserialize(resourceStream)
            End Using
        End Sub
        Public Sub Store()
            If vmStream Is Nothing Then
                vmStream = New MemoryStream()
            End If
            If layoutStream Is Nothing Then
                layoutStream = New MemoryStream()
            End If
            vmStream.SetLength(0)
            layoutStream.SetLength(0)
            StoreToStream(vmStream)
            SerializationService.Serialize(layoutStream)
        End Sub
        Private Function GenerateItems(ByVal count As Integer) As IList(Of Object)

            Dim items_Renamed As New ObservableCollection(Of Object)()
            For i As Integer = 0 To count - 1
                items_Renamed.Add(SerializableViewModel.Create(i))
            Next i
            Return items_Renamed
        End Function
        Private Sub RestoreFromStream(ByVal stream As Stream)
            Dim serializer As ViewModelSerializer = ViewModelSerializer.Deserialize(stream)
            Items.Clear()
            For Each info In serializer.Infos
                Items.Add(SerializableViewModel.Create(info))
            Next info
        End Sub
        Private Sub StoreToStream(ByVal stream As Stream)
            Dim serializer As New ViewModelSerializer()
            For Each item As SerializableViewModel In Items
                serializer.Infos.Add(item.GetSerializationInfo())
            Next item
            ViewModelSerializer.Serialize(stream, serializer)
        End Sub

        <XmlRoot("ViewModels")>
        Public Class ViewModelSerializer
            Public Sub New()
                Infos = New List(Of SerializableViewModel.SerializationInfo)()
            End Sub

            Public Property Infos() As List(Of SerializableViewModel.SerializationInfo)
            Public Property Name() As String

            Public Shared Function Deserialize(ByVal stream As Stream) As ViewModelSerializer
                Dim res As ViewModelSerializer
                Dim s As New XmlSerializer(GetType(ViewModelSerializer), New Type() { GetType(SerializableViewModel.SerializationInfo) })
                res = DirectCast(s.Deserialize(stream), ViewModelSerializer)
                Return res
            End Function
            Public Shared Sub Serialize(ByVal stream As Stream, ByVal obj As ViewModelSerializer)
                Dim s As New XmlSerializer(GetType(ViewModelSerializer), New Type() { GetType(SerializableViewModel.SerializationInfo) })
                s.Serialize(stream, obj)
            End Sub
            Public Shared Sub Serialize(ByVal path As String, ByVal obj As ViewModelSerializer)
                Dim s As New XmlSerializer(GetType(ViewModelSerializer), New Type() { GetType(SerializableViewModel.SerializationInfo) })
                Using st As Stream = New FileStream(path, FileMode.Create)
                    s.Serialize(st, obj)
                End Using
            End Sub
        End Class
    End Class
End Namespace
