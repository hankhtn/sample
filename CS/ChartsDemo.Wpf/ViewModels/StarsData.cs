using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Resources;

namespace ChartsDemo {
    public static class StarsData {
        static List<StarData> starsData;
        public static List<StarData> Data { get { return starsData ?? (starsData = GetStarsData()); } }

        static List<StarData> GetStarsData() {
            string fileName = "starsdata.csv";
            var dataSource = new List<StarData>();
            try {
                string path = "/ChartsDemo;component/Data/" + fileName;
                StreamResourceInfo info = Application.GetResourceStream(new Uri(path, UriKind.RelativeOrAbsolute));
                using(StreamReader reader = new StreamReader(info.Stream)) {
                    while(!reader.EndOfStream) {
                        string line = reader.ReadLine();
                        var values = line.Split(';');
                        StarData data = new StarData();
                        data.HipID = int.Parse(values[0], CultureInfo.InvariantCulture);
                        data.Spectr = values[1];
                        data.CI = double.Parse(values[2], CultureInfo.InvariantCulture);
                        data.X = double.Parse(values[3], CultureInfo.InvariantCulture);
                        data.Y = double.Parse(values[4], CultureInfo.InvariantCulture);
                        data.Z = double.Parse(values[5], CultureInfo.InvariantCulture);
                        data.Lum = double.Parse(values[6], CultureInfo.InvariantCulture);
                        dataSource.Add(data);
                    }
                }
            }
            catch {
                throw new Exception("It's impossible to load " + fileName);
            }
            return dataSource;
        }
    }

    public struct StarData {
        public int HipID { get; set; }
        public string Spectr { get; set; }
        public double Lum { get; set; }
        public double CI { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
