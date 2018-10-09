Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid.Printing
Imports DevExpress.Xpf.Reports.UserDesigner.Extensions
Imports DevExpress.XtraExport.Helpers
Imports System
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Windows
Imports System.Windows.Controls

Namespace GridDemo
    Partial Public Class RecentReports
        Inherits UserControl

        Public Shared ReadOnly ReportServiceProperty As DependencyProperty = DependencyProperty.Register("ReportService", GetType(GridReportManagerService), GetType(RecentReports), New PropertyMetadata(Nothing))
        Public Property ReportService() As GridReportManagerService
            Get
                Return DirectCast(GetValue(ReportServiceProperty), GridReportManagerService)
            End Get
            Set(ByVal value As GridReportManagerService)
                SetValue(ReportServiceProperty, value)
            End Set
        End Property
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
