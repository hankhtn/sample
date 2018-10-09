using System.Collections.Generic;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("Countries")]
    public class CountryData : List<Country> {
        static List<Country> data = null;

        public static List<Country> Data {
            get {
                if (data == null) {
                    XmlSerializer serializer = new XmlSerializer(typeof(CountryData));
                    data = (List<Country>)serializer.Deserialize(DataLoader.LoadFromResources("/Data/Countries.xml"));
                    data.Sort(CompareCoutriesByArea);
                }
                return data;
            }
        }

        static int CompareCoutriesByArea(Country country1, Country country2) {
            return country2.Area.CompareTo(country1.Area);
        }
    }

    public class Country {
        public double Area { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
    }
}
