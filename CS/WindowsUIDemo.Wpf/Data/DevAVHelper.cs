using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace WindowsUIDemo {
    public static class DevAVHelper {
        static List<DevExpress.DevAV.Employee> employees = null;
        internal static List<DevExpress.DevAV.Employee> Employees {
            get {
                if(employees == null) {
                    var devAvDb = new DevExpress.DevAV.DevAVDbContext();
                    devAvDb.Employees.Load();
                    employees = devAvDb.Employees.Local.ToList();
                }
                return employees;
            }
        }
        internal static DevExpress.DevAV.Employee GetEmployee(string email) {
            return Employees.SingleOrDefault(x => x.Email.Equals(email));
        }
    }
}
