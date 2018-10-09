using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.TypedStylesDemo {
    public class DXBindingAndDXCommandViewModel : BindableBase {
        public string FirstName {
            get { return GetProperty(() => FirstName); }
            set { SetProperty(() => FirstName, value); }
        }
        public string LastName {
            get { return GetProperty(() => LastName); }
            set { SetProperty(() => LastName, value); }
        }
        public void Register() {
            MessageBox.Show("Registered");
        }
        public DXBindingAndDXCommandViewModel() {
            FirstName = "Gregory";
            LastName = "Price";
        }
    }
}
