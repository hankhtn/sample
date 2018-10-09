Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Editors.DateNavigator
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler.iCalendar

Namespace ProductsDemo.Modules



    Partial Public Class SchedulerModule
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class SchedulerExchangeCreatorConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim scheduler As SchedulerControl = TryCast(value, SchedulerControl)
            If scheduler Is Nothing Then
                Return Nothing
            End If
            Return New SchedulerExchangeCreator(scheduler)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class SchedulerExchangeCreator
        Implements ISchedulerExchangeCreator

        Private control As SchedulerControl
        Public Sub New(ByVal control As SchedulerControl)
            Me.control = control
        End Sub
        Public Function CreateImporter() As iCalendarImporter Implements ISchedulerExchangeCreator.CreateImporter
            Return New iCalendarImporter(Me.control.Storage.InnerStorage)
        End Function

        Public Function CreateExporter() As iCalendarExporter Implements ISchedulerExchangeCreator.CreateExporter
            Return New iCalendarExporter(Me.control.Storage.InnerStorage)
        End Function
    End Class
End Namespace
