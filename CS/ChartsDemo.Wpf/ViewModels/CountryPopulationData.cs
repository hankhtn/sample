using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("CountryPopulationData")]
    public class CountryPopulationData : List<CountryPopulation> {
        static List<CountryPopulation> data = null;

        public static List<CountryPopulation> Data {
            get {
                if(data == null) {
                    var s = new XmlSerializer(typeof(CountryPopulationData));
                    data = (List<CountryPopulation>)s.Deserialize(DataLoader.LoadFromResources("/Data/CountryPopulationData.xml"));
                }
                return data;
            }
        }
    }
    public class CountryPopulation {
        public string Country { get; set; }
        public int Population { get; set; }
    }
}
