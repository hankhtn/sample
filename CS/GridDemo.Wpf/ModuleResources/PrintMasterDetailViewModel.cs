using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    [POCOViewModel]
    public class PrintMasterDetailViewModel : PrintViewModelBase {

        protected PrintMasterDetailViewModel() { }

        public override void OnLoaded(IPrintableControl printableControl) {
            base.OnLoaded(printableControl);
            ShowPreviewInNewTab();
        }

        protected override string GetTitle() {
            return "Print Preview";
        }
    }
}
