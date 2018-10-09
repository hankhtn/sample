using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using DevExpress.Utils.Filtering;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/DataFilteringControl.xaml"),
     CodeFile("Modules/DataBinding/DataFilteringControl.xaml.(cs)")]
    public partial class DataFilteringControl : ChartsDemoModule {
        const double SalesTickFrequency = 0.42;
        const double ChargesTickFrequency = 0.08;

        public DataFilteringControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void SetUpTrackBar(TrackBarEdit trackBar, double tickFrequency) {
            trackBar.SmallStep = tickFrequency;
            trackBar.TickFrequency = tickFrequency;
            trackBar.LargeStep = tickFrequency;
        }
        void DataLayoutControl_AutoGeneratingItem(object sender, DataLayoutControlAutoGeneratingItemEventArgs e) {
            LayoutGroup group = e.Item.Content as LayoutGroup;
            if (group != null) {
                BarManager.SetDXContextMenu(group, null);
                TrackBarEdit trackBar = group.Children[group.Children.Count - 1] as TrackBarEdit;
                if (trackBar != null) {
                    if (e.PropertyName == "Sales")
                        SetUpTrackBar(trackBar, SalesTickFrequency);
                    else if (e.PropertyName == "Charges")
                        SetUpTrackBar(trackBar, ChargesTickFrequency);
                }
            }
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
            chart.AnimationMode = ChartAnimationMode.OnDataChanged;
        }
    }

    public class SalesFilteringViewModel {
        [FilterRange("StartYear", "EndYear", EditorType = RangeUIEditorType.Range)]
        public int Year { get; set; }
        [Display(Name = "Sales, millions of USD")]
        [FilterRange("MinSales", "MaxSales")]
        public decimal Sales { get; set; }
        [FilterLookup("Companies")]
        public string Company { get; set; }
        [Display(Name = "Charges, millions of USD")]
        [FilterRange("MinCharges", "MaxCharges")]
        public decimal Charges { get; set; }
    }

    public class SalesDataViewModel {
        public IEnumerable<string> Companies { get; private set; }
        public List<DevAVDataItem> Sales { get; private set; }
        public decimal MinSales { get; private set; }
        public decimal MaxSales { get; private set; }
        public decimal MinCharges { get; private set; }
        public decimal MaxCharges { get; private set; }
        public int StartYear { get; private set; }
        public int EndYear { get; private set; }

        public SalesDataViewModel() {
            this.Sales = DevAVBranchesSales.GetList();
            IEnumerable<int> years = Sales.Select(x => x.Year).Distinct();
            this.StartYear = years.Min();
            this.EndYear = years.Max();
            this.Companies = Sales.Select(x => x.Company).Distinct();
            this.MinSales = Sales.Min(x => x.Sales);
            this.MaxSales = Sales.Max(x => x.Sales);
            this.MinCharges = Sales.Min(x => x.Charges);
            this.MaxCharges = Sales.Max(x => x.Charges);
        }
    }
}
