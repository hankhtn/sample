Imports System
Imports DevExpress.Xpf.DemoBase
Imports System.IO
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Diagram.Core
Imports DevExpress.Internal
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Markup
Imports DiagramDemo
Imports System.Windows.Media
Imports DevExpress.Utils
Imports System.Windows.Media.Imaging

Namespace DiagramDemo
    Public Class DiagramDemoModule
        Inherits DemoModule

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme() As Boolean
            Get
                Return False
            End Get
        End Property
        Protected Overridable Sub LoadDemoData()
        End Sub
    End Class
    Public Module DiagramControlDemoExtensions
        <System.Runtime.CompilerServices.Extension> _
        Public Sub LoadDemoData(ByVal diagramControl As DiagramControl, ByVal dataSourceName As String)
            diagramControl.LoadDocument(DiagramDemoHelper.GetDataFilePath(dataSourceName))
        End Sub
    End Module
    Public NotInheritable Class DiagramDemoHelper

        Private Sub New()
        End Sub

        Public Shared Function GetDataFilePath(ByVal relativePath As String) As String
            Return DataDirectoryHelper.GetFile("Diagram\" & relativePath, DataDirectoryHelper.DataFolderName)
        End Function
        Public Shared Function GetCircleDiagramItemPosition(ByVal radius As Double, ByVal phase As Double, ByVal diagramCenter As Point, ByVal itemSize As Size) As Point
            Dim point = GetCartesianPointByPolarPoint(radius, phase)
            Dim offsetX As Double = diagramCenter.X - itemSize.Width / 2R
            Dim offsetY As Double = diagramCenter.Y - itemSize.Height / 2R
            point.Offset(offsetX, offsetY)
            Return point
        End Function
        Public Shared Function GetCartesianPointByPolarPoint(ByVal magnitude As Double, ByVal phase As Double) As Point
            Dim x As Double = magnitude * Math.Cos(phase)
            Dim y As Double = magnitude * Math.Sin(phase)
            Return New Point(x, y)
        End Function
        Public Shared Sub LayoutCircleDiagramItems(ByVal items() As DiagramItem, ByVal pageSize As Size, ByVal radius As Double)
            Dim phase As Double = -Math.PI / 2R
            Dim phaseDelta As Double = 2 * Math.PI / items.Length
            Dim centerX As Double = pageSize.Width / 2R
            Dim centerY As Double = pageSize.Height / 2R
            Dim center As New Point(centerX, centerY)
            For Each item In items
                item.Position = DiagramDemoHelper.GetCircleDiagramItemPosition(radius, phase, center, item.DesiredSize)
                phase += phaseDelta
            Next item
        End Sub
    End Class
    Public Class DiagramContentItemTool
        Inherits MarkupExtension

        Public Property ToolId() As String
        Public Property ToolName() As String
        Public Property CustomStyleId() As String
        Public Property DefaultSize() As Size
        Public Property IsQuick() As Boolean

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return New FactoryItemTool(ToolId, Function() ToolName, Function(diagram) CreateItem(), DefaultSize, IsQuick)
        End Function
        Protected Overridable Function CreateItem() As DiagramItem
            Return New DiagramContentItem With {.CustomStyleId = CustomStyleId}
        End Function
    End Class
End Namespace

Namespace DevExpress.Diagram.Demos
    Public NotInheritable Class DiagramDemoFileHelper

        Private Sub New()
        End Sub

        Public Shared Function GetDataStream(ByVal fileName As String) As Stream
            Dim path As String = DiagramDemoHelper.GetDataFilePath(fileName)
            Return File.OpenRead(path)
        End Function
        Public Shared Function GetResourceStream(ByVal path As String) As Stream
            Dim assembly = GetType(DiagramDemoFileHelper).Assembly
            Return AssemblyHelper.GetResourceStream(assembly, path, True)
        End Function
    End Class
End Namespace
