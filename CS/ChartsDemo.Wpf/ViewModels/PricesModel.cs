using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartsDemo {
    public class PricesModel {
        #region Data

        const int PointCount = 1500;
        static List<PricesModel> data;

        public static List<PricesModel> Data { get { return data ?? (data = GetData()); } }
        public static DateTime MinVisibleDate { get { return Data.First().ProductData[PointCount - 150].TradeDate; } }

        static List<PricesModel> GetData() {
            var startDate = DateTime.Now.Subtract(TimeSpan.FromDays(PointCount));
            var random = new Random();
            return Enumerable.Range(1, 3).Select(x => new PricesModel { ProductName = "Product" + x, ProductData = CreateProductData(startDate, random) }).ToList();
        }

        #endregion
        #region Data Generator

        static List<PriceByDate> CreateProductData(DateTime date, Random random) {
            var price = GenerateStartValue(random);
            var productData = new List<PriceByDate>();
            for(int i = 0; i < PointCount; i++) {
                productData.Add(new PriceByDate(date, price));
                price += GenerateAddition(random);
                date = date.AddDays(1);
            }
            return productData;
        }
        static double GenerateAddition(Random random) {
            double factor = random.NextDouble();
            if(factor == 1)
                factor = 5;
            else if(factor == 0)
                factor = -5;
            return (factor - 0.475) * 10;
        }
        static double GenerateStartValue(Random random) {
            return random.NextDouble() * 100;
        }

        #endregion
        public string ProductName { get; private set; }
        public List<PriceByDate> ProductData { get; private set; }
    }

    public class PriceByDate {
        double price;
        DateTime tradeDate;

        public double Price {
            get { return price; }
        }
        public DateTime TradeDate {
            get { return tradeDate; }
        }

        public PriceByDate(DateTime date, double price) {
            this.tradeDate = date;
            this.price = price;
        }
    }
}
