using System.Windows;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Ribbon;
using DevExpress.XtraReports;
using DevExpress.Mvvm.UI;

namespace SchedulingDemo {
    public interface IBackstageViewService {
        void Close();
    }
    public class BackstageViewService : ServiceBase, IBackstageViewService {
        public void Close() {
            if((AssociatedObject is RibbonControl))
                ((RibbonControl)AssociatedObject).CloseApplicationMenu();
        }
    }
    public class CustomBackstageDocumentPreview : BackstageDocumentPreview {
        public static readonly DependencyProperty DocumentSourceProperty =
            DependencyProperty.Register("DocumentSource", typeof(IReport), typeof(CustomBackstageDocumentPreview), new PropertyMetadata(null));
        public IReport DocumentSource { get { return (IReport)GetValue(DocumentSourceProperty); } set { SetValue(DocumentSourceProperty, value); } }

        public CustomBackstageDocumentPreview() {
            ExportFormats = new string[] { "Pdf", "Htm", "Image" };
        }
    }
}
