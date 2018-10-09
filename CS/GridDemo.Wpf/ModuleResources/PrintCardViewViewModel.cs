using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    [POCOViewModel]
    public class PrintCardViewViewModel : PrintViewModelBase {
        protected PrintCardViewViewModel() { }
        public virtual bool UseCustomPrintStyles { get; set; }

        public override void OnLoaded(IPrintableControl printableControl) {
            base.OnLoaded(printableControl);
            ShowPreviewInNewTab();
            UseCustomPrintStyles = true;
            ShowPreviewInNewTab();
            UseCustomPrintStyles = false;
        }
        protected override string GetTitle() {
            return (UseCustomPrintStyles ? "Uniform Cards Size" : "Default") + " Style Print Preview";
        }
    }
}
