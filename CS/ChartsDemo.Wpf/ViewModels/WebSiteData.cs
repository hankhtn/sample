using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartsDemo {
    public static class WebSiteData {
        public static readonly List<WebSiteCategoryInfo> Categories;

        static WebSiteData() {
            var data = CreateDataSource();
            Categories = new[] { "Politics", "Entertainment", "Travel" }
                .Select(x => new WebSiteCategoryInfo { Category = x, Data = data })
                .ToList();
        }

        static IList<WebSitePopularity> CreateDataSource() {
            List<WebSitePopularity> dataSource = new List<WebSitePopularity>();
            int year = DateTime.Now.Year;
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 1), 65, 56, 45));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 2), 78, 45, 40));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 3), 95, 70, 56));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 4), 110, 82, 47));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 5), 108, 80, 38));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 6), 52, 20, 31));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 7), 46, 10, 27));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 8), 70, null, 37));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 9), 86, null, 42));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 10), 92, 65, null));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 11), 108, 45, 37));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 12), 115, 56, 21));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 13), 75, 10, 10));
            dataSource.Add(new WebSitePopularity(new DateTime(year, 1, 14), 65, 0, 5));
            return dataSource;
        }
    }
    public class WebSiteCategoryInfo {
        public string Category { get; set; }
        public IList<WebSitePopularity> Data { get; set; }
    }
    public class WebSitePopularity {
        readonly DateTime date;
        readonly double? politics;
        readonly double? entertainment;
        readonly double? travel;

        public DateTime Date { get { return date; } }
        public double? Politics { get { return politics; } }
        public double? Entertainment { get { return entertainment; } }
        public double? Travel { get { return travel; } }

        public WebSitePopularity(DateTime date, double? politics, double? entertainment, double? travel) {
            this.date = date;
            this.politics = politics;
            this.entertainment = entertainment;
            this.travel = travel;
        }
    }
}
