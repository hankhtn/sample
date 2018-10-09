using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EditorsDemo {
    public partial class ValidationSettings : UserControl {
        public static readonly DependencyProperty FocusedEditorProperty =
            DependencyProperty.Register("FocusedEditor", typeof(DependencyObject), typeof(ValidationSettings), new PropertyMetadata(null, new PropertyChangedCallback(FocusedEditorPropertyChanged)));
        static void FocusedEditorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            d.SetValue(FrameworkElement.DataContextProperty, e.NewValue);
        }
        public DependencyObject FocusedEditor {
            get { return (DependencyObject)GetValue(FocusedEditorProperty); }
            set { SetValue(FocusedEditorProperty, value); }
        }
        public ValidationSettings() {
            InitializeComponent();
        }
    }
}
