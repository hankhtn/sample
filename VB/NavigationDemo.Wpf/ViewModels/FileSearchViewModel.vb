Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace NavigationDemo
    Public Class FileSearchViewModel
        #Region "Static"

        Private Shared Function GetNestedDirectories(ByVal directories As IEnumerable(Of DirectoryInfo), ByVal predicate As Func(Of DirectoryInfo, Boolean)) As IEnumerable(Of DirectoryInfo)
            Return directories.SelectMany(Function(x)
                Dim childDirectories = New DirectoryInfo(){}
                Try
                    childDirectories = x.GetDirectories().Where(predicate).ToArray()
                Catch
                End Try
                Return { x }.Concat(GetNestedDirectories(childDirectories, predicate))
            End Function)
        End Function

        #End Region

        Private Const maximumFilesCount As Integer = 2000
        Private ReadOnly _selectedPaths As ObservableCollection(Of String)
        Private dateTimeKind As ModifiedIntervalKind = ModifiedIntervalKind.Undefined
        #Region "Properties"

        Protected Overridable ReadOnly Property FolderBrowserDialogService() As IFolderBrowserDialogService
            Get
                Return Me.GetService(Of IFolderBrowserDialogService)()
            End Get
        End Property

        Public Overridable Property SearchPattern() As String

        Public ReadOnly Property SelectedPaths() As ObservableCollection(Of String)
            Get
                Return _selectedPaths
            End Get
        End Property
        Public Overridable Property SelectedPath() As String

        Public Overridable Property DateKind() As SpecifiedDateKind

        Public Overridable Property FromDate() As Date
        Public Overridable Property ToDate() As Date

        Public Overridable Property IsSearchSubfolders() As Boolean
        Public Overridable Property IsSearchHiddenFilesAndFolders() As Boolean
        Public Overridable Property IsSearchSystemFolders() As Boolean
        Public Overridable Property IsReadOnlyFiles() As Boolean

        Public Overridable Property Searching() As Boolean
        Private privateSearchResult As ReadOnlyCollection(Of FileInfo)
        <DevExpress.Mvvm.DataAnnotations.BindableProperty>
        Public Overridable Property SearchResult() As ReadOnlyCollection(Of FileInfo)
            Get
                Return privateSearchResult
            End Get
            Protected Set(ByVal value As ReadOnlyCollection(Of FileInfo))
                privateSearchResult = value
            End Set
        End Property

        #End Region

        Public Sub New()
            _selectedPaths = New ObservableCollection(Of String)()
            FromDate = Date.Now
            ToDate = Date.Now
            IsSearchSystemFolders = True
            IsSearchSubfolders = True
        End Sub

        #Region "Command Methods"

        Public Sub OnInitialized()
            SearchPattern = ".exe"
            SetSelectedPath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)
            StartSearch()
        End Sub
        Public Overridable Sub SelectPath()
            If FolderBrowserDialogService.ShowDialog() Then
                SetSelectedPath(FolderBrowserDialogService.ResultPath)
            End If
        End Sub
        Public Sub SetSelectedPath(ByVal path As String)
            SelectedPaths.Add(path)
            SelectedPath = path
        End Sub
        Public Function StartSearch() As Task
            Searching = True
            Return Task.Factory.StartNew(Sub()
                WalkFolderTree()
                Searching = False
            End Sub)
        End Function
        Public Overridable Function CanStartSearch() As Boolean
            Return Not(String.IsNullOrEmpty(SearchPattern) OrElse String.IsNullOrEmpty(SelectedPath) OrElse Searching)
        End Function
        Public Overridable Sub Cancel()
            Me.GetAsyncCommand(Function(x) x.StartSearch()).CancelCommand.Execute(Nothing)
        End Sub
        Public Overridable Function CanCancel() As Boolean
            Return Searching
        End Function
        Public Overridable Sub SelectDateTimeKind(ByVal kind As ModifiedIntervalKind)
            dateTimeKind = kind
        End Sub

        #End Region
        #Region "Filtering Predicates"


        Private Function AllowFolderIterate(ByVal info_Renamed As FileSystemInfo) As Boolean
            Return GetHiddenElementPredicate(info_Renamed) AndAlso (IsSearchSystemFolders OrElse (Not info_Renamed.Attributes.HasFlag(FileAttributes.System)))
        End Function

        Private Function GetHiddenElementPredicate(ByVal info_Renamed As FileSystemInfo) As Boolean
            Return IsSearchHiddenFilesAndFolders OrElse Not info_Renamed.Attributes.HasFlag(FileAttributes.Hidden)
        End Function


        Private Function GetDateTimePredicate(ByVal info_Renamed As FileInfo) As Boolean
            Dim result = True
            Dim start? As Date = Nothing
            Dim finish? As Date = Nothing
            GetDateTimeBounds(start, finish)
            If start.HasValue AndAlso finish.HasValue Then
                If dateTimeKind <> ModifiedIntervalKind.SpecifiedDates OrElse DateKind = SpecifiedDateKind.Modified Then
                    result = DateTimeIsInRange(info_Renamed.LastWriteTime, start, finish)
                Else
                    result = DateTimeIsInRange(If(DateKind = SpecifiedDateKind.Accessed, info_Renamed.LastAccessTime, info_Renamed.CreationTime), start, finish)
                End If
            End If
            Return result
        End Function
        Private Sub GetDateTimeBounds(ByRef start? As Date, ByRef finish? As Date)
            start = Nothing
            finish = Nothing
            Select Case dateTimeKind
                Case ModifiedIntervalKind.LastWeek
                    start = Date.Now.Subtract(TimeSpan.FromDays(7))
                    finish = Date.Now
                Case ModifiedIntervalKind.PastMonth
                    Dim year As Integer = If(Date.Now.Month > 1, Date.Now.Year, Date.Now.Year - 1)
                    Dim month As Integer = If(Date.Now.Month > 1, Date.Now.Month - 1, 12)
                    start = New Date(year, month, 1)
                    finish = New Date(year, month, DateTime.DaysInMonth(year, month))
                Case ModifiedIntervalKind.PastYear
                    start = Date.Now.Subtract(TimeSpan.FromDays(365))
                    finish = Date.Now
                Case ModifiedIntervalKind.SpecifiedDates
                    start = FromDate
                    finish = ToDate
                Case Else
            End Select
        End Sub
        Private Function DateTimeIsInRange(ByVal dateTime As Date, ByVal start? As Date, ByVal finish? As Date) As Boolean
            Return dateTime >= start.Value AndAlso dateTime <= finish.Value
        End Function

        #End Region

        Private Sub WalkFolderTree()
            Dim directories = GetSearchDirectories()

            Dim searchResult_Renamed = New List(Of FileInfo)()
            For Each directoryInfo In directories
                If Me.GetAsyncCommand(Function(y) y.StartSearch()).IsCancellationRequested Then
                    Exit For
                End If
                If Not AllowFolderIterate(directoryInfo) Then
                    Continue For
                End If

                Dim files As IEnumerable(Of FileInfo) = Nothing
                Try
                    files = directoryInfo.GetFiles(String.Format("*{0}*", SearchPattern), SearchOption.TopDirectoryOnly).Where(Function(x) GetDateTimePredicate(x) AndAlso GetHiddenElementPredicate(x) AndAlso ((Not IsReadOnlyFiles) OrElse x.IsReadOnly)).ToArray()
                Catch
                    Continue For
                End Try

                searchResult_Renamed.AddRange(files)
                If searchResult_Renamed.Count > maximumFilesCount Then
                    Cancel()
                End If
            Next directoryInfo
            SearchResult = New ReadOnlyCollection(Of FileInfo)(searchResult_Renamed)
        End Sub
        Private Function GetSearchDirectories() As IEnumerable(Of DirectoryInfo)
            Dim rootInfo = { New DirectoryInfo(SelectedPath) }
            If Not IsSearchSubfolders Then
                Return rootInfo
            End If
            Return GetNestedDirectories(rootInfo, Function(x) GetHiddenElementPredicate(x))
        End Function
    End Class

    Public Enum SpecifiedDateKind
        <Display(Name := "Modified Date")>
        Modified
        <Display(Name := "Created Date")>
        Created
        <Display(Name := "Accessed Date")>
        Accessed
    End Enum

    Public Enum ModifiedIntervalKind
        Undefined
        LastWeek
        PastMonth
        PastYear
        SpecifiedDates
    End Enum
End Namespace
