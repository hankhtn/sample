using DevExpress.DemoData;
using DevExpress.DemoData.Models;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace CommonDemo {
    
    
    
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiColumnLookupEditorTemplates.xaml")]
    public partial class StandaloneMultiColumnLookupEditor : CommonDemoModule {
        NWindDataLoader NWind { get; set; }
        IList<Product> Products { get { return (IList<Product>)lookUpEdit.DataContext; } }
        public StandaloneMultiColumnLookupEditor() {
            InitializeComponent();
            OptionsDataContext = lookUpEdit;
            NWind = Resources["NWindDataLoader"] as NWindDataLoader;
        }
        Control control;
        private void lookUpEdit_ProcessNewValue(DependencyObject sender, DevExpress.Xpf.Editors.ProcessNewValueEventArgs e) {
            if(!(bool)chProcessNewValue.IsChecked)
                return;

            control = new ContentControl { Template = (ControlTemplate)Resources["addNewRecordTemplate"], Tag = e };
            Product row = new Product();
            row.ProductName = e.DisplayText;
            control.DataContext = row;
            FrameworkElement owner = sender as FrameworkElement;
            DialogClosedDelegate closeHandler = CloseAddNewRecordHandler;

            FloatingContainer.ShowDialogContent(control, owner, Size.Empty, new FloatingContainerParameters()
            {
                Title = "Add New Record",
                AllowSizing = false,
                ClosedDelegate = closeHandler,
                ContainerFocusable = false,
                ShowModal = true
            });

            e.PostponedValidation = true;
            e.Handled = true;
        }

        void CloseAddNewRecordHandler(bool? close) {
            if(close.HasValue && close.Value)
                Products.Add((Product)control.DataContext);
            control = null;
        }
    }
}
