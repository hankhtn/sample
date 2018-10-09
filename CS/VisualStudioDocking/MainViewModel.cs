using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.DemoData.Utils;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.About;
using DevExpress.Xpf;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.PropertyGrid;
using Microsoft.Win32;

namespace VisualStudioDocking.ViewModels {
    public class DocumentViewModel : PanelWorkspaceViewModel {
        public DocumentViewModel() {
            IsClosed = false;
        }
        public DocumentViewModel(string displayName, string text) : this() {
            DisplayName = displayName;
            CodeLanguageText = new CodeLanguageText(CodeLanguage.CS, text);
        }

        public CodeLanguage CodeLanguage { get; private set; }
        public CodeLanguageText CodeLanguageText { get; private set; }
        public string Description { get; protected set; }
        public string FilePath { get; protected set; }
        public string Footer { get; protected set; }
        protected override string WorkspaceName { get { return "DocumentHost"; } }

        public bool OpenFile() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Visual C# Files (*.cs)|*.cs|XAML Files (*.xaml)|*.xaml";
            openFileDialog.FilterIndex = 1;
            bool? dialogResult = openFileDialog.ShowDialog();
            bool dialogResultOK = dialogResult.HasValue && dialogResult.Value;
            if(dialogResultOK) {
                DisplayName = openFileDialog.SafeFileName;
                FilePath = openFileDialog.FileName;
                SetCodeLanguageProperties(Path.GetExtension(openFileDialog.SafeFileName));
                Stream fileStream = File.OpenRead(openFileDialog.FileName);
                using(StreamReader reader = new StreamReader(fileStream)) {
                    CodeLanguageText = new CodeLanguageText(CodeLanguage, reader.ReadToEnd());
                }
                fileStream.Close();
            }
            return dialogResultOK;
        }
        public override void OpenItemByPath(string path) {
            DisplayName = Path.GetFileName(path);
            FilePath = path;
            SetCodeLanguageProperties(Path.GetExtension(path));
            CodeLanguageText = new CodeLanguageText(CodeLanguage, () => { return GetCodeTextByPath(path); });
            IsActive = true;
        }
        string GenerateClassText(string className) {
            string text = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualStudioDocking {{
    class {0} {{
    }}
}}";
            return string.Format(text, className);
        }
        CodeLanguage GetCodeLanguage(string fileExtension) {
            switch(fileExtension) {
                case ".cs": return CodeLanguage.CS;
                case ".vb": return CodeLanguage.VB;
                case ".xaml": return CodeLanguage.XAML;
                default: return CodeLanguage.Plain;
            }
        }
        string GetCodeTextByPath(string path) {
            Assembly assembly = typeof(DocumentViewModel).Assembly;
            using(Stream stream = AssemblyHelper.GetResourceStream(assembly, path, true)) {
                if(stream == null)
                    return GenerateClassText(Path.GetFileNameWithoutExtension(path));
                using(StreamReader reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
        string GetDescription(CodeLanguage codeLanguage) {
            switch(codeLanguage) {
                case CodeLanguage.CS: return "Visual C# Source file";
                case CodeLanguage.VB: return "Visual Basic Source file";
                case CodeLanguage.XAML: return "Windows Markup File";
                default: return "Other file";
            }
        }
        void SetCodeLanguageProperties(string fileExtension) {
            CodeLanguage = GetCodeLanguage(fileExtension);
            Description = GetDescription(CodeLanguage);
            Footer = DisplayName;
            Glyph = CodeLanguage == CodeLanguage.XAML ? Images.FileXaml : CodeLanguage == CodeLanguage.CS ? Images.FileCS : null;
        }
    }
    public class MainViewModel {
        CommandViewModel errorList;
        PanelWorkspaceViewModel lastOpenedItem;
        CommandViewModel loadLayout;
        CommandViewModel newFile;
        CommandViewModel newProject;
        CommandViewModel openFile;
        CommandViewModel openProject;
        CommandViewModel output;
        CommandViewModel properties;
        CommandViewModel save;
        CommandViewModel saveAll;
        CommandViewModel saveLayout;
        CommandViewModel searchResults;
        CommandViewModel solutionExplorer;
        SolutionExplorerViewModel solutionExplorerViewModel;
        CommandViewModel toolbox;
        ObservableCollection<WorkspaceViewModel> workspaces;

        public MainViewModel() {
            ErrorListViewModel = CreatePanelWorkspaceViewModel<ErrorListViewModel>();
            OutputViewModel = CreatePanelWorkspaceViewModel<OutputViewModel>();
            PropertiesViewModel = CreatePanelWorkspaceViewModel<PropertiesViewModel>();
            SearchResultsViewModel = CreatePanelWorkspaceViewModel<SearchResultsViewModel>();
            ToolboxViewModel = CreatePanelWorkspaceViewModel<ToolboxViewModel>();
            Bars = new ReadOnlyCollection<BarModel>(CreateBars());
            InitDefaultLayout();
        }

        public ReadOnlyCollection<BarModel> Bars { get; private set; }
        public ErrorListViewModel ErrorListViewModel { get; private set; }
        public OutputViewModel OutputViewModel { get; private set; }
        public PropertiesViewModel PropertiesViewModel { get; private set; }
        public SearchResultsViewModel SearchResultsViewModel { get; set; }
        public SolutionExplorerViewModel SolutionExplorerViewModel {
            get {
                if(solutionExplorerViewModel == null) {
                    solutionExplorerViewModel = CreatePanelWorkspaceViewModel<SolutionExplorerViewModel>();
                    solutionExplorerViewModel.ItemOpening += SolutionExplorerViewModel_ItemOpening;
                    solutionExplorerViewModel.Solution = Solution.Create();
                    OpenItem(solutionExplorerViewModel.Solution.LastOpenedItem.FilePath);
                }
                return solutionExplorerViewModel;
            }
        }
        public ToolboxViewModel ToolboxViewModel { get; private set; }
        public ObservableCollection<WorkspaceViewModel> Workspaces {
            get {
                if(workspaces == null) {
                    workspaces = new ObservableCollection<WorkspaceViewModel>();
                    workspaces.CollectionChanged += OnWorkspacesChanged;
                }
                return workspaces;
            }
        }
        protected virtual IDockingSerializationDialogService SaveLoadLayoutService { get { return null; } }

        protected virtual List<CommandViewModel> CreateAboutCommands() {
            var showAboutCommnad = new DelegateCommand(ShowAbout);
            return new List<CommandViewModel>() { new CommandViewModel("About", showAboutCommnad) { Glyph = Images.About } };
        }
        protected virtual List<CommandViewModel> CreateEditCommands() {
            var findCommand = new CommandViewModel("Find") { Glyph = Images.Find, KeyGesture = new KeyGesture(Key.F, ModifierKeys.Control) };
            var replaceCommand = new CommandViewModel("Replace") { Glyph = Images.Replace, KeyGesture = new KeyGesture(Key.H, ModifierKeys.Control) };
            var findInFilesCommand = new CommandViewModel("Find in Files") {
                Glyph = Images.FindInFiles,
                KeyGesture = new KeyGesture(Key.F, ModifierKeys.Control | ModifierKeys.Shift)
            };
            var list = new List<CommandViewModel>() { findCommand, replaceCommand, findInFilesCommand };
            CommandViewModel findReplaceDocument = new CommandViewModel("Find and Replace", list);
            findReplaceDocument.IsEnabled = true;
            findReplaceDocument.IsSubItem = true;
            return new List<CommandViewModel>() { findReplaceDocument };
        }
        protected virtual List<CommandViewModel> CreateLayoutCommands() {
            loadLayout = new CommandViewModel("Load Layout...", new DelegateCommand(OnLoadLayout)) { Glyph = Images.LoadLayout };
            saveLayout = new CommandViewModel("Save Layout...", new DelegateCommand(OnSaveLayout)) { Glyph = Images.SaveLayout };
            return new List<CommandViewModel>() { loadLayout, saveLayout };
        }
        protected T CreatePanelWorkspaceViewModel<T>() where T : PanelWorkspaceViewModel {
            return ViewModelSource<T>.Create();
        }
        protected virtual List<CommandViewModel> CreateViewCommands() {
            toolbox = GetShowCommand(ToolboxViewModel);
            solutionExplorer = GetShowCommand(SolutionExplorerViewModel);
            properties = GetShowCommand(PropertiesViewModel);
            errorList = GetShowCommand(ErrorListViewModel);
            output = GetShowCommand(OutputViewModel);
            searchResults = GetShowCommand(SearchResultsViewModel);
            var themesCommands = new List<CommandViewModel>();
            foreach(Theme theme in Theme.Themes.Where(x => x.Category == Theme.VisualStudioCategory && x.Name.Contains("VS2017"))) {
                var command = new CommandViewModel(theme.Name.Replace("VS2017", ""), new DelegateCommand<Theme>(
                    (t) => ThemeManager.SetTheme(Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive), theme)));
                command.Glyph = new BitmapImage(theme.SmallGlyph);
                themesCommands.Add(command);
            }
            CommandViewModel themesSwitcher = new CommandViewModel("Themes", themesCommands) { IsEnabled = true, IsSubItem = true };
            return new List<CommandViewModel>() { toolbox, solutionExplorer, properties, errorList, output, searchResults, themesSwitcher };
        }
        protected void OpenOrCloseWorkspace(PanelWorkspaceViewModel workspace, bool activateOnOpen = true) {
            if(Workspaces.Contains(workspace)) {
                workspace.IsClosed = !workspace.IsClosed;
            } else {
                Workspaces.Add(workspace);
                workspace.IsClosed = false;
            }
            if(activateOnOpen && workspace.IsOpened)
                SetActiveWorkspace(workspace);
        }
        bool ActivateDocument(string path) {
            var document = GetDocument(path);
            bool isFound = document != null;
            if(isFound) document.IsActive = true;
            return isFound;
        }
        List<BarModel> CreateBars() {
            return new List<BarModel>() {
                new BarModel("Main") { IsMainMenu = true, Commands = CreateCommands() },
                new BarModel("Standard") { Commands = CreateToolbarCommands() }
            };
        }
        List<CommandViewModel> CreateCommands() {
            return new List<CommandViewModel> {
                new CommandViewModel("File", CreateFileCommands()),
                new CommandViewModel("Edit", CreateEditCommands()),
                new CommandViewModel("Layouts", CreateLayoutCommands()),
                new CommandViewModel("View", CreateViewCommands()),
                new CommandViewModel("Help", CreateAboutCommands())
            };
        }
        DocumentViewModel CreateDocumentViewModel() {
            return CreatePanelWorkspaceViewModel<DocumentViewModel>();
        }
        List<CommandViewModel> CreateFileCommands() {
            var fileExecutedCommand = new DelegateCommand<object>(OnNewFileExecuted);
            var fileOpenCommand = new DelegateCommand<object>(OnFileOpenExecuted);

            CommandViewModel newCommand = new CommandViewModel("New") { IsSubItem = true };
            newProject = new CommandViewModel("Project...", fileExecutedCommand) { Glyph = Images.NewProject, KeyGesture = new KeyGesture(Key.N, ModifierKeys.Control | ModifierKeys.Shift), IsEnabled = false };
            newFile = new CommandViewModel("New File", fileExecutedCommand) { Glyph = Images.File, KeyGesture = new KeyGesture(Key.N, ModifierKeys.Control) };
            newCommand.Commands = new List<CommandViewModel>() { newProject, newFile };

            CommandViewModel openCommand = new CommandViewModel("Open") { IsSubItem = true, };
            openProject = new CommandViewModel("Project/Solution...") {
                Glyph = Images.OpenSolution,
                IsEnabled = false,
                KeyGesture = new KeyGesture(Key.O, ModifierKeys.Control | ModifierKeys.Shift),
            };
            openFile = new CommandViewModel("Open File", fileOpenCommand) { Glyph = Images.OpenFile, KeyGesture = new KeyGesture(Key.O, ModifierKeys.Control) };
            openCommand.Commands = new List<CommandViewModel>() { openProject, openFile };

            CommandViewModel closeFile = new CommandViewModel("Close");
            CommandViewModel closeSolution = new CommandViewModel("Close Solution") { Glyph = Images.CloseSolution };
            save = new CommandViewModel("Save") { Glyph = Images.Save, KeyGesture = new KeyGesture(Key.S, ModifierKeys.Control) };
            saveAll = new CommandViewModel("Save All") { Glyph = Images.SaveAll, KeyGesture = new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };

            return new List<CommandViewModel>() { newCommand, openCommand, GetSeparator(), closeFile, closeSolution, GetSeparator(), save, saveAll };
        }
        List<CommandViewModel> CreateToolbarCommands() {
            CommandViewModel start = new CommandViewModel("Start") {
                Glyph = Images.Run,
                KeyGesture = new KeyGesture(Key.F5, ModifierKeys.None),
                DisplayMode = BarItemDisplayMode.ContentAndGlyph
            };
            CommandViewModel combo = new CommandViewModel("Configuration") { IsSubItem = true, IsComboBox = true };
            combo.Commands = new List<CommandViewModel>() { new CommandViewModel("Debug"), new CommandViewModel("Release") };

            CommandViewModel cut = new CommandViewModel("Cut") { Glyph = Images.Cut, KeyGesture = new KeyGesture(Key.X, ModifierKeys.Control) };
            CommandViewModel copy = new CommandViewModel("Copy") { Glyph = Images.Copy, KeyGesture = new KeyGesture(Key.C, ModifierKeys.Control) };
            CommandViewModel paste = new CommandViewModel("Paste") { Glyph = Images.Paste, KeyGesture = new KeyGesture(Key.V, ModifierKeys.Control) };

            CommandViewModel undo = new CommandViewModel("Undo") { Glyph = Images.Undo, KeyGesture = new KeyGesture(Key.Z, ModifierKeys.Control) };
            CommandViewModel redo = new CommandViewModel("Redo") { Glyph = Images.Redo, KeyGesture = new KeyGesture(Key.Y, ModifierKeys.Control) };

            return new List<CommandViewModel>() {
                newProject, newFile, openFile, save, saveAll, GetSeparator(), combo, start,
                GetSeparator(), cut, copy, paste, GetSeparator(), undo, redo, GetSeparator(),
                toolbox, solutionExplorer, properties, errorList, output, searchResults,
                GetSeparator(), loadLayout, saveLayout
            };
        }
        DocumentViewModel GetDocument(string filePath) {
            return Workspaces.OfType<DocumentViewModel>().FirstOrDefault(x => x.FilePath == filePath);
        }
        CommandViewModel GetSeparator() {
            return new CommandViewModel() { IsSeparator = true };
        }
        CommandViewModel GetShowCommand(PanelWorkspaceViewModel viewModel) {
            return new CommandViewModel(viewModel, new DelegateCommand(() => OpenOrCloseWorkspace(viewModel)));
        }
        void InitDefaultLayout() {
            var panels = new List<PanelWorkspaceViewModel> { ToolboxViewModel, SolutionExplorerViewModel, PropertiesViewModel, ErrorListViewModel };
            foreach(var panel in panels) {
                OpenOrCloseWorkspace(panel, false);
            }
        }
        void OnFileOpenExecuted(object param) {
            var document = CreateDocumentViewModel();
            if(!document.OpenFile() || ActivateDocument(document.FilePath)) {
                document.Dispose();
                return;
            }
            OpenOrCloseWorkspace(document);
        }
        void OnLoadLayout() {
            SaveLoadLayoutService.LoadLayout();
        }
        void OnNewFileExecuted(object param) {
            string newItemName = solutionExplorerViewModel.Solution.AddNewItemToRoot();
            OpenItem(newItemName);
        }
        void OnSaveLayout() {
            SaveLoadLayoutService.SaveLayout();
        }
        void OnWorkspaceRequestClose(object sender, EventArgs e) {
            var workspace = sender as PanelWorkspaceViewModel;
            if(workspace != null) {
                workspace.IsClosed = true;
                if(workspace is DocumentViewModel) {
                    workspace.Dispose();
                    Workspaces.Remove(workspace);
                }
            }
        }
        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.NewItems != null && e.NewItems.Count != 0)
                foreach(WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += OnWorkspaceRequestClose;
            if(e.OldItems != null && e.OldItems.Count != 0)
                foreach(WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= OnWorkspaceRequestClose;
        }
        void OpenItem(string filePath) {
            if(ActivateDocument(filePath)) return;
            lastOpenedItem = CreateDocumentViewModel();
            lastOpenedItem.OpenItemByPath(filePath);
            OpenOrCloseWorkspace(lastOpenedItem);
        }
        void SetActiveWorkspace(WorkspaceViewModel workspace) {
            workspace.IsActive = true;
        }
        void ShowAbout() {
            About.ShowAbout(ProductKind.DXperienceWPF);
        }
        void SolutionExplorerViewModel_ItemOpening(object sender, SolutionItemOpeningEventArgs e) {
            OpenItem(e.SolutionItem.FilePath);
        }
    }
    abstract public class PanelWorkspaceViewModel : WorkspaceViewModel, IMVVMDockingProperties {
        string targetName;

        protected PanelWorkspaceViewModel() {
            targetName = WorkspaceName;
        }

        abstract protected string WorkspaceName { get; }
        string IMVVMDockingProperties.TargetName {
            get { return targetName; }
            set { targetName = value; }
        }

        public virtual void OpenItemByPath(string path) { }
    }
    public abstract class WorkspaceViewModel : ViewModel {
        protected WorkspaceViewModel() {
            IsClosed = true;
        }

        public event EventHandler RequestClose;

        public virtual bool IsActive { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnIsClosedChanged")]
        public virtual bool IsClosed { get; set; }
        public virtual bool IsOpened { get; set; }

        public void Close() {
            EventHandler handler = RequestClose;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }
        protected virtual void OnIsClosedChanged() {
            IsOpened = !IsClosed;
        }
    }

    public abstract class ViewModel : IDisposable {
        public string BindableName { get { return GetBindableName(DisplayName); } }
        public virtual string DisplayName { get; protected set; }
        public virtual ImageSource Glyph { get; set; }

        string GetBindableName(string name) { return "_" + System.Text.RegularExpressions.Regex.Replace(name, @"\W", ""); }

        #region IDisposable Members
        public void Dispose() {
            OnDispose();
        }
        protected virtual void OnDispose() { }
#if DEBUG
        ~ViewModel() {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", GetType().Name, DisplayName, GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif
        #endregion 
    }

    #region Tool Panels
    public class ErrorListViewModel : PanelWorkspaceViewModel {
        public ErrorListViewModel() {
            DisplayName = "Error List";
            Glyph = Images.TaskList;
            Error = Images.Error;
            Warning = Images.Warning;
            Info = Images.Info;
        }

        public ImageSource Error { get; set; }
        public ImageSource Info { get; set; }
        public ImageSource Warning { get; set; }
        protected override string WorkspaceName { get { return "BottomHost"; } }
    }

    public class OutputViewModel : PanelWorkspaceViewModel {
        public OutputViewModel() {
            DisplayName = "Output";
            Glyph = Images.Output;
            Text = @"1>------ Build started: Project: VisualStudioInspiredUIDemo, Configuration: Debug Any CPU ------
1>  DockingDemo -> C:\VisualStudioInspiredUIDemo.exe
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========";
        }

        public string Text { get; private set; }
        protected override string WorkspaceName { get { return "BottomHost"; } }
    }

    public class PropertiesViewModel : PanelWorkspaceViewModel {
        public PropertiesViewModel() {
            DisplayName = "Properties";
            Glyph = Images.PropertiesWindow;
            SelectedItem = new PropertyItem(new PropertyGridControl());
            Items = new List<PropertyItem> {
                SelectedItem,
                new PropertyItem(new AccordionControl()),
                new PropertyItem(new DocumentPanel()),
                new PropertyItem(new DocumentGroup()),
                new PropertyItem(new DevExpress.Xpf.Docking.LayoutPanel())
            };
        }

        public List<PropertyItem> Items { get; set; }
        public virtual PropertyItem SelectedItem { get; set; }
        protected override string WorkspaceName { get { return "RightHost"; } }
    }

    public class PropertyItem {
        public PropertyItem(object data) {
            Data = data;
            Name = Data.ToString();
        }

        public object Data { get; set; }
        public string Name { get; set; }
    }

    public class SearchResultsViewModel : PanelWorkspaceViewModel {
        public SearchResultsViewModel() {
            DisplayName = "Search Results";
            Glyph = Images.FindInFilesWindow;
            Text = @"Matching lines: 0    Matching files: 0    Total files searched: 61";
        }

        public string Text { get; private set; }
        protected override string WorkspaceName { get { return "BottomHost"; } }
    }

    public class Solution : BindableBase {
        string[] codePaths = new string[] {
            "MainWindow.xaml",
            "MainWindow.xaml.cs",
            "SplashScreenWindow.xaml",
            "SplashScreenWindow.xaml.cs",
            "Resources.xaml",
            "BarTemplateSelector.cs",
            "MainViewModel.cs"
        };
        int newItemsCount;

        protected Solution() {
            SolutionItem root = SolutionItem.Create(this, "WPFApplication", ImagePaths.SolutionExplorer);
            SolutionItem properties = SolutionItem.Create(this, "Properties", ImagePaths.PropertiesWindow);
            SolutionItem references = SolutionItem.Create(this, "References", ImagePaths.References);
            root.Items.Add(properties);
            root.Items.Add(references);
            var files = GetCodeFiles();
            foreach(var file in files) {
                root.Items.Add(file);
            }
            LastOpenedItem = files.FirstOrDefault();
            Items = new ObservableCollection<SolutionItem> { root };
        }

        public ObservableCollection<SolutionItem> Items { get; private set; }
        public virtual SolutionItem LastOpenedItem { get; set; }

        public static Solution Create() {
            return ViewModelSource.Create(() => new Solution());
        }
        public string AddNewItemToRoot() {
            newItemsCount++;
            string newItemName = string.Format("Class{0}.cs", newItemsCount);
            var solutionItem = SolutionItem.CreateFile(this, newItemName);
            (Items[0] as SolutionItem).Items.Add(solutionItem);
            return newItemName;
        }
        List<SolutionItem> GetCodeFiles() {
            var result = new List<SolutionItem>();
            var subFiles = new List<SolutionItem>();
            foreach(var codePath in codePaths)
                if(codePath.EndsWith(".xaml.cs") || codePath.EndsWith(".xaml.vb"))
                    subFiles.Add(SolutionItem.CreateFile(this, codePath));
                else
                    result.Add(SolutionItem.CreateFile(this, codePath));
            foreach(var subFile in subFiles) {
                var xamlFile = result.FirstOrDefault(x => x.FilePath == subFile.FilePath.Replace(".xaml.cs", ".xaml").Replace(".xaml.vb", ".xaml"));
                if(xamlFile == null)
                    result.Add(subFile);
                else
                    xamlFile.Items.Add(subFile);
            }
            return result;
        }
    }

    public class SolutionExplorerViewModel : PanelWorkspaceViewModel {
        public SolutionExplorerViewModel() {
            DisplayName = "Solution Explorer";
            Glyph = Images.SolutionExplorer;
            PropertiesWindow = Images.PropertiesWindow;
            ShowAllFiles = Images.ShowAllFiles;
            Refresh = Images.Refresh;
        }

        public event EventHandler<SolutionItemOpeningEventArgs> ItemOpening;

        public ImageSource PropertiesWindow { get; set; }
        public ImageSource Refresh { get; set; }
        public ImageSource ShowAllFiles { get; set; }
        public Solution Solution { get; set; }
        protected override string WorkspaceName { get { return "RightHost"; } }

        public void OpenItem(SolutionItem item) {
            if(item.IsFile && ItemOpening != null)
                ItemOpening.Invoke(this, new SolutionItemOpeningEventArgs(item));
        }
    }

    public class SolutionItem {
        readonly Solution solution;

        protected SolutionItem(Solution solution) {
            this.solution = solution;
            Items = new ObservableCollection<SolutionItem>();
        }

        public string DisplayName { get; private set; }
        public string FilePath { get; private set; }
        public string GlyphPath { get; private set; }
        public bool IsFile { get { return FilePath != null; } }
        public ObservableCollection<SolutionItem> Items { get; private set; }

        public static SolutionItem Create(Solution solution, string displayName, string glyph) {
            var solutionItem = ViewModelSource.Create(() => new SolutionItem(solution));
            solutionItem.Do(x => {
                x.DisplayName = displayName;
                x.GlyphPath = glyph;
            });
            return solutionItem;
        }
        public static SolutionItem CreateFile(Solution solution, string path) {
            var solutionItem = ViewModelSource.Create(() => new SolutionItem(solution));
            solutionItem.Do(x => {
                x.DisplayName = Path.GetFileName(path);
                x.GlyphPath = Path.GetExtension(path) == ".cs" ? ImagePaths.FileCS : ImagePaths.FileXaml;
                x.FilePath = path;
            });
            return solutionItem;
        }
    }

    public class SolutionItemOpeningEventArgs : EventArgs {
        public SolutionItemOpeningEventArgs(SolutionItem solutionItem) {
            SolutionItem = solutionItem;
        }

        public SolutionItem SolutionItem { get; set; }
    }

    public class ToolboxViewModel : PanelWorkspaceViewModel {
        public ToolboxViewModel() {
            DisplayName = "Toolbox";
            Glyph = Images.Toolbox;
        }

        protected override string WorkspaceName { get { return "Toolbox"; } }
    }
    #endregion

    #region Bars
    public class BarModel : ViewModel {
        public BarModel(string displayName) {
            DisplayName = displayName;
        }
        public List<CommandViewModel> Commands { get; set; }
        public bool IsMainMenu { get; set; }
    }

    public class CommandViewModel : ViewModel {
        public CommandViewModel() { }
        public CommandViewModel(string displayName, List<CommandViewModel> subCommands)
            : this(displayName, null, null, subCommands) {
        }
        public CommandViewModel(string displayName, ICommand command = null)
            : this(displayName, null, command, null) {
        }
        public CommandViewModel(WorkspaceViewModel owner, ICommand command)
            : this(string.Empty, owner, command) {
        }
        private CommandViewModel(string displayName, WorkspaceViewModel owner = null, ICommand command = null, List<CommandViewModel> subCommands = null) {
            IsEnabled = true;
            Owner = owner;
            if(Owner != null) {
                DisplayName = Owner.DisplayName;
                Glyph = Owner.Glyph;
            } else DisplayName = displayName;
            Command = command;
            Commands = subCommands;
        }

        public ICommand Command { get; private set; }
        public List<CommandViewModel> Commands { get; set; }
        public BarItemDisplayMode DisplayMode { get; set; }
        public bool IsComboBox { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsSeparator { get; set; }
        public bool IsSubItem { get; set; }
        public KeyGesture KeyGesture { get; set; }
        public WorkspaceViewModel Owner { get; private set; }
    }
    #endregion

    #region Images
    public static class ImagePaths {
        public const string About = folder + "About.png";
        public const string CloseSolution = folder + "CloseSolution.png";
        public const string Copy = folder + "Copy.png";
        public const string Cut = folder + "Cut.png";
        public const string DevExpressLogo = folder + "DevExpressLogo.png";
        public const string Error = folder + "Error.png";
        public const string File = folder + "File.png";
        public const string FileCS = folder + "FileCS.png";
        public const string FileXaml = folder + "FileXaml.png";
        public const string Find = folder + "Find.png";
        public const string FindInFiles = folder + "FindInFiles.png";
        public const string FindInFilesWindow = folder + "FindInFilesWindow.png";
        public const string Info = folder + "Info.png";
        public const string LoadLayout = folder + "LoadLayout.png";
        public const string NewProject = folder + "NewProject.png";
        public const string Notification = folder + "Notification.png";
        public const string OpenFile = folder + "OpenFile.png";
        public const string OpenSolution = folder + "OpenSolution.png";
        public const string Output = folder + "Output.png";
        public const string Paste = folder + "Paste.png";
        public const string PropertiesWindow = folder + "PropertiesWindow.png";
        public const string Redo = folder + "Redo.png";
        public const string References = folder + "References.png";
        public const string Refresh = folder + "Refresh.png";
        public const string Replace = folder + "Replace.png";
        public const string Run = folder + "Run.png";
        public const string Save = folder + "Save.png";
        public const string SaveAll = folder + "SaveAll.png";
        public const string SaveLayout = folder + "SaveLayout.png";
        public const string ShowAllFiles = folder + "ShowAllFiles.png";
        public const string SolutionExplorer = folder + "SolutionExplorer.png";
        public const string TaskList = folder + "TaskList.png";
        public const string Toolbox = folder + "Toolbox.png";
        public const string Undo = folder + "Undo.png";
        public const string Warning = folder + "Warning.png";
        const string folder = "/VisualStudioDocking;component/Images/Docking/";
    }

    public static class Images {
        public static ImageSource About { get { return GetImage(ImagePaths.About); } }
        public static ImageSource CloseSolution { get { return GetImage(ImagePaths.CloseSolution); } }
        public static ImageSource Copy { get { return GetImage(ImagePaths.Copy); } }
        public static ImageSource Cut { get { return GetImage(ImagePaths.Cut); } }
        public static ImageSource DevExpressLogo { get { return GetImage(ImagePaths.DevExpressLogo); } }
        public static ImageSource Error { get { return GetImage(ImagePaths.Error); } }
        public static ImageSource File { get { return GetImage(ImagePaths.File); } }
        public static ImageSource FileCS { get { return GetImage(ImagePaths.FileCS); } }
        public static ImageSource FileXaml { get { return GetImage(ImagePaths.FileXaml); } }
        public static ImageSource Find { get { return GetImage(ImagePaths.Find); } }
        public static ImageSource FindInFiles { get { return GetImage(ImagePaths.FindInFiles); } }
        public static ImageSource FindInFilesWindow { get { return GetImage(ImagePaths.FindInFilesWindow); } }
        public static ImageSource Info { get { return GetImage(ImagePaths.Info); } }
        public static ImageSource LoadLayout { get { return GetImage(ImagePaths.LoadLayout); } }
        public static ImageSource NewProject { get { return GetImage(ImagePaths.NewProject); } }
        public static ImageSource Notification { get { return GetImage(ImagePaths.Notification); } }
        public static ImageSource OpenFile { get { return GetImage(ImagePaths.OpenFile); } }
        public static ImageSource OpenSolution { get { return GetImage(ImagePaths.OpenSolution); } }
        public static ImageSource Output { get { return GetImage(ImagePaths.Output); } }
        public static ImageSource Paste { get { return GetImage(ImagePaths.Paste); } }
        public static ImageSource PropertiesWindow { get { return GetImage(ImagePaths.PropertiesWindow); } }
        public static ImageSource Redo { get { return GetImage(ImagePaths.Redo); } }
        public static ImageSource References { get { return GetImage(ImagePaths.References); } }
        public static ImageSource Refresh { get { return GetImage(ImagePaths.Refresh); } }
        public static ImageSource Replace { get { return GetImage(ImagePaths.Replace); } }
        public static ImageSource Run { get { return GetImage(ImagePaths.Run); } }
        public static ImageSource Save { get { return GetImage(ImagePaths.Save); } }
        public static ImageSource SaveAll { get { return GetImage(ImagePaths.SaveAll); } }
        public static ImageSource SaveLayout { get { return GetImage(ImagePaths.SaveLayout); } }
        public static ImageSource ShowAllFiles { get { return GetImage(ImagePaths.ShowAllFiles); } }
        public static ImageSource SolutionExplorer { get { return GetImage(ImagePaths.SolutionExplorer); } }
        public static ImageSource TaskList { get { return GetImage(ImagePaths.TaskList); } }
        public static ImageSource Toolbox { get { return GetImage(ImagePaths.Toolbox); } }
        public static ImageSource Undo { get { return GetImage(ImagePaths.Undo); } }
        public static ImageSource Warning { get { return GetImage(ImagePaths.Warning); } }

        static ImageSource GetImage(string path) {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }
    }
    #endregion
}
