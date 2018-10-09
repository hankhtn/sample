Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo


    Partial Public Class UnboundExpressions
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub PivotGridDemoModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGrid.BestFit(True, False)
            teBonusName.Text = GetUniqueFieldName()
        End Sub
        Private Function GetNewInvisibleBonusField() As PivotGridField
            Dim newBonusField As New PivotGridField(String.Empty, FieldArea.DataArea) With {.ValueTemplate = CType(Resources("UnboundFieldTemplate"), DataTemplate), .Name = GetUniqueFieldName(), .Caption = teBonusName.Text, .Visible = False, .UnboundType = FieldUnboundColumnType.Object, .UnboundExpression = beExpression.Text}
            Return newBonusField
        End Function
        Private Function GetUniqueFieldName() As String
            Dim fieldName As String = "NewBonus"
            Dim uniqueId As Integer = Enumerable.Range(0, Integer.MaxValue).First(Function(i) (Not pivotGrid.Fields.Select(Function(x) x.Name).Contains(fieldName & i.ToString())))
            Return fieldName + uniqueId.ToString()
        End Function
        Private Sub beExpression_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim newBonus As PivotGridField = GetNewInvisibleBonusField()
            pivotGrid.Fields.Add(newBonus)
            pivotGrid.ShowUnboundExpressionEditor(newBonus)
            beExpression.Text = newBonus.UnboundExpression
            pivotGrid.Fields.Remove(newBonus)
        End Sub
        Private Sub btnAddField_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim newBonus As PivotGridField = GetNewInvisibleBonusField()
            newBonus.Visible = True
            pivotGrid.Fields.Add(newBonus)
            teBonusName.Text = GetUniqueFieldName()
            beExpression.Text = String.Empty
        End Sub
        Private Sub teBonusName_EditValueChanging(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangingEventArgs)
            btnAddField.IsEnabled = Not String.IsNullOrEmpty(TryCast(e.NewValue, String))
        End Sub
        Private Sub pivotGrid_FieldUnboundExpressionChanged(ByVal sender As Object, ByVal e As PivotFieldEventArgs)
            If e.Field IsNot Nothing AndAlso (Not e.Field.Visible) AndAlso btnAddField.IsEnabled Then
                beExpression.Text = e.Field.UnboundExpression
            End If
        End Sub
        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim fieldValue As FieldValueElementData = TryCast(DirectCast(sender, Control).DataContext, FieldValueElementData)
            If fieldValue IsNot Nothing AndAlso fieldValue.Field IsNot Nothing Then
                pivotGrid.ShowUnboundExpressionEditor(fieldValue.Field)
            End If
        End Sub
        Private Sub removeBonus_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            Dim fieldValue As FieldValueElementData = TryCast(DirectCast(sender, BarButtonItem).Tag, FieldValueElementData)
            If fieldValue IsNot Nothing AndAlso fieldValue.Field IsNot Nothing Then
                pivotGrid.Fields.Remove(fieldValue.Field)
            End If
        End Sub
    End Class
End Namespace
