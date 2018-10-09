using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils;

namespace BarsDemo {
    public class BarsDemoModule : DemoModule {
        public static readonly DependencyProperty BarManagerProperty = 
            DependencyPropertyManager.Register("BarManager", typeof(BarManager), typeof(BarsDemoModule), new FrameworkPropertyMetadata(null));
        public BarManager Manager {
            get { return (BarManager)GetValue(BarManagerProperty); }
            set { SetValue(BarManagerProperty, value); }
        }
        static BarsDemoModule() {
            BarNameScope.IsScopeOwnerProperty.OverrideMetadata(typeof(BarsDemoModule), new FrameworkPropertyMetadata(true));
        }
        public BarsDemoModule() {
            Loaded += BarsDemoModule_Loaded;
        }
        void BarsDemoModule_Loaded(object sender, RoutedEventArgs e) {
            UpdateBorder();
        }
        void UpdateBorder() {
            Margin = new Thickness(25);
            BorderThickness = new Thickness(1);
            if(Theme.MetropolisLightName == ThemeManager.GetThemeName(this))
                BorderBrush = new SolidColorBrush(Colors.DarkGray);
            else {
                Color color = (TextElement.GetForeground(this) as SolidColorBrush).Color;
                BorderBrush = new SolidColorBrush(Color.FromArgb(50, color.R, color.G, color.B));
            }
            
        }
    }
}
