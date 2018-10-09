using DevExpress.Xpf.DemoBase.DataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace GridDemo {
    public class SalesByEmployeeData {
        public List<ExpandoObject> Data { get; private set; }
        public BandDescription[] Bands { get; private set; }
        public ColumnDescription[] Columns { get; private set; }
        public SalesByEmployeeData() {
            GenerateBandsAndColumns();
            GenerateData();
        }

        void GenerateBandsAndColumns() {
            Bands = Enumerable.Range(0, 10)
                .Reverse()
                .Select(yearIndex => {
                    var yearBandName = (DateTime.Now.Year - yearIndex).ToString();
                    var quarters = Enumerable.Range(1, 4)
                        .Select(quarter => new ColumnDescription(yearBandName + "-Q" + quarter, "Q" + quarter))
                        .Concat(new[] { new ColumnDescription(yearBandName + "-YearTotal", "Year Total") })
                        .ToArray();
                    return new BandDescription(yearBandName, quarters);
                })
                .ToArray();
            Columns = Bands.SelectMany(x => x.Columns).ToArray();
        }

        void GenerateData() {
            Random random = new Random();
            Data = new List<ExpandoObject>();
            foreach(var employee in EmployeesWithPhotoData.DataSource) {
                IDictionary<string, object> row = new ExpandoObject();
                row["Employee"] = employee.FirstName + " " + employee.LastName;
                row["GroupName"] = employee.GroupName;

                var total = 0d;
                foreach(var band in Bands) {
                    var yearTotal = 0d;
                    foreach(var column in band.Columns.Take(band.Columns.Length - 1)) {
                        var value = random.Next(100000);
                        yearTotal += value;
                        total += value;
                        row[column.PropertyName] = value;
                    }
                    row[band.Columns.Last().PropertyName] = yearTotal;
                }

                row["Total"] = total;
                Data.Add((ExpandoObject)row);
            }
        }
    }
}
