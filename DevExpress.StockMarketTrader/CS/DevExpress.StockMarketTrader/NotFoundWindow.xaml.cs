using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DevExpress.StockMarketTrader {
    
    
    
    public partial class NotFoundWindow : ThemedWindow {
        public NotFoundWindow() {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Process.Start(@"http://apps.microsoft.com/windows/en-us/app/devexpress-live-tile-manager/acdad900-0021-4cbd-90cd-4ae5a03e91f5");
            Close();
        }
    }
}
