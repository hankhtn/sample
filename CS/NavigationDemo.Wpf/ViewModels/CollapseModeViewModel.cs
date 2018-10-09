using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace NavigationDemo {
    public class CollapseModeViewModel {

        public ReadOnlyCollection<FilterGroup> FiltersData { get; private set; }

        public CollapseModeViewModel() {
            FiltersData = new ReadOnlyCollection<FilterGroup>(CreateFilters());
        }

        FilterGroup[] CreateFilters() {
            var experienceItems = new FilterItem[] {
                new FilterItem("> 20 years", "(DateDiffYear([HireDate], Today()) > 20)"),
                new FilterItem("> 15 and <= 20 years", "(DateDiffYear([HireDate], Today()) > 15 AND DateDiffYear([HireDate], Today()) <= 20)"),
                new FilterItem("> 10 and <= 15 years", "(DateDiffYear([HireDate], Today()) > 10 AND DateDiffYear([HireDate], Today()) <= 15)"),
                new FilterItem("<= 10 years", "(DateDiffYear([HireDate], Today()) <= 10)"),
            };
            var regionItems = EmployeesWithPhotoData.DataSource.Select(x => x.CountryRegionName)
                .Distinct()
                .Select(x => new FilterItem(x, string.Format("([CountryRegionName] = '{0}')", x), false, GetFlag(x))).ToArray();
            regionItems.Take(4).ToList().ForEach(x => x.ShowInCollapsedMode = true);

            return new FilterGroup[] {
                new FilterGroup("Experience", experienceItems),
                new FilterGroup("Region", regionItems.ToArray())
            };
        }
        static byte[] GetFlag(string country) {
            var countryInfo = CountriesData.DataSource.FirstOrDefault(x => object.Equals(x.ActualName, country));
            return countryInfo == null ? null : countryInfo.Flag;
        }
    }

    public class FilterGroup {
        public string Name { get; private set; }
        public List<FilterItem> FilterItems { get; private set; }

        public FilterGroup(string name, FilterItem[] filterItems) {
            Name = name;
            FilterItems = filterItems.ToList();
        }
        public override string ToString() {
            return Name;
        }
    }
    public class FilterItem {
        public string Name { get; private set; }
        public string FilterString { get; private set; }
        public bool ShowInCollapsedMode { get; set; }
        public object Icon { get; private set; }

        public FilterItem(string name, string filterString, bool showInCollapsedMode = false, byte[] icon = null) {
            Name = name;
            FilterString = filterString;
            ShowInCollapsedMode = showInCollapsedMode;
            Icon = icon;
        }
        public override string ToString() {
            return Name;
        }
    }
}
