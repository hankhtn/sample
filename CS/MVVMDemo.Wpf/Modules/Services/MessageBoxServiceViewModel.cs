using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.Services {
    public class MessageBoxServiceViewModel {
        public void ShowMessage(string serviceName) {
            IMessageBoxService messageBoxService = this.GetRequiredService<IMessageBoxService>(serviceName);
            Result = messageBoxService.ShowMessage(Text, Caption, Buttons, Icon);
        }

        #region properties
        public string Text { get; set; }
        public string Caption { get; set; }
        public MessageButton Buttons { get; set; }
        public MessageIcon Icon { get; set; }

        public virtual MessageResult? Result { get; protected set; }

        protected MessageBoxServiceViewModel() {
            Text = "Message text";
            Caption = "Caption";
            Buttons = MessageButton.OKCancel;
            Icon = MessageIcon.Information;
        }
        #endregion
    }
}
