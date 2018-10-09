using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("RegionPopulationData")]
    public class RegionPopulationData : List<RegionPopulation> {
        static List<RegionPopulation> data = null;

        public static List<RegionPopulation> Data {
            get {
                if(data == null) {
                    var s = new XmlSerializer(typeof(RegionPopulationData));
                    data = (List<RegionPopulation>)s.Deserialize(DataLoader.LoadFromResources("/Data/RegionPopulationData.xml"));
                }
                return data;
            }
        }
    }
    public class RegionPopulation {
        public string Region { get; set; }
        public List<PopulationByYear> Items { get; set; }
    }
    public class PopulationByYear {
        public int Year { get; set; }
        public int Population { get; set; }
    }
}
