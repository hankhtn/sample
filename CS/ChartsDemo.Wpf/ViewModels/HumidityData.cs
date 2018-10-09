using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("HumidityData")]
    public class HumidityData : List<HumidityInfo> {
        static List<HumidityInfo> londonData = null;
        public static List<HumidityInfo> LondonData {
            get {
                if(londonData == null) {
                    var s = new XmlSerializer(typeof(HumidityData));
                    londonData = (List<HumidityInfo>)s.Deserialize(DataLoader.LoadFromResources("/Data/LondonHumidity.xml"));
                }
                return londonData;
            }
        }
    }
    public class HumidityInfo {
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }
    }
}
