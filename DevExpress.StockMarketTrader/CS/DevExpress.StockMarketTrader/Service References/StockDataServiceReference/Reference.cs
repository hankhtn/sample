









namespace DevExpress.StockMarketTrader.StockDataServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StockData", Namespace="http://schemas.datacontract.org/2004/07/DevExpress.StockMarketTrader.Web.Model", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class StockData : DevExpress.StockMarketTrader.StockDataServiceReference.EntityObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal ClosePField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CompanyIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal HighPField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal LowPField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal OpenPField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int VolumneField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal CloseP {
            get {
                return this.ClosePField;
            }
            set {
                if ((this.ClosePField.Equals(value) != true)) {
                    this.ClosePField = value;
                    this.RaisePropertyChanged("CloseP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CompanyID {
            get {
                return this.CompanyIDField;
            }
            set {
                if ((this.CompanyIDField.Equals(value) != true)) {
                    this.CompanyIDField = value;
                    this.RaisePropertyChanged("CompanyID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal HighP {
            get {
                return this.HighPField;
            }
            set {
                if ((this.HighPField.Equals(value) != true)) {
                    this.HighPField = value;
                    this.RaisePropertyChanged("HighP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal LowP {
            get {
                return this.LowPField;
            }
            set {
                if ((this.LowPField.Equals(value) != true)) {
                    this.LowPField = value;
                    this.RaisePropertyChanged("LowP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OpenP {
            get {
                return this.OpenPField;
            }
            set {
                if ((this.OpenPField.Equals(value) != true)) {
                    this.OpenPField = value;
                    this.RaisePropertyChanged("OpenP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Volumne {
            get {
                return this.VolumneField;
            }
            set {
                if ((this.VolumneField.Equals(value) != true)) {
                    this.VolumneField = value;
                    this.RaisePropertyChanged("Volumne");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StructuralObject", Namespace="http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.EntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.StockData))]
    public partial class StructuralObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityObject", Namespace="http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.StockData))]
    public partial class EntityObject : DevExpress.StockMarketTrader.StockDataServiceReference.StructuralObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DevExpress.StockMarketTrader.StockDataServiceReference.EntityKey EntityKeyField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public DevExpress.StockMarketTrader.StockDataServiceReference.EntityKey EntityKey {
            get {
                return this.EntityKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityKeyField, value) != true)) {
                    this.EntityKeyField = value;
                    this.RaisePropertyChanged("EntityKey");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityKey", Namespace="http://schemas.datacontract.org/2004/07/System.Data", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class EntityKey : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EntityContainerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DevExpress.StockMarketTrader.StockDataServiceReference.EntityKeyMember[] EntityKeyValuesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EntitySetNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EntityContainerName {
            get {
                return this.EntityContainerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityContainerNameField, value) != true)) {
                    this.EntityContainerNameField = value;
                    this.RaisePropertyChanged("EntityContainerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public DevExpress.StockMarketTrader.StockDataServiceReference.EntityKeyMember[] EntityKeyValues {
            get {
                return this.EntityKeyValuesField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityKeyValuesField, value) != true)) {
                    this.EntityKeyValuesField = value;
                    this.RaisePropertyChanged("EntityKeyValues");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EntitySetName {
            get {
                return this.EntitySetNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EntitySetNameField, value) != true)) {
                    this.EntitySetNameField = value;
                    this.RaisePropertyChanged("EntitySetName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityKeyMember", Namespace="http://schemas.datacontract.org/2004/07/System.Data")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.EntityKey))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.EntityKeyMember[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.StockData[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.StockData))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.EntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.StructuralObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.DateTime[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(string[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(int[]))]
    public partial class EntityKeyMember : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key {
            get {
                return this.KeyField;
            }
            set {
                if ((object.ReferenceEquals(this.KeyField, value) != true)) {
                    this.KeyField = value;
                    this.RaisePropertyChanged("Key");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TopRatedCompanyData", Namespace="http://StockDataService/service/")]
    [System.SerializableAttribute()]
    public partial class TopRatedCompanyData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompanyNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double NewPriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double OldPriceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyName {
            get {
                return this.CompanyNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CompanyNameField, value) != true)) {
                    this.CompanyNameField = value;
                    this.RaisePropertyChanged("CompanyName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double NewPrice {
            get {
                return this.NewPriceField;
            }
            set {
                if ((this.NewPriceField.Equals(value) != true)) {
                    this.NewPriceField = value;
                    this.RaisePropertyChanged("NewPrice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double OldPrice {
            get {
                return this.OldPriceField;
            }
            set {
                if ((this.OldPriceField.Equals(value) != true)) {
                    this.OldPriceField = value;
                    this.RaisePropertyChanged("OldPrice");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompaniesVolumeData", Namespace="http://StockDataService/service/")]
    [System.SerializableAttribute()]
    public partial class CompaniesVolumeData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompanyNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int VolumeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyName {
            get {
                return this.CompanyNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CompanyNameField, value) != true)) {
                    this.CompanyNameField = value;
                    this.RaisePropertyChanged("CompanyName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Volume {
            get {
                return this.VolumeField;
            }
            set {
                if ((this.VolumeField.Equals(value) != true)) {
                    this.VolumeField = value;
                    this.RaisePropertyChanged("Volume");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompanyData", Namespace="http://StockDataService/service/")]
    [System.SerializableAttribute()]
    public partial class CompanyData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ClosePriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DevExpress.StockMarketTrader.StockDataServiceReference.StockData DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HighPriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LowPriceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double ClosePrice {
            get {
                return this.ClosePriceField;
            }
            set {
                if ((this.ClosePriceField.Equals(value) != true)) {
                    this.ClosePriceField = value;
                    this.RaisePropertyChanged("ClosePrice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double HighPrice {
            get {
                return this.HighPriceField;
            }
            set {
                if ((this.HighPriceField.Equals(value) != true)) {
                    this.HighPriceField = value;
                    this.RaisePropertyChanged("HighPrice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LowPrice {
            get {
                return this.LowPriceField;
            }
            set {
                if ((this.LowPriceField.Equals(value) != true)) {
                    this.LowPriceField = value;
                    this.RaisePropertyChanged("LowPrice");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompanyStockData", Namespace="http://StockDataService/service/")]
    [System.SerializableAttribute()]
    public partial class CompanyStockData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompanyNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] DataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyName {
            get {
                return this.CompanyNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CompanyNameField, value) != true)) {
                    this.CompanyNameField = value;
                    this.RaisePropertyChanged("CompanyName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://StockDataService/service/", ConfigurationName="StockDataServiceReference.IStockDataService")]
    public interface IStockDataService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/Test", ReplyAction="http://StockDataService/service/IStockDataService/TestResponse")]
        string Test();
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/Test", ReplyAction="http://StockDataService/service/IStockDataService/TestResponse")]
        System.IAsyncResult BeginTest(System.AsyncCallback callback, object asyncState);
        
        string EndTest(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/Initialize", ReplyAction="http://StockDataService/service/IStockDataService/InitializeResponse")]
        void Initialize();
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/Initialize", ReplyAction="http://StockDataService/service/IStockDataService/InitializeResponse")]
        System.IAsyncResult BeginInitialize(System.AsyncCallback callback, object asyncState);
        
        void EndInitialize(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetAllPeriodData", ReplyAction="http://StockDataService/service/IStockDataService/GetAllPeriodDataResponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] GetAllPeriodData(string companyName);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetAllPeriodData", ReplyAction="http://StockDataService/service/IStockDataService/GetAllPeriodDataResponse")]
        System.IAsyncResult BeginGetAllPeriodData(string companyName, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] EndGetAllPeriodData(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetDates", ReplyAction="http://StockDataService/service/IStockDataService/GetDatesResponse")]
        System.DateTime[] GetDates();
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetDates", ReplyAction="http://StockDataService/service/IStockDataService/GetDatesResponse")]
        System.IAsyncResult BeginGetDates(System.AsyncCallback callback, object asyncState);
        
        System.DateTime[] EndGetDates(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetStockDataByDate", ReplyAction="http://StockDataService/service/IStockDataService/GetStockDataByDateResponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] GetStockDataByDate(System.DateTime currentDate);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetStockDataByDate", ReplyAction="http://StockDataService/service/IStockDataService/GetStockDataByDateResponse")]
        System.IAsyncResult BeginGetStockDataByDate(System.DateTime currentDate, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] EndGetStockDataByDate(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetCompaniesName", ReplyAction="http://StockDataService/service/IStockDataService/GetCompaniesNameResponse")]
        string[] GetCompaniesName();
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetCompaniesName", ReplyAction="http://StockDataService/service/IStockDataService/GetCompaniesNameResponse")]
        System.IAsyncResult BeginGetCompaniesName(System.AsyncCallback callback, object asyncState);
        
        string[] EndGetCompaniesName(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetCompanyStockData", ReplyAction="http://StockDataService/service/IStockDataService/GetCompanyStockDataResponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] GetCompanyStockData(System.DateTime date, string companyName);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetCompanyStockData", ReplyAction="http://StockDataService/service/IStockDataService/GetCompanyStockDataResponse")]
        System.IAsyncResult BeginGetCompanyStockData(System.DateTime date, string companyName, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] EndGetCompanyStockData(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetCompanyStockDataSL", ReplyAction="http://StockDataService/service/IStockDataService/GetCompanyStockDataSLResponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] GetCompanyStockDataSL(System.DateTime newDate, System.DateTime oldDate, string companyName);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetCompanyStockDataSL", ReplyAction="http://StockDataService/service/IStockDataService/GetCompanyStockDataSLResponse")]
        System.IAsyncResult BeginGetCompanyStockDataSL(System.DateTime newDate, System.DateTime oldDate, string companyName, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] EndGetCompanyStockDataSL(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetHighestPrice", ReplyAction="http://StockDataService/service/IStockDataService/GetHighestPriceResponse")]
        double GetHighestPrice(string companyName, System.DateTime start, System.DateTime end);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetHighestPrice", ReplyAction="http://StockDataService/service/IStockDataService/GetHighestPriceResponse")]
        System.IAsyncResult BeginGetHighestPrice(string companyName, System.DateTime start, System.DateTime end, System.AsyncCallback callback, object asyncState);
        
        double EndGetHighestPrice(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetLowestPrice", ReplyAction="http://StockDataService/service/IStockDataService/GetLowestPriceResponse")]
        double GetLowestPrice(string companyName, System.DateTime start, System.DateTime end);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetLowestPrice", ReplyAction="http://StockDataService/service/IStockDataService/GetLowestPriceResponse")]
        System.IAsyncResult BeginGetLowestPrice(string companyName, System.DateTime start, System.DateTime end, System.AsyncCallback callback, object asyncState);
        
        double EndGetLowestPrice(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetTopRatedCompanyIDs", ReplyAction="http://StockDataService/service/IStockDataService/GetTopRatedCompanyIDsResponse")]
        int[] GetTopRatedCompanyIDs(System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetTopRatedCompanyIDs", ReplyAction="http://StockDataService/service/IStockDataService/GetTopRatedCompanyIDsResponse")]
        System.IAsyncResult BeginGetTopRatedCompanyIDs(System.DateTime date, System.AsyncCallback callback, object asyncState);
        
        int[] EndGetTopRatedCompanyIDs(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetTopRatedCompaniesDataSL", ReplyAction="http://StockDataService/service/IStockDataService/GetTopRatedCompaniesDataSLRespo" +
            "nse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData[] GetTopRatedCompaniesDataSL(System.DateTime start, System.DateTime end, string selectedCompany);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetTopRatedCompaniesDataSL", ReplyAction="http://StockDataService/service/IStockDataService/GetTopRatedCompaniesDataSLRespo" +
            "nse")]
        System.IAsyncResult BeginGetTopRatedCompaniesDataSL(System.DateTime start, System.DateTime end, string selectedCompany, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData[] EndGetTopRatedCompaniesDataSL(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetCompaniesVolumeFromPeriod", ReplyAction="http://StockDataService/service/IStockDataService/GetCompaniesVolumeFromPeriodRes" +
            "ponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData[] GetCompaniesVolumeFromPeriod(System.DateTime start, System.DateTime end);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetCompaniesVolumeFromPeriod", ReplyAction="http://StockDataService/service/IStockDataService/GetCompaniesVolumeFromPeriodRes" +
            "ponse")]
        System.IAsyncResult BeginGetCompaniesVolumeFromPeriod(System.DateTime start, System.DateTime end, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData[] EndGetCompaniesVolumeFromPeriod(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetCompanyData", ReplyAction="http://StockDataService/service/IStockDataService/GetCompanyDataResponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData GetCompanyData(System.DateTime newDate, System.DateTime oldDate, string companyName);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetCompanyData", ReplyAction="http://StockDataService/service/IStockDataService/GetCompanyDataResponse")]
        System.IAsyncResult BeginGetCompanyData(System.DateTime newDate, System.DateTime oldDate, string companyName, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData EndGetCompanyData(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetCompanyMultipleDataFromPerio" +
            "d", ReplyAction="http://StockDataService/service/IStockDataService/GetCompanyMultipleDataFromPerio" +
            "dResponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData[] GetCompanyMultipleDataFromPeriod(int currentDate, int count, int periodSize, string companyName);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetCompanyMultipleDataFromPerio" +
            "d", ReplyAction="http://StockDataService/service/IStockDataService/GetCompanyMultipleDataFromPerio" +
            "dResponse")]
        System.IAsyncResult BeginGetCompanyMultipleDataFromPeriod(int currentDate, int count, int periodSize, string companyName, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData[] EndGetCompanyMultipleDataFromPeriod(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetStockDataFromPeriodByCompany" +
            "List", ReplyAction="http://StockDataService/service/IStockDataService/GetStockDataFromPeriodByCompany" +
            "ListResponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] GetStockDataFromPeriodByCompanyList(int currentDate, int count, int periodSize, string[] companies);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetStockDataFromPeriodByCompany" +
            "List", ReplyAction="http://StockDataService/service/IStockDataService/GetStockDataFromPeriodByCompany" +
            "ListResponse")]
        System.IAsyncResult BeginGetStockDataFromPeriodByCompanyList(int currentDate, int count, int periodSize, string[] companies, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] EndGetStockDataFromPeriodByCompanyList(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://StockDataService/service/IStockDataService/GetStockDataFromDateByCompanyLi" +
            "st", ReplyAction="http://StockDataService/service/IStockDataService/GetStockDataFromDateByCompanyLi" +
            "stResponse")]
        DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] GetStockDataFromDateByCompanyList(System.DateTime date, string[] companies);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://StockDataService/service/IStockDataService/GetStockDataFromDateByCompanyLi" +
            "st", ReplyAction="http://StockDataService/service/IStockDataService/GetStockDataFromDateByCompanyLi" +
            "stResponse")]
        System.IAsyncResult BeginGetStockDataFromDateByCompanyList(System.DateTime date, string[] companies, System.AsyncCallback callback, object asyncState);
        
        DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] EndGetStockDataFromDateByCompanyList(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStockDataServiceChannel : DevExpress.StockMarketTrader.StockDataServiceReference.IStockDataService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public TestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetAllPeriodDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetAllPeriodDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.StockData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetDatesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetDatesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.DateTime[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.DateTime[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetStockDataByDateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetStockDataByDateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.StockData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetCompaniesNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetCompaniesNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetCompanyStockDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetCompanyStockDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.StockData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetCompanyStockDataSLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetCompanyStockDataSLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.StockData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetHighestPriceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetHighestPriceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public double Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((double)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetLowestPriceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetLowestPriceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public double Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((double)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetTopRatedCompanyIDsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetTopRatedCompanyIDsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public int[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((int[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetTopRatedCompaniesDataSLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetTopRatedCompaniesDataSLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetCompaniesVolumeFromPeriodCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetCompaniesVolumeFromPeriodCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetCompanyDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetCompanyDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetCompanyMultipleDataFromPeriodCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetCompanyMultipleDataFromPeriodCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetStockDataFromPeriodByCompanyListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetStockDataFromPeriodByCompanyListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetStockDataFromDateByCompanyListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetStockDataFromDateByCompanyListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StockDataServiceClient : System.ServiceModel.ClientBase<DevExpress.StockMarketTrader.StockDataServiceReference.IStockDataService>, DevExpress.StockMarketTrader.StockDataServiceReference.IStockDataService {
        
        private BeginOperationDelegate onBeginTestDelegate;
        
        private EndOperationDelegate onEndTestDelegate;
        
        private System.Threading.SendOrPostCallback onTestCompletedDelegate;
        
        private BeginOperationDelegate onBeginInitializeDelegate;
        
        private EndOperationDelegate onEndInitializeDelegate;
        
        private System.Threading.SendOrPostCallback onInitializeCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetAllPeriodDataDelegate;
        
        private EndOperationDelegate onEndGetAllPeriodDataDelegate;
        
        private System.Threading.SendOrPostCallback onGetAllPeriodDataCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetDatesDelegate;
        
        private EndOperationDelegate onEndGetDatesDelegate;
        
        private System.Threading.SendOrPostCallback onGetDatesCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetStockDataByDateDelegate;
        
        private EndOperationDelegate onEndGetStockDataByDateDelegate;
        
        private System.Threading.SendOrPostCallback onGetStockDataByDateCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetCompaniesNameDelegate;
        
        private EndOperationDelegate onEndGetCompaniesNameDelegate;
        
        private System.Threading.SendOrPostCallback onGetCompaniesNameCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetCompanyStockDataDelegate;
        
        private EndOperationDelegate onEndGetCompanyStockDataDelegate;
        
        private System.Threading.SendOrPostCallback onGetCompanyStockDataCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetCompanyStockDataSLDelegate;
        
        private EndOperationDelegate onEndGetCompanyStockDataSLDelegate;
        
        private System.Threading.SendOrPostCallback onGetCompanyStockDataSLCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetHighestPriceDelegate;
        
        private EndOperationDelegate onEndGetHighestPriceDelegate;
        
        private System.Threading.SendOrPostCallback onGetHighestPriceCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetLowestPriceDelegate;
        
        private EndOperationDelegate onEndGetLowestPriceDelegate;
        
        private System.Threading.SendOrPostCallback onGetLowestPriceCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetTopRatedCompanyIDsDelegate;
        
        private EndOperationDelegate onEndGetTopRatedCompanyIDsDelegate;
        
        private System.Threading.SendOrPostCallback onGetTopRatedCompanyIDsCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetTopRatedCompaniesDataSLDelegate;
        
        private EndOperationDelegate onEndGetTopRatedCompaniesDataSLDelegate;
        
        private System.Threading.SendOrPostCallback onGetTopRatedCompaniesDataSLCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetCompaniesVolumeFromPeriodDelegate;
        
        private EndOperationDelegate onEndGetCompaniesVolumeFromPeriodDelegate;
        
        private System.Threading.SendOrPostCallback onGetCompaniesVolumeFromPeriodCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetCompanyDataDelegate;
        
        private EndOperationDelegate onEndGetCompanyDataDelegate;
        
        private System.Threading.SendOrPostCallback onGetCompanyDataCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetCompanyMultipleDataFromPeriodDelegate;
        
        private EndOperationDelegate onEndGetCompanyMultipleDataFromPeriodDelegate;
        
        private System.Threading.SendOrPostCallback onGetCompanyMultipleDataFromPeriodCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetStockDataFromPeriodByCompanyListDelegate;
        
        private EndOperationDelegate onEndGetStockDataFromPeriodByCompanyListDelegate;
        
        private System.Threading.SendOrPostCallback onGetStockDataFromPeriodByCompanyListCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetStockDataFromDateByCompanyListDelegate;
        
        private EndOperationDelegate onEndGetStockDataFromDateByCompanyListDelegate;
        
        private System.Threading.SendOrPostCallback onGetStockDataFromDateByCompanyListCompletedDelegate;
        
        public StockDataServiceClient() {
        }
        
        public StockDataServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StockDataServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockDataServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockDataServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<TestCompletedEventArgs> TestCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> InitializeCompleted;
        
        public event System.EventHandler<GetAllPeriodDataCompletedEventArgs> GetAllPeriodDataCompleted;
        
        public event System.EventHandler<GetDatesCompletedEventArgs> GetDatesCompleted;
        
        public event System.EventHandler<GetStockDataByDateCompletedEventArgs> GetStockDataByDateCompleted;
        
        public event System.EventHandler<GetCompaniesNameCompletedEventArgs> GetCompaniesNameCompleted;
        
        public event System.EventHandler<GetCompanyStockDataCompletedEventArgs> GetCompanyStockDataCompleted;
        
        public event System.EventHandler<GetCompanyStockDataSLCompletedEventArgs> GetCompanyStockDataSLCompleted;
        
        public event System.EventHandler<GetHighestPriceCompletedEventArgs> GetHighestPriceCompleted;
        
        public event System.EventHandler<GetLowestPriceCompletedEventArgs> GetLowestPriceCompleted;
        
        public event System.EventHandler<GetTopRatedCompanyIDsCompletedEventArgs> GetTopRatedCompanyIDsCompleted;
        
        public event System.EventHandler<GetTopRatedCompaniesDataSLCompletedEventArgs> GetTopRatedCompaniesDataSLCompleted;
        
        public event System.EventHandler<GetCompaniesVolumeFromPeriodCompletedEventArgs> GetCompaniesVolumeFromPeriodCompleted;
        
        public event System.EventHandler<GetCompanyDataCompletedEventArgs> GetCompanyDataCompleted;
        
        public event System.EventHandler<GetCompanyMultipleDataFromPeriodCompletedEventArgs> GetCompanyMultipleDataFromPeriodCompleted;
        
        public event System.EventHandler<GetStockDataFromPeriodByCompanyListCompletedEventArgs> GetStockDataFromPeriodByCompanyListCompleted;
        
        public event System.EventHandler<GetStockDataFromDateByCompanyListCompletedEventArgs> GetStockDataFromDateByCompanyListCompleted;
        
        public string Test() {
            return base.Channel.Test();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginTest(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginTest(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndTest(System.IAsyncResult result) {
            return base.Channel.EndTest(result);
        }
        
        private System.IAsyncResult OnBeginTest(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return this.BeginTest(callback, asyncState);
        }
        
        private object[] OnEndTest(System.IAsyncResult result) {
            string retVal = this.EndTest(result);
            return new object[] {
                    retVal};
        }
        
        private void OnTestCompleted(object state) {
            if ((this.TestCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.TestCompleted(this, new TestCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void TestAsync() {
            this.TestAsync(null);
        }
        
        public void TestAsync(object userState) {
            if ((this.onBeginTestDelegate == null)) {
                this.onBeginTestDelegate = new BeginOperationDelegate(this.OnBeginTest);
            }
            if ((this.onEndTestDelegate == null)) {
                this.onEndTestDelegate = new EndOperationDelegate(this.OnEndTest);
            }
            if ((this.onTestCompletedDelegate == null)) {
                this.onTestCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnTestCompleted);
            }
            base.InvokeAsync(this.onBeginTestDelegate, null, this.onEndTestDelegate, this.onTestCompletedDelegate, userState);
        }
        
        public void Initialize() {
            base.Channel.Initialize();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginInitialize(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginInitialize(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndInitialize(System.IAsyncResult result) {
            base.Channel.EndInitialize(result);
        }
        
        private System.IAsyncResult OnBeginInitialize(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return this.BeginInitialize(callback, asyncState);
        }
        
        private object[] OnEndInitialize(System.IAsyncResult result) {
            this.EndInitialize(result);
            return null;
        }
        
        private void OnInitializeCompleted(object state) {
            if ((this.InitializeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.InitializeCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void InitializeAsync() {
            this.InitializeAsync(null);
        }
        
        public void InitializeAsync(object userState) {
            if ((this.onBeginInitializeDelegate == null)) {
                this.onBeginInitializeDelegate = new BeginOperationDelegate(this.OnBeginInitialize);
            }
            if ((this.onEndInitializeDelegate == null)) {
                this.onEndInitializeDelegate = new EndOperationDelegate(this.OnEndInitialize);
            }
            if ((this.onInitializeCompletedDelegate == null)) {
                this.onInitializeCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnInitializeCompleted);
            }
            base.InvokeAsync(this.onBeginInitializeDelegate, null, this.onEndInitializeDelegate, this.onInitializeCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] GetAllPeriodData(string companyName) {
            return base.Channel.GetAllPeriodData(companyName);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetAllPeriodData(string companyName, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetAllPeriodData(companyName, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] EndGetAllPeriodData(System.IAsyncResult result) {
            return base.Channel.EndGetAllPeriodData(result);
        }
        
        private System.IAsyncResult OnBeginGetAllPeriodData(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string companyName = ((string)(inValues[0]));
            return this.BeginGetAllPeriodData(companyName, callback, asyncState);
        }
        
        private object[] OnEndGetAllPeriodData(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] retVal = this.EndGetAllPeriodData(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetAllPeriodDataCompleted(object state) {
            if ((this.GetAllPeriodDataCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetAllPeriodDataCompleted(this, new GetAllPeriodDataCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetAllPeriodDataAsync(string companyName) {
            this.GetAllPeriodDataAsync(companyName, null);
        }
        
        public void GetAllPeriodDataAsync(string companyName, object userState) {
            if ((this.onBeginGetAllPeriodDataDelegate == null)) {
                this.onBeginGetAllPeriodDataDelegate = new BeginOperationDelegate(this.OnBeginGetAllPeriodData);
            }
            if ((this.onEndGetAllPeriodDataDelegate == null)) {
                this.onEndGetAllPeriodDataDelegate = new EndOperationDelegate(this.OnEndGetAllPeriodData);
            }
            if ((this.onGetAllPeriodDataCompletedDelegate == null)) {
                this.onGetAllPeriodDataCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetAllPeriodDataCompleted);
            }
            base.InvokeAsync(this.onBeginGetAllPeriodDataDelegate, new object[] {
                        companyName}, this.onEndGetAllPeriodDataDelegate, this.onGetAllPeriodDataCompletedDelegate, userState);
        }
        
        public System.DateTime[] GetDates() {
            return base.Channel.GetDates();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetDates(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetDates(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.DateTime[] EndGetDates(System.IAsyncResult result) {
            return base.Channel.EndGetDates(result);
        }
        
        private System.IAsyncResult OnBeginGetDates(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return this.BeginGetDates(callback, asyncState);
        }
        
        private object[] OnEndGetDates(System.IAsyncResult result) {
            System.DateTime[] retVal = this.EndGetDates(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetDatesCompleted(object state) {
            if ((this.GetDatesCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetDatesCompleted(this, new GetDatesCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetDatesAsync() {
            this.GetDatesAsync(null);
        }
        
        public void GetDatesAsync(object userState) {
            if ((this.onBeginGetDatesDelegate == null)) {
                this.onBeginGetDatesDelegate = new BeginOperationDelegate(this.OnBeginGetDates);
            }
            if ((this.onEndGetDatesDelegate == null)) {
                this.onEndGetDatesDelegate = new EndOperationDelegate(this.OnEndGetDates);
            }
            if ((this.onGetDatesCompletedDelegate == null)) {
                this.onGetDatesCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetDatesCompleted);
            }
            base.InvokeAsync(this.onBeginGetDatesDelegate, null, this.onEndGetDatesDelegate, this.onGetDatesCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] GetStockDataByDate(System.DateTime currentDate) {
            return base.Channel.GetStockDataByDate(currentDate);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetStockDataByDate(System.DateTime currentDate, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetStockDataByDate(currentDate, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] EndGetStockDataByDate(System.IAsyncResult result) {
            return base.Channel.EndGetStockDataByDate(result);
        }
        
        private System.IAsyncResult OnBeginGetStockDataByDate(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime currentDate = ((System.DateTime)(inValues[0]));
            return this.BeginGetStockDataByDate(currentDate, callback, asyncState);
        }
        
        private object[] OnEndGetStockDataByDate(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] retVal = this.EndGetStockDataByDate(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetStockDataByDateCompleted(object state) {
            if ((this.GetStockDataByDateCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetStockDataByDateCompleted(this, new GetStockDataByDateCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetStockDataByDateAsync(System.DateTime currentDate) {
            this.GetStockDataByDateAsync(currentDate, null);
        }
        
        public void GetStockDataByDateAsync(System.DateTime currentDate, object userState) {
            if ((this.onBeginGetStockDataByDateDelegate == null)) {
                this.onBeginGetStockDataByDateDelegate = new BeginOperationDelegate(this.OnBeginGetStockDataByDate);
            }
            if ((this.onEndGetStockDataByDateDelegate == null)) {
                this.onEndGetStockDataByDateDelegate = new EndOperationDelegate(this.OnEndGetStockDataByDate);
            }
            if ((this.onGetStockDataByDateCompletedDelegate == null)) {
                this.onGetStockDataByDateCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetStockDataByDateCompleted);
            }
            base.InvokeAsync(this.onBeginGetStockDataByDateDelegate, new object[] {
                        currentDate}, this.onEndGetStockDataByDateDelegate, this.onGetStockDataByDateCompletedDelegate, userState);
        }
        
        public string[] GetCompaniesName() {
            return base.Channel.GetCompaniesName();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetCompaniesName(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetCompaniesName(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string[] EndGetCompaniesName(System.IAsyncResult result) {
            return base.Channel.EndGetCompaniesName(result);
        }
        
        private System.IAsyncResult OnBeginGetCompaniesName(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return this.BeginGetCompaniesName(callback, asyncState);
        }
        
        private object[] OnEndGetCompaniesName(System.IAsyncResult result) {
            string[] retVal = this.EndGetCompaniesName(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetCompaniesNameCompleted(object state) {
            if ((this.GetCompaniesNameCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetCompaniesNameCompleted(this, new GetCompaniesNameCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetCompaniesNameAsync() {
            this.GetCompaniesNameAsync(null);
        }
        
        public void GetCompaniesNameAsync(object userState) {
            if ((this.onBeginGetCompaniesNameDelegate == null)) {
                this.onBeginGetCompaniesNameDelegate = new BeginOperationDelegate(this.OnBeginGetCompaniesName);
            }
            if ((this.onEndGetCompaniesNameDelegate == null)) {
                this.onEndGetCompaniesNameDelegate = new EndOperationDelegate(this.OnEndGetCompaniesName);
            }
            if ((this.onGetCompaniesNameCompletedDelegate == null)) {
                this.onGetCompaniesNameCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetCompaniesNameCompleted);
            }
            base.InvokeAsync(this.onBeginGetCompaniesNameDelegate, null, this.onEndGetCompaniesNameDelegate, this.onGetCompaniesNameCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] GetCompanyStockData(System.DateTime date, string companyName) {
            return base.Channel.GetCompanyStockData(date, companyName);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetCompanyStockData(System.DateTime date, string companyName, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetCompanyStockData(date, companyName, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] EndGetCompanyStockData(System.IAsyncResult result) {
            return base.Channel.EndGetCompanyStockData(result);
        }
        
        private System.IAsyncResult OnBeginGetCompanyStockData(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime date = ((System.DateTime)(inValues[0]));
            string companyName = ((string)(inValues[1]));
            return this.BeginGetCompanyStockData(date, companyName, callback, asyncState);
        }
        
        private object[] OnEndGetCompanyStockData(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] retVal = this.EndGetCompanyStockData(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetCompanyStockDataCompleted(object state) {
            if ((this.GetCompanyStockDataCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetCompanyStockDataCompleted(this, new GetCompanyStockDataCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetCompanyStockDataAsync(System.DateTime date, string companyName) {
            this.GetCompanyStockDataAsync(date, companyName, null);
        }
        
        public void GetCompanyStockDataAsync(System.DateTime date, string companyName, object userState) {
            if ((this.onBeginGetCompanyStockDataDelegate == null)) {
                this.onBeginGetCompanyStockDataDelegate = new BeginOperationDelegate(this.OnBeginGetCompanyStockData);
            }
            if ((this.onEndGetCompanyStockDataDelegate == null)) {
                this.onEndGetCompanyStockDataDelegate = new EndOperationDelegate(this.OnEndGetCompanyStockData);
            }
            if ((this.onGetCompanyStockDataCompletedDelegate == null)) {
                this.onGetCompanyStockDataCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetCompanyStockDataCompleted);
            }
            base.InvokeAsync(this.onBeginGetCompanyStockDataDelegate, new object[] {
                        date,
                        companyName}, this.onEndGetCompanyStockDataDelegate, this.onGetCompanyStockDataCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] GetCompanyStockDataSL(System.DateTime newDate, System.DateTime oldDate, string companyName) {
            return base.Channel.GetCompanyStockDataSL(newDate, oldDate, companyName);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetCompanyStockDataSL(System.DateTime newDate, System.DateTime oldDate, string companyName, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetCompanyStockDataSL(newDate, oldDate, companyName, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] EndGetCompanyStockDataSL(System.IAsyncResult result) {
            return base.Channel.EndGetCompanyStockDataSL(result);
        }
        
        private System.IAsyncResult OnBeginGetCompanyStockDataSL(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime newDate = ((System.DateTime)(inValues[0]));
            System.DateTime oldDate = ((System.DateTime)(inValues[1]));
            string companyName = ((string)(inValues[2]));
            return this.BeginGetCompanyStockDataSL(newDate, oldDate, companyName, callback, asyncState);
        }
        
        private object[] OnEndGetCompanyStockDataSL(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.StockData[] retVal = this.EndGetCompanyStockDataSL(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetCompanyStockDataSLCompleted(object state) {
            if ((this.GetCompanyStockDataSLCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetCompanyStockDataSLCompleted(this, new GetCompanyStockDataSLCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetCompanyStockDataSLAsync(System.DateTime newDate, System.DateTime oldDate, string companyName) {
            this.GetCompanyStockDataSLAsync(newDate, oldDate, companyName, null);
        }
        
        public void GetCompanyStockDataSLAsync(System.DateTime newDate, System.DateTime oldDate, string companyName, object userState) {
            if ((this.onBeginGetCompanyStockDataSLDelegate == null)) {
                this.onBeginGetCompanyStockDataSLDelegate = new BeginOperationDelegate(this.OnBeginGetCompanyStockDataSL);
            }
            if ((this.onEndGetCompanyStockDataSLDelegate == null)) {
                this.onEndGetCompanyStockDataSLDelegate = new EndOperationDelegate(this.OnEndGetCompanyStockDataSL);
            }
            if ((this.onGetCompanyStockDataSLCompletedDelegate == null)) {
                this.onGetCompanyStockDataSLCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetCompanyStockDataSLCompleted);
            }
            base.InvokeAsync(this.onBeginGetCompanyStockDataSLDelegate, new object[] {
                        newDate,
                        oldDate,
                        companyName}, this.onEndGetCompanyStockDataSLDelegate, this.onGetCompanyStockDataSLCompletedDelegate, userState);
        }
        
        public double GetHighestPrice(string companyName, System.DateTime start, System.DateTime end) {
            return base.Channel.GetHighestPrice(companyName, start, end);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetHighestPrice(string companyName, System.DateTime start, System.DateTime end, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetHighestPrice(companyName, start, end, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public double EndGetHighestPrice(System.IAsyncResult result) {
            return base.Channel.EndGetHighestPrice(result);
        }
        
        private System.IAsyncResult OnBeginGetHighestPrice(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string companyName = ((string)(inValues[0]));
            System.DateTime start = ((System.DateTime)(inValues[1]));
            System.DateTime end = ((System.DateTime)(inValues[2]));
            return this.BeginGetHighestPrice(companyName, start, end, callback, asyncState);
        }
        
        private object[] OnEndGetHighestPrice(System.IAsyncResult result) {
            double retVal = this.EndGetHighestPrice(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetHighestPriceCompleted(object state) {
            if ((this.GetHighestPriceCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetHighestPriceCompleted(this, new GetHighestPriceCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetHighestPriceAsync(string companyName, System.DateTime start, System.DateTime end) {
            this.GetHighestPriceAsync(companyName, start, end, null);
        }
        
        public void GetHighestPriceAsync(string companyName, System.DateTime start, System.DateTime end, object userState) {
            if ((this.onBeginGetHighestPriceDelegate == null)) {
                this.onBeginGetHighestPriceDelegate = new BeginOperationDelegate(this.OnBeginGetHighestPrice);
            }
            if ((this.onEndGetHighestPriceDelegate == null)) {
                this.onEndGetHighestPriceDelegate = new EndOperationDelegate(this.OnEndGetHighestPrice);
            }
            if ((this.onGetHighestPriceCompletedDelegate == null)) {
                this.onGetHighestPriceCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetHighestPriceCompleted);
            }
            base.InvokeAsync(this.onBeginGetHighestPriceDelegate, new object[] {
                        companyName,
                        start,
                        end}, this.onEndGetHighestPriceDelegate, this.onGetHighestPriceCompletedDelegate, userState);
        }
        
        public double GetLowestPrice(string companyName, System.DateTime start, System.DateTime end) {
            return base.Channel.GetLowestPrice(companyName, start, end);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetLowestPrice(string companyName, System.DateTime start, System.DateTime end, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetLowestPrice(companyName, start, end, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public double EndGetLowestPrice(System.IAsyncResult result) {
            return base.Channel.EndGetLowestPrice(result);
        }
        
        private System.IAsyncResult OnBeginGetLowestPrice(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string companyName = ((string)(inValues[0]));
            System.DateTime start = ((System.DateTime)(inValues[1]));
            System.DateTime end = ((System.DateTime)(inValues[2]));
            return this.BeginGetLowestPrice(companyName, start, end, callback, asyncState);
        }
        
        private object[] OnEndGetLowestPrice(System.IAsyncResult result) {
            double retVal = this.EndGetLowestPrice(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetLowestPriceCompleted(object state) {
            if ((this.GetLowestPriceCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetLowestPriceCompleted(this, new GetLowestPriceCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetLowestPriceAsync(string companyName, System.DateTime start, System.DateTime end) {
            this.GetLowestPriceAsync(companyName, start, end, null);
        }
        
        public void GetLowestPriceAsync(string companyName, System.DateTime start, System.DateTime end, object userState) {
            if ((this.onBeginGetLowestPriceDelegate == null)) {
                this.onBeginGetLowestPriceDelegate = new BeginOperationDelegate(this.OnBeginGetLowestPrice);
            }
            if ((this.onEndGetLowestPriceDelegate == null)) {
                this.onEndGetLowestPriceDelegate = new EndOperationDelegate(this.OnEndGetLowestPrice);
            }
            if ((this.onGetLowestPriceCompletedDelegate == null)) {
                this.onGetLowestPriceCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetLowestPriceCompleted);
            }
            base.InvokeAsync(this.onBeginGetLowestPriceDelegate, new object[] {
                        companyName,
                        start,
                        end}, this.onEndGetLowestPriceDelegate, this.onGetLowestPriceCompletedDelegate, userState);
        }
        
        public int[] GetTopRatedCompanyIDs(System.DateTime date) {
            return base.Channel.GetTopRatedCompanyIDs(date);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetTopRatedCompanyIDs(System.DateTime date, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetTopRatedCompanyIDs(date, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public int[] EndGetTopRatedCompanyIDs(System.IAsyncResult result) {
            return base.Channel.EndGetTopRatedCompanyIDs(result);
        }
        
        private System.IAsyncResult OnBeginGetTopRatedCompanyIDs(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime date = ((System.DateTime)(inValues[0]));
            return this.BeginGetTopRatedCompanyIDs(date, callback, asyncState);
        }
        
        private object[] OnEndGetTopRatedCompanyIDs(System.IAsyncResult result) {
            int[] retVal = this.EndGetTopRatedCompanyIDs(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetTopRatedCompanyIDsCompleted(object state) {
            if ((this.GetTopRatedCompanyIDsCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetTopRatedCompanyIDsCompleted(this, new GetTopRatedCompanyIDsCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetTopRatedCompanyIDsAsync(System.DateTime date) {
            this.GetTopRatedCompanyIDsAsync(date, null);
        }
        
        public void GetTopRatedCompanyIDsAsync(System.DateTime date, object userState) {
            if ((this.onBeginGetTopRatedCompanyIDsDelegate == null)) {
                this.onBeginGetTopRatedCompanyIDsDelegate = new BeginOperationDelegate(this.OnBeginGetTopRatedCompanyIDs);
            }
            if ((this.onEndGetTopRatedCompanyIDsDelegate == null)) {
                this.onEndGetTopRatedCompanyIDsDelegate = new EndOperationDelegate(this.OnEndGetTopRatedCompanyIDs);
            }
            if ((this.onGetTopRatedCompanyIDsCompletedDelegate == null)) {
                this.onGetTopRatedCompanyIDsCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetTopRatedCompanyIDsCompleted);
            }
            base.InvokeAsync(this.onBeginGetTopRatedCompanyIDsDelegate, new object[] {
                        date}, this.onEndGetTopRatedCompanyIDsDelegate, this.onGetTopRatedCompanyIDsCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData[] GetTopRatedCompaniesDataSL(System.DateTime start, System.DateTime end, string selectedCompany) {
            return base.Channel.GetTopRatedCompaniesDataSL(start, end, selectedCompany);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetTopRatedCompaniesDataSL(System.DateTime start, System.DateTime end, string selectedCompany, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetTopRatedCompaniesDataSL(start, end, selectedCompany, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData[] EndGetTopRatedCompaniesDataSL(System.IAsyncResult result) {
            return base.Channel.EndGetTopRatedCompaniesDataSL(result);
        }
        
        private System.IAsyncResult OnBeginGetTopRatedCompaniesDataSL(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime start = ((System.DateTime)(inValues[0]));
            System.DateTime end = ((System.DateTime)(inValues[1]));
            string selectedCompany = ((string)(inValues[2]));
            return this.BeginGetTopRatedCompaniesDataSL(start, end, selectedCompany, callback, asyncState);
        }
        
        private object[] OnEndGetTopRatedCompaniesDataSL(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.TopRatedCompanyData[] retVal = this.EndGetTopRatedCompaniesDataSL(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetTopRatedCompaniesDataSLCompleted(object state) {
            if ((this.GetTopRatedCompaniesDataSLCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetTopRatedCompaniesDataSLCompleted(this, new GetTopRatedCompaniesDataSLCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetTopRatedCompaniesDataSLAsync(System.DateTime start, System.DateTime end, string selectedCompany) {
            this.GetTopRatedCompaniesDataSLAsync(start, end, selectedCompany, null);
        }
        
        public void GetTopRatedCompaniesDataSLAsync(System.DateTime start, System.DateTime end, string selectedCompany, object userState) {
            if ((this.onBeginGetTopRatedCompaniesDataSLDelegate == null)) {
                this.onBeginGetTopRatedCompaniesDataSLDelegate = new BeginOperationDelegate(this.OnBeginGetTopRatedCompaniesDataSL);
            }
            if ((this.onEndGetTopRatedCompaniesDataSLDelegate == null)) {
                this.onEndGetTopRatedCompaniesDataSLDelegate = new EndOperationDelegate(this.OnEndGetTopRatedCompaniesDataSL);
            }
            if ((this.onGetTopRatedCompaniesDataSLCompletedDelegate == null)) {
                this.onGetTopRatedCompaniesDataSLCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetTopRatedCompaniesDataSLCompleted);
            }
            base.InvokeAsync(this.onBeginGetTopRatedCompaniesDataSLDelegate, new object[] {
                        start,
                        end,
                        selectedCompany}, this.onEndGetTopRatedCompaniesDataSLDelegate, this.onGetTopRatedCompaniesDataSLCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData[] GetCompaniesVolumeFromPeriod(System.DateTime start, System.DateTime end) {
            return base.Channel.GetCompaniesVolumeFromPeriod(start, end);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetCompaniesVolumeFromPeriod(System.DateTime start, System.DateTime end, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetCompaniesVolumeFromPeriod(start, end, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData[] EndGetCompaniesVolumeFromPeriod(System.IAsyncResult result) {
            return base.Channel.EndGetCompaniesVolumeFromPeriod(result);
        }
        
        private System.IAsyncResult OnBeginGetCompaniesVolumeFromPeriod(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime start = ((System.DateTime)(inValues[0]));
            System.DateTime end = ((System.DateTime)(inValues[1]));
            return this.BeginGetCompaniesVolumeFromPeriod(start, end, callback, asyncState);
        }
        
        private object[] OnEndGetCompaniesVolumeFromPeriod(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.CompaniesVolumeData[] retVal = this.EndGetCompaniesVolumeFromPeriod(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetCompaniesVolumeFromPeriodCompleted(object state) {
            if ((this.GetCompaniesVolumeFromPeriodCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetCompaniesVolumeFromPeriodCompleted(this, new GetCompaniesVolumeFromPeriodCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetCompaniesVolumeFromPeriodAsync(System.DateTime start, System.DateTime end) {
            this.GetCompaniesVolumeFromPeriodAsync(start, end, null);
        }
        
        public void GetCompaniesVolumeFromPeriodAsync(System.DateTime start, System.DateTime end, object userState) {
            if ((this.onBeginGetCompaniesVolumeFromPeriodDelegate == null)) {
                this.onBeginGetCompaniesVolumeFromPeriodDelegate = new BeginOperationDelegate(this.OnBeginGetCompaniesVolumeFromPeriod);
            }
            if ((this.onEndGetCompaniesVolumeFromPeriodDelegate == null)) {
                this.onEndGetCompaniesVolumeFromPeriodDelegate = new EndOperationDelegate(this.OnEndGetCompaniesVolumeFromPeriod);
            }
            if ((this.onGetCompaniesVolumeFromPeriodCompletedDelegate == null)) {
                this.onGetCompaniesVolumeFromPeriodCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetCompaniesVolumeFromPeriodCompleted);
            }
            base.InvokeAsync(this.onBeginGetCompaniesVolumeFromPeriodDelegate, new object[] {
                        start,
                        end}, this.onEndGetCompaniesVolumeFromPeriodDelegate, this.onGetCompaniesVolumeFromPeriodCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData GetCompanyData(System.DateTime newDate, System.DateTime oldDate, string companyName) {
            return base.Channel.GetCompanyData(newDate, oldDate, companyName);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetCompanyData(System.DateTime newDate, System.DateTime oldDate, string companyName, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetCompanyData(newDate, oldDate, companyName, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData EndGetCompanyData(System.IAsyncResult result) {
            return base.Channel.EndGetCompanyData(result);
        }
        
        private System.IAsyncResult OnBeginGetCompanyData(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime newDate = ((System.DateTime)(inValues[0]));
            System.DateTime oldDate = ((System.DateTime)(inValues[1]));
            string companyName = ((string)(inValues[2]));
            return this.BeginGetCompanyData(newDate, oldDate, companyName, callback, asyncState);
        }
        
        private object[] OnEndGetCompanyData(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData retVal = this.EndGetCompanyData(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetCompanyDataCompleted(object state) {
            if ((this.GetCompanyDataCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetCompanyDataCompleted(this, new GetCompanyDataCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetCompanyDataAsync(System.DateTime newDate, System.DateTime oldDate, string companyName) {
            this.GetCompanyDataAsync(newDate, oldDate, companyName, null);
        }
        
        public void GetCompanyDataAsync(System.DateTime newDate, System.DateTime oldDate, string companyName, object userState) {
            if ((this.onBeginGetCompanyDataDelegate == null)) {
                this.onBeginGetCompanyDataDelegate = new BeginOperationDelegate(this.OnBeginGetCompanyData);
            }
            if ((this.onEndGetCompanyDataDelegate == null)) {
                this.onEndGetCompanyDataDelegate = new EndOperationDelegate(this.OnEndGetCompanyData);
            }
            if ((this.onGetCompanyDataCompletedDelegate == null)) {
                this.onGetCompanyDataCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetCompanyDataCompleted);
            }
            base.InvokeAsync(this.onBeginGetCompanyDataDelegate, new object[] {
                        newDate,
                        oldDate,
                        companyName}, this.onEndGetCompanyDataDelegate, this.onGetCompanyDataCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData[] GetCompanyMultipleDataFromPeriod(int currentDate, int count, int periodSize, string companyName) {
            return base.Channel.GetCompanyMultipleDataFromPeriod(currentDate, count, periodSize, companyName);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetCompanyMultipleDataFromPeriod(int currentDate, int count, int periodSize, string companyName, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetCompanyMultipleDataFromPeriod(currentDate, count, periodSize, companyName, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData[] EndGetCompanyMultipleDataFromPeriod(System.IAsyncResult result) {
            return base.Channel.EndGetCompanyMultipleDataFromPeriod(result);
        }
        
        private System.IAsyncResult OnBeginGetCompanyMultipleDataFromPeriod(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int currentDate = ((int)(inValues[0]));
            int count = ((int)(inValues[1]));
            int periodSize = ((int)(inValues[2]));
            string companyName = ((string)(inValues[3]));
            return this.BeginGetCompanyMultipleDataFromPeriod(currentDate, count, periodSize, companyName, callback, asyncState);
        }
        
        private object[] OnEndGetCompanyMultipleDataFromPeriod(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.CompanyData[] retVal = this.EndGetCompanyMultipleDataFromPeriod(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetCompanyMultipleDataFromPeriodCompleted(object state) {
            if ((this.GetCompanyMultipleDataFromPeriodCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetCompanyMultipleDataFromPeriodCompleted(this, new GetCompanyMultipleDataFromPeriodCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetCompanyMultipleDataFromPeriodAsync(int currentDate, int count, int periodSize, string companyName) {
            this.GetCompanyMultipleDataFromPeriodAsync(currentDate, count, periodSize, companyName, null);
        }
        
        public void GetCompanyMultipleDataFromPeriodAsync(int currentDate, int count, int periodSize, string companyName, object userState) {
            if ((this.onBeginGetCompanyMultipleDataFromPeriodDelegate == null)) {
                this.onBeginGetCompanyMultipleDataFromPeriodDelegate = new BeginOperationDelegate(this.OnBeginGetCompanyMultipleDataFromPeriod);
            }
            if ((this.onEndGetCompanyMultipleDataFromPeriodDelegate == null)) {
                this.onEndGetCompanyMultipleDataFromPeriodDelegate = new EndOperationDelegate(this.OnEndGetCompanyMultipleDataFromPeriod);
            }
            if ((this.onGetCompanyMultipleDataFromPeriodCompletedDelegate == null)) {
                this.onGetCompanyMultipleDataFromPeriodCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetCompanyMultipleDataFromPeriodCompleted);
            }
            base.InvokeAsync(this.onBeginGetCompanyMultipleDataFromPeriodDelegate, new object[] {
                        currentDate,
                        count,
                        periodSize,
                        companyName}, this.onEndGetCompanyMultipleDataFromPeriodDelegate, this.onGetCompanyMultipleDataFromPeriodCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] GetStockDataFromPeriodByCompanyList(int currentDate, int count, int periodSize, string[] companies) {
            return base.Channel.GetStockDataFromPeriodByCompanyList(currentDate, count, periodSize, companies);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetStockDataFromPeriodByCompanyList(int currentDate, int count, int periodSize, string[] companies, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetStockDataFromPeriodByCompanyList(currentDate, count, periodSize, companies, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] EndGetStockDataFromPeriodByCompanyList(System.IAsyncResult result) {
            return base.Channel.EndGetStockDataFromPeriodByCompanyList(result);
        }
        
        private System.IAsyncResult OnBeginGetStockDataFromPeriodByCompanyList(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int currentDate = ((int)(inValues[0]));
            int count = ((int)(inValues[1]));
            int periodSize = ((int)(inValues[2]));
            string[] companies = ((string[])(inValues[3]));
            return this.BeginGetStockDataFromPeriodByCompanyList(currentDate, count, periodSize, companies, callback, asyncState);
        }
        
        private object[] OnEndGetStockDataFromPeriodByCompanyList(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] retVal = this.EndGetStockDataFromPeriodByCompanyList(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetStockDataFromPeriodByCompanyListCompleted(object state) {
            if ((this.GetStockDataFromPeriodByCompanyListCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetStockDataFromPeriodByCompanyListCompleted(this, new GetStockDataFromPeriodByCompanyListCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetStockDataFromPeriodByCompanyListAsync(int currentDate, int count, int periodSize, string[] companies) {
            this.GetStockDataFromPeriodByCompanyListAsync(currentDate, count, periodSize, companies, null);
        }
        
        public void GetStockDataFromPeriodByCompanyListAsync(int currentDate, int count, int periodSize, string[] companies, object userState) {
            if ((this.onBeginGetStockDataFromPeriodByCompanyListDelegate == null)) {
                this.onBeginGetStockDataFromPeriodByCompanyListDelegate = new BeginOperationDelegate(this.OnBeginGetStockDataFromPeriodByCompanyList);
            }
            if ((this.onEndGetStockDataFromPeriodByCompanyListDelegate == null)) {
                this.onEndGetStockDataFromPeriodByCompanyListDelegate = new EndOperationDelegate(this.OnEndGetStockDataFromPeriodByCompanyList);
            }
            if ((this.onGetStockDataFromPeriodByCompanyListCompletedDelegate == null)) {
                this.onGetStockDataFromPeriodByCompanyListCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetStockDataFromPeriodByCompanyListCompleted);
            }
            base.InvokeAsync(this.onBeginGetStockDataFromPeriodByCompanyListDelegate, new object[] {
                        currentDate,
                        count,
                        periodSize,
                        companies}, this.onEndGetStockDataFromPeriodByCompanyListDelegate, this.onGetStockDataFromPeriodByCompanyListCompletedDelegate, userState);
        }
        
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] GetStockDataFromDateByCompanyList(System.DateTime date, string[] companies) {
            return base.Channel.GetStockDataFromDateByCompanyList(date, companies);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetStockDataFromDateByCompanyList(System.DateTime date, string[] companies, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetStockDataFromDateByCompanyList(date, companies, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] EndGetStockDataFromDateByCompanyList(System.IAsyncResult result) {
            return base.Channel.EndGetStockDataFromDateByCompanyList(result);
        }
        
        private System.IAsyncResult OnBeginGetStockDataFromDateByCompanyList(object[] inValues, System.AsyncCallback callback, object asyncState) {
            System.DateTime date = ((System.DateTime)(inValues[0]));
            string[] companies = ((string[])(inValues[1]));
            return this.BeginGetStockDataFromDateByCompanyList(date, companies, callback, asyncState);
        }
        
        private object[] OnEndGetStockDataFromDateByCompanyList(System.IAsyncResult result) {
            DevExpress.StockMarketTrader.StockDataServiceReference.CompanyStockData[] retVal = this.EndGetStockDataFromDateByCompanyList(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetStockDataFromDateByCompanyListCompleted(object state) {
            if ((this.GetStockDataFromDateByCompanyListCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetStockDataFromDateByCompanyListCompleted(this, new GetStockDataFromDateByCompanyListCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetStockDataFromDateByCompanyListAsync(System.DateTime date, string[] companies) {
            this.GetStockDataFromDateByCompanyListAsync(date, companies, null);
        }
        
        public void GetStockDataFromDateByCompanyListAsync(System.DateTime date, string[] companies, object userState) {
            if ((this.onBeginGetStockDataFromDateByCompanyListDelegate == null)) {
                this.onBeginGetStockDataFromDateByCompanyListDelegate = new BeginOperationDelegate(this.OnBeginGetStockDataFromDateByCompanyList);
            }
            if ((this.onEndGetStockDataFromDateByCompanyListDelegate == null)) {
                this.onEndGetStockDataFromDateByCompanyListDelegate = new EndOperationDelegate(this.OnEndGetStockDataFromDateByCompanyList);
            }
            if ((this.onGetStockDataFromDateByCompanyListCompletedDelegate == null)) {
                this.onGetStockDataFromDateByCompanyListCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetStockDataFromDateByCompanyListCompleted);
            }
            base.InvokeAsync(this.onBeginGetStockDataFromDateByCompanyListDelegate, new object[] {
                        date,
                        companies}, this.onEndGetStockDataFromDateByCompanyListDelegate, this.onGetStockDataFromDateByCompanyListCompletedDelegate, userState);
        }
    }
}
