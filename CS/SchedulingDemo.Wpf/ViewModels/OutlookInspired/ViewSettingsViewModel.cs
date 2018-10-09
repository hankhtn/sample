using System.Windows.Controls;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.ViewModel;

namespace SchedulingDemo.ViewModels {
    public class ViewSettingsViewModel {
        public static ViewSettingsViewModel Create() {
            return ViewModelSource.Create(() => new ViewSettingsViewModel());
        }

        protected ViewSettingsViewModel() {
            ResetView();
        }

        public virtual Orientation Orientation { get; set; }
        public virtual bool IsDataPaneVisible { get; set; }
        public void ResetView() {
            Orientation = Orientation.Horizontal;
            IsDataPaneVisible = false;
        }
              
        public void DataPaneRight() {
            Orientation = Orientation.Horizontal;
            IsDataPaneVisible = true;
        }
        public bool CanDataPaneRight() {
            return Orientation != Orientation.Horizontal || !IsDataPaneVisible;
        }
        public void DataPaneBottom() {
            Orientation = Orientation.Vertical;
            IsDataPaneVisible = true;
        }
        public bool CanDataPaneBottom() {
            return Orientation != Orientation.Vertical || !IsDataPaneVisible;
        }
        public void DataPaneOff() {
            IsDataPaneVisible = false;
        }
        public bool CanDataPaneOff() {
            return IsDataPaneVisible;
        }
    }
}
