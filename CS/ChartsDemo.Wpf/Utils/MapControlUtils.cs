using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Map;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ChartsDemo {
    public class SelectedItemNameBindingBehavior : Behavior<VectorLayer> {
        public static readonly DependencyProperty SelectedItemNameProperty = DependencyProperty.Register("SelectedItemName", typeof(string), typeof(SelectedItemNameBindingBehavior), new PropertyMetadata(null, (d, e) => ((SelectedItemNameBindingBehavior)d).OnSelectedItemNameChanged()));

        public string SelectedItemName {
            get { return (string)GetValue(SelectedItemNameProperty); }
            set { SetValue(SelectedItemNameProperty, value); }
        }

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.Map.SelectionChanged += MapSelectionChanged;
            AssociatedObject.DataLoaded += (_d, _e) => OnSelectedItemNameChanged();
        }

        static string GetName(MapItem mapItem) {
            return (mapItem != null && mapItem.Attributes["NAME"] != null) ? (string)mapItem.Attributes["NAME"].Value : string.Empty;
        }
        void MapSelectionChanged(object sender, MapItemSelectionChangedEventArgs e) {
            SelectedItemName = GetName((MapItem)e.Selection.FirstOrDefault());
        }
        void OnSelectedItemNameChanged() {
            if(AssociatedObject != null && AssociatedObject.Data != null && AssociatedObject.Data.DisplayItems.Any()) 
                AssociatedObject.SelectedItem = AssociatedObject.Data.DisplayItems.FirstOrDefault(x => GetName(x) == SelectedItemName);
        }
    }

    public class ChartPaletteToMapColorsConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Palette chartColors = (Palette)value;
            DoubleCollection rangeStops = (DoubleCollection)parameter;
            int colorsCount = (int)(rangeStops[rangeStops.Count - 1] - rangeStops[0]) + 1;
            DevExpress.Xpf.Map.ColorCollection mapColors = new DevExpress.Xpf.Map.ColorCollection();
            for(int i = 0; i < colorsCount; i++)
                mapColors.Add(chartColors[i]);
            return mapColors;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
