using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;
using System.Xml.Linq;
using DevExpress.Mvvm;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class DailyWeatherViewModel {
        const string vostokStationName = "Vostok Station";
        const string deathValleyName = "Death Valley, NV";

        readonly List<WeatherItem> weather;

        public List<WeatherItem> Weather { get { return weather; } }

        public DailyWeatherViewModel() {
            List<WeatherRecord> valleyData = LoadWeatherData("DeathValley.xml");
            List<WeatherRecord> vostokData = LoadWeatherData("VostokStation.xml");

            Palette palette = new OfficePalette();
            weather = new List<WeatherItem>() {
                new WeatherItem(valleyData, false, palette[1], deathValleyName),
                new WeatherItem(valleyData, true, palette[1], deathValleyName),
                new WeatherItem(vostokData, false, palette[0], vostokStationName),
                new WeatherItem(vostokData, true, palette[0], vostokStationName),
            };
        }
        List<WeatherRecord> LoadWeatherData(string fileName) {
            List<WeatherRecord> items = new List<WeatherRecord>();
            XDocument weatherDocument = DataLoader.LoadXmlFromResources("/Data/DailyWeather/" + fileName);
            foreach (XElement element in weatherDocument.Root.Elements("Weather")) {
                items.Add(WeatherRecord.Load(element));
            }
            return items;
        }
        public virtual IChartAnimationService ChartAnimationService { get { return null; } }
        public void OnModuleLoaded() {
            if (ChartAnimationService != null)
                ChartAnimationService.Animate();
        }
    }

    public class WeatherItem : BindableBase {
        public int AverageLineThickness {
            get { return GetProperty(() => AverageLineThickness); }
            set { SetProperty(() => AverageLineThickness, value); }
        }
        public List<WeatherRecord> Data { get; private set; }
        public bool IsAverageWeather { get; private set; }
        public Color Color { get; private set; }
        public string Name { get; private set; }

        public WeatherItem(List<WeatherRecord> data, bool isAverageWeather, Color color, string name) {
            this.Data = data;
            this.IsAverageWeather = isAverageWeather;
            this.Color = color;
            this.Name = name;
            AverageLineThickness = 2;
        }
    }

    public class WeatherRecord {
        public static WeatherRecord Load(XElement element) {
            CultureInfo culture = CultureInfo.InvariantCulture;
            DateTime _date = DateTime.Parse(element.Attribute("Date").Value, culture);
            double min = Double.Parse(element.Attribute("Min").Value, culture);
            double max = Double.Parse(element.Attribute("Max").Value, culture);
            double avg = Double.Parse(element.Attribute("Avg").Value, culture);
            return new WeatherRecord(_date, max, avg, min);
        }

        DateTime date;

        public double MinValue { get; private set; }
        public double MaxValue { get; private set; }
        public double AvgValue { get; private set; }
        public DateTime Date { get { return date; } private set { date = value; } }

        WeatherRecord(DateTime _date, double maxValue, double avgValue, double minValue) {
            this.Date = _date;
            this.MaxValue = maxValue;
            this.AvgValue = avgValue;
            this.MinValue = minValue;
        }
    }
}
