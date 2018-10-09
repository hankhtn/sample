using System;

namespace DockingDemo {
    public partial class FloatingPanels : DockingDemoModule {
        public FloatingPanels() {
            InitializeComponent();
            DesktopContainer.Loaded += OnDesktopContainerLoaded;
        }
        void OnDesktopContainerLoaded(object sender, System.Windows.RoutedEventArgs e) {
            DesktopContainer.Loaded -= OnDesktopContainerLoaded;
            ShowWindowModeContainer();
        }
        bool isWindow;
        void OnFloatingModeButtonlick(object sender, System.Windows.RoutedEventArgs e) {
            if(isWindow) ShowDesktopModeContainer();
            else ShowWindowModeContainer();
        }
        void ShowWindowModeContainer() {
            ModeSwitchButton.Content = "Show in Desktop Mode";
            WindowContainer.Visibility = System.Windows.Visibility.Visible;
            DesktopContainer.Visibility = System.Windows.Visibility.Collapsed;
            isWindow = true;
        }
        void ShowDesktopModeContainer() {
            ModeSwitchButton.Content = "Show in Window Mode";
            DesktopContainer.Visibility = System.Windows.Visibility.Visible;
            WindowContainer.Visibility = System.Windows.Visibility.Collapsed;
            isWindow = false;
        }
    }
}
