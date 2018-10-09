using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Scheduling;

namespace SchedulingDemo {
   public class SportGroupToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return SportChannelsData.SportGroups[(int)value].Image;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
    }

    public class CommandBarStyleToBooleanConverter : ObjectToObjectConverter {
        public CommandBarStyleToBooleanConverter() {
            Map.Add(new MapItem() { Target = true, Source = CommandBarStyle.Ribbon });
            Map.Add(new MapItem() { Target = false, Source = CommandBarStyle.Empty });
        }
    }

    public class SportGroupToImageSourceConverterExtension : MarkupExtension {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new SportGroupToImageSourceConverter();
        }
    }

    public class CommandBarStyleToBooleanConverterExtension : MarkupExtension {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new CommandBarStyleToBooleanConverter();
        }
    }
}
