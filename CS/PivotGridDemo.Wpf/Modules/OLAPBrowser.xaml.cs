using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    
    public partial class OLAPBrowser : PivotGridDemoModule {

        const string YearFieldName = "[Date].[Calendar].[Calendar Year]";
        const string CategoryFieldName = "[Product].[Product].[Product]";
        const string TotalCostFieldName = "[Measures].[Total Product Cost]";
        const string FreightFieldName = "[Measures].[Freight Cost]";
        const string QuantityOrderFieldName = "[Measures].[Order Quantity]";
        protected const int DefaultFieldWidth = 90;

		static string GetSampleConnectionString() {
			return "Provider=msolap;Data Source=" + GetSampleFileName() + ";Initial Catalog=Adventure Works;Cube Name=Adventure Works";
		}
		static string GetSampleFileName() {
			string sampleFileName = Path.GetFullPath(DataFilesHelper.FindFile("AdventureWorks.cub", DataFilesHelper.DataPath));
			if(File.Exists(sampleFileName))
				try {
					File.SetAttributes(sampleFileName, FileAttributes.Normal);
				} catch { }
			return sampleFileName;
		}

        string PivotConnectionString() {
            return pivotGrid.OlapConnectionString;
        }
        public OLAPBrowser() {
            InitializeComponent();
        }
        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            InitPivotGrid(GetSampleConnectionString());
        }
        bool IsSampleCube() {
            return pivotGrid.OlapConnectionString.Contains("Cube Name=Adventure Works");
        }
        
        void InitPivotLayoutSampleOlapDB(AsyncOperationResult result) {
            if(pivotGrid.Fields.Count == 0 || !IsSampleCube()) return;
            pivotGrid.BeginUpdate();
            PivotGridField fieldProduct = pivotGrid.Fields[CategoryFieldName],
                fieldYear = pivotGrid.Fields[YearFieldName],
                fieldTotalCost = pivotGrid.Fields[TotalCostFieldName],
                fieldFreightCost = pivotGrid.Fields[FreightFieldName],
                fieldOrderQuantity = pivotGrid.Fields[QuantityOrderFieldName];
            if(fieldProduct == null ||
                fieldYear == null ||
                fieldTotalCost == null ||
                fieldFreightCost == null ||
                fieldOrderQuantity == null) {
                pivotGrid.EndUpdateAsync();
                return;
            }
            fieldProduct.Area = FieldArea.RowArea;
            fieldYear.Area = FieldArea.ColumnArea;
            fieldYear.SortOrder = FieldSortOrder.Descending;
            fieldTotalCost.Width = DefaultFieldWidth + 20;
            fieldTotalCost.CellFormat = "c2";
            fieldFreightCost.Width = DefaultFieldWidth;
            fieldFreightCost.CellFormat = "c2";
            fieldOrderQuantity.Width = DefaultFieldWidth + 5;
            fieldProduct.Visible = true;
            fieldYear.Visible = true;
            fieldTotalCost.Visible = true;
            fieldFreightCost.Visible = true;
            fieldOrderQuantity.Visible = true;
            pivotGrid.EndUpdateAsync();
        }

        void InitPivotGrid(string connectionString) {
            if(string.IsNullOrWhiteSpace(connectionString)) {
                pivotGrid.DataSource = null;
                return;
            }
            if(PivotConnectionString() == connectionString) return;
            pivotGrid.BeginUpdate();
            pivotGrid.Fields.Clear();
            pivotGrid.Groups.Clear();
            pivotGrid.OlapConnectionString = connectionString;
            pivotGrid.EndUpdateAsync(delegate(AsyncOperationResult result) {
                if(pivotGrid.Fields.Count == 0)
                    pivotGrid.RetrieveFieldsAsync(FieldArea.FilterArea, false, InitPivotLayoutSampleOlapDB);
            });
        }

        DataSourceDialog dialog;
        void OnShowConnectionClick(object sender, RoutedEventArgs e) {
            if(pivotGrid == null || pivotGrid.IsAsyncInProgress)
                return;
            dialog = new DataSourceDialog();
            FloatingContainerParameters pars = new FloatingContainerParameters();
            pars.AllowSizing = false;
            pars.CloseOnEscape = true;
            pars.Title = "OLAP Connection";
            pars.ClosedDelegate = DialogClosed;
            FloatingContainer.ShowDialogContent(dialog, this, new Size(600, 230), pars);
        }

        void DialogClosed(bool? close) {
            Application.Current.MainWindow.Activate();
            if(dialog == null)
                return;
            String connectionString = dialog.GetConnectionString();
            dialog = null;
            if(close != true)
                return;
            if(string.IsNullOrWhiteSpace(connectionString)) {
                return;
            }
            InitPivotGrid(connectionString);

        }
    }
}
