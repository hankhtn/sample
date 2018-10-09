using System.Windows;
using DevExpress.DevAV.Common.Utils;
using DevExpress.Utils.TouchHelpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.WindowsUI;

namespace DevExpress.DevAV {
    public partial class MainWindow : ThemedWindow {
        public MainWindow() {
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, this); 
            InitializeComponent();
            if(Height > SystemParameters.VirtualScreenHeight || Width > SystemParameters.VirtualScreenWidth)
                WindowState = WindowState.Maximized;
            if(DeviceDetector.IsTablet) {
                WindowStyle = WindowStyle.None;
                ResizeMode = ResizeMode.CanMinimize;
                WindowState = WindowState.Maximized;
            }
            EventManager.RegisterClassHandler(typeof(AppBarButton), AppBarButton.ClickEvent, new RoutedEventHandler(OnAppBarButtonClick), true);                                
        }
        void MainWindowLoaded(object sender, RoutedEventArgs e) {
            if(!DeviceDetector.IsTablet && (Left < 0 || Top < 0))
                WindowState = WindowState.Maximized;
        }
        void OnAppBarButtonClick(object sender, RoutedEventArgs e) {   
            var barButton = (AppBarButton)sender;
            var content = barButton.Label ?? barButton.Name;
            if(content != null) {
                Xpf.DemoBase.Helpers.Logger.Log(string.Format("HybridApp: BarButtonClick: {0}", content.ToString()));
            }
        }
    }
}
