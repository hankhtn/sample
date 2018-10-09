Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Editors
Imports DevExpress.Data.Filtering

Namespace EditorsDemo
    Partial Public Class SearchLookUpEditOptions
        Inherits UserControl

        Public Property FocusedEditor() As BaseEdit
            Get
                Return DirectCast(GetValue(FocusedEditorProperty), BaseEdit)
            End Get
            Set(ByVal value As BaseEdit)
                SetValue(FocusedEditorProperty, value)
            End Set
        End Property

        Public Shared ReadOnly FocusedEditorProperty As DependencyProperty = DependencyProperty.Register("FocusedEditor", GetType(BaseEdit), GetType(SearchLookUpEditOptions), Nothing)


        Public Sub New()
            InitializeComponent()
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FilterCondition, FilterCondition)(filterConditionComboBox)
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FindMode, FindMode)(findModeComboBox)

            ComboBoxEdit.SetupComboBoxEnumItemSource(Of EditorPlacement, EditorPlacement)(addNewComboBox)

        End Sub
    End Class
End Namespace
