Imports System.Collections.Generic
Imports DevExpress.Mvvm.POCO

Namespace SchedulingDemo.ViewModels
    Public Class AppointmentCustomizationDemoViewModel
        Private ReadOnly data As New CarsData()
        Protected Sub New()
            CustomFlyout = False
            CustomTemplate_AllDay = False
            CustomTemplate = CustomTemplate_AllDay
            ShowStatus_AllDay = True
            ShowStatus = ShowStatus_AllDay
            ShowLocation_AllDay = True
            ShowLocation = ShowLocation_AllDay
            ShowDescription_AllDay = True
            ShowDescription = ShowDescription_AllDay
            ShowInterval_AllDay = True
            ShowInterval = ShowInterval_AllDay
            IntervalStringFormat_AllDay = "{0:hh:mm tt} - {1:hh:mm tt}"
            IntervalStringFormat = IntervalStringFormat_AllDay
        End Sub
        Public Overridable ReadOnly Property Events() As List(Of CarScheduling)
            Get
                Return Me.data.Events
            End Get
        End Property
        Public Overridable ReadOnly Property Cars() As List(Of Car)
            Get
                Return Me.data.Cars
            End Get
        End Property

        Public Overridable Property CustomFlyout() As Boolean

        Public Overridable Property CustomTemplate() As Boolean
        Public Overridable Property ShowStatus() As Boolean
        Public Overridable Property ShowLocation() As Boolean
        Public Overridable Property ShowDescription() As Boolean
        Public Overridable Property ShowInterval() As Boolean
        Public Overridable Property IntervalStringFormat() As String

        Public Overridable Property CustomTemplate_AllDay() As Boolean
        Public Overridable Property ShowStatus_AllDay() As Boolean
        Public Overridable Property ShowLocation_AllDay() As Boolean
        Public Overridable Property ShowDescription_AllDay() As Boolean
        Public Overridable Property ShowInterval_AllDay() As Boolean
        Public Overridable Property IntervalStringFormat_AllDay() As String
    End Class
End Namespace
