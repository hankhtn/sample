using System;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PdfViewer;
using DevExpress.Internal;

namespace ProductsDemo {
    public partial class App : Application {
        public App() {
            ExceptionHelper.Initialize();
            DevExpress.Images.ImagesAssemblyLoader.Load();
            PdfViewerLocalizer.Active = new CustomPdfViewerLocalizer();
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013Name;
        }
        protected override void OnStartup(StartupEventArgs e) {
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
            base.OnStartup(e);
        }
    }
    public class CustomPdfViewerLocalizer : PdfViewerLocalizer {
        public override string GetLocalizedString(PdfViewerStringId id) {
            switch(id) {
                case PdfViewerStringId.BarCaption: return "PDF VIEWER";
                case PdfViewerStringId.BarCommentCaption: return "COMMENT";
                case PdfViewerStringId.BarFormDataCaption: return "FORM DATA";
                default: return base.GetLocalizedString(id);
            }
        }
    }
}
#if CLICKONCE
namespace DevExpress.Internal.DemoLauncher {
    public static class ClickOnceEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new ProductsDemo.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new ProductsDemo.MainWindow();
                    window.Show();
                    app.MainWindow = window;
                    done.SetResult(window);
                };
                app.Run();
            };
            return Tuple.Create(run, done.Task);
        }
    }
}
#endif
