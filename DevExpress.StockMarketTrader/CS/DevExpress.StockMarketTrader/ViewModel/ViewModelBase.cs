using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System;
namespace DevExpress.StockMarketTrader.ViewModel {
    public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    
    
    
    
    
    

    
    
    
    
    
    
    
    
    
    
    

    
    
}
