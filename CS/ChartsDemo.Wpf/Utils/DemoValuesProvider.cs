using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace ChartsDemo {
    public static class DemoValuesProvider {
        #region Models

        static readonly Marker2DModel[] predefinedMarker2DModels = CreateModels<Marker2DModel>(Marker2DModel.GetPredefinedKinds());
        static readonly Bar2DModel[] predefinedBar2DModels = CreateModels<Bar2DModel>(Bar2DModel.GetPredefinedKinds());
        static readonly CandleStick2DModel[] predefinedCandleStick2DModels = CreateModels<CandleStick2DModel>(CandleStick2DModel.GetPredefinedKinds());
        static readonly Stock2DModel[] predefinedStock2DModels = CreateModels<Stock2DModel>(Stock2DModel.GetPredefinedKinds());
        static readonly Pie2DModel[] predefinedPie2DModels = CreateModels<Pie2DModel>(Pie2DModel.GetPredefinedKinds());
        static readonly RangeBar2DModel[] predefinedRangeBarModels = CreateModels<RangeBar2DModel>(RangeBar2DModel.GetPredefinedKinds());

        public static IEnumerable<Marker2DModel> PredefinedMarker2DModels { get { return predefinedMarker2DModels; } }
        public static IEnumerable<Bar2DModel> PredefinedBar2DModels { get { return predefinedBar2DModels; } }
        public static IEnumerable<CandleStick2DModel> PredefinedCandleStick2DModels { get { return predefinedCandleStick2DModels; } }
        public static IEnumerable<Stock2DModel> PredefinedStock2DModels { get { return predefinedStock2DModels; } }
        public static IEnumerable<Pie2DModel> PredefinedPie2DModels { get { return predefinedPie2DModels; } }
        public static IEnumerable<RangeBar2DModel> PredefinedRangeBar2DModels { get { return predefinedRangeBarModels; } }
        public static IEnumerable<Marker3DPointModel> PredefinedBubble3DModels {
            get {
                yield return new Marker3DCubePointModel();
                yield return new Marker3DSpherePointModel();
            }
        }

        #endregion
        #region Models Instances

        public static CrossMarker2DModel PredefinedCrossMarker2DModel { get { return predefinedMarker2DModels.OfType<CrossMarker2DModel>().First(); } }
        public static RingMarker2DModel PredefinedRingMarker2DModel { get { return predefinedMarker2DModels.OfType<RingMarker2DModel>().First(); } }
        public static CircleMarker2DModel PredefinedCircleMarker2DModel { get { return predefinedMarker2DModels.OfType<CircleMarker2DModel>().First(); } }

        public static FlatGlassBar2DModel PredefinedFlatGlassBar2DModel { get { return predefinedBar2DModels.OfType<FlatGlassBar2DModel>().First(); } }
        public static SimpleBar2DModel PredefinedSimpleBar2DModel { get { return predefinedBar2DModels.OfType<SimpleBar2DModel>().First(); } }

        public static OutsetRangeBar2DModel PredefinedOutsetRangeBar2DModel { get { return predefinedRangeBarModels.OfType<OutsetRangeBar2DModel>().First(); } }
        public static ThinStock2DModel PredefinedThinStock2DModel { get { return predefinedStock2DModels.OfType<ThinStock2DModel>().First(); } }
        public static SimplePie2DModel PredefinedSimplePie2DModel { get { return predefinedPie2DModels.OfType<SimplePie2DModel>().First(); } }
        public static SimpleCandleStick2DModel PredefinedSimpleCandleStick2DModel { get { return predefinedCandleStick2DModels.OfType<SimpleCandleStick2DModel>().First(); } }

        #endregion

        public static T[] CreateModels<T>(IEnumerable<PredefinedElementKind> kinds) where T : DependencyObject {
            return kinds.Select(x => ((T)Activator.CreateInstance(x.Type))).ToArray();
        }
    }
    public abstract class Base3DModelsProvider<T> : INotifyPropertyChanged where T : DependencyObject {
        T[] predefined3DModels;
        public IEnumerable<T> Predefined3DModels { get { return predefined3DModels ?? (predefined3DModels = DemoValuesProvider.CreateModels<T>(GetPredefinedKinds())); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName) {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected abstract IEnumerable<PredefinedElementKind> GetPredefinedKinds();
    }
    public class Pie3DModelsProvider : Base3DModelsProvider<Pie3DModel> {
        public PentagonPie3DModel PredefinedPentagonPie3DModel { get { return Predefined3DModels.OfType<PentagonPie3DModel>().First(); } }

        protected override IEnumerable<PredefinedElementKind> GetPredefinedKinds() {
            return Pie3DModel.GetPredefinedKinds();
        }
    }
    public class Marker3DModelsProvider : Base3DModelsProvider<Marker3DModel> {
        public SphereMarker3DModel PredefinedSphereMarker3DModel { get { return Predefined3DModels.OfType<SphereMarker3DModel>().First(); } }

        protected override IEnumerable<PredefinedElementKind> GetPredefinedKinds() {
            return Marker3DModel.GetPredefinedKinds();
        }
    }
    public class Bar3DModelsProvider : Base3DModelsProvider<Bar3DModel> {
        public BoxBar3DModel PredefinedBoxBar3DModel { get { return Predefined3DModels.OfType<BoxBar3DModel>().First(); } }
        public CylinderBar3DModel PredefinedCylinderBar3DModel { get { return Predefined3DModels.OfType<CylinderBar3DModel>().First(); } }

        protected override IEnumerable<PredefinedElementKind> GetPredefinedKinds() {
            return Bar3DModel.GetPredefinedKinds();
        }
    }
}
