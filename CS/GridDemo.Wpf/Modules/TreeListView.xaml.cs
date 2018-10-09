using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Grid;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GridDemo {
    public partial class TreeListView : GridDemoModule {
        public TreeListView() {
            InitializeComponent();
        }      
    }
    public class EmployeeCategoryImageSelector : TreeListNodeImageSelector {
        public override ImageSource Select(DevExpress.Xpf.Grid.TreeList.TreeListRowData rowData) {
            Employee empl = (rowData.Row as Employee);
            if(empl == null || string.IsNullOrEmpty(empl.GroupName))
                return null;
            var path = GroupNameToImageConverterExtension.GetImagePathByGroupName(empl.GroupName);
            return new BitmapImage(new Uri(path));
        }
    }
}
