using DevExpress.Diagram.Core;
using DevExpress.Diagram.Core.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace DiagramDemo {
    public partial class CircularLayoutModule : LayoutModuleBase {
        public CircularLayoutModule() {
            InitializeComponent();
            diagramControl.LoadDemoData("CircularLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplyCircularLayout();
            diagramControl.FitToPage();
        }
    }
}
