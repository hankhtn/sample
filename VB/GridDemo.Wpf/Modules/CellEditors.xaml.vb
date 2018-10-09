Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Grid
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace GridDemo



    <DevExpress.Xpf.DemoBase.CodeFile("Controls/Converters.(cs)"), DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GridCellMultiColumnLookupEditorTemplates.xaml")>
    Partial Public Class CellEditors
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim settings As New ComboBoxEditSettings() With {.IsTextEditable = False}
            ComboBoxEdit.SetupComboBoxSettingsEnumItemSource(Of Priority)(settings)
            colPriority.EditSettings = settings
            grid.ItemsSource = OutlookDataGenerator.CreateOutlookDataTable(1000)
            AddHandler booleanColumnEditorListBox.EditValueChanged, AddressOf booleanColumnEditorListBox_EditValueChanged
            AddHandler editorButtonShowModeListBox.EditValueChanged, AddressOf editorButtonShowModeListBox_EditValueChanged
            AddHandler viewsListBox.EditValueChanged, AddressOf viewsListBox_SelectionChanged
            alternativeDisplayTemplateCheckBox.IsChecked = True
            alternativeEditTemplateCheckBox.IsChecked = True
            AddHandler editorShowModeCombobox.EditValueChanged, AddressOf editorShowModeCombobox_EditValueChanged

        End Sub
#Region "options"

        Private Sub editorShowModeCombobox_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateEditorShowMode()
        End Sub
        Private Sub viewsListBox_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If viewsListBox.SelectedIndex = 0 Then
                grid.View = CType(FindResource("columnView"), GridViewBase)
            End If
            If viewsListBox.SelectedIndex = 1 Then
                grid.View = CType(FindResource("cardView"), GridViewBase)
            End If
            UpdateEditorButtonShowMode()
            UpdateEditorShowMode()
        End Sub
        Private Sub booleanColumnEditorListBox_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If booleanColumnEditorListBox.SelectedIndex = 0 Then
                colHasAttachment.EditSettings = Nothing
            End If
            If booleanColumnEditorListBox.SelectedIndex = 1 Then
                colHasAttachment.EditSettings = New TextEditSettings()
            End If
            If booleanColumnEditorListBox.SelectedIndex = 2 Then
                colHasAttachment.EditSettings = New ComboBoxEditSettings() With {
                    .ItemsSource = New Boolean() { True, False }
                }
            End If
        End Sub
        Private Sub editorButtonShowModeListBox_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateEditorButtonShowMode()
        End Sub
        Private Sub UpdateEditorButtonShowMode()
            If editorButtonShowModeListBox.SelectedIndex = 0 Then
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowOnlyInEditor
            End If
            If editorButtonShowModeListBox.SelectedIndex = 1 Then
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowForFocusedCell
            End If
            If editorButtonShowModeListBox.SelectedIndex = 2 Then
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowForFocusedRow
            End If
            If editorButtonShowModeListBox.SelectedIndex = 3 Then
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowAlways
            End If
        End Sub
        Private Sub UpdateEditorShowMode()
            If editorShowModeCombobox.SelectedIndex = 0 Then
                grid.View.EditorShowMode = EditorShowMode.MouseDown
            End If
            If editorShowModeCombobox.SelectedIndex = 1 Then
                grid.View.EditorShowMode = EditorShowMode.MouseDownFocused
            End If
            If editorShowModeCombobox.SelectedIndex = 2 Then
                grid.View.EditorShowMode = EditorShowMode.MouseUp
            End If
            If editorShowModeCombobox.SelectedIndex = 3 Then
                grid.View.EditorShowMode = EditorShowMode.MouseUpFocused
            End If
        End Sub
        Private Sub colHoursActive_Validate(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
            Dim value As Double = Convert.ToDouble(e.Value, e.Culture)
            If value <= 0 OrElse 1000 < value Then
                e.SetError("The Hours Active value must be greater than zero and less than or equal to 1000", ErrorType.Default)
            End If
        End Sub
#End Region
#Region "colHoursActive options"
        Private Sub alternativeDisplayTemplateCheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            colHoursActive.DisplayTemplate = CType(Resources("alternativeHoursActiveDisplayTemplate"), ControlTemplate)
        End Sub
        Private Sub alternativeDisplayTemplateCheckBox_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            colHoursActive.DisplayTemplate = Nothing
        End Sub
        Private Sub alternativeEditTemplateCheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            colHoursActive.EditTemplate = CType(Resources("alternativeHoursActiveEditTemplate"), ControlTemplate)
        End Sub
        Private Sub alternativeEditTemplateCheckBox_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            colHoursActive.EditTemplate = Nothing
        End Sub
#End Region
#Region "custom edit template events"
        Private Sub TextEditSettings_GetIsActivatingKey(ByVal sender As Object, ByVal e As GetIsActivatingKeyEventArgs)
            Select Case e.Key
                Case Key.Add, Key.Subtract, Key.OemPlus, Key.OemMinus
                    e.IsActivatingKey = (e.Modifiers = ModifierKeys.None) OrElse (e.Modifiers = ModifierKeys.Control)
            End Select
        End Sub
        Private Sub TextEditSettings_ProcessActivatingKey(ByVal sender As Object, ByVal e As ProcessActivatingKeyEventArgs)
            Dim command As RoutedCommand = GetCommand(e)
            If command IsNot Nothing Then
                command.Execute(Nothing, e.EditCore)
            End If
        End Sub
        Private Function GetCommand(ByVal e As ProcessActivatingKeyEventArgs) As RoutedCommand
            Select Case e.Key
                Case Key.Add, Key.OemPlus
                    Return If(e.Modifiers = ModifierKeys.None, Slider.IncreaseSmall, Slider.IncreaseLarge)
                Case Key.Subtract, Key.OemMinus
                    Return If(e.Modifiers = ModifierKeys.None, Slider.DecreaseSmall, Slider.DecreaseLarge)
            End Select
            Return Nothing
        End Function
#End Region
    End Class
End Namespace
