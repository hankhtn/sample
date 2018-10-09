Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Threading
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Utils

Namespace EditorsDemo



    Partial Public Class FontEditModule
        Inherits EditorsDemoModule


        Private selectedColor_Renamed As Color = Colors.Black
        Private Property SelectedColor() As Color
            Get
                Return Me.selectedColor_Renamed
            End Get
            Set(ByVal value As Color)
                Me.selectedColor_Renamed = value
                If Me.richControl IsNot Nothing Then
                    Me.richControl.TextColor = value
                End If
            End Set
        End Property
        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
            richControl.Text = "The DXEditors Library offers a collection of advanced data editors available for use within data entry forms, option editors and data-aware controls. Our editors provide seamless integration with the rest of our product line, including the data grid and toolbar-menu controls. When it comes to data input and representation, the DevExpress Data Editors Library is unmatched in providing the same level of customization and flexibility."
        End Sub
        <DefaultValue(False)>
        Private Property IsInUpdate() As Boolean
        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CType(eFontSize.EditSettings, ComboBoxEditSettings).ItemsSource = FontSizes.Sizes
            UpdateBarsValues()
            richControl.Focus()
        End Sub
        Private Sub eFontFamily_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If IsInUpdate Then
                Return
            End If
            richControl.TextFontFamily = eFontFamily.EditValue
            FocusRichControl()
        End Sub
        Private Sub eFontSize_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If IsInUpdate Then
                Return
            End If
            richControl.TextFontSize = eFontSize.EditValue
            FocusRichControl()
        End Sub
        Private Sub richControl_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateBarsValues()
        End Sub
        Private Sub UpdateBarsValues()
            IsInUpdate = True
            eFontFamily.EditValue = richControl.TextFontFamily
            eFontSize.EditValue = richControl.TextFontSize
            Dim textAlignment As TextAlignment = Me.richControl.GetTextAlignment()
            Me.bLeft.IsChecked = Object.Equals(textAlignment, System.Windows.TextAlignment.Left)
            Me.bCenter.IsChecked = Object.Equals(textAlignment, System.Windows.TextAlignment.Center)
            Me.bRight.IsChecked = Object.Equals(textAlignment,System.Windows.TextAlignment.Right)
            Me.bBold.IsChecked = Me.richControl.TextIsBold
            Me.bItalic.IsChecked = Me.richControl.TextIsItalic
            Me.bUnderline.IsChecked = Me.richControl.TextIsUnderline
            IsInUpdate = False
        End Sub
        Private Sub FocusRichControl()
            richControl.Dispatcher.BeginInvoke(DispatcherPriority.Normal, New ThreadStart(Function() richControl.Focus()))
        End Sub

        Private Sub fontColorChooser_ColorChanged(ByVal sender As Object, ByVal e As EventArgs)
            If IsInUpdate Then
                Return
            End If
            SelectedColor = DirectCast(sender, ColorChooser).Color
            FocusRichControl()
        End Sub

        Private Sub bBold_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate Then
                Return
            End If
            richControl.TextIsBold = CBool(Me.bBold.IsChecked)
            FocusRichControl()
        End Sub

        Private Sub bItalic_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate Then
                Return
            End If
            richControl.TextIsItalic = CBool(Me.bItalic.IsChecked)
            FocusRichControl()
        End Sub

        Private Sub bUnderline_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate Then
                Return
            End If
            richControl.TextIsUnderline = CBool(Me.bUnderline.IsChecked)
            FocusRichControl()
        End Sub

        Private Sub bLeft_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate OrElse richControl Is Nothing Then
                Return
            End If
            If CBool(DirectCast(sender, BarCheckItem).IsChecked) Then
                richControl.ToggleTextAlignmentLeft()
            End If
            FocusRichControl()
        End Sub

        Private Sub bCenter_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate OrElse richControl Is Nothing Then
                Return
            End If
            If CBool(DirectCast(sender, BarCheckItem).IsChecked) Then
                richControl.ToggleTextAlignmentCenter()
            End If
            FocusRichControl()
        End Sub

        Private Sub bRight_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate OrElse richControl Is Nothing Then
                Return
            End If
            If CBool(DirectCast(sender, BarCheckItem).IsChecked) Then
                richControl.ToggleTextAlignmentRight()
            End If
            FocusRichControl()
        End Sub

        Private Sub bFontColor_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate Then
                Return
            End If
            richControl.TextColor = SelectedColor
            FocusRichControl()
        End Sub
    End Class
End Namespace
