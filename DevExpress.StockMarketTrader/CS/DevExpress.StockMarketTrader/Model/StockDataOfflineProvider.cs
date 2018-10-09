using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using DevExpress.StockMarketTrader.StockDataServiceReference;

namespace DevExpress.StockMarketTrader.Model.Offline {
    public class Company {
        public Company() {
        }

        public string CompanyName { get; set; }

        public int ID { get; set; }
    }
    public class OfflineStockData {

        public OfflineStockData() { }

        public double CloseP { get; set; }

        public int CompanyID { get; set; }

        public DateTime Date { get; set; }

        public double HighP { get; set; }

        public double LowP { get; set; }

        public double OpenP { get; set; }

        public double Price { get; set; }

        public int Volume { get; set; }
    }
    public class XmlStockDataProvider {
        static string companiesPath = "DevExpress.StockMarketTrader.Data.Companies.xml";
        static string dataPath = "DevExpress.StockMarketTrader.Data.StockData.xml";
        static XmlStockDataProvider() {
            PopulateDataFromXml();
        }

        private static void PopulateDataFromXml() {
            PopulateCompanies();
            PopulateStockData();
        }

        private static void PopulateStockData() {
            Stream stream = GetResourceStream(dataPath);
            if (stream != null) {
                stockData = new List<OfflineStockData>();
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                var nodes = doc.LastChild.SelectNodes("//StockData");
                if (nodes != null) {
                    foreach (XmlNode node in nodes) {
                        var data = new OfflineStockData() {
                            CompanyID = Int32.Parse(node.Attributes["Name"].Value),
                            Date = DateTime.Parse(node.Attributes["Date"].Value, CultureInfo.InvariantCulture),
                            Price = Double.Parse(node.Attributes["Price"].Value, CultureInfo.InvariantCulture),
                            OpenP = Double.Parse(node.Attributes["Open"].Value, CultureInfo.InvariantCulture),
                            CloseP = Double.Parse(node.Attributes["Close"].Value, CultureInfo.InvariantCulture),
                            HighP = Double.Parse(node.Attributes["High"].Value, CultureInfo.InvariantCulture),
                            LowP = Double.Parse(node.Attributes["Low"].Value, CultureInfo.InvariantCulture),
                            Volume = int.Parse(node.Attributes["Volume"].Value, CultureInfo.InvariantCulture),
                        };
                        stockData.Add(data);
                    }
                }
            }
            stream.Close();
        }

        private static void PopulateCompanies() {
            Stream stream = GetResourceStream(companiesPath);
            if (stream != null) {
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                var nodes =  doc.LastChild != null ? doc.LastChild.SelectNodes("//Company") : null;
                companies = new List<Company>();
                if (nodes != null) {
                    foreach (XmlNode node in nodes) {
                        companies.Add(new Company() { ID = Int32.Parse(node.Attributes["Id"].Value), CompanyName = node.Attributes["Name"].Value });
                    }
                }
            }
            stream.Close();
        }

        private static Stream GetResourceStream(string path) {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
        }
        static List<Company> companies;
        public List<Company> Companies {
            get { return companies; }
        }
        static List<OfflineStockData> stockData;
        public List<OfflineStockData> StockData {
            get { return stockData; }
        }
    }
    public class StockDataOfflineService : System.ServiceModel.ClientBase<DevExpress.StockMarketTrader.StockDataServiceReference.IStockDataService>, DevExpress.StockMarketTrader.StockDataServiceReference.IStockDataService {

        XmlStockDataProvider dataProvider;
        Dictionary<IAsyncResult, Delegate> executions;
        object sem = new object();
        public StockDataOfflineService() {
            dataProvider = new XmlStockDataProvider();
            executions = new Dictionary<IAsyncResult, Delegate>();
        }
        #region IStockDataService Members

        string StockDataServiceReference.IStockDataService.Test() { return ""; }

        IAsyncResult StockDataServiceReference.IStockDataService.BeginTest(AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        string StockDataServiceReference.IStockDataService.EndTest(IAsyncResult result) {
            throw new NotImplementedException();
        }

        void StockDataServiceReference.IStockDataService.Initialize() {
            throw new NotImplementedException();
        }

        IAsyncResult StockDataServiceReference.IStockDataService.BeginInitialize(AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        void StockDataServiceReference.IStockDataService.EndInitialize(IAsyncResult result) {
            throw new NotImplementedException();
        }
        private int GetCompanyID(string companyName) {
            lock (sem) {
                var result = GetCompanies().Where(e => e.CompanyName == companyName).Select(e => e.ID).ToList();
                return result.Count > 0 ? result[0] : 0;
            }
        }
        public List<Company> GetCompanies() {
            return dataProvider.Companies;
        }
        public List<OfflineStockData> GetStockData() {
            return dataProvider.StockData;
        }
        StockDataServiceReference.StockData[] StockDataServiceReference.IStockDataService.GetAllPeriodData(string companyName) {
            throw new NotImplementedException();
        }

        
        
        
        
        
        
        
        
        
        
        

        IAsyncResult StockDataServiceReference.IStockDataService.BeginGetAllPeriodData(string companyName, AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        StockDataServiceReference.StockData[] StockDataServiceReference.IStockDataService.EndGetAllPeriodData(IAsyncResult result) {
            throw new NotImplementedException();
        }
        static DateTime[] dates;
        public DateTime[] GetDates() {
            lock (sem) {
                if (dates == null || dates.Length == 0)
                    dates = GetStockData().Select(e => e.Date).Distinct().OrderBy(e => e.Date).ToArray();
                return dates;
            }
        }

        public IAsyncResult BeginGetDates(AsyncCallback callback, object asyncState) {
            Func<DateTime[]> f = new Func<DateTime[]>(GetDates);
            var result = f.BeginInvoke(callback, asyncState);
            AddExecution(f, result);
            return result;
        }

        private void AddExecution(Delegate d, IAsyncResult result) {
            lock (executions) executions.Add(result, d);
        }

        public DateTime[] EndGetDates(IAsyncResult result) {
            var endResult = ((Func<DateTime[]>)executions[result]).EndInvoke(result);
            RemoveExecution(result);
            return endResult;
        }

        private void RemoveExecution(IAsyncResult result) {
            lock (executions) executions.Remove(result);
        }

        public StockDataServiceReference.StockData[] GetStockDataByDate(DateTime currentDate) {
            lock (sem) {
                StockData[] result = GetStockData().Where(e => e.Date == currentDate).OrderBy(e => e.CompanyID).Select(e => GetServiceStockData(e)).ToArray();
                return result;
            }
        }

        public IAsyncResult BeginGetStockDataByDate(DateTime currentDate, AsyncCallback callback, object asyncState) {
            Func<DateTime, StockData[]> f = new Func<DateTime, StockData[]>(GetStockDataByDate);
            var result = f.BeginInvoke(currentDate, callback, asyncState);
            AddExecution(f, result);
            return result;
        }

        public StockDataServiceReference.StockData[] EndGetStockDataByDate(IAsyncResult result) {
            var endResult = ((Func<DateTime, StockData[]>)GetExecution(result)).EndInvoke(result);
            RemoveExecution(result);
            return endResult;
        }

        public string[] GetCompaniesName() {
            lock (sem) {
                return GetCompanies().Select(e => e.CompanyName).ToArray();
            }
        }

        public IAsyncResult BeginGetCompaniesName(AsyncCallback callback, object asyncState) {
            Func<string[]> f = new Func<string[]>(GetCompaniesName);
            var result = f.BeginInvoke(callback, asyncState);
            AddExecution(f, result);
            return result;
        }

        public string[] EndGetCompaniesName(IAsyncResult result) {
            var endResult = ((Func<string[]>)GetExecution(result)).EndInvoke(result);
            RemoveExecution(result);
            return endResult;
        }

        private Delegate GetExecution(IAsyncResult result) {
            lock (executions) return executions[result];
        }

        public StockDataServiceReference.StockData[] GetCompanyStockData(DateTime date, string companyName) {
            lock (sem) {
                int id = GetCompanyID(companyName);
                return GetStockData().Where(e => e.CompanyID == id && e.Date == date).Select(e => GetServiceStockData(e)).ToArray();
            }
        }

        public IAsyncResult BeginGetCompanyStockData(DateTime date, string companyName, AsyncCallback callback, object asyncState) {
            Func<DateTime, string, StockData[]> f = new Func<DateTime, string, StockData[]>(GetCompanyStockData);
            var result = f.BeginInvoke(date, companyName, callback, asyncState);
            AddExecution(f, result);
            return result;
        }

        public StockDataServiceReference.StockData[] EndGetCompanyStockData(IAsyncResult result) {
            var endresult = ((Func<DateTime, string, StockData[]>)GetExecution(result)).EndInvoke(result);
            RemoveExecution(result);
            return endresult;
        }

        StockDataServiceReference.StockData[] StockDataServiceReference.IStockDataService.GetCompanyStockDataSL(DateTime newDate, DateTime oldDate, string companyName) {
            throw new NotImplementedException();
        }

        IAsyncResult StockDataServiceReference.IStockDataService.BeginGetCompanyStockDataSL(DateTime newDate, DateTime oldDate, string companyName, AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        StockDataServiceReference.StockData[] StockDataServiceReference.IStockDataService.EndGetCompanyStockDataSL(IAsyncResult result) {
            throw new NotImplementedException();
        }

        double StockDataServiceReference.IStockDataService.GetHighestPrice(string companyName, DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        IAsyncResult StockDataServiceReference.IStockDataService.BeginGetHighestPrice(string companyName, DateTime start, DateTime end, AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        double StockDataServiceReference.IStockDataService.EndGetHighestPrice(IAsyncResult result) {
            throw new NotImplementedException();
        }

        double StockDataServiceReference.IStockDataService.GetLowestPrice(string companyName, DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        IAsyncResult StockDataServiceReference.IStockDataService.BeginGetLowestPrice(string companyName, DateTime start, DateTime end, AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        double StockDataServiceReference.IStockDataService.EndGetLowestPrice(IAsyncResult result) {
            throw new NotImplementedException();
        }

        int[] StockDataServiceReference.IStockDataService.GetTopRatedCompanyIDs(DateTime date) {
            throw new NotImplementedException();
        }

        IAsyncResult StockDataServiceReference.IStockDataService.BeginGetTopRatedCompanyIDs(DateTime date, AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        int[] StockDataServiceReference.IStockDataService.EndGetTopRatedCompanyIDs(IAsyncResult result) {
            throw new NotImplementedException();
        }

        StockDataServiceReference.TopRatedCompanyData[] StockDataServiceReference.IStockDataService.GetTopRatedCompaniesDataSL(DateTime start, DateTime end, string selectedCompany) {
            throw new NotImplementedException();
        }

        IAsyncResult StockDataServiceReference.IStockDataService.BeginGetTopRatedCompaniesDataSL(DateTime start, DateTime end, string selectedCompany, AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        StockDataServiceReference.TopRatedCompanyData[] StockDataServiceReference.IStockDataService.EndGetTopRatedCompaniesDataSL(IAsyncResult result) {
            throw new NotImplementedException();
        }
        int[] GetTopRatedCompanyIDs(DateTime date) {
            IOrderedEnumerable<OfflineStockData> data = GetStockDataByDate2(date).OrderByDescending((e => e.Volume));
            int[] ids = data.Select(e => e.CompanyID).ToArray();
            int[] result = new int[ids.Length];
            for (int i = 0; i < result.Length; i++) {
                result[i] = ids[i];
            }
            return result;
        }

        private OfflineStockData[] GetStockDataByDate2(DateTime currentDate) {
            List<OfflineStockData> data = GetStockData();
            OfflineStockData[] result = data.Where(e => e.Date == currentDate).OrderBy(e => e.CompanyID).ToArray();
            return result;
        }
        public StockDataServiceReference.CompaniesVolumeData[] GetCompaniesVolumeFromPeriod(DateTime start, DateTime end) {
            lock (sem) {
                int[] topIds = GetTopRatedCompanyIDs(start);
                CompaniesVolumeData[] result = new CompaniesVolumeData[topIds.Length];
                for (int i = 0; i < topIds.Length; i++) {
                    int volume = GetStockData().Where(e => e.Date >= start && e.CompanyID == topIds[i]).Select(e => e.Volume).ToArray()[0];
                    CompaniesVolumeData data = new CompaniesVolumeData() { CompanyName = GetCompanyNameByID(topIds[i]), Volume = (int)volume };
                    result[i] = data;
                }
                return result;
            }
        }

        private string GetCompanyNameByID(int id) {
            lock (sem) {
                return GetCompanies().Where(e => e.ID == id).Select(e => e.CompanyName).ToArray()[0];
            }
        }

        public IAsyncResult BeginGetCompaniesVolumeFromPeriod(DateTime start, DateTime end, AsyncCallback callback, object asyncState) {
            Func<DateTime, DateTime, CompaniesVolumeData[]> f = new Func<DateTime, DateTime, CompaniesVolumeData[]>(GetCompaniesVolumeFromPeriod);
            var result = f.BeginInvoke(start, end, callback, asyncState);
            AddExecution(f, result);
            return result;
        }

        public StockDataServiceReference.CompaniesVolumeData[] EndGetCompaniesVolumeFromPeriod(IAsyncResult result) {
            var endResult = ((Func<DateTime, DateTime, CompaniesVolumeData[]>)GetExecution(result)).EndInvoke(result);
            RemoveExecution(result);
            return endResult;
        }

        StockDataServiceReference.CompanyData StockDataServiceReference.IStockDataService.GetCompanyData(DateTime newDate, DateTime oldDate, string companyName) {
            throw new NotImplementedException();
        }

        IAsyncResult StockDataServiceReference.IStockDataService.BeginGetCompanyData(DateTime newDate, DateTime oldDate, string companyName, AsyncCallback callback, object asyncState) {
            throw new NotImplementedException();
        }

        StockDataServiceReference.CompanyData StockDataServiceReference.IStockDataService.EndGetCompanyData(IAsyncResult result) {
            throw new NotImplementedException();
        }

        public StockDataServiceReference.CompanyData[] GetCompanyMultipleDataFromPeriod(int currentDate, int count, int periodSize, string companyName) {
            lock (sem) {
                List<DateTime> datesList = new List<DateTime>();
                int dateCount = currentDate;
                for (int i = 0; i < (count + 1) * periodSize; i++) {
                    if (dateCount < dates.Length) {
                        datesList.Add(dates[dateCount]);
                        dateCount++;
                    }
                }
                OfflineStockData[] data = GetMultipleCompanyStockData(datesList, companyName);
                List<CompanyData> result = new List<CompanyData>();
                for (int i = 0; i < count; i++) {
                    DateTime nextDate = datesList[(i + 1) * periodSize];
                    double closePrice = (double)data[(i + 1) * periodSize].CloseP;
                    double highPrice = GetHighLowPriceBetweenDates(data, dates[currentDate], dates[currentDate + periodSize], companyName, true);
                    double lowPrice = GetHighLowPriceBetweenDates(data, dates[currentDate], dates[currentDate + periodSize], companyName, false);
                    currentDate += periodSize;
                    result.Add(new CompanyData() { Data = GetServiceStockData(data[i * periodSize]), HighPrice = highPrice, LowPrice = lowPrice, ClosePrice = closePrice });
                }
                return result.ToArray();
            }
        }

        private StockData GetServiceStockData(OfflineStockData data) {
            return new StockData() {
                CompanyID = data.CompanyID,
                Date = data.Date,
                Price = Convert.ToDecimal(data.Price),
                OpenP = Convert.ToDecimal(data.OpenP),
                CloseP = Convert.ToDecimal(data.CloseP),
                HighP = Convert.ToDecimal(data.HighP),
                LowP = Convert.ToDecimal(data.LowP),
                Volumne = data.Volume
            };
        }

        private double GetHighLowPriceBetweenDates(OfflineStockData[] data, DateTime start, DateTime end, string companyName, bool isMax) {
            int id = GetCompanyID(companyName);
            if (isMax)
                return (double)data.Where(e => e.Date >= start && e.Date <= end && e.CompanyID == id).Select(e => e.HighP).Max();
            else
                return (double)data.Where(e => e.Date >= start && e.Date <= end && e.CompanyID == id).Select(e => e.LowP).Min();
        }

        private OfflineStockData[] GetMultipleCompanyStockData(List<DateTime> datesList, string companyName) {
            int id = GetCompanyID(companyName);
            return GetStockData().Where(e => e.CompanyID == id && datesList.Contains(e.Date)).Select(e => e).OrderBy(e => e.Date).ToArray();
        }

        public IAsyncResult BeginGetCompanyMultipleDataFromPeriod(int currentDate, int count, int periodSize, string companyName, AsyncCallback callback, object asyncState) {
            Func<int, int, int, string, CompanyData[]> f = new Func<int, int, int, string, CompanyData[]>(GetCompanyMultipleDataFromPeriod);
            var result = f.BeginInvoke(currentDate, count, periodSize, companyName, callback, asyncState);
            AddExecution(f, result);
            return result;
        }

        public StockDataServiceReference.CompanyData[] EndGetCompanyMultipleDataFromPeriod(IAsyncResult result) {
            var endResult = ((Func<int, int, int, string, CompanyData[]>)GetExecution(result)).EndInvoke(result);
            RemoveExecution(result);
            return endResult;
        }

        public StockDataServiceReference.CompanyStockData[] GetStockDataFromPeriodByCompanyList(int currentDate, int count, int periodSize, string[] companies) {
            lock (sem) {
                List<CompanyStockData> result = new List<CompanyStockData>();
                foreach (string company in companies) {
                    var data = GetCompanyMultipleDataFromPeriod(currentDate, count, periodSize, company);
                    result.Add(new CompanyStockData() { Data = data.Select(d => d.Data).ToArray(), CompanyName = company });
                }
                return result.ToArray();
            }
        }

        public IAsyncResult BeginGetStockDataFromPeriodByCompanyList(int currentDate, int count, int periodSize, string[] companies, AsyncCallback callback, object asyncState) {
            Func<int, int, int, string[], CompanyStockData[]> f = new Func<int, int, int, string[], CompanyStockData[]>(GetStockDataFromPeriodByCompanyList);
            var result = f.BeginInvoke(currentDate, count, periodSize, companies, callback, asyncState);
            AddExecution(f, result);
            return result;
        }

        public StockDataServiceReference.CompanyStockData[] EndGetStockDataFromPeriodByCompanyList(IAsyncResult result) {
            var endResult = ((Func<int, int, int, string[], CompanyStockData[]>)GetExecution(result)).EndInvoke(result);
            RemoveExecution(result);
            return endResult;
        }

        public StockDataServiceReference.CompanyStockData[] GetStockDataFromDateByCompanyList(DateTime date, string[] companies) {
            lock (sem) {
                List<CompanyStockData> result = new List<CompanyStockData>();
                foreach (string company in companies) {
                    StockData[] data = ((IStockDataService)this).GetCompanyStockData(date, company);
                    result.Add(new CompanyStockData() { Data = data, CompanyName = company });
                }
                return result.ToArray();
            }
        }

        public IAsyncResult BeginGetStockDataFromDateByCompanyList(DateTime date, string[] companies, AsyncCallback callback, object asyncState) {
            Func<DateTime, string[], StockDataServiceReference.CompanyStockData[]> f = new Func<DateTime, string[], StockDataServiceReference.CompanyStockData[]>(GetStockDataFromDateByCompanyList);
            var result = f.BeginInvoke(date, companies, callback, asyncState);
            AddExecution(f, result);
            return result;
        }

        public StockDataServiceReference.CompanyStockData[] EndGetStockDataFromDateByCompanyList(IAsyncResult result) {
            var endResult = ((Func<DateTime, string[], StockDataServiceReference.CompanyStockData[]>)GetExecution(result)).EndInvoke(result);
            RemoveExecution(result);
            return endResult;
        }

        #endregion
    }
}
