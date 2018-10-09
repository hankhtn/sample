using DevExpress.Data;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Ribbon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductsDemo.Modules {
    
    
    
    public partial class GridTasksModule : UserControl {
        public GridTasksModule() {
            InitializeComponent();
        }

        void TableView_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e) {
            e.Source.PostEditor();
        }
    }

    public abstract class GridControlBehaviorBase : Behavior<GridControl> {
        protected GridViewModelBase ViewModel;

        protected override void OnAttached() {
            base.OnAttached();
            ViewModel = AssociatedObject.DataContext as GridViewModelBase;
        }
    }
    public class BarManagerFlagStatusService : Behavior<BarManager> {
        GridTasksModuleViewModel ViewModel { get { return (GridTasksModuleViewModel)AssociatedObject.DataContext; } }

        protected override void OnAttached() {
            base.OnAttached();
            ViewModel.SelectedItemChanged += ViewModel_SelectedItemChanged;
        }

        void ViewModel_SelectedItemChanged(object sender, DevExpress.Xpf.Core.ValueChangedEventArgs<Task> e) {
            if(e.OldValue != null)
                e.OldValue.PropertyChanged -= Task_PropertyChanged;
            if(e.NewValue != null)
                e.NewValue.PropertyChanged += Task_PropertyChanged;
            UpdateFlags();
        }

        void Task_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if(e.PropertyName == "FlagStatus")
                UpdateFlags();
        }

        void UpdateFlags() {
            foreach(BarCheckItem item in AssociatedObject.Items.Where(it => it.Name.StartsWith("followUp_"))) {
                if(ViewModel.SelectedItem == null) {
                    item.IsChecked = false;
                    continue;
                }
                item.IsChecked = item.Name.Contains(ViewModel.SelectedItem.FlagStatus.ToString());
            }
        }

        protected override void OnDetaching() {
            ViewModel.SelectedItemChanged += ViewModel_SelectedItemChanged;
            base.OnDetaching();
        }
    }
    public class GridControlColumnsUpdateLocker : GridControlBehaviorBase {
        protected override void OnAttached() {
            base.OnAttached();
            ViewModel.IsLoadingChanged += viewModel_IsLoadingChanged;
        }

        void viewModel_IsLoadingChanged(object sender, IsLoadingEventArgs e) {
            if(e.IsLoading) {
                AssociatedObject.Columns.BeginUpdate();
                AssociatedObject.SortInfo.BeginUpdate();
            } else {
                AssociatedObject.SortInfo.EndUpdate();
                AssociatedObject.Columns.EndUpdate();
            }
        }
        protected override void OnDetaching() {
            ViewModel.IsLoadingChanged -= viewModel_IsLoadingChanged;
            base.OnDetaching();
        }
    }
    public class GridControlPrint : GridControlBehaviorBase {
        GridControl printGridControl;

        protected override void OnAttached() {
            base.OnAttached();
            PrintingService.PrintableControlLink = GetPrintableControlLink();
            ViewModel.Print -= ViewModel_Print;
            ViewModel.Print += ViewModel_Print;
        }
        protected override void OnDetaching() {
            PrintingService.PrintableControlLink = null;
            ViewModel.Print -= ViewModel_Print;
            base.OnDetaching();
        }
        PrintableControlLink GetPrintableControlLink() {
            return new PrintableControlLink(GetPrintGridControl().View);
        }

        void ViewModel_Print(object sender, EventArgs e) {
            var link = GetPrintableControlLink();
            var preview = new DocumentPreviewControl() { DocumentSource = link };
            link.CreateDocument(true);
            var previewWindow = new Window() { Content = preview, Title = "Print Preview" };
            previewWindow.ShowDialog();
        }

        GridControl GetPrintGridControl() {
            if(AssociatedObject.View is TableView)
                return AssociatedObject;
            if(printGridControl == null) {
                printGridControl = AssociatedObject.TryFindResource("printGridControl") as GridControl;
                printGridControl.DataContext = AssociatedObject.DataContext;
                printGridControl.Style = AssociatedObject.TryFindResource("gridControlMVVMStyle") as Style;
                Interaction.GetBehaviors(printGridControl).Add(new GridControlColumnsUpdateLocker());
            }
            return printGridControl;
        }
    }

    public class StatusBarSummaryUpdate : GridControlBehaviorBase {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.CustomSummary += AssociatedObject_CustomSummary;
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(StatusBarSummaryUpdate), new PropertyMetadata(null));

        public int Count {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        void AssociatedObject_CustomSummary(object sender, CustomSummaryEventArgs e) {
            switch(e.SummaryProcess) {
                case CustomSummaryProcess.Start:
                    Count = 0;
                    break;
                case CustomSummaryProcess.Calculate:
                    break;
                case CustomSummaryProcess.Finalize:
                    Count = (int)AssociatedObject.GetTotalSummaryValue(AssociatedObject.TotalSummary[0]);
                    break;
            }
        }
        protected override void OnDetaching() {
            AssociatedObject.CustomSummary -= AssociatedObject_CustomSummary;
            base.OnDetaching();
        }
    }
}
