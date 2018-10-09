using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridDemo {
    public static class VirtualDataSourceValuesProvider {
        public const int UniqueTypesCount = 5;
        public const int DateTimePropertyOffset = 2;
        public const int MinIntValue = 0;
        public const int MaxIntValue = 1000;

        static Random GetRandom(int rowIndex, int columnIndex) { return new Random(rowIndex + columnIndex); }

        static UnboundSourceEnum GetEnumValue(int value) {
            return (UnboundSourceEnum)value;
        }

        public static object GetValue(int rowIndex, int columnIndex) {
            switch(columnIndex % UniqueTypesCount) {
                case 0: return GetRandom(rowIndex, columnIndex).Next(MinIntValue, MaxIntValue);
                case 1: return String.Format("String {0}", GetRandom(rowIndex, columnIndex).Next(0, 100));
                case 2: return DateTime.Now.AddDays(GetRandom(rowIndex, columnIndex).Next(-100, 1));
                case 3: return GetRandom(rowIndex, columnIndex).Next(0, 2) == 0;
                case 4: return GetEnumValue(GetRandom(rowIndex, columnIndex).Next(0, 7));
                default: return null;
            }
        }
        public static Type GetColumnType(int columnIndex) {
            switch(columnIndex % UniqueTypesCount) {
                case 0: return typeof(int);
                case 1: return typeof(string);
                case 2: return typeof(DateTime);
                case 3: return typeof(bool);
                case 4: return typeof(UnboundSourceEnum);
                default: return null;
            }
        }
        public static string GetColumnName(int columnIndex) {
            switch(columnIndex % UniqueTypesCount) {
                case 0: return String.Format("Int ({0})", columnIndex);
                case 1: return String.Format("Text ({0})", columnIndex);
                case 2: return String.Format("DateTime ({0})", columnIndex);
                case 3: return String.Format("Boolean ({0})", columnIndex);
                case 4: return String.Format("Enum ({0})", columnIndex);
                default: return null;
            }
        }
    }

    public enum UnboundSourceEnum { Value0, Value1, Value2, Value3, Value4, Value5, Value6 }
}
