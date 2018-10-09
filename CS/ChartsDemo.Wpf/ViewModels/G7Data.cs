using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("G7GDPs")]
    public class G7Data : List<GDP> {
        static List<G7Member> members;
        static List<GDP> data;

        public static List<G7Member> Members { get { return members ?? (members = GetG7Data()); } }
        public static List<GDP> Data { get { return data ?? (data = GetGDPData()); } }

        static List<G7Member> GetG7Data() {
            return Data.GroupBy(x => x.Country).Select(x => new G7Member(x.Key, x.ToList())).ToList();
        }
        static List<GDP> GetGDPData() {
            var s = new XmlSerializer(typeof(G7Data));
            return (List<GDP>)s.Deserialize(DataLoader.LoadFromResources("/Data/GDPofG7.xml"));
        }
    }

    public class G7Member {
        public decimal GDPin2015 { get { return GDPs.First((gdp) => gdp.Year == 2015).Product; } }
        public string CountryName { get; set; }
        public List<GDP> GDPs { get; private set; }

        public G7Member(string countryName, List<GDP> items) {
            this.CountryName = countryName;
            this.GDPs = items;
        }
    }

    public class GDP {
        public string Country { get; set; }
        public int Year { get; set; }
        public decimal Product { get; set; }
    }
}
