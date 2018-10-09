using System;
using System.Globalization;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TreeMapDemo {
    public partial class Hierarchical : TreeMapDemoModule {
        public Hierarchical() {
            InitializeComponent();
            DataContext = LoadDataFromXML();
        }
        List<CountryEnergyInfo> LoadDataFromXML() {
            XDocument document = DataLoader.LoadXDocumentFromResources("/Data/EnergyStatistic.xml");
            List<CountryEnergyInfo> infos = new List<CountryEnergyInfo>();
            if (document != null) {
                foreach (XElement element in document.Element("ArrayOfEnergyStatistic").Elements()) {
                    CountryEnergyInfo energyInfo = new CountryEnergyInfo();
                    energyInfo.Country = element.Attribute("Country").Value;
                    foreach (XElement energyElement in element.Elements()) {
                        EnergyStatisticItem item = new EnergyStatisticItem();
                        item.TypeName = energyElement.Attribute("TypeName").Value;
                        item.Value = Convert.ToDouble(energyElement.Attribute("Value").Value, CultureInfo.InvariantCulture);
                        energyInfo.EnergyStatistic.Add(item);
                    }
                    infos.Add(energyInfo);
                }
            }
            return infos;
        }
    }

    public class CountryEnergyInfo {
        readonly List<EnergyStatisticItem> energyStatistic = new List<EnergyStatisticItem>();
        public List<EnergyStatisticItem> EnergyStatistic { get { return energyStatistic; } }
        public string Country { get; set; }
    }

    public class EnergyStatisticItem {
        public double Value { get; set; }
        public string TypeName { get; set; }
    }
}
