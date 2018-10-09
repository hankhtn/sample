using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ChartsDemo {
    public class ChartDataSourceViewModel<T> {
        public List<ChartDataSource<T>> Sources { get; set; }
        public virtual ChartDataSource<T> SelectedSource { get; set; }
        public virtual IChartAnimationService ChartAnimationService { get { return null; } }

        public void Animate() {
            if(ChartAnimationService != null)
                ChartAnimationService.Animate();
        }
        protected void OnSelectedSourceChanged() {
            Animate();
        }
    }
    public class ChartDataSource<T> {
        public string Description { get; set; }
        public List<T> DataSource { get; set; }
    }
    public static class ChartViewModelFactory {
        #region Scatter Line ViewModels

        public static ChartDataSourceViewModel<Point> CreatePolarScatterLineViewModel() {
            return CreateScatterViewModel(new DegreeScatterFunctionCalculator());
        }
        public static ChartDataSourceViewModel<Point> CreateRadarScatterLineViewModel() {
            return CreateScatterViewModel(new RadianScatterFunctionCalculator());
        }

        static ChartDataSourceViewModel<Point> CreateScatterViewModel(ScatterFunctionCalculatorBase calculator) {
            var viewModel = ViewModelSource.Create<ChartDataSourceViewModel<Point>>();
            viewModel.Sources = new List<ChartDataSource<Point>> {
                new ChartDataSource<Point> { Description = "Archimedean Spiral", DataSource = calculator.CreateArchimedeanSpiralData() },
                new ChartDataSource<Point> { Description = "Polar Rose", DataSource = calculator.CreateRoseData() },
                new ChartDataSource<Point> { Description = "Polar Folium", DataSource = calculator.CreateFoliumData() }
            };
            viewModel.SelectedSource = viewModel.Sources.First();
            return viewModel;
        }

        #endregion
        #region Polar ViewModel

        public static ChartDataSourceViewModel<RangeDataPoint> CreatePolarViewModel() {
            var viewModel = ViewModelSource.Create<ChartDataSourceViewModel<RangeDataPoint>>();
            viewModel.Sources = new List<ChartDataSource<RangeDataPoint>> {
                new ChartDataSource<RangeDataPoint> { Description = "Taubin's Heart", DataSource = PolarFunctionsPointGenerator.CreateTaubinsHeartData() },
                new ChartDataSource<RangeDataPoint> { Description = "Cardioid", DataSource = PolarFunctionsPointGenerator.CreateCardioidData() },
                new ChartDataSource<RangeDataPoint> { Description = "Lemniscate", DataSource = PolarFunctionsPointGenerator.CreateLemniskateData() }
            };
            viewModel.SelectedSource = viewModel.Sources.Last();
            return viewModel;
        }

        #endregion
        #region Cartesian ViewModel

        public static ChartDataSourceViewModel<Point> CreateCartesianViewModel() {
            var viewModel = ViewModelSource.Create<ChartDataSourceViewModel<Point>>();
            viewModel.Sources = new List<ChartDataSource<Point>> {
                new ChartDataSource<Point> { Description = "Archimedean Spiral", DataSource = CartesianFunctionsPointGenerator.CreateArchimedianSpiralPoints() },
                new ChartDataSource<Point> { Description = "Cardioid", DataSource = CartesianFunctionsPointGenerator.CreateCardioidPoints() },
                new ChartDataSource<Point> { Description = "Cartesian Folium", DataSource = CartesianFunctionsPointGenerator.CreateCartesianFoliumPoints() }
            };
            viewModel.SelectedSource = viewModel.Sources.First();
            return viewModel;
        }

        #endregion
    }
}
