Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.Editors
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors.Helpers

Imports DevExpress.Xpf.PivotGrid
Imports PivotGridDemo
Imports DevExpress.Xpf.PivotGrid.Internal
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Core

Namespace PivotGridDemo.Tests
    Public Class PivotGridCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
            Return moduleType IsNot GetType(ServerMode)
        End Function


        Protected Overrides Sub CreateCheckOptionsAction()
            AddCheck0()
            CreateCheck1()
        End Sub

        Private Sub CreateCheck1()
            If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ChartsIntegration) Then
                Dispatch(AddressOf HidePrefilterAction)
            End If
            Dispatch(AddressOf AssertHeadersFilterButtonVisibilityB183058)
            Dispatch(AddressOf AssertTextEditsWidthB185051)
            Dispatch(AddressOf TextBlockTextTrimmingB185312)
            If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(PrintTemplates) Then
                Dim pivotGrid As PivotGridControl = DispatchExpr(Function() FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule))
                Dim cont As FloatingContainer = DispatchExpr(Function() PrintHelper.ShowPrintPreview(pivotGrid, pivotGrid))
                DispatchBusyWait(Function() cont.IsOpen)
                System.Threading.Thread.Sleep(2000)
                Dispatch(Sub() cont.Close())
            End If
        End Sub

        Private Sub AddCheck0()
            Dispatch(AddressOf ComboBoxItemsEditableB181973)
            Dispatch(Sub()
                If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ChartsIntegration) Then
                    ShowPrefilterAction()
                End If
            End Sub)
            Dispatch(Sub()
                If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ChartsIntegration) Then
                    BestFitAction()
                End If
            End Sub)
        End Sub

        Private Sub TextBlockTextTrimmingB185312()
            Dim control As FrameworkElement = DemoBaseTesting.CurrentDemoModule.Options
            If control Is Nothing Then
                Return
            End If
            Dim editors As List(Of TextBlock) = FindAllElements(Of TextBlock)(control)
            For Each edit As TextBlock In editors
                Dim trimmed As Boolean = TextBlockService.CalcIsTextTrimmed(edit) AndAlso edit.DesiredSize.Width > (edit.ActualWidth + edit.Margin.Left + edit.Margin.Right)
                AssertLog.IsFalse(trimmed, "Text is trimmed: " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " text=" & edit.Text & ", Name=" & edit.Name & ", actual " & edit.ActualWidth & "px, desired:" & edit.DesiredSize.Width & "px")
            Next edit
        End Sub

        Private Sub AssertTextEditsWidthB185051()
            Dim control As FrameworkElement = DemoBaseTesting.CurrentDemoModule.Options
            If control Is Nothing Then
                Return
            End If
            Dim editors As List(Of TextEdit) = FindAllElements(Of TextEdit)(control)
            For Each edit As TextEdit In editors
                If edit.EditMode = EditMode.InplaceInactive OrElse edit.IsPrintingMode = True Then
                    Continue For
                End If
                AssertLog.IsFalse(edit.Width = Double.PositiveInfinity, "TextEdit unlimited width: " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " text=" & edit.Text & ", Name=" & edit.Name & " desired " & edit.ActualWidth & "px")
            Next edit
        End Sub

        Private Sub AssertHeadersFilterButtonVisibilityB183058()
            Dim pivotGrid As PivotGridControl = FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule)
            Dim headers As List(Of FieldHeader) = FindAllElements(Of FieldHeader)(pivotGrid)
            For Each header As FieldHeader In headers
                Dim field As PivotGridField = header.Field
                Dim fieldFiltered As Boolean = GetIsFiltered(field)
                Dim filterVisibility As Visibility = If(fieldFiltered OrElse header.IsMouseOver, Visibility.Visible, Visibility.Hidden)
                If Not field.GetInternalField().ShowFilterButton Then
                    filterVisibility = Visibility.Collapsed
                End If
                If filterVisibility <> Visibility.Collapsed Then
                    AssertLog.IsTrue(fieldFiltered = field.IsFiltered, "field.IsFiltered " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " " & field.Name & " expected" & ControlChars.Cr & "eal:" & fieldFiltered.ToString() & " \ " & field.IsFiltered.ToString())
                End If
                AssertLog.IsTrue(filterVisibility = header.IsFilterButtonVisible, "header.IsFiltered " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " " & field.Name & " expected" & ControlChars.Cr & "eal:" & filterVisibility.ToString() & " \ " & header.IsFilterButtonVisible.ToString())
            Next header
        End Sub

        Private Function GetIsFiltered(ByVal field As PivotGridField) As Boolean
            Dim internalField As PivotGridInternalField = field.GetInternalField()
            Return ((internalField.Group Is Nothing OrElse internalField.GroupFilterMode.Equals(DevExpress.XtraPivotGrid.PivotGroupFilterMode.List)) AndAlso (Not internalField.FilterValues.IsEmpty)) OrElse field.Group IsNot Nothing AndAlso internalField.GroupFilterMode.Equals(DevExpress.XtraPivotGrid.PivotGroupFilterMode.Tree) AndAlso Not field.Group.FilterValues.IsEmpty
        End Function

        Private Sub ComboBoxItemsEditableB181973()
            Dim control As FrameworkElement = DemoBaseTesting.CurrentDemoModule.Options
            If control Is Nothing Then
                Return
            End If
            Dim editors As List(Of ComboBoxEdit) = FindAllElements(Of ComboBoxEdit)(control)
            For i As Integer = 0 To editors.Count - 1
                AssertLog.IsFalse(CBool(editors(i).IsTextEditable), "ComboBoxEdit items editable: " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " " & editors(i).Name)
                AssertLog.IsFalse(editors(i).SelectedIndex < 0, "ComboBoxEdit item is not selected: " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " " & editors(i).Name)
            Next i
        End Sub

        Private Function ChartGeneralOptionsCondition() As Boolean
            Return DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ChartsIntegration)
        End Function

        Private Sub BestFitAction()
            Dim pivotGrid As PivotGridControl = FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule)
            pivotGrid.BestFit()
        End Sub
        Private Sub HidePrefilterAction()
            UpdateLayoutAndDoEvents()
            Dim pivotGrid As PivotGridControl = FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule)
            If pivotGrid IsNot Nothing AndAlso pivotGrid.PrefilterContainer IsNot Nothing Then
                pivotGrid.PrefilterContainer.Close()
            End If
            UpdateLayoutAndDoEvents()
        End Sub

        Private Sub ShowPrefilterAction()
            UpdateLayoutAndDoEvents()
            Dim pivotGrid As PivotGridControl = FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule)
            If pivotGrid Is Nothing Then
                Return
            End If
            pivotGrid.ShowPrefilter()
            UpdateLayoutAndDoEvents()
        End Sub

        Private Function FindAllElements(Of T As FrameworkElement)(ByVal element As FrameworkElement) As List(Of T)
            Dim items As New List(Of T)()
            LayoutHelper.ForEachElement(element, Function(d) addItem(Of T)(items, d))
            Return items
        End Function
        Private Function addItem(Of T As FrameworkElement)(ByVal items As List(Of T), ByVal d As FrameworkElement) As Boolean
            If d.GetType() Is GetType(T) Then
                items.Add(CType(d, T))
            End If
            Return True
        End Function

        Private Function FindElement(Of T As FrameworkElement)(ByVal element As FrameworkElement) As T
            Return CType(LayoutHelper.FindElement(element, Function(d) d.GetType() Is GetType(T)), T)
        End Function
    End Class
End Namespace
