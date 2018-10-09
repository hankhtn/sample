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
Imports System.ComponentModel
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler.UI

Namespace SchedulerDemo.Forms



    Partial Public Class HospitalRecurrenceTypeEditor
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        #Region "ViewModel"
        Public Property ViewModel() As RecurrenceDialogViewModel
            Get
                Return DirectCast(GetValue(ViewModelProperty), RecurrenceDialogViewModel)
            End Get
            Set(ByVal value As RecurrenceDialogViewModel)
                SetValue(ViewModelProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ViewModelProperty As DependencyProperty = CreateViewModelProperty()
        Private Shared Function CreateViewModelProperty() As DependencyProperty
            Return DependencyProperty.Register("ViewModel", GetType(RecurrenceDialogViewModel), GetType(HospitalRecurrenceTypeEditor), New PropertyMetadata(Nothing, AddressOf OnRecurrenceElementChangedProperty))
        End Function
        Private Shared Sub OnRecurrenceElementChangedProperty(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, HospitalRecurrenceTypeEditor).OnRecurrenceElementChanged(DirectCast(e.OldValue, RecurrenceDialogViewModel), DirectCast(e.NewValue, RecurrenceDialogViewModel))
        End Sub
        Private Sub OnRecurrenceElementChanged(ByVal oldValue As RecurrenceDialogViewModel, ByVal newValue As RecurrenceDialogViewModel)
            SetRecurrenceType(ViewModel.RecurrenceType)
        End Sub
        #End Region
        #Region "IsReadOnly"
        Public Property IsReadOnly() As Boolean
            Get
                Return DirectCast(GetValue(IsReadOnlyProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsReadOnlyProperty, value)
            End Set
        End Property
        Public Shared ReadOnly IsReadOnlyProperty As DependencyProperty = DependencyProperty.Register("IsReadOnly", GetType(Boolean), GetType(HospitalRecurrenceTypeEditor), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnIsReadOnlyPropertyChanged)))
        Protected Shared Sub OnIsReadOnlyPropertyChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, HospitalRecurrenceTypeEditor).OnIsReadOnlyChanged(DirectCast(e.OldValue, Boolean), DirectCast(e.NewValue, Boolean))
        End Sub
        Protected Overridable Sub OnIsReadOnlyChanged(ByVal oldValue As Boolean, ByVal newValue As Boolean)
            brdRecurrenceType.IsHitTestVisible = Not newValue
        End Sub
        #End Region

        Protected Friend Overridable Sub SetRecurrenceType(ByVal type? As RecurrenceType)
            Select Case type
                Case RecurrenceType.Daily
                    DailyRadioButton.IsChecked = True
                Case RecurrenceType.Weekly
                    WeeklyRadioButton.IsChecked = True
                Case RecurrenceType.Monthly
                    MonthlyRadioButton.IsChecked = True
                Case Else
                    DailyRadioButton.IsChecked = True
            End Select
        End Sub

        Private Sub DailyRadioButton_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.RecurrenceType = RecurrenceType.Daily
        End Sub

        Private Sub WeeklyRadioButton_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.RecurrenceType = RecurrenceType.Weekly
        End Sub

        Private Sub MonthlyRadioButton_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.RecurrenceType = RecurrenceType.Monthly
        End Sub
    End Class
End Namespace
