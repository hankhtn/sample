using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ChartsDemo {
    public static class ExchangeRatesModel {
        static List<PriceByDate> _gbpUsdRate;
        static List<PriceByDate> _eurUsdRate;

        public static List<PriceByDate> GbpUsdRate {
            get { return _gbpUsdRate ?? (_gbpUsdRate = LoadData(DataLoader.LoadXmlFromResources("/Data/GbpUsdRate.xml"))); }
        }
        public static List<PriceByDate> EurUsdRate {
            get { return _eurUsdRate ?? (_eurUsdRate = LoadData(DataLoader.LoadXmlFromResources("/Data/EurUsdRate.xml"))); }
        }

        static List<PriceByDate> LoadData(XDocument document) {
            if(document == null) return new List<PriceByDate>();
            return document.Descendants("CurrencyRate").Select(element => {
                CultureInfo culture = CultureInfo.InvariantCulture;
                var date = DateTime.Parse(element.Element("DateTime").Value, culture);
                var price = double.Parse(element.Element("Rate").Value, culture);
                return new PriceByDate(date, price);
            }).ToList();
        }
    }
}
