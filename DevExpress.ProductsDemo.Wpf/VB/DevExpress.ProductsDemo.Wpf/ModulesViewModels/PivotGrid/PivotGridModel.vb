Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace ProductsDemo.Modules
    Public Class SeriesTypeDescriptor
        Public Sub New(ByVal seriesType As Type, ByVal diagramType As Type, ByVal displayText As String)
            Me.SeriesType = seriesType
            Me.DiagramType = diagramType
            Me.DisplayText = displayText
        End Sub
        Public Property SeriesType() As Type
        Public Property DiagramType() As Type
        Public Property DisplayText() As String
    End Class
    Public Enum ChartOrientation
        GenerateSeriesFromColumns
        GenerateSeriesFromRows
    End Enum

    Public Class OnlyItemWrapper
        Inherits ItemWrapper

        Public Sub New(ByVal chartOrientation As ChartOrientation)
            Me.ChartOrientation = chartOrientation
        End Sub
        Public Overrides ReadOnly Property Text() As String
            Get
                If ChartOrientation = ChartOrientation.GenerateSeriesFromColumns Then
                    Return "Series from columns"
                End If
                Return "Series from rows"
            End Get
        End Property
        Private privateChartOrientation As ChartOrientation
        Public Property ChartOrientation() As ChartOrientation
            Get
                Return privateChartOrientation
            End Get
            Private Set(ByVal value As ChartOrientation)
                privateChartOrientation = value
            End Set
        End Property
    End Class

    Public Class ItemWrapper

        Private text_Renamed As String
        Protected Sub New()
        End Sub
        Public Sub New(ByVal text As String)
            Me.text_Renamed = text
        End Sub
        Public Overrides Function ToString() As String
            Return Text
        End Function
        Public Overridable ReadOnly Property Text() As String
            Get
                Return text_Renamed
            End Get
        End Property
        Public Shared Function Create(ByVal str As String) As ItemWrapper
            Return New ItemWrapper(str)
        End Function
    End Class
End Namespace
