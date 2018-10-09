using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using System.Collections;
using DevExpress.Xpf.Core;
using DevExpress.Utils;
using System.Data;
using DevExpress.Xpf.Editors;
using DevExpress.DemoData;
using System.Collections.Generic;
using DevExpress.DemoData.Models;
using EditorsDemo;
using DevExpress.Data.Filtering;

namespace CommonDemo {
    
    
    
    [CodeFile("ModuleResources/LookUpEditTemplates.xaml")]
    [CodeFile("ModuleResources/LookUpEditWithMultipleSelectionTemplates.xaml")]
    [CodeFile("ViewModels/LookUpEditorDemoViewModel.cs")]
    public partial class LookUpEdit : CommonDemoModule {

        public LookUpEdit() {
            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(string.Format("/{0};component/Themes/{1}", AssemblyHelper.GetPartialName(typeof(LookUpEdit).Assembly), GenericXamlName), UriKind.Relative) });
            InitializeComponent();
            OptionsDataContext = lookUpEdit;
            Initialize();
        }

        LookUpEditorDemoViewModel ViewModel { get; set; }
        string GenericXamlName { get { return "Common.xaml"; } }

        void Initialize() {
            ComboBoxEdit.SetupComboBoxEnumItemSource<FindMode, FindMode>(cbFindModeComboBox);
            ComboBoxEdit.SetupComboBoxEnumItemSource<FilterCondition, FilterCondition>(cbFilterConditionComboBox);
            DataContext = ViewModel = new LookUpEditorDemoViewModel();
        }

        Control control;
        void LookUpEditProcessNewValue(DependencyObject sender, DevExpress.Xpf.Editors.ProcessNewValueEventArgs e) {
            if (!(bool)ceProcessNewValue.IsChecked)
                return;

            control = new ContentControl { Template = (ControlTemplate)Resources["addNewRecordTemplate"], Tag = e };
            Product row = new Product();
            row.ProductName = e.DisplayText;

            control.DataContext = row;
            FrameworkElement owner = sender as FrameworkElement;
            DialogClosedDelegate closeHandler = CloseAddNewRecordHandler;

            FloatingContainer.ShowDialogContent(control, owner, Size.Empty, new FloatingContainerParameters() {
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
            if (close.HasValue && close.Value)
                ViewModel.ProductsSource.Add((Product)control.DataContext);
            control = null;
        }
    }
}
