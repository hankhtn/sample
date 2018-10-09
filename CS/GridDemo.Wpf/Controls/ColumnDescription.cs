namespace GridDemo {
    public class ColumnDescription {
        public ColumnDescription(string propertyName, string displayName = null) {
            PropertyName = propertyName;
            DisplayName = displayName ?? propertyName;
        }
        public string PropertyName { get; private set; }
        public string DisplayName { get; private set; }
    }
    public class BandDescription {
        public BandDescription(string displayName, ColumnDescription[] columns) {
            DisplayName = displayName;
            Columns = columns;
        }
        public string DisplayName { get; private set; }
        public ColumnDescription[] Columns { get; private set; }
    }
}
