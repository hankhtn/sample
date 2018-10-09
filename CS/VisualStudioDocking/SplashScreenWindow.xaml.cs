using System.Windows.Controls;

namespace VisualStudioDocking {
    public partial class SplashScreenWindow : UserControl {
        public SplashScreenWindow() {
            InitializeComponent();
            copyrightText.Text = AssemblyInfo.AssemblyCopyright + "\r\nAll rights reserverd";
        }
    }
}
