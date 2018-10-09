using System;
using System.Windows;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/ColorizerControl.xaml"),
     CodeFile("Modules/DataBinding/ColorizerControl.xaml.(cs)")]
    public partial class ColorizerControl : ChartsDemoModule {
        public ColorizerControl() {
            InitializeComponent();
            ActualChart = chart;
            series.DataSource = GetHPIs();
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        List<HPIStatistics> GetHPIs() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/HPI.xml");
            List<HPIStatistics> result = new List<HPIStatistics>();
            if (document != null) {
                foreach (XElement element in document.Element("G20HPIs").Elements()) {
                    string country = element.Element("Country").Value;
                    double population = int.Parse(element.Element("Population").Value) / 1000000.0d;
                    double hpi = Convert.ToDouble(element.Element("HPI").Value, CultureInfo.InvariantCulture);
                    decimal product = Convert.ToDecimal(element.Element("Product").Value, CultureInfo.InvariantCulture);
                    result.Add(new HPIStatistics(country, population, hpi, product, string.Format("{0:n2}", hpi)));
                }
            }
            return result;
        }
    }

    public class HPIStatistics {
        public string Country { get; private set; }
        public double Population { get; private set; }
        public double HPI { get; private set; }
        public string HPIHint { get; private set; }
        public decimal Product { get; private set; }

        public HPIStatistics(string country, double population, double hpi, decimal product, string hpiHint) {
            this.Country = country;
            this.Population = population;
            this.HPI = hpi;
            this.Product = product;
            this.HPIHint = hpiHint;
        }
    }
}
