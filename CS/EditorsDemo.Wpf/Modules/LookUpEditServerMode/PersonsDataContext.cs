using DevExpress.Xpf.DemoBase;
using System.Data.Entity;
using System.IO;

namespace GridDemo {
    public class PersonsDataContext : DbContext {
        public static readonly string FileName = Path.GetFullPath("PersonData.db");
        public PersonsDataContext()
            : base(SQLiteDataBaseGenerator.CreateConnection(LookUpInstantFeedbackModeViewModel.DatabaseInfo), true) {
        }
        public DbSet<Person> People { get; set; }
    }
    public class Person {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
