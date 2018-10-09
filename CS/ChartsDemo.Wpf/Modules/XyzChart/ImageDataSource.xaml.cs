using System;
using System.ComponentModel;
using System.Windows.Media;
using DevExpress.DemoData.Utils;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Mvvm;

namespace ChartsDemo {
    [CodeFile("Modules/XyzChart/ImageDataSource.xaml"),
    CodeFile("Modules/XyzChart/ImageDataSource.xaml.(cs)")]
    public partial class ImageDataSource : ChartsDemoModule {
        public ImageDataSource() {
            InitializeComponent();
            ActualChart = chart;
        }
    }
    public class FillStyleItem : BindableBase {
        string title;
        FillStyleBase fillStyle;

        public string Title {
            get { return title; }
            set { SetProperty(ref title, value, () => Title); }
        }
        public FillStyleBase FillStyle {
            get { return fillStyle; }
            set { SetProperty(ref fillStyle, value, () => FillStyle); }
        }
    }
    public class HeightViewModel : BindableBase {
        public DoubleCollection MapValues { get; private set; }
        public DoubleCollection MapX { get; private set; }
        public DoubleCollection MapY { get; private set; }
        public static Uri HeightMapUri { get { return AssemblyHelper.GetResourceUri(typeof(ImageDataSource).Assembly, "/Data/Heightmap.jpg"); } }

        public HeightViewModel() {
            GenerateMap(HeightMapUri);
        }

        void GenerateMap(Uri uri) {
            ImageData ImageData = new ImageData(uri);
            PixelColor[,] pixels = ImageData.GetPixels();

            int countX = pixels.GetLength(0);
            int countY = pixels.GetLength(1);

            int startX = 0;
            int startY = 0;
            int gridStep = 100;
            double minY = -300;
            double maxY = 3000;

            DoubleCollection mapX = new DoubleCollection(countX);
            DoubleCollection mapY = new DoubleCollection(countY);
            DoubleCollection values = new DoubleCollection(countX * countY);
            bool fullY = false;
            for(int i = 0; i < countX; i++) {
                mapX.Add(startX + i * gridStep);
                for(int j = 0; j < countY; j++) {
                    if(!fullY)
                        mapY.Add(startY + j * gridStep);
                    double value = GetValue(pixels[i, j], minY, maxY);
                    values.Add(value);

                }
                fullY = true;
            }
            MapY = mapY;
            MapX = mapX;
            MapValues = values;
        }

        double GetValue(PixelColor color, double min, double max) {
            double normalizedValue = (double)color.Gray / 255.0;
            return min + normalizedValue * (max - min);
        }
    }
}
