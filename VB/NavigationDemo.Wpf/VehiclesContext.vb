Imports DevExpress.Internal
Imports System
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.SQLite
Imports System.IO

Namespace DevExpress.DemoData.Models.Vehicles
    Public Class Model
        Public Property ID() As Long
        Public Property TrademarkID() As Long
        Public Property Name() As String
        Public Property Modification() As String
        Public Property CategoryID() As Long
        Public Property Price() As Decimal
        Public Property MPGCity() As Integer?
        Public Property MPGHighway() As Integer?
        Public Property Doors() As Integer
        Public Property BodyStyleID() As Long
        Public Property Cylinders() As Integer
        Public Property Horsepower() As String
        Public Property Torque() As String
        Public Property TransmissionSpeeds() As String
        Public Property TransmissionTypeID() As Long
        Public Property Description() As String
        Public Property Image() As Byte()
        Public Property Photo() As Byte()
        Public Property DeliveryDate() As Date?
        Public Property InStock() As Boolean

        Public ReadOnly Property TrademarkName() As String
            Get
                Return Trademark.Name
            End Get
        End Property
        Public ReadOnly Property CategoryName() As String
            Get
                Return Category.Name
            End Get
        End Property
        Public ReadOnly Property BodyStyleName() As String
            Get
                Return BodyStyle.Name
            End Get
        End Property
        Public ReadOnly Property TransmissionTypeName() As String
            Get
                Return TransmissionType.Name
            End Get
        End Property

        Public Overridable Property Trademark() As Trademark
        Public Overridable Property Category() As Category
        Public Overridable Property BodyStyle() As BodyStyle
        Public Overridable Property TransmissionType() As TransmissionType
    End Class

    Public Class BodyStyle
        Public Property ID() As Long
        Public Property Name() As String
    End Class

    Public Class Category
        Public Property ID() As Long
        Public Property Name() As String
        Public Property Picture() As Byte()
    End Class

    Public Class Trademark
        Public Property ID() As Long
        Public Property Name() As String
        Public Property Site() As String
        Public Property Logo() As Byte()
        Public Property Description() As String
    End Class

    Public Class TransmissionType
        Public Property ID() As Long
        Public Property Name() As String
    End Class

    Partial Public Class VehiclesContext
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

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Entity(Of Model)().ToTable("Model")
            modelBuilder.Entity(Of BodyStyle)().ToTable("BodyStyle")
            modelBuilder.Entity(Of Category)().ToTable("Category")
            modelBuilder.Entity(Of Trademark)().ToTable("Trademark")
            modelBuilder.Entity(Of TransmissionType)().ToTable("TransmissionType")
            modelBuilder.Entity(Of Model)().Property(Function(x) x.MPGCity).HasColumnName("MPG City")
            modelBuilder.Entity(Of Model)().Property(Function(x) x.MPGHighway).HasColumnName("MPG Highway")
            modelBuilder.Entity(Of Model)().Property(Function(x) x.TransmissionSpeeds).HasColumnName("Transmission Speeds")
            modelBuilder.Entity(Of Model)().Property(Function(x) x.TransmissionTypeID).HasColumnName("Transmission Type")
            modelBuilder.Entity(Of Model)().Property(Function(x) x.DeliveryDate).HasColumnName("Delivery Date")
        End Sub

        Private Shared filePath As String
        Private Shared Function CreateConnection() As DbConnection
            If filePath Is Nothing Then
                filePath = DataDirectoryHelper.GetFile("vehicles.db", DataDirectoryHelper.DataFolderName)
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

        Public Property Models() As DbSet(Of Model)
        Public Property BodyStyles() As DbSet(Of BodyStyle)
        Public Property Categories() As DbSet(Of Category)
        Public Property Trademarks() As DbSet(Of Trademark)
        Public Property TransmissionTypes() As DbSet(Of TransmissionType)
    End Class
End Namespace
