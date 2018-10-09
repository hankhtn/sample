using DevExpress.Mvvm;

namespace MVVMDemo.ViewModelsInteraction {
    public class DetailViewModel : ViewModelBase {
        public string DetailInfo {
            get { return GetProperty(() => DetailInfo); }
            set { SetProperty(() => DetailInfo, value); }
        }

        protected override void OnParameterChanged(object parameter) {
            base.OnParameterChanged(parameter);
            DetailInfo = string.Format("Detail for {0} item", parameter);
        }
    }
}
