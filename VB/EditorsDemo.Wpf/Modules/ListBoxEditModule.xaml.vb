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
Imports EditorsDemo.Utils
Imports System.Collections
Imports DevExpress.DemoData.Models

Namespace EditorsDemo
    Partial Public Class ListBoxEditModule
        Inherits EditorsDemoModule

        Private isLoadedCompleted As Boolean = False
        Public Sub New()
            InitializeComponent()
            edit.Focus()
            AddHandler Loaded, AddressOf ListBoxEditModule_Loaded
            InitSources()
        End Sub
        Private Sub InitSources()
            edit.ItemsSource = NWindContext.Create().CountriesArray

            Dim modes As New List(Of SelectionMode)()
            modes.Add(SelectionMode.Single)
            modes.Add(SelectionMode.Multiple)
            modes.Add(SelectionMode.Extended)
            selectionModeSelector.ItemsSource = modes
        End Sub
        Private Sub ListBoxEditModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            isLoadedCompleted = True
        End Sub
        Private Sub styleSelector_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not isLoadedCompleted Then
                Return
            End If
            Dim selectedStyleName As String = CStr(styleSelector.EditValue)
            Select Case selectedStyleName
                Case "Checked"
                    edit.StyleSettings = New CheckedListBoxEditStyleSettings()
                    selectionModeSelector.IsEnabled = False
                    allowHighlightingCheck.IsEnabled = False
                Case "Radio"
                    edit.StyleSettings = New RadioListBoxEditStyleSettings()
                    selectionModeSelector.IsEnabled = False
                    allowHighlightingCheck.IsEnabled = False
                Case Else
                    edit.StyleSettings = New ListBoxEditStyleSettings()
                    selectionModeSelector.IsEnabled = True
                    allowHighlightingCheck.IsEnabled = True
            End Select
        End Sub
        Private Sub selectionModeSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            selectedItemList.IsEnabled = edit.SelectionMode <> SelectionMode.Single
        End Sub
    End Class
    Public Class DisplayTextConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim values As IList = TryCast(value, IList)
            If values IsNot Nothing Then
                Dim builder As New StringBuilder()
                builder.Append("{")
                Dim isFirst As Boolean = True
                For Each obj As Object In values
                    If isFirst Then
                        builder.Append(obj)
                        isFirst = False
                        Continue For
                    End If
                    builder.AppendFormat(", {0}", obj)
                Next obj
                builder.Append("}")
                Return builder.ToString()
            End If
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
End Namespace
