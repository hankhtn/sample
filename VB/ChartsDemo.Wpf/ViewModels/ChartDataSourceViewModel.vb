Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows

Namespace ChartsDemo
    Public Class ChartDataSourceViewModel(Of T)
        Public Property Sources() As List(Of ChartDataSource(Of T))
        Public Overridable Property SelectedSource() As ChartDataSource(Of T)
        Public Overridable ReadOnly Property ChartAnimationService() As IChartAnimationService
            Get
                Return Nothing
            End Get
        End Property

        Public Sub Animate()
            If ChartAnimationService IsNot Nothing Then
                ChartAnimationService.Animate()
            End If
        End Sub
        Protected Sub OnSelectedSourceChanged()
            Animate()
        End Sub
    End Class
    Public Class ChartDataSource(Of T)
        Public Property Description() As String
        Public Property DataSource() As List(Of T)
    End Class
    Public NotInheritable Class ChartViewModelFactory

        Private Sub New()
        End Sub
        #Region "Scatter Line ViewModels"

        Public Shared Function CreatePolarScatterLineViewModel() As ChartDataSourceViewModel(Of Point)
            Return CreateScatterViewModel(New DegreeScatterFunctionCalculator())
        End Function
        Public Shared Function CreateRadarScatterLineViewModel() As ChartDataSourceViewModel(Of Point)
            Return CreateScatterViewModel(New RadianScatterFunctionCalculator())
        End Function

        Private Shared Function CreateScatterViewModel(ByVal calculator As ScatterFunctionCalculatorBase) As ChartDataSourceViewModel(Of Point)
            Dim viewModel = ViewModelSource.Create(Of ChartDataSourceViewModel(Of Point))()
            viewModel.Sources = New List(Of ChartDataSource(Of Point)) From {
                New ChartDataSource(Of Point) With {.Description = "Archimedean Spiral", .DataSource = calculator.CreateArchimedeanSpiralData()},
                New ChartDataSource(Of Point) With {.Description = "Polar Rose", .DataSource = calculator.CreateRoseData()},
                New ChartDataSource(Of Point) With {.Description = "Polar Folium", .DataSource = calculator.CreateFoliumData()}
            }
            viewModel.SelectedSource = viewModel.Sources.First()
            Return viewModel
        End Function

        #End Region
        #Region "Polar ViewModel"

        Public Shared Function CreatePolarViewModel() As ChartDataSourceViewModel(Of RangeDataPoint)
            Dim viewModel = ViewModelSource.Create(Of ChartDataSourceViewModel(Of RangeDataPoint))()
            viewModel.Sources = New List(Of ChartDataSource(Of RangeDataPoint)) From {
                New ChartDataSource(Of RangeDataPoint) With {.Description = "Taubin's Heart", .DataSource = PolarFunctionsPointGenerator.CreateTaubinsHeartData()},
                New ChartDataSource(Of RangeDataPoint) With {.Description = "Cardioid", .DataSource = PolarFunctionsPointGenerator.CreateCardioidData()},
                New ChartDataSource(Of RangeDataPoint) With {.Description = "Lemniscate", .DataSource = PolarFunctionsPointGenerator.CreateLemniskateData()}
            }
            viewModel.SelectedSource = viewModel.Sources.Last()
            Return viewModel
        End Function

        #End Region
        #Region "Cartesian ViewModel"

        Public Shared Function CreateCartesianViewModel() As ChartDataSourceViewModel(Of Point)
            Dim viewModel = ViewModelSource.Create(Of ChartDataSourceViewModel(Of Point))()
            viewModel.Sources = New List(Of ChartDataSource(Of Point)) From {
                New ChartDataSource(Of Point) With {.Description = "Archimedean Spiral", .DataSource = CartesianFunctionsPointGenerator.CreateArchimedianSpiralPoints()},
                New ChartDataSource(Of Point) With {.Description = "Cardioid", .DataSource = CartesianFunctionsPointGenerator.CreateCardioidPoints()},
                New ChartDataSource(Of Point) With {.Description = "Cartesian Folium", .DataSource = CartesianFunctionsPointGenerator.CreateCartesianFoliumPoints()}
            }
            viewModel.SelectedSource = viewModel.Sources.First()
            Return viewModel
        End Function

        #End Region
    End Class
End Namespace
