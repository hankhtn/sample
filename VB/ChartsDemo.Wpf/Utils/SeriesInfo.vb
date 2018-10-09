Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Markup

Namespace ChartsDemo
    <ContentProperty("SeriesTemplate")>
    Public Class SeriesInfo
        Inherits BindableBase


        Private content_Renamed As String

        Private dataSource_Renamed As Object

        Private seriesTemplate_Renamed As DataTemplate

        Private diagramType_Renamed As DiagramType

        Public Property Content() As String
            Get
                Return content_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(content_Renamed, value, Function() Content)
            End Set
        End Property
        Public Property DataSource() As Object
            Get
                Return dataSource_Renamed
            End Get
            Set(ByVal value As Object)
                SetProperty(dataSource_Renamed, value, Function() DataSource)
            End Set
        End Property
        Public Property SeriesTemplate() As DataTemplate
            Get
                Return seriesTemplate_Renamed
            End Get
            Set(ByVal value As DataTemplate)
                SetProperty(seriesTemplate_Renamed, value, Function() SeriesTemplate)
            End Set
        End Property
        Public Property DiagramType() As DiagramType
            Get
                Return diagramType_Renamed
            End Get
            Set(ByVal value As DiagramType)
                SetProperty(diagramType_Renamed, value, Function() DiagramType)
            End Set
        End Property
    End Class

    Public Enum DiagramType
        XY
        Simple
        Radar
        Polar
        XY3D
    End Enum
End Namespace
