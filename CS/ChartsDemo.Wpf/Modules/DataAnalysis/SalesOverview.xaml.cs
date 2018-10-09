using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Utils.Filtering;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace CommonChartsDemo {
    [CodeFile("Modules/DataAnalysis/SalesOverview.xaml"),
     CodeFile("Modules/DataAnalysis/SalesOverview.xaml.(cs)"),
     CodeFile("ViewModels/SalesProductData.(cs)")]
    public partial class SalesOverview : CommonChartsDemoModule {

        Dictionary<string, SolidColorBrush> seriesBrushes;
        Dictionary<string, SolidColorBrush> SeriesBrushes {
            get {
                if(seriesBrushes == null)
                    seriesBrushes = CreateBrushes();
                return seriesBrushes;
            }
        }

        public SalesOverview() {
            InitializeComponent();
            DataContext = new BicyclesDataViewModel(SeriesBrushes);
        }

        Dictionary<string, SolidColorBrush> CreateBrushes() {
            Dictionary<string, SolidColorBrush> result = new Dictionary<string, SolidColorBrush>();
            for(int i = 0; i < SalesProductData.BikeCategories.Count; i++)
                result.Add(SalesProductData.BikeCategories[i], new SolidColorBrush(chart.Palette[i]));
            return result;
        }
        void DataLayoutControl_AutoGeneratingItem(object sender, DataLayoutControlAutoGeneratingItemEventArgs e) {
            LayoutGroup group = e.Item.Content as LayoutGroup;
            if(group != null) {
                BarManager.SetDXContextMenu(group, null);
                if(e.PropertyName == "ReportDate")
                    e.Item.Visibility = Visibility.Hidden;
                if(e.PropertyName == "Category") {
                    ListBoxEdit edit = group.Children[1] as ListBoxEdit;
                    edit.ItemTemplate = Resources["CategoryItemTemplate"] as DataTemplate;
                }
            }
        }
        void chart_BoundDataChanged(object sender, RoutedEventArgs e) {
            foreach(Series series in chart.Diagram.Series)
                ((XYSeries)series).Brush = SeriesBrushes[series.DisplayName];
        }
    }

    public class BicyclesSaleFilteringViewModel {
        [FilterLookup("Categories",  UseBlanks = false, UseSelectAll = false, ValueMember = "Text")]
        [Display(Name = "Bicycles Category")]
        public string Category { get; set; }
        [FilterRange("MinRevenue", "MaxRevenue")]
        [Display(Name = "Weekly Revenue, USD")]
        [DataType(DataType.Currency)]
        public decimal Revenue { get; set; }
        [Display(Name = "Units Sold")]
        [FilterRange("MinSold", "MaxSold", EditorType = RangeUIEditorType.Range)]
        public int UnitsSold { get; set; }
        [FilterRange("StartSelectionDate", "EndSelectionDate")]
        [Display(Name = "Report Date")]
        public DateTime ReportDate{ get; set; }
    }

    [POCOViewModel]
    public class BicyclesDataViewModel {
        public class CategoryInfo {
            public string Text { get; set; }
            public SolidColorBrush Brush { get; set; }
        }
        public virtual IEnumerable<CategoryInfo> Categories { get; protected set; }
        public virtual List<BikeReportItem> Data { get; protected set; }
        public virtual List<BikeReportRangeItem> RangeData { get; protected set; }
        public virtual decimal MinRevenue { get; protected set; }
        public virtual decimal MaxRevenue { get; protected set; }
        public virtual int MinSold { get; protected set; }
        public virtual int MaxSold { get; protected set; } 
        public virtual DateTime EndSelectionDate { get; set; }
        public virtual DateTime StartSelectionDate { get; set; }

        public BicyclesDataViewModel(Dictionary<string, SolidColorBrush> seriesBrushes) {
            List<BikeReportItem> data = SalesProductData.BicyclesReport.BicyclesData;
            this.Data = data;
            this.Categories = seriesBrushes.Select(item => new CategoryInfo() { Text = item.Key, Brush = item.Value });
            this.MinSold = data.Min(x => x.UnitsSold);
            this.MaxSold = data.Max(x => x.UnitsSold);
            this.MinRevenue = data.Min(x => x.Revenue);
            this.MaxRevenue = data.Max(x => x.Revenue);
            this.RangeData = SalesProductData.BicyclesReport.BicycleRangesData;
            this.StartSelectionDate = data.Min(x => x.ReportDate);
            this.EndSelectionDate = data.Max(x => x.ReportDate);
        }
    }
}
