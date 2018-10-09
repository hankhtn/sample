using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Linq;
using DevExpress.Mvvm;

namespace ChartsDemo {
    public class MathematicalFunctionsViewModel {
        const double UnitFactor = 15;

        public List<Function3DItemData> FunctionItemsData { get; private set; }
        public virtual Function3DItemData Function { get; set; }

        public MathematicalFunctionsViewModel() {
            FunctionItemsData = CreateFunctionItemsData();
            Function = FunctionItemsData[3];
        }

        #region Static

        static List<Function3DItemData> CreateFunctionItemsData() {
            var funcs = new Func<double, double, Point3D>[] 
                { CalculateFirstValue, CalculateSecondValue, CalculateThirdValue, CalculateFourthValue, CalculateFifthValue };
            var images = Enumerable.Range(1, 5).Select(x => Chart3DUtils.CreateFunctionImage(x));
            return funcs.Zip(images, (func, image) => new Function3DItemData { Image = image, Points = CreatePoints(func) }).ToList();
        }

        static double Sinc(double x) {
            return x != 0 ? Math.Sin(x) : 1;
        }
        static Point3D CalculateFirstValue(double x, double y) {
            x *= UnitFactor;
            y *= UnitFactor;
            double value = Sinc(Math.Sin(Math.Pow(Math.Pow(x, 6) + Math.Pow(y, 6), 1.0d / 6.0d))) * 5;
            return new Point3D(x, y, value);
        }
        static Point3D CalculateSecondValue(double x, double y) {
            x *= UnitFactor;
            y *= UnitFactor;
            double square = Math.Sqrt(x * x + y * y);
            double value = square * Sinc(square) * 0.2;
            return new Point3D(x, y, value);
        }
        static Point3D CalculateThirdValue(double x, double y) {
            x *= UnitFactor / 2;
            y *= UnitFactor / 2;
            double value = Math.Sin(x) * Math.Cos(y) * 2;
            return new Point3D(x, y, value);
        }
        static Point3D CalculateFourthValue(double x, double y) {
            double theta = Math.Atan2(y, x);
            double r = x * x + y * y;
            double z = Math.Exp(-r) * Math.Sin(2 * Math.PI * Math.Sqrt(r)) * Math.Cos(3 * theta);
            return new Point3D(x, y, z);
        }
        static Point3D CalculateFifthValue(double x, double y) {
            x *= 3;
            y *= 3;
            double z = Math.Sin(x * y);
            return new Point3D(x, y, z);
        }
        static List<Point3D> CreatePoints(Func<double, double, Point3D> valueCalculator) {
            List<Point3D> points = new List<Point3D>();
            for(double x = -1; x < 1; x += 0.017)
                for(double y = -1; y < 1; y += 0.017)
                    points.Add(valueCalculator(x, y));
            return points;
        }

        #endregion
    }
    public class Function3DItemData : BindableBase {
        List<Point3D> points;
        BitmapImage image;

        public List<Point3D> Points {
            get { return points; }
            set { SetProperty(ref points, value, () => Points); }
        }
        public BitmapImage Image {
            get { return image; }
            set { SetProperty(ref image, value, () => Image); }
        }
    }
}
