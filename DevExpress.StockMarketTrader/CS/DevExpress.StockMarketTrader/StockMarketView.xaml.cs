using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using DevExpress.StockMarketTrader.ViewModel;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;

namespace DevExpress.StockMarketTrader {
    public partial class StockMarketView : UserControl {
        const int CandleCountMin = 20;

        bool isDrawingMode = false;
        bool isRemoving = false;
        int daysCount, selectedPiePointIndex = 0, workspaceNumber = 1;
        string currentWorkspace = "Workspace1", selectedCompanyName;
        DispatcherTimer updateTimer;
        List<int> ticks = new List<int>() { 5, 10, 20 };
        IWorkspaceManager Manager;
        NotFoundWindow notFoundWindow;
        FinancialIndicator drawingIndicator;
        FinancialIndicator indicatorToRemove;

        NotFoundWindow NotFoundWindow {
            get {
                if (notFoundWindow != null)
                    notFoundWindow.Close();
                notFoundWindow = new NotFoundWindow();
                notFoundWindow.Owner = App.Current.Windows[0];
                return notFoundWindow;
            }
        }
        XYDiagram2D StockDiagram { get { return (XYDiagram2D)stockChart.Diagram; } }
        CandleStickSeries2D PriceSeries { get { return (CandleStickSeries2D)StockDiagram.Series[0]; } }
        BarSeries2D VolumeSeries { get { return (BarSeries2D)StockDiagram.Series[1]; } }
        internal Pane PricePane { get { return StockDiagram.DefaultPane; } }
        internal Pane IndicatorPane { get { return StockDiagram.Panes[1]; } }
        internal SecondaryAxisY2D IndicatorAxisY { get { return StockDiagram.SecondaryAxesY[1]; } }
        internal Legend IndicatorLegend { get { return stockChart.Legends[2]; } }
        internal AxisX2D StockAxisX { get { return StockDiagram.AxisX; } }

        public StockMarketView() {
            DevExpress.Xpf.Bars.BarManager.CheckBarItemNames = false;
            DevExpress.Xpf.Grid.GridControl.AllowInfiniteGridSize = true;
            InitializeComponent();
            SetWorkspaceManager();
            bcDisplayPeriod3.IsChecked = true;

            ecIndicators.Items.Add(new LinearRegressionItem());

            ecIndicators.Items.Add(new SimpleMovingAverageItem());
            ecIndicators.Items.Add(new ExponentialMovingAverageItem());
            ecIndicators.Items.Add(new TripleExponentialMovingAverageItem());
            ecIndicators.Items.Add(new TriangularMovingAverageItem());
            ecIndicators.Items.Add(new WeightedMovingAverageItem());

            ecIndicators.Items.Add(new MedianPriceItem());
            ecIndicators.Items.Add(new TypicalPriceItem());
            ecIndicators.Items.Add(new WeightedCloseItem());
            ecIndicators.Items.Add(new AverageTrueRangeItem());
            ecIndicators.Items.Add(new CommodityChannelIndexItem());
            ecIndicators.Items.Add(new DetrendedPriceOscillatorItem());
            ecIndicators.Items.Add(new MassIndexItem());
            ecIndicators.Items.Add(new MovingAverageConvergenceDivergenceItem());
            ecIndicators.Items.Add(new RateOfChangeItem());
            ecIndicators.Items.Add(new RelativeStrengthIndexItem());
            ecIndicators.Items.Add(new StandardDeviationItem());
            ecIndicators.Items.Add(new ChaikinsVolatilityItem());
            ecIndicators.Items.Add(new WilliamsRItem());
        }
        void SetStockChartVisible() {
            stockChart.Visibility = (bcCandleChartVisible.IsChecked == false && bcBarsChartVisible.IsChecked == false) ?
                System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }
        void OnStockChartBoundDataChanged(object sender, RoutedEventArgs e) {
            if (PriceSeries.Points.Count > 0) {
                SeriesPoint lastPoint = PriceSeries.Points[PriceSeries.Points.Count - 1];
                double value = FinancialSeries2D.GetCloseValue(lastPoint);
                CustomAxisLabel priceLabel = StockDiagram.AxisY.CustomLabels[0];
                priceLabel.Value = value;
                if (priceLabel.Content != null) {
                    TextBlock priceLabelText = (TextBlock)((Border)priceLabel.Content).Child;
                    priceLabelText.Text = String.Format("${0:F1}", value);
                }
            }
        }
        void OnDisplayPeriodChanged(object sender, ItemClickEventArgs e) {
            if (((BarCheckItem)sender).IsChecked != false) {
                RealTimeDataViewModel rtdvm = DataContext as RealTimeDataViewModel;
                string content = ((BarCheckItem)sender).Content.ToString();
                daysCount = StringToIntConvert(content[0], content[1]);
                List<string> temp = new List<string>();
                foreach (int tick in ticks) {
                    int numberOfCandles = daysCount / tick;
                    if (numberOfCandles >= CandleCountMin)
                        temp.Add(IntToStringConvert(tick));
                }
                ecTicks.ItemsSource = temp;
                bTicks.EditValue = String.Empty;
                bTicks.EditValue = temp[0];
            }
        }
        void OnTicksChanged(object sender, RoutedEventArgs e) {
            RealTimeDataViewModel rtdvm = DataContext as RealTimeDataViewModel;
            if (bTicks.EditValue.ToString() != String.Empty && rtdvm != null) {
                string value = bTicks.EditValue.ToString();
                int numberOfTicks = StringToIntConvert(value[0], value[2]);
                int newCandlesCount = daysCount / numberOfTicks;
                if (rtdvm.Ticks != numberOfTicks || rtdvm.CandlesCount != newCandlesCount) {
                    rtdvm.Ticks = numberOfTicks;
                    rtdvm.CandlesCount = newCandlesCount;
                    rtdvm.OnCandlesCountChanged();
                    ManualDateTimeScaleOptions scaelOptions = (ManualDateTimeScaleOptions)StockAxisX.DateTimeScaleOptions;
                    switch (numberOfTicks) {
                        case 5:
                            scaelOptions.MeasureUnit = DateTimeMeasureUnit.Week;
                            VolumeSeries.BarWidth = 0.6;
                            PriceSeries.CandleWidth = 0.6;
                            break;
                        case 10:
                            scaelOptions.MeasureUnit = DateTimeMeasureUnit.Week;
                            VolumeSeries.BarWidth = 1.2;
                            PriceSeries.CandleWidth = 1.2;
                            break;
                        case 20:
                            scaelOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                            VolumeSeries.BarWidth = 0.6;
                            PriceSeries.CandleWidth = 0.6;
                            break;
                    }
                }
            }
        }
        void OnCandlesChartVisibleChanged(object sender, ItemClickEventArgs e) {
            string scale = bcCandleChartVisible.IsChecked.Value ? "2" : "0";
            RealTimeDataViewModel rtdvm = DataContext as RealTimeDataViewModel;
            rtdvm.ChartScale = scale + rtdvm.ChartScale[1] + rtdvm.ChartScale[2];
            SetStockChartVisible();
        }
        void OnBarsChartVisibleChanged(object sender, ItemClickEventArgs e) {
            string scale = bcBarsChartVisible.IsChecked.Value ? "1" : "0";
            RealTimeDataViewModel rtdvm = DataContext as RealTimeDataViewModel;
            rtdvm.ChartScale = rtdvm.ChartScale[0] + scale + rtdvm.ChartScale[2];
            SetStockChartVisible();
        }
        void OnHomeItemClick(object sender, ItemClickEventArgs e) {
            System.Diagnostics.Process.Start("http://www.devexpress.com");
        }
        void OnHyperlinkClick(object sender, RequestNavigateEventArgs e) {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
        void SetWorkspaceManager() {
            Manager = WorkspaceManager.GetWorkspaceManager(dockManager);
            Manager.TransitionEffect = TransitionEffect.Fade;
            Manager.AfterApplyWorkspace += Manager_AfterApplyWorkspace;
            LoadPredefinedWorkspaces();
        }
        void AddBarButtonItem() {
            Xpf.Bars.BarCheckItem newWorkspaceBarButton;
            BarItemLink newWorkspaceBarButtonLink;
            CreateBarButtonItem(out newWorkspaceBarButton, out newWorkspaceBarButtonLink);
            barManager.Items.Add(newWorkspaceBarButton);
            bWorkspaces.ItemLinks.Add(newWorkspaceBarButtonLink);
        }
        void DeleteBarButtonItem() {
            string name = "bb" + currentWorkspace;
            for (int i = 0; i < barManager.Items.Count; i++) {
                BarItem bi = barManager.Items[i];
                if (typeof(BarButtonItem) == bi.GetType())
                    if (bi.Name == name) {
                        barManager.Items.Remove(barManager.Items[i]);
                        break;
                    }
            }
            int count = bWorkspaces.ItemLinks.Count;
            for (int i = 0; i < count; i++) {
                BarItemLink bil = (BarItemLink)bWorkspaces.ItemLinks[i];
                if (bil.BarItemName == name) {
                    bWorkspaces.ItemLinks.Remove(bil);
                    break;
                }
            }

        }
        void CreateBarButtonItem(out Xpf.Bars.BarCheckItem newWorkspaceBarButton, out BarItemLink newWorkspaceBarButtonLink) {
            newWorkspaceBarButton = new BarCheckItem();
            newWorkspaceBarButton.Name = "bb" + currentWorkspace;
            newWorkspaceBarButton.Content = currentWorkspace;
            newWorkspaceBarButton.GroupIndex = 0;
            newWorkspaceBarButton.ItemClick += new ItemClickEventHandler(OnChangeWorkSpaceItemClick);
            newWorkspaceBarButtonLink = new BarItemLink();
            newWorkspaceBarButtonLink.BarItemName = newWorkspaceBarButton.Name;
        }
        void LoadPredefinedWorkspaces() {
            for (int i = 0; i < 2; i++) {
                string assemblyName = "DevExpress.StockMarketTrader";
                string workspaceName = "Workspace" + workspaceNumber;
                string resourceName = String.Format("{0}.WorkSpaces.{1}.xml", assemblyName, workspaceName);
                Stream workspaceStream = typeof(StockMarketView).Assembly.GetManifestResourceStream(resourceName);
                Manager.LoadWorkspace(workspaceName, workspaceStream);
                workspaceNumber++;
            }
        }
        void OnAddWorkspace(object sender, ItemClickEventArgs e) {
            AddWorkspace();
        }
        void AddWorkspace() {
            if (bWorkspaces.IsEnabled == false)
                bWorkspaces.IsEnabled = true;
            currentWorkspace = string.Format("Workspace{0}", workspaceNumber);
            Manager.CaptureWorkspace(currentWorkspace);
            AddBarButtonItem();
            workspaceNumber++;
            
        }
        void WriteWorkspaceInFile() {
            if (!Directory.Exists("WorkSpaces"))
                Directory.CreateDirectory("WorkSpaces");
            string fileName = "WorkSpaces\\" + currentWorkspace + ".xml";
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (StreamWriter writer = new StreamWriter(fileName, true)) {
                byte[] data = Manager.Workspaces[Manager.Workspaces.Count - 1].SerializationData as byte[];
                for (int i = 0; i < data.Length; i++) {
                    char symbol = (char)data[i];
                    writer.Write(symbol);
                }
                writer.Close();
            }
        }
        void OnDeleteWorkspace(object sender, ItemClickEventArgs e) {
            DeleteWorkspace();
        }
        void DeleteWorkspace() {
            int count = bWorkspaces.ItemLinks.Count;
            if (count > 0) {
                Manager.RemoveWorkspace(currentWorkspace);
                DeleteBarButtonItem();
                count--;
                if (count == 0) {
                    workspaceNumber = 1;
                    bWorkspaces.IsEnabled = false;
                    currentWorkspace = String.Empty;
                }
                else {
                    BarItemLink link = (BarItemLink)bWorkspaces.ItemLinks[count - 1];
                    currentWorkspace = link.BarItemName.Replace("bb", "");
                }
            }
        }
        void OnChangeWorkSpaceItemClick(object sender, ItemClickEventArgs e) {
            string newWorkspace = ((Xpf.Bars.BarCheckItem)sender).Content.ToString();
            currentWorkspace = newWorkspace;
            ApplyWorkspace();
        }
        void Manager_AfterApplyWorkspace(object sender, EventArgs e) {
            stockChart.Legends[0].DockTarget = StockDiagram.DefaultPane;
            stockChart.Legends[1].DockTarget = StockDiagram.Panes[0];
            stockChart.Legends[2].DockTarget = StockDiagram.Panes[1];
            StockDiagram.PanesPanel = FindResource("panesTemplate") as ItemsPanelTemplate;
            StockDiagram.AxisY.CustomLabels[0].Content = FindResource("customLabels");
            StockDiagram.DefaultPane.SetValue(Grid.RowProperty, 0);
            StockDiagram.Panes[0].SetValue(Grid.RowProperty, 2);
            StockDiagram.Panes[1].SetValue(Grid.RowProperty, 4);
            UpdateIndicators(true);
        }
        void ApplyWorkspace() {
            Manager.TransitionEffect = (TransitionEffect)((RealTimeDataViewModel)DataContext).TransitionEffect;
            Manager.ApplyWorkspace(currentWorkspace);
        }
        void OnVolumeChartMouseDown(object sender, MouseButtonEventArgs e) {
            ChartHitInfo hitInfo = volumeChart.CalcHitInfo(e.GetPosition(volumeChart));
            if (hitInfo == null || hitInfo.SeriesPoint == null)
                return;
            double distance = PieSeries.GetExplodedDistance(hitInfo.SeriesPoint);
            Storyboard storyBoard = new Storyboard();
            storyBoard = SetStoryboard(storyBoard, 0, selectedPiePointIndex);
            double animationDistance = distance > 0 ? 0 : 0.15;
            int currentPointIndex = volumeChart.Diagram.Series[0].Points.IndexOf(hitInfo.SeriesPoint);
            storyBoard = SetStoryboard(storyBoard, animationDistance, currentPointIndex);
            if (distance == 0) {
                selectedPiePointIndex = currentPointIndex;
                selectedCompanyName = hitInfo.SeriesPoint.Argument.ToString();
                RealTimeDataViewModel rtdvm = DataContext as RealTimeDataViewModel;
                rtdvm.SetSelectedCompany(selectedCompanyName);
            }
            else
                selectedPiePointIndex = -1;
            storyBoard.Begin();
        }
        void OnVolumeChartBoundDataChanged(object sender, RoutedEventArgs e) {
            if (volumeChart.Diagram.Series[0].Points.Count > 0 && selectedPiePointIndex != -1 && volumeChart.Diagram.Series[0].Points.Count > selectedPiePointIndex) {
                for (int i = 0; i < volumeChart.Diagram.Series[0].Points.Count; i++) {
                    if (selectedCompanyName == volumeChart.Diagram.Series[0].Points[i].Argument.ToString()) {
                        volumeChart.Diagram.Series[0].Points[i].SetValue(PieSeries.ExplodedDistanceProperty, 0.15);
                        selectedPiePointIndex = i;
                        break;
                    }
                }
            }
        }
        void OnWatchListGridSelectedItemChanged(object sender, SelectedItemChangedEventArgs e) {
            if (e.NewItem == null)
                return;
            CompanyTradingDataViewModel ctdvm = e.NewItem as CompanyTradingDataViewModel;
            selectedCompanyName = ctdvm.CompanyName;
            if (volumeChart.Diagram.Series[0].Points.Count > 0 && ctdvm != null) {
                ChangeSelectedPiePoint(ctdvm);
            }
        }
        void ChangeSelectedPiePoint(CompanyTradingDataViewModel ctdvm) {
            SetAnimation(selectedPiePointIndex);
        }
        void SetAnimation(int index) {
            if (index == -2) {
                selectedPiePointIndex = -1;
                Storyboard storyBoard = new Storyboard();
                storyBoard = SetStoryboard(storyBoard, 0, selectedPiePointIndex);
                storyBoard.Begin();
                return;
            }
            if (index != selectedPiePointIndex) {
                Storyboard storyBoard = new Storyboard();
                SetStoryboard(storyBoard, 0, selectedPiePointIndex);
                selectedPiePointIndex = index;
                storyBoard = SetStoryboard(storyBoard, 0.15, index);
                storyBoard.Begin();
            }
        }
        void OnAboutItemClick(object sender, ItemClickEventArgs e) {
            AboutWindow window = new AboutWindow();
            window.ShowDialog();
        }
        void watchListGrid_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Down || e.Key == Key.Up) {
                if (updateTimer != null) updateTimer.Stop();
                updateTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
                RealTimeDataViewModel viewModel = DataContext as RealTimeDataViewModel;
                viewModel.LockUpdate();
                updateTimer.Tick += (s, a) => {
                    updateTimer.Stop();
                    viewModel.UnLockUpdate();
                };
                updateTimer.Start();
            }
        }
        void OnWatchListViewShowFilterPopup(object sender, FilterPopupEventArgs e) {
            if (e.Column.FieldName == "CompanyName") {
                List<object> items =  new List<object>((List<object>)e.ComboBoxEdit.ItemsSource);
                for (int i = 0; i < items.Count; i++) {
                    if ((items[i] is CustomComboBoxItem) && (string.Equals(((CustomComboBoxItem)items[i]).DisplayValue, "(Blanks)") || string.Equals(((CustomComboBoxItem)items[i]).DisplayValue, "(Non blanks)"))) {
                        items.Remove(items[i]);
                        i--;
                    }
                }
                e.ComboBoxEdit.ItemsSource = items;
            }
        }
        void bcBarsChartDrawing_CheckedChanged(object sender, ItemClickEventArgs e) {
            isDrawingMode = bcBarsChartTrendLine.IsChecked.Value || bcBarsChartFibonacciArcs.IsChecked.Value ||
                bcBarsChartFibonacciFans.IsChecked.Value || bcBarsChartFibonacciRetracement.IsChecked.Value;
        }
        void bcBarsChartRemoveIndicator_CheckedChanged(object sender, ItemClickEventArgs e) {
            isRemoving = bcBarsChartRemoveIndicator.IsChecked.Value;
            stockChart.CrosshairEnabled = !isRemoving;
        }
        void stockChart_MouseDown(object sender, MouseButtonEventArgs e) {
            if (isDrawingMode) {
                stockChart.CrosshairEnabled = false;
                Point point = e.GetPosition(stockChart);
                ChartHitInfo hitInfo = stockChart.CalcHitInfo(point);
                if (hitInfo.Pane == PricePane) {
                    DiagramCoordinates coords = StockDiagram.PointToDiagram(point);
                    if (coords != null && !coords.IsEmpty) {
                        stockChart.BeginInit();
                        drawingIndicator = CreateIndicator();
                        if (drawingIndicator != null) {
                            PriceSeries.Indicators.Add(drawingIndicator);
                            drawingIndicator.Argument1 = coords.DateTimeArgument;
                            drawingIndicator.ValueLevel1 = ValueLevel.Close;
                            drawingIndicator.ValueLevel2 = ValueLevel.Close;
                            drawingLine.Value = coords.DateTimeArgument;
                            drawingLine.Visible = true;
                            stockChart.EndInit();
                            stockChart.CaptureMouse();
                        }
                    }
                }
            }
            else if (isRemoving && indicatorToRemove != null) {
                PriceSeries.Indicators.Remove(indicatorToRemove);
                indicatorToRemove = null;
            }
        }
        void stockChart_MouseMove(object sender, MouseEventArgs e) {
            if (isDrawingMode && drawingIndicator != null) {
                Point point = e.GetPosition(stockChart);
                ChartHitInfo hitInfo = stockChart.CalcHitInfo(point);
                if (hitInfo.Pane == PricePane) {
                    DiagramCoordinates coords = StockDiagram.PointToDiagram(point);
                    if (coords != null && !coords.IsEmpty) {
                        drawingLine.Value = coords.DateTimeArgument;
                        drawingIndicator.Argument2 = coords.DateTimeArgument;
                    }
                }
            }
            if (isRemoving) {
                Point point = e.GetPosition(stockChart);
                ChartHitInfo hitInfo = stockChart.CalcHitInfo(point);
                FinancialIndicator indicator = hitInfo.Indicator as FinancialIndicator;
                if (indicator != null) {
                    indicator.Brush = Brushes.White;
                    if (indicatorToRemove != null && indicatorToRemove != indicator)
                        indicatorToRemove.Brush = null;
                    indicatorToRemove = indicator;
                }
                else if (indicatorToRemove != null) {
                    indicatorToRemove.Brush = null;
                    indicatorToRemove = null;
                }
            }
        }
        void stockChart_MouseUp(object sender, MouseButtonEventArgs e) {
            if (isDrawingMode && drawingIndicator != null) {
                drawingLine.Visible = false;
                drawingIndicator = null;
                stockChart.CrosshairEnabled = true;
                stockChart.ReleaseMouseCapture();
            }
        }
        void UpdateIndicators(bool invalidate) {
            CheckedIndicatorItem selectedItem = bIndicators.EditValue as CheckedIndicatorItem;
            foreach (CheckedIndicatorItem item in ecIndicators.Items)
                if (item != selectedItem)
                    item.UpdateIndicator(this, PriceSeries, false, invalidate);
            if (selectedItem != null)
                selectedItem.UpdateIndicator(this, PriceSeries, true, invalidate);

            RealTimeDataViewModel rtdvm = DataContext as RealTimeDataViewModel;
            if (rtdvm != null)
                IndicatorAxisY.Visible = rtdvm.ChartScale[2] != '0';
        }
        void OnIndicatorChanged(object sender, RoutedEventArgs e) {
            UpdateIndicators(false);
        }
        FinancialIndicator CreateIndicator() {
            if (bcBarsChartTrendLine.IsChecked.Value)
                return new TrendLine();
            if (bcBarsChartFibonacciArcs.IsChecked.Value)
                return new FibonacciArcs();
            if (bcBarsChartFibonacciFans.IsChecked.Value)
                return new FibonacciFans();
            if (bcBarsChartFibonacciRetracement.IsChecked.Value)
                return new FibonacciRetracement();
            return null;
        }
        int StringToIntConvert(char symbol1, char symbol2) {
            int factor = 0;
            switch (symbol2) {
                case 'd':
                    factor = 1;
                    break;
                case 'w':
                    factor = 5;
                    break;
                case 'm':
                    factor = 20;
                    break;
                case 'y':
                    factor = 240;
                    break;
                case '.':
                    return 360;
            }
            return factor * int.Parse(symbol1.ToString());
        }
        string IntToStringConvert(int tick) {
            string result;
            if (tick <= 10)
                result = tick / 5 + " week";
            else
                result = 1 + " month";
            return result;
        }
        System.Drawing.Image RenderTreeToImage(UIElement userControl) {
            double height = userControl.RenderSize.Height;
            double width = userControl.RenderSize.Width;
            if (height == 0 || width == 0) return new System.Drawing.Bitmap(1, 1);
            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Default);
            VisualBrush sourceBrush = new VisualBrush(userControl);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            using (drawingContext) {
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(width, height)));
            }
            renderTarget.Render(drawingVisual);
            using (MemoryStream ms = new MemoryStream()) {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                encoder.Save(ms);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                return img;
            }
        }
        Storyboard SetStoryboard(Storyboard storyBoard, double distance, int index) {
            SeriesPointCollection points = volumeChart.Diagram.Series[0].Points;
            if (points.Count > index && index >= 0) {
                DoubleAnimation animation = new DoubleAnimation();
                animation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 300));
                animation.To = distance;
                storyBoard.Children.Add(animation);
                Storyboard.SetTarget(animation, volumeChart.Diagram.Series[0].Points[index]);
                Storyboard.SetTargetProperty(animation, new PropertyPath(PieSeries.ExplodedDistanceProperty));
            }
            return storyBoard;
        }
        internal void UpdateIndicatorPaneScale(bool visible) {
            string scale = visible ? "1" : "0";
            RealTimeDataViewModel rtdvm = DataContext as RealTimeDataViewModel;
            rtdvm.ChartScale = rtdvm.ChartScale[0].ToString() + rtdvm.ChartScale[1].ToString() + scale;
        }
    }

    #region Converters
    public class TotalCellTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            GridCellData cellData = item as GridCellData;
            if (cellData != null) {
                try {
                    decimal value = Convert.ToDecimal(cellData.Value);
                    FrameworkElement presenter = container as FrameworkElement;
                    if (presenter != null) {
                        return ((GridViewBase)cellData.View).Grid.Resources["totalCellTemplate1"] as DataTemplate;
                    }
                }
                catch (FormatException) { }
            }
            return base.SelectTemplate(item, container);
        }
    }

    public class BoolReverseConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return !((bool)value);

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(bool)value;
        }
    }

    public class BoolToOpacityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (bool)value == true ? 0 : 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class DecimalToBoolConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (typeof(double) == value.GetType()) {
                double temp = Math.Round(Double.Parse(parameter.ToString(), CultureInfo.InvariantCulture), 1);
                return (double)value == temp ? true : false;
            }
            return value.ToString() == parameter.ToString() ? true : false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return Math.Round(Double.Parse(parameter.ToString(), CultureInfo.InvariantCulture), 1);
        }
    }

    public class DecimalToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return Double.Parse(value.ToString(), CultureInfo.InvariantCulture);
        }
    }

    public class IntToBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Color red = Color.FromArgb(255, 255, 76, 118);
            Color green = Color.FromArgb(255, 103, 223, 43);
            Color color;
            if (int.Parse(value.ToString()) == 0)
                color = red;
            else
                color = green;
            return new SolidColorBrush(color);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class IntToOpacityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (double.Parse(value.ToString()) == 0)
                return 0;
            else
                return 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class StringToGridLengthConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return new GridLength(1, GridUnitType.Star);
            string str = value as string;
            int scale;
            string paramString = parameter.ToString();
            if (paramString == "Candle")
                scale = Int32.Parse(str[0].ToString());
            else if (paramString == "Bar")
                scale = Int32.Parse(str[1].ToString());
            else
                scale = Int32.Parse(str[2].ToString());
            return new GridLength(scale, GridUnitType.Star);

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region Indicator Items
    public abstract class CheckedIndicatorItem {
        bool indicatorVisible;

        protected abstract string Name { get; }
        protected virtual string IndicatorName { get { return Name; } }

        protected abstract Indicator CreateIndicator();
        public void UpdateIndicator(StockMarketView view, FinancialSeries2D series, bool indicatorVisible, bool invalidate) {
            if (this.indicatorVisible != indicatorVisible || invalidate) {
                if (indicatorVisible) {
                    Indicator indicator = CreateIndicator();
                    if (indicator != null) {
                        indicator.Tag = GetHashCode();
                        indicator.LegendText = Name;
                        indicator.ShowInLegend = true;
                        indicator.LineStyle = new LineStyle(2);
                        indicator.Brush = Brushes.White;
                        series.Indicators.Add(indicator);
                        SeparatePaneIndicator separatePaneIndicator = indicator as SeparatePaneIndicator;
                        if (separatePaneIndicator != null) {
                            separatePaneIndicator.Pane = view.IndicatorPane;
                            separatePaneIndicator.AxisY = view.IndicatorAxisY;
                            view.StockAxisX.VisibilityInPanes[1].Visible = false;
                            view.StockAxisX.VisibilityInPanes[2].Visible = true;
                            view.UpdateIndicatorPaneScale(true);
                            separatePaneIndicator.Legend = view.IndicatorLegend;
                        }
                    }
                }
                else {
                    int tag = GetHashCode();
                    foreach (Indicator indicator in series.Indicators) {
                        if (indicator.Tag is int && (int)indicator.Tag == tag) {
                            series.Indicators.Remove(indicator);
                            if (indicator is SeparatePaneIndicator) {
                                view.StockAxisX.VisibilityInPanes[1].Visible = true;
                                view.StockAxisX.VisibilityInPanes[2].Visible = false;
                                view.UpdateIndicatorPaneScale(false);
                            }
                            break;
                        }
                    }
                }
                this.indicatorVisible = indicatorVisible;
            }
        }
        public override string ToString() {
            return Name;
        }
    }

    public abstract class MovingAverageItem : CheckedIndicatorItem {
        protected override string Name { get { return IndicatorName + " Moving Average"; } }

        protected abstract MovingAverage CreateMovingAverage();
        protected override Indicator CreateIndicator() {
            MovingAverage movingAverage = CreateMovingAverage();
            movingAverage.ValueLevel = ValueLevel.Close;
            return movingAverage;
        }
    }

    public class SimpleMovingAverageItem : MovingAverageItem {
        protected override string IndicatorName { get { return "Simple"; } }

        protected override MovingAverage CreateMovingAverage() {
            return new SimpleMovingAverage();
        }
    }

    public class ExponentialMovingAverageItem : MovingAverageItem {
        protected override string IndicatorName { get { return "Exponential"; } }

        protected override MovingAverage CreateMovingAverage() {
            return new ExponentialMovingAverage();
        }
    }

    public class TripleExponentialMovingAverageItem : MovingAverageItem {
        protected override string IndicatorName { get { return "Triple Exponential"; } }

        protected override MovingAverage CreateMovingAverage() {
            return new TripleExponentialMovingAverageTema();
        }
    }

    public class TriangularMovingAverageItem : MovingAverageItem {
        protected override string IndicatorName { get { return "Triangular"; } }

        protected override MovingAverage CreateMovingAverage() {
            return new TriangularMovingAverage();
        }
    }

    public class WeightedMovingAverageItem : MovingAverageItem {
        protected override string IndicatorName { get { return "Weighted"; } }

        protected override MovingAverage CreateMovingAverage() {
            return new WeightedMovingAverage();
        }
    }

    public class MedianPriceItem : CheckedIndicatorItem {
        protected override string Name { get { return "Median Price"; } }

        protected override Indicator CreateIndicator() {
            return new MedianPrice();
        }
    }

    public class TypicalPriceItem : CheckedIndicatorItem {
        protected override string Name { get { return "Typical Price"; } }

        protected override Indicator CreateIndicator() {
            return new TypicalPrice();
        }
    }

    public class WeightedCloseItem : CheckedIndicatorItem {
        protected override string Name { get { return "Weighted Close"; } }

        protected override Indicator CreateIndicator() {
            return new WeightedClose();
        }
    }

    public class AverageTrueRangeItem : CheckedIndicatorItem {
        protected override string Name { get { return "Average True Range"; } }

        protected override Indicator CreateIndicator() {
            return new AverageTrueRange();
        }
    }

    public class CommodityChannelIndexItem : CheckedIndicatorItem {
        protected override string Name { get { return "Commodity Channel Index"; } }

        protected override Indicator CreateIndicator() {
            return new CommodityChannelIndex();
        }
    }

    public class DetrendedPriceOscillatorItem : CheckedIndicatorItem {
        protected override string Name { get { return "Detrended Price Oscillator"; } }

        protected override Indicator CreateIndicator() {
            return new DetrendedPriceOscillator();
        }
    }

    public class MassIndexItem : CheckedIndicatorItem {
        protected override string Name { get { return "Mass Index"; } }

        protected override Indicator CreateIndicator() {
            return new MassIndex();
        }
    }

    public class MovingAverageConvergenceDivergenceItem : CheckedIndicatorItem {
        protected override string Name { get { return "Moving Average Convergence Divergence"; } }

        protected override Indicator CreateIndicator() {
            return new MovingAverageConvergenceDivergence();
        }
    }

    public class RateOfChangeItem : CheckedIndicatorItem {
        protected override string Name { get { return "Rate Of Change"; } }

        protected override Indicator CreateIndicator() {
            return new RateOfChange();
        }
    }

    public class RelativeStrengthIndexItem : CheckedIndicatorItem {
        protected override string Name { get { return "Relative Strength Index"; } }

        protected override Indicator CreateIndicator() {
            return new RelativeStrengthIndex();
        }
    }

    public class StandardDeviationItem : CheckedIndicatorItem {
        protected override string Name { get { return "Standard Deviation"; } }

        protected override Indicator CreateIndicator() {
            return new StandardDeviation();
        }
    }

    public class ChaikinsVolatilityItem : CheckedIndicatorItem {
        protected override string Name { get { return "Chaikins Volatility"; } }

        protected override Indicator CreateIndicator() {
            return new ChaikinsVolatility();
        }
    }

    public class WilliamsRItem : CheckedIndicatorItem {
        protected override string Name { get { return "Williams %R"; } }

        protected override Indicator CreateIndicator() {
            return new WilliamsR();
        }
    }

    public abstract class RegressionItem : CheckedIndicatorItem {
        protected override string Name { get { return IndicatorName + " Regression"; } }
    }

    public class LinearRegressionItem : RegressionItem {
        protected override string IndicatorName { get { return "Linear"; } }

        protected override Indicator CreateIndicator() {
            RegressionLine regressionLine = new RegressionLine();
            regressionLine.ValueLevel = ValueLevel.Close;
            return regressionLine;
        }
    }
    #endregion
}
