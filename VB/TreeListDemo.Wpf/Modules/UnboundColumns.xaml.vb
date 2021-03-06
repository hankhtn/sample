Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace TreeListDemo



    Partial Public Class UnboundColumns
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            Me.treeList.View.ExpandAllNodes()
        End Sub

        Private Sub showExpressionEditorButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            treeList.View.ShowUnboundExpressionEditor(treeList.Columns(DirectCast(CType(columnsList.SelectedItem, ListBoxItem).Tag, String)))
        End Sub

        Private Sub view_UnboundExpressionEditorCreated(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.UnboundExpressionEditorEventArgs)
            e.AutoCompleteExpressionEditorControl.Context.Columns.Add(New DevExpress.Data.Controls.ExpressionEditor.ColumnInfo() With {.Name = "Title", .Category = "Columns", .Type = GetType(String), .Description = "string Title"})
            e.AutoCompleteExpressionEditorControl.Context.Columns.Add(New DevExpress.Data.Controls.ExpressionEditor.ColumnInfo() With {.Name = "FirstName", .Category = "Columns", .Type = GetType(String), .Description = "string FirstName"})
            e.AutoCompleteExpressionEditorControl.Context.Columns.Add(New DevExpress.Data.Controls.ExpressionEditor.ColumnInfo() With {.Name = "LastName", .Category = "Columns", .Type = GetType(String), .Description = "string LastName"})
            e.AutoCompleteExpressionEditorControl.Context.Columns.Add(New DevExpress.Data.Controls.ExpressionEditor.ColumnInfo() With {.Name = "BirthDate", .Category = "Columns", .Type = GetType(Date), .Description = "string BirthDate"})
        End Sub
    End Class
    Public Class BooleanToVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean AndAlso DirectCast(value, Boolean) Then
                Return Visibility.Visible
            End If
            Return Visibility.Collapsed
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
