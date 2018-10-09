using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;

namespace NavigationDemo {
    public class NavigationViewModelBase { }

    public class ContactsNavigationViewModel : NavigationViewModelBase {
        public static ContactsNavigationViewModel Create() {
            return ViewModelSource.Create(() => new ContactsNavigationViewModel());
        }
        public virtual ObservableCollection<ContactItem> Contacts { get; set; }

        protected ContactsNavigationViewModel() {
            Contacts = NavigationPaneData.ContactsData;
        }
    }
}
