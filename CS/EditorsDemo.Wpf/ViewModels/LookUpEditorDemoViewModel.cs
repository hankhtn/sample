
using System.Collections.Generic;
using DevExpress.DemoData;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace EditorsDemo {
    public class LookUpEditorDemoViewModel {

        public LookUpEditorDemoViewModel() {
            CarsSource = CarsData.DataSource;
            CollectionViewSource = new CollectionViewViewModel();
            DataLoader = new NWindDataLoader();
            ProductsSource = (IList<Product>)DataLoader.Products;
            SelectedCars = new List<object>() { CarsSource[0], CarsSource[2], CarsSource[3], CarsSource[5], CarsSource[8] };
        }

        public List<Cars> CarsSource { get; private set; }
        public object CollectionViewSource { get; private set; }
        public IList<Product> ProductsSource { get; private set; }
        public IList<object> SelectedCars { get; set; }

        NWindDataLoader DataLoader { get; set; }

    }
}
