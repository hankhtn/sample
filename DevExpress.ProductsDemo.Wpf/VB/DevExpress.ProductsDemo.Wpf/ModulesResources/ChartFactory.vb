Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Editors
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace ProductsDemo
    Friend NotInheritable Class ChartFactory

        Private Sub New()
        End Sub


        Public Shared Function GenerateDiagram(ByVal seriesType As Type, ByVal diagramType As Type, ByVal showPointsLabels? As Boolean) As Diagram
            Dim seriesTemplate As Series = CreateSeriesInstance(seriesType)
            Dim diagram As Diagram = CreateDiagramBySeriesType(diagramType)
            If TypeOf diagram Is XYDiagram2D Then
                PrepareXYDiagram2D(TryCast(diagram, XYDiagram2D))
            End If
            If TypeOf diagram Is XYDiagram3D Then
                PrepareXYDiagram3D(TryCast(diagram, XYDiagram3D))
            End If
            If TypeOf diagram Is Diagram3D Then
                CType(diagram, Diagram3D).RuntimeRotation = True
            End If
            diagram.SeriesDataMember = "Series"
            seriesTemplate.ArgumentDataMember = "Arguments"
            seriesTemplate.ValueDataMember = "Values"
            If seriesTemplate.Label Is Nothing Then
                seriesTemplate.Label = New SeriesLabel()
            End If
            seriesTemplate.LabelsVisibility = showPointsLabels.Value = True
            If TypeOf seriesTemplate Is PieSeries2D OrElse TypeOf seriesTemplate Is PieSeries3D Then
                seriesTemplate.LegendTextPattern = "{A}"
                seriesTemplate.Label.TextPattern = "{A}: {VP:P0}"
            Else
                seriesTemplate.LegendTextPattern = "{A}: {V}"
                seriesTemplate.ShowInLegend = True
            End If
            diagram.SeriesTemplate = seriesTemplate
            Return diagram
        End Function
        Private Shared Sub PrepareXYDiagram2D(ByVal diagram As XYDiagram2D)
            If diagram Is Nothing Then
                Return
            End If
            diagram.AxisX = New AxisX2D()
            diagram.AxisX.Label = New AxisLabel()
            diagram.AxisX.Label.Staggered = True
        End Sub
        Private Shared Sub PrepareXYDiagram3D(ByVal diagram As XYDiagram3D)
            If diagram Is Nothing Then
                Return
            End If
            diagram.AxisX = New AxisX3D()
            diagram.AxisX.Label = New AxisLabel()
            diagram.AxisX.Label.Visible = False
        End Sub
        Public Shared Function CreateSeriesInstance(ByVal seriesType As Type) As Series
            Dim series As Series = DirectCast(Activator.CreateInstance(seriesType), Series)
            Dim supportTransparency As ISupportTransparency = TryCast(series, ISupportTransparency)
            If supportTransparency IsNot Nothing Then
                Dim flag As Boolean = TypeOf series Is AreaSeries2D
                flag = flag OrElse TypeOf series Is AreaSeries3D
                If flag Then
                    supportTransparency.Transparency = 0.4
                Else
                    supportTransparency.Transparency = 0
                End If
            End If
            Return series
        End Function
        Private Shared Function CreateDiagramBySeriesType(ByVal diagramType As Type) As Diagram
            Return DirectCast(Activator.CreateInstance(diagramType), Diagram)
        End Function
    End Class
End Namespace
