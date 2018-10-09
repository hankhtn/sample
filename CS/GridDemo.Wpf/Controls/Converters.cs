using DevExpress.Data.Filtering;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace GridDemo {
    public abstract class BaseValueConverter : MarkupExtension, IValueConverter {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public sealed override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class MultiSelectModeConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return !object.Equals((MultiSelectMode)value, MultiSelectMode.None);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? MultiSelectMode.Row : MultiSelectMode.None;
        }
    }
    public class FocusedToColorConverter : MarkupExtension, IValueConverter {
        public FocusedToColorConverter() { }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(((string)parameter) == Enum.GetName(typeof(FocusedGrid), (FocusedGrid)value))
                return new SolidColorBrush(Color.FromArgb(50, 200, 0, 0));
            return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class PastUnderFocusedRowToSelectedIndexConverter : MarkupExtension, IValueConverter {
        public PastUnderFocusedRowToSelectedIndexConverter() { }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((bool)value) ? 0 : 1;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((int)value == 0) ? true : false;
        }
        #endregion
    }

    public class BooleanToDefaultBooleanConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ScrollingAnimationDurationToBooleanConverterExtension : BaseValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToDouble(value) > 0;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToBoolean(value) ? 350 : 0;
        }
    }
    public class ShowSearchPanelModeToTextConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            ShowSearchPanelMode? showSearchPanelModeq = (ShowSearchPanelMode?)value;
            if(showSearchPanelModeq == null)
                return value;
            ShowSearchPanelMode showSearchPanelMode = showSearchPanelModeq.Value;
            switch(showSearchPanelMode) {
                case ShowSearchPanelMode.Default: return "Default";
                case ShowSearchPanelMode.Always: return "Always";
                case ShowSearchPanelMode.Never: return "Never";
            }
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class FindModeToTextConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            FindMode? findModeq = (FindMode?)value;
            if(findModeq == null)
                return value;
            FindMode findMode = findModeq.Value;
            switch(findMode) {
                case FindMode.Always: return "Always";
                case FindMode.FindClick: return "Find on Click";
            }
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class DefaultBooleanToNulltableConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (int)value != (int)DefaultBoolean.False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(!(value is bool))
                return DefaultBoolean.Default;
            return (bool)value ? DefaultBoolean.True : DefaultBoolean.False;
        }
    }


    public class SearchPanelModeConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((ListBoxEditItem)value).Content;
        }
    }
    public class SearchPanelFindButtonEnabledConverter : IMultiValueConverter {  
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(values == null || values.Length != 2)
                return false;
            return !(bool)values[0] && (int)values[1] == 1;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class BoolToScrollBarSearchAnnotationConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? ScrollBarAnnotationMode.SearchResult : ScrollBarAnnotationMode.None;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class GroupNameToImageConverterExtension : BaseValueConverter {
        public static List<string> images = new List<string> { "administration", "inventory", "manufacturing", "quality", "research", "sales" };
        public static string GetImagePathByGroupName(string groupName) {
            groupName = groupName.ToLower();
            foreach(string item in images) {
                if(groupName.Contains(item)) {
                    return "pack://application:,,,/GridDemo;component/Images/GroupName/" + item + ".png";
                }
            }
            return groupName;
        }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return null;
            return GetImagePathByGroupName((string)value);
        }
    }
    public class DemoHeaderImageExtension : MarkupExtension {
        readonly string imageName;

        public DemoHeaderImageExtension(string imageName) {
            this.imageName = imageName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/GridDemo;component/Images/HeaderImages/" + imageName.Replace(" ", String.Empty) + ".png"));
        }
    }
    public class ColumnHeaderTextConverter : IValueConverter {
        public string ColumnName { get; set; }
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return null;
            return ((string)value).Replace(" ", "") == ColumnName ? "Bold" : "Normal";
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class BirthdayImageVisibilityConverterExtension : BaseValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null || !(value is DateTime))
                return Visibility.Collapsed;
            DateTime birthDate = (DateTime)value;
            DateTime someDate = DateTime.Now.AddMonths(3);
            int someMonth = someDate.Month < 3 ? someDate.Month + 12 : someDate.Month;
            int birthMonth = birthDate.Month < 3 ? birthDate.Month + 12 : birthDate.Month;
            return (birthMonth >= DateTime.Now.Month && birthMonth <= someMonth && (birthDate.Month == DateTime.Now.Month ? birthDate.Day > DateTime.Now.Day : true)) ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    public class CountToVisibilityConverter : IValueConverter {
        public bool Invert { get; set; }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((int)value > 0) ^ Invert ? Visibility.Visible : Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class IntToDoubleConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return Convert.ToDouble(value);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return Convert.ToInt32(value);
        }
        #endregion
    }
    public class PriorityToImageConverter : MarkupExtension, IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return new Image() { Source = ((EnumMemberInfo)value).Image };
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class EditFormShowModeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return false;
            if(!Enum.IsDefined(typeof(EditFormShowMode), value))
                return false;
            EditFormShowMode mode = (EditFormShowMode)value;
            return !mode.Equals(EditFormShowMode.None);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class IncrementalSearchModeToBoolConverter : IValueConverter {     
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(!(value is IncrementalSearchMode))
                return false;
            if(((IncrementalSearchMode)value) == IncrementalSearchMode.Enabled)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(!(value is bool))
                return IncrementalSearchMode.Disabled;
            if((bool)value)
                return IncrementalSearchMode.Enabled;
            return IncrementalSearchMode.Disabled;          
        }      
    }
    public class Int32Extension : MarkupExtension {
        readonly int value;
        public Int32Extension(int value) {
            this.value = value;
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return value;
        }
    }
    public class TabItemEventArgsToDataConverter : EventArgsConverterBase<TabControlTabRemovedEventArgs> {
        protected override object Convert(object sender, TabControlTabRemovedEventArgs args) {
            return ((DXTabItem)args.Item).DataContext;
        }
    }
    public class FormatTypeConverter : BaseValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is FormatType))
                return null;
            switch((FormatType)value) {
                case FormatType.DateTimeToday:
                    return "YellowFillWithDarkYellowText";
                case FormatType.TopPercent:
                    return "GreenFillWithDarkGreenText";
                case FormatType.BottomPercent:
                default:
                    return "LightRedFillWithDarkRedText";
            }
        }
    }
    public class ConditionTypeConverter : BaseValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is ConditionType))
                return ConditionRule.None;
            switch((ConditionType)value) {
                case ConditionType.Greater: return ConditionRule.Greater;
                case ConditionType.Less:
                default:
                    return ConditionRule.Less;
            }
        }
    }
}
