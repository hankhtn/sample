using DevExpress.Xpf.DemoBase;
using PivotGridDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PivotGridDemo {
    public class ServerModeViewModel : ServerModeViewModelBase<OrderDataRecord> {
        #region DatabaseInfo
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(
            OrderDataContext.FileName, "OrderDataRecords", typeof(OrderDataRecord), CreateValues, (sql, connection) => new SQLiteCommand(sql, (SQLiteConnection)connection));
        static Dictionary<string, object> CreateValues() {
            var dict = new Dictionary<string, object>();
            var product = DatabaseHelper.GetProduct();
            dict.Add("OrderDate", DatabaseHelper.GetOrderDate());
            dict.Add("Quantity", DatabaseHelper.GetQuantity());
            dict.Add("UnitPrice", DatabaseHelper.GetProductPrice(product));
            dict.Add("CustomerName", DatabaseHelper.GetCustomerName());
            dict.Add("ProductName", product.ProductName);
            dict.Add("CategoryName", product.CategoryName);
            dict.Add("SalesPersonName", DatabaseHelper.GetSalesPersonName());
            return dict;
        }
        #endregion

        protected ServerModeViewModel()
            : base(DatabaseInfo, () => new OrderDataContext().OrderDataRecords) {
        }
    }
}
