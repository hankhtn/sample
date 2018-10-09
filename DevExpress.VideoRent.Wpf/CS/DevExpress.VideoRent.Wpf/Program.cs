using System;
using System.IO;
using System.Linq;
using System.Windows;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.VideoRent.Wpf.Helpers;
using System.Reflection;
using DevExpress.Internal;
using DevExpress.Xpf.Core;

namespace DevExpress.VideoRent.Wpf {
    class App : Application {
        public App() {
            Startup += OnStartup;
        }
        public event EventHandler BeforeMainWindowClosed;
        void OnStartup(object sender, StartupEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BeforeClosed += OnMainWindowBeforeClosed;
            mainWindow.Show();
        }
        void OnMainWindowBeforeClosed(object sender, EventArgs e) {
            if(BeforeMainWindowClosed != null)
                BeforeMainWindowClosed(this, EventArgs.Empty);
        }
    }
    static class Program {
        const string iniFilePath = "VideoRent.ini";
        [STAThread]
        static void Main(string[] args) {
            ExceptionHelper.Initialize();
            LoadPlugins();
            new System.Windows.Documents.FlowDocument();
            foreach(var theme in Theme.Themes.Where(t => t.Name.Contains("Touch")))
                theme.ShowInThemeSelector = false;
            ApplicationThemeHelper.ApplicationThemeName = WpfLayoutData.DefaultApplicationTheme.Name;
            WpfViewsManager.RegisterHelperViews();
            IniFile iniFile = new IniFile();
            if(File.Exists(iniFilePath)) iniFile.Load(iniFilePath);
            InitialDbCreator initialDbCreator = new InitialDbCreator(new CreateInitialDbDialog(), ExceptionProcesser.Current);
            if(!initialDbCreator.OpenDb(iniFile)) return;
            iniFile.Save(iniFilePath);
            //TODO Create Login-Dialog
            if(!LayoutManager.Current.Login(ReferenceData.AdministratorString, string.Empty)) return;
            App app = new App();
            app.BeforeMainWindowClosed += (d, e) => { LayoutManager.Current.Logout(); };
            app.Run();
        }
        #region LoadPlugins
        static void LoadPlugins() {
            foreach(string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "DevExpress.VideoRent.Wpf.Plugins.*.exe")) {
                Assembly plugin = Assembly.LoadFrom(file);
                if(plugin == null) continue;
                Type entry = plugin.GetType("Global.Program");
                if(entry == null) continue;
                MethodInfo start = entry.GetMethod("Start", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null);
                if(start == null) continue;
                start.Invoke(null, new object[] { });
            }
        }
        #endregion
    }
}
