using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Charts;
using System.Linq;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/CurrencyExchangeRates.xaml")]
    [CodeFile("Modules/Interactivity/CurrencyExchangeRates.xaml.(cs)")]
    [CodeFile("ViewModels/ExchangeRatesModel.(cs)")]
    public partial class CurrencyExchangeRates : ChartsDemoModule {
        public CurrencyExchangeRates() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
}
