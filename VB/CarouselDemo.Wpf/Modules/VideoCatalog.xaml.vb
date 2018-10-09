Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Controls
Imports DevExpress.Xpf.Carousel
Namespace CarouselDemo
    Partial Public Class VideoCatalog
        Inherits CarouselDemoModule

        Public Shared ReadOnly MediaPathProperty As DependencyProperty = DependencyProperty.RegisterAttached("MediaPath", GetType(String), GetType(VideoCatalog), New FrameworkPropertyMetadata("test"), New ValidateValueCallback(AddressOf ValidateMediaPathProperty))
        Public Shared Function ValidateMediaPathProperty(ByVal value As Object) As Boolean
            Return True
        End Function
        Public Shared Function GetMediaPath(ByVal ic As ImageControl) As String
            Return CStr(ic.GetValue(MediaPathProperty))
        End Function
        Public Shared Sub SetMediaPath(ByVal ic As ImageControl, ByVal value As String)
            ic.SetValue(MediaPathProperty, value)
        End Sub
        Public Sub New()
            InitializeComponent()
            AddItems("/CarouselDemo;component/Data/Images/VideoPhotos", ItemType.BinaryImage, carousel)
        End Sub
        Protected Overrides Sub AddItems(ByVal path As String, ByVal it As ItemType, ByVal carousel As CarouselPanel)
            Dim items = CreateItems(path, it)
            Dim links() As String = { "http://tv.devexpress.com//Content//ASPxGridView//ASPxBinaryImageIntro//ASPxBinaryImageIntro.flv", "http://tv.devexpress.com//Content//XtraBars/Lesson4//XtraBars04.flv", "http://tv.devexpress.com//Content//AgLayoutControl//AgLayoutPromo1//AgLayoutPromo1.flv", "http://tv.devexpress.com//Content//SkinningLibrary//SkinsPromo1//SkinsPromo1.flv", "http://tv.devexpress.com//Content//ASPxperience//Lesson1//ASPxperience01.flv", "http://tv.devexpress.com//Content//Mehul//AGVFilterControl//AGVFilterControl.flv", "http://tv.devexpress.com//Content//ASPxGridView/AGVInitNewRows//AGVInitNewRows.flv", "http://tv.devexpress.com//Content//XtraSpellChecker//ASPxSpellChecker01//ASPxSpellChecker01.flv", "http://tv.devexpress.com//Content//ASPxGridView//AGVBindArrayList//AGVBindArrayList.flv", "http://tv.devexpress.com//Content//Mehul//FreeSilverLight//FreeSilverLightQuestions.flv" }
            Dim imageControl As ImageControl
            Dim i As Integer = 0
            For Each item In items
                Dim border = New Border()
                imageControl = New ImageControl()
                imageControl.Width = 70
                imageControl.Height = 70
                imageControl.Source = CType(item, Image).Source
                imageControl.Template = CType(TryFindResource("imageControlTemplate"), ControlTemplate)
                SetMediaPath(imageControl, links(i))
                i += 1
                carousel.Children.Add(imageControl)
            Next item
        End Sub
        Private Sub MediaElement_MediaOpened(ByVal sender As Object, ByVal e As RoutedEventArgs)
            splashImage.Visibility = Visibility.Collapsed
        End Sub

        Private Sub MediaElement_SourceUpdated(ByVal sender As Object, ByVal e As System.Windows.Data.DataTransferEventArgs)
            splashImage.Visibility = Visibility.Visible
        End Sub

        Private Sub splashImage_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            mediaElement.Play()
            splashImage.Visibility = Visibility.Collapsed
        End Sub
        Private Sub carousel_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            mediaElement.Stop()
            mediaElement.Close()
            splashImage.Visibility = Visibility.Visible
        End Sub
    End Class
End Namespace
