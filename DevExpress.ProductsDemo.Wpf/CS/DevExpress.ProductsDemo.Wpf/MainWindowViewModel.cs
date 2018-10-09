using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using DevExpress.Images;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using ProductsDemo.Modules;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.DevAV;

namespace ProductsDemo {
    public class MainWindowViewModel {

        public MainWindowViewModel() {
            SplashScreenType = typeof(SplashScreenWindow);
            Modules = new List<ModuleInfo>() {
                ViewModelSource.Create(() => new ModuleInfo("GridTasksModule", this, "Grid: Tasks")).SetIcon("GridTasks"),
                ViewModelSource.Create(() => new ModuleInfo("GridContactsModule", this, "Grid: Contacts")).SetIcon("GridContacts"),
                ViewModelSource.Create(() => new ModuleInfo("SpreadsheetModule", this, "Spreadsheet")).SetIcon("Spreadsheet"),
                ViewModelSource.Create(() => new ModuleInfo("RichEditModule", this, "Word Processing")).SetIcon("WordProcessing"),
                ViewModelSource.Create(() => new ModuleInfo("ReportsModule", this, "Banded Reports")).SetIcon("BandedReports"),
                ViewModelSource.Create(() => new ModuleInfo("PivotGridModule", this, "Pivot Table")).SetIcon("Pivot"),
                ViewModelSource.Create(() => new ModuleInfo("SalesDashboard", this, "Analytics")).SetIcon("Analytics"),
                ViewModelSource.Create(() => new ModuleInfo("PhotoGallery", this, "Photo Gallery Map")).SetIcon("WeatherMap"),
                ViewModelSource.Create(() => new ModuleInfo("SchedulerModule", this, "Scheduler")).SetIcon("Scheduler"),
                ViewModelSource.Create(() => new ModuleInfo("PdfViewerDemo", this, "Pdf Viewer")).SetIcon("PDFViewer")
            };
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata<PrefixEnumWithExternalMetadata>();
        }
        public virtual IEnumerable<ModuleInfo> Modules { get; protected set; }

        public virtual ModuleInfo SelectedModuleInfo { get; set; }
        public virtual Type SplashScreenType { get; set; }
        public virtual int DefaultBackstatgeIndex { get; set; }
        public virtual bool HasPrinting { get; set; }
        public virtual bool IsBackstageOpen { get; set; }
        public void Exit() {
            CurrentWindowService.Close();
        }
        public void OnModulesLoaded() {
            if(SelectedModuleInfo == null) {
                SelectedModuleInfo = Modules.First();
                SelectedModuleInfo.IsSelected = true;
                SelectedModuleInfo.Show();
            }
            SplashScreenType = typeof(ProgressWindow);
            ApplicationJumpListService.Items.AddOrReplace("New Task", NewTaskIcon, ShowGridTasksModuleNewItemWindow);
            ApplicationJumpListService.Items.AddOrReplace("New Contact", NewContactIcon, ShowGridContactsModuleNewItemWindow);
            ApplicationJumpListService.Apply();
        }
        [Required]
        protected virtual ICurrentWindowService CurrentWindowService { get { return null; } }
        [Required]
        protected virtual IApplicationJumpListService ApplicationJumpListService { get { return null; } }
        [Required]
        protected virtual INavigationService NavigationService { get { return null; } }
        protected virtual void OnSelectedModuleInfoChanged() {
            if(!allowSelectedModuleInfoChanged)
                return;
            PrintingService.PrintableControlLink = null;
            SelectedModuleInfo.IsSelected = true;
            SelectedModuleInfo.Show();
        }
        protected virtual void OnIsBackstageOpenChanged() {
            HasPrinting = PrintingService.HasPrinting;
            if(!HasPrinting && DefaultBackstatgeIndex == 1)
                DefaultBackstatgeIndex = 0;
        }
        BitmapImage NewTaskIcon {
            get { return new BitmapImage(AssemblyHelper.GetResourceUri(typeof(DXImages).Assembly, "Images/Tasks/NewTask_16x16.png")); }
        }
        BitmapImage NewContactIcon {
            get { return new BitmapImage(AssemblyHelper.GetResourceUri(typeof(DXImages).Assembly, "Images/Mail/NewContact_16x16.png")); }
        }
        bool allowSelectedModuleInfoChanged = true;
        void ShowGridModuleNewItemWindow<T>(string moduleType) where T : class {
            if(Application.Current.Windows.Count != 1) return;
            GridViewModelBase<T> viewModel = ViewHelper.GetViewModelFromView(NavigationService.Current) as GridViewModelBase<T>;
            if(viewModel != null)
                viewModel.ShowNewItemWindow();
            else {
                var moduleInfo = Modules.Single(m => m.Type == moduleType);
                allowSelectedModuleInfoChanged = false;
                SelectedModuleInfo = moduleInfo;
                allowSelectedModuleInfoChanged = true;
                moduleInfo.Show(GridModuleNavigationParameter.NewItem);
            }
        }
        void ShowGridTasksModuleNewItemWindow() {
            ShowGridModuleNewItemWindow<Task>("GridTasksModule");
        }
        void ShowGridContactsModuleNewItemWindow() {
            ShowGridModuleNewItemWindow<Contact>("GridContactsModule");
        }
    }
    public class ModuleInfo {
        ISupportServices parent;

        public ModuleInfo(string _type, object parent, string _title) {
            Type = _type;
            this.parent = (ISupportServices)parent;
            Title = _title;
        }
        public string Type { get; private set; }
        public virtual bool IsSelected { get; set; }
        public string Title { get; private set; }
        public virtual Uri Icon { get; set; }
        public ModuleInfo SetIcon(string icon) {
            this.Icon = AssemblyHelper.GetResourceUri(typeof(ModuleInfo).Assembly, string.Format("Images/{0}.png", icon));
            return this;
        }
        public void Show(object parameter = null) {
            INavigationService navigationService = parent.ServiceContainer.GetRequiredService<INavigationService>();
            navigationService.Navigate(Type, parameter, parent);
        }
    }
    public class PrefixEnumWithExternalMetadata {
        public static void BuildMetadata(EnumMetadataBuilder<PersonPrefix> builder) {
            builder
                .Member(PersonPrefix.Dr)
                    .ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Doctor.png")
                .EndMember()
                .Member(PersonPrefix.Mr)
                    .ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Mr.png")
                .EndMember()
                .Member(PersonPrefix.Ms)
                    .ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Ms.png")
                .EndMember()
                .Member(PersonPrefix.Miss)
                    .ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Miss.png")
                .EndMember()
                .Member(PersonPrefix.Mrs)
                    .ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Mrs.png")
                .EndMember();
        }
    }
}
