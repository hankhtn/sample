Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraReports.UI

Namespace DevExpress.DevAV.ViewModels
    Public Class ReportDesignerViewModel
        Public Shared Function Create(ByVal report As XtraReport) As ReportDesignerViewModel
            Return ViewModelSource.Create(Function() New ReportDesignerViewModel(report))
        End Function
        Protected Sub New(ByVal report As XtraReport)
            Me.Report = report
        End Sub

        Private privateReport As XtraReport
        Public Property Report() As XtraReport
            Get
                Return privateReport
            End Get
            Private Set(ByVal value As XtraReport)
                privateReport = value
            End Set
        End Property
        Private privateIsReportSaved As Boolean
        Public Property IsReportSaved() As Boolean
            Get
                Return privateIsReportSaved
            End Get
            Private Set(ByVal value As Boolean)
                privateIsReportSaved = value
            End Set
        End Property

        Public Overridable Sub Save()
            IsReportSaved = True
        End Sub
    End Class
End Namespace
