Imports DevExpress.Mvvm.Native
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq

Namespace ChartsDemo
    Public Class BindingUsingSeriesTemplateViewModel

        Private dataSource_Renamed As IEnumerable(Of GDP)

        Private series_Renamed As IEnumerable(Of ChartDataBindingControlSeriesViewModel)

        Public Sub New()
            ShowLabels = True
            SelectedSeries = Me.Series.First()
        End Sub
        Public ReadOnly Property DataSource() As IEnumerable(Of GDP)
            Get
                If dataSource_Renamed Is Nothing Then
                    dataSource_Renamed = G7Data.Data.Where(Function(x) x.Year > 2010).OrderBy(Function(gdpItem) gdpItem.Year).ToList()
                End If
                Return dataSource_Renamed
            End Get
        End Property
        Public ReadOnly Property Series() As IEnumerable(Of ChartDataBindingControlSeriesViewModel)
            Get
                If series_Renamed Is Nothing Then
                    Dim series2 = New ChartDataBindingControlSeriesViewModel("Year", "Country")
                    Dim series1 = New ChartDataBindingControlSeriesViewModel("Country", "Year")
                    series_Renamed = New ChartDataBindingControlSeriesViewModel() { series1, series2 }
                End If
                Return series_Renamed
            End Get
        End Property
        Public Overridable Property ShowLabels() As Boolean
        Public Overridable Property SelectedSeries() As ChartDataBindingControlSeriesViewModel
        Public Overridable ReadOnly Property ChartAnimationService() As IChartAnimationService
            Get
                Return Nothing
            End Get
        End Property
        Public Sub OnModuleLoaded()
            If ChartAnimationService IsNot Nothing Then
                ChartAnimationService.Animate()
            End If
        End Sub
        Protected Sub OnSelectedSeriesChanged(ByVal oldValue As ChartDataBindingControlSeriesViewModel)
            If ChartAnimationService IsNot Nothing Then
                ChartAnimationService.Animate()
            End If
        End Sub
    End Class

    Public Class ChartDataBindingControlSeriesViewModel
        Public Sub New(ByVal dataMember As String, ByVal argumentDataMember As String)
            Me.DataMember = dataMember
            Me.ArgumentDataMember = argumentDataMember
        End Sub
        Private privateDataMember As String
        Public Property DataMember() As String
            Get
                Return privateDataMember
            End Get
            Private Set(ByVal value As String)
                privateDataMember = value
            End Set
        End Property
        Private privateArgumentDataMember As String
        Public Property ArgumentDataMember() As String
            Get
                Return privateArgumentDataMember
            End Get
            Private Set(ByVal value As String)
                privateArgumentDataMember = value
            End Set
        End Property
    End Class
End Namespace
