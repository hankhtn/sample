using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("WeatherData")]
    public class WeatherData : List<WeatherInfo> {
        static List<WeatherInfo> data = null;

        public static List<WeatherInfo> Data {
            get {
                if(data == null) {
                    var s = new XmlSerializer(typeof(WeatherData));
                    data = (List<WeatherInfo>)s.Deserialize(DataLoader.LoadFromResources("/Data/WeatherData.xml"));
                }
                return data;
            }
        }
    }
    public class WeatherInfo {
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double RelativeHumidity { get; set; }
    }
}
