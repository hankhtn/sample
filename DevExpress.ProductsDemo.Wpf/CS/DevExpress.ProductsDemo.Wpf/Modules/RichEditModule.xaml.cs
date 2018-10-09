using System;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpellChecker;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.SpellChecker;
using System.Windows.Controls;
using System.Globalization;
using DevExpress.XtraSpellChecker.Native;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.SpellChecker;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Data;
using System.Linq;
using DevExpress.Xpf.Grid;

namespace ProductsDemo.Modules {
    public partial class RichEditModule : UserControl {
        public RichEditModule() {
            InitializeComponent();
        }

        void richEdit_StartHeaderFooterEditing(object sender, HeaderFooterEditingEventArgs e) {
            catHeaderFooterTools.IsVisible = true;
            ribbonControl.SelectedPage = pageHeaderFooterToolsInsert;
        }

        void richEdit_FinishHeaderFooterEditing(object sender, HeaderFooterEditingEventArgs e) {
            catHeaderFooterTools.IsVisible = false;
        }

        void richEdit_SelectionChanged(object sender, EventArgs e) {
            bool isSelectionInTable = richEdit.IsSelectionInTable();
            if (catTableTools.IsVisible != isSelectionInTable) {
                catTableTools.IsVisible = isSelectionInTable;
                if (isSelectionInTable)
                    ribbonControl.SelectedPage = pageTableToolsDesign;
            }

            bool isSelectionInFloatingObject = richEdit.IsFloatingObjectSelected;
            if (catPictureTools.IsVisible != isSelectionInFloatingObject) {
                catPictureTools.IsVisible = isSelectionInFloatingObject;
                if (isSelectionInFloatingObject)
                    ribbonControl.SelectedPage = pagePictureToolsFormat;
            }
        }
    }
}
