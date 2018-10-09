using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using DevExpress.Internal;
using DevExpress.StockMarketTrader.ViewModel;
using DevExpress.Utils;
using DevExpress.Xpf.Core;

namespace DevExpress.StockMarketTrader {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
            DevExpress.Xpf.Bars.BarManager.IgnoreMenuDropAlignment = true;
            DevExpress.Xpf.Bars.ResourceStorage.UseResourceStorage = false;
            Window window = Start(() => base.OnStartup(e));
            window.Show();
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, this); 
        }
        public static Window Start(Action baseStartup) {
            DXSplashScreen.Show<StockMarketTraderSplashScreen>();
            ApplicationThemeHelper.ApplicationThemeName = Theme.MetropolisDarkName;
            RealTimeDataViewModel rtdvm = new RealTimeDataViewModel();
            baseStartup();
            StockMarketView view = new StockMarketView();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            ThemedWindow window = new ThemedWindow();
            window.MinWidth = 1200;
            window.MinHeight = 600;
            window.Width = 1200;
            window.Height = 750;
            window.ShowIcon = false;
            window.ShowTitle = false;
            window.Title = "Stock Market";
            window.Icon = new BitmapImage(AssemblyHelper.GetResourceUri(typeof(App).Assembly, "Images/DX.ico"));
            view.DataContext = rtdvm;
            window.Content = view;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Loaded += window_Loaded;
            DXSplashScreen.Close();
            return window;
        }
        static void window_Loaded(object sender, RoutedEventArgs e) {
            ((ThemedWindow)sender).Activate();
        }
    }
}
#if CLICKONCE
namespace DevExpress.Internal.DemoLauncher {
    public static class ClickOnceEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            Action run = () => {
                var app = new DevExpress.StockMarketTrader.App();
                app.Run();
            };
            return Tuple.Create(run, default(System.Threading.Tasks.Task<Window>));
        }
    }
}
#endif
