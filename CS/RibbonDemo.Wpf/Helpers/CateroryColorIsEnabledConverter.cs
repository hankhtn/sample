using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace RibbonDemo {
    class CateroryColorEnableConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) { return this; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var walker = value as ThemeTreeWalker;
            if (walker != null && (walker.ThemeName == Theme.Office2016ColorfulSEName
                || walker.ThemeName == Theme.Office2016DarkGraySEName
                || walker.ThemeName == Theme.Office2016BlackSEName))
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }
}
