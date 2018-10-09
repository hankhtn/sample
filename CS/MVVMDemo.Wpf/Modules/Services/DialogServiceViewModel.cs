using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace MVVMDemo.Services {
    public class DialogServiceViewModel {
        public void ShowDetail(string serviceName) {
            DialogServiceDetailViewModel detailViewModel = DialogServiceDetailViewModel.Create();

            UICommand registerCommand = new UICommand() {
                Caption = "Register",
                IsDefault = true,
                Command = new DelegateCommand(() => { }, () => !string.IsNullOrEmpty(detailViewModel.CustomerName))
            };
            UICommand cancelCommand = new UICommand() {
                Caption = "Cancel",
                IsCancel = true,
            };

            IDialogService service = this.GetService<IDialogService>(serviceName);
            UICommand result = service.ShowDialog(
                dialogCommands: new[] { registerCommand, cancelCommand }, 
                title: "Registration Dialog", 
                viewModel: detailViewModel
            );

            if(result == registerCommand)
                MessageBox.Show("Registered: " + detailViewModel.CustomerName);
        }
    }
}
