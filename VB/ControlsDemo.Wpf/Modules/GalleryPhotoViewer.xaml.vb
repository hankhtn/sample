Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Bars
Imports ControlsDemo.GalleryDemo
Imports System.Windows.Media.Effects
Imports DevExpress.Xpf.Core
Imports DevExpress.Data.Extensions

Namespace ControlsDemo
    Partial Public Class GalleryPhotoViewer
        Inherits ControlsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Property SlideMode() As Boolean

        Private Sub GalleryMenu_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.GalleryItemEventArgs)
            gallery.ScrollToGroupByIndex(galleryMenu.Gallery.Groups(0).Items.IndexOf(e.Item))
        End Sub

        Private Sub ControlPanel_CommandClick(ByVal sender As Object, ByVal e As ControlPanelEventArgs)

            Select Case e.Command
                Case ControlPanelCommand.ZoomValueChanged
                    imageViewer.ScaleCenter(controlPanel.ZoomValue / 100)
                Case ControlPanelCommand.RotateLeft
                    RotateCounterclockwise()
                Case ControlPanelCommand.RotateRight
                    RotateClockwise()
                Case ControlPanelCommand.ZoomToOriginalSize
                    controlPanel.SetAndAnimateZoomValue(100)
                Case ControlPanelCommand.VerSize
                    controlPanel.SetAndAnimateZoomValue(imageViewer.VerticalFitScale * 100)
                Case ControlPanelCommand.HorSize
                    controlPanel.SetAndAnimateZoomValue(imageViewer.HorizontalFitScale * 100)
                Case ControlPanelCommand.AutoSize
                    controlPanel.SetAndAnimateZoomValue(imageViewer.GetBestFitScale() * 100)
                Case ControlPanelCommand.Next
                    ShowItemInImageVewer(GetNextItem(CType(imageViewer.Tag, GalleryItem)))
                Case ControlPanelCommand.Prior
                    ShowItemInImageVewer(GetPriorItem(CType(imageViewer.Tag, GalleryItem)))
                Case ControlPanelCommand.Play
                    slideViewer.Visibility = Visibility.Visible
                    slideViewer.NextSlideImageSource = CType(gallery.Gallery.Groups(0).Items(0).Caption, PhotoInfo).Source
                    slideViewer.Tag = gallery.Gallery.Groups(0).Items(0)
                    SlideMode = True
                    slideViewer.Play()
                Case ControlPanelCommand.Print
                    PrintCurrentImage()
            End Select
        End Sub

        Private Sub RotateClockwise()
            Select Case imageViewer.Rotation
                Case Rotation.Rotate0
                    imageViewer.Rotation = Rotation.Rotate90
                Case Rotation.Rotate90
                    imageViewer.Rotation = Rotation.Rotate180
                Case Rotation.Rotate180
                    imageViewer.Rotation = Rotation.Rotate270
                Case Rotation.Rotate270
                    imageViewer.Rotation = Rotation.Rotate0
            End Select
        End Sub
        Private Sub RotateCounterclockwise()
            Select Case imageViewer.Rotation
                Case Rotation.Rotate0
                    imageViewer.Rotation = Rotation.Rotate270
                Case Rotation.Rotate90
                    imageViewer.Rotation = Rotation.Rotate0
                Case Rotation.Rotate180
                    imageViewer.Rotation = Rotation.Rotate90
                Case Rotation.Rotate270
                    imageViewer.Rotation = Rotation.Rotate180
            End Select
        End Sub

        Private Function GetNextItem(ByVal item As GalleryItem) As GalleryItem
            Return GetItem(item, True)
        End Function
        Private Function GetPriorItem(ByVal item As GalleryItem) As GalleryItem
            Return GetItem(item, False)
        End Function

        Private Function GetItem(ByVal item As GalleryItem, ByVal [next] As Boolean) As GalleryItem
            If item Is Nothing Then
                Return Nothing
            End If
            Dim coeff = If([next], 1, -1)
            Dim itemIndex = item.Group.Items.IndexOf(item) + coeff
            If item.Group.Items.IsValidIndex(itemIndex) Then
                Return item.Group.Items(itemIndex)
            End If

            Dim groups = item.Group.Gallery.Groups
            Dim groupIndex = (groups.IndexOf(item.Group) + coeff + groups.Count) Mod groups.Count
            Dim group = groups(groupIndex)
            Return If([next], group.Items.First(), group.Items.Last())
        End Function
        Private Sub bntCloseImageViewer_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CloseImageViewPopup()
        End Sub
        Private Sub CloseImageViewPopup()

            mainView.Effect = Nothing
            imageViewPopup.Visibility = Visibility.Collapsed
        End Sub

        Private Sub Gallery_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.GalleryItemEventArgs)
            OpenImageViewPopup(e.Item)
        End Sub
        Private Sub OpenImageViewPopup(ByVal item As GalleryItem)
            ShowItemInImageVewer(item)
            mainView.Effect = New BlurEffect() With {.Radius = 3}

            imageViewPopup.Visibility = Visibility.Visible
        End Sub
        Private Sub ShowItemInImageVewer(ByVal item As GalleryItem)
            imageViewerTitle.Text = CType(item.Caption, PhotoInfo).Caption
            imageViewer.ImageSource = CType(CType(item.Caption, PhotoInfo).Source, BitmapSource)
            imageViewer.Tag = item
            imageViewer.Rotation = Rotation.Rotate0
            AddHandler imageViewer.LayoutUpdated, AddressOf imageViewer_LayoutUpdated
            FitImageInViewport()
        End Sub

        Private Sub imageViewer_LayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
            FitImageInViewport()
            RemoveHandler imageViewer.LayoutUpdated, AddressOf imageViewer_LayoutUpdated
        End Sub
        Private Sub FitImageInViewport()
            If imageViewer.ImageSource Is Nothing Then
                controlPanel.ZoomValue = 100
                Return
            End If
            Dim scaleWidth As Double = Math.Min(1.0, (imageViewer.Viewport.ActualWidth - 20) / imageViewer.ImageSource.PixelWidth)
            Dim scaleHeight As Double = Math.Min(1.0, (imageViewer.Viewport.ActualHeight - 20) / imageViewer.ImageSource.PixelHeight)
            controlPanel.ZoomValue = 100 * Math.Min(scaleWidth, scaleHeight)
        End Sub

        Private Sub imageViewer_MouseWheelZoom(ByVal sender As Object, ByVal e As EventArgs)
            controlPanel.ZoomValue = imageViewer.Scale * 100
        End Sub

        Private Sub slideViewer_BeforeCurrentSlideLoading(ByVal sender As Object, ByVal e As EventArgs)
            Dim item As GalleryItem = GetNextItem(CType(slideViewer.Tag, GalleryItem))
            slideViewer.Tag = item
            slideViewer.NextSlideImageSource = CType(item.Caption, PhotoInfo).Source
        End Sub
        Protected Overridable Sub OnMouseDownCore()
            If SlideMode Then
                SlideMode = False
                slideViewer.Stop()
                slideViewer.Visibility = Visibility.Collapsed
            End If
        End Sub
        Protected Overrides Sub OnMouseLeftButtonDown(ByVal e As MouseButtonEventArgs)
            OnMouseDownCore()
            MyBase.OnMouseLeftButtonDown(e)
        End Sub
        Protected Overrides Sub OnMouseRightButtonDown(ByVal e As MouseButtonEventArgs)
            OnMouseDownCore()
            MyBase.OnMouseRightButtonDown(e)
        End Sub

        Public Sub PrintCurrentImage()
            Dim printDialog As New PrintDialog()
            If printDialog.ShowDialog() = True Then
                Dim photoInfo As PhotoInfo = CType(CType(imageViewer.Tag, GalleryItem).Caption, PhotoInfo)
                printDialog.PrintVisual(New Image() With {.Source = photoInfo.Source}, photoInfo.Caption)
            End If
        End Sub
        Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
            MyBase.OnKeyDown(e)
            If e.Key = Key.Escape Then
                If SlideMode Then
                    SlideMode = False
                    slideViewer.Stop()
                    slideViewer.Visibility = Visibility.Collapsed
                    Return
                End If
                If imageViewPopup.Visibility = Visibility.Visible Then
                    CloseImageViewPopup()
                End If
            End If
        End Sub
    End Class

    Public Class PhotoInfo
        Public Property Source() As ImageSource
        Public Property Caption() As String
        Public Property SizeInfo() As String
        Public Property ViewSize() As Size
        Public Sub New()
            ViewSize = New Size(Double.NaN, Double.NaN)
        End Sub
    End Class
End Namespace
