Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils.Helpers
Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq

Namespace GridDemo
    <POCOViewModel>
    Public Class CardViewExplorerViewModel
        #Region "Properties"
        Private Const Root As String = "Root"

        Private ForwardStack As New Stack(Of String)()
        Private BackStack As New Stack(Of String)()

        Public Overridable Property SizeType() As SizeIcon
        Protected Sub OnSizeTypeChanged()
            Resize()
            Me.RaisePropertyChanged(Function(x) x.Size)
        End Sub

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


        Private path_Renamed As String
        Public Property Path() As String
            Get
                Return path_Renamed
            End Get
            Set(ByVal value As String)
                If value <> Root AndAlso (Not System.IO.Directory.Exists(value)) Then
                    RaisePathChanged()
                    Return
                End If
                If path_Renamed IsNot Nothing Then
                    ForwardStack.Push(path_Renamed)
                End If
                path_Renamed = value
                OpenFolder(value)
                RaisePathChanged()
            End Set
        End Property

        Public Overridable Property IsLoading() As Boolean
        Public Overridable Property SearchText() As String
        Private privateSource As ObservableCollectionCore(Of CardViewExplorerFileSource)
        Public Property Source() As ObservableCollectionCore(Of CardViewExplorerFileSource)
            Get
                Return privateSource
            End Get
            Private Set(ByVal value As ObservableCollectionCore(Of CardViewExplorerFileSource))
                privateSource = value
            End Set
        End Property
        Public Property CurrentItem() As CardViewExplorerFileSource
        #End Region

        #Region "POCO commands"
        Public Sub ShowProperties()
            FileSystemHelper.ShellExecuteFileInfo(CurrentItem.FullPath, ShellExecuteInfoFileType.Properties)
        End Sub
        Public Function CanShowProperties() As Boolean
            Return CurrentItem IsNot Nothing
        End Function

        Public Sub Back()
            BackStack.Push(Path)
            Dim tmp As String = ForwardStack.Pop()
            path_Renamed = tmp
            RaisePathChanged()
            OpenFolder(tmp, False)
        End Sub
        Public Function CanBack() As Boolean
            Return ForwardStack.Count > 0
        End Function

        Public Sub Forward()
            Dim tmp As String = BackStack.Pop()
            ForwardStack.Push(path_Renamed)
            path_Renamed = tmp
            RaisePathChanged()
            OpenFolder(tmp, False)
        End Sub
        Public Function CanForward() As Boolean
            Return BackStack.Count > 0
        End Function

        Public Sub Open()
            Dim element As CardViewExplorerFileSource = CurrentItem
            If element.Type = CardViewExplorerFileSource.TypeElement.File Then
                FileSystemHelper.ShellExecuteFileInfo(CurrentItem.FullPath, ShellExecuteInfoFileType.Open)
            Else
                Path = element.FullPath
            End If
        End Sub
        Public Function CanOpen() As Boolean
            Return CurrentItem IsNot Nothing
        End Function

        Public Sub Up()
            If Path.Length <> 3 Then
                Path = System.IO.Directory.GetParent(Path).FullName
            Else
                Path = Root
            End If
        End Sub
        Public Function CanUp() As Boolean
            Return Path <> Root
        End Function
        #End Region

        #Region "Members"
        Protected Sub New()
            Source = New ObservableCollectionCore(Of CardViewExplorerFileSource)()
            SizeType = SizeIcon.Medium
            OpenRoot()
        End Sub

        Private Sub OpenFolder(ByVal path As String, Optional ByVal clearNextStack As Boolean = True)
            Source.Clear()
            Source.BeginUpdate()
            Try
                IsLoading = True
                If path = Root Then
                    OpenRoot()
                Else

                    Dim sizeType_Renamed As SizeIcon = SizeType

                    Dim size_Renamed As Integer = Size
                    For Each item In FileSystemHelper.GetSubDirs(path)
                        Source.Add(CardViewExplorerFileSource.Create(System.IO.Path.Combine(path, item), CardViewExplorerFileSource.TypeElement.Folder, sizeType_Renamed, size_Renamed))
                    Next item
                    For Each item In System.IO.Directory.EnumerateFiles(path)
                        Source.Add(CardViewExplorerFileSource.Create(item, CardViewExplorerFileSource.TypeElement.File, sizeType_Renamed, size_Renamed))
                    Next item
                End If
                If clearNextStack Then
                    BackStack.Clear()
                End If
            Catch ex As UnauthorizedAccessException
                System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                Back()
            Finally
                IsLoading = False
                Source.EndUpdate()
            End Try
        End Sub

        Private Sub Resize()
            Try
                IsLoading = True
                CardViewExplorerFileSource.ClearCache()
                For Each item As CardViewExplorerFileSource In Source
                    item.Resize(SizeType, Size)
                Next item
            Finally
                IsLoading = False
            End Try
        End Sub

        Private Sub OpenRoot()
            Source.Clear()
            For Each drive In System.IO.DriveInfo.GetDrives().Where(Function(x) x.DriveType = System.IO.DriveType.Fixed)
                Source.Add(CardViewExplorerFileSource.Create(drive.RootDirectory.Name, CardViewExplorerFileSource.TypeElement.Drive, SizeType, Size))
            Next drive
            path_Renamed = Root
            RaisePathChanged()
        End Sub

        Private Sub RaisePathChanged()
            Me.RaisePropertyChanged(Function(x) x.Path)
        End Sub
        #End Region
    End Class

    Public Enum SizeIcon
        <Display(Name := "Extra large icons")>
        ExtraLarge
        <Display(Name := "Large icons")>
        Large
        <Display(Name := "Medium icons")>
        Medium
    End Enum
End Namespace
