using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;

namespace DockingDemo {
    public partial class IDEWorkspaces : DockingDemoModule {
        IWorkspaceManager Manager;
        int workspaceNumber = 1;
        public IDEWorkspaces() {
            InitializeComponent();
            Manager = WorkspaceManager.GetWorkspaceManager(DemoDockContainer);
            Manager.TransitionEffect = TransitionEffect.Ripple;
            LoadPredefinedWorkspaces();
        }
        void LoadPredefinedWorkspaces() {
            workspaces.Items.Clear();
            Assembly thisExe = typeof(IDEWorkspaces).Assembly;
            for(int i = 1; i <= 4; i++, workspaceNumber++) {
                string workspaceName = string.Format("workspace{0}", i);
                string fileName = string.Format("{0}.xml", workspaceName);
                using(Stream resourceStream = AssemblyHelper.GetEmbeddedResourceStream(thisExe, DemoHelper.GetPath("Layouts/", thisExe) + fileName, true))
                    Manager.LoadWorkspace(workspaceName, resourceStream);
                workspaces.Items.Add(workspaceName);
            }
        }
        void captureWorkspace_Click(object sender, System.Windows.RoutedEventArgs e) {
            string name = string.Format("workspace{0}", workspaceNumber++);
            Manager.CaptureWorkspace(name);
            workspaces.Items.Add(name);
            if(workspaces.Visibility == Visibility.Collapsed)
                workspaces.Visibility = Visibility.Visible;
        }
        void removeWorkspace_Click(object sender, System.Windows.RoutedEventArgs e) {
            string name = workspaces.SelectedItem as string;
            if(name == null)
                return;
            Manager.RemoveWorkspace(name);
            workspaces.Items.Remove(name);
            if(workspaces.Items.Count == 0) {
                workspaces.SelectedIndex = -1;
                workspaces.Visibility = Visibility.Collapsed;
            }
        }
        void workspaces_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string name = workspaces.SelectedItem as string;
            if(name != null)
                Manager.ApplyWorkspace(name);
        }
    }
}
