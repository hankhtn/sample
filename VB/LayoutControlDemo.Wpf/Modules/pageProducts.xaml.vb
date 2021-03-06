Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Interop
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation

Namespace LayoutControlDemo
    Partial Public Class pageProducts
        Inherits LayoutControlDemoModule

        Private _SelectedPackIndex As Integer = -1

        Public Sub New()
            InitializeComponent()
            Dim DisabledProductBrush = CType(Resources("DisabledProductBrush"), Brush)
            RegisterName("DisabledProductBrush", DisabledProductBrush)
        End Sub

        Protected Sub UpdateProducts()
            Dim DisabledProductBrushAnimation = CType(Resources("DisabledProductBrushAnimation"), Storyboard)
            DisabledProductBrushAnimation.Stop()

            Dim packName As String = "Pack" & SelectedPackIndex.ToString()
            Dim EnabledProductBrush = TryCast(Resources("EnabledProductBrush"), Brush)
            Dim DisabledProductBrush = TryCast(Resources("DisabledProductBrush"), Brush)

            For Each element As FrameworkElement In layoutProducts.GetVisibleChildren()
                Dim textBlock = TryCast(element, TextBlock)
                If textBlock Is Nothing OrElse textBlock.Tag Is Nothing Then
                    Continue For
                End If
                Dim isEnabled As Boolean = SelectedPackIndex = -1 OrElse DirectCast(textBlock.Tag, String).Contains(packName)
                textBlock.Foreground = If(isEnabled, EnabledProductBrush, DisabledProductBrush)
            Next element

            DisabledProductBrushAnimation.Begin()
        End Sub

        Protected Property SelectedPackIndex() As Integer
            Get
                Return _SelectedPackIndex
            End Get
            Set(ByVal value As Integer)
                If _SelectedPackIndex <> value Then
                    _SelectedPackIndex = value
                    UpdateProducts()
                End If
            End Set
        End Property

        Private Sub PackTextBlock_RequestNavigate(ByVal sender As Object, ByVal e As RequestNavigateEventArgs)
            If BrowserInteropHelper.IsBrowserHosted Then
                Return
            End If
            Process.Start(New ProcessStartInfo(e.Uri.AbsoluteUri))
            e.Handled = True
        End Sub
        Private Sub PackTextBlock_MouseEnter(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim hyperlink As System.Windows.Documents.Hyperlink = TryCast(sender, System.Windows.Documents.Hyperlink)
            hyperlink.TextDecorations = TextDecorations.Underline
            If hyperlink IsNot Nothing Then
                SelectedPackIndex = Integer.Parse(hyperlink.Name.Chars(hyperlink.Name.Length - 1).ToString())
            End If
        End Sub
        Private Sub PackTextBlock_MouseLeave(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim hyperlink As System.Windows.Documents.Hyperlink = TryCast(sender, System.Windows.Documents.Hyperlink)
            hyperlink.TextDecorations = Nothing
            SelectedPackIndex = -1
        End Sub
    End Class
End Namespace
