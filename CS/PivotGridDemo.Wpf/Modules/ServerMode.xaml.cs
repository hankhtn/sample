using System;
using System.Diagnostics;
using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace PivotGridDemo {
    [CodeFile("Helpers/ServerMode/ServerModeViewModelBase.(cs)")]
    [CodeFile("Helpers/ServerMode/ServerModeViewModel.(cs)")]
    [CodeFile("Helpers/ServerMode/OrderDataContext.(cs)")]
    public partial class ServerMode : PivotGridDemoModule {
        readonly Stopwatch timer = new Stopwatch();

        public ServerMode() {
            InitializeComponent();
        }

        void pivotGrid_AsyncOperationStarting(object sender, RoutedEventArgs e) {
            tbTimeTaken.Text = "...";
            timer.Restart();
        }

        void pivotGrid_AsyncOperationCompleted(object sender, RoutedEventArgs e) {
            timer.Stop();
            tbTimeTaken.Text = timer.ElapsedMilliseconds.ToString();
        }
    }
}
