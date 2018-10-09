using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class Groups : PivotGridDemoModule {
        public Groups() {
            InitializeComponent();
        }
        void UpdateGroupsExpanded(bool expanded) {
            pivotGrid.BeginUpdate();
            try {
                foreach(PivotGridGroup group in pivotGrid.Groups)
                    foreach(PivotGridField field in group)
                        field.ExpandedInFieldsGroup = expanded;
            } finally {
                pivotGrid.EndUpdate();
            }
        }
        void Collapse_Click(object sender, RoutedEventArgs e) {
            UpdateGroupsExpanded(false);
        }
        void Expand_Click(object sender, RoutedEventArgs e) {
            UpdateGroupsExpanded(true);
        }
    }
}
