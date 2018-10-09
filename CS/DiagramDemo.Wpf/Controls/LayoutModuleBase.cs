using System;
using DevExpress.Xpf.DemoBase;
using System.IO;
using DevExpress.Xpf.Diagram;
using DevExpress.Diagram.Core;
using DevExpress.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using DiagramDemo;
using System.Windows.Media;
using DevExpress.Utils;
using System.Windows.Media.Imaging;

namespace DiagramDemo {
    public class LayoutModuleBase : DiagramDemoModule {
        public LayoutModuleBase() {
            this.Loaded += OnLoad;
        }
        void OnLoad(object sender, RoutedEventArgs e) {
            RelayoutDiagram();
        }
        void RelayoutDiagram() {
            if(!IsLoaded) return;
            RelayoutDiagramCore();
        }
        protected virtual void RelayoutDiagramCore() { }
        protected void SelectedIndexChanged(object sender, RoutedEventArgs e) {
            RelayoutDiagram();
        }
        protected void EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            RelayoutDiagram();
        }
    }
}
