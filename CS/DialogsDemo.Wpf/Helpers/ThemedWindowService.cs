using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;

namespace DialogsDemo {
    public interface IThemedWindowService {
        bool Show(object dataContext);
        bool Hide();
        bool IsWindowOpened { get; }
    }
    public class ThemedWindowService : ServiceBase, IThemedWindowService {
        ThemedWindow themedWindow;
        public Style WindowStyle { get; set; }
        public ThemedWindowService() {            
        }

        void OnThemedWindowClosed(object sender, EventArgs e) {
            IsWindowOpened = false;
            themedWindow.Closed -= OnThemedWindowClosed;
            themedWindow = null;            
        }

        public bool Show(object dataContext) {
            if (IsWindowOpened)
                return false;
            IsWindowOpened = true;
            themedWindow = new ThemedWindow();
            themedWindow.Closed += OnThemedWindowClosed;            
            themedWindow.DataContext = dataContext;
            themedWindow.Style = WindowStyle;
            themedWindow.Show();
            return true;
        }

        public bool Hide() {
            if (!IsWindowOpened)
                return false;
            themedWindow.Close();            
            return true;
        }

        public bool IsWindowOpened { get; private set; }
    }
}
