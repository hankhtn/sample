using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Linq;
using System.ComponentModel;
using System.Collections;
using DevExpress.Mvvm.POCO;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/AnimationControl.xaml")]
    [CodeFile("Modules/Interactivity/AnimationControl.xaml.(cs)")]
    [CodeFile("ViewModels/DataPointSources.(cs)")]
    [CodeFile("Utils/AnimationHelper.(cs)")]
    [CodeFile("Utils/SeriesInfo.(cs)")]
    public partial class AnimationControl : ChartsDemoModule {
        public AnimationControl() {
            InitializeComponent();
            ActualChart = chart;
            Dispatcher.BeginInvoke(new Action(OnSeriesTemplateChanged), DispatcherPriority.Background);
        }

        void SeriesTemplateChanged(object sender, RoutedEventArgs e) {
            Dispatcher.BeginInvoke(new Action(OnSeriesTemplateChanged), DispatcherPriority.Background);
        }
        void OnSeriesTemplateChanged() {
            var firstSeries = chart.Diagram.Series.FirstOrDefault();
            if(firstSeries == null) return;
            if(firstSeries is RangeBarOverlappedSeries2D) {
                ((RangeBarOverlappedSeries2D)chart.Diagram.Series.Last()).BarWidth = 0.2;
            }
            if(firstSeries is ISupportStackedGroup) {
                for(int i = 0; i < chart.Diagram.Series.Count; i++)
                    ((ISupportStackedGroup)chart.Diagram.Series[i]).StackedGroup = i % 2;
            }
            AnimationHelper.InitializeAnimationListBoxEdit(lbPointAnimation, firstSeries.GetPredefinedPointAnimationKinds(), AnimationHelper.GetDefaultPointAnimationType(firstSeries));
            AnimationHelper.InitializeAnimationListBoxEdit(lbSeriesAnimation, firstSeries.GetPredefinedSeriesAnimationKinds(), AnimationHelper.GetDefaultSeriesAnimationType(firstSeries));
            chart.Animate();
        }
        void AnimationKindChanged(object sender, RoutedEventArgs e) {
            if(chart.Diagram == null) return;
            for(int i = 0; i < chart.Diagram.Series.Count; i++) {
                var series = chart.Diagram.Series[i];
                var seriesAnimation = AnimationHelper.CreateSeriesAnimation((AnimationKind)lbSeriesAnimation.SelectedItem, i);
                var pointAnimation = AnimationHelper.CreatePointAnimation((AnimationKind)lbPointAnimation.SelectedItem, seriesAnimation, i);
                series.SetSeriesAnimation(seriesAnimation);
                series.SetPointAnimation(pointAnimation);
            }
            chart.Animate();
        }
    }
}
