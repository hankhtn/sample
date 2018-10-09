using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    [POCOViewModel]
    public class PrintOptionsViewModel : PrintViewModelBase {
        protected PrintOptionsViewModel() { }
        public virtual bool UseCustomPrintStyles { get; set; }

        public override void OnLoaded(IPrintableControl printableControl) {
            base.OnLoaded(printableControl);
            ShowPreviewInNewTab();
            UseCustomPrintStyles = true;
            ShowPreviewInNewTab();
            UseCustomPrintStyles = false;
        }
        protected override string GetTitle() {
            return (UseCustomPrintStyles ? "Custom" : "Default") + " Style Print Preview";
        }

    }
}
