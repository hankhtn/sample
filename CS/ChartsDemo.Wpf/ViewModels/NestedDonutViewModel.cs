using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartsDemo {
    public class NestedDonutViewModel {
        const string AgeDataMember = "Age";
        const string GenderDataMember = "Sex";

        public virtual string ArgumentDataMember { get; set; }
        public virtual string SeriesDataMember { get; set; }
        public virtual string DemoTitle { get; set; }
        public virtual string HintDataMember { get; set; }
        public object DataSource { get; private set; }

        public NestedDonutViewModel() {
            DataSource = AgeStructure.GetPopulationAgeStructure();
            ArgumentDataMember = AgeDataMember;
        }

        protected void OnArgumentDataMemberChanged() {
            HintDataMember = (ArgumentDataMember == AgeDataMember) ? GenderDataMember : AgeDataMember;
            SeriesDataMember = "Country" + HintDataMember + "Key";
            DemoTitle = "Population: " + ArgumentDataMember + " Structure";
        }
    }
}
