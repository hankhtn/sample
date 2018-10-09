using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Editors;

namespace DevExpress.StockMarketTrader {

    public partial class StockMarketTraderSplashScreen : Window, ISplashScreen {
        public StockMarketTraderSplashScreen() {
            InitializeComponent();
            Footer_Text.Text = String.Format("Copyright © 1998-{0}", DateTime.Now.Year);
            this.board.Completed += OnAnimationCompleted;
        }

        #region ISplashScreen
        public void Progress(double value) {
            progressBar.Value = value;
        }
        public void CloseSplashScreen() {
            this.board.Begin(this);
        }
        public void SetProgressState(bool isIndeterminate) {
            
            progressBar.StyleSettings = new ProgressBarMarqueeStyleSettings();
#if DEBUGTEST
            progressBar.SetValue(ThemeManager.ThemeNameProperty, Theme.MetropolisDarkName);
#else
            Theme metropolisDarkTheme = new Theme("MetropolisDark") { IsStandard = true };
            progressBar.SetValue(ThemeManager.ThemeNameProperty, metropolisDarkTheme.Name);
#endif
        }
        #endregion

        #region Event Handlers
        void OnAnimationCompleted(object sender, EventArgs e) {
            this.board.Completed -= OnAnimationCompleted;
            this.Close();
        }
        #endregion
    }
}
