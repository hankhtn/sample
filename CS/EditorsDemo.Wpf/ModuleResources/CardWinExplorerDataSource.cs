using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using DevExpress.Utils.Helpers;

namespace EditorsDemo.ModuleResources {
    public class CardWinExplorerDataSource : INotifyPropertyChanged {

        const string Root = "Root";

        SizeIcon sizeType = SizeIcon.Medium;
        string path;
        string searchText;
        readonly Stack<string> UndoStack = new Stack<string>();
        readonly Stack<string> NextStack = new Stack<string>();
        readonly ObservableCollectionCore<CardWinExplorerFileSource> source = new ObservableCollectionCore<CardWinExplorerFileSource>();

        public event PropertyChangedEventHandler PropertyChanged;

        public enum SizeIcon {
            ExtraLarge,
            Large,
            Medium
        }

        public SizeIcon SizeType {
            get { return sizeType; }
            set {
                if (value != sizeType) {
                    sizeType = value;
                    Resize();
                    if (PropertyChanged != null) {
                        PropertyChanged(this, new PropertyChangedEventArgs("Size"));
                        PropertyChanged(this, new PropertyChangedEventArgs("SizeType"));
                    }
                }
            }
        }

        public int Size {
            get {
                switch (SizeType) {
                    case SizeIcon.ExtraLarge:
                        return 256;
                    case SizeIcon.Large:
                        return 128;
                    case SizeIcon.Medium:
                        return 65;
                    default:
                        return 128;
                }
            }
        }

        public string Path {
            get { return path; }
            set {
                string tempPath = value;
                bool fileExist = File.Exists(value);
                if (fileExist) 
                    tempPath = System.IO.Path.GetDirectoryName(tempPath);
                bool directoryExist = Directory.Exists(tempPath);

                if (value != Root && (!directoryExist && !fileExist)) {
                    OpenWeb(value);
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Path"));
                    return;
                }
                if (path != null)
                    UndoStack.Push(path);
                path = tempPath;
                if(fileExist)
                    OpenFile(value);
                else if(directoryExist || path == Root)
                    OpenFolder(tempPath);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Path"));
            }
        }
        void OpenFile(string path) {
            try {
                Process.Start(path);
            }
            catch {
            }
        }
        void OpenWeb(string path) {
            try {
                Process.Start("http://" +  path + ".com");
            }
            catch {
            }
        }
        public string SearchText {
            get { return searchText; }
            set {
                searchText = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchText"));
            }
        }

        public ObservableCollectionCore<CardWinExplorerFileSource> Source { get { return source; } }

        public CardWinExplorerFileSource CurrentItem { get; set; }

        public ICommand UndoCommand {
            get { return new DevExpress.Mvvm.DelegateCommand(CallUndo, () => UndoStack.Count > 0); }
        }

        public ICommand NextCommand {
            get { return new DevExpress.Mvvm.DelegateCommand(CallNext, () => NextStack.Count > 0); }
        }

        public ICommand OpenCommand {
            get { return new DevExpress.Mvvm.DelegateCommand(() => Open(CurrentItem), () => CurrentItem != null); }
        }

        public ICommand UpCommand {
            get { return new DevExpress.Mvvm.DelegateCommand(CallUp, () => Path != Root); }
        }

        public ICommand PropertyCommand {
            get { return new DevExpress.Mvvm.DelegateCommand(CallExecute, () => CurrentItem != null); }
        }

        public CardWinExplorerDataSource() {
            OpenRoot();
        }

        public void Open(CardWinExplorerFileSource element) {
            if (element.Type == CardWinExplorerFileSource.TypeElement.File)
                FileSystemHelper.ShellExecuteFileInfo(CurrentItem.FullPath, ShellExecuteInfoFileType.Open);
            else
                Path = element.FullPath;
        }

        void OpenFolder(string path, bool clearNextStack = true) {
            Source.Clear();
            Source.BeginUpdate();
            try {
                Mouse.OverrideCursor = Cursors.Wait;
                if (path == Root)
                    OpenRoot();
                else {
                    SizeIcon sizeType = SizeType;
                    int size = Size;
                    foreach (var item in FileSystemHelper.GetSubDirs(path))
                        Source.Add(new CardWinExplorerFileSource(System.IO.Path.Combine(path, item), CardWinExplorerFileSource.TypeElement.Folder, sizeType, size));
                    foreach (var item in Directory.EnumerateFiles(path))
                        Source.Add(new CardWinExplorerFileSource(item, CardWinExplorerFileSource.TypeElement.File, sizeType, size));
                }
                if (clearNextStack)
                    NextStack.Clear();
            }
            catch (UnauthorizedAccessException ex) {
                System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                CallUndo();
            }
            finally {
                Mouse.OverrideCursor = null;
                Source.EndUpdate();
            }
        }

        void Resize() {
            try {
                Mouse.OverrideCursor = Cursors.Wait;
                CardWinExplorerFileSource.ClearCache();
                foreach (CardWinExplorerFileSource item in Source)
                    item.Resize(SizeType, Size);
            }
            finally {
                Mouse.OverrideCursor = null;
            }
        }

        void OpenRoot() {
            Source.Clear();
            foreach (var drive in DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed))
                Source.Add(new CardWinExplorerFileSource(drive.RootDirectory.Name, CardWinExplorerFileSource.TypeElement.Drive, SizeType, Size));
            path = Root;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Path"));
        }

        void CallUp() {
            if (this.Path.Length != 3)
                Path = System.IO.Directory.GetParent(Path).FullName;
            else
                Path = Root;
        }
        void CallExecute() {
            FileSystemHelper.ShellExecuteFileInfo(CurrentItem.FullPath, ShellExecuteInfoFileType.Properties);
        }

        void CallUndo() {
            NextStack.Push(Path);
            string tmp = UndoStack.Pop();
            path = tmp;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Path"));
            OpenFolder(tmp, false);
        }

        void CallNext() {
            string tmp = NextStack.Pop();
            UndoStack.Push(path);
            path = tmp;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Path"));
            OpenFolder(tmp, false);
        }
        public IEnumerable<CardWinExplorerFileSource> Find(string path) {
            if(path == Root) 
                return DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed).Select(drive => new CardWinExplorerFileSource(drive.RootDirectory.Name, CardWinExplorerFileSource.TypeElement.Drive, SizeType, Size, false));            
            if (string.IsNullOrEmpty(path) || !IsPathValid(path))
                return Enumerable.Empty<CardWinExplorerFileSource>();           
            if(System.IO.Directory.Exists(path))
                path += System.IO.Path.DirectorySeparatorChar;
            var directory = System.IO.Path.GetDirectoryName(path) ;
            if (string.IsNullOrEmpty(directory))
                directory = System.IO.Path.GetPathRoot(path);
            string filter = System.IO.Path.GetFileName(path);

            var directories = FileSystemHelper.GetSubDirs(directory)
                .Where(x => x.StartsWith(filter, true, CultureInfo.InvariantCulture))
                .Select(x => new CardWinExplorerFileSource(System.IO.Path.Combine(directory, x), CardWinExplorerFileSource.TypeElement.Folder, SizeType, Size, false));
            var files = Directory.EnumerateFiles(directory, filter + "*")
                .Select(x => new CardWinExplorerFileSource(x, CardWinExplorerFileSource.TypeElement.File, SizeType, Size, false));

            return directories.Concat(files).ToList();
        }

        bool IsPathValid(string path) {
            Uri pathUri;
            Boolean isValidUri = Uri.TryCreate(path, UriKind.Absolute, out pathUri);
            return isValidUri && pathUri != null && pathUri.IsLoopback;
        }
    }
}
