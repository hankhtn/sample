using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Threading;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {

    public static class DataLoader {
        public static XDocument LoadXmlFromResources(string fileName) {
            try {
                return XDocument.Load(LoadFromResources(fileName));
            }
            catch {
                return null;
            }
        }
        public static Stream LoadFromResources(string fileName) {
            try {
                Uri uri = new Uri("/ChartsDemo;component" + fileName, UriKind.RelativeOrAbsolute);
                return Application.GetResourceStream(uri).Stream;
            }
            catch {
                return null;
            }
        }
    }


    public class FinancialPoint {
        public static FinancialPoint Create(int argument, double lowValue, double highValue, double openValue, double closeValue) {
            return new FinancialPoint {
                IntArgument = argument,
                LowValue = lowValue,
                HighValue = highValue,
                OpenValue = openValue,
                CloseValue = closeValue
            };
        }

        public int IntArgument { get; set; }
        public string Argument { get; set; }
        public DateTime DateTimeArgument { get; set; }
        public double HighValue { get; set; }
        public double LowValue { get; set; }
        public double OpenValue { get; set; }
        public double CloseValue { get; set; }
    }

    public struct RangeDataPoint {
        double x;
        double y1;
        double y2;

        public double X { get { return x; } }
        public double Y1 { get { return y1; } }
        public double Y2 { get { return y2; } }

        public RangeDataPoint(double x, double y1, double y2) {
            this.x = x;
            this.y1 = y1;
            this.y2 = y2;
        }
    }


    public static class PolarFunctionsPointGenerator {
        public static List<RangeDataPoint> CreateLemniskateData() {
            List<RangeDataPoint> list = new List<RangeDataPoint>();
            for (double x = 0; x < 360; x += 5) {
                double xRadian = DegreeToRadian(x);
                double cos = Math.Cos(2 * xRadian);
                double y = Math.Pow(Math.Abs(cos), 2);
                list.Add(new RangeDataPoint(x, y, 1));
            }
            return list;
        }

        public static List<RangeDataPoint> CreateCardioidData() {
            List<RangeDataPoint> list = new List<RangeDataPoint>();
            const double a = 0.5;
            for (double x = 0; x < 360; x += 15) {
                double y = 2 * a * Math.Cos(DegreeToRadian(x));
                list.Add(new RangeDataPoint(x, y, 1));
            }
            return list;
        }

        public static List<RangeDataPoint> CreateTaubinsHeartData() {
            List<RangeDataPoint> list = new List<RangeDataPoint>();
            for (double x = 0; x < 360; x += 15) {
                double xRadian = DegreeToRadian(x);
                double y = 2 - 0.5 * Math.Sin(xRadian) + Math.Sin(xRadian) * Math.Sqrt(Math.Abs(Math.Cos(xRadian))) / (Math.Sin(xRadian) + 1.4);
                list.Add(new RangeDataPoint(x, y, 1));
            }
            return list;
        }

        static double DegreeToRadian(double degree) {
            return 2 * Math.PI / 360 * degree;
        }
    }

    public static class CartesianFunctionsPointGenerator {
        const int a = 10;

        public static List<Point> CreateArchimedianSpiralPoints() {
            var points = new List<Point>();
            for(int i = 0; i < 720; i += 15) {
                double t = (double)i / 180 * Math.PI;
                double x = t * Math.Cos(t);
                double y = t * Math.Sin(t);
                points.Add(new Point(x, y));
            }
            return points;
        }
        public static List<Point> CreateCardioidPoints() {
            var points = new List<Point>();
            for(int i = 0; i < 360; i += 10) {
                double t = (double)i / 180 * Math.PI;
                double x = a * (2 * Math.Cos(t) - Math.Cos(2 * t));
                double y = a * (2 * Math.Sin(t) - Math.Sin(2 * t));
                points.Add(new Point(x, y));
            }
            return points;
        }
        public static List<Point> CreateCartesianFoliumPoints() {
            var points = new List<Point>();
            for(int i = -30; i < 125; i += 5) {
                double t = Math.Tan((double)i / 180 * Math.PI);
                double x = 3 * (double)a * t / (t * t * t + 1);
                double y = x * t;
                points.Add(new Point(x, y));
            }
            return points;
        }
    }


    public abstract class ScatterFunctionCalculatorBase {
        const int spiralIntervalsCount = 72;
        const int roseIntervalsCount = 288;
        const int foliumSegmentIntervalsCount = 30;

        const double roseParameter = 7.0d / 4.0d;
        const double foliumDistanceLimit = 3.0;

        protected abstract double NormalizeAngle(double angle);
        protected abstract double ToRadian(double angle);
        protected abstract double FromDegrees(double angle);

        List<Point> FilterPointsByRange(List<Point> points, double min, double max) {
            List<Point> resultPoints = new List<Point>();
            foreach (Point point in points) {
                double pointValue = point.Y;
                if (pointValue <= max && pointValue >= min)
                    resultPoints.Add(point);
            }
            return resultPoints;
        }
        List<Point> CreatePolarFunctionPoints(double minAngleDegree, double maxAngleDegree, int intervalsCount, Func<double, double> function) {
            var points = new List<Point>();
            double minAngle = FromDegrees(minAngleDegree);
            double maxAngle = FromDegrees(maxAngleDegree);
            double angleStep = (maxAngle - minAngle) / (double)intervalsCount;
            for (int pointIndex = 0; pointIndex <= intervalsCount; pointIndex++) {
                double angle = minAngle + pointIndex * angleStep;
                double angleRadians = ToRadian(angle);
                double distance = function(angleRadians);
                double normalAngle = NormalizeAngle(angle);
                points.Add(new Point(normalAngle, distance));
            }
            return points;
        }
        double ArchimedeanSpiralFunction(double angleRadians) {
            return angleRadians;
        }
        double PolarRoseFunction(double angleRadians) {
            return Math.Max(0.0, Math.Sin(roseParameter * angleRadians));
        }
        double PolarFoliumFunction(double angleRadians) {
            double sin = Math.Sin(angleRadians);
            double cos = Math.Cos(angleRadians);
            return (3.0 * sin * cos) / (Math.Pow(sin, 3.0) + Math.Pow(cos, 3.0));
        }

        public List<Point> CreateArchimedeanSpiralData() {
            return CreatePolarFunctionPoints(0.0, 720.0, spiralIntervalsCount, ArchimedeanSpiralFunction);
        }
        public List<Point> CreateRoseData() {
            return CreatePolarFunctionPoints(0.0, 1440.0, roseIntervalsCount, PolarRoseFunction);
        }
        public List<Point> CreateFoliumData() {
            var points1 = CreatePolarFunctionPoints(120.0, 180.0, foliumSegmentIntervalsCount, PolarFoliumFunction);
            var points2 = CreatePolarFunctionPoints(0.0, 90.0, foliumSegmentIntervalsCount, PolarFoliumFunction);
            var points3 = CreatePolarFunctionPoints(270.0, 330.0, foliumSegmentIntervalsCount, PolarFoliumFunction);
            return FilterPointsByRange(points1.Concat(points2).Concat(points3).ToList(), 0.0, foliumDistanceLimit);
        }
    }


    public class RadianScatterFunctionCalculator : ScatterFunctionCalculatorBase {
        protected override double NormalizeAngle(double angle) {
            return angle % (Math.PI * 2.0);
        }
        protected override double ToRadian(double angle) {
            return angle;
        }
        protected override double FromDegrees(double angle) {
            return angle * Math.PI / 180.0;
        }
    }


    public class DegreeScatterFunctionCalculator : ScatterFunctionCalculatorBase {
        protected override double NormalizeAngle(double angle) {
            return angle % 360;
        }
        protected override double ToRadian(double angle) {
            return angle * Math.PI / 180.0;
        }
        protected override double FromDegrees(double angle) {
            return angle;
        }
    }


    public class Bar2DKindToTickmarksLengthConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Bar2DKind bar2DKind = value as Bar2DKind;
            if (bar2DKind != null) {
                switch (bar2DKind.Name) {
                    case "Glass Cylinder":
                        return 18;
                    case "Quasi-3D Bar":
                        return 9;
                }
            }
            return 5;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }


    public class MarkerSizeToLabelIndentConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((double)value) / 2;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class BoolToResolveOverlappingModeConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool booleanValue = (bool)value;
            if (booleanValue == true)
                return ResolveOverlappingMode.Default;
            else
                return ResolveOverlappingMode.None;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }


    public static class PaletteSelectorHelper {
        static Palette actualPalette = new OfficePalette();

        public static Palette ActualPalette {
            get { return actualPalette; }
            set { actualPalette = value; }
        }
    }


    public static class CsvReader {
        const string FileNamePrefix = "/ChartsDemo;component/Data/";

        public static List<FinancialPoint> ReadFinancialData(string fileName) {
            string longFileName = string.Empty;
            StreamReader reader;
            var dataSource = new List<FinancialPoint>();
            try {
                longFileName = FileNamePrefix + fileName;
                Uri uri = new Uri(longFileName, UriKind.RelativeOrAbsolute);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                reader = new StreamReader(info.Stream);
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    var values = line.Split(',');
                    var point = new FinancialPoint();
                    point.DateTimeArgument = DateTime.ParseExact(values[0], "yyyy.MM.dd", CultureInfo.InvariantCulture);
                    point.OpenValue = double.Parse(values[1], CultureInfo.InvariantCulture);
                    point.HighValue = double.Parse(values[2], CultureInfo.InvariantCulture);
                    point.LowValue = double.Parse(values[3], CultureInfo.InvariantCulture);
                    point.CloseValue = double.Parse(values[4], CultureInfo.InvariantCulture);
                    dataSource.Add(point);
                }
            }
            catch {
                throw new Exception("It's impossible to load " + fileName);
            }
            return dataSource;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PixelColor {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
        public byte Gray { get { return (byte)(((int)Blue + (int)Green + (int)Red) / 3); } }
    }


    public class ImageData {
        readonly StreamResourceInfo streamResourceInfo;

        public ImageData(Uri uri) {
            this.streamResourceInfo = Application.GetResourceStream(uri);
        }
        PixelColor[,] GetPixels(BitmapSource source) {
            if(source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);
            PixelColor[,] result = new PixelColor[source.PixelWidth, source.PixelHeight];
            int stride = (int)source.PixelWidth * (source.Format.BitsPerPixel / 8);
            CopyPixels(source, result, stride, 0);
            return result;
        }
        void CopyPixels(BitmapSource source, PixelColor[,] pixels, int stride, int offset) {
            var height = source.PixelHeight;
            var width = source.PixelWidth;
            var pixelBytes = new byte[height * width * 4];
            source.CopyPixels(pixelBytes, stride, 0);
            int y0 = offset / width;
            int x0 = offset - width * y0;
            for(int y = 0; y < height; y++)
                for(int x = 0; x < width; x++)
                    pixels[x + x0, y + y0] = new PixelColor {
                        Blue = pixelBytes[(y * width + x) * 4 + 0],
                        Green = pixelBytes[(y * width + x) * 4 + 1],
                        Red = pixelBytes[(y * width + x) * 4 + 2],
                        Alpha = pixelBytes[(y * width + x) * 4 + 3],
                    };
        }
        void DoEvents() {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        }
        public PixelColor[,] GetPixels() {
            PixelColor[,] pixels = new PixelColor[0, 0];
            try {
                BitmapImage source = new BitmapImage();
                source.BeginInit();
                source.StreamSource = this.streamResourceInfo.Stream;
                source.EndInit();
                while(source.IsDownloading) {
                    DoEvents();
                }
                pixels = GetPixels(source);
            }
            catch {
            }
            return pixels;
        }
    }

    [System.Windows.Markup.ContentProperty("Content")]
    public class ValueSelectorItem : DependencyObject {
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ValueSelectorItem), new PropertyMetadata(null));

        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }
        public static readonly DependencyProperty DisplayNameProperty = DependencyProperty.Register("DisplayName", typeof(string), typeof(ValueSelectorItem), new PropertyMetadata(null));
    }

}
