using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Resources;

namespace ChartsDemo {
    public static class Chart3DUtils {
        public static Size3D CreateSize3D(double x, double y, double z) {
            return new Size3D(x, y, z);
        }
        public static BitmapImage CreateFunctionImage(int index) {
            string uriStr = string.Format(@"/ChartsDemo;component/Images/Functions/{0}.png", index);
            return new BitmapImage(new Uri(uriStr, UriKind.RelativeOrAbsolute));
        }
    }

    public class DataGenerator {
        public class ElevationData {
            int directionX;
            int directionZ;
            int directionY;

            public Point3D Elevation { get; set; }
            public int DirectionX { get { return directionX; } }
            public int DirectionZ { get { return directionZ; } }
            public int DirectionY { get { return directionY; } }

            public ElevationData(Point3D elevation) {
                Elevation = elevation;
                this.directionX = 1;
                this.directionZ = 1;
                this.directionY = 1;
            }
            public void UpdateDirection(int sideLength) {
                if(Elevation.X < 0 || Elevation.X >= sideLength) directionX *= -1;
                if(Elevation.Z < 0 || Elevation.Z >= sideLength) directionZ *= -1;
                if(Elevation.Y < -sideLength / 3 || Elevation.Y > sideLength / 3) directionY *= -1;
            }
        }

        const double OffsetFactor = Math.PI * 3.0 / 2.0;
        const double RadiusFactor = 10.0 / Math.PI;
        const int ElevationsCount = 25;

        readonly Random rnd = new Random();
        readonly ElevationData[] vertices;
        int sensetivitiDirection = 1;
        double sensitivity;

        public int Size { get; set; }

        public DataGenerator() {
            this.vertices = new ElevationData[ElevationsCount];
        }
        void NextElevations() {
            double k = (double)Size / 66.0;
            for(int i = 0; i < ElevationsCount; i++) {
                vertices[i].UpdateDirection(Size);
                double dx = k * vertices[i].DirectionX;
                double dz = k * vertices[i].DirectionZ;
                double dy = k * vertices[i].DirectionY;
                double x = vertices[i].Elevation.X + dx;
                double z = vertices[i].Elevation.Z + dz;
                double y = vertices[i].Elevation.Y + dy;
                vertices[i].Elevation = new Point3D(x, y, z);
            }
        }
        void NextSensetivity() {
            if(sensitivity < Size * 0.15)
                sensetivitiDirection = 1;
            if(sensitivity > Size * 0.6)
                sensetivitiDirection = -1;
            sensitivity += sensetivitiDirection * 0.2;
        }
        public void RecreateElevations() {
            sensitivity = Size * 0.5;
            for(int i = 0; i < ElevationsCount; i++) {
                Point3D elevation = new Point3D(rnd.Next(0, Size), rnd.Next((int)(-Size / 3), (int)(Size / 3)), rnd.Next(0, Size));
                vertices[i] = new ElevationData(elevation);
            }
        }
        public IEnumerable<object> GenerateArguments() {
            List<object> arguments = new List<object>();
            for(int i = 0; i < Size; i++)
                arguments.Add(i);
            return arguments;
        }
        public IEnumerable<double> GenerateValues() {
            NextElevations();
            NextSensetivity();
            double[] values = new double[Size * Size];
            for(int j = 0; j < vertices.Length; j++) {
                double dataX = vertices[j].Elevation.X;
                double dataZ = vertices[j].Elevation.Z;
                double dataY = vertices[j].Elevation.Y;
                int counter = 0;
                for(int x = 0; x < Size; x++) {
                    for(int z = 0; z < Size; z++) {
                        double dx = dataX - x;
                        double dz = dataZ - z;
                        double distance = Math.Sqrt(dx * dx + dz * dz);
                        if(distance <= sensitivity) {
                            double percent = 1.0 - distance / sensitivity;
                            double coef = dataY - values[counter];
                            double elevate = coef * (Math.Sin(percent * RadiusFactor + OffsetFactor) + 1.0) / 2;
                            values[counter] += elevate;
                        }
                        counter++;
                    }
                }
            }
            return values;
        }
    }

    public class Point3DComparer : IEqualityComparer<Point3D> {
        const double DefaultEps = 10e-12;
        double eps = DefaultEps;

        public Point3DComparer() {
        }

        public Point3DComparer(double eps) {
            this.eps = eps;
        }

        public bool Equals(Point3D p1, Point3D p2) {
            return Math.Abs(p1.X - p2.X) < eps && Math.Abs(p1.Y - p2.Y) < eps && Math.Abs(p1.Z - p2.Z) < eps;
        }
        public int GetHashCode(Point3D e1) {
            return e1.GetHashCode();
        }
    }

    public class StarAxisLabelDataToStringConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double res = 0;
            if(double.TryParse(value as string, NumberStyles.Float, CultureInfo.InvariantCulture, out res))
                return Math.Abs(res) > 1000 ? string.Format("{0}k", res * 0.001) : res.ToString();
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class StarEffect : ShaderEffect {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(StarEffect), 0);
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty WaveSizeProperty = DependencyProperty.Register("WaveSize", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(196D)), PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty CenterPointProperty = DependencyProperty.Register("CenterPoint", typeof(Point), typeof(StarEffect), new UIPropertyMetadata(new Point(0.5D, 0.5D), PixelShaderConstantCallback(2)));
        public static readonly DependencyProperty InnerRadiusProperty = DependencyProperty.Register("InnerRadius", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(0.2D)), PixelShaderConstantCallback(3)));
        public static readonly DependencyProperty OuterRadiusProperty = DependencyProperty.Register("OuterRadius", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(0.4D)), PixelShaderConstantCallback(4)));
        public static readonly DependencyProperty MagnificationAmountProperty = DependencyProperty.Register("MagnificationAmount", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(1D)), PixelShaderConstantCallback(5)));
        public static readonly DependencyProperty AspectRatioProperty = DependencyProperty.Register("AspectRatio", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(1.5D)), PixelShaderConstantCallback(6)));
        public static readonly DependencyProperty BlurAmountProperty = DependencyProperty.Register("BlurAmount", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(2D)), PixelShaderConstantCallback(7)));
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(8)));
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(StarEffect), new UIPropertyMetadata(((double)(0.5D)), PixelShaderConstantCallback(9)));
        public StarEffect() {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri(@"/ChartsDemo;component/Data/Star.ps", UriKind.Relative);
            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(TimeProperty);
            this.UpdateShaderValue(WaveSizeProperty);
            this.UpdateShaderValue(CenterPointProperty);
            this.UpdateShaderValue(InnerRadiusProperty);
            this.UpdateShaderValue(OuterRadiusProperty);
            this.UpdateShaderValue(MagnificationAmountProperty);
            this.UpdateShaderValue(AspectRatioProperty);
            this.UpdateShaderValue(BlurAmountProperty);
            this.UpdateShaderValue(MinValueProperty);
            this.UpdateShaderValue(MaxValueProperty);
        }
        public Brush Input {
            get {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set {
                this.SetValue(InputProperty, value);
            }
        }
        public double Time {
            get {
                return ((double)(this.GetValue(TimeProperty)));
            }
            set {
                this.SetValue(TimeProperty, value);
            }
        }
        public double WaveSize {
            get {
                return ((double)(this.GetValue(WaveSizeProperty)));
            }
            set {
                this.SetValue(WaveSizeProperty, value);
            }
        }
        public Point CenterPoint {
            get {
                return ((Point)(this.GetValue(CenterPointProperty)));
            }
            set {
                this.SetValue(CenterPointProperty, value);
            }
        }
        public double InnerRadius {
            get {
                return ((double)(this.GetValue(InnerRadiusProperty)));
            }
            set {
                this.SetValue(InnerRadiusProperty, value);
            }
        }
        public double OuterRadius {
            get {
                return ((double)(this.GetValue(OuterRadiusProperty)));
            }
            set {
                this.SetValue(OuterRadiusProperty, value);
            }
        }
        public double MagnificationAmount {
            get {
                return ((double)(this.GetValue(MagnificationAmountProperty)));
            }
            set {
                this.SetValue(MagnificationAmountProperty, value);
            }
        }
        public double AspectRatio {
            get {
                return ((double)(this.GetValue(AspectRatioProperty)));
            }
            set {
                this.SetValue(AspectRatioProperty, value);
            }
        }
        public double BlurAmount {
            get {
                return ((double)(this.GetValue(BlurAmountProperty)));
            }
            set {
                this.SetValue(BlurAmountProperty, value);
            }
        }
        public double MinValue {
            get {
                return ((double)(this.GetValue(MinValueProperty)));
            }
            set {
                this.SetValue(MinValueProperty, value);
            }
        }
        public double MaxValue {
            get {
                return ((double)(this.GetValue(MaxValueProperty)));
            }
            set {
                this.SetValue(MaxValueProperty, value);
            }
        }
    }
}
