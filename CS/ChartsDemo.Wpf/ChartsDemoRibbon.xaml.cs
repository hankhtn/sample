using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class ChartsDemoRibbon : UserControl {
        public static readonly DependencyProperty ChartProperty =
            DependencyProperty.Register("Chart", typeof(ChartControlBase), typeof(ChartsDemoRibbon), new PropertyMetadata(null, OnChartChanged));

        static void OnChartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ChartControlBase chart = e.NewValue as ChartControlBase;
            if (chart != null)
                chart.Palette = SavedPalette;
        }

        public ChartControlBase Chart {
            get { return (ChartControlBase)GetValue(ChartProperty); }
            set { SetValue(ChartProperty, value); }
        }

        static Palette SavedPalette { get; set; }

        static ChartsDemoRibbon() {
            SavedPalette = new OfficePalette();
        }

        public ChartsDemoRibbon() {
            InitializeComponent();
            DataContext = new PalettesViewModel(this);
        }

        class PaletteViewModel : BindableBase {
            readonly PalettesViewModel owner;
            readonly PaletteKind kind;
            bool isActualPalette;

            public string Name { get { return kind.Name; } }
            public bool IsActualPalette {
                get { return isActualPalette; }
                set {
                    if (isActualPalette != value) {
                        SetProperty(ref isActualPalette, value, () => IsActualPalette);
                        if (value)
                            owner.SetActualPalette(this);
                    }
                }
            }

            public PaletteViewModel(PaletteKind kind, PalettesViewModel owner) {
                this.kind = kind;
                this.owner = owner;
                this.isActualPalette = kind.Type == SavedPalette.GetType();
            }
            public Palette CreatePalette() {
                Palette palette = (Palette)Activator.CreateInstance(kind.Type);
                palette.ColorCycleLength = 10;
                return palette;
            }
        }

        class PalettesViewModel : BindableBase {
            PaletteViewModel[] palettes;
            WeakReference owner;

            public PaletteViewModel[] Palettes { get { return palettes; } }

            public PalettesViewModel(ChartsDemoRibbon owner) {
                this.owner = new WeakReference(owner);
                palettes = Palette.GetPredefinedKinds().Select(x => new PaletteViewModel(x, this)).ToArray();
            }
            public void SetActualPalette(PaletteViewModel viewModel) {
                foreach (PaletteViewModel paletteModel in palettes)
                    paletteModel.IsActualPalette = viewModel == paletteModel;
                ChartsDemoRibbon ribbon = owner.IsAlive ? owner.Target as ChartsDemoRibbon : null;
                if (ribbon != null)
                    ribbon.Chart.Palette = SavedPalette = viewModel.CreatePalette();
            }
        }
    }
}
