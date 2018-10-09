Imports DockingDemo.Helpers
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports System.Linq
Imports System.Windows.Media
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Layout.Core

Namespace DockingDemo.ViewModels
    Public Class PinnedTabsViewModel
        Public Shared Function Create() As PinnedTabsViewModel
            Return ViewModelSource.Create(Function() New PinnedTabsViewModel())
        End Function
        Protected Sub New()
            Photos = New ObservableCollection(Of NaturePhoto)()
            For i As Integer = 0 To 5
                Dim photo = ImagesHelper.NaturePhotos.ElementAt(i)
                Photos.Add(NaturePhoto.Create("Nature Photo #" & i.ToString(), photo))
            Next i
            Photos(0).TogglePin()
            Photos(0).PinLocation = TabHeaderPinLocation.Far
        End Sub

        Public Overridable Property Photos() As ObservableCollection(Of NaturePhoto)
    End Class
    Public Class NaturePhoto
        Public Shared Function Create(ByVal title As String, ByVal photo As ImageSource) As NaturePhoto
            Return ViewModelSource.Create(Function() New NaturePhoto(title, photo))
        End Function
        Protected Sub New(ByVal title As String, ByVal photo As ImageSource)
            Me.Title = title
            Me.Photo = photo
            Pinned = False
        End Sub
        Private privateTitle As String
        Public Property Title() As String
            Get
                Return privateTitle
            End Get
            Private Set(ByVal value As String)
                privateTitle = value
            End Set
        End Property
        Private privatePhoto As ImageSource
        Public Property Photo() As ImageSource
            Get
                Return privatePhoto
            End Get
            Private Set(ByVal value As ImageSource)
                privatePhoto = value
            End Set
        End Property
        Public Property Pinned() As Boolean
        Public Property PinLocation() As TabHeaderPinLocation
        Public Sub TogglePin()
            Pinned = Not Pinned
        End Sub
    End Class
End Namespace
