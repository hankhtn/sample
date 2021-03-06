using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Ribbon;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase;

namespace DialogsDemo {
    [CodeFile("ViewModels/ThemedWindowModule/ThemedWindowViewModel.cs")]
    [CodeFile("Resources/ThemedWindowModuleResources.xaml")]
    [CodeFile("Helpers/ThemedWindowService.cs")]
    public partial class ThemedWindowModule {
        public ThemedWindowModule() {
            InitializeComponent();
        }
    }
}
