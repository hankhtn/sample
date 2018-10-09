using DevExpress.Mvvm;

namespace MVVMDemo.BindableBaseDemo {
    public class BindablePropertiesViaFieldsViewModel : BindableBase {
        string _FirstName;
        public string FirstName {
            get { return _FirstName; }
            set { SetProperty(ref _FirstName, value, () => FirstName, changedCallback: NotifyFullNameChanged); }
        }

        string _LastName;
        public string LastName {
            get { return _LastName; }
            set { SetProperty(ref _LastName, value, () => LastName, changedCallback: NotifyFullNameChanged); }
        }

        public string FullName { get { return FirstName + " " + LastName; } }

        void NotifyFullNameChanged() {
            RaisePropertyChanged(() => FullName);
        }

    }
}
