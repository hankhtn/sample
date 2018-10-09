using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.Windows;

namespace MVVMDemo.Commands {
    public class AutoCommandsChangeNotificationsViewModel : ViewModelBase {
        [Command(UseCommandManager = false)]
        public void Save() {
            MessageBox.Show("Action: Save");
        }
        public bool CanSave() {
            return CanExecuteSaveCommand;
        }

        public bool CanExecuteSaveCommand {
            get { return GetProperty(() => CanExecuteSaveCommand); }
            set {
                #region !
                if(SetProperty(() => CanExecuteSaveCommand, value))
                    RaiseCanExecuteChanged(() => Save());
                #endregion            }
            }
        }

        public AutoCommandsChangeNotificationsViewModel() {
            CanExecuteSaveCommand = true;
        }
    }
}
