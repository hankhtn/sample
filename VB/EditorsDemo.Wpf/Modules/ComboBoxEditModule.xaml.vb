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
Imports DevExpress.Xpf.Editors
Imports System.Windows.Markup
Imports System.IO
Imports DevExpress.DemoData.Models
Imports System.Data.Entity

Namespace EditorsDemo
    Partial Public Class ComboBoxEditModule
        Inherits EditorsDemoModule

        Private context As NWindContext = NWindContext.Create()
        Public Shared ReadOnly separators() As String = { ",", ";", "/", "-" }
        Public Sub New()
            InitializeComponent()
            InitSources()
        End Sub
        Private Sub InitSources()
            Dim platforms() As String = { "Win98", "Win2000", "WinNT", "WinXP", "Vista", "Win7" }
            checkedComboBox.ItemsSource = platforms
            checkedComboBox.SelectedItems.Add(platforms(4))
            checkedComboBox.SelectedItems.Add(platforms(5))
            cboSeparators.ItemsSource = separators

            defaultComboBox.ItemsSource = context.CountriesArray
            radioComboBox.ItemsSource = context.Categories.ToList()
            nonEditableComboBox.ItemsSource = context.CountriesArray
        End Sub
        Private Sub CheckEdit_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            radioComboBox.ItemTemplate = CType(Resources("productCategoryTemplate"), DataTemplate)
        End Sub
        Private Sub CheckEdit_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            radioComboBox.ClearValue(ComboBoxEdit.ItemTemplateProperty)
        End Sub
    End Class
    Public Class BytesToBitmapConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
