using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Printing;
using System.Linq;
using System;
using DevExpress.Xpf.DemoBase.Helpers;

namespace GridDemo {
    public abstract class PrintViewModelBase {
        IPrintableControl printableControl;
        protected PrintViewModelBase() {

        }
        public virtual void OnLoaded(IPrintableControl printableControl) {
            this.printableControl = printableControl;
        }
        public void OnUnloaded() {
            var service = this.GetService<IDocumentManagerService>();
            foreach(var document in service.Documents.ToArray()) {
                OnTabClosed(document.Content);
                document.Close();
            }
        }
        public void ShowPreviewInNewTab() {
            Logger.Log("ShowPreviewInNewTab");
            var service = this.GetService<IDocumentManagerService>();
            var link = new PrintableControlLink(printableControl);
            link.CreateDocument(true);
            var doc = service.CreateDocument(link);
            doc.Title = GetTitle();
            doc.DestroyOnClose = true;
            doc.Show();
        }
        public void OnTabClosed(object tabContent) {
            ((PrintableControlLink)tabContent).Dispose();
        }
        protected abstract string GetTitle();
    }
}
