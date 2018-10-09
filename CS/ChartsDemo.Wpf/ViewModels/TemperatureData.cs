using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("TemperatureData")]
    public class TemperatureData : List<TemperaturesInfo> {
        static List<TemperaturesInfo> londonData = null;
        public static List<TemperaturesInfo> LondonData {
            get {
                if(londonData == null) {
                    var s = new XmlSerializer(typeof(TemperatureData));
                    londonData = (List<TemperaturesInfo>)s.Deserialize(DataLoader.LoadFromResources("/Data/LondonTemperature.xml"));
                }
                return londonData;
            }
        }
    }
    public class TemperaturesInfo {
        public string Description { get; set; }
        public List<TemperatureInfo> Items { get; set; }
    }
    public class TemperatureInfo {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
    }
}
