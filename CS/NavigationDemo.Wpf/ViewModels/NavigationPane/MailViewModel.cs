using System.Collections.ObjectModel;
using DevExpress.Mvvm;

namespace NavigationDemo {
    public class MailViewModel : ISupportParameter {
        public virtual ObservableCollection<MailItem> ItemsSource { get; set; }
        public virtual object Parameter { get; set; }

        public virtual int SentGroupIndex { get; set; }
        public virtual int ReceiveGroupIndex { get; set; }

        public MailViewModel() {
            SentGroupIndex = -1;
            ReceiveGroupIndex = -1;
        }
        protected virtual void OnParameterChanged() {
            var id = (NavigationId)Parameter;
            switch(id) {
                case NavigationId.Inbox: SentGroupIndex = -1; ReceiveGroupIndex = 0; break;
                case NavigationId.Outbox: SentGroupIndex = 0; ReceiveGroupIndex = -1; break;
                case NavigationId.SentItems: SentGroupIndex = 0; ReceiveGroupIndex = -1; break;
                case NavigationId.DeletedItems: SentGroupIndex = -1; ReceiveGroupIndex = 0; break;
                case NavigationId.Drafts: SentGroupIndex = 0; ReceiveGroupIndex = -1; break;
            }
            ItemsSource = NavigationPaneData.MailData[id];
        }
        #region ISupportParameter
        object ISupportParameter.Parameter {
            get { return Parameter; }
            set { Parameter = value; }
        }
        #endregion
    }
}
