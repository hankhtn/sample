using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace MVVMDemo.ViewModelBaseDemo {
    public class ServicesViewModel : ViewModelBase {
        public string UserName {
            get { return GetProperty(() => UserName); }
            set { SetProperty(() => UserName, value); }
        }

        #region !
        IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } }

        [Command]
        public void Login() {
            MessageBoxService.ShowMessage("User: " + UserName, "Login", MessageButton.OK, MessageIcon.Information);
        } 
        #endregion

        public bool CanLogin() {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}
