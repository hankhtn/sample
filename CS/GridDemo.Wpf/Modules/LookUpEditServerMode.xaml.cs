using System;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InstantFeedbackModeViewModelBase.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/LookUpEditServerMode/LookUpInstantFeedbackModeViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/LookUpEditServerMode/PersonsDataContext.(cs)")]
    public partial class LookUpEditServerMode : GridDemoModule {
        public LookUpEditServerMode() {
            InitializeComponent();
            ModuleLoaded += (o, e) => {
                Dispatcher.BeginInvoke(new Action(() => {
                    ((LookUpInstantFeedbackModeViewModel)DataContext).OnLoaded();
                }));
            };
        }
    }
}
