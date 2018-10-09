Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Partial Public Class OLAPBrowser
        Inherits PivotGridDemoModule

        Private Const YearFieldName As String = "[Date].[Calendar].[Calendar Year]"
        Private Const CategoryFieldName As String = "[Product].[Product].[Product]"
        Private Const TotalCostFieldName As String = "[Measures].[Total Product Cost]"
        Private Const FreightFieldName As String = "[Measures].[Freight Cost]"
        Private Const QuantityOrderFieldName As String = "[Measures].[Order Quantity]"
        Protected Const DefaultFieldWidth As Integer = 90

        Private Shared Function GetSampleConnectionString() As String
            Return "Provider=msolap;Data Source=" & GetSampleFileName() & ";Initial Catalog=Adventure Works;Cube Name=Adventure Works"
        End Function
        Private Shared Function GetSampleFileName() As String
            Dim sampleFileName As String = Path.GetFullPath(DataFilesHelper.FindFile("AdventureWorks.cub", DataFilesHelper.DataPath))
            If File.Exists(sampleFileName) Then
                Try
                    File.SetAttributes(sampleFileName, FileAttributes.Normal)
                Catch
                End Try
            End If
            Return sampleFileName
        End Function

        Private Function PivotConnectionString() As String
            Return pivotGrid.OlapConnectionString
        End Function
        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub PivotGridDemoModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            InitPivotGrid(GetSampleConnectionString())
        End Sub
        Private Function IsSampleCube() As Boolean
            Return pivotGrid.OlapConnectionString.Contains("Cube Name=Adventure Works")
        End Function

        Private Sub InitPivotLayoutSampleOlapDB(ByVal result As AsyncOperationResult)
            If pivotGrid.Fields.Count = 0 OrElse (Not IsSampleCube()) Then
                Return
            End If
            pivotGrid.BeginUpdate()
            Dim fieldProduct As PivotGridField = pivotGrid.Fields(CategoryFieldName), fieldYear As PivotGridField = pivotGrid.Fields(YearFieldName), fieldTotalCost As PivotGridField = pivotGrid.Fields(TotalCostFieldName), fieldFreightCost As PivotGridField = pivotGrid.Fields(FreightFieldName), fieldOrderQuantity As PivotGridField = pivotGrid.Fields(QuantityOrderFieldName)
            If fieldProduct Is Nothing OrElse fieldYear Is Nothing OrElse fieldTotalCost Is Nothing OrElse fieldFreightCost Is Nothing OrElse fieldOrderQuantity Is Nothing Then
                pivotGrid.EndUpdateAsync()
                Return
            End If
            fieldProduct.Area = FieldArea.RowArea
            fieldYear.Area = FieldArea.ColumnArea
            fieldYear.SortOrder = FieldSortOrder.Descending
            fieldTotalCost.Width = DefaultFieldWidth + 20
            fieldTotalCost.CellFormat = "c2"
            fieldFreightCost.Width = DefaultFieldWidth
            fieldFreightCost.CellFormat = "c2"
            fieldOrderQuantity.Width = DefaultFieldWidth + 5
            fieldProduct.Visible = True
            fieldYear.Visible = True
            fieldTotalCost.Visible = True
            fieldFreightCost.Visible = True
            fieldOrderQuantity.Visible = True
            pivotGrid.EndUpdateAsync()
        End Sub

        Private Sub InitPivotGrid(ByVal connectionString As String)
            If String.IsNullOrWhiteSpace(connectionString) Then
                pivotGrid.DataSource = Nothing
                Return
            End If
            If PivotConnectionString() = connectionString Then
                Return
            End If
            pivotGrid.BeginUpdate()
            pivotGrid.Fields.Clear()
            pivotGrid.Groups.Clear()
            pivotGrid.OlapConnectionString = connectionString
            pivotGrid.EndUpdateAsync(Sub(result As AsyncOperationResult)
                If pivotGrid.Fields.Count = 0 Then
                    pivotGrid.RetrieveFieldsAsync(FieldArea.FilterArea, False, AddressOf InitPivotLayoutSampleOlapDB)
                End If
            End Sub)
        End Sub

        Private dialog As DataSourceDialog
        Private Sub OnShowConnectionClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If pivotGrid Is Nothing OrElse pivotGrid.IsAsyncInProgress Then
                Return
            End If
            dialog = New DataSourceDialog()
            Dim pars As New FloatingContainerParameters()
            pars.AllowSizing = False
            pars.CloseOnEscape = True
            pars.Title = "OLAP Connection"
            pars.ClosedDelegate = AddressOf DialogClosed
            FloatingContainer.ShowDialogContent(dialog, Me, New Size(600, 230), pars)
        End Sub

        Private Sub DialogClosed(ByVal close? As Boolean)
            Application.Current.MainWindow.Activate()
            If dialog Is Nothing Then
                Return
            End If
            Dim connectionString As String = dialog.GetConnectionString()
            dialog = Nothing
            If Not close.Equals(True) Then
                Return
            End If
            If String.IsNullOrWhiteSpace(connectionString) Then
                Return
            End If
            InitPivotGrid(connectionString)

        End Sub
    End Class
End Namespace
