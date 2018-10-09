Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Reports.UserDesigner.Extensions

Namespace GridDemo
    Partial Public Class GridDemoRibbon
        Inherits UserControl

       Public Shared ReadOnly ExportGridProperty As DependencyProperty = DependencyProperty.Register("ExportGrid", GetType(GridControl), GetType(GridDemoRibbon), New PropertyMetadata(Nothing))
        Public Property ExportGrid() As GridControl
            Get
                Return DirectCast(GetValue(ExportGridProperty), GridControl)
            End Get
            Set(ByVal value As GridControl)
                SetValue(ExportGridProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ReportServiceProperty As DependencyProperty = DependencyProperty.Register("ReportService", GetType(GridReportManagerService), GetType(GridDemoRibbon), New PropertyMetadata(Nothing))
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
