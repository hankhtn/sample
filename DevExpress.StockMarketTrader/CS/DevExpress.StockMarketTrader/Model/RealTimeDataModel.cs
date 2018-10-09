using System;
using DevExpress.StockMarketTrader.StockDataServiceReference;
using System.Windows.Threading;
using System.Collections.Generic;
using DevExpress.StockMarketTrader.Model.BusinessObjects;
using System.ServiceModel;


namespace DevExpress.StockMarketTrader.Model {
    public class PriceAscedingComparer : IComparer<TransactionData> {
        public int Compare(TransactionData x, TransactionData y) {
            if (x == null || y == null)
                return 0;
            if (x.Price == y.Price) {
                if (y.Bid == 0 && x.Ask == 0)
                    return -1;
                else return 1;
            }
            if (x.Price < y.Price)
                return -1;
            else
                return 1;
        }
    }
    public class PriceDescedingComparer : IComparer<TransactionData> {
        public int Compare(TransactionData x, TransactionData y) {
            if (x == null || y == null)
                return 0;
            if (x.Price == y.Price) {
                if (y.Ask == 0 && x.Bid == 0)
                    return -1;
                else return 1;
            }
            if (x.Price < y.Price)
                return 1;
            else
                return -1;
        }
    }
    public class RealTimeDataEventArgs : EventArgs {
        public RealTimeDataEventArgs(object data, string key, bool canNewUpdate) {
            this.Data = data;
            this.Key = key;
            this.CanNewUpdate = canNewUpdate;
        }
        public RealTimeDataEventArgs(string message) {
            this.ExceptionMessage = message;
        }
        public bool CanNewUpdate { get; set; }
        public string Key { get; set; }
        public string ExceptionMessage { get; set; }
        public object Data { get; set; }
    }
    public class RealTimeDataModel {
        Executor executor;
        
        NetworkMonitor2 networkMonitor;
        StockDataServiceClient onlineService;
        DevExpress.StockMarketTrader.Model.Offline.StockDataOfflineService offlineService;
        Random rand;
        List<TransactionData> transactions;
        IComparer<TransactionData> comparer;
        int currentPriceIndex;

        public RealTimeDataModel() {
            this.executor = new Executor();
            this.networkMonitor = new NetworkMonitor2();
            this.networkMonitor.InternetAvailableChanged += networkMonitor_InternetAvailableChanged;
            executor.NetMonitor = networkMonitor;
            this.executor.ExecuteFailed += new EventHandler(OnExecuteFailed);
            this.transactions = new List<TransactionData>();
            this.comparer = new PriceAscedingComparer();
            rand = new Random();
        }

        void networkMonitor_InternetAvailableChanged(object sender, EventArgs e) {
            var oldStatus = executor.Status;
            executor.Status = networkMonitor.IsInternetAvailable == true ? ExecutorStatus.Enabled : ExecutorStatus.Offline;
            if (!isInitialized)
                InitServer();
            else if(oldStatus != executor.Status) {
                ChangeUserState();
                UpdateService();
            }
            if (networkMonitor.IsInternetAvailable == false)
                RaiseUpdateFailed("The remote server is not responding. Check your internet connection.");
            else
                executor.ForceExecute();
        }
        IStockDataService Service { get; set; }
        public event EventHandler<RealTimeDataEventArgs> GetDataCompleted;
        public event EventHandler<RealTimeDataEventArgs> UpdateFailed;
        public event EventHandler<EventArgs> Initialized;
        bool isInitialized = false;
        public string NetworkState {
            
            get { return networkMonitor.IsInternetAvailable ? "Connected" : "Offline"; }
        }
        public bool IsOffline { get { return NetworkState == "Offline"; } }
        public string TransactionsSortType {
            set {
                if (value == "Asceding")
                    comparer = new PriceAscedingComparer();
                else
                    comparer = new PriceDescedingComparer();
                transactions.Sort(comparer);
            }
        }
        public int CurrentPriceIndex { get { return currentPriceIndex; } set { currentPriceIndex = value; } }
        public void ChangeUserState() {
            executor.ChangeUserID();
        }
        public void BeginGetDates() {
            Func<AsyncCallback, object, IAsyncResult> method = new Func<AsyncCallback, object, IAsyncResult>(Service.BeginGetDates);
            object[] args = new object[] { new AsyncCallback(executor.EndExecute), GetUserState(new AsyncCallback(EndGetDates)) };
            ExecutorAction action = new ExecutorAction(method, !IsOffline ? ActionPriority.Blocked : ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public void BeginGetCompanies() {
            Func<AsyncCallback, object, IAsyncResult> method = new Func<AsyncCallback, object, IAsyncResult>(Service.BeginGetCompaniesName);
            object[] args = new object[] { new AsyncCallback(executor.EndExecute), GetUserState(new AsyncCallback(EndGetCompanies)) };
            ExecutorAction action = new ExecutorAction(method, ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public void BeginGetStockDataByDate(DateTime date) {
            Func<DateTime, AsyncCallback, object, IAsyncResult> method = new Func<DateTime, AsyncCallback, object, IAsyncResult>(Service.BeginGetStockDataByDate);
            object[] args = new object[] { date, new AsyncCallback(executor.EndExecute), GetUserState(new AsyncCallback(EndGetStockDataByDate)) };
            ExecutorAction action = new ExecutorAction(method, ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public void BeginGetCompanyMultipleDataFromPeriod(int currentDate, int count, int periodSize, string companyName) {
            if (Service == null || executor == null) return;
            Func<int, int, int, string, AsyncCallback, object, IAsyncResult> method =
                new Func<int, int, int, string, AsyncCallback, object, IAsyncResult>(Service.BeginGetCompanyMultipleDataFromPeriod);
            object[] args =
                new object[] { currentDate, count, periodSize, companyName, new AsyncCallback(executor.EndExecute), GetUserState(new AsyncCallback(EndGetCompanyMultipleDataFromPeriod)) };
            ExecutorAction action = new ExecutorAction(method, ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public void BeginGetStockDataFromPeriodByCompanyList(int currentDate, int count, int periodSize, string[] companiesList) {
            Func<int, int, int, string[], AsyncCallback, object, IAsyncResult> method =
                new Func<int, int, int, string[], AsyncCallback, object, IAsyncResult>(Service.BeginGetStockDataFromPeriodByCompanyList);
            UserStateParams p = GetUserState(new AsyncCallback(EndGetStockDataFromPeriodByCompanyList));
            object[] args =
                new object[] { currentDate, count, periodSize, companiesList, new AsyncCallback(executor.EndExecute), p };
            ExecutorAction action = new ExecutorAction(method, ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public void BeginGetAllPeriodData(string company) {
            Func<string, AsyncCallback, object, IAsyncResult> method =
                new Func<string, AsyncCallback, object, IAsyncResult>(Service.BeginGetAllPeriodData);
            UserStateParams p = GetUserState(new AsyncCallback(EndGetAllPeriodData));
            object[] args = new object[] { company, new AsyncCallback(executor.EndExecute), p };
            ExecutorAction action = new ExecutorAction(method, ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public void EndGetAllPeriodData(IAsyncResult result) {
            StockData[] data = Service.EndGetAllPeriodData(result);
            RaiseGetDataCompleted(data, "GetAllPeriodData");
        }
        public void BeginGetCompaniesVolumeFromPeriod(DateTime start, DateTime end) {
            Func<DateTime, DateTime, AsyncCallback, object, IAsyncResult> method =
                new Func<DateTime, DateTime, AsyncCallback, object, IAsyncResult>(Service.BeginGetCompaniesVolumeFromPeriod);
            object[] args = new object[] { start, end, new AsyncCallback(executor.EndExecute), GetUserState(new AsyncCallback(EndGetCompaniesVolumeFromPeriod)) };
            ExecutorAction action = new ExecutorAction(method, ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public void BeginGetCompanyStockData(DateTime date, string companyName) {
            Func<DateTime, string, AsyncCallback, object, IAsyncResult> method =
                 new Func<DateTime, string, AsyncCallback, object, IAsyncResult>(Service.BeginGetCompanyStockData);
            object[] args = new object[] { date, companyName, new AsyncCallback(executor.EndExecute), GetUserState(new AsyncCallback(EndGetCompanyStockData)) };
            ExecutorAction action = new ExecutorAction(method, ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public void BeginGetStockDataFromDateByCompanyList(DateTime date, string[] companiesList) {
            Func<DateTime, string[], AsyncCallback, object, IAsyncResult> method =
                 new Func<DateTime, string[], AsyncCallback, object, IAsyncResult>(Service.BeginGetStockDataFromDateByCompanyList);
            UserStateParams p = GetUserState(new AsyncCallback(EndGetStockDataFromDateByCompanyList));
            object[] args = new object[] { date, companiesList, new AsyncCallback(executor.EndExecute), p };
            ExecutorAction action = new ExecutorAction(method, ActionPriority.Normal, args);
            executor.AddAction(action);
        }
        public List<TransactionData> GetTransactions(double currentPrice) {
            int maxTransactionCount = 3;
            int numOfTransactions = 1 + rand.Next(maxTransactionCount);
            for (int i = 0; i < numOfTransactions; i++) {
                AddNewTransaction(currentPrice, "Bid");
                AddNewTransaction(currentPrice, "Ask");
            }
            currentPriceIndex = FindNewCurrentPriceIndex();
            CorrectTransactionData();
            CorrectOnOneTransactionTypeExists();
            return transactions;
        }
        public void ClearTransactions() {
            transactions.Clear();
        }

        private void EndGetCompanyStockData(IAsyncResult result) {
            StockData[] data = Service.EndGetCompanyStockData(result);
            RaiseGetDataCompleted(data, "GetCompanyStockData");
        }
        private void EndGetStockDataFromDateByCompanyList(IAsyncResult result) {
            CompanyStockData[] data = Service.EndGetStockDataFromDateByCompanyList(result);
            RaiseGetDataCompleted(data, "GetStockDataFromDateByCompanyList");
        }
        private void EndGetCompaniesVolumeFromPeriod(IAsyncResult result) {
            CompaniesVolumeData[] data = Service.EndGetCompaniesVolumeFromPeriod(result);
            RaiseGetDataCompleted(data, "GetCompaniesVolumeFromPeriod");
        }
        private void EndGetCompanyMultipleDataFromPeriod(IAsyncResult result) {
            CompanyData[] data = Service.EndGetCompanyMultipleDataFromPeriod(result);
            RaiseGetDataCompleted(data, "GetCompanyMultipleDataFromPeriod");
        }
        private void EndGetStockDataFromPeriodByCompanyList(IAsyncResult result) {
            CompanyStockData[] data = Service.EndGetStockDataFromPeriodByCompanyList(result);
            RaiseGetDataCompleted(data, "GetStockDataFromPeriodByCompanyList");
        }
        private void EndGetStockDataByDate(IAsyncResult result) {
            StockData[] data = Service.EndGetStockDataByDate(result);
            RaiseGetDataCompleted(data, "GetStockDataByDate");
        }
        private void EndGetDates(IAsyncResult result) {
            DateTime[] dates = Service.EndGetDates(result);
            RaiseGetDataCompleted(dates, "GetDates");
        }
        private void EndGetCompanies(IAsyncResult result) {
            string[] companies = Service.EndGetCompaniesName(result);
            RaiseGetDataCompleted(companies, "GetCompanies");
        }
        private UserStateParams GetUserState(Delegate callback) {
            return new UserStateParams(callback, executor.UserID);
        }
        private void RaiseGetDataCompleted(object data, string key) {
            bool canNewUpdate = executor.ExecutingActions == 0 ? true : false;
            this.GetDataCompleted(this, new RealTimeDataEventArgs(data, key, canNewUpdate));
        }
        private void OnNetworkIsAvailableChanged(object sender, EventArgs e) {
            
            
            
            
            
            
            
            
        }
        private void RaiseInitialized() {
            if (this.Initialized != null)
                this.Initialized(this, EventArgs.Empty);
        }
        private void RaiseUpdateFailed(string message) {
            Service = CreateOfflineService();
            this.UpdateFailed(this, new RealTimeDataEventArgs(message));
        }

        private IStockDataService CreateOfflineService() {
            return offlineService ?? (offlineService = new Offline.StockDataOfflineService());
        }
        private void OnExecuteFailed(object sender, EventArgs e) {
            RaiseUpdateFailed("The remote server is not responding. Check your internet connection.");
        }
        private void InitServer() {
            try {
                isInitialized = true;
                UpdateService();
                RaiseInitialized();
            }
            catch {
                RaiseUpdateFailed("The remote server is not responding. Check your internet connection.");
            }
        }

        private void UpdateService() {
            Service = networkMonitor.IsInternetAvailable ? CreateOnlineService() : CreateOfflineService();
        }

        private IStockDataService CreateOnlineService() {
            if (onlineService == null) {

                onlineService = GetStockDataService();
            }
            return onlineService;
        }

        void InnerChannel_Closed(object sender, EventArgs e) {
            ChangeUserState();
        }

        void InnerChannel_Faulted(object sender, EventArgs e) {
            ChangeUserState();
        }
        StockDataServiceClient GetStockDataService() {
#if CLICKONCE
            System.ServiceModel.BasicHttpBinding binding2 = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.None);
            binding2.CloseTimeout = new TimeSpan(0, 0, 30);
            binding2.OpenTimeout = new TimeSpan(0, 0, 30);
            binding2.ReceiveTimeout = new TimeSpan(0, 0, 30);
            binding2.SendTimeout = new TimeSpan(0, 0, 30);
            binding2.AllowCookies = false;
            binding2.BypassProxyOnLocal = false;
            binding2.HostNameComparisonMode = System.ServiceModel.HostNameComparisonMode.StrongWildcard;
            binding2.MaxBufferSize = 5000000;
            binding2.MaxBufferPoolSize = 524288;
            binding2.MaxReceivedMessageSize = 5000000;
            binding2.MessageEncoding = System.ServiceModel.WSMessageEncoding.Text;
            binding2.TextEncoding = System.Text.Encoding.UTF8;
            binding2.TransferMode = System.ServiceModel.TransferMode.Buffered;
            binding2.UseDefaultWebProxy = true;
            binding2.ReaderQuotas.MaxDepth = 32;
            binding2.ReaderQuotas.MaxStringContentLength = 8192;
            binding2.ReaderQuotas.MaxArrayLength = 16384;
            binding2.ReaderQuotas.MaxBytesPerRead = 4096;
            binding2.ReaderQuotas.MaxNameTableCharCount = 16384;
            binding2.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.None;
            binding2.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;
            binding2.Security.Transport.Realm = "";
            binding2.Security.Message.ClientCredentialType = System.ServiceModel.BasicHttpMessageCredentialType.UserName;
            binding2.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
            System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress("http://demos.devexpress.com/Services/StockDataService/Services/StockDataService.svc");
            return new StockDataServiceClient(binding2, endpointAddress);
#else
            return new StockDataServiceClient();
#endif
        }

        void OnServerInitializeCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
            executor.ForceExecute();
        }
        private void CorrectTransactionData() {
            if (transactions.Count > 30) {
                int transactionsCount = 0;
                if (currentPriceIndex > 0)
                    transactionsCount = transactions.Count - currentPriceIndex;
                bool isDominating = transactionsCount > (transactions.Count - transactionsCount) ? true : false;
                for (int i = 0; i < 4; i++) {
                    currentPriceIndex--;
                    if (isDominating)
                        transactions.RemoveAt(0);
                    else
                        transactions.RemoveAt(transactions.Count - 1);
                }
            }
        }
        private void CorrectOnOneTransactionTypeExists() {
            if (currentPriceIndex == -1 && transactions.Count != 0) {
                int lastIndex = transactions.Count - 1;
                string transantionType = transactions[0].Bid == 0 ? "Bid" : "Ask";
                int volume = transactions[lastIndex].Volume + 5;
                double price;
                if (comparer.GetType() == typeof(PriceAscedingComparer))
                    price = IsDownMoving(transantionType) ? transactions[lastIndex].Price + 0.5 : transactions[0].Price - 0.5;
                else
                    price = IsDownMoving(transantionType) ? transactions[lastIndex].Price - 0.5 : transactions[0].Price + 0.5;
                TransactionData tdvm = new TransactionData(transantionType, volume, price);
                transactions.Add(tdvm);
                currentPriceIndex = transactions.IndexOf(tdvm);
            }
        }
        private double GetNewTransactionPrice(double currentPrice) {
            double factor = currentPrice * 0.1 * rand.NextDouble();
            factor = rand.NextDouble() > 0.5 ? factor : -factor;
            double price = currentPrice + factor;
            return Math.Round(price, 2);
        }
        private void CheckTransactions(int currentIndex, double price, string trantctionType) {
            bool isDownMoving = IsDownMoving(trantctionType);
            int index = isDownMoving ? 1 : -1;
            int i = currentIndex + index;
            while ((i < transactions.Count && i > -1) && transactions[i].TransactionType != transactions[currentIndex].TransactionType) {
                int delta = transactions[currentIndex].Volume - transactions[i].Volume;
                if (delta == 0) {
                    if (isDownMoving) {
                        transactions.Remove(transactions[i]);
                        transactions.Remove(transactions[currentIndex]);
                    }
                    else {
                        transactions.Remove(transactions[currentIndex]);
                        transactions.Remove(transactions[i]);
                    }
                    break;
                }
                if (delta < 0) {
                    transactions[i].Volume = -delta;
                    transactions.Remove(transactions[currentIndex]);
                    break;
                }
                else {
                    transactions[currentIndex].Volume = delta;
                    transactions.Remove(transactions[i]);
                    if (!isDownMoving) { currentIndex--; i--; }
                }
            }
        }
        private bool IsDownMoving(string trantctionType) {
            Type type = comparer.GetType();
            if ((type == typeof(PriceAscedingComparer) && trantctionType == "Bid") || (type == typeof(PriceDescedingComparer) && trantctionType == "Ask"))
                return true;
            else
                return false;
        }
        private void AddNewTransaction(double currentPrice, string transactionType) {
            int maxVolumeValue = 100;
            double price = GetNewTransactionPrice(currentPrice);
            int volume = 1 + rand.Next(maxVolumeValue);
            TransactionData currentTransaction = new TransactionData(transactionType, volume, price);
            transactions.Add(currentTransaction);
            transactions.Sort(comparer);
            CheckTransactions(transactions.IndexOf(currentTransaction), price, transactionType);
        }
        private int FindNewCurrentPriceIndex() {
            for (int i = 0; i < transactions.Count; i++) {
                if (i < transactions.Count - 1 && transactions[i].TransactionType != transactions[i + 1].TransactionType)
                    return i;
            }
            return -1;
        }
    }
}
