using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace MVVMDemo.Commands {
    public class DelegateCommandsViewModel : ViewModelBase {
        public ICommand SaveCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }

        #region !
        public DelegateCommandsViewModel() {
            CanExecuteSaveCommand = true;

            SaveCommand = new DelegateCommand(
                () => MessageBox.Show("Action: Save"),
                () => CanExecuteSaveCommand);

            OpenCommand = new DelegateCommand<string>(
                fileName => MessageBox.Show(string.Format("Action: Open {0}", FileName)),
                fileName => !string.IsNullOrEmpty(fileName));
        } 
        #endregion

        public bool CanExecuteSaveCommand {
            get { return GetProperty(() => CanExecuteSaveCommand); }
            set { SetProperty(() => CanExecuteSaveCommand, value); }
        }
        public string FileName {
            get { return GetProperty(() => FileName); }
            set { SetProperty(() => FileName, value); }
        }
    }
}
