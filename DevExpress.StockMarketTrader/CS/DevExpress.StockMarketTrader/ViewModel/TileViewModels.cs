using System;
using System.Collections.ObjectModel;
namespace DevExpress.StockMarketTrader.ViewModel {
    public class LiveTileViewModel : ViewModelBase {

        ObservableCollection<TradingDataViewModel> source = new ObservableCollection<TradingDataViewModel>();
        public ObservableCollection<TradingDataViewModel> Source {
            get { return source; }
            set {
                source = value;
                OnPropertyChanged("Source");
            }
        }
        string arrow;
        public string Arrow {
            get { return arrow; }
            set {
                arrow = value;
                OnPropertyChanged("Arrow");
            }
        }
        string companyIndex;
        public string CompanyIndex {
            get { return companyIndex; }
            set {
                companyIndex = value;
                OnPropertyChanged("CompanyIndex");
            }
        }
        string persent;
        public string Persent {
            get { return persent; }
            set {
                persent = value;
                OnPropertyChanged("Persent");
            }
        }
        string priceIncrease;
        public string PriceIncrease {
            get { return priceIncrease; }
            set {
                priceIncrease = value;
                OnPropertyChanged("PriceIncrease");
            }
        }
    }
}
