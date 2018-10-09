using DevExpress.Mvvm;

namespace NavigationDemo {
    public class ContactsViewModel: ISupportParameter {
        public virtual object Parameter { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }

        public virtual void AddContact() {
            var viewModel = (ContactsNavigationViewModel)Parameter;
            viewModel.Contacts.Add(new ContactItem() { FirstName = FirstName, LastName = LastName, Email = Email });
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }
        public virtual bool CanAddContact() {
            return !(string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(FirstName));
        }

        #region ISupportParameter
        object ISupportParameter.Parameter {
            get { return Parameter; }
            set { Parameter = value; }
        }
        #endregion
    }
}
