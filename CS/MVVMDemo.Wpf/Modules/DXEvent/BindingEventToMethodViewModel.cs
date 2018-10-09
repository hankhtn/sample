using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXEventDemo {
    public class BindingEventToMethodViewModel : BindableBase {
        public string State {
            get { return GetProperty(() => State); }
            private set { SetProperty(() => State, value); }
        }
        public void Initialize() {
            State = "Initialized";
        }
    }
}
