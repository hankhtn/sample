using DevExpress.DemoData.Models;
using System.Data.Entity;

namespace GridDemo {
    public partial class MasterDetailViewViaEntityFramework : GridDemoModule {
        public MasterDetailViewViaEntityFramework() {
            InitializeComponent();
            var customers = new NWindContext().Customers;
            customers.Load();
            grid.ItemsSource = customers.Local;
        }
    }
}
