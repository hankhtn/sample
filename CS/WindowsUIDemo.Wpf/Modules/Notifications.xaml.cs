using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace WindowsUIDemo {
    [CodeFile("ViewModels/NotificationsViewModel.(cs)")]
    public partial class Notifications : WindowsUIDemoModule {
        public Notifications() {
            InitializeComponent();
#if CLICKONCE
            useWin8Notifications.IsChecked = false;
            useWin8Notifications.IsEnabled = false;
#endif
        }

        public static string ApplicationID {
            get { return string.Format("Components_{0}_Demo_Center_{0}", AssemblyInfo.VersionShort.Replace(".", "_")); }
        }

        NotificationsViewModel ViewModel { get { return (NotificationsViewModel)DataContext; } }
        
        MemoryStream PatchBackground(MemoryStream stream, Color color) {
            string doc = Encoding.UTF8.GetString(stream.ToArray());
            Regex rx = new Regex("color=\"#(.*?)\"");
            var matches = rx.Matches(doc);
            if (matches.Count > 0) {
                string strColor = matches[0].Groups[1].ToString();
                doc = doc.Replace(strColor, color.ToString().Substring(3));
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(doc));
        }
    }
}
