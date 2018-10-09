using System.Windows;
using System;
using DevExpress.StockMarketTrader.ViewModel;
using DevExpress.StockMarketTrader.Model.BusinessObjects;
using DevExpress.Xpf.Core;
namespace DevExpress.StockMarketTrader.ViewModel {
    public class TradingDataViewModel : ViewModelBase {

        public TradingDataViewModel() { }
        public TradingDataViewModel(TradingData data) {
            this.TradingData = new TradingData();
            Assign(data);
        }

        public TradingDataViewModel(DateTime dateTime) {
            this.TradingData = new TradingData();
            this.Date = dateTime;
        }
        TradingData data;
        public TradingData TradingData {
            get { return data; }
            private set { data = value; }
        }
        public void Assign(TradingData data) {
            this.TradingData.Assign(data);
            OnPropertyChanged("Price");
            OnPropertyChanged("Volume");
        }
        public DateTime Date {
            get { return TradingData.Date; }
            set { TradingData.Date = value; OnPropertyChanged("Date"); }
        }
        public double Price {
            get { return TradingData.Price; }
            set { TradingData.Price = value; OnPropertyChanged("Price"); }
        }
        public double Open {
            get { return TradingData.Open; }
            set { TradingData.Open = value; OnPropertyChanged("Open"); }
        }
        public double Close {
            get { return TradingData.Close; }
            set { TradingData.Close = value; OnPropertyChanged("Close"); }
        }
        public double High {
            get { return TradingData.High; }
            set { TradingData.High = value; OnPropertyChanged("High"); }
        }
        public double Low {
            get { return TradingData.Low; }
            set { TradingData.Low = value; OnPropertyChanged("Low"); }
        }
        public double Volume {
            get { return TradingData.Volume; }
            set { TradingData.Volume = value; OnPropertyChanged("Volume"); }
        }
    }
    public class CompanyTradingDataViewModel : TradingDataViewModel {
        int rise;
        double totalRise;

        public CompanyTradingDataViewModel(TradingData data, string companyName, int rise, double totalRise)
            : base(data) {
            CompanyName = companyName;
            TotalRise = totalRise;
            Rise = rise;
        }

        public void Assign(CompanyTradingData ctd) {
            CompanyName = ctd.CompanyName;
            base.Assign(ctd);
        }

        public string CompanyName { get; set; }
        public double TotalRise { get { return totalRise; } set { totalRise = value; } }
        public int Rise { get { return rise; } set { rise = value; OnPropertyChanged("Rise"); } }
        LockableCollection<TradingDataViewModel> volumeDynamics;
        public LockableCollection<TradingDataViewModel> VolumeDynamics { get { return volumeDynamics; } set { volumeDynamics = value; OnPropertyChanged("VolumeDynamics"); } }
    }
    
    
    
    

    
    
    
    
    

    
    
    
    
    
    
    public class VolumeDataViewModel : ViewModelBase {

        public VolumeDataViewModel(string companyName, int volume) {
            CompanyName = companyName;
            Volume = volume;
        }

        public string CompanyName { get; set; }
        public int Volume { get; set; }

        public void Assign(VolumeDataViewModel data) {
            CompanyName = data.CompanyName;
            Volume = data.Volume;
            OnPropertyChanged("Volume");
        }
    }
}
