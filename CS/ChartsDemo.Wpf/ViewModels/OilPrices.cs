using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("OilPrices")]
    public class OilPrices : List<OilPricesByCompany> {
        static List<OilPricesByCompany> dataSource = null;
        public static List<OilPricesByCompany> DataSource {
            get {
                if(dataSource == null) {
                    var s = new XmlSerializer(typeof(OilPrices));
                    dataSource = (List<OilPricesByCompany>)s.Deserialize(DataLoader.LoadFromResources("/Data/OilPrices.xml"));
                }
                return dataSource;
            }
        }
        public static List<OilPriceByDate> EuropeBrentPrices { get { return OilPrices.DataSource.First(x => x.CompanyName == "Europe Brent").Prices; } }
    }
    public class OilPricesByCompany {
        public string CompanyName { get; set; }
        public List<OilPriceByDate> Prices { get; set; }
    }
    public class OilPriceByDate {
        public DateTime Timestamp { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
