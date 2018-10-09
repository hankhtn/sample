using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Navigation.Internal;

namespace ControlsDemo {
    public class TileNavViewLocator : ViewLocator {
        public TileNavViewLocator()
            : base(typeof(TileNavViewLocator).Assembly) {
        }
    }

    public class TileNavBaseViewModel : ViewModelBase {
        private ObservableCollection<TileNavBaseItemViewModel> _Actions;
        private ObservableCollection<TileNavBaseItemViewModel> _Categories;
        TileNavBaseItemViewModel Selected;

        public TileNavBaseViewModel() {
            Messenger.Default.Register<NavigationMessage>(this, OnNavigationMessage);
            _Categories = new ObservableCollection<TileNavBaseItemViewModel>(TileNavBaseViewModelDataProvider.CreateCategories());
            _Actions = new ObservableCollection<TileNavBaseItemViewModel>(TileNavBaseViewModelDataProvider.CreateActions());
            BackCommand = DelegateCommandFactory.Create(OnBackCommand, CanBackCommand);
            ViewUnloadedCommand = new DelegateCommand(OnViewUnloaded);
        }

        public ObservableCollection<TileNavBaseItemViewModel> Actions {
            get { return _Actions; }
        }
        public ICommand BackCommand { get; private set; }
        public ObservableCollection<TileNavBaseItemViewModel> Categories {
            get { return _Categories; }
        }
        public ICommand ViewUnloadedCommand { get; private set; }
        INavigationService NavigationService { get { return ServiceContainer.GetService<INavigationService>(); } }

        protected virtual void OnNavigationMessage(NavigationMessage message) {
            switch(message.MessageType) {
                case NavigationMessageType.New:
                    TileNavBaseItemViewModel vm = message.Source as TileNavBaseItemViewModel;
                    if(vm != null && vm.IsSelected) {
                        NavigationService.Navigate("TileNavDetailsView", vm);
                        if(Selected != null) Selected.IsSelected = false;
                        Selected = vm;
                    }
                    break;
                case NavigationMessageType.Back:
                    OnBackCommand();
                    break;
            }
        }
        protected virtual void OnViewUnloaded() {
            Messenger.Default.Unregister<NavigationMessage>(this, OnNavigationMessage);
        }
        bool CanBackCommand() {
            return NavigationService != null && NavigationService.CanGoBack;
        }
        void OnBackCommand() {
            if(NavigationService.CanGoBack) {
                NavigationService.GoBack();
                if(Selected != null)
                    Selected.IsSelected = false;
                Selected = null;
            }
        }
    }
    public class TileNavBaseItemViewModel {
        protected TileNavBaseItemViewModel() {
            Items = new ObservableCollection<TileNavBaseItemViewModel>();
            BackCommand = DelegateCommandFactory.Create(OnBackCommand);
            ShowNotificationCommand = DelegateCommandFactory.Create<string>(OnShowNotificationCommand);
        }

        public ICommand BackCommand { get; private set; }
        public virtual Color Color { get; set; }
        public virtual ImageSource ContentImage { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual ImageSource Image { get; set; }
        public virtual bool IsSelected { get; set; }
        public ObservableCollection<TileNavBaseItemViewModel> Items { get; private set; }
        public virtual ICommand ShowNotificationCommand { get; set; }
        public TileSize Size { get; set; }

        public static TileNavBaseItemViewModel Create() {
            return ViewModelSource.Create(() => new TileNavBaseItemViewModel());
        }
        void OnBackCommand() {
            Messenger.Default.Send(new NavigationMessage(NavigationMessageType.Back));
        }
        protected void OnIsSelectedChanged() {
            if(IsSelected)
                Messenger.Default.Send(new NavigationMessage(this));
        }
        void OnShowNotificationCommand(string message) {
            Messenger.Default.Send(new NotificationMessage(DisplayName + " clicked"));
        }
    }
    static class TileNavBaseViewModelDataProvider {
        static string[] ContentImages = new string[] { "/ControlsDemo;component/Images/Calc.png", "/ControlsDemo;component/Images/Rates.png", "/ControlsDemo;component/Images/Research.png",
            "/ControlsDemo;component/Images/Statistics.png","/ControlsDemo;component/Images/System.png","/ControlsDemo;component/Images/UserManagment.png","/ControlsDemo;component/Images/ZillowLogo.png",};
        static string[] ItemImages = new string[] {
            "/ControlsDemo;component/Images/TileNavPane/itemElement01.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement02.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement03.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement04.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement05.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement06.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement07.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement08.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement09.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement10.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement11.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement12.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement13.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement14.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement15.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement16.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement17.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement18.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement19.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement20.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement21.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement22.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement23.Glyph.png"
        };
        static string[] SubItemImages = new string[] {
            "/ControlsDemo;component/Images/TileNavPane/subItemElement01.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement02.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement03.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement04.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement05.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement06.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement07.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement08.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement09.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement10.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement11.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement12.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement13.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement14.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement15.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement16.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement17.Glyph.png"
        };
        static Color[] Colors = new Color[] { (Color)ColorConverter.ConvertFromString("#FFC3213F"), (Color)ColorConverter.ConvertFromString("#FFE65E20"),
            (Color)ColorConverter.ConvertFromString("#FFD4AF00"), (Color)ColorConverter.ConvertFromString("#FF6652A2"), (Color)ColorConverter.ConvertFromString("#FF54AF0E"),
            (Color)ColorConverter.ConvertFromString("#FF00ABDC"), (Color)ColorConverter.ConvertFromString("#FFDA8515") };
        static Random random = new Random(DateTime.Now.Millisecond);
        static TileNavBaseItemViewModel CreateItem(string displayName, string[] Images) {
            TileNavBaseItemViewModel vm = TileNavBaseItemViewModel.Create();
            vm.Do(x => {
                x.DisplayName = displayName;
                x.Color = Colors[random.Next(Colors.Length)];
                x.Image = new BitmapImage(new Uri(Images[random.Next(Images.Length)], UriKind.RelativeOrAbsolute));
                x.ContentImage = new BitmapImage(new Uri(ContentImages[random.Next(ContentImages.Length)], UriKind.RelativeOrAbsolute));
            });
            return vm;
        }
        public static IEnumerable<TileNavBaseItemViewModel> CreateCategories() {
            var categories = new List<TileNavBaseItemViewModel>();
            for(int i = 1; i < 6; i++) {
                TileNavBaseItemViewModel category = CreateItem(string.Format("Category {0}", i), ItemImages);
                for(int j = 1; j < 5; j++) {
                    TileNavBaseItemViewModel item = CreateItem(string.Format("Item {0}.{1}", i, j), ItemImages);
                    for(int k = 1; k < 4; k++) {
                        TileNavBaseItemViewModel subItem = CreateItem(string.Format("SubItem {0}.{1}.{2}", i, j, k), SubItemImages);
                        item.Items.Add(subItem);
                    }
                    category.Items.Add(item);
                }
                categories.Add(category);
            }
            return categories;
        }
        public static IEnumerable<TileNavBaseItemViewModel> CreateActions() {
            var actions = new List<TileNavBaseItemViewModel>();
            for(int i = 1; i < 5; i++) {
                TileNavBaseItemViewModel action = CreateItem(string.Format("Action {0}", i), ItemImages);
                if(i < 3) action.Size = TileSize.Small;
                actions.Add(action);
            }
            return actions;
        }
    }
    public enum NavigationMessageType { New, Back }
    public sealed class NavigationMessage {
        public NavigationMessage(object source) {
            Source = source;
        }
        public NavigationMessage(NavigationMessageType messageType, object source = null)
            : this(source) {
            MessageType = messageType;
        }
        public object Source { get; private set; }
        public NavigationMessageType MessageType { get; private set; }
    }
    public sealed class NotificationMessage {
        public NotificationMessage(string source) {
            Source = source;
        }
        public string Source { get; private set; }
    }
}
