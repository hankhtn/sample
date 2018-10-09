Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports System.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DiagramDemo
    <POCOViewModel>
    Public Class DiagramEventNode
        Public Shared Function Create(ByVal title As String, ByVal isChecked? As Boolean, ByVal kind As DiagramEventNodeKind) As DiagramEventNode
            Return ViewModelSource.Create(Function() New DiagramEventNode(title, isChecked, kind))
        End Function
        Protected Sub New(ByVal title As String, ByVal isChecked? As Boolean, ByVal kind As DiagramEventNodeKind)
            Me.title_Renamed = title
            Me.kind_Renamed = kind
            Me.IsChecked = isChecked
        End Sub

        Private ReadOnly title_Renamed As String
        Public ReadOnly Property Title() As String
            Get
                Return title_Renamed
            End Get
        End Property

        Private ReadOnly kind_Renamed As DiagramEventNodeKind
        Public ReadOnly Property Kind() As DiagramEventNodeKind
            Get
                Return kind_Renamed
            End Get
        End Property
        Public Overridable Property IsChecked() As Boolean?
        Public ReadOnly Property ActualIsChecked() As Boolean
            Get
                Return IsChecked.HasValue AndAlso IsChecked.Value
            End Get
        End Property
        Public Overridable Property Parent() As DiagramEventNode

        Private ReadOnly children_Renamed As New ObservableCollection(Of DiagramEventNode)()
        Public ReadOnly Property Children() As IEnumerable(Of DiagramEventNode)
            Get
                Return children_Renamed
            End Get
        End Property
        Public Sub AddChild(ByVal child As DiagramEventNode)
            child.Parent = Me
            children_Renamed.Add(child)
        End Sub
        Public Sub ShowHelp()
            Process.Start("https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl." & Title & ".event")
        End Sub
    End Class
    Public Enum DiagramEventNodeKind
        Group
        EventNode
        Parameter
    End Enum
End Namespace
