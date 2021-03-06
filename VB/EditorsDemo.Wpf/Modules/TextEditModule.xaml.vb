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
Imports System.ComponentModel
Imports DevExpress.Xpf.Editors.Validation
Imports EditorsDemo.Utils

Namespace EditorsDemo



    Partial Public Class TextEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            InitSources()
            dDate.DateTime = Date.Now
        End Sub
        Private Sub InitSources()
            numericValue.ItemsSource = New Decimal() { 0.5D, 1D, 123.45D, -12.34D, 100D }

            Dim numericFormats As New List(Of FormatWrapper)()
            numericFormats.Add(New FormatWrapper("No formatting", String.Empty))
            numericFormats.Add(New FormatWrapper("Number", "n"))
            numericFormats.Add(New FormatWrapper("Currency", "c"))
            numericFormats.Add(New FormatWrapper("Scientific", "e"))
            numericFormats.Add(New FormatWrapper("Percent", "p"))
            numericFormat.ItemsSource = numericFormats

            Dim dateTimeFormats As New List(Of FormatWrapper)()
            dateTimeFormats.Add(New FormatWrapper("No formatting", String.Empty))
            dateTimeFormats.Add(New FormatWrapper("Short date", "d"))
            dateTimeFormats.Add(New FormatWrapper("Long date", "D"))
            dateTimeFormats.Add(New FormatWrapper("Short time", "t"))
            dateTimeFormats.Add(New FormatWrapper("Long time", "T"))
            dateTimeFormats.Add(New FormatWrapper("Full date/time", "f"))
            dateTimeFormats.Add(New FormatWrapper("General date/time", "g"))
            dateTimeFormats.Add(New FormatWrapper("Sortable date/time", "s"))
            dateTimeFormat.ItemsSource = dateTimeFormats

        End Sub
        Private Sub ButtonInfo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            txtEditValue.ClearValue(BaseEdit.EditValueProperty)
        End Sub
    End Class
End Namespace
