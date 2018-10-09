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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DevExpress.StockMarketTrader {
    
    
    
    public partial class SparklineTileControl : UserControl {
        public SparklineTileControl(System.Drawing.Size tileSize) {
            InitializeComponent();
            Width = tileSize.Width;
            Height = tileSize.Height;
            if (Width == Height) textContainer.Opacity = 0;
        }
    }
}
