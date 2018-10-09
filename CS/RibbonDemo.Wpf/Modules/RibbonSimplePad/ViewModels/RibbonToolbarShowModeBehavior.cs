using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace RibbonDemo {
    class RibbonOptionsBehavior: Behavior<RibbonControl> {
        public static readonly DependencyProperty CurrentToolBarShowModeProperty;
        public static readonly DependencyProperty CurrentPageCategoryAlignmentProperty;
        public static readonly DependencyProperty CurrentTitleBarVisibilityProperty;
        
        static RibbonOptionsBehavior() {
            CurrentToolBarShowModeProperty = DependencyProperty.Register("CurrentToolBarShowMode", typeof(RibbonQuickAccessToolbarShowMode), typeof(RibbonOptionsBehavior), new PropertyMetadata(RibbonQuickAccessToolbarShowMode.Default, (d, e) => ((RibbonOptionsBehavior)d).Update()));
            CurrentPageCategoryAlignmentProperty = DependencyProperty.Register("CurrentPageCategoryAlignmentProperty", typeof(RibbonPageCategoryCaptionAlignment), typeof(RibbonOptionsBehavior), new PropertyMetadata(RibbonPageCategoryCaptionAlignment.Left, (d, e) => ((RibbonOptionsBehavior)d).Update()));
            CurrentTitleBarVisibilityProperty = DependencyProperty.Register("CurrentTitleBarVisibilityProperty", typeof(RibbonTitleBarVisibility), typeof(RibbonOptionsBehavior), new PropertyMetadata(RibbonTitleBarVisibility.Auto, (d, e) => ((RibbonOptionsBehavior)d).Update()));
        }        
        public RibbonTitleBarVisibility CurrentTitleBarVisibility {
            get { return (RibbonTitleBarVisibility)GetValue(CurrentTitleBarVisibilityProperty); }
            set { SetValue(CurrentTitleBarVisibilityProperty, value); }
        }
        public RibbonQuickAccessToolbarShowMode CurrentToolBarShowMode {
            get { return (RibbonQuickAccessToolbarShowMode)GetValue(CurrentToolBarShowModeProperty); }
            set { SetValue(CurrentToolBarShowModeProperty, value); }
        }
        public RibbonPageCategoryCaptionAlignment CurrentPageCategoryAlignment {
            get { return (RibbonPageCategoryCaptionAlignment)GetValue(CurrentPageCategoryAlignmentProperty); }
            set { SetValue(CurrentPageCategoryAlignmentProperty, value); }
        }        
        
        protected override void OnAttached() {
            base.OnAttached();
            BindingOperations.SetBinding(this, CurrentToolBarShowModeProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath(RibbonControl.ToolbarShowModeProperty) });
            BindingOperations.SetBinding(this, CurrentTitleBarVisibilityProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath(RibbonControl.PageCategoryAlignmentProperty) });
            BindingOperations.SetBinding(this, CurrentPageCategoryAlignmentProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath(RibbonControl.PageCategoryAlignmentProperty) });            
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            ClearValue(CurrentToolBarShowModeProperty);
            ClearValue(CurrentTitleBarVisibilityProperty);
            ClearValue(CurrentPageCategoryAlignmentProperty);
        }
        
        void Update() {            
            if (!IsAttached || AssociatedObject.ActualMergedParent == null)
                return;
            AssociatedObject.ActualMergedParent.ToolbarShowMode = CurrentToolBarShowMode;
            AssociatedObject.ActualMergedParent.PageCategoryAlignment = CurrentPageCategoryAlignment;
            AssociatedObject.ActualMergedParent.RibbonTitleBarVisibility = CurrentTitleBarVisibility;            
        }
    }
}
