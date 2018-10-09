using DevExpress.Mvvm;

namespace MVVMDemo.BindableBaseDemo {
    public class BindablePropertiesViewModel : BindableBase {
        public string FirstName {
            get { return GetProperty(() => FirstName); }
            set { SetProperty(() => FirstName, value, changedCallback: NotifyFullNameChanged); }
        }

        public string LastName {
            get { return GetProperty(() => LastName); }
            set { SetProperty(() => LastName, value, changedCallback: NotifyFullNameChanged); }
        }

        public string FullName { get { return FirstName + " " + LastName; } }

        void NotifyFullNameChanged() {
            RaisePropertyChanged(() => FullName);
        }

    }
}
