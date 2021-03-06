Imports DevExpress.Xpf.Ribbon
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Linq
Imports System.Windows.Controls
Imports System.Collections.Generic
Imports DevExpress.Mvvm.POCO
Imports System
Imports DevExpress.Mvvm.UI
Imports System.Windows.Media.Imaging
Imports System.Windows.Documents
Namespace RibbonDemo
    Public Class RibbonSimplePadViewModel
        #Region "properties"
        Public ReadOnly Property DXVersion() As String
            Get
                Return "Version: " & AssemblyInfo.VersionShort
            End Get
        End Property
        Public ReadOnly Property DXCopyright() As String
            Get
                Return AssemblyInfo.AssemblyCopyright
            End Get
        End Property
        Public Overridable Property SelectedImage() As InlineImageViewModel
        Public Overridable Property CurrentOptions() As RibbonSimplePadOptionsViewModel
        Public Overridable Property NewOptions() As RibbonSimplePadOptionsViewModel
        Public Property IsSelectionEmpty() As Boolean
        Public Property TextAlignment() As TextAlignment
        Public Property IsBold() As Boolean
        Public Property IsItalic() As Boolean
        Public Property IsUnderline() As Boolean
        Public Overridable Property FontSize() As Double?
        Public Overridable Property FontFamily() As String
        Public Property Foreground() As Color
        Public Property Background() As Color
        Public Property SelectedImageColor() As Color
        Private privateShapeTypes As InlineImageBorderType()
        Public Property ShapeTypes() As InlineImageBorderType()
            Get
                Return privateShapeTypes
            End Get
            Protected Set(ByVal value As InlineImageBorderType())
                privateShapeTypes = value
            End Set
        End Property
        Private privateListMarkerStyles As TextMarkerStyle()
        Public Property ListMarkerStyles() As TextMarkerStyle()
            Get
                Return privateListMarkerStyles
            End Get
            Protected Set(ByVal value As TextMarkerStyle())
                privateListMarkerStyles = value
            End Set
        End Property
        Private privateRecentDocuments As RecentItemViewModel()
        Public Property RecentDocuments() As RecentItemViewModel()
            Get
                Return privateRecentDocuments
            End Get
            Protected Set(ByVal value As RecentItemViewModel())
                privateRecentDocuments = value
            End Set
        End Property
        Private privateRecentPlaces As RecentItemViewModel()
        Public Property RecentPlaces() As RecentItemViewModel()
            Get
                Return privateRecentPlaces
            End Get
            Protected Set(ByVal value As RecentItemViewModel())
                privateRecentPlaces = value
            End Set
        End Property

        Private privateClipartImages As String()
        Public Property ClipartImages() As String()
            Get
                Return privateClipartImages
            End Get
            Protected Set(ByVal value As String())
                privateClipartImages = value
            End Set
        End Property
        Private privateFontSizes As IEnumerable(Of Double?)
        Public Property FontSizes() As IEnumerable(Of Double?)
            Get
                Return privateFontSizes
            End Get
            Protected Set(ByVal value As IEnumerable(Of Double?))
                privateFontSizes = value
            End Set
        End Property
        Private privateFontFamilies As String()
        Public Property FontFamilies() As String()
            Get
                Return privateFontFamilies
            End Get
            Protected Set(ByVal value As String())
                privateFontFamilies = value
            End Set
        End Property
        Private privateBorderWeightArray As Double()
        Public Property BorderWeightArray() As Double()
            Get
                Return privateBorderWeightArray
            End Get
            Protected Set(ByVal value As Double())
                privateBorderWeightArray = value
            End Set
        End Property
        Private privateImageScaleValueArray As String()
        Public Property ImageScaleValueArray() As String()
            Get
                Return privateImageScaleValueArray
            End Get
            Protected Set(ByVal value As String())
                privateImageScaleValueArray = value
            End Set
        End Property
        Private privatePageCategoryColors As Color()
        Public Property PageCategoryColors() As Color()
            Get
                Return privatePageCategoryColors
            End Get
            Protected Set(ByVal value As Color())
                privatePageCategoryColors = value
            End Set
        End Property
        #End Region
        #Region "services"
        Public Overridable ReadOnly Property ImageService() As IInlineImageService
            Get
                Return Nothing
            End Get
        End Property
        Public Overridable ReadOnly Property BackstageViewService() As IBackstageViewService
            Get
                Return Nothing
            End Get
        End Property
        #End Region
        #Region "commands"
        Public Sub NavigateToContacts(ByVal parameter As Object)
            BackstageViewService.Close()
            NavigateTo("http://devexpress.com/Home/ContactUs.xml")
        End Sub
        Public Sub NavigateToHomeSite(ByVal parameter As Object)
            NavigateTo("http://www.devexpress.com")
        End Sub
        Public Sub NavigateToOnlineHelp()
            BackstageViewService.Close()
            NavigateTo("http://documentation.devexpress.com/#WPF/CustomDocument7895")
        End Sub
        Public Sub NavigateToCodeCentral()
            BackstageViewService.Close()
            NavigateTo("http://www.devexpress.com/Support/Center/Example/ChangeFilterSet/1?FavoritesOnly=False&MyItemsOnly=False&MyTeamItemsOnly=False&TechnologyName=.NET&PlatformName=WPF&ProductName=DXRibbon%20for%20WPF&TicketType=Examples")
        End Sub
        Public Sub BackstageOpened()
            NewOptions.PageCategoryAlignment = CurrentOptions.PageCategoryAlignment
            NewOptions.PageCategoryColor = CurrentOptions.PageCategoryColor
            NewOptions.TitleBarVisibility = CurrentOptions.TitleBarVisibility
            NewOptions.ToolbarShowMode = CurrentOptions.ToolbarShowMode
            NewOptions.RibbonStyle = CurrentOptions.RibbonStyle
        End Sub
        Public Sub ApplyOptions()
            BackstageViewService.Close()
            CurrentOptions.PageCategoryAlignment = NewOptions.PageCategoryAlignment
            CurrentOptions.PageCategoryColor = NewOptions.PageCategoryColor
            CurrentOptions.TitleBarVisibility = NewOptions.TitleBarVisibility
            CurrentOptions.ToolbarShowMode = NewOptions.ToolbarShowMode
            CurrentOptions.RibbonStyle = NewOptions.RibbonStyle
        End Sub
        Public Sub ContainerChanged()
            SelectedImage = ImageService.GetViewModelFromContainer()
        End Sub
        Public Sub InsertImage(ByVal source As Object)
            Dim image As InlineImageViewModel = InlineImageViewModel.Create(source.ToString())
            ImageService.InsertImage(image)
        End Sub
        Public Sub SetRibbonStyle(ByVal ribbonStyle As RibbonStyle)
            CurrentOptions.RibbonStyle = ribbonStyle
        End Sub
        Private Function OnGrowFontCommandCanExecute() As Boolean
            Return FontSize IsNot Nothing
        End Function

        Public Overridable Sub GrowFont()
            FontSize += 2
        End Sub
        Public Overridable Sub ShrinkFont()
            FontSize = If(FontSize <= 2, FontSize, FontSize - 2)
        End Sub

        Private Function OnShrinkFontCommandCanExecute() As Boolean
            Return FontSize IsNot Nothing
        End Function
        #End Region

        Public Sub New()
            Initialize()
        End Sub

        Private Sub Initialize()
            ClipartImages = New String() { "/RibbonDemo;component/Images/Clipart/caCompClient.png", "/RibbonDemo;component/Images/Clipart/caCompClientEnabled.png", "/RibbonDemo;component/Images/Clipart/caDatabaseBlue.png", "/RibbonDemo;component/Images/Clipart/caDataBaseDisabled.png", "/RibbonDemo;component/Images/Clipart/caDataBaseGreen.png", "/RibbonDemo;component/Images/Clipart/caDataBaseViolet.png", "/RibbonDemo;component/Images/Clipart/caInet.png", "/RibbonDemo;component/Images/Clipart/caInetSearch.png", "/RibbonDemo;component/Images/Clipart/caModem.png", "/RibbonDemo;component/Images/Clipart/caModemEnabled.png", "/RibbonDemo;component/Images/Clipart/caNetCard.png", "/RibbonDemo;component/Images/Clipart/caNetwork.png", "/RibbonDemo;component/Images/Clipart/caNetworkEnabled.png", "/RibbonDemo;component/Images/Clipart/caServer.png", "/RibbonDemo;component/Images/Clipart/caServerEnabled.png", "/RibbonDemo;component/Images/Clipart/caWebCam.png" }
            RecentDocuments = New RecentItemViewModel() {
                New RecentItemViewModel("Recent Document 1", "c:\My Documents\Recent Document 1.rtf", "../../../Images/Icons/new-32x32.png"),
                New RecentItemViewModel("Recent Document 2", "c:\My Documents\Recent Document 2.rtf", "../../../Images/Icons/new-32x32.png"),
                New RecentItemViewModel("Recent Document 3", "c:\My Documents\Recent Document 3.rtf", "../../../Images/Icons/new-32x32.png"),
                New RecentItemViewModel("Recent Document 4", "c:\My Documents\Recent Document 4.rtf", "../../../Images/Icons/new-32x32.png")
            }

            RecentPlaces = New RecentItemViewModel() {
                New RecentItemViewModel("Recent place 1", "c:\My Documents\Recent place 1", "../../../Images/Icons/open-32x32.png"),
                New RecentItemViewModel("Recent place 2", "c:\My Documents\Recent place 2", "../../../Images/Icons/open-32x32.png"),
                New RecentItemViewModel("Recent place 3", "c:\My Documents\Recent place 3", "../../../Images/Icons/open-32x32.png")
            }
            CurrentOptions = RibbonSimplePadOptionsViewModel.Create()
            NewOptions = RibbonSimplePadOptionsViewModel.Create()
            ListMarkerStyles = New TextMarkerStyle() { TextMarkerStyle.None, TextMarkerStyle.Disc, TextMarkerStyle.Circle, TextMarkerStyle.Square, TextMarkerStyle.Box, TextMarkerStyle.LowerRoman, TextMarkerStyle.UpperRoman, TextMarkerStyle.LowerLatin, TextMarkerStyle.UpperLatin, TextMarkerStyle.Decimal }
            FontSizes = New Double?() { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144 }
            ShapeTypes = New InlineImageBorderType() { InlineImageBorderType.None, InlineImageBorderType.Rectangle, InlineImageBorderType.Circle, InlineImageBorderType.Triangle, InlineImageBorderType.Star, InlineImageBorderType.LeftArrow, InlineImageBorderType.RightArrow, InlineImageBorderType.DownArrow, InlineImageBorderType.UpArrow }
            FontFamilies = GetFontFamilies()
            BorderWeightArray = New Double() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
            ImageScaleValueArray = New String() { "25%", "50%", "75%", "100%", "125%", "150%", "175%", "200%", "250%", "300%", "400%", "500%" }
            PageCategoryColors = New Color() { Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green, Colors.Blue, Color.FromArgb(255, 75, 0, 130), Color.FromArgb(255, 238, 130, 238) }
        End Sub
        Private Function GetFontFamilies() As String()
            Return Fonts.SystemFontFamilies.Select(Function(f) f.ToString()).OrderBy(Function(f) f).ToArray()
        End Function

        Protected Overridable Sub NavigateTo(ByVal url As String)
            System.Diagnostics.Process.Start(url)
        End Sub
    End Class
End Namespace
