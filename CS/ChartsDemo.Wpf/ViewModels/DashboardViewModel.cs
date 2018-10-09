using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Map;
using System.Xml.Serialization;
using System.Linq;

namespace ChartsDemo {
    public class DashboardViewModel {
        public List<CountryStatisticInfo> CountriesData { get { return CountriesInfo.DataSource; } }
        public virtual CountryStatisticInfo SelectedCountry { get; set; }
        public virtual string SelectedCountryName { get; set; }

        public DashboardViewModel() {
            SelectedCountry = CountriesData.First();
        }

        protected void OnSelectedCountryChanged() {
            SelectedCountryName = SelectedCountry != null ? SelectedCountry.Name : string.Empty;
        }
        protected void OnSelectedCountryNameChanged() {
            SelectedCountry = CountriesData.FirstOrDefault(x => x.Name == SelectedCountryName);
        }
    }

    [XmlRoot("CountriesInfo")]
    public class CountriesInfo : List<CountryStatisticInfo> {
        static List<CountryStatisticInfo> dataSource = null;
        public static List<CountryStatisticInfo> DataSource {
            get {
                if(dataSource == null) {
                    var s = new XmlSerializer(typeof(CountriesInfo));
                    dataSource = (List<CountryStatisticInfo>)s.Deserialize(DataLoader.LoadFromResources("/Data/Top10LargestCountriesInfo.xml"));
                }
                return dataSource;
            }
        }
    }

    [XmlType("CountryInfo")]
    public class CountryStatisticInfo {
        public string Name { get; set; }
        [XmlArray("Statistic")]
        [XmlArrayItem("PopulationStatisticByYear")]
        public List<PopulationStatisticByYear> PopulationDynamic { get; set; }
        public double AreaSqrKilometers { get; set; }
        public double AreaMSqrKilometers { get { return AreaSqrKilometers / 1000000; } }
    }

    public class PopulationStatisticByYear {
        public int Year { get; set; }
        public double Population { get; set; }
        public double UrbanPercent { get; set; }

        public double PopulationMillionsOfPeople { get { return Population / 1000000; } }
        public double RuralPercent { get { return 100 - UrbanPercent; } }
    }
}
