using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler.iCalendar;

namespace ProductsDemo.Modules {
    
    
    
    public partial class SchedulerModule : UserControl {
        public SchedulerModule() {
            InitializeComponent();
        }
    }
    public class SchedulerExchangeCreatorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            SchedulerControl scheduler = value as SchedulerControl;
            if (scheduler == null)
                return null;
            return new SchedulerExchangeCreator(scheduler);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class SchedulerExchangeCreator : ISchedulerExchangeCreator {
        SchedulerControl control;
        public SchedulerExchangeCreator(SchedulerControl control) {
            this.control = control;
        }
        public iCalendarImporter CreateImporter() {
            return new iCalendarImporter(this.control.Storage.InnerStorage);
        }

        public iCalendarExporter CreateExporter() {
            return new iCalendarExporter(this.control.Storage.InnerStorage);
        }
    }
}
