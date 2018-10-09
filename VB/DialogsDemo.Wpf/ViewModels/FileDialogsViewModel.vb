Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports DialogsDemo.Helpers
Imports DevExpress.Xpf.Core
Imports System.Collections.Generic
Imports DevExpress.Xpf.Dialogs
Imports DevExpress.Mvvm.DataAnnotations
Imports CommonDemo.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace DialogsDemo
    Public Class FileDialogsViewModel
        Private ReadOnly Property DefaultPhotoGalleryPath() As String
            Get
                Return DialogsDemoHelper.GetPhotosPath("Photos")
            End Get
        End Property
        Public ReadOnly Property AdditionalPhotosPath() As String
            Get
                Return DialogsDemoHelper.GetPhotosPath("AdditionalPhotos")
            End Get
        End Property
        Private ReadOnly Property DefaultPhotoExtension() As String
            Get
                Return ".jpg"
            End Get
        End Property

        Private privatePhotos As ObservableCollection(Of PhotoData)
        <BindableProperty>
        Public Overridable Property Photos() As ObservableCollection(Of PhotoData)
            Get
                Return privatePhotos
            End Get
            Protected Set(ByVal value As ObservableCollection(Of PhotoData))
                privatePhotos = value
            End Set
        End Property
        <BindableProperty>
        Public Overridable Property CurrentPhoto() As PhotoData

        <BindableProperty>
        Public Overridable Property DefaultExt() As String
        Private privateGalleryDirectory As String
        <BindableProperty>
        Public Overridable Property GalleryDirectory() As String
            Get
                Return privateGalleryDirectory
            End Get
            Protected Set(ByVal value As String)
                privateGalleryDirectory = value
            End Set
        End Property

        Public ReadOnly Property Filters() As Filters
            Get
                Return FiltersHelper.PhotoFilters
            End Get
        End Property
        Public Property Extensions() As String()

        Public Sub New()
            UpdateExtensions()
            UpdateGallery(DefaultPhotoGalleryPath)
        End Sub

        Private Sub UpdateExtensions()
            Extensions = New String() { "JPG", "PNG", "BMP", "JPEG" }
            DefaultExt = Extensions.FirstOrDefault()
        End Sub
        Private Sub UpdateGallery(ByVal path As String)
            GalleryDirectory = System.IO.Path.GetFullPath(path)
            Photos = New ObservableCollection(Of PhotoData)(ImageHelper.GetJPGPhotos(GalleryDirectory))
            CurrentPhoto = Photos.FirstOrDefault()
        End Sub

        Public Sub ChangeGalleryDirectory()
            Dim folderBrowser = GetFolderBrowserDialog()
            If folderBrowser.ShowDialog().Value Then
                UpdateGallery(folderBrowser.FileName)
            End If
        End Sub
        Public Sub UploadPhotos()
            Dim openDialog = GetOpenDialog()
            If openDialog.ShowDialog().Value Then
                For Each file In openDialog.FileNames
                    Photos.Add(ImageHelper.GetJPGPhoto(file))
                    CopyPhoto(file, GalleryDirectory & "\" & Path.GetFileName(file))
                Next file
            End If
        End Sub

        Public Sub DownloadPhoto()
            Dim saveDialog = GetSaveDialog()
            If saveDialog.ShowDialog().Value Then
                Dim file = saveDialog.FileName
                CopyPhoto(Path.Combine(GalleryDirectory, CurrentPhoto.PhotoName & DefaultPhotoExtension), file)
            End If
        End Sub
        Public Function CanDownloadPhoto() As Boolean
            Return CurrentPhoto IsNot Nothing
        End Function

        Private Sub CopyPhoto(ByVal sourceFilePath As String, ByVal destFileName As String)
            Try
                File.Copy(sourceFilePath, destFileName, True)
            Catch e As System.Exception
                DXMessageBox.Show(e.Message, "Error")
            End Try
        End Sub

        #Region "dialogs"
        Private Function GetOpenDialog() As DXOpenFileDialog
            Return New DXOpenFileDialog() With {.InitialDirectory = AdditionalPhotosPath, .Filter = Filters.GetFilterString(), .Title = "DX Open Dialog", .Multiselect = True}
        End Function
        Private Function GetSaveDialog() As DXSaveFileDialog
            Return New DXSaveFileDialog() With {.InitialDirectory = AdditionalPhotosPath, .DefaultExt = DefaultExt, .Title = "DX Save Dialog"}
        End Function
        Private Function GetFolderBrowserDialog() As DXOpenFileDialog
            Return New DXOpenFileDialog() With {.InitialDirectory = DialogsDemoHelper.GetPhotosPath(String.Empty), .Title = "DX Open folder Dialog", .OpenFileDialogMode = OpenFileDialogMode.Folders}
        End Function
        #End Region
    End Class
End Namespace
