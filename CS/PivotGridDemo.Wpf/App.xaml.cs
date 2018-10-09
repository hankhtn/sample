using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace PivotGridDemo {
    public partial class App : Application {
        static App() {
            ApplicationThemeHelper.ApplicationThemeName = (DemoBaseControl.DefaultTheme ?? Theme.Office2016ColorfulSE).Name;
        }
#if DEBUG
        public bool IsDebug { get { return true; } }
#endif
    }
}
