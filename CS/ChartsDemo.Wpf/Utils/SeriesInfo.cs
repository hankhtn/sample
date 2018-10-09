using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace ChartsDemo {
    [ContentProperty("SeriesTemplate")]
    public class SeriesInfo : BindableBase {
        string content;
        object dataSource;
        DataTemplate seriesTemplate;
        DiagramType diagramType;

        public string Content {
            get { return content; }
            set { SetProperty(ref content, value, () => Content); }
        }
        public object DataSource {
            get { return dataSource; }
            set { SetProperty(ref dataSource, value, () => DataSource); }
        }
        public DataTemplate SeriesTemplate {
            get { return seriesTemplate; }
            set { SetProperty(ref seriesTemplate, value, () => SeriesTemplate); }
        }
        public DiagramType DiagramType {
            get { return diagramType; }
            set { SetProperty(ref diagramType, value, () => DiagramType); }
        }
    }

    public enum DiagramType {
        XY,
        Simple,
        Radar,
        Polar,
        XY3D
    }
}
