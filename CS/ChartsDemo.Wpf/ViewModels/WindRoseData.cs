using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("WindRoseData")]
    public class WindRoseData : List<WindInfo> {
        static List<WindInfo> data = null;

        public static List<WindInfo> Data {
            get {
                if(data == null) {
                    var s = new XmlSerializer(typeof(WindRoseData));
                    data = (List<WindInfo>)s.Deserialize(DataLoader.LoadFromResources("/Data/WindRoseData.xml"));
                }
                return data;
            }
        }
    }
    public class WindInfo {
        public string Direction { get; set; }
        public int Speed { get; set; }
    }
}
