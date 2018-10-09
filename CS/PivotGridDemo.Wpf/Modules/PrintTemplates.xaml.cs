using System;
using System.Windows;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class PrintTemplates : PivotGridDemoModule {
        public PrintTemplates() {
            InitializeComponent();
            ResetLayout();
        }

        void Button_Click(object sender, RoutedEventArgs e) {
            pivotGrid.ShowPrintPreview(Window.GetWindow(this));
        }

        void pivotGrid_CustomGroupInterval(object sender, PivotCustomGroupIntervalEventArgs e) {
            e.GroupValue = MoonPhaseCalculator.CalculateMoonPhase((DateTime)e.Value);
        }

        void ResetLayout() {
            
            fieldCategory.Area = FieldArea.RowArea;
            fieldMoonPhase.Area = FieldArea.ColumnArea;
            fieldMoonPhase.Visible = false;
            fieldMoonPhase.FilterValues.Clear();
            fieldYear.Area = FieldArea.ColumnArea;
            fieldYear.Visible = true;
            fieldYear.AreaIndex = 0;
            fieldYear.FilterValues.Clear();
            fieldQuarter.Area = FieldArea.ColumnArea;
            fieldQuarter.Visible = true;
            fieldQuarter.AreaIndex = 1;
            fieldQuarter.FilterValues.Clear();
            fieldSales.Area = FieldArea.DataArea;
            fieldSales.Visible = true;
            fieldSales.FilterValues.Clear();
        }

        void templatesList_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(!IsInitialized) return;
            ResetLayout();
            if(templatesList.SelectedIndex == 1) {
                fieldYear.Visible = false;
                fieldQuarter.Visible = false;
                fieldMoonPhase.Visible = true;
            }
        }
    }
}
