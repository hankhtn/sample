using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomFinancial2DControl.xaml"),
     CodeFile("Modules/Appearance/CustomFinancial2DControl.xaml.(cs)")]
    public partial class CustomFinancial2DControl : ChartsDemoModule {
        public static List<FinancialPoint> Data {
            get {
                var data = new List<FinancialPoint> {
                    FinancialPoint.Create(1, 1.1, 2.9, 2.7, 1.6),
                    FinancialPoint.Create(2, 1.9, 2.6, 2.4, 2.1),
                    FinancialPoint.Create(3, 0.7, 2.4, 1.3, 2.1),
                    FinancialPoint.Create(4, 1.8, 2.8, 2.4, 1.9),
                    FinancialPoint.Create(5, 2.1, 3.4, 2.3, 3.1),
                    FinancialPoint.Create(6, 2.2, 3.2, 3.0, 2.6),
                    FinancialPoint.Create(7, 1.4, 2.7, 2.3, 2.5),
                    FinancialPoint.Create(8, 2.1, 3.6, 3.2, 2.7),
                    FinancialPoint.Create(9, 1.2, 3.1, 1.6, 2.6),
                    FinancialPoint.Create(10, 2.7, 4.1, 3.4, 4.0)
                };
                return data;
            }
        }

        public CustomFinancial2DControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
