using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/RealTimeFinancialControl.xaml"),
     CodeFile("Modules/DataAnalysis/RealTimeFinancialControl.xaml.(cs)")]
    [CodeFile("ViewModels/FinancialDataViewModel.(cs)")]
    public partial class RealTimeFinancialControl : ChartsDemoModule {
        public RealTimeFinancialControl() {
            InitializeComponent();
            ActualChart = chart;
        }

        void UpdateIndicators() {
            priceSeries.Indicators.Clear();
            bool existSeparatePaneIndicators = false;
            foreach(ComboBoxEditItem item in indicatorsBox.SelectedItems) {
                Indicator indicator = (Indicator)Activator.CreateInstance((Type)item.Tag);
                SeparatePaneIndicator separatePaneIndicator = indicator as SeparatePaneIndicator;
                if(separatePaneIndicator != null) {
                    separatePaneIndicator.Pane = indicatorsPane;
                    separatePaneIndicator.AxisY = indicatorsAxis;
                    existSeparatePaneIndicators = true;
                    indicator.Legend = indicatorsLegend;
                }
                else
                    indicator.Legend = priceLegend;
                indicator.LegendText = (string)item.Content;
                indicator.ShowInLegend = true;
                priceSeries.Indicators.Add(indicator);
            }
            indicatorsAxis.Visible = existSeparatePaneIndicators;
            indicatorsPane.Visibility = existSeparatePaneIndicators ? Visibility.Visible : Visibility.Hidden;
            indicatorsAxis.VisibilityInPanes[0].Visible = existSeparatePaneIndicators;
            dateAxis.VisibilityInPanes[1].Visible = !existSeparatePaneIndicators;
            volumePane.AxisXScrollBarOptions.Visible = !existSeparatePaneIndicators;
        }
        void ChartLoaded(object sender, RoutedEventArgs e) {
            var maxRange = priceSeries.Points.Last().DateTimeArgument;
            var minRange = priceSeries.Points[priceSeries.Points.Count / 2].DateTimeArgument;
            dateAxis.VisualRange.SetMinMaxValues(minRange, maxRange);
            UpdateIndicators();
        }
        void QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Position);
            if (hitInfo != null && hitInfo.Pane != null)
                e.Cursor = Cursors.Hand;
        }
        void IndicatorsComboBoxCustomDisplayText(object sender, CustomDisplayTextEventArgs e) {
            if(string.IsNullOrEmpty(e.DisplayText)) {
                e.DisplayText = "None";
                e.Handled = true;
            }
        }
        void IndicatorsComboBoxEditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateIndicators();
        }
    }
}
