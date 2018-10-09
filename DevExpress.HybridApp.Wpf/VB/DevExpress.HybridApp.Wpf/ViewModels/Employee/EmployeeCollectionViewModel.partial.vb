Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class EmployeeCollectionViewModel
        Implements ISupportFiltering(Of Employee), IFilterTreeViewModelContainer(Of Employee, Long)

        Private ReadOnly Property EditNoteDocumentManagerService() As IDocumentManagerService
            Get
                Return Me.GetRequiredService(Of IDocumentManagerService)("EditNoteDocumentManagerService")
            End Get
        End Property

        Public Sub ShowMailMerge()

            Dim mailMergeViewModel_Renamed = MailMergeViewModel(Of Employee, Object).Create(UnitOfWorkFactory, Function(x) x.Employees,If(SelectedEntity Is Nothing, CType(Nothing, Long?), SelectedEntity.Id))
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel_Renamed, Nothing, Me).Show()
        End Sub
        Public Sub ShowPrint(ByVal employeeReportType As EmployeeReportType)
            DocumentManagerService.CreateDocument("ReportPreview", ReportPreviewViewModel.Create(GetReport(employeeReportType)), Nothing, Me).Show()
        End Sub
        Public Function CanShowPrint(ByVal employeeReportType As EmployeeReportType) As Boolean
            Return employeeReportType <> DevExpress.DevAV.ViewModels.EmployeeReportType.Profile OrElse SelectedEntity IsNot Nothing
        End Function
        Private Function GetReport(ByVal reportType As EmployeeReportType) As IReportInfo
            Xpf.DemoBase.Helpers.Logger.Log(String.Format("HybridApp: Create Report : Employees: {0}", reportType.ToString()))
            Select Case reportType
                Case EmployeeReportType.TaskList
                    Return ReportInfoFactory.EmployeeTaskList(UnitOfWorkFactory.CreateUnitOfWork().Tasks.ToList())
                Case EmployeeReportType.Profile
                    Return ReportInfoFactory.EmployeeProfile(SelectedEntity)
                Case EmployeeReportType.Summary
                    Return ReportInfoFactory.EmployeeSummary(Entities)
                Case EmployeeReportType.Directory
                    Return ReportInfoFactory.EmployeeDirectory(Entities)
            End Select
            Throw New ArgumentException("", "reportType")
        End Function
        Protected Overrides Sub OnEntitiesAssigned(ByVal getSelectedEntityCallback As Func(Of Employee))
            MyBase.OnEntitiesAssigned(getSelectedEntityCallback)
            If SelectedEntity Is Nothing Then
                SelectedEntity = Entities.FirstOrDefault()
            End If
        End Sub
        Public Sub AddTask()
            Dim initializer As Action(Of EmployeeTask) = Sub(x)
                x.AssignedEmployeeId = SelectedEntity.Id
                x.OwnerId = SelectedEntity.Id
            End Sub
            EditNoteDocumentManagerService.CreateDocument("EmployeeTaskView", Nothing, initializer, Me).Show()
        End Sub
        Public Function CanAddTask() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub AddNote()
            Dim initializer As Action(Of Evaluation)
            If SelectedEntity Is Nothing Then
                initializer = Nothing
             Else
                initializer = Sub(x) x.EmployeeId = SelectedEntity.Id
             End If
            EditNoteDocumentManagerService.CreateDocument("EvaluationView", Nothing, initializer, Me).Show()
        End Sub
        Public Function CanAddNote() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Overridable Property FilterTreeViewModel() As FilterTreeViewModel(Of Employee, Long) Implements IFilterTreeViewModelContainer(Of Employee, Long).FilterTreeViewModel
        #Region "ISupportFiltering"
        Private Property ISupportFilteringGeneric_FilterExpression() As Expression(Of Func(Of Employee, Boolean)) Implements ISupportFiltering(Of Employee).FilterExpression
            Get
                Return FilterExpression
            End Get
            Set(ByVal value As Expression(Of Func(Of Employee, Boolean)))
                FilterExpression = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
