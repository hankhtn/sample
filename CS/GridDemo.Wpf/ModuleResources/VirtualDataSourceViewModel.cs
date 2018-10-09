using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using DevExpress.Data;
using Point = System.Drawing.Point;

namespace GridDemo {
    [POCOViewModel]
    public class VirtualDataSourceViewModel {
        readonly Dictionary<Point, object> valuesCache = new Dictionary<Point, object>();

        public VirtualDataSourceViewModel() {
            FormatConditions = CreateFormatConditions();
        }
        
        public FormatConditionModel[] FormatConditions { get; private set; }

        FormatConditionModel[] CreateFormatConditions() {
            int uniqueColumnsCount = VirtualDataSource.ColumnsCount / VirtualDataSourceValuesProvider.UniqueTypesCount;
            int intDelta = VirtualDataSourceValuesProvider.MaxIntValue - VirtualDataSourceValuesProvider.MinIntValue;
            int intGreaterValue = (int)(intDelta * 0.95);
            int intLessValue = (int)(intDelta * 0.05);
            DateTime yesterday = DateTime.Now.AddDays(-1);
            FormatConditionModel[] formatConditions = new FormatConditionModel[uniqueColumnsCount * 3];
            for(int i = 0; i < uniqueColumnsCount; i++) {
                string intColumnName = String.Format("Int ({0})", i * VirtualDataSourceValuesProvider.UniqueTypesCount);
                formatConditions[i] = CreateFormatCondition(intColumnName, FormatType.TopPercent, ConditionType.Greater, intGreaterValue);
                formatConditions[uniqueColumnsCount + i] = CreateFormatCondition(intColumnName, FormatType.BottomPercent, ConditionType.Less, intLessValue);
                string dateTimeColumnName = String.Format("DateTime ({0})", i * VirtualDataSourceValuesProvider.UniqueTypesCount + VirtualDataSourceValuesProvider.DateTimePropertyOffset);
                formatConditions[uniqueColumnsCount * 2 + i] = CreateFormatCondition(dateTimeColumnName, FormatType.DateTimeToday, ConditionType.Greater, yesterday);
            }
            return formatConditions;
        }

        FormatConditionModel CreateFormatCondition(string fieldName, FormatType formatType, ConditionType conditionType, object value) {
            FormatConditionModel formatCondition = new FormatConditionModel();
            formatCondition.FormatType = formatType;
            formatCondition.PropertyName = fieldName;
            formatCondition.ConditionType = conditionType;
            formatCondition.Value = value;
            return formatCondition;
        }

        public object GetValue(int rowIndex, int columnIndex) {
            object value = null;
            if(valuesCache.TryGetValue(new Point(rowIndex, columnIndex), out value))
                return value;
            return VirtualDataSourceValuesProvider.GetValue(rowIndex, columnIndex);
        }
        public void SetValue(int rowIndex, int columnIndex, object value) {
            var key = new Point(rowIndex, columnIndex);
            if(valuesCache.ContainsKey(key))
                valuesCache[key] = value;
            else
                valuesCache.Add(key, value);
        }
        public VirtualDataSourceProperty CreateProperty(int index) {
            return new VirtualDataSourceProperty(VirtualDataSourceValuesProvider.GetColumnName(index), VirtualDataSourceValuesProvider.GetColumnType(index));
        }
    }

    public class FormatConditionModel {
        public string PropertyName { get; set; }
        public FormatType FormatType { get; set; }
        public ConditionType ConditionType { get; set; }
        public object Value { get; set; }
    }
    public class VirtualDataSourceProperty {
        public VirtualDataSourceProperty(string name, Type type) {
            Name = name;
            Type = type;
        }
        public string Name { get; private set; }
        public Type Type { get; private set; }
    }

    public enum FormatType {
        DateTimeToday,
        TopPercent,
        BottomPercent,
    }
    public enum ConditionType {
        Greater,
        Less,
    }
}
