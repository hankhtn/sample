Imports System
Imports System.Windows.Media
Imports System.Text
Imports System.Linq
Imports System.IO
Imports System.Data.SQLite
Imports System.Data.Entity
Imports System.Data.Common
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports DevExpress.Internal
Imports DevExpress.DemoData.Helpers



Namespace DevExpress.DemoData.Models
    Partial Public Class CarsContext
        Inherits DbContext

        Public Sub New()
            MyBase.New(CreateConnection(), True)
        End Sub
        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString)
        End Sub
        Public Sub New(ByVal connection As DbConnection)
            MyBase.New(connection, True)
        End Sub

        Shared Sub New()
            Database.SetInitializer(Of CarsContext)(Nothing)
        End Sub

        Public Property Cars() As DbSet(Of Car)
        Public Property CarSchedule() As DbSet(Of CarScheduling)
        Public Property UsageTypes() As DbSet(Of UsageType)

        Private Shared filePath As String
        Private Shared Function CreateConnection() As DbConnection
            If filePath Is Nothing Then
                filePath = DataDirectoryHelper.GetFile("cars.db", DataDirectoryHelper.DataFolderName)
            End If
            Try
                Dim attributes = File.GetAttributes(filePath)
                If attributes.HasFlag(FileAttributes.ReadOnly) Then
                    File.SetAttributes(filePath, attributes And (Not FileAttributes.ReadOnly))
                End If
            Catch
            End Try
            Dim connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection()
            connection.ConnectionString = New SQLiteConnectionStringBuilder With {.DataSource = filePath}.ConnectionString
            Return connection
        End Function
    End Class
End Namespace



Namespace DevExpress.DemoData.Models
    Public Class Car

        Private imageSource_Renamed As ImageSource
        Public Property Id() As Long
        Public Property Trademark() As String
        Public Property Model() As String
        Public Property HP() As Integer
        Public Property Liter() As Double
        Public Property Cyl() As Integer
        Public Property TransmissSpeedCount() As Integer
        Public Property TransmissAutomatic() As String
        Public Property MPGCity() As Integer
        Public Property MPGHighway() As Integer
        Public Property Category() As String
        Public Property Description() As String
        Public Property Hyperlink() As String
        Public Property Picture() As Byte()
        Public Property Icon() As Byte()
        Public ReadOnly Property ImageSource() As ImageSource
            Get
                If imageSource_Renamed Is Nothing Then
                    imageSource_Renamed = ImageSourceHelper.GetImageSource(New MemoryStream(Picture))
                End If
                Return imageSource_Renamed
            End Get
        End Property
        Public Property Price() As Decimal
        Public Property DeliveryDate() As Date
        Public Property IsInStock() As Boolean
    End Class

    Public Class CarScheduling
        Public Property Id() As Long
        Public Overridable Property Car() As Car
        Public Property Status() As Integer
        Public Property Subject() As String
        Public Property Description() As String
        Public Property Label() As Integer
        Public Property StartTime() As Date
        Public Property EndTime() As Date
        Public Property Location() As String
        Public Property AllDay() As Boolean
        Public Property EventType() As Integer
        Public Property RecurrenceInfo() As String
        Public Property ReminderInfo() As String
        Public Property Price() As Decimal
        Public Property ContactInfo() As String
    End Class

    Public Class UsageType
        Public Property Id() As Long
        Public Property Name() As String
        Public Property Color() As Integer
    End Class
End Namespace



Namespace DevExpress.DemoData.Models
    Public Class CarsData
        Private context As New CarsContext()
        Public ReadOnly Property Cars() As IEnumerable(Of Car)
            Get
                Return context.Cars.ToList()
            End Get
        End Property
        Public ReadOnly Property CarsSchedule() As IEnumerable(Of CarScheduling)
            Get
                Return context.CarSchedule.ToList()
            End Get
        End Property
    End Class
End Namespace
