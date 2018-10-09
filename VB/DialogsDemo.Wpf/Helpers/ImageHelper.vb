Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace CommonDemo.Helpers
    Public NotInheritable Class ImageHelper

        Private Sub New()
        End Sub

        Public Shared Function GetJPGPhotos(ByVal directoryPath As String) As IEnumerable(Of PhotoData)
            Return Directory.GetFiles(directoryPath, "*.jpg").Select(AddressOf GetJPGPhoto)
        End Function
        Public Shared Function GetJPGPhoto(ByVal photoPath As String) As PhotoData
            Return New PhotoData(photoPath)
        End Function
    End Class
    Public Class ImagesHelper
        Public Shared Function GetWebIcon(ByVal icon As String) As ImageSource
            Return New BitmapImage(New Uri("/DialogsDemo;component/Images/TabControl/" & icon, UriKind.Relative))
        End Function
    End Class
    Public Class PhotoData
        Private privatePhoto As BitmapImage
        Public Property Photo() As BitmapImage
            Get
                Return privatePhoto
            End Get
            Private Set(ByVal value As BitmapImage)
                privatePhoto = value
            End Set
        End Property
        Private privatePhotoName As String
        Public Property PhotoName() As String
            Get
                Return privatePhotoName
            End Get
            Private Set(ByVal value As String)
                privatePhotoName = value
            End Set
        End Property

        Public Sub New(ByVal photoPath As String)
            Photo = New BitmapImage(New Uri(photoPath))
            PhotoName = Path.GetFileNameWithoutExtension(photoPath)
        End Sub
    End Class
End Namespace
