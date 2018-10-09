using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXCommandDemo {
    public class MultipleCallsViewModel : BindableBase {
        public bool IsReadonly {
            get { return GetProperty(() => IsReadonly); }
            set { SetProperty(() => IsReadonly, value); }
        }

        public string FileName {
            get { return GetProperty(() => FileName); }
            set { SetProperty(() => FileName, value); }
        }

        public void Open() {
            MessageBox.Show(string.Format("Action: Open {0}", FileName));
        }
        public bool CanOpen() {
            return !string.IsNullOrEmpty(FileName);
        }

        public void Clear() {
            FileName = null;
        }
    }
}
