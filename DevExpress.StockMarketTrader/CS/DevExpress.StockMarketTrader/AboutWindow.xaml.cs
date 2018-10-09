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
using System.Windows.Navigation;

namespace DevExpress.StockMarketTrader {
    
    
    
    public partial class AboutWindow : Window {
        public AboutWindow() {
            InitializeComponent();
            tbCopyRight.Text = String.Format("Copyright © 1998-{0} Developer Express Inc.", DateTime.Now.Year);
        }
        private void OnHyperlinkClick(object sender, RequestNavigateEventArgs e) {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e) {
            Close();
        }

        private void OnSupportClick(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start(AssemblyInfo.DXLinkGetSupport);
        }

        private void OnBuyNowClick(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start(AssemblyInfo.DXLinkCompare);
        }

        private void OnDiscountsClick(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start(AssemblyInfo.DXLinkCompetitiveDiscounts);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }
    }
}
