Imports System
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo
    Partial Public Class PrintTemplates
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
            ResetLayout()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGrid.ShowPrintPreview(Window.GetWindow(Me))
        End Sub

        Private Sub pivotGrid_CustomGroupInterval(ByVal sender As Object, ByVal e As PivotCustomGroupIntervalEventArgs)
            e.GroupValue = MoonPhaseCalculator.CalculateMoonPhase(CDate(e.Value))
        End Sub

        Private Sub ResetLayout()

            fieldCategory.Area = FieldArea.RowArea
            fieldMoonPhase.Area = FieldArea.ColumnArea
            fieldMoonPhase.Visible = False
            fieldMoonPhase.FilterValues.Clear()
            fieldYear.Area = FieldArea.ColumnArea
            fieldYear.Visible = True
            fieldYear.AreaIndex = 0
            fieldYear.FilterValues.Clear()
            fieldQuarter.Area = FieldArea.ColumnArea
            fieldQuarter.Visible = True
            fieldQuarter.AreaIndex = 1
            fieldQuarter.FilterValues.Clear()
            fieldSales.Area = FieldArea.DataArea
            fieldSales.Visible = True
            fieldSales.FilterValues.Clear()
        End Sub

        Private Sub templatesList_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not IsInitialized Then
                Return
            End If
            ResetLayout()
            If templatesList.SelectedIndex = 1 Then
                fieldYear.Visible = False
                fieldQuarter.Visible = False
                fieldMoonPhase.Visible = True
            End If
        End Sub
    End Class
End Namespace
