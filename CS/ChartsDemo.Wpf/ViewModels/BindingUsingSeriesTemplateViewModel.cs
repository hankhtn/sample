using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ChartsDemo {
    public class BindingUsingSeriesTemplateViewModel {
        IEnumerable<GDP> dataSource;
        IEnumerable<ChartDataBindingControlSeriesViewModel> series;

        public BindingUsingSeriesTemplateViewModel() {
            ShowLabels = true;
            SelectedSeries = this.Series.First();
        }
        public IEnumerable<GDP> DataSource {
            get {
                if(dataSource == null)
                    dataSource = G7Data.Data.Where(x => x.Year > 2010).OrderBy(gdpItem => gdpItem.Year).ToList();
                return dataSource;
            }
        }
        public IEnumerable<ChartDataBindingControlSeriesViewModel> Series {
            get {
                if(series == null) {
                    var series2 = new ChartDataBindingControlSeriesViewModel("Year", "Country");
                    var series1 = new ChartDataBindingControlSeriesViewModel("Country", "Year");
                    series = new ChartDataBindingControlSeriesViewModel[] { series1, series2 };
                }
                return series;
            }
        }
        public virtual bool ShowLabels { get; set; }
        public virtual ChartDataBindingControlSeriesViewModel SelectedSeries { get; set; }
        public virtual IChartAnimationService ChartAnimationService { get { return null; } }
        public void OnModuleLoaded() {
            if(ChartAnimationService != null)
                ChartAnimationService.Animate();
        }
        protected void OnSelectedSeriesChanged(ChartDataBindingControlSeriesViewModel oldValue) {
            if(ChartAnimationService != null)
                ChartAnimationService.Animate();
        }
    }

    public class ChartDataBindingControlSeriesViewModel {
        public ChartDataBindingControlSeriesViewModel(string dataMember, string argumentDataMember) {
            DataMember = dataMember;
            ArgumentDataMember = argumentDataMember;
        }
        public string DataMember { get; private set; }
        public string ArgumentDataMember { get; private set; }
    }
}
