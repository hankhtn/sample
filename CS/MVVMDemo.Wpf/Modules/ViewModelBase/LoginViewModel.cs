using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace MVVMDemo.ViewModelBaseDemo {
    public class LoginViewModel : ViewModelBase {
        public string UserName {
            get { return GetProperty(() => UserName); }
            set { SetProperty(() => UserName, value); }
        }
        public string Status {
            get { return GetProperty(() => Status); }
            private set { SetProperty(() => Status, value); }
        }

        [Command]
        public void Login() {
            Status = "User: " + UserName;
        }
        public bool CanLogin() {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}
