Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.DemoData.Utils
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils.About
Imports DevExpress.Xpf
Imports DevExpress.Xpf.Accordion
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.Docking
Imports DevExpress.Xpf.PropertyGrid
Imports Microsoft.Win32

Namespace VisualStudioDocking.ViewModels
    Public Class DocumentViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            IsClosed = False
        End Sub
        Public Sub New(ByVal displayName As String, ByVal text As String)
            Me.New()
            DisplayName = displayName
            CodeLanguageText = New CodeLanguageText(CodeLanguage.CS, text)
        End Sub

        Private privateCodeLanguage As CodeLanguage
        Public Property CodeLanguage() As CodeLanguage
            Get
                Return privateCodeLanguage
            End Get
            Private Set(ByVal value As CodeLanguage)
                privateCodeLanguage = value
            End Set
        End Property
        Private privateCodeLanguageText As CodeLanguageText
        Public Property CodeLanguageText() As CodeLanguageText
            Get
                Return privateCodeLanguageText
            End Get
            Private Set(ByVal value As CodeLanguageText)
                privateCodeLanguageText = value
            End Set
        End Property
        Private privateDescription As String
        Public Property Description() As String
            Get
                Return privateDescription
            End Get
            Protected Set(ByVal value As String)
                privateDescription = value
            End Set
        End Property
        Private privateFilePath As String
        Public Property FilePath() As String
            Get
                Return privateFilePath
            End Get
            Protected Set(ByVal value As String)
                privateFilePath = value
            End Set
        End Property
        Private privateFooter As String
        Public Property Footer() As String
            Get
                Return privateFooter
            End Get
            Protected Set(ByVal value As String)
                privateFooter = value
            End Set
        End Property
        Protected Overrides ReadOnly Property WorkspaceName() As String
            Get
                Return "DocumentHost"
            End Get
        End Property

        Public Function OpenFile() As Boolean
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Visual C# Files (*.cs)|*.cs|XAML Files (*.xaml)|*.xaml"
            openFileDialog.FilterIndex = 1
            Dim dialogResult? As Boolean = openFileDialog.ShowDialog()
            Dim dialogResultOK As Boolean = dialogResult.HasValue AndAlso dialogResult.Value
            If dialogResultOK Then
                DisplayName = openFileDialog.SafeFileName
                FilePath = openFileDialog.FileName
                SetCodeLanguageProperties(Path.GetExtension(openFileDialog.SafeFileName))
                Dim fileStream As Stream = File.OpenRead(openFileDialog.FileName)
                Using reader As New StreamReader(fileStream)
                    CodeLanguageText = New CodeLanguageText(CodeLanguage, reader.ReadToEnd())
                End Using
                fileStream.Close()
            End If
            Return dialogResultOK
        End Function
        Public Overrides Sub OpenItemByPath(ByVal path As String)
            DisplayName = System.IO.Path.GetFileName(path)
            FilePath = path
            SetCodeLanguageProperties(System.IO.Path.GetExtension(path))
            CodeLanguageText = New CodeLanguageText(CodeLanguage, Function() GetCodeTextByPath(path))
            IsActive = True
        End Sub
        Private Function GenerateClassText(ByVal className As String) As String
            Dim text As String = "" & ControlChars.CrLf & _
"using System;" & ControlChars.CrLf & _
"using System.Collections.Generic;" & ControlChars.CrLf & _
"using System.Linq;" & ControlChars.CrLf & _
"using System.Text;" & ControlChars.CrLf & _
ControlChars.CrLf & _
"namespace VisualStudioDocking {{" & ControlChars.CrLf & _
"    class {0} {{" & ControlChars.CrLf & _
"    }}" & ControlChars.CrLf & _
"}}"
            Return String.Format(text, className)
        End Function
        Private Function GetCodeLanguage(ByVal fileExtension As String) As CodeLanguage
            Select Case fileExtension
                Case ".cs"
                    Return CodeLanguage.CS
                Case ".vb"
                    Return CodeLanguage.VB
                Case ".xaml"
                    Return CodeLanguage.XAML
                Case Else
                    Return CodeLanguage.Plain
            End Select
        End Function
        Private Function GetCodeTextByPath(ByVal path As String) As String
            Dim assembly As System.Reflection.Assembly = GetType(DocumentViewModel).Assembly
            Using stream As Stream = AssemblyHelper.GetResourceStream(assembly, path, True)
                If stream Is Nothing Then
                    Return GenerateClassText(System.IO.Path.GetFileNameWithoutExtension(path))
                End If
                Using reader As New StreamReader(stream)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Function
        Private Function GetDescription(ByVal codeLanguage As CodeLanguage) As String
            Select Case codeLanguage
                Case Me.CodeLanguage.CS
                    Return "Visual C# Source file"
                Case Me.CodeLanguage.VB
                    Return "Visual Basic Source file"
                Case Me.CodeLanguage.XAML
                    Return "Windows Markup File"
                Case Else
                    Return "Other file"
            End Select
        End Function
        Private Sub SetCodeLanguageProperties(ByVal fileExtension As String)
            CodeLanguage = GetCodeLanguage(fileExtension)
            Description = GetDescription(CodeLanguage)
            Footer = DisplayName
            Glyph = If(CodeLanguage Is CodeLanguage.XAML, Images.FileXaml, If(CodeLanguage Is CodeLanguage.CS, Images.FileCS, Nothing))
        End Sub
    End Class
    Public Class MainViewModel
        Private errorList As CommandViewModel
        Private lastOpenedItem As PanelWorkspaceViewModel
        Private loadLayout As CommandViewModel
        Private newFile As CommandViewModel
        Private newProject As CommandViewModel
        Private openFile As CommandViewModel
        Private openProject As CommandViewModel
        Private output As CommandViewModel
        Private properties As CommandViewModel
        Private save As CommandViewModel
        Private saveAll As CommandViewModel
        Private saveLayout As CommandViewModel
        Private searchResults As CommandViewModel
        Private solutionExplorer As CommandViewModel

        Private solutionExplorerViewModel_Renamed As SolutionExplorerViewModel
        Private toolbox As CommandViewModel

        Private workspaces_Renamed As ObservableCollection(Of WorkspaceViewModel)

        Public Sub New()
            ErrorListViewModel = CreatePanelWorkspaceViewModel(Of ErrorListViewModel)()
            OutputViewModel = CreatePanelWorkspaceViewModel(Of OutputViewModel)()
            PropertiesViewModel = CreatePanelWorkspaceViewModel(Of PropertiesViewModel)()
            SearchResultsViewModel = CreatePanelWorkspaceViewModel(Of SearchResultsViewModel)()
            ToolboxViewModel = CreatePanelWorkspaceViewModel(Of ToolboxViewModel)()
            Bars = New ReadOnlyCollection(Of BarModel)(CreateBars())
            InitDefaultLayout()
        End Sub

        Private privateBars As ReadOnlyCollection(Of BarModel)
        Public Property Bars() As ReadOnlyCollection(Of BarModel)
            Get
                Return privateBars
            End Get
            Private Set(ByVal value As ReadOnlyCollection(Of BarModel))
                privateBars = value
            End Set
        End Property
        Private privateErrorListViewModel As ErrorListViewModel
        Public Property ErrorListViewModel() As ErrorListViewModel
            Get
                Return privateErrorListViewModel
            End Get
            Private Set(ByVal value As ErrorListViewModel)
                privateErrorListViewModel = value
            End Set
        End Property
        Private privateOutputViewModel As OutputViewModel
        Public Property OutputViewModel() As OutputViewModel
            Get
                Return privateOutputViewModel
            End Get
            Private Set(ByVal value As OutputViewModel)
                privateOutputViewModel = value
            End Set
        End Property
        Private privatePropertiesViewModel As PropertiesViewModel
        Public Property PropertiesViewModel() As PropertiesViewModel
            Get
                Return privatePropertiesViewModel
            End Get
            Private Set(ByVal value As PropertiesViewModel)
                privatePropertiesViewModel = value
            End Set
        End Property
        Public Property SearchResultsViewModel() As SearchResultsViewModel
        Public ReadOnly Property SolutionExplorerViewModel() As SolutionExplorerViewModel
            Get
                If solutionExplorerViewModel_Renamed Is Nothing Then
                    solutionExplorerViewModel_Renamed = CreatePanelWorkspaceViewModel(Of SolutionExplorerViewModel)()
                    AddHandler solutionExplorerViewModel_Renamed.ItemOpening, AddressOf SolutionExplorerViewModel_ItemOpening
                    solutionExplorerViewModel_Renamed.Solution = Solution.Create()
                    OpenItem(solutionExplorerViewModel_Renamed.Solution.LastOpenedItem.FilePath)
                End If
                Return solutionExplorerViewModel_Renamed
            End Get
        End Property
        Private privateToolboxViewModel As ToolboxViewModel
        Public Property ToolboxViewModel() As ToolboxViewModel
            Get
                Return privateToolboxViewModel
            End Get
            Private Set(ByVal value As ToolboxViewModel)
                privateToolboxViewModel = value
            End Set
        End Property
        Public ReadOnly Property Workspaces() As ObservableCollection(Of WorkspaceViewModel)
            Get
                If workspaces_Renamed Is Nothing Then
                    workspaces_Renamed = New ObservableCollection(Of WorkspaceViewModel)()
                    AddHandler workspaces_Renamed.CollectionChanged, AddressOf OnWorkspacesChanged
                End If
                Return workspaces_Renamed
            End Get
        End Property
        Protected Overridable ReadOnly Property SaveLoadLayoutService() As IDockingSerializationDialogService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable Function CreateAboutCommands() As List(Of CommandViewModel)
            Dim showAboutCommnad = New DelegateCommand(AddressOf ShowAbout)
            Return New List(Of CommandViewModel)() From {
                New CommandViewModel("About", showAboutCommnad) With {.Glyph = Images.About}
            }
        End Function
        Protected Overridable Function CreateEditCommands() As List(Of CommandViewModel)
            Dim findCommand = New CommandViewModel("Find") With {.Glyph = Images.Find, .KeyGesture = New KeyGesture(Key.F, ModifierKeys.Control)}
            Dim replaceCommand = New CommandViewModel("Replace") With {.Glyph = Images.Replace, .KeyGesture = New KeyGesture(Key.H, ModifierKeys.Control)}
            Dim findInFilesCommand = New CommandViewModel("Find in Files") With {.Glyph = Images.FindInFiles, .KeyGesture = New KeyGesture(Key.F, ModifierKeys.Control Or ModifierKeys.Shift)}
            Dim list = New List(Of CommandViewModel)() From {findCommand, replaceCommand, findInFilesCommand}
            Dim findReplaceDocument As New CommandViewModel("Find and Replace", list)
            findReplaceDocument.IsEnabled = True
            findReplaceDocument.IsSubItem = True
            Return New List(Of CommandViewModel)() From {findReplaceDocument}
        End Function
        Protected Overridable Function CreateLayoutCommands() As List(Of CommandViewModel)
            loadLayout = New CommandViewModel("Load Layout...", New DelegateCommand(AddressOf OnLoadLayout)) With {.Glyph = Images.LoadLayout}
            saveLayout = New CommandViewModel("Save Layout...", New DelegateCommand(AddressOf OnSaveLayout)) With {.Glyph = Images.SaveLayout}
            Return New List(Of CommandViewModel)() From {loadLayout, saveLayout}
        End Function
        Protected Function CreatePanelWorkspaceViewModel(Of T As PanelWorkspaceViewModel)() As T
            Return ViewModelSource(Of T).Create()
        End Function
        Protected Overridable Function CreateViewCommands() As List(Of CommandViewModel)
            toolbox = GetShowCommand(ToolboxViewModel)
            solutionExplorer = GetShowCommand(SolutionExplorerViewModel)
            properties = GetShowCommand(PropertiesViewModel)
            errorList = GetShowCommand(ErrorListViewModel)
            output = GetShowCommand(OutputViewModel)
            searchResults = GetShowCommand(SearchResultsViewModel)
            Dim themesCommands = New List(Of CommandViewModel)()
            For Each theme As Theme In Theme.Themes.Where(Function(x) x.Category = Theme.VisualStudioCategory AndAlso x.Name.Contains("VS2017"))
                Dim command = New CommandViewModel(theme.Name.Replace("VS2017", ""), New DelegateCommand(Of Theme)(Sub(t) ThemeManager.SetTheme(Application.Current.Windows.OfType(Of Window)().FirstOrDefault(Function(x) x.IsActive), theme)))
                command.Glyph = New BitmapImage(theme.SmallGlyph)
                themesCommands.Add(command)
            Next theme
            Dim themesSwitcher As New CommandViewModel("Themes", themesCommands) With {.IsEnabled = True, .IsSubItem = True}
            Return New List(Of CommandViewModel)() From {toolbox, solutionExplorer, properties, errorList, output, searchResults, themesSwitcher}
        End Function
        Protected Sub OpenOrCloseWorkspace(ByVal workspace As PanelWorkspaceViewModel, Optional ByVal activateOnOpen As Boolean = True)
            If Workspaces.Contains(workspace) Then
                workspace.IsClosed = Not workspace.IsClosed
            Else
                Workspaces.Add(workspace)
                workspace.IsClosed = False
            End If
            If activateOnOpen AndAlso workspace.IsOpened Then
                SetActiveWorkspace(workspace)
            End If
        End Sub
        Private Function ActivateDocument(ByVal path As String) As Boolean
            Dim document = GetDocument(path)
            Dim isFound As Boolean = document IsNot Nothing
            If isFound Then
                document.IsActive = True
            End If
            Return isFound
        End Function
        Private Function CreateBars() As List(Of BarModel)
            Return New List(Of BarModel)() From {
                New BarModel("Main") With {.IsMainMenu = True, .Commands = CreateCommands()},
                New BarModel("Standard") With {.Commands = CreateToolbarCommands()}
            }
        End Function
        Private Function CreateCommands() As List(Of CommandViewModel)
            Return New List(Of CommandViewModel) From {
                New CommandViewModel("File", CreateFileCommands()),
                New CommandViewModel("Edit", CreateEditCommands()),
                New CommandViewModel("Layouts", CreateLayoutCommands()),
                New CommandViewModel("View", CreateViewCommands()),
                New CommandViewModel("Help", CreateAboutCommands())
            }
        End Function
        Private Function CreateDocumentViewModel() As DocumentViewModel
            Return CreatePanelWorkspaceViewModel(Of DocumentViewModel)()
        End Function
        Private Function CreateFileCommands() As List(Of CommandViewModel)
            Dim fileExecutedCommand = New DelegateCommand(Of Object)(AddressOf OnNewFileExecuted)
            Dim fileOpenCommand = New DelegateCommand(Of Object)(AddressOf OnFileOpenExecuted)

            Dim newCommand As New CommandViewModel("New") With {.IsSubItem = True}
            newProject = New CommandViewModel("Project...", fileExecutedCommand) With {.Glyph = Images.NewProject, .KeyGesture = New KeyGesture(Key.N, ModifierKeys.Control Or ModifierKeys.Shift), .IsEnabled = False}
            newFile = New CommandViewModel("New File", fileExecutedCommand) With {.Glyph = Images.File, .KeyGesture = New KeyGesture(Key.N, ModifierKeys.Control)}
            newCommand.Commands = New List(Of CommandViewModel)() From {newProject, newFile}

            Dim openCommand As New CommandViewModel("Open") With {.IsSubItem = True}
            openProject = New CommandViewModel("Project/Solution...") With {.Glyph = Images.OpenSolution, .IsEnabled = False, .KeyGesture = New KeyGesture(Key.O, ModifierKeys.Control Or ModifierKeys.Shift)}
            openFile = New CommandViewModel("Open File", fileOpenCommand) With {.Glyph = Images.OpenFile, .KeyGesture = New KeyGesture(Key.O, ModifierKeys.Control)}
            openCommand.Commands = New List(Of CommandViewModel)() From {openProject, openFile}

            Dim closeFile As New CommandViewModel("Close")
            Dim closeSolution As New CommandViewModel("Close Solution") With {.Glyph = Images.CloseSolution}
            save = New CommandViewModel("Save") With {.Glyph = Images.Save, .KeyGesture = New KeyGesture(Key.S, ModifierKeys.Control)}
            saveAll = New CommandViewModel("Save All") With {.Glyph = Images.SaveAll, .KeyGesture = New KeyGesture(Key.S, ModifierKeys.Control Or ModifierKeys.Shift)}

            Return New List(Of CommandViewModel)() From {newCommand, openCommand, GetSeparator(), closeFile, closeSolution, GetSeparator(), save, saveAll}
        End Function
        Private Function CreateToolbarCommands() As List(Of CommandViewModel)
            Dim start As New CommandViewModel("Start") With {.Glyph = Images.Run, .KeyGesture = New KeyGesture(Key.F5, ModifierKeys.None), .DisplayMode = BarItemDisplayMode.ContentAndGlyph}
            Dim combo As New CommandViewModel("Configuration") With {.IsSubItem = True, .IsComboBox = True}
            combo.Commands = New List(Of CommandViewModel)() From {
                New CommandViewModel("Debug"),
                New CommandViewModel("Release")
            }

            Dim cut As New CommandViewModel("Cut") With {.Glyph = Images.Cut, .KeyGesture = New KeyGesture(Key.X, ModifierKeys.Control)}
            Dim copy As New CommandViewModel("Copy") With {.Glyph = Images.Copy, .KeyGesture = New KeyGesture(Key.C, ModifierKeys.Control)}
            Dim paste As New CommandViewModel("Paste") With {.Glyph = Images.Paste, .KeyGesture = New KeyGesture(Key.V, ModifierKeys.Control)}

            Dim undo As New CommandViewModel("Undo") With {.Glyph = Images.Undo, .KeyGesture = New KeyGesture(Key.Z, ModifierKeys.Control)}
            Dim redo As New CommandViewModel("Redo") With {.Glyph = Images.Redo, .KeyGesture = New KeyGesture(Key.Y, ModifierKeys.Control)}

            Return New List(Of CommandViewModel)() From {newProject, newFile, openFile, save, saveAll, GetSeparator(), combo, start, GetSeparator(), cut, copy, paste, GetSeparator(), undo, redo, GetSeparator(), toolbox, solutionExplorer, properties, errorList, output, searchResults, GetSeparator(), loadLayout, saveLayout}
        End Function
        Private Function GetDocument(ByVal filePath As String) As DocumentViewModel
            Return Workspaces.OfType(Of DocumentViewModel)().FirstOrDefault(Function(x) x.FilePath = filePath)
        End Function
        Private Function GetSeparator() As CommandViewModel
            Return New CommandViewModel() With {.IsSeparator = True}
        End Function
        Private Function GetShowCommand(ByVal viewModel As PanelWorkspaceViewModel) As CommandViewModel
            Return New CommandViewModel(viewModel, New DelegateCommand(Sub() OpenOrCloseWorkspace(viewModel)))
        End Function
        Private Sub InitDefaultLayout()
            Dim panels = New List(Of PanelWorkspaceViewModel) From {ToolboxViewModel, SolutionExplorerViewModel, PropertiesViewModel, ErrorListViewModel}
            For Each panel In panels
                OpenOrCloseWorkspace(panel, False)
            Next panel
        End Sub
        Private Sub OnFileOpenExecuted(ByVal param As Object)
            Dim document = CreateDocumentViewModel()
            If (Not document.OpenFile()) OrElse ActivateDocument(document.FilePath) Then
                document.Dispose()
                Return
            End If
            OpenOrCloseWorkspace(document)
        End Sub
        Private Sub OnLoadLayout()
            SaveLoadLayoutService.LoadLayout()
        End Sub
        Private Sub OnNewFileExecuted(ByVal param As Object)
            Dim newItemName As String = solutionExplorerViewModel_Renamed.Solution.AddNewItemToRoot()
            OpenItem(newItemName)
        End Sub
        Private Sub OnSaveLayout()
            SaveLoadLayoutService.SaveLayout()
        End Sub
        Private Sub OnWorkspaceRequestClose(ByVal sender As Object, ByVal e As EventArgs)
            Dim workspace = TryCast(sender, PanelWorkspaceViewModel)
            If workspace IsNot Nothing Then
                workspace.IsClosed = True
                If TypeOf workspace Is DocumentViewModel Then
                    workspace.Dispose()
                    Workspaces.Remove(workspace)
                End If
            End If
        End Sub
        Private Sub OnWorkspacesChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
            If e.NewItems IsNot Nothing AndAlso e.NewItems.Count <> 0 Then
                For Each workspace As WorkspaceViewModel In e.NewItems
                    AddHandler workspace.RequestClose, AddressOf OnWorkspaceRequestClose
                Next workspace
            End If
            If e.OldItems IsNot Nothing AndAlso e.OldItems.Count <> 0 Then
                For Each workspace As WorkspaceViewModel In e.OldItems
                    RemoveHandler workspace.RequestClose, AddressOf OnWorkspaceRequestClose
                Next workspace
            End If
        End Sub
        Private Sub OpenItem(ByVal filePath As String)
            If ActivateDocument(filePath) Then
                Return
            End If
            lastOpenedItem = CreateDocumentViewModel()
            lastOpenedItem.OpenItemByPath(filePath)
            OpenOrCloseWorkspace(lastOpenedItem)
        End Sub
        Private Sub SetActiveWorkspace(ByVal workspace As WorkspaceViewModel)
            workspace.IsActive = True
        End Sub
        Private Sub ShowAbout()
            About.ShowAbout(ProductKind.DXperienceWPF)
        End Sub
        Private Sub SolutionExplorerViewModel_ItemOpening(ByVal sender As Object, ByVal e As SolutionItemOpeningEventArgs)
            OpenItem(e.SolutionItem.FilePath)
        End Sub
    End Class
    Public MustInherit Class PanelWorkspaceViewModel
        Inherits WorkspaceViewModel
        Implements IMVVMDockingProperties


        Private targetName_Renamed As String

        Protected Sub New()
            targetName_Renamed = WorkspaceName
        End Sub

        Protected MustOverride ReadOnly Property WorkspaceName() As String
        Private Property IMVVMDockingProperties_TargetName() As String Implements IMVVMDockingProperties.TargetName
            Get
                Return targetName_Renamed
            End Get
            Set(ByVal value As String)
                targetName_Renamed = value
            End Set
        End Property

        Public Overridable Sub OpenItemByPath(ByVal path As String)
        End Sub
    End Class
    Public MustInherit Class WorkspaceViewModel
        Inherits ViewModel

        Protected Sub New()
            IsClosed = True
        End Sub

        Public Event RequestClose As EventHandler

        Public Overridable Property IsActive() As Boolean
        <BindableProperty(OnPropertyChangedMethodName := "OnIsClosedChanged")>
        Public Overridable Property IsClosed() As Boolean
        Public Overridable Property IsOpened() As Boolean

        Public Sub Close()
            Dim handler As EventHandler = RequestCloseEvent
            If handler IsNot Nothing Then
                handler(Me, EventArgs.Empty)
            End If
        End Sub
        Protected Overridable Sub OnIsClosedChanged()
            IsOpened = Not IsClosed
        End Sub
    End Class

    Public MustInherit Class ViewModel
        Implements IDisposable

        Public ReadOnly Property BindableName() As String
            Get
                Return GetBindableName(DisplayName)
            End Get
        End Property
        Public Overridable Property DisplayName() As String
        Public Overridable Property Glyph() As ImageSource

        Private Function GetBindableName(ByVal name As String) As String
            Return "_" & System.Text.RegularExpressions.Regex.Replace(name, "\W", "")
        End Function

        #Region "IDisposable Members"
        Public Sub Dispose() Implements IDisposable.Dispose
            OnDispose()
        End Sub
        Protected Overridable Sub OnDispose()
        End Sub
#If DEBUG Then
        Protected Overrides Sub Finalize()
            Dim msg As String = String.Format("{0} ({1}) ({2}) Finalized", Me.GetType().Name, DisplayName, GetHashCode())
            System.Diagnostics.Debug.WriteLine(msg)
        End Sub
#End If
        #End Region 
    End Class

    #Region "Tool Panels"
    Public Class ErrorListViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Error List"
            Glyph = Images.TaskList
            [Error] = Images.Error
            Warning = Images.Warning
            Info = Images.Info
        End Sub

        Public Property [Error]() As ImageSource
        Public Property Info() As ImageSource
        Public Property Warning() As ImageSource
        Protected Overrides ReadOnly Property WorkspaceName() As String
            Get
                Return "BottomHost"
            End Get
        End Property
    End Class

    Public Class OutputViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Output"
            Glyph = Images.Output
            Text = "1>------ Build started: Project: VisualStudioInspiredUIDemo, Configuration: Debug Any CPU ------" & ControlChars.CrLf & _
"1>  DockingDemo -> C:\VisualStudioInspiredUIDemo.exe" & ControlChars.CrLf & _
"========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped =========="
        End Sub

        Private privateText As String
        Public Property Text() As String
            Get
                Return privateText
            End Get
            Private Set(ByVal value As String)
                privateText = value
            End Set
        End Property
        Protected Overrides ReadOnly Property WorkspaceName() As String
            Get
                Return "BottomHost"
            End Get
        End Property
    End Class

    Public Class PropertiesViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Properties"
            Glyph = Images.PropertiesWindow
            SelectedItem = New PropertyItem(New PropertyGridControl())
            Items = New List(Of PropertyItem) From {SelectedItem, New PropertyItem(New AccordionControl()), New PropertyItem(New DocumentPanel()), New PropertyItem(New DocumentGroup()), New PropertyItem(New DevExpress.Xpf.Docking.LayoutPanel())}
        End Sub

        Public Property Items() As List(Of PropertyItem)
        Public Overridable Property SelectedItem() As PropertyItem
        Protected Overrides ReadOnly Property WorkspaceName() As String
            Get
                Return "RightHost"
            End Get
        End Property
    End Class

    Public Class PropertyItem
        Public Sub New(ByVal data As Object)
            Me.Data = data
            Name = Me.Data.ToString()
        End Sub

        Public Property Data() As Object
        Public Property Name() As String
    End Class

    Public Class SearchResultsViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Search Results"
            Glyph = Images.FindInFilesWindow
            Text = "Matching lines: 0    Matching files: 0    Total files searched: 61"
        End Sub

        Private privateText As String
        Public Property Text() As String
            Get
                Return privateText
            End Get
            Private Set(ByVal value As String)
                privateText = value
            End Set
        End Property
        Protected Overrides ReadOnly Property WorkspaceName() As String
            Get
                Return "BottomHost"
            End Get
        End Property
    End Class

    Public Class Solution
        Inherits BindableBase

        Private codePaths() As String = { "MainWindow.xaml", "MainWindow.xaml.cs", "SplashScreenWindow.xaml", "SplashScreenWindow.xaml.cs", "Resources.xaml", "BarTemplateSelector.cs", "MainViewModel.cs" }
        Private newItemsCount As Integer

        Protected Sub New()
            Dim root As SolutionItem = SolutionItem.Create(Me, "WPFApplication", ImagePaths.SolutionExplorer)
            Dim properties As SolutionItem = SolutionItem.Create(Me, "Properties", ImagePaths.PropertiesWindow)
            Dim references As SolutionItem = SolutionItem.Create(Me, "References", ImagePaths.References)
            root.Items.Add(properties)
            root.Items.Add(references)
            Dim files = GetCodeFiles()
            For Each file In files
                root.Items.Add(file)
            Next file
            LastOpenedItem = files.FirstOrDefault()
            Items = New ObservableCollection(Of SolutionItem) From {root}
        End Sub

        Private privateItems As ObservableCollection(Of SolutionItem)
        Public Property Items() As ObservableCollection(Of SolutionItem)
            Get
                Return privateItems
            End Get
            Private Set(ByVal value As ObservableCollection(Of SolutionItem))
                privateItems = value
            End Set
        End Property
        Public Overridable Property LastOpenedItem() As SolutionItem

        Public Shared Function Create() As Solution
            Return ViewModelSource.Create(Function() New Solution())
        End Function
        Public Function AddNewItemToRoot() As String
            newItemsCount += 1
            Dim newItemName As String = String.Format("Class{0}.cs", newItemsCount)

            Dim solutionItem_Renamed = VisualStudioDocking.ViewModels.SolutionItem.CreateFile(Me, newItemName)
            TryCast(Items(0), SolutionItem).Items.Add(solutionItem_Renamed)
            Return newItemName
        End Function
        Private Function GetCodeFiles() As List(Of SolutionItem)
            Dim result = New List(Of SolutionItem)()
            Dim subFiles = New List(Of SolutionItem)()
            For Each codePath In codePaths
                If codePath.EndsWith(".xaml.cs") OrElse codePath.EndsWith(".xaml.vb") Then
                    subFiles.Add(SolutionItem.CreateFile(Me, codePath))
                Else
                    result.Add(SolutionItem.CreateFile(Me, codePath))
                End If
            Next codePath
            For Each subFile In subFiles
                Dim xamlFile = result.FirstOrDefault(Function(x) x.FilePath = subFile.FilePath.Replace(".xaml.cs", ".xaml").Replace(".xaml.vb", ".xaml"))
                If xamlFile Is Nothing Then
                    result.Add(subFile)
                Else
                    xamlFile.Items.Add(subFile)
                End If
            Next subFile
            Return result
        End Function
    End Class

    Public Class SolutionExplorerViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Solution Explorer"
            Glyph = Images.SolutionExplorer
            PropertiesWindow = Images.PropertiesWindow
            ShowAllFiles = Images.ShowAllFiles
            Refresh = Images.Refresh
        End Sub

        Public Event ItemOpening As EventHandler(Of SolutionItemOpeningEventArgs)

        Public Property PropertiesWindow() As ImageSource
        Public Property Refresh() As ImageSource
        Public Property ShowAllFiles() As ImageSource
        Public Property Solution() As Solution
        Protected Overrides ReadOnly Property WorkspaceName() As String
            Get
                Return "RightHost"
            End Get
        End Property

        Public Sub OpenItem(ByVal item As SolutionItem)
            If item.IsFile AndAlso ItemOpeningEvent IsNot Nothing Then
                ItemOpeningEvent.Invoke(Me, New SolutionItemOpeningEventArgs(item))
            End If
        End Sub
    End Class

    Public Class SolutionItem

        Private ReadOnly solution_Renamed As Solution


        Protected Sub New(ByVal solution_Renamed As Solution)
            Me.solution_Renamed = solution_Renamed
            Items = New ObservableCollection(Of SolutionItem)()
        End Sub

        Private privateDisplayName As String
        Public Property DisplayName() As String
            Get
                Return privateDisplayName
            End Get
            Private Set(ByVal value As String)
                privateDisplayName = value
            End Set
        End Property
        Private privateFilePath As String
        Public Property FilePath() As String
            Get
                Return privateFilePath
            End Get
            Private Set(ByVal value As String)
                privateFilePath = value
            End Set
        End Property
        Private privateGlyphPath As String
        Public Property GlyphPath() As String
            Get
                Return privateGlyphPath
            End Get
            Private Set(ByVal value As String)
                privateGlyphPath = value
            End Set
        End Property
        Public ReadOnly Property IsFile() As Boolean
            Get
                Return FilePath IsNot Nothing
            End Get
        End Property
        Private privateItems As ObservableCollection(Of SolutionItem)
        Public Property Items() As ObservableCollection(Of SolutionItem)
            Get
                Return privateItems
            End Get
            Private Set(ByVal value As ObservableCollection(Of SolutionItem))
                privateItems = value
            End Set
        End Property


        Public Shared Function Create(ByVal solution_Renamed As Solution, ByVal displayName As String, ByVal glyph As String) As SolutionItem

            Dim solutionItem_Renamed = ViewModelSource.Create(Function() New SolutionItem(solution_Renamed))
            solutionItem_Renamed.Do(Sub(x)
                x.DisplayName = displayName
                x.GlyphPath = glyph
            End Sub)
            Return solutionItem_Renamed
        End Function

        Public Shared Function CreateFile(ByVal solution_Renamed As Solution, ByVal path As String) As SolutionItem

            Dim solutionItem_Renamed = ViewModelSource.Create(Function() New SolutionItem(solution_Renamed))
            solutionItem_Renamed.Do(Sub(x)
                x.DisplayName = System.IO.Path.GetFileName(path)
                x.GlyphPath = If(System.IO.Path.GetExtension(path) = ".cs", ImagePaths.FileCS, ImagePaths.FileXaml)
                x.FilePath = path
            End Sub)
            Return solutionItem_Renamed
        End Function
    End Class

    Public Class SolutionItemOpeningEventArgs
        Inherits EventArgs


        Public Sub New(ByVal solutionItem_Renamed As SolutionItem)
            Me.SolutionItem = solutionItem_Renamed
        End Sub

        Public Property SolutionItem() As SolutionItem
    End Class

    Public Class ToolboxViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Toolbox"
            Glyph = Images.Toolbox
        End Sub

        Protected Overrides ReadOnly Property WorkspaceName() As String
            Get
                Return "Toolbox"
            End Get
        End Property
    End Class
    #End Region

    #Region "Bars"
    Public Class BarModel
        Inherits ViewModel

        Public Sub New(ByVal displayName As String)
            Me.DisplayName = displayName
        End Sub
        Public Property Commands() As List(Of CommandViewModel)
        Public Property IsMainMenu() As Boolean
    End Class

    Public Class CommandViewModel
        Inherits ViewModel

        Public Sub New()
        End Sub
        Public Sub New(ByVal displayName As String, ByVal subCommands As List(Of CommandViewModel))
            Me.New(displayName, Nothing, Nothing, subCommands)
        End Sub
        Public Sub New(ByVal displayName As String, Optional ByVal command As ICommand = Nothing)
            Me.New(displayName, Nothing, command, Nothing)
        End Sub
        Public Sub New(ByVal owner As WorkspaceViewModel, ByVal command As ICommand)
            Me.New(String.Empty, owner, command)
        End Sub
        Private Sub New(ByVal displayName As String, Optional ByVal owner As WorkspaceViewModel = Nothing, Optional ByVal command As ICommand = Nothing, Optional ByVal subCommands As List(Of CommandViewModel) = Nothing)
            IsEnabled = True
            Me.Owner = owner
            If Me.Owner IsNot Nothing Then
                Me.DisplayName = Me.Owner.DisplayName
                Glyph = Me.Owner.Glyph
            Else
                Me.DisplayName = displayName
            End If
            Me.Command = command
            Commands = subCommands
        End Sub

        Private privateCommand As ICommand
        Public Property Command() As ICommand
            Get
                Return privateCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateCommand = value
            End Set
        End Property
        Public Property Commands() As List(Of CommandViewModel)
        Public Property DisplayMode() As BarItemDisplayMode
        Public Property IsComboBox() As Boolean
        Public Property IsEnabled() As Boolean
        Public Property IsSeparator() As Boolean
        Public Property IsSubItem() As Boolean
        Public Property KeyGesture() As KeyGesture
        Private privateOwner As WorkspaceViewModel
        Public Property Owner() As WorkspaceViewModel
            Get
                Return privateOwner
            End Get
            Private Set(ByVal value As WorkspaceViewModel)
                privateOwner = value
            End Set
        End Property
    End Class
    #End Region

    #Region "Images"
    Public NotInheritable Class ImagePaths

        Private Sub New()
        End Sub

        Public Const About As String = folder & "About.png"
        Public Const CloseSolution As String = folder & "CloseSolution.png"
        Public Const Copy As String = folder & "Copy.png"
        Public Const Cut As String = folder & "Cut.png"
        Public Const DevExpressLogo As String = folder & "DevExpressLogo.png"
        Public Const [Error] As String = folder & "Error.png"
        Public Const File As String = folder & "File.png"
        Public Const FileCS As String = folder & "FileCS.png"
        Public Const FileXaml As String = folder & "FileXaml.png"
        Public Const Find As String = folder & "Find.png"
        Public Const FindInFiles As String = folder & "FindInFiles.png"
        Public Const FindInFilesWindow As String = folder & "FindInFilesWindow.png"
        Public Const Info As String = folder & "Info.png"
        Public Const LoadLayout As String = folder & "LoadLayout.png"
        Public Const NewProject As String = folder & "NewProject.png"
        Public Const Notification As String = folder & "Notification.png"
        Public Const OpenFile As String = folder & "OpenFile.png"
        Public Const OpenSolution As String = folder & "OpenSolution.png"
        Public Const Output As String = folder & "Output.png"
        Public Const Paste As String = folder & "Paste.png"
        Public Const PropertiesWindow As String = folder & "PropertiesWindow.png"
        Public Const Redo As String = folder & "Redo.png"
        Public Const References As String = folder & "References.png"
        Public Const Refresh As String = folder & "Refresh.png"
        Public Const Replace As String = folder & "Replace.png"
        Public Const Run As String = folder & "Run.png"
        Public Const Save As String = folder & "Save.png"
        Public Const SaveAll As String = folder & "SaveAll.png"
        Public Const SaveLayout As String = folder & "SaveLayout.png"
        Public Const ShowAllFiles As String = folder & "ShowAllFiles.png"
        Public Const SolutionExplorer As String = folder & "SolutionExplorer.png"
        Public Const TaskList As String = folder & "TaskList.png"
        Public Const Toolbox As String = folder & "Toolbox.png"
        Public Const Undo As String = folder & "Undo.png"
        Public Const Warning As String = folder & "Warning.png"
        Private Const folder As String = "/VisualStudioDocking;component/Images/Docking/"
    End Class

    Public NotInheritable Class Images

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property About() As ImageSource
            Get
                Return GetImage(ImagePaths.About)
            End Get
        End Property
        Public Shared ReadOnly Property CloseSolution() As ImageSource
            Get
                Return GetImage(ImagePaths.CloseSolution)
            End Get
        End Property
        Public Shared ReadOnly Property Copy() As ImageSource
            Get
                Return GetImage(ImagePaths.Copy)
            End Get
        End Property
        Public Shared ReadOnly Property Cut() As ImageSource
            Get
                Return GetImage(ImagePaths.Cut)
            End Get
        End Property
        Public Shared ReadOnly Property DevExpressLogo() As ImageSource
            Get
                Return GetImage(ImagePaths.DevExpressLogo)
            End Get
        End Property
        Public Shared ReadOnly Property [Error]() As ImageSource
            Get
                Return GetImage(ImagePaths.Error)
            End Get
        End Property
        Public Shared ReadOnly Property File() As ImageSource
            Get
                Return GetImage(ImagePaths.File)
            End Get
        End Property
        Public Shared ReadOnly Property FileCS() As ImageSource
            Get
                Return GetImage(ImagePaths.FileCS)
            End Get
        End Property
        Public Shared ReadOnly Property FileXaml() As ImageSource
            Get
                Return GetImage(ImagePaths.FileXaml)
            End Get
        End Property
        Public Shared ReadOnly Property Find() As ImageSource
            Get
                Return GetImage(ImagePaths.Find)
            End Get
        End Property
        Public Shared ReadOnly Property FindInFiles() As ImageSource
            Get
                Return GetImage(ImagePaths.FindInFiles)
            End Get
        End Property
        Public Shared ReadOnly Property FindInFilesWindow() As ImageSource
            Get
                Return GetImage(ImagePaths.FindInFilesWindow)
            End Get
        End Property
        Public Shared ReadOnly Property Info() As ImageSource
            Get
                Return GetImage(ImagePaths.Info)
            End Get
        End Property
        Public Shared ReadOnly Property LoadLayout() As ImageSource
            Get
                Return GetImage(ImagePaths.LoadLayout)
            End Get
        End Property
        Public Shared ReadOnly Property NewProject() As ImageSource
            Get
                Return GetImage(ImagePaths.NewProject)
            End Get
        End Property
        Public Shared ReadOnly Property Notification() As ImageSource
            Get
                Return GetImage(ImagePaths.Notification)
            End Get
        End Property
        Public Shared ReadOnly Property OpenFile() As ImageSource
            Get
                Return GetImage(ImagePaths.OpenFile)
            End Get
        End Property
        Public Shared ReadOnly Property OpenSolution() As ImageSource
            Get
                Return GetImage(ImagePaths.OpenSolution)
            End Get
        End Property
        Public Shared ReadOnly Property Output() As ImageSource
            Get
                Return GetImage(ImagePaths.Output)
            End Get
        End Property
        Public Shared ReadOnly Property Paste() As ImageSource
            Get
                Return GetImage(ImagePaths.Paste)
            End Get
        End Property
        Public Shared ReadOnly Property PropertiesWindow() As ImageSource
            Get
                Return GetImage(ImagePaths.PropertiesWindow)
            End Get
        End Property
        Public Shared ReadOnly Property Redo() As ImageSource
            Get
                Return GetImage(ImagePaths.Redo)
            End Get
        End Property
        Public Shared ReadOnly Property References() As ImageSource
            Get
                Return GetImage(ImagePaths.References)
            End Get
        End Property
        Public Shared ReadOnly Property Refresh() As ImageSource
            Get
                Return GetImage(ImagePaths.Refresh)
            End Get
        End Property
        Public Shared ReadOnly Property Replace() As ImageSource
            Get
                Return GetImage(ImagePaths.Replace)
            End Get
        End Property
        Public Shared ReadOnly Property Run() As ImageSource
            Get
                Return GetImage(ImagePaths.Run)
            End Get
        End Property
        Public Shared ReadOnly Property Save() As ImageSource
            Get
                Return GetImage(ImagePaths.Save)
            End Get
        End Property
        Public Shared ReadOnly Property SaveAll() As ImageSource
            Get
                Return GetImage(ImagePaths.SaveAll)
            End Get
        End Property
        Public Shared ReadOnly Property SaveLayout() As ImageSource
            Get
                Return GetImage(ImagePaths.SaveLayout)
            End Get
        End Property
        Public Shared ReadOnly Property ShowAllFiles() As ImageSource
            Get
                Return GetImage(ImagePaths.ShowAllFiles)
            End Get
        End Property
        Public Shared ReadOnly Property SolutionExplorer() As ImageSource
            Get
                Return GetImage(ImagePaths.SolutionExplorer)
            End Get
        End Property
        Public Shared ReadOnly Property TaskList() As ImageSource
            Get
                Return GetImage(ImagePaths.TaskList)
            End Get
        End Property
        Public Shared ReadOnly Property Toolbox() As ImageSource
            Get
                Return GetImage(ImagePaths.Toolbox)
            End Get
        End Property
        Public Shared ReadOnly Property Undo() As ImageSource
            Get
                Return GetImage(ImagePaths.Undo)
            End Get
        End Property
        Public Shared ReadOnly Property Warning() As ImageSource
            Get
                Return GetImage(ImagePaths.Warning)
            End Get
        End Property

        Private Shared Function GetImage(ByVal path As String) As ImageSource
            Return New BitmapImage(New Uri(path, UriKind.Relative))
        End Function
    End Class
    #End Region
End Namespace
