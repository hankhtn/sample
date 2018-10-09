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
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Core
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.PropertyGrid
Imports DevExpress.Xpf.Bars

Namespace EditorsDemo

    Partial Public Class SimpleButtonModule
        Inherits EditorsDemoModule

        Public Property EventsLog() As ObservableCollection(Of String)
        Public Sub New()
            InitializeComponent()
            EventsLog = New ObservableCollection(Of String)()
            rbSimpleButton.IsChecked = True
        End Sub

        Private Sub OnRadioButtonChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim radioButton As RadioButton = TryCast(sender, RadioButton)
            Select Case radioButton.Name
                Case "rbSimpleButton"
                    propertyGrid.SelectedObject = simpleButton
                Case "rbSplitButton"
                    propertyGrid.SelectedObject = splitButton
                Case "rbDropDownButton"
                    propertyGrid.SelectedObject = dropdownButton
            End Select
        End Sub

        Private Sub AddStringInLog(ByVal button As Object)
            Dim buttonItem = TryCast(button, BarButtonItem)
            If buttonItem IsNot Nothing Then
                EventsLog.Add(String.Format("{0} - {1} '{2}' Click", Date.Now.ToShortTimeString(), buttonItem.GetType().Name, buttonItem.Content))
            Else
                EventsLog.Add(String.Format("{0} - {1} Click", Date.Now.ToShortTimeString(), button.GetType().Name))
            End If
            If EventsLog.Count > 20 Then
                EventsLog.RemoveAt(0)
            End If
        End Sub

        Private Sub OnButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = TryCast(sender, SimpleButton)
            If button IsNot Nothing AndAlso button.ButtonKind = ButtonKind.Toggle Then
                EventsLog.Add(String.Format("{0} - {1} state is: {2}", Date.Now.ToShortTimeString(), button.GetType().Name, button.IsChecked))
            End If
            AddStringInLog(sender)
        End Sub

        Private Sub OnBarButtonItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            AddStringInLog(sender)
        End Sub
    End Class
End Namespace
