Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Utils
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Ribbon
Imports System.ComponentModel
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Interop
Imports DevExpress.Xpf.Utils
Imports System.Windows.Navigation

Namespace RibbonDemo

    <CodeFiles("Modules/RibbonSimplePad/Views/RibbonSimplePad.xaml;" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/Views/RibbonSimplePad.xaml.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/ViewModels/SimplePadViewModel.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/Views/BackstageViewPanes.xaml;" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/Views/TemplateSelectors.xaml;" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/ViewModels/InlineImageViewModel.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/ViewModels/RecentItemViewModel.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/ViewModels/RibbonSimplePadOptionsViewModel.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/Services/BackstageViewService.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/Services/InlineImageService.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/TemplateSelectors/InlineImageContentTemplateSelector.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/TemplateSelectors/TemplateSelectorDictionary.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/TemplateSelectors/TextMarkerStyleTemplateSelector.(cs);" & ControlChars.CrLf & "                 Modules/RibbonSimplePad/Views/BackstageViewCommonStyles.xaml")>
    Partial Public Class RibbonSimplePad
        Inherits RibbonDemoModule

        Public Shared ReadOnly FontEditWidthProperty As DependencyProperty = DependencyProperty.Register("FontEditWidth", GetType(Double?), GetType(RibbonSimplePad), New FrameworkPropertyMetadata(Nothing))

        Public Property FontEditWidth() As Double?
            Get
                Return CDbl(GetValue(FontEditWidthProperty))
            End Get
            Set(ByVal value? As Double)
                SetValue(FontEditWidthProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            Ribbon = RibbonControl
            richControl.Document.Blocks.Add(New Paragraph(New Run("Select the image below to show a contextual tab.")))
            richControl.Document.Blocks.Add(New Paragraph(New InlineUIContainer() With {.Child = New InlineImage(InlineImageViewModel.Create("/RibbonDemo;component/Images/Clipart/caCompClientEnabled.png"))}))
            AddHandler ModuleLoaded, AddressOf OnModuleLoaded
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler Unloaded, AddressOf OnUnloaded
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RibbonControl.SetCurrentValue(RibbonControl.RibbonStyleProperty, GetRibbonStyle())
            RibbonControl.SetCurrentValue(RibbonControl.RibbonTitleBarVisibilityProperty, GetTitleBarVisibility())
            AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
            OnThemeChanged(Nothing, Nothing)
        End Sub
        Protected Overridable Function GetTitleBarVisibility() As RibbonTitleBarVisibility
            Return RibbonTitleBarVisibility.Auto
        End Function
        Protected Overridable Function GetRibbonStyle() As RibbonStyle
            Return RibbonStyle.Office2010
        End Function
        Protected Overrides Sub Hide()
            RibbonControl.CloseApplicationMenu()
            MyBase.Hide()
        End Sub

        Private Sub OnModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not Me.IsInDesignTool() Then
                richControl.SetFocus()
            End If
        End Sub
        Private Sub OnUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
        End Sub
        Private Sub OnThemeChanged(ByVal sender As DependencyObject, ByVal e As ThemeChangedRoutedEventArgs)
            If Not Dispatcher.CheckAccess() Then
                Return
            End If

            FontEditWidth = If(ThemeManager.GetIsTouchEnabled(Me), 90, 50)
        End Sub
    End Class
End Namespace
