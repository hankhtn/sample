using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using DevExpress.DemoData.Helpers;

namespace DevExpress.RealtorWorld.Xpf.DataModel {
    public class MortgageRateRepository : XmlRepository<MortgageRate, DateTime>, IMortgageRateRepository {
        protected override DateTime GetKey(MortgageRate entity) { return entity.Date; }
        protected override ReusableStream GetDataStream() {
            return GetDataStreamCore("Mortgage.xml");
        }
        protected override Type ObservableCollectionType { get { return typeof(EntityObservableCollection); } }
        [XmlRoot("dsHistoricalMortgageRateData", Namespace = "http://tempuri.org/dsHistoricalMortgageRateData.xsd")]
        public class EntityObservableCollection : ObservableCollection<MortgageRate> { }
    }
}
