Imports System
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Utils
Imports DevExpress.Xpf.Bars
Imports DevExpress.Mvvm

Namespace PivotGridDemo
    Partial Public Class ContextMenu
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub PivotGridDemoModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGrid.BestFit(FieldArea.ColumnArea, True, False)
        End Sub

        Private Sub OnPivotGridCustomSummary(ByVal sender As Object, ByVal e As PivotCustomSummaryEventArgs)




            e.CustomValue = e.SummaryValue.Summary
        End Sub

        Private Sub OnPivotGridShowMenu(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
            If e.MenuType.Equals(PivotGridMenuType.Header) AndAlso e.GetFieldInfo().Field.Area = FieldArea.DataArea Then
                e.Customizations.Add(New BarItemLinkSeparator())
                Dim item As New BarSubItem() With {.Content = "Summary Type"}
                e.Customizations.Add(item)
                Dim arr As Array = EnumExtensions.GetValues(GetType(FieldSummaryType))
                For Each type As FieldSummaryType In arr
                  item.ItemLinks.Add(New BarButtonItem() With {.Name = "item" & type, .Content = type.ToString(), .CommandParameter = New FieldSummaryItem() With {.Type = type, .Field = e.GetFieldInfo().Field}, .Command = New DelegateCommand(Of FieldSummaryItem)(AddressOf SetFieldSummaryType, AddressOf CanSetFieldSummaryType)})
                Next type
            End If
        End Sub

        Private Sub SetFieldSummaryType(ByVal item As FieldSummaryItem)
            item.Field.SummaryType = item.Type
            item.Field.CellFormat = GetFormat(item.Type, item.Field)
            pivotGrid.BestFit(FieldArea.ColumnArea, True, False)
        End Sub

        Private Function GetFormat(ByVal fieldSummaryType As FieldSummaryType, ByVal field As PivotGridField) As String
            If field Is fieldQuantity Then
                Return ""
            End If
            Select Case fieldSummaryType
                Case FieldSummaryType.Average, FieldSummaryType.Sum, FieldSummaryType.Custom, FieldSummaryType.Max, FieldSummaryType.Min
                    Return "c"
                Case FieldSummaryType.Count
                    Return ""
                Case FieldSummaryType.StdDev, FieldSummaryType.StdDevp, FieldSummaryType.Var, FieldSummaryType.Varp
                    Return "p"
            End Select
            Return String.Empty
        End Function

        Private Function CanSetFieldSummaryType(ByVal item As FieldSummaryItem) As Boolean
            Return Not Object.Equals(item.Field.SummaryType, item.Type)
        End Function
    End Class

    Public Class FieldSummaryItem
        Public Property Field() As PivotGridField
        Public Property Type() As FieldSummaryType
    End Class
End Namespace
