Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Controls
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Bars
Imports DevExpress.XtraRichEdit
Imports System.Windows.Data
Imports System
Imports System.Globalization
Imports System.Collections
Imports System.Windows.Markup
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Images

Namespace DevExpress.DevAV.Views
    Partial Public Class EmployeeMailMergeView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            DirectCast(richEdit, IRichEditControl).Print()
        End Sub
        Private Sub UserControl_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            richEdit.Views.PrintLayoutView.AllowDisplayLineNumbers = False
        End Sub
        Private Const additionOffset As Double = 137
        Private Sub richEdit_PopupMenuShowing(ByVal sender As Object, ByVal e As Xpf.RichEdit.PopupMenuShowingEventArgs)
            radialMenu.Items.Clear()
            Dim items = Validate(e.Menu.Items)
            For Each item In items
                radialMenu.Items.Add(item)
            Next item
            radialMenu.IsOpen = True
            e.Menu.Items.Clear()
        End Sub
        Private Const maxItemsInRadialmenu As Integer = 8
        Private Function Validate(ByVal items As CommonBarItemCollection) As List(Of BarItem)
            Dim filteredItems = items.Where(Function(x) TypeOf x Is BarItem).Select(Function(x) TryCast(x, BarItem)).Where(Function(x) x.IsEnabled AndAlso x.IsVisible AndAlso Not(TypeOf x Is BarItemSeparator)).ToList()
            UpdateImages(filteredItems)
            If filteredItems.Count <= maxItemsInRadialmenu Then
                Return filteredItems
            End If
            Dim firstLevelItems = filteredItems.Where(Function(i) TypeOf i Is BarSubItem).ToList()
            Dim anotherItems = filteredItems.Where(Function(i) Not(TypeOf i Is BarSubItem)).ToList()
            Dim additionCount As Integer = maxItemsInRadialmenu - 1 - firstLevelItems.Count
            Dim firstLevelAnotherItems = anotherItems.Take(additionCount).ToList()
            anotherItems.RemoveRange(0, additionCount)
            Dim secondLevelItems = anotherItems
            firstLevelItems.AddRange(firstLevelAnotherItems)
            Dim popupMenu = New PopupMenu()
            For Each item In secondLevelItems
                popupMenu.Items.Add(item)
            Next item
            firstLevelItems.Add(New BarSplitButtonItem() With {.PopupControl = popupMenu, .Content = "Actions"})
            Return firstLevelItems.ToList()
        End Function
        Private Sub UpdateImages(ByVal filteredItems As IEnumerable(Of BarItem))
            For Each item In filteredItems
                If item.Content Is Nothing Then
                    Continue For
                End If
                Dim path As String = Nothing
                If namesAndPaths.TryGetValue(item.Content.ToString(), path) Then
                    item.LargeGlyph = GetImage(path)
                    item.GlyphSize = GlyphSize.Large
                Else

                End If
                If TypeOf item Is BarSubItem Then
                    UpdateImages((TryCast(item, BarSubItem)).Items.Where(Function(i) TypeOf i Is BarItem).Select(Function(i) TryCast(i, BarItem)))
                End If
            Next item
        End Sub
        Private namesAndPaths As New Dictionary(Of String, String)() From {
            { "Toggle Field Codes", "ToggleFieldCodes_32x32.png" },
            { "Update Field", "UpdateField_32x32.png" },
            { "Paste", "Paste_32x32.png" },
            { "Increase Indent", "IndentIncrease_32x32.png" },
            { "Decrease Indent", "IndentDecrease_32x32.png" },
            { "Bookmark...", "Bookmark_32x32.png" },
            { "Paragraph...", "Paragraph_32x32.png" },
            { "Font...", "Font_32x32.png" },
            { "Hyperlink...", "Hyperlink_32x32.png" },
            { "Table Properties...", "TableProperties_32x32.png" },
            { "AutoFit", "TableAutoFitWindow_32x32.png" },
            { "Cell Alignment", "AlignMiddleCenter_32x32.png" },
            { "Split Cells...", "SplitTableCells_32x32.png" },
            { "Delete Cells...", "DeleteTableCells_32x32.png" },
            { "Insert", "InsertTable_32x32.png" },
            { "Insert Left", "InsertTableColumnsToTheLeft_32x32.png" },
            { "Insert Right", "InsertTableColumnsToTheRight_32x32.png" },
            { "Insert Above", "InsertTableRowsAbove_32x32.png" },
            { "Insert Below", "InsertTableRowsBelow_32x32.png" },
            { "Insert Cells", "InsertTableCells_32x32.png" },
            { "AutoFit Contents", "TableAutoFitContents_32x32.png" },
            { "AutoFit Window", "TableAutoFitWindow_32x32.png" },
            { "Fixed Column Width", "TableAutoFitContents_32x32.png" }
        }
        Private Function GetImage(ByVal name As String) As System.Windows.Media.ImageSource
            Dim stream = ImageResourceCache.Default.GetResourceByFileName(name, Utils.Design.ImageType.Colored)
            If stream Is Nothing Then
                Return Nothing
            End If

            Using stream
                Dim res = New BitmapImage()
                res.BeginInit()
                res.StreamSource = stream
                res.EndInit()
                Return res
            End Using
        End Function
    End Class
End Namespace
