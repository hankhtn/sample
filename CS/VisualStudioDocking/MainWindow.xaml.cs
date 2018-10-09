using System;
using DevExpress.Xpf.Core;

namespace VisualStudioDocking {
    public partial class MainWindow : ThemedWindow {
        public MainWindow() {
            InitializeComponent();
        }
    }
    public class CodeViewPresenter : DevExpress.Xpf.DemoBase.Helpers.Internal.CodeViewPresenter { }
}
