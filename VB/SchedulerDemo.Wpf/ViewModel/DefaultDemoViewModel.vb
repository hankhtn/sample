Imports System
Imports System.Xml
Imports System.Windows
Imports System.Windows.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.Xpf.Scheduler
Imports System.Collections
Imports SchedulerDemo.Internal
Imports DevExpress.Mvvm
Imports DevExpress.XtraScheduler.Internal

Namespace SchedulerDemo
    Public Class DefaultDemoViewModel
        Inherits BindableBase

        Private appointmentListCore As IList
        Private resourceListCore As IList
        Private appointmentMappingsDictionaryCore As New Dictionary(Of String, String)()
        Private resourceMappingsDictionaryCore As New Dictionary(Of String, String)()
        Private startTimeCore As Date = Date.MinValue
        Private showBorderCore As Boolean = False

        Public Sub New()
            Me.appointmentListCore = CreateAppointmentList()
            Me.resourceListCore = CreateResourceList()

            Me.appointmentMappingsDictionaryCore = CreateAppointmentMappings()
            Me.resourceMappingsDictionaryCore = CreaterRsourceMappings()

            StartTime = New Date(2016, 07, 12)
        End Sub

        Public ReadOnly Property AppointmentsList() As IList
            Get
                Return appointmentListCore
            End Get
        End Property
        Public ReadOnly Property AppointmentMappingsDictionary() As Dictionary(Of String, String)
            Get
                Return appointmentMappingsDictionaryCore
            End Get
        End Property
        Public ReadOnly Property ResourcesList() As IList
            Get
                Return resourceListCore
            End Get
        End Property
        Public ReadOnly Property ResourceMappingsDictionary() As Dictionary(Of String, String)
            Get
                Return resourceMappingsDictionaryCore
            End Get
        End Property
        Public Property StartTime() As Date
            Get
                Return startTimeCore
            End Get
            Set(ByVal value As Date)
                SetProperty(startTimeCore, value, "StartTime")
            End Set
        End Property
        Public Property ShowBorder() As Boolean
            Get
                Return showBorderCore
            End Get
            Set(ByVal value As Boolean)
                SetProperty(showBorderCore, value, "ShowBorder")
            End Set
        End Property
        Protected Overridable Function CreateAppointmentList() As IList
            Dim helper As New EventItemXmlPersistenceHelper()
            Dim doc As XmlDocument = SchedulerDataHelper.LoadXmlDocumentFromResource(SchedulerDataHelper.DefaultAppointmentXmlSource)
            Return TryCast(helper.FromXmlNode(doc), IList)
        End Function
        Protected Overridable Function CreateResourceList() As IList
            Dim helper As New CarItemXmlPersistenceHelper()
            Dim doc As XmlDocument = SchedulerDataHelper.LoadXmlDocumentFromResource(SchedulerDataHelper.DefaultResourceXmlSource)
            Return TryCast(helper.FromXmlNode(doc), IList)
        End Function
        Protected Overridable Function CreaterRsourceMappings() As Dictionary(Of String, String)
            Dim result As New Dictionary(Of String, String)()
            result.Add(ResourceSR.Caption, "Caption")
            result.Add(ResourceSR.Color, "Color")
            result.Add(ResourceSR.Id, "Id")
            result.Add(ResourceSR.Image, "Image")
            Return result
        End Function
        Protected Overridable Function CreateAppointmentMappings() As Dictionary(Of String, String)
            Dim result As New Dictionary(Of String, String)()
            result.Add(AppointmentSR.AllDay, "AllDay")
            result.Add(AppointmentSR.Description, "Description")
            result.Add(AppointmentSR.End, "EndTime")
            result.Add(AppointmentSR.Label, "Label")
            result.Add(AppointmentSR.Location, "Location")
            result.Add(AppointmentSR.RecurrenceInfo, "RecurrenceInfo")
            result.Add(AppointmentSR.ReminderInfo, "ReminderInfo")
            result.Add(AppointmentSR.ResourceId, "ResourceId")
            result.Add(AppointmentSR.Start, "StartTime")
            result.Add(AppointmentSR.Status, "Status")
            result.Add(AppointmentSR.Subject, "Subject")
            result.Add(AppointmentSR.Type, "Type")
            Return result
        End Function
    End Class

    Public Class AppointmentDictionaryToMappingConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim appointmentMappings As New AppointmentMapping()
            Dim mappings As Dictionary(Of String, String) = TryCast(value, Dictionary(Of String, String))
            If mappings IsNot Nothing Then
                If mappings.ContainsKey("AllDay") Then
                    appointmentMappings.AllDay = mappings("AllDay")
                End If
                If mappings.ContainsKey("Description") Then
                    appointmentMappings.Description = mappings("Description")
                End If
                If mappings.ContainsKey("End") Then
                    appointmentMappings.End = mappings("End")
                End If
                If mappings.ContainsKey("Label") Then
                    appointmentMappings.Label = mappings("Label")
                End If
                If mappings.ContainsKey("Location") Then
                    appointmentMappings.Location = mappings("Location")
                End If
                If mappings.ContainsKey("RecurrenceInfo") Then
                    appointmentMappings.RecurrenceInfo = mappings("RecurrenceInfo")
                End If
                If mappings.ContainsKey("ReminderInfo") Then
                    appointmentMappings.ReminderInfo = mappings("ReminderInfo")
                End If
                If mappings.ContainsKey("ResourceId") Then
                    appointmentMappings.ResourceId = mappings("ResourceId")
                End If
                If mappings.ContainsKey("Start") Then
                    appointmentMappings.Start = mappings("Start")
                End If
                If mappings.ContainsKey("Status") Then
                    appointmentMappings.Status = mappings("Status")
                End If
                If mappings.ContainsKey("Subject") Then
                    appointmentMappings.Subject = mappings("Subject")
                End If
                If mappings.ContainsKey("Type") Then
                    appointmentMappings.Type = mappings("Type")
                End If
            End If
            Return appointmentMappings
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
    Public Class ResourceDictionaryToMappingConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim resourceMappings As New ResourceMapping()
            Dim mappings As Dictionary(Of String, String) = TryCast(value, Dictionary(Of String, String))
            If mappings IsNot Nothing Then
                If mappings.ContainsKey("Caption") Then
                    resourceMappings.Caption = mappings("Caption")
                End If
                If mappings.ContainsKey("Color") Then
                    resourceMappings.Color = mappings("Color")
                End If
                If mappings.ContainsKey("Id") Then
                    resourceMappings.Id = mappings("Id")
                End If
                If mappings.ContainsKey("Image") Then
                    resourceMappings.Image = mappings("Image")
                End If
            End If
            Return resourceMappings
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        #End Region
    End Class
End Namespace
