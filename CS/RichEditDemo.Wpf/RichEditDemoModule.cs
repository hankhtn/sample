using System.Linq;
using System.Windows;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.Utils;

namespace RichEditDemo {
    public class RichEditDemoModule : DemoModule {
        static readonly DependencyPropertyKey RichEditControlPropertyKey;
        public static readonly DependencyProperty RichEditControlProperty;

        static RichEditDemoModule() {
            NWindContext.Preload();
            RichEditControlPropertyKey = DependencyPropertyManager.RegisterReadOnly("RichEditControl", typeof(RichEditControl), typeof(RichEditDemoModule), new FrameworkPropertyMetadata(null));
            RichEditControlProperty = RichEditControlPropertyKey.DependencyProperty;
        }

        public RichEditControl RichEditControl {
            get { return (RichEditControl)GetValue(RichEditControlProperty); }
            private set { SetValue(RichEditControlPropertyKey, value); }
        }

        void OnRichEditControlLoaded(object sender, RoutedEventArgs e) {
            SetFocus(RichEditControl);
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            RichEditControl = Content as RichEditControl ?? LayoutTreeHelper.GetVisualChildren((DependencyObject)Content).OfType<RichEditControl>().FirstOrDefault();
            if(RichEditControl != null) {
                RichEditControl.Loaded += OnRichEditControlLoaded;
                new RichEditDemoExceptionsHandler(RichEditControl).Install();
                SetBehaviorOptions();
            }
        }
        void SetBehaviorOptions() {
            RichEditControl.BehaviorOptions.FontSource = DevExpress.XtraRichEdit.RichEditBaseValueSource.Document;
            RichEditControl.BehaviorOptions.ForeColorSource = DevExpress.XtraRichEdit.RichEditBaseValueSource.Document;
        }
        protected internal virtual void SetFocus(RichEditControl control) {
            if(control == null)
                return;
            if(control.KeyCodeConverter != null)
                control.KeyCodeConverter.Focus();
        }
        protected override void ShowPopupContent() {
            base.ShowPopupContent();
            if(RichEditControl != null)
                RichEditControl.ShowHoverMenu = true;
        }
        protected override void HidePopupContent() {
            if(RichEditControl != null)
                RichEditControl.ShowHoverMenu = false;
            base.HidePopupContent();
        }
    }
}
