using System.Windows;
using DevExpress.Xpf.Core;

namespace DiagramDesigner {
    public partial class App : Application {
        public App() {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2016ColorfulSE.Name;
        }
    }
}
