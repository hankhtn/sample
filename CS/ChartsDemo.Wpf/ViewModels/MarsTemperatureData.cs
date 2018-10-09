using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("MarsTemperatureData")]
    public class MarsTemperatureData : List<MarsTemperature> {
        static List<MarsTemperature> fullData = null;
        static List<MarsTemperature> data = null;

        public static List<MarsTemperature> FullData {
            get {
                if(fullData == null) {
                    var s = new XmlSerializer(typeof(MarsTemperatureData));
                    fullData = (List<MarsTemperature>)s.Deserialize(DataLoader.LoadFromResources("/Data/MarsTemperatureData.xml"));
                }
                return fullData;
            }
        }
        public static List<MarsTemperature> Data { get { return data ?? (data = FullData.Take(31).ToList()); } }
    }
    public class MarsTemperature {
        public double Sol { get; set; }
        public double Temperature { get; set; }
    }
}
