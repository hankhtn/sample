using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DialogsDemo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DialogsDemo {
    [CodeFile("Modules/Views/DXWindowHeaderModuleWindow.xaml")]
    [CodeFile("Modules/Views/DXWindowHeaderModuleWindow.xaml.cs")]
    [CodeFile("Modules/DXWindowBorderHighlightingEffects.xaml")]
    [CodeFile("Modules/DXMessageBoxModule.xaml")]
    public partial class DXWindowCustomHeaderButton : DialogsDemoModule {
        DXWindow window;
        Window rootWindow;
        Size desiredWindowSize = new Size(400, 200);

        public DXWindowCustomHeaderButton() {
            InitializeComponent();
            Init();
        }

        public bool BrouwserInteropHlper { get; set; }

        void Init() {
            ComboBoxEdit.SetupComboBoxEnumItemSource<MessageBoxButton, MessageBoxButton>(buttons);
            List<EnumHelperData> iconsCollection = new List<EnumHelperData>();
            foreach (string mbi in Enum.GetNames(typeof(MessageBoxImage)))
                iconsCollection.Add(new EnumHelperData() { Text = mbi, Value = Enum.Parse(typeof(MessageBoxImage), mbi) });
            icons.ItemsSource = iconsCollection;
            icons.DisplayMember = "Text";
            ComboBoxEdit.SetupComboBoxEnumItemSource<FloatingMode, FloatingMode>(floatingMode);
            icons.SelectedIndex = 0;
            buttons.SelectedIndex = 0;
            floatingMode.SelectedIndex = 1;
            if (BrowserInteropHelper.IsBrowserHosted) {
                floatingMode.Visibility = System.Windows.Visibility.Collapsed;
                floatingModeLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        void ButtonMessage_Click(object sender, RoutedEventArgs e) {
            DXMessageBox.Show(
                LayoutHelper.GetRoot(this) as Window,
                contentEdit.DisplayText,
                captionEdit.DisplayText,
                (MessageBoxButton)buttons.EditValue,
                (MessageBoxImage)((EnumHelperData)icons.EditValue).Value,
                MessageBoxResult.None, MessageBoxOptions.None,
                (FloatingMode)floatingMode.EditValue
                );
        }        

        void ShowCustomHeaderWindow() {
            if (window != null)
                window.Close();
            window = new DXWindowHeaderModuleWindow();
            
            window.Show();
        }

        void Button_Click(object sender, RoutedEventArgs e) {
            ShowCustomHeaderWindow();
        }

        protected override void HidePopupContent() {
            if(window != null)
                window.Close();
            base.HidePopupContent();
        }
        
        protected Rect GetWindowSuggestedRect(Rect parentRect) {
            return new Rect(parentRect.Left + parentRect.Width / 2d - desiredWindowSize.Width, parentRect.Top + parentRect.Height / 2d - desiredWindowSize.Height, desiredWindowSize.Width, desiredWindowSize.Height);
        }
                
        void SetBorderEffectCustomColors() {
            window.BorderEffectActiveColor = new SolidColorBrush(activeColor.Color);
            window.BorderEffectInactiveColor = new SolidColorBrush(inactiveColor.Color);
        }

        void ShowWindow() {
            if (window != null)
                window.Close();
            window = new DXWindow();
            
                window.BorderEffect = BorderEffect.Default;
            if (enableCustomization.IsChecked.Value)
                SetBorderEffectCustomColors();
            rootWindow = LayoutHelper.GetRoot(this) as Window;
            if (rootWindow != null) {
                window.SetBounds(GetWindowSuggestedRect(rootWindow.GetBounds()));
                window.Icon = rootWindow.Icon;
                rootWindow.Closed += RootWindowClosed;
                window.Owner = rootWindow;
            }
            window.Title = "DXWindow";
            window.Topmost = true;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        void ButtonEffectsClick(object sender, RoutedEventArgs e) {
            ShowWindow();
        }

        void RootWindowClosed(object sender, EventArgs e) {
            if (window != null)
                window.Close();
        }

        void EnableCustomizationEditValueChanged(object sender, EditValueChangedEventArgs e) {
            if (window == null)
                return;
            if ((bool)e.NewValue)
                SetBorderEffectCustomColors();
            else
                window.BorderEffectReset();
        }

        void EnableEffectEditValueChanged(object sender, EditValueChangedEventArgs e) {
            if (window == null)
                return;
            if ((bool)e.NewValue)
                window.BorderEffect = BorderEffect.Default;
            else {
                window.BorderEffectReset();
                window.BorderEffect = BorderEffect.None;
            }
            if (enableCustomization.IsChecked.Value)
                SetBorderEffectCustomColors();
            

        }

        void ActiveColorColorChanged(object sender, RoutedEventArgs e) {
            if (window != null)
                window.BorderEffectActiveColor = new SolidColorBrush(activeColor.Color);
        }

        void InactiveColorColorChanged(object sender, RoutedEventArgs e) {
            if (window != null)
                window.BorderEffectInactiveColor = new SolidColorBrush(inactiveColor.Color);
        }
    }

    public class EnumHelperData {
        public string Text { get; set; }
        public object Value { get; set; }
    }
}
