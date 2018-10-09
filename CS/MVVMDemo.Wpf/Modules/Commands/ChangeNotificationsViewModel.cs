using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace MVVMDemo.Commands {
    public class ChangeNotificationsViewModel : ViewModelBase {
        public DelegateCommand SaveCommand { get; private set; }

        public bool CanExecuteSaveCommand {
            get { return GetProperty(() => CanExecuteSaveCommand); }
            set {
                #region !
                if(SetProperty(() => CanExecuteSaveCommand, value))
                    SaveCommand.RaiseCanExecuteChanged(); 
                #endregion
            }
        }

        public ChangeNotificationsViewModel() {
            SaveCommand = new DelegateCommand(
                () => MessageBox.Show("Action: Save"), 
                () => CanExecuteSaveCommand,
                useCommandManager: false);

            CanExecuteSaveCommand = true;
        }
    }
}
