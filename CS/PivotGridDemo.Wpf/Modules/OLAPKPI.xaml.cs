using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class OLAPKPI : PivotGridDemoModule {
        static string sampleFileName;

        public OLAPKPI() {
            InitializeComponent();
            pivotGrid.OlapConnectionString = "Provider=msolap;Data Source=" + SampleFileName + ";Initial Catalog=Adventure Works;Cube Name=Adventure Works";
        }

        static string SampleFileName {
            get {
                if(string.IsNullOrEmpty(sampleFileName)) {
                    sampleFileName = Path.GetFullPath(DataFilesHelper.FindFile("AdventureWorks.cub", DataFilesHelper.DataPath));
                    if(File.Exists(sampleFileName))
						try {
							File.SetAttributes(sampleFileName, FileAttributes.Normal);
						} catch { }
                }
                return sampleFileName;
            }
        }
    }
}
