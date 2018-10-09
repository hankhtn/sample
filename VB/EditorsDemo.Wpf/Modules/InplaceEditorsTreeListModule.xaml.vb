Imports DevExpress.DemoData.Models
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemosHelpers.Grid
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo
    <CodeFile("ModuleResources/DataEditorTypesDescriptions.xaml"), CodeFile("ModuleResources/DataEditorTypesTemplates.xaml"), CodeFile("ModuleResources/DataEditorTypesClasses.(cs)")>
    Partial Public Class InplaceEditorsTreeListModule
        Inherits EditorsDemoModule

        Private list As TreeListMultiEditorsList
        Public Sub New()
            InitializeComponent()
            UpdateDescription()
            AssignDataSource()
            treeListView.ExpandAllNodes()
        End Sub
        Private Sub AssignDataSource()
            list = New TreeListMultiEditorsList()

            Dim templateSelector As New TreeListMultiEditorsTemplateSelector()

            For Each propertyDescriptor As PropertyDescriptor In DirectCast(list, ITypedList).GetItemProperties(Nothing)
                Dim column As New TreeListColumn() With {.FieldName = propertyDescriptor.Name}
                If column.FieldName = "Field" Then
                    column.AllowEditing = DefaultBoolean.False
                    column.Fixed = FixedStyle.Right
                End If
                If column.FieldName = "TemplateName" Then
                    column.Visible = False
                    column.ShowInColumnChooser = False
                End If
                If column.FieldName = "EditorType" Then
                    column.Fixed = FixedStyle.Left
                    column.AllowEditing = DefaultBoolean.False
                    column.Width = 200
                End If
                If column.FieldName = "Id" Then
                    column.Visible = False
                    column.ShowInColumnChooser = False
                End If
                If column.FieldName = "ParentId" Then
                    column.Visible = False
                    column.ShowInColumnChooser = False
                End If

                If column.FieldName <> "Field" AndAlso column.FieldName <> "EditorType" Then
                    column.CellTemplateSelector = templateSelector
                End If
                treeList.Columns.Add(column)
            Next propertyDescriptor
            treeList.ItemsSource = list
        End Sub
        Private Sub PART_Editor_DefaultButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim listBoxEdit As New ListBoxEdit() With {.ItemsSource = NWindContext.Create().CountriesArray, .ShowBorder = False}
            listBoxEdit.EditValue = treeList.GetCellValue(treeListView.FocusedRowHandle, CType(treeList.CurrentColumn, TreeListColumn))
            Dim closedHandler As DialogClosedDelegate = Sub(dialogResult? As Boolean)
                If dialogResult = True Then
                    treeListView.ShowEditor()
                    treeListView.ActiveEditor.EditValue = listBoxEdit.EditValue
                End If
            End Sub

            FloatingContainer.ShowDialogContent(listBoxEdit, treeListView.ActiveEditor, New Size(400, 300), New FloatingContainerParameters() With {.ClosedDelegate = closedHandler, .Title = "Select Country", .ContainerFocusable = False})
        End Sub
        Private lastDescription As String
        Private Sub UpdateDescription()
            If descriptionRichTextBox Is Nothing Then
                Return
            End If
            Dim blocks As BlockCollection = descriptionRichTextBox.Document.Blocks
            If treeListView.FocusedRowHandle = GridControl.InvalidRowHandle Then
                Return
            End If
            Dim newDescription As String = list.FieldDescriptions(treeListView.FocusedRowHandle).TemplateName & "Description"
            If newDescription = lastDescription Then
                Return
            End If
            lastDescription = newDescription
            Dim control As New ContentControl() With {.Template = TryCast(Resources(newDescription), ControlTemplate)}
            control.ApplyTemplate()
            Dim container As ParagraphContainer = TryCast(VisualTreeHelper.GetChild(control, 0), ParagraphContainer)

            blocks.Clear()
            blocks.Add(container.Paragraph)


        End Sub
        Private Sub TableView_ShowingEditor(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListShowingEditorEventArgs)
            e.Cancel = list.FieldDescriptions(treeListView.FocusedRowHandle).TemplateName = "ProgressBarEdit"
        End Sub
        Private Sub CurrentItemChanged(ByVal sender As Object, ByVal e As CurrentItemChangedEventArgs)
            UpdateDescription()
        End Sub
    End Class
    Public Class TreeListMultiEditorsList
        Inherits MultiEditorsListBaseSQLite

        Protected Overrides Sub CreateColumnCollection()
            Dim pds((Table.Count + 5) - 1) As MultiEditorsListPropertyDescriptorSQLite
            pds(0) = New MultiEditorsListPropertyDescriptorSQLite(Me, 0, "EditorType", True)
            For n As Integer = 1 To Table.Count
                pds(n) = New MultiEditorsListPropertyDescriptorSQLite(Me, n, "Product #" & n, False)
            Next n

            pds(Table.Count + 1) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 1, "Field", True)
            pds(Table.Count + 2) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 2, "TemplateName", True)
            pds(Table.Count + 3) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 3, "Id", True)
            pds(Table.Count + 4) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 4, "ParentId", True)
            ColumnCollection = New PropertyDescriptorCollection(pds)
        End Sub
        Public Overrides Function GetPropertyValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Object
            If columnIndex = 0 Then
                Return FieldDescriptions(rowIndex).EditorDisplayName
            End If
            If columnIndex = Table.Count + 1 Then
                Return FieldDescriptions(rowIndex).ColumnName
            End If
            If columnIndex = Table.Count + 2 Then
                Return FieldDescriptions(rowIndex).TemplateName
            End If
            If columnIndex = Table.Count + 3 Then
                Return FieldDescriptions(rowIndex).Id
            End If
            If columnIndex = Table.Count + 4 Then
                Return FieldDescriptions(rowIndex).ParentId
            End If
            Return Table(columnIndex - 1)(FieldDescriptions(rowIndex).ColumnName)
        End Function
    End Class
    Public Class TreeListMultiEditorsTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim data As GridCellData = DirectCast(item, GridCellData)
            Dim grid As TreeListControl = CType(data.View.DataControl, TreeListControl)
            Dim editorType As String = TryCast(grid.GetCellValue(data.RowData.RowHandle.Value, "TemplateName"), String)
            Return If(String.IsNullOrEmpty(editorType), Nothing, CType(grid.Resources(editorType), DataTemplate))
        End Function
    End Class
End Namespace

