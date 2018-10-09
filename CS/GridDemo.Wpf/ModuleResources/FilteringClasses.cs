using DevExpress.Data.Filtering;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using System;
using System.Windows.Data;

namespace GridDemo {
    public class DoubleToCriteriaOperatorConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            BinaryOperator op = value as BinaryOperator;
            if(object.ReferenceEquals(op, null))
                return 0d;
            OperandValue operandValue = op.RightOperand as OperandValue;
            return operandValue.Value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return new BinaryOperator("Quantity", Math.Round((double)value), BinaryOperatorType.Greater);
        }
        #endregion
    }

    public class FilteringViewModel : ViewModelBase {
        string _CurrentView = "Table View";
        ColumnFilterPopupMode _ColumnFilterPopupMode = ColumnFilterPopupMode.Excel;
        int _IncrementalSearchClearDelay = 1000;
        IncrementalSearchMode _IncrementalSearchMode = IncrementalSearchMode.Disabled;
        bool _AllowFilterEditor = true;
        bool _AllowColumnFiltering = true;
        
        public string CurrentView {
            get { return _CurrentView; }
            set { SetProperty(ref _CurrentView, value, "CurrentView"); }
        }
        public ColumnFilterPopupMode ColumnFilterPopupMode {
            get { return _ColumnFilterPopupMode; }
            set { SetProperty(ref _ColumnFilterPopupMode, value, "ColumnFilterPopupMode"); }
        }
        public int IncrementalSearchClearDelay {
            get { return _IncrementalSearchClearDelay; }
            set { SetProperty(ref _IncrementalSearchClearDelay, value, "IncrementalSearchClearDelay"); }
        }
        public IncrementalSearchMode IncrementalSearchMode {
            get { return _IncrementalSearchMode; }
            set { SetProperty(ref _IncrementalSearchMode, value, "IncrementalSearchMode"); }
        }
        public bool AllowFilterEditor {
            get { return _AllowFilterEditor; }
            set { SetProperty(ref _AllowFilterEditor, value, "AllowFilterEditor"); }
        }
        public bool AllowColumnFiltering {
            get { return _AllowColumnFiltering; }
            set { SetProperty(ref _AllowColumnFiltering, value, "AllowColumnFiltering"); }
        }

    }
}
