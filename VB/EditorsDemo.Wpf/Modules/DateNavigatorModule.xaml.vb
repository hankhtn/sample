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
Imports DevExpress.Xpf.Core
Imports System.Windows.Threading
Imports DevExpress.Xpf.Core.Native
Imports System.Globalization

Namespace EditorsDemo
    Partial Public Class DateNavigatorModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            For Each day As DayOfWeek In DevExpress.Utils.EnumExtensions.GetValues(GetType(DayOfWeek))
                CType(FindName("chk" & day.ToString()), CheckEdit).IsChecked = navigator.Workdays.Contains(day)
            Next day
            UpdateButtonsEnabledState()
        End Sub

        Private Sub WeekDaysCheckEditChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim edit As CheckEdit = DirectCast(sender, CheckEdit)
            Dim day As DayOfWeek = GetDay(CStr(edit.Content))
            If Not navigator.Workdays.Contains(day) Then
                navigator.Workdays.Add(day)
            End If
        End Sub
        Private Sub WeekDaysCheckEditUnchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim edit As CheckEdit = DirectCast(sender, CheckEdit)
            Dim day As DayOfWeek = GetDay(CStr(edit.Content))
            navigator.Workdays.Remove(day)
        End Sub
        Private Function GetDay(ByVal day As String) As DayOfWeek
            Return DirectCast(System.Enum.Parse(GetType(DayOfWeek), day, False), DayOfWeek)
        End Function

        Private Sub AddSpecialDate(ByVal sender As Object, ByVal e As RoutedEventArgs)
            navigator.SpecialDates.Add(deSpecialDate.DateTime)
            UpdateButtonsEnabledState()
        End Sub
        Private Sub DeleteSpecialDate(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim selectedDates As New List(Of Object)()
            For Each [date] As Date In lbSpecialDates.SelectedItems
                selectedDates.Add([date])
            Next [date]
            For Each [date] As Date In selectedDates
                navigator.SpecialDates.Remove([date])
            Next [date]
            UpdateButtonsEnabledState()
        End Sub
        Private Sub DeleteAllSpecialDates(ByVal sender As Object, ByVal e As RoutedEventArgs)
            navigator.SpecialDates.Clear()
            UpdateButtonsEnabledState()
        End Sub
        Private Sub UpdateButtonsEnabledState()
            btnAddSpecialDate.IsEnabled = deSpecialDate.EditValue IsNot Nothing AndAlso Not navigator.SpecialDates.Contains(deSpecialDate.DateTime)
            btnDeleteSpecialDate.IsEnabled = lbSpecialDates.SelectedItems.Count <> 0
            btnDeleteAllSpecialDates.IsEnabled = navigator.SpecialDates.Count <> 0
        End Sub
        Private Sub deSpecialDate_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateButtonsEnabledState()
        End Sub
        Private Sub lbSpecialDates_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateButtonsEnabledState()
        End Sub
    End Class
End Namespace
