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
    public partial class TipOverTreeLayoutModule : LayoutModuleBase {
        public TipOverTreeLayoutModule() {
            InitializeComponent();
            diagramControl.LoadDemoData("TipOverTreeLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplyTipOverTreeLayout();
            diagramControl.FitToPage();
        }
    }
}
