Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Text.RegularExpressions
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map
Imports DevExpress.Mvvm.ViewModel

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class OrderCollectionViewModel
        Implements ISupportFiltering(Of Order), IFilterTreeViewModelContainer(Of Order, Long)

        Private Const employeeFilterCriteriaHeader As String = "[EmployeeId]"

        Private selectedEntityId? As Long
        Private orderItemsUnitOfWork As IDevAVDbUnitOfWork


        Private viewSettings_Renamed As ViewSettingsViewModel

        Public ReadOnly Property ViewSettings() As ViewSettingsViewModel
            Get
                If viewSettings_Renamed Is Nothing Then
                    viewSettings_Renamed = ViewSettingsViewModel.Create(CollectionViewKind.MasterDetailView, Me)
                End If
                Return viewSettings_Renamed
            End Get
        End Property
        Public ReadOnly Property DefaultFilter() As String
            Get
                Return "IsOutlookIntervalYesterday([OrderDate]) OR IsOutlookIntervalToday([OrderDate])"
            End Get
        End Property

        Public Overrides Sub OnLoaded()
            MyBase.OnLoaded()
            If FilterTreeViewModel Is Nothing Then
                Return
            End If
            DeleteEmployeeFilters()
            CreateEmployeeFilters()
        End Sub
        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            SetDefaultReport(ReportInfoFactory.SalesInvoice(SelectedEntity))
        End Sub
        Protected Overrides Sub OnEntitiesAssigned(ByVal getSelectedEntityCallback As Func(Of Order))
            RestoreSelection()
        End Sub
        Public Sub PrintInvoice()
            ShowReport(ReportInfoFactory.SalesInvoice(SelectedEntity), "Invoice")
        End Sub
        Public Function CanPrintInvoice() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintSummaryReport()
            ShowReport(ReportInfoFactory.SalesOrdersSummaryReport(QueriesHelper.GetSaleSummaries(CreateUnitOfWork().OrderItems)), "OrdersSummary")
        End Sub
        Public Sub PrintSalesAnalysisReport()
            ShowReport(ReportInfoFactory.SalesAnalysisReport(QueriesHelper.GetSaleAnalysis(CreateUnitOfWork().OrderItems)), "Analysis")
        End Sub
        Public Sub ShowRevenueReport()
            DocumentManagerService.CreateDocument("OrderRevenueReportView", OrderRevenueViewModel.Create(QueriesHelper.GetRevenueReportItems(CreateUnitOfWork().OrderItems), RevenueReportFormat.Summary), Nothing, Me).Show()
        End Sub
        Public Overridable Sub ShowRevenueAnalysisReport()
            DocumentManagerService.CreateDocument("OrderRevenueReportView", OrderRevenueViewModel.Create(QueriesHelper.GetRevenueAnalysisReportItems(CreateUnitOfWork().OrderItems, SelectedEntity.StoreId.Value), RevenueReportFormat.Analysis), Nothing, Me).Show()
        End Sub
        Private Sub ShowReport(ByVal reportInfo As IReportInfo, ByVal reportId As String)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
            Xpf.DemoBase.Helpers.Logger.Log(String.Format("OutlookInspiredApp: Create Report : Sales: {0}", reportId))
        End Sub
        Private Sub SetDefaultReport(ByVal reportInfo As IReportInfo)
            Me.GetRequiredService(Of IReportService)("DocumentViewerService").SetDefaultReport(reportInfo)
            If Me.IsInDesignMode() Then
                Return
            End If
            ExportService.SetDefaultReport(reportInfo)
            PrintService.SetDefaultReport(reportInfo)
        End Sub
        Private ReadOnly Property PrintService() As IReportService
            Get
                Return Me.GetRequiredService(Of IReportService)("PrintService")
            End Get
        End Property
        Private ReadOnly Property ExportService() As IReportService
            Get
                Return Me.GetRequiredService(Of IReportService)("ExportService")
            End Get
        End Property

        Public Sub ShowMap()
            Xpf.DemoBase.Helpers.Logger.Log("OutlookInspiredApp: View Orders Map")
            DocumentManagerService.CreateDocument("OrderMapView", OrderMapViewModel.Create(SelectedEntity), Nothing, Me).Show()
        End Sub
        Public Function CanShowMap() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub CreateCustomFilter()
            Messenger.Default.Send(New CreateCustomFilterMessage(Of Order)())
        End Sub
        Public Sub Paid()
            SelectedEntity.PaymentTotal = SelectedEntity.TotalAmount
            Me.Save(SelectedEntity)
        End Sub
        Public Overrides Sub [New]()
            Dim document = DocumentManagerService.ShowNewEntityDocument(Of Order)(Me, Sub(newOrder) InitializeNewOrder(newOrder))
            Dim newEntity = CType(document.Content, OrderViewModel).Entity
            If newEntity IsNot Nothing Then
                selectedEntityId = newEntity.Id
                Refresh()
            End If
        End Sub
        Private Sub InitializeNewOrder(ByVal order As Order)
            Dim currentUnitOfWork = UnitOfWorkFactory.CreateUnitOfWork()
            Dim getInvoiceNumber As Func(Of Order, Integer) = Function(x)
                Dim number As Integer = 0
                Integer.TryParse(x.InvoiceNumber, number)
                Return number
            End Function
            Dim maxInvoiceNumber = currentUnitOfWork.Orders.Max(getInvoiceNumber)
            order.InvoiceNumber = (maxInvoiceNumber + 1).ToString()
            order.OrderDate = Date.Now

            Dim customer = currentUnitOfWork.Customers.First()
            order.CustomerId = customer.Id
            order.StoreId = customer.CustomerStores.First().Id
            order.EmployeeId = currentUnitOfWork.Employees.First().Id
        End Sub
        Protected Overrides Sub OnBeforeEntityDeleted(ByVal primaryKey As Long, ByVal entity As Order)
            MyBase.OnBeforeEntityDeleted(primaryKey, entity)
            If Not entity.OrderItems.Any() Then
                Return
            End If
            Dim deletedItemKeys = entity.OrderItems.Select(Function(x) x.Id).ToList()
            orderItemsUnitOfWork = Me.UnitOfWorkFactory.CreateUnitOfWork()
            deletedItemKeys.ForEach(Sub(x)
                Dim item = orderItemsUnitOfWork.OrderItems.Find(x)
                orderItemsUnitOfWork.OrderItems.Remove(item)
            End Sub)
        End Sub
        Protected Overrides Sub OnEntityDeleted(ByVal primaryKey As Long, ByVal entity As Order)
            MyBase.OnEntityDeleted(primaryKey, entity)
            orderItemsUnitOfWork.SaveChanges()
        End Sub
        Public Overrides Sub Edit(ByVal projectionEntity As Order)
            selectedEntityId = SelectedEntity.Id
            MyBase.Edit(projectionEntity)
            RefreshIfNeeded()
        End Sub
        Public Function CanPaid() As Boolean
            Return SelectedEntity IsNot Nothing AndAlso SelectedEntity.PaymentTotal < SelectedEntity.TotalAmount
        End Function
        Public Sub Refund()
            SelectedEntity.RefundTotal = SelectedEntity.PaymentTotal
            Me.Save(SelectedEntity)
        End Sub
        Public Function CanRefund() As Boolean
            Return SelectedEntity IsNot Nothing AndAlso SelectedEntity.RefundTotal < SelectedEntity.PaymentTotal
        End Function
        Public Sub MailTo()
            ExecuteMailTo(MessageBoxService, SelectedEntity.Employee.Email)
        End Sub
        Protected Overrides Sub OnEntitySaved(ByVal primaryKey As Long, ByVal entity As Order)
            MyBase.OnEntitySaved(primaryKey, entity)
            Me.UpdateSelectedEntity()
        End Sub
        Public Sub QuickLetter(ByVal templateName As String)

            Dim mailMergeViewModel_Renamed = MailMergeViewModel(Of Order, LinksViewModel).Create(UnitOfWorkFactory, Function(x) x.Orders, GetSelectedEntityKey(), templateName, LinksViewModel.Create())
            DocumentManagerService.CreateDocument("OrderMailMergeView", mailMergeViewModel_Renamed, Nothing, Me).Show()
        End Sub
        Public Function CanQuickLetter(ByVal templateName As String) As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub QuickReport(ByVal format As ReportFormat)
            Dim quickReportsViewModel = OrderQuickReportsViewModel.Create(SelectedEntity, format)
            Xpf.DemoBase.Helpers.Logger.Log(String.Format("OutlookInspiredApp: View Order Quick Report: {0}", format.ToString()))
            DocumentManagerService.CreateDocument(String.Format("Order{0}QuickReportView", format.ToString()), quickReportsViewModel, Nothing, Me).Show()
        End Sub
        Public Function CanQuickReport(ByVal format As ReportFormat) As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Private Function GetSelectedEntityKey() As Long?
            Return If(SelectedEntity Is Nothing, DirectCast(Nothing, Long?), SelectedEntity.Id)
        End Function

        Private Sub CreateEmployeeFilters()
            UnitOfWorkFactory.CreateUnitOfWork().Orders.Where(Function(x) x.EmployeeId IsNot Nothing AndAlso x.Employee IsNot Nothing AndAlso (Not String.IsNullOrEmpty(x.Employee.FullName))).Select(Function(x) New FilterInfo() With {.FilterCriteria = employeeFilterCriteriaHeader & "=" & x.Employee.Id, .Name = "Sales by " & x.Employee.FullName}).Distinct().ToList().ForEach(Sub(x) AddEmployeeFilter(x))
        End Sub
        Private Sub DeleteEmployeeFilters()
            FilterTreeViewModel.CustomFilters.Where(Function(x) IsEmployeeFilter(x)).ToList().ForEach(Sub(x) FilterTreeViewModel.DeleteCustomFilter(x))
        End Sub

        Private Sub AddEmployeeFilter(ByVal filterInfo As FilterInfo)
            FilterTreeViewModel.AddCustomFilter(filterInfo.Name, Data.Filtering.CriteriaOperator.Parse(filterInfo.FilterCriteria))
        End Sub


        Private Function IsEmployeeFilter(ByVal filterItem_Renamed As FilterItem) As Boolean
            Return filterItem_Renamed.FilterCriteria.LegacyToString().Contains(employeeFilterCriteriaHeader)
        End Function
        Public Shared Sub ExecuteMailTo(ByVal messageBoxService As IMessageBoxService, ByVal email As String)
            Try
                System.Diagnostics.Process.Start("mailto://" & email)
            Catch
                If messageBoxService IsNot Nothing Then
                    messageBoxService.Show("Mail To: " & email)
                End If
            End Try
        End Sub
        Private Sub RefreshIfNeeded()
            If FindEntity(selectedEntityId) IsNot Nothing Then
                Refresh()
            Else
                ClearSelectedEntityId()
            End If
        End Sub
        Private Sub RestoreSelection()
            Dim entity = FindEntity(selectedEntityId)
            If entity IsNot Nothing Then
                SelectedEntity = entity
            End If
            ClearSelectedEntityId()
        End Sub
        Private Function FindEntity(ByVal id? As Long) As Order
            Return If(id.HasValue, Me.Entities.SingleOrDefault(Function(x) x.Id = id.Value), Nothing)
        End Function

        Private Sub ClearSelectedEntityId()
            selectedEntityId = Nothing
        End Sub

        #Region "IFilterTreeViewModelContainer"
        Public Overridable Property FilterTreeViewModel() As FilterTreeViewModel(Of Order, Long) Implements IFilterTreeViewModelContainer(Of Order, Long).FilterTreeViewModel

        #End Region

        #Region "ISupportFiltering"
        Private Property ISupportFilteringGeneric_FilterExpression() As Expression(Of Func(Of Order, Boolean)) Implements ISupportFiltering(Of Order).FilterExpression
            Get
                Return FilterExpression
            End Get
            Set(ByVal value As Expression(Of Func(Of Order, Boolean)))
                FilterExpression = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
