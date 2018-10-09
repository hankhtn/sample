using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    [POCOViewModel]
    public class PrintTemplatesViewModel : PrintViewModelBase {
        protected PrintTemplatesViewModel() { }
        public virtual PrintTemplatesMode Mode { get; set; }

        public override void OnLoaded(IPrintableControl printableControl) {
            base.OnLoaded(printableControl);
            Mode = PrintTemplatesMode.Default;
            ShowPreviewInNewTab();
            Mode = PrintTemplatesMode.MailMerge;
            ShowPreviewInNewTab();
            Mode = PrintTemplatesMode.Detail;
            ShowPreviewInNewTab();
        }
        protected override string GetTitle() {
            return Mode.ToString() + " Print Preview";
        }
    }
    public enum PrintTemplatesMode {
        Detail,
        MailMerge,
        Default
    }
}
