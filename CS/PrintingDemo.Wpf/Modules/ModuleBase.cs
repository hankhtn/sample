using System.ComponentModel;
using System.Windows.Data;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase;

namespace PrintingDemo {
    public class ModuleBase : DemoModule {
        static ModuleBase() {
            NWindContext.Preload();
        }
        public ModuleBase() {
            Interaction.GetBehaviors(this).Add(CreateDispatchFocusBehavior());
            Loaded += ModuleBase_Loaded;
        }

        private void ModuleBase_Loaded(object sender, System.Windows.RoutedEventArgs e) {

            if(!DesignerProperties.GetIsInDesignMode(this) && ViewModel != null) {
                ViewModel.CreateDocument();
            }
        }

        public ModuleViewModelBase ViewModel {
            get {
                return (ModuleViewModelBase)DataContext;
            }
        }

        protected override void Clear() {
            base.Clear();
            if(ViewModel != null) {
                ViewModel.ClearDocument();
            }
        }

        protected virtual bool NeedChangeEditorsTheme { get { return false; } }

        static DispatchFocusBehavior CreateDispatchFocusBehavior() {
            var behavior = new DispatchFocusBehavior();
            BindingOperations.SetBinding(behavior, DispatchFocusBehavior.ElementProperty, new Binding() { ElementName = "viewer" });
            return behavior;
        }
    }
}
