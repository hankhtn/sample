using System.Windows;
using System.Collections.Generic;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Windows.Media;
using System;
using System.Windows.Media.Animation;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/ErrorBarsControl.xaml"),
    CodeFile("Modules/DataAnalysis/ErrorBarsControl.xaml.(cs)")]
    public partial class ErrorBarsControl : ChartsDemoModule {
        #region Data

        public static List<DataItem> Data {
            get {
                return new List<DataItem>() {
                    new DataItem("A", 20, 5, 8),
                    new DataItem("B", 50, 3, 5),
                    new DataItem("C", 40, 20, 10),
                    new DataItem("D", 22, 15, 5),
                    new DataItem("E", 30, 5, 8),
                    new DataItem("F", 45, 5, 4),
                    new DataItem("G", 35, 5, 3),
                    new DataItem("H", 28, 4, 2),
                    new DataItem("I", 46, 6, 4),
                    new DataItem("J", 27, 8, 20),
                    new DataItem("K", 20, 5, 8),
                    new DataItem("L", 50, 3, 5),
                    new DataItem("M", 40, 20, 10),
                    new DataItem("N", 22, 15, 5),
                    new DataItem("O", 30, 5, 8),
                    new DataItem("P", 45, 5, 2),
                    new DataItem("Q", 35, 5, 5),
                    new DataItem("R", 28, 4, 4),
                    new DataItem("S", 46, 6, 5),
                    new DataItem("T", 27, 8, 8),
                };
            }
        }

        #endregion
        public ErrorBarsControl() {
            InitializeComponent();
            ActualChart = chart;
        }
    }

    public class DataItem {
        public string Argument { get; private set; }
        public int Value { get; private set; }
        public int Negative { get; private set; }
        public int Positive { get; private set; }

        public DataItem(string argument, int value, int negative, int positive) {
            Argument = argument;
            Value = value;
            Negative = negative;
            Positive = positive;
        }
    }
}
