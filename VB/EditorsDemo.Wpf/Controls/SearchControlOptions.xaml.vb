Imports System
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace EditorsDemo
    Partial Public Class SearchControlOptions
        Inherits UserControl

        Public Shared ReadOnly FocusedControlProperty As DependencyProperty = DependencyProperty.Register("FocusedControl", GetType(SearchControl), GetType(SearchControlOptions), Nothing)
        Public Shared ReadOnly CustomColumnsProperty As DependencyProperty = DependencyProperty.Register("CustomColumns", GetType(ObservableCollection(Of String)), GetType(SearchControlOptions), Nothing)
        Public Sub New()
            InitializeComponent()
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FindMode, FindMode)(FindModeComboBox)
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FilterCondition, FilterCondition)(FilterConditionComboBox)
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of CriteriaOperatorType, CriteriaOperatorType)(CriteriaOperatorTypeComboBox)
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FilterByColumnsMode, FilterByColumnsMode)(FilterByColumnsModeComboBox)
            CustomColumns = New ObservableCollection(Of String)()
        End Sub

        Public Property CustomColumns() As ObservableCollection(Of String)
            Get
                Return DirectCast(GetValue(CustomColumnsProperty), ObservableCollection(Of String))
            End Get
            Private Set(ByVal value As ObservableCollection(Of String))
                SetValue(CustomColumnsProperty, value)
            End Set
        End Property
        Public Property FocusedControl() As SearchControl
            Get
                Return DirectCast(GetValue(FocusedControlProperty), SearchControl)
            End Get
            Set(ByVal value As SearchControl)
                SetValue(FocusedControlProperty, value)
            End Set
        End Property

        Private Sub FindModeComboBoxEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            ShowFindButtonCheckEdit.IsEnabled = Object.Equals(FindModeComboBox.EditValue, FindMode.FindClick)
        End Sub
        Private Sub FilterByColumnsModeComboBoxEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            CustomColumnsComboBox.IsEnabled = Object.Equals(FilterByColumnsModeComboBox.EditValue, FilterByColumnsMode.Custom)
        End Sub
        Private Sub CustomColumnsComboBoxSelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CustomColumns = New ObservableCollection(Of String)(CustomColumnsComboBox.SelectedItems.Cast(Of String)())
        End Sub
    End Class
    Public Class ToIntConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            If TypeOf value Is String Then
                Return Integer.Parse(DirectCast(value, String))
            End If
            Return Convert.ToInt32(value)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return value
        End Function
    End Class
End Namespace
