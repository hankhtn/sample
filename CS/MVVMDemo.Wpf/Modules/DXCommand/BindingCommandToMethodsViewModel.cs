using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXCommandDemo {
    public class BindingCommandToMethodsViewModel : BindableBase {
        public bool CanExecuteSaveCommand {
            get { return GetProperty(() => CanExecuteSaveCommand); }
            set { SetProperty(() => CanExecuteSaveCommand, value); }
        }
        public void Save() {
            MessageBox.Show("Action: Save");
        }

        public string FileName {
            get { return GetProperty(() => FileName); }
            set { SetProperty(() => FileName, value); }
        }
        public void Open(string fileName) {
            MessageBox.Show(string.Format("Action: Open {0}", fileName));
        }

        public BindingCommandToMethodsViewModel() {
            CanExecuteSaveCommand = true;
        }

    }
}
