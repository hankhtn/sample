using System.Collections.Generic;
using System.Linq;
using DevExpress.DemoData.Models.Vehicles;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Filtering;

namespace NavigationDemo {
    public class AutoTraderViewModel {
        public IEnumerable<Model> Vehicles { get; private set; }
        public IEnumerable<Trademark> Trademarks { get; private set; }
        public IEnumerable<TransmissionType> TransmissionTypes { get; private set; }
        public IEnumerable<BodyStyle> BodyStyles { get; private set; }
        public int[] DoorTypes { get; private set; }
        public decimal MinPrice { get; private set; }
        public decimal MaxPrice { get; private set; }
        public int MinMPGCity { get; private set; }
        public int MaxMPGCity { get; private set; }
        public int MinMPGHighway { get; private set; }
        public int MaxMPGHighway { get; private set; }
        public AutoTraderViewModel() {
            if(this.IsInDesignMode()) return;
            var context = new VehiclesContext();
            Vehicles = context.Models.ToList();
            Trademarks = context.Trademarks.ToList();
            TransmissionTypes = context.TransmissionTypes.ToList();
            BodyStyles = context.BodyStyles.ToList();
            DoorTypes = new int[] { 2, 3, 4 };
            CalculateProperties();
            context.Dispose();
        }
        void CalculateProperties() {
            MinPrice = Vehicles.Min(x => x.Price);
            MaxPrice = Vehicles.Max(x => x.Price);
            MinMPGCity = Vehicles.Min(x => x.MPGCity ?? int.MaxValue);
            MaxMPGCity = Vehicles.Max(x => x.MPGCity ?? int.MinValue);
            MinMPGHighway = Vehicles.Min(x => x.MPGHighway ?? int.MaxValue);
            MaxMPGHighway = Vehicles.Max(x => x.MPGHighway ?? int.MinValue);
        }
    }

    public class VehiclesFilteringViewModel {
        [FilterRange("MinPrice", "MaxPrice")]
        public decimal Price { get; set; }
        [FilterLookup("Trademarks", DisplayMember = "Name", ValueMember = "ID")]
        public long TrademarkID { get; set; }
        [FilterLookup("TransmissionTypes", DisplayMember = "Name", ValueMember = "ID")]
        public long TransmissionTypeID { get; set; }
        [FilterLookup("BodyStyles", DisplayMember = "Name", ValueMember = "ID")]
        public long BodyStyleID { get; set; }
        [FilterLookup("DoorTypes")]
        public int Doors { get; set; }
        [FilterRange("MinMPGCity", "MaxMPGCity")]
        public int? MPGCity { get; set; }
        [FilterRange("MinMPGHighway", "MaxMPGHighway")]
        public int? MPGHighway { get; set; }
    }
}
