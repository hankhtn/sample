using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using DevExpress.StockMarketTrader.Model.BusinessObjects;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Documents;
using DevExpress.Data;
using DevExpress.StockMarketTrader.StockDataServiceReference;
using System.Threading;
using DevExpress.Xpf.Core;
using System.Windows.Media;
using DevExpress.Xpf.Editors;
using System.Windows.Interop;
using System.Diagnostics;
using DevExpress.StockMarketTrader.Model;
using System.Windows.Data;

namespace DevExpress.StockMarketTrader.ViewModel {

    public class RealTimeDataViewModel : ViewModelBase {
        RealTimeDataModel model;
        Dictionary<string, Queue<Delegate>> defferedUpdate = new Dictionary<string, Queue<Delegate>>();
        DispatcherTimer timer;
        List<DateTime> dates;
        List<string> companies;
        LockableCollection<TransactionData> transactionGridBindingData;
        LockableCollection<TradingDataViewModel> stockChartBindingData;
        LockableCollection<CompanyTradingDataViewModel> watchLisBindingData;
        LockableCollection<CompaniesVolumeData> volumeChartBindingData;
        string selectedCompany = "IBM", chartScale = "210";
        int candlesCount = 73, currentDate = 0, timeInterval = 1, offlineTimeInterval = 500, tickCount = 0, totalTicks = 5;
        bool isAllBindingDataUpdate = false, canEndUpdate = true, canUpdate = true;
        double highestPrice = 0, lowestPrice = 0;

        public RealTimeDataViewModel() {
            Initialize();
            CreateTimers(timeInterval);
            IsLoading = true;
            
        }

        #region Static
        public static readonly DependencyProperty TransitionEffectProperty;
        public static readonly DependencyProperty TicksProperty;
        public static readonly DependencyProperty TransactionsSortOrderProperty;
        public static readonly DependencyProperty SelectedCompanyProperty;
        public static readonly DependencyProperty CandlesCountProperty;
        public static readonly DependencyProperty CurrentPriceIndexProperty;

        static RealTimeDataViewModel() {
            TicksProperty =
                DependencyProperty.Register("Ticks", typeof(int), typeof(RealTimeDataViewModel), new PropertyMetadata(5));
            TransactionsSortOrderProperty =
                DependencyProperty.Register("TransactionsSortOrder", typeof(ColumnSortOrder), typeof(RealTimeDataViewModel), new PropertyMetadata(ColumnSortOrder.Ascending, OnTransactionsSortOrderPropertyChanged));
            SelectedCompanyProperty =
                DependencyProperty.Register("SelectedCompany", typeof(CompanyTradingDataViewModel), typeof(RealTimeDataViewModel), new PropertyMetadata(OnSelectedCompanyPropertyChanged));
            CandlesCountProperty =
                DependencyProperty.Register("CandlesCount", typeof(int), typeof(RealTimeDataViewModel), new PropertyMetadata(72));
            TransitionEffectProperty =
                DependencyProperty.Register("TransitionEffect", typeof(int), typeof(RealTimeDataViewModel), new PropertyMetadata(2));
            CurrentPriceIndexProperty =
            DependencyProperty.Register("CurrentPriceIndex", typeof(int), typeof(RealTimeDataViewModel), new FrameworkPropertyMetadata());
        }

        static void OnTransactionsSortOrderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            RealTimeDataViewModel rtdp = d as RealTimeDataViewModel;
            rtdp.OnTransactionsSortOrderChanged((ColumnSortOrder)e.NewValue);
        }
        static void OnSelectedCompanyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (e.NewValue != null) {
                RealTimeDataViewModel rtdvm = d as RealTimeDataViewModel;
                CompanyTradingDataViewModel ctdvm = e.NewValue as CompanyTradingDataViewModel;
                if (rtdvm.selectedCompany != ctdvm.CompanyName)
                    rtdvm.SetSelectedCompany(ctdvm.CompanyName);
            }
        }
        #endregion

        public int CurrentPriceIndex {
            get { return (int)GetValue(CurrentPriceIndexProperty); }
            set { SetValue(CurrentPriceIndexProperty, value); }
        }
        public ColumnSortOrder TransactionsSortOrder {
            get { return (ColumnSortOrder)GetValue(TransactionsSortOrderProperty); }
            set { SetValue(TransactionsSortOrderProperty, value); }
        }
        public CompanyTradingDataViewModel SelectedCompany {
            get { return (CompanyTradingDataViewModel)GetValue(SelectedCompanyProperty); }
            set { SetValue(SelectedCompanyProperty, value); OnPropertyChanged("SelectedCompany"); }
        }
        public int TransitionEffect {
            get { return (int)GetValue(TransitionEffectProperty); }
            set { SetValue(TransitionEffectProperty, value); }
        }
        public int Ticks {
            get { return (int)GetValue(TicksProperty); }
            set { SetValue(TicksProperty, value); }
        }
        public int CandlesCount {
            get { return (int)GetValue(CandlesCountProperty); }
            set { SetValue(CandlesCountProperty, value); }
        }
        LiveTileViewModel sparklineTileViewModel = new LiveTileViewModel();
        public LiveTileViewModel SparklineTileViewModel { get { return sparklineTileViewModel; } set { sparklineTileViewModel = value; OnPropertyChanged("SparklineTileViewModel"); } }
        List<LiveTileViewModel> topThreeCompanies = new List<LiveTileViewModel>() {
            new LiveTileViewModel(),new LiveTileViewModel(),new LiveTileViewModel()
        };
        public List<LiveTileViewModel> TopThreeCompanies { get { return topThreeCompanies; } set { topThreeCompanies = value; OnPropertyChanged("TopThreeCompanies"); } }
        public int TickCount { get { return tickCount; } set { tickCount = value; } }
        public LockableCollection<CompanyTradingDataViewModel> WatchListBindingData { get { return watchLisBindingData; } }
        public LockableCollection<TradingDataViewModel> StockChartBindingData { get { return stockChartBindingData; } }
        public LockableCollection<TransactionData> TransactionGridBindingData { get { return transactionGridBindingData; } }
        public LockableCollection<CompaniesVolumeData> VolumeChartBindingData { get { return volumeChartBindingData; } }
        public string NetworkState { get { return model.NetworkState; } }
        public string ChartScale { get { return chartScale; } set { chartScale = value; OnPropertyChanged("ChartScale"); } }
        public bool IsSuspendUpdating { get; set; }
        public bool IsLoading { get; set; }
        bool IsInitialized { get; set; }

        public void SetSelectedCompany(string newSelectedCompany) {
            if (this.selectedCompany != newSelectedCompany) {
                List<CompanyTradingDataViewModel> result = watchLisBindingData.Where(e => e.CompanyName == newSelectedCompany).Select(e => e).ToList();
                if (result.Count > 0) {
                    this.selectedCompany = newSelectedCompany;
                    SelectedCompany = result[0];
                    SetSelectedCompany();
                }
            }
        }
        public void OnCandlesCountChanged() {
            OnCandlesCountChangedCallBack();
        }

        private void Initialize() {
            model = new RealTimeDataModel();
            model.GetDataCompleted += new EventHandler<RealTimeDataEventArgs>(OnGetDataCompleted);
            model.UpdateFailed += new EventHandler<RealTimeDataEventArgs>(OnUpdateFailed);
            model.Initialized += new EventHandler<EventArgs>(OnInitialized);
            stockChartBindingData = new LockableCollection<TradingDataViewModel>();
            watchLisBindingData = new LockableCollection<CompanyTradingDataViewModel>();
            transactionGridBindingData = new LockableCollection<TransactionData>();
            volumeChartBindingData = new LockableCollection<CompaniesVolumeData>();
        }

        private void OnInitialized(object sender, EventArgs e) {
            GetDatesAsync();
        }
        private void OnUpdateFailed(object sender, RealTimeDataEventArgs e) {
            CreateTimers(GetTimeInterval());
            if (dates != null)
                timer.Start();
            OnPropertyChanged("NetworkState");
            
        }
        private void SuspendTimer(bool isSuspend) {
            if (!isAllBindingDataUpdate) {
                if (isSuspend) {
                    timer.Stop();
                } else {
                    StartTimer();
                }
            }
        }
        private void StartTimer() {
            timer.Start();
        }
        private void ShowExceptionMessage(string message) {
            App.Current.Dispatcher.Invoke(new Action(() => {
                DXMessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }));
        }
        private void OnTransactionsSortOrderChanged(ColumnSortOrder columnSortOrder) {
            if (columnSortOrder == ColumnSortOrder.Ascending)
                model.TransactionsSortType = "Ascending";
            else
                model.TransactionsSortType = "Descending";
        }
        private void OnCandlesCountChangedCallBack() {
            CorrectCurrentDate();
            totalTicks = Ticks;
            tickCount = 0;
            candlesCount = CandlesCount + 1;
            timer.Stop();
            UpdateAllBindingData();
        }
        private void CorrectCurrentDate() {
            int delta = currentDate;
            delta = currentDate - (CandlesCount + 1) * Ticks;
            if (delta > -1)
                currentDate = delta;
            else {
                delta = currentDate - candlesCount * totalTicks;
                currentDate = delta < 0 ? 0 : delta;
            }
        }
        private void CreateTimers(int time) {
            if (timer != null) {
                timer.Stop();
                timer.Tick -= new EventHandler(UpdateOnTimer);
                timer = null;
            }
            timer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            timer.Interval = new TimeSpan(0, 0, 0, 0, time);
            timer.Tick += new EventHandler(UpdateOnTimer);
        }

        private int GetTimeInterval() {
            return !model.IsOffline ? timeInterval : offlineTimeInterval;
        }
        private void GetDatesAsync() {
            Action<DateTime[]> action = new Action<DateTime[]>(SetDates);
            AddDefferedDelegate("GetDates", action);
            model.BeginGetDates();
        }
        private void SetDates(DateTime[] dates) {
            try {
                if (dates.Length == 0)
                    throw new Exception();
                this.dates = new List<DateTime>(dates);
                GetCompaniesAsync();
                canEndUpdate = true;
            } catch {
                canUpdate = false;
                ShowExceptionMessage("The remote server is not responding.");
            }
        }
        private void InitializeChartBindingData() {
            isAllBindingDataUpdate = true;
            stockChartBindingData.BeginUpdate();
            stockChartBindingData.Clear();
            stockChartBindingData.EndUpdate();
        }
        private void GetCompaniesAsync() {
            Action<string[]> action = new Action<string[]>(SetCompanies);
            AddDefferedDelegate("GetCompanies", action);
            model.BeginGetCompanies();
        }
        private void SetCompanies(string[] companies) {
            this.companies = companies != null ? new List<string>(companies) : new List<string>();
            if(companies.Length > 0)
                selectedCompany = companies[0];
            InitializeChartBindingData();
            InitializeBidingData();
        }
        private void GetVolumeDynamicsDataAsyncCompleted2(StockData[] data) { }
        private void InitializeBidingData() {
            GetBindingDataAsync();
            
        }
        List<string> initializationList;
        private void GetBindingDataAsync() {
            GetWatchListBindingDataAsync();
            InitializeCompaniesVolumeAsync();
            currentDate = 0;
            GetStockChartBindingDataAsync();
            GetVolumeDynamicsDataAsync();
        }
        int volumeCount = 15;

        void AddToInitizalization(string method) {
            if (initializationList == null)
                initializationList = new List<string>();
            initializationList.Add(method);
        }
        void CompleteInitiallization(string method) {
            if (initializationList.Contains(method))
                initializationList.Remove(method);
            IsInitialized = initializationList.Count == 0;
        }

        private void GetVolumeDynamicsDataAsync(int current = 0) {
            if (companies == null) return;
            AddToInitizalization("GetVolumeDynamicsData");
            Action<CompanyStockData[]> action = new Action<CompanyStockData[]>(GetVolumeDynamicsDataAsyncCompleted);
            AddDefferedDelegate("GetStockDataFromPeriodByCompanyList", action);
            int date = current + totalTicks * ((candlesCount - volumeCount) + 1);
            model.BeginGetStockDataFromPeriodByCompanyList(date, volumeCount, totalTicks, companies.ToArray());
        }
        private void GetVolumeDynamicsDataAsyncCompleted(CompanyStockData[] cd) {
            CompleteInitiallization("GetVolumeDynamicsData");
            foreach (CompanyStockData companyStockData in cd) {
                CompanyTradingDataViewModel vm = watchLisBindingData.Where(c => c.CompanyName == companyStockData.CompanyName).Select(c => c).FirstOrDefault();
                vm.VolumeDynamics = CreateVolumeDynamicsBindingData(companyStockData.Data);
            }
            TryEndUpdateBindingData();
        }

        private LockableCollection<TradingDataViewModel> CreateVolumeDynamicsBindingData(StockData[] data) {
            LockableCollection<TradingDataViewModel> result = new LockableCollection<TradingDataViewModel>();
            foreach (StockData currentData in data) {
                CompanyTradingData ctd = new CompanyTradingData(currentData, "");
                ctd.Close = double.Parse(currentData.CloseP.ToString());
                ctd.Volume = Math.Round(((double)ctd.Volume / 1000000), 2);
                ctd.Low = double.Parse(currentData.LowP.ToString());
                ctd.High = double.Parse(currentData.HighP.ToString());
                result.Add(new TradingDataViewModel(ctd));
            }
            return result;
        }
        private void GetWatchListBindingDataAsync() {
            AddToInitizalization("GetWatchListBindingData");
            currentDate += candlesCount * totalTicks;
            Action<StockData[]> action = new Action<StockData[]>(GetWatchListBindingDataAsyncCompleted);
            AddDefferedDelegate("GetStockDataByDate", action);
            model.BeginGetStockDataByDate(dates[currentDate - 1]);

        }
        void GetWatchListBindingDataAsyncCompleted(StockData[] data) {
            CompleteInitiallization("GetWatchListBindingData");
            UpdateWatchListBindingDataAsyncCompleted(data);
        }
        private void GetStockChartBindingDataAsync() {
            AddToInitizalization("GetStockChartBindingData");
            Action<CompanyData[]> action = new Action<CompanyData[]>(GetStockChartBindingDataAsyncCompleted);
            AddDefferedDelegate("GetCompanyMultipleDataFromPeriod", action);
            model.BeginGetCompanyMultipleDataFromPeriod(currentDate, candlesCount, totalTicks, selectedCompany);
            currentDate += totalTicks * (candlesCount + 1);
        }
        private void GetStockChartBindingDataAsyncCompleted(CompanyData[] data) {
            CompleteInitiallization("GetStockChartBindingData");
            List<CompanyData> newStockChartData = new List<CompanyData>();
            try {
                newStockChartData = new List<CompanyData>(data);
            } catch { }
            AddChartPoints(newStockChartData);
        }
        private void AddChartPoints(List<CompanyData> newStockChartData) {
            CreateChartBindingData(newStockChartData);
            UpdateSparklineViewModelSource();
            isAllBindingDataUpdate = false;
            TryEndUpdateBindingData();
        }

        private void UpdateSparklineViewModelSource() {
            SparklineTileViewModel.Source = stockChartBindingData;
        }
        private void UpdateWatchListBindingDataAsync() {
            if (dates == null || dates.Count <= currentDate) return;
            Action<StockData[]> action = new Action<StockData[]>(UpdateWatchListBindingDataAsyncCompleted);
            AddDefferedDelegate("GetStockDataByDate", action);
            model.BeginGetStockDataByDate(dates[currentDate]);
        }
        private void UpdateCompaniesVolumeAsync() {
            if (tickCount == totalTicks || tickCount == 0)
                GetCompaniesVolumeAsync();
        }
        private void UpdateWatchListBindingDataAsyncCompleted(StockData[] gridData) {
            UpdateWatchList(gridData);
            UpdateSparklineViewModelCompany();
            UpdateTopThreeCompany();
            TryEndUpdateBindingData();
        }

        private void UpdateTopThreeCompany() {
            var topThree = GetTopThreeVolume();
            if (topThree.Count < 3) return;
            var result = new List<LiveTileViewModel>();
            foreach (CompaniesVolumeData volumeData in topThree) {
                LiveTileViewModel vm = new LiveTileViewModel();
                vm.CompanyIndex = volumeData.CompanyName;
                double delta = 0;
                double increace = CalcIncrease(volumeData.CompanyName, ref delta);
                vm.Arrow = increace > 0 ? "▲" : "▼";
                vm.Persent = Math.Abs(increace) * 100 + "%";
                vm.PriceIncrease = Math.Abs(Math.Round(delta, 3)).ToString();
                result.Add(vm);
            }
            TopThreeCompanies = result;
        }

        private List<CompaniesVolumeData> GetTopThreeVolume() {
            List<CompaniesVolumeData> result = new List<CompaniesVolumeData>();
            if (volumeChartBindingData != null && volumeChartBindingData.Count > 0) {
                for (int i = 0; i < 3; i++) if (volumeChartBindingData.Count >= i + 1) result.Add(volumeChartBindingData[i]);
                
                
                
            }
            return result;
        }
        private void UpdateSparklineViewModelCompany() {
            SparklineTileViewModel.CompanyIndex = SelectedCompany.CompanyName;
            double delta = 0;
            double increace = CalcIncrease(SelectedCompany.CompanyName, ref delta);
            SparklineTileViewModel.Arrow = increace > 0 ? "▲" : "▼";
            SparklineTileViewModel.Persent = Math.Abs(increace) * 100 + "%";
        }

        private double CalcIncrease(string company, ref double delta) {
            var companyData = watchLisBindingData.Where(c => c.CompanyName == company).Select(c => c).FirstOrDefault();
            var oldCompanyData = oldWatchListData.Where(c => c.CompanyName == company).Select(c => c).FirstOrDefault();
            if (companyData != null && oldCompanyData != null) {
                double newPrice = companyData.Price;
                double oldPrice = oldCompanyData.Price;
                delta = newPrice - oldPrice;
                return Math.Round(delta / newPrice, 3);
            }
            return 0;
        }
        void InitializeCompaniesVolumeAsync() {
            AddToInitizalization("GetCompaniesVolume");
            GetCompaniesVolumeAsync();
        }
        private void GetCompaniesVolumeAsync() {
            int endIndex = currentDate - totalTicks;
            if (dates != null && currentDate < dates.Count && endIndex > 0) {
                DateTime start = dates[currentDate];
                DateTime end = dates[endIndex];
                Action<CompaniesVolumeData[]> action = new Action<CompaniesVolumeData[]>(GetCompaniesVolumeCompleted);
                AddDefferedDelegate("GetCompaniesVolumeFromPeriod", action);
                model.BeginGetCompaniesVolumeFromPeriod(start, end);
            }
        }
        private void GetCompaniesVolumeCompleted(CompaniesVolumeData[] data) {
            CompleteInitiallization("GetCompaniesVolume");
            UpdateVolumeChartBindingData(data);
        }
        private void UpdateVolumeChartBindingData(CompaniesVolumeData[] data) {
            volumeChartBindingData.BeginUpdate();
            volumeChartBindingData.Clear();
            SetVolumeOfSelectedCompany();
            int index = 1;
            for (int i = 0; i < data.Length - 1; i++) {
                if (data[i] == null || data[i].CompanyName == selectedCompany)
                    continue;
                if (volumeChartBindingData.Count < data.Length + 1)
                    volumeChartBindingData.Add(data[i]);
                index++;
            }
            ValidateCompaniesVolume();
            volumeChartBindingData.EndUpdate();
            TryEndUpdateBindingData();
        }

        private void ValidateCompaniesVolume() {
            if (volumeChartBindingData == null || companies == null) return;
            List<string> volumeCompanies = volumeChartBindingData.Select(c => c.CompanyName).ToList();
            List<string> exceptCompanies = companies.Except(volumeCompanies).ToList();
            for (int i = 0; i < volumeChartBindingData.Count; i++) {
                CompaniesVolumeData volumeData = volumeChartBindingData[i];
                if (!companies.Contains(volumeData.CompanyName)) {
                    var res = watchLisBindingData.Where(c => exceptCompanies.Contains(c.CompanyName)).ToList();
                    if (res.Count > 0) {
                        double maxVolume = res.Max(c => c.Volume);
                        CompanyTradingDataViewModel vm = watchLisBindingData.Where(c => c.Volume == maxVolume).Select(c => c).FirstOrDefault();
                        exceptCompanies.Remove(vm.CompanyName);
                        volumeChartBindingData[i] = new CompaniesVolumeData() {
                            CompanyName = vm.CompanyName,
                            Volume = (int)maxVolume
                        };
                    }
                }
            }
        }
        private void SetVolumeOfSelectedCompany() {
            if (volumeChartBindingData.Count == 0 && SelectedCompany != null) {
                CompaniesVolumeData cvd = new CompaniesVolumeData();
                cvd.CompanyName = SelectedCompany.CompanyName;
                cvd.Volume = (int)SelectedCompany.Volume;
                volumeChartBindingData.Add(cvd);
            } else if (SelectedCompany != null) {
                volumeChartBindingData[0].CompanyName = SelectedCompany.CompanyName;
                volumeChartBindingData[0].Volume = (int)SelectedCompany.Volume;
            }
        }
        private void TryEndUpdateBindingData() {
            if (canEndUpdate && !isUpdateLocked)
                EndUpdateBindingData();
        }
        private void EndUpdateBindingData() {
            if (timer.IsEnabled == false && !isAllBindingDataUpdate && IsInitialized)
                StartTimer();
        }
        private void UpdateOnTimer(object sender, EventArgs e) {
            if (!IsSuspendUpdating)
                UpdateData();
        }
        private void UpdateAllBindingData(bool needUpdateVolumeDynamics = true) {
            if (canUpdate && !isUpdateLocked && IsInitialized) {
                InitProperties();
                model.ChangeUserState();
                defferedUpdate.Clear();
                tickCount = 0;
                if (needUpdateVolumeDynamics)
                    GetVolumeDynamicsDataAsync(currentDate);
                GetStockChartBindingDataAsync();
                UpdateCompaniesVolumeAsync();
                UpdateWatchListBindingDataAsync();
                InitializeChartBindingData();
            }
        }
        private void UpdateData() {
            timer.Stop();
            if (tickCount >= totalTicks + 1) {
                tickCount = 0;
                if(stockChartBindingData != null && stockChartBindingData.Count > 0)
                    stockChartBindingData.RemoveAt(0);
                foreach (CompanyTradingDataViewModel vm in watchLisBindingData) {
                    if (vm.VolumeDynamics != null && vm.VolumeDynamics.Count > 0)
                        vm.VolumeDynamics.RemoveAt(0);
                }
            }
            UpdateBindingData();
        }
        private void UpdateBindingData() {
            UpdateStockChartBindingDataAsync();
            UpdateWatchListBindingDataAsync();
            UpdateCompaniesVolumeAsync();
            UpdateVolumeDynamicsAsync();
            UpdateTransactionBindingData();
        }

        private void UpdateVolumeDynamicsAsync() {
            if (!HasDates() || model == null || companies == null) return;
            Action<CompanyStockData[]> action = new Action<CompanyStockData[]>(UpdateVolumeDynamicsAsyncCompleted);
            AddDefferedDelegate("GetStockDataFromDateByCompanyList", action);
            model.BeginGetStockDataFromDateByCompanyList(dates[currentDate], companies.ToArray());
        }

        private bool HasDates() {
            return dates != null;
        }

        private void UpdateVolumeDynamicsAsyncCompleted(CompanyStockData[] cd) {
            foreach (CompanyStockData companynStockData in cd) {
                try {
                    if (companynStockData.Data.Length == 0) continue;
                    DateTime current = dates[currentDate];
                    CompanyTradingData ctd = GetCompanyTradingData(companynStockData.Data[0], current);
                    CompanyTradingDataViewModel vm = watchLisBindingData.Where(c => c.CompanyName == companynStockData.CompanyName).Select(c => c).FirstOrDefault();
                    if (vm == null) continue;
                    IList<TradingDataViewModel> volDyn = vm.VolumeDynamics as IList<TradingDataViewModel>;

                    if (vm.VolumeDynamics == null || ((IList)vm.VolumeDynamics).Count == 0) continue;
                    if (tickCount == 2) {
                        ctd.Volume = Math.Round((ctd.Volume / 1000000), 2);
                        volDyn.Add(new TradingDataViewModel(ctd));
                    }
                    if (tickCount != 2) {
                        TradingDataViewModel tdvm = new TradingDataViewModel(volDyn[volDyn.Count - 1].TradingData);
                        tdvm.Volume = Math.Round((ctd.Volume / 1000000), 2);
                        tdvm.Close = ctd.Close;
                        tdvm.Low = lowestPrice;
                        tdvm.High = highestPrice;
                        tdvm.Price = ctd.Price;
                        volDyn[volDyn.Count - 1] = tdvm;
                    }
                } catch {
                    currentDate++;
                    UpdateStockChartBindingDataAsync();
                }
            }
            TryEndUpdateBindingData();
        }

        private void UpdateStockChartBindingDataAsync() {
            if (!HasDates() || currentDate < 0 || currentDate > dates.Count) return;
            Action<StockData[]> action = new Action<StockData[]>(UpdateStockChartBindingDataAsyncCompleted);
            AddDefferedDelegate("GetCompanyStockData", action);
            model.BeginGetCompanyStockData(dates[currentDate], selectedCompany);
        }
        private void UpdateStockChartBindingDataAsyncCompleted(StockData[] data) {
            if (data.Length == 0) {
                IncrementDateAndUpdateStockChartBindingData();
            } else {
                try {
                    UpdateStockChart(data[0]);
                    UpdateSparklineViewModelSource();
                    TryEndUpdateBindingData();
                } catch {
                    IncrementDateAndUpdateStockChartBindingData();
                }
            }
        }

        private void IncrementDateAndUpdateStockChartBindingData() {
            currentDate++;
            UpdateStockChartBindingDataAsync();
        }
        private void UpdateStockChart(StockData newChartData) {
            tickCount++;
            DateTime current = dates[currentDate];
            CompanyTradingData ctd = GetCompanyTradingData(newChartData, current);
            if (tickCount == 1) {
                ctd.Volume = Math.Round((ctd.Volume / 1000000), 2);
                stockChartBindingData.Add(new TradingDataViewModel(ctd));
                highestPrice = ctd.High;
                lowestPrice = ctd.Low;
            }
            if (tickCount != 1) {
                SetStockChartBindingData(ctd);
            }
            currentDate++;
        }
        private void TryResetCurrentDate() {
            if (dates != null && currentDate + totalTicks > dates.Count - 1) {
                currentDate = 0;
                tickCount = 0;
            }
        }
        private void CreateChartBindingData(List<CompanyData> newStockChartData) {
            TryResetCurrentDate();
            stockChartBindingData.BeginUpdate();
            int index = 0;
            foreach (CompanyData candleData in newStockChartData) {
                CompanyTradingData ctd = new CompanyTradingData(candleData.Data, "");
                ctd.Close = candleData.ClosePrice;
                ctd.Volume = Math.Round(((double)ctd.Volume / 1000000), 2);
                ctd.Low = lowestPrice = candleData.LowPrice;
                ctd.High = highestPrice = candleData.HighPrice;
                if (stockChartBindingData.Count < candlesCount) {
                    stockChartBindingData.Add(new TradingDataViewModel(ctd));
                }
                
                else if(index < stockChartBindingData.Count)
                    stockChartBindingData[index] = new TradingDataViewModel(ctd);
                index++;
            }
            stockChartBindingData.EndUpdate();
            
        }
        ObservableCollection<CompanyTradingDataViewModel> oldWatchListData;
        private void UpdateWatchList(StockData[] watchListData) {
            int count = 0;
            int isRise = 0;
            if (watchListData == null || watchLisBindingData == null) return;
            oldWatchListData = new ObservableCollection<CompanyTradingDataViewModel>();
            foreach (CompanyTradingDataViewModel item in watchLisBindingData) {
                if (item == null)
                    continue;
                oldWatchListData.Add(new CompanyTradingDataViewModel(item.TradingData, item.CompanyName, item.Rise, item.TotalRise));
            }
            watchLisBindingData.BeginUpdate();
            foreach (StockData dt in watchListData) {
                if (dt == null)
                    continue;
                if (companies.Count <= (dt.CompanyID)) continue;
                CompanyTradingData ctd = new CompanyTradingData(dt, companies[dt.CompanyID]);
                if (watchLisBindingData.Count < companies.Count)
                    watchLisBindingData.Add(new CompanyTradingDataViewModel(ctd, ctd.CompanyName, isRise, 1));
                else {
                    SetWatchListBindingData(count, ctd);
                    count++;
                }
            }
            watchLisBindingData.EndUpdate();
        }
        private void SetWatchListBindingData(int index, CompanyTradingData ctd) {
            if (ctd.Price > watchLisBindingData[index].Price) {
                watchLisBindingData[index].TotalRise = Math.Min(0.5 + (1 - watchLisBindingData[index].Price / ctd.Price) * 10, 1.3);
                watchLisBindingData[index].Rise = 180;
            } else if (ctd.Price < watchLisBindingData[index].Price) {
                watchLisBindingData[index].TotalRise = Math.Min(0.5 + (1 - ctd.Price / watchLisBindingData[index].Price) * 10, 1.3);
                watchLisBindingData[index].Rise = 0;
            }
            watchLisBindingData[index].Assign(ctd);
        }
        private void SetStockChartBindingData(CompanyTradingData ctd) {
            TradingDataViewModel tdvm = new TradingDataViewModel(stockChartBindingData[stockChartBindingData.Count - 1].TradingData);
            highestPrice = Math.Max(highestPrice, ctd.High);
            lowestPrice = Math.Min(lowestPrice, ctd.Low);
            tdvm.Volume = Math.Round((ctd.Volume / 1000000), 2);
            tdvm.Close = ctd.Close;
            tdvm.Low = lowestPrice;
            tdvm.High = highestPrice;
            tdvm.Price = ctd.Price;
            stockChartBindingData[stockChartBindingData.Count - 1] = tdvm;
        }
        private CompanyTradingData GetCompanyTradingData(StockData newData, DateTime current) {
            CompanyTradingData ctd = new CompanyTradingData(newData, "");
            return ctd;
        }
        private void UpdateTransactionBindingData() {
            TryResetCurrentDate();
            if (stockChartBindingData.Count > 0) {
                List<TransactionData> transactions = model.GetTransactions(stockChartBindingData[stockChartBindingData.Count - 1].Price);
                SetTransactionGridBindingData(transactions);
                IsLoading = false;
                OnPropertyChanged("IsLoading");
            }
        }
        private void SetTransactionGridBindingData(List<TransactionData> transactions) {
            transactionGridBindingData.Clear();
            transactionGridBindingData.BeginUpdate();
            foreach (TransactionData tdvm in transactions) {
                transactionGridBindingData.Add(tdvm);
            }
            transactionGridBindingData.EndUpdate();
            CurrentPriceIndex = model.CurrentPriceIndex;
        }
        private void SetSelectedCompany() {
            timer.Stop();
            int delta = currentDate - candlesCount * totalTicks;
            currentDate = delta < 0 ? 0 : delta;
            UpdateAllBindingData(false);
        }
        private void InitProperties() {
            TryResetCurrentDate();
            isAllBindingDataUpdate = true;
            IsLoading = true;
            model.ClearTransactions();
            OnPropertyChanged("IsLoading");
        }
        private void OnGetDataCompleted(object sender, RealTimeDataEventArgs e) {
            OnPropertyChanged("NetworkState");
            canEndUpdate = e.CanNewUpdate;
            Delegate method = GetDefferedDelegate(e.Key);
            if (method != null && App.Current != null)
                App.Current.Dispatcher.Invoke(method, new object[] { e.Data });
        }
        private Delegate GetDefferedDelegate(string key) {
            if (defferedUpdate.ContainsKey(key) && defferedUpdate[key].Count != 0)
                return defferedUpdate[key].Dequeue();
            return null;

        }
        private void AddDefferedDelegate(string key, Delegate method) {
            if (!IsSuspendUpdating) {
                if (!defferedUpdate.ContainsKey(key))
                    defferedUpdate.Add(key, new Queue<Delegate>());
                lock (defferedUpdate) {
                    defferedUpdate[key].Enqueue(method);
                }
            }
        }
        bool isUpdateLocked = false;
        internal void LockUpdate() {
            isUpdateLocked = true;
        }
        internal void UnLockUpdate() {
            isUpdateLocked = false;
            UpdateAllBindingData();
        }
    }
}
