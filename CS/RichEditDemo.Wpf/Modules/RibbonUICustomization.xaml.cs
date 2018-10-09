using DevExpress.Xpf.Core;
using System.Windows;

namespace RichEditDemo {
    public partial class RibbonUICustomization : RichEditDemoModule {
        public RibbonUICustomization() {
            InitializeComponent();
        }

        void About_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            DXMessageBox.Show("This demo illustrates how to customize the WPF Rich Text Editor's integrated ribbon UI.\n\nSwitch to the Code View to learn how to use the Rich Text Editor's RibbonActions collection to create, remove or modify ribbon elements.",
                "Rich Text Editor Ribbon Customization", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
