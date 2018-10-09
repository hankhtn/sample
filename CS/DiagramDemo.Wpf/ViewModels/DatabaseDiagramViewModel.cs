using DevExpress.Diagram.Demos;
using DevExpress.Mvvm;

namespace DiagramDemo {
    public class DatabaseDiagramViewModel : ViewModelBase {
        public DatabaseDefinition Database { get; private set; }

        public DatabaseDiagramViewModel() {
            Database = DatabaseData.GetDatabaseDefinition();
        }
    }
}
