Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace ProductsDemo.Modules
    Public Class Appointment
        Implements INotifyPropertyChanged


        Private eventType_Renamed? As Integer
        Public Property EventType() As Integer?
            Get
                Return eventType_Renamed
            End Get
            Set(ByVal value? As Integer)
                If eventType_Renamed = value Then
                    Return
                End If
                eventType_Renamed = value
                RaisePropertyChanged("EventType")
            End Set
        End Property


        Private startDate_Renamed? As Date
        Public Property StartDate() As Date?
            Get
                Return startDate_Renamed
            End Get
            Set(ByVal value? As Date)
                If startDate_Renamed = value Then
                    Return
                End If
                startDate_Renamed = value
                RaisePropertyChanged("StartDate")
            End Set
        End Property


        Private endDate_Renamed? As Date
        Public Property EndDate() As Date?
            Get
                Return endDate_Renamed
            End Get
            Set(ByVal value? As Date)
                If endDate_Renamed = value Then
                    Return
                End If
                endDate_Renamed = value
                RaisePropertyChanged("EndDate")
            End Set
        End Property


        Private allDay_Renamed? As Boolean
        Public Property AllDay() As Boolean?
            Get
                Return allDay_Renamed
            End Get
            Set(ByVal value? As Boolean)
                If allDay_Renamed = value Then
                    Return
                End If
                allDay_Renamed = value
                RaisePropertyChanged("AllDay")
            End Set
        End Property


        Private subject_Renamed As String
        Public Property Subject() As String
            Get
                Return subject_Renamed
            End Get
            Set(ByVal value As String)
                If subject_Renamed = value Then
                    Return
                End If
                subject_Renamed = value
                RaisePropertyChanged("Subject")
            End Set
        End Property


        Private location_Renamed As String
        Public Property Location() As String
            Get
                Return location_Renamed
            End Get
            Set(ByVal value As String)
                If location_Renamed = value Then
                    Return
                End If
                location_Renamed = value
                RaisePropertyChanged("Location")
            End Set
        End Property


        Private description_Renamed As String
        Public Property Description() As String
            Get
                Return description_Renamed
            End Get
            Set(ByVal value As String)
                If description_Renamed = value Then
                    Return
                End If
                description_Renamed = value
                RaisePropertyChanged("Description")
            End Set
        End Property


        Private status_Renamed? As Integer
        Public Property Status() As Integer?
            Get
                Return status_Renamed
            End Get
            Set(ByVal value? As Integer)
                If status_Renamed = value Then
                    Return
                End If
                status_Renamed = value
                RaisePropertyChanged("Status")
            End Set
        End Property


        Private label_Renamed? As Integer
        Public Property Label() As Integer?
            Get
                Return label_Renamed
            End Get
            Set(ByVal value? As Integer)
                If label_Renamed = value Then
                    Return
                End If
                label_Renamed = value
                RaisePropertyChanged("Label")
            End Set
        End Property


        Private recurrenceInfo_Renamed As String
        Public Property RecurrenceInfo() As String
            Get
                Return recurrenceInfo_Renamed
            End Get
            Set(ByVal value As String)
                If recurrenceInfo_Renamed = value Then
                    Return
                End If
                recurrenceInfo_Renamed = value
                RaisePropertyChanged("RecurrenceInfo")
            End Set
        End Property


        Private reminderInfo_Renamed As String
        Public Property ReminderInfo() As String
            Get
                Return reminderInfo_Renamed
            End Get
            Set(ByVal value As String)
                If reminderInfo_Renamed = value Then
                    Return
                End If
                reminderInfo_Renamed = value
                RaisePropertyChanged("ReminderInfo")
            End Set
        End Property


        Private contactInfo_Renamed As String
        Public Property ContactInfo() As String
            Get
                Return contactInfo_Renamed
            End Get
            Set(ByVal value As String)
                If contactInfo_Renamed = value Then
                    Return
                End If
                contactInfo_Renamed = value
                RaisePropertyChanged("ContactInfo")
            End Set
        End Property
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub RaisePropertyChanged(ByVal propertyName As String)
            If TypeDescriptor.GetProperties(Me)(propertyName) Is Nothing Then
                Throw New ArgumentException(propertyName & " doesn't exist in " & Me.GetType().Name & " type")
            End If
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

    Public Class CalendarModel
        Public ReadOnly Property Appointments() As ObservableCollection(Of Appointment)
            Get
                Return DataBaseHelper.Instance.Appointments
            End Get
        End Property


        Private Shared model_Renamed As CalendarModel = Nothing
        Public Shared ReadOnly Property Model() As CalendarModel
            Get
                If model_Renamed Is Nothing Then
                    model_Renamed = New CalendarModel()
                End If

                Return model_Renamed
            End Get
        End Property
    End Class
End Namespace
