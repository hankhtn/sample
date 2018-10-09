using System.Collections.Generic;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class IntervalGrouping : PivotGridDemoModule {
        public class GroupIntervalItem {
            public FieldGroupInterval GroupInterval { get; set; }
            public string Caption { get; set; }
        }

        public IntervalGrouping() {
            InitializeComponent();
        }

        public static IEnumerable<object> FieldGroupIntervals {
            get {
                return new[] {
                    new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateMonth, Caption = "Month" },
                    new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateQuarter, Caption = "Quarter" },
                    new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateYear, Caption = "Year" },
                    new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateMonthYear, Caption = "Month-Year" },
                    new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateQuarterYear, Caption = "Quarter-Year" },
                    new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateDayOfWeek, Caption = "Day Of Week" },
                    new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateWeekOfMonth, Caption = "Week Of Month" },
                };
            }
        }

        void pivotGrid_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e) {
            if(object.ReferenceEquals(e.Field, fieldOrderDate) && e.Field.GroupInterval == FieldGroupInterval.DateQuarter) {
                e.DisplayText = string.Format("Qtr {0}", e.Value);
                if(e.ValueType == FieldValueType.Total) e.DisplayText += " Total";
            }
        }
    }
}
