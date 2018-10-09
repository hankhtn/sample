Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows.Input
Imports DevExpress.Utils.Helpers

Namespace EditorsDemo.ModuleResources
    Public Class CardWinExplorerDataSource
        Implements INotifyPropertyChanged

        Private Const Root As String = "Root"


        Private sizeType_Renamed As SizeIcon = SizeIcon.Medium

        Private path_Renamed As String

        Private searchText_Renamed As String
        Private ReadOnly UndoStack As New Stack(Of String)()
        Private ReadOnly NextStack As New Stack(Of String)()

        Private ReadOnly source_Renamed As New ObservableCollectionCore(Of CardWinExplorerFileSource)()

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Enum SizeIcon
            ExtraLarge
            Large
            Medium
        End Enum

        Public Property SizeType() As SizeIcon
            Get
                Return sizeType_Renamed
            End Get
            Set(ByVal value As SizeIcon)
                If value <> sizeType_Renamed Then
                    sizeType_Renamed = value
                    Resize()
                    If PropertyChangedEvent IsNot Nothing Then
                        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Size"))
                        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SizeType"))
                    End If
                End If
            End Set
        End Property

        Public ReadOnly Property Size() As Integer
            Get
                Select Case SizeType
                    Case SizeIcon.ExtraLarge
                        Return 256
                    Case SizeIcon.Large
                        Return 128
                    Case SizeIcon.Medium
                        Return 65
                    Case Else
                        Return 128
                End Select
            End Get
        End Property

        Public Property Path() As String
            Get
                Return path_Renamed
            End Get
            Set(ByVal value As String)
                Dim tempPath As String = value
                Dim fileExist As Boolean = File.Exists(value)
                If fileExist Then
                    tempPath = System.IO.Path.GetDirectoryName(tempPath)
                End If
                Dim directoryExist As Boolean = Directory.Exists(tempPath)

                If value <> Root AndAlso ((Not directoryExist) AndAlso (Not fileExist)) Then
                    OpenWeb(value)
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Path"))
                    Return
                End If
                If path_Renamed IsNot Nothing Then
                    UndoStack.Push(path_Renamed)
                End If
                path_Renamed = tempPath
                If fileExist Then
                    OpenFile(value)
                ElseIf directoryExist OrElse path_Renamed = Root Then
                    OpenFolder(tempPath)
                End If
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Path"))
            End Set
        End Property
        Private Sub OpenFile(ByVal path As String)
            Try
                Process.Start(path)
            Catch
            End Try
        End Sub
        Private Sub OpenWeb(ByVal path As String)
            Try
                Process.Start("http://" & path & ".com")
            Catch
            End Try
        End Sub
        Public Property SearchText() As String
            Get
                Return searchText_Renamed
            End Get
            Set(ByVal value As String)
                searchText_Renamed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SearchText"))
            End Set
        End Property

        Public ReadOnly Property Source() As ObservableCollectionCore(Of CardWinExplorerFileSource)
            Get
                Return source_Renamed
            End Get
        End Property

        Public Property CurrentItem() As CardWinExplorerFileSource

        Public ReadOnly Property UndoCommand() As ICommand
            Get
                Return New DevExpress.Mvvm.DelegateCommand(AddressOf CallUndo, Function() UndoStack.Count > 0)
            End Get
        End Property

        Public ReadOnly Property NextCommand() As ICommand
            Get
                Return New DevExpress.Mvvm.DelegateCommand(AddressOf CallNext, Function() NextStack.Count > 0)
            End Get
        End Property

        Public ReadOnly Property OpenCommand() As ICommand
            Get
                Return New DevExpress.Mvvm.DelegateCommand(Sub() Open(CurrentItem), Function() CurrentItem IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property UpCommand() As ICommand
            Get
                Return New DevExpress.Mvvm.DelegateCommand(AddressOf CallUp, Function() Path <> Root)
            End Get
        End Property

        Public ReadOnly Property PropertyCommand() As ICommand
            Get
                Return New DevExpress.Mvvm.DelegateCommand(AddressOf CallExecute, Function() CurrentItem IsNot Nothing)
            End Get
        End Property

        Public Sub New()
            OpenRoot()
        End Sub

        Public Sub Open(ByVal element As CardWinExplorerFileSource)
            If element.Type = CardWinExplorerFileSource.TypeElement.File Then
                FileSystemHelper.ShellExecuteFileInfo(CurrentItem.FullPath, ShellExecuteInfoFileType.Open)
            Else
                Path = element.FullPath
            End If
        End Sub

        Private Sub OpenFolder(ByVal path As String, Optional ByVal clearNextStack As Boolean = True)
            Source.Clear()
            Source.BeginUpdate()
            Try
                Mouse.OverrideCursor = Cursors.Wait
                If path = Root Then
                    OpenRoot()
                Else

                    Dim sizeType_Renamed As SizeIcon = SizeType

                    Dim size_Renamed As Integer = Size
                    For Each item In FileSystemHelper.GetSubDirs(path)
                        Source.Add(New CardWinExplorerFileSource(System.IO.Path.Combine(path, item), CardWinExplorerFileSource.TypeElement.Folder, sizeType_Renamed, size_Renamed))
                    Next item
                    For Each item In Directory.EnumerateFiles(path)
                        Source.Add(New CardWinExplorerFileSource(item, CardWinExplorerFileSource.TypeElement.File, sizeType_Renamed, size_Renamed))
                    Next item
                End If
                If clearNextStack Then
                    NextStack.Clear()
                End If
            Catch ex As UnauthorizedAccessException
                System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                CallUndo()
            Finally
                Mouse.OverrideCursor = Nothing
                Source.EndUpdate()
            End Try
        End Sub

        Private Sub Resize()
            Try
                Mouse.OverrideCursor = Cursors.Wait
                CardWinExplorerFileSource.ClearCache()
                For Each item As CardWinExplorerFileSource In Source
                    item.Resize(SizeType, Size)
                Next item
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        Private Sub OpenRoot()
            Source.Clear()
            For Each drive In DriveInfo.GetDrives().Where(Function(x) x.DriveType = DriveType.Fixed)
                Source.Add(New CardWinExplorerFileSource(drive.RootDirectory.Name, CardWinExplorerFileSource.TypeElement.Drive, SizeType, Size))
            Next drive
            path_Renamed = Root
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Path"))
        End Sub

        Private Sub CallUp()
            If Me.Path.Length <> 3 Then
                Path = System.IO.Directory.GetParent(Path).FullName
            Else
                Path = Root
            End If
        End Sub
        Private Sub CallExecute()
            FileSystemHelper.ShellExecuteFileInfo(CurrentItem.FullPath, ShellExecuteInfoFileType.Properties)
        End Sub

        Private Sub CallUndo()
            NextStack.Push(Path)
            Dim tmp As String = UndoStack.Pop()
            path_Renamed = tmp
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Path"))
            OpenFolder(tmp, False)
        End Sub

        Private Sub CallNext()
            Dim tmp As String = NextStack.Pop()
            UndoStack.Push(path_Renamed)
            path_Renamed = tmp
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Path"))
            OpenFolder(tmp, False)
        End Sub
        Public Function Find(ByVal path As String) As IEnumerable(Of CardWinExplorerFileSource)
            If path = Root Then
                Return DriveInfo.GetDrives().Where(Function(x) x.DriveType = DriveType.Fixed).Select(Function(drive) New CardWinExplorerFileSource(drive.RootDirectory.Name, CardWinExplorerFileSource.TypeElement.Drive, SizeType, Size, False))
            End If
            If String.IsNullOrEmpty(path) OrElse (Not IsPathValid(path)) Then
                Return Enumerable.Empty(Of CardWinExplorerFileSource)()
            End If
            If System.IO.Directory.Exists(path) Then
                path &= System.IO.Path.DirectorySeparatorChar
            End If
            Dim directory = System.IO.Path.GetDirectoryName(path)
            If String.IsNullOrEmpty(directory) Then
                directory = System.IO.Path.GetPathRoot(path)
            End If
            Dim filter As String = System.IO.Path.GetFileName(path)

            Dim directories = FileSystemHelper.GetSubDirs(directory).Where(Function(x) x.StartsWith(filter, True, CultureInfo.InvariantCulture)).Select(Function(x) New CardWinExplorerFileSource(System.IO.Path.Combine(directory, x), CardWinExplorerFileSource.TypeElement.Folder, SizeType, Size, False))
            Dim files = System.IO.Directory.EnumerateFiles(directory, filter & "*").Select(Function(x) New CardWinExplorerFileSource(x, CardWinExplorerFileSource.TypeElement.File, SizeType, Size, False))

            Return directories.Concat(files).ToList()
        End Function

        Private Function IsPathValid(ByVal path As String) As Boolean
            Dim pathUri As Uri = Nothing
            Dim isValidUri As Boolean = Uri.TryCreate(path, UriKind.Absolute, pathUri)
            Return isValidUri AndAlso pathUri IsNot Nothing AndAlso pathUri.IsLoopback
        End Function
    End Class
End Namespace
