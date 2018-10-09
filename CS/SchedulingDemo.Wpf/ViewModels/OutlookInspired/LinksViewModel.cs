using DevExpress.Xpf.Core;
using DevExpress.Mvvm.POCO;

namespace SchedulingDemo.ViewModels {
    public class LinksViewModel {
        public static LinksViewModel Create() {
            return ViewModelSource.Create(() => new LinksViewModel());
        }
        protected LinksViewModel() { }

        public void GettingStarted() {
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_GetStarted_WPF.aspx");
        }
        public void GetFreeSupport() {
            DocumentPresenter.OpenLink(AssemblyInfo.DXLinkGetSupport);
        }
        public void BuyNow() {
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_Subscriptions_Buy.aspx");
        }
        public void UniversalSubscription() {
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_UniversalSubscription.aspx");
        }
        public void About() {
            DevExpress.Xpf.About.ShowAbout(DevExpress.Utils.About.ProductKind.DXperienceWPF);
        }
    }
}