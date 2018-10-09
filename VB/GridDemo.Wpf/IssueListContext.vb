Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.DemoData.Models
Imports DevExpress.DemoData.Models.Mapping
Imports DevExpress.Internal
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports System.Data.SQLite
Imports System.IO

Namespace DevExpress.DemoData.Models
    Partial Public Class Department
        Public Property ID() As Long
        Public Property Name() As String
    End Class
End Namespace

Namespace DevExpress.DemoData

    Public Class IssueDataLoader
        Inherits DataLoaderBase

        Private itemsLoaded As Boolean = False
        Private projectsLoaded As Boolean = False
        Private usersLoaded As Boolean = False
        Private context As New IssueListContext()
        Public ReadOnly Property Items() As ObservableCollection(Of Item)
            Get
                LoadIfNeed(itemsLoaded, context.Items)
                Return context.Items.Local
            End Get
        End Property
        Public ReadOnly Property Projects() As ObservableCollection(Of Project)
            Get
                LoadIfNeed(projectsLoaded, context.Projects)
                Return context.Projects.Local
            End Get
        End Property
        Public ReadOnly Property Users() As ObservableCollection(Of User)
            Get
                LoadIfNeed(usersLoaded, context.Users)
                Return context.Users.Local
            End Get
        End Property
    End Class
    Public Enum IssueType
        Request
        Bug
    End Enum

    Friend NotInheritable Class IssuePriorityHelper

        Private Sub New()
        End Sub

        Public Const CellMergingImagesPath As String = "pack://application:,,,/GridDemo;component/Images/CellMerging/"
    End Class
    Public Enum IssuePriority
        <Image(IssuePriorityHelper.CellMergingImagesPath & "Low.png")>
        Low
        <Image(IssuePriorityHelper.CellMergingImagesPath & "Medium.png")>
        Medium
        <Image(IssuePriorityHelper.CellMergingImagesPath & "High.png")>
        High
    End Enum
    Public Enum Status
        [New]
        Postponed
        Fixed
        Rejected
    End Enum
End Namespace


Namespace DevExpress.DemoData.Models
    Partial Public Class Item
        Public Property ID() As Long
        Public Property Name() As String
        Public Property Type() As IssueType
        Public Property ProjectID() As Long?
        Public Property Priority() As IssuePriority?
        Public Property Status() As Status?
        Public Property CreatorID() As Long?
        Public Property CreatedDate() As Date?
        Public Property OwnerID() As Long?
        Public Property ModifiedDate() As Date?
        Public Property FixedDate() As Date?
        Public Property Description() As String
        Public Property Resolution() As String
    End Class
End Namespace


Namespace DevExpress.DemoData.Models
    Partial Public Class IssueListContext
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

        Private Shared defaultContext As IssueListContext
        Public Shared ReadOnly Property [Default]() As IssueListContext
            Get
                If defaultContext IsNot Nothing Then
                    Return defaultContext
                Else
                    defaultContext = New IssueListContext()
                    Return defaultContext
                End If
            End Get
        End Property

        Shared Sub New()
            Database.SetInitializer(Of IssueListContext)(Nothing)
        End Sub

        Public Property Departments() As DbSet(Of Department)
        Public Property Items() As DbSet(Of Item)
        Public Property Projects() As DbSet(Of Project)
        Public Property ProjectTeams() As DbSet(Of ProjectTeam)
        Public Property Schedulers() As DbSet(Of Scheduler)
        Public Property Tasks() As DbSet(Of Task)
        Public Property Users() As DbSet(Of User)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New DepartmentMap())
            modelBuilder.Configurations.Add(New ItemMap())
            modelBuilder.Configurations.Add(New ProjectMap())
            modelBuilder.Configurations.Add(New ProjectTeamMap())
            modelBuilder.Configurations.Add(New SchedulerMap())
            modelBuilder.Configurations.Add(New TaskMap())
            modelBuilder.Configurations.Add(New UserMap())
        End Sub

        Private Shared filePath As String
        Private Shared Function CreateConnection() As DbConnection
            If filePath Is Nothing Then
                filePath = DataDirectoryHelper.GetFile("issue-list.db", DataDirectoryHelper.DataFolderName)
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
    Partial Public Class Project
        Public Property ID() As Long
        Public Property Name() As String
        Public Property ManagerID() As Long?
    End Class
End Namespace


Namespace DevExpress.DemoData.Models
    Partial Public Class ProjectTeam
        Public Property ID() As Long
        Public Property ProjectID() As Long?
        Public Property UserID() As Long?
        Public Property [Function]() As String
    End Class
End Namespace


Namespace DevExpress.DemoData.Models
    Partial Public Class Scheduler
        Public Property ID() As Long
        Public Property ProjectID() As Long?
        Public Property UserID() As Long?
        Public Property Sunday() As Short?
        Public Property Monday() As Short?
        Public Property Tuesday() As Short?
        Public Property Wednesday() As Short?
        Public Property Thursday() As Short?
        Public Property Friday() As Short?
        Public Property Saturday() As Short?
    End Class
End Namespace


Namespace DevExpress.DemoData.Models
    Partial Public Class Task
        Public Property ID() As Long
        Public Property Name() As String
        Public Property ParentID() As Long
        Public Property UserID() As Long
        Public Property StartDate() As Date?
        Public Property EndDate() As Date?
        Public Property Done() As Boolean
        Public Property Priority() As Long?
        Public Property Complete() As Double?
    End Class
End Namespace


Namespace DevExpress.DemoData.Models
    Partial Public Class User
        Public Property ID() As Long
        Public Property FirstName() As String
        Public Property MiddleName() As String
        Public Property LastName() As String
        Public Property Country() As String
        Public Property PostalCode() As String
        Public Property City() As String
        Public Property Address() As String
        Public Property Phone() As String
        Public Property Fax() As String
        Public Property Email() As String
        Public Property HomePage() As String
        Public Property DepartmentID() As Long?
        Public ReadOnly Property FullName() As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property
    End Class
End Namespace


Namespace DevExpress.DemoData.Models.Mapping
    Public Class DepartmentMap
        Inherits EntityTypeConfiguration(Of Department)

        Public Sub New()

            Me.HasKey(Function(t) t.ID)

            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            Me.Property(Function(t) t.Name).HasMaxLength(100)

            Me.ToTable("Departments")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Name).HasColumnName("Name")
        End Sub
    End Class
End Namespace


Namespace DevExpress.DemoData.Models.Mapping
    Public Class ItemMap
        Inherits EntityTypeConfiguration(Of Item)

        Public Sub New()

            Me.HasKey(Function(t) New With {Key t.ID})

            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            Me.Property(Function(t) t.Name).HasMaxLength(50)
            Me.Property(Function(t) t.Description).HasMaxLength(2147483647)
            Me.Property(Function(t) t.Resolution).HasMaxLength(2147483647)

            Me.ToTable("Items")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Name).HasColumnName("Name")
            Me.Property(Function(t) t.Type).HasColumnName("Type")
            Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
            Me.Property(Function(t) t.Priority).HasColumnName("Priority")
            Me.Property(Function(t) t.Status).HasColumnName("Status")
            Me.Property(Function(t) t.CreatorID).HasColumnName("CreatorID")
            Me.Property(Function(t) t.CreatedDate).HasColumnName("CreatedDate")
            Me.Property(Function(t) t.OwnerID).HasColumnName("OwnerID")
            Me.Property(Function(t) t.ModifiedDate).HasColumnName("ModifiedDate")
            Me.Property(Function(t) t.FixedDate).HasColumnName("FixedDate")
            Me.Property(Function(t) t.Description).HasColumnName("Description")
            Me.Property(Function(t) t.Resolution).HasColumnName("Resolution")
        End Sub
    End Class
End Namespace


Namespace DevExpress.DemoData.Models.Mapping
    Public Class ProjectMap
        Inherits EntityTypeConfiguration(Of Project)

        Public Sub New()

            Me.HasKey(Function(t) t.ID)

            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            Me.Property(Function(t) t.Name).HasMaxLength(100)

            Me.ToTable("Projects")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Name).HasColumnName("Name")
            Me.Property(Function(t) t.ManagerID).HasColumnName("ManagerID")
        End Sub
    End Class
End Namespace


Namespace DevExpress.DemoData.Models.Mapping
    Public Class ProjectTeamMap
        Inherits EntityTypeConfiguration(Of ProjectTeam)

        Public Sub New()

            Me.HasKey(Function(t) t.ID)

            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            Me.Property(Function(t) t.Function).HasMaxLength(50)

            Me.ToTable("ProjectTeam")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
            Me.Property(Function(t) t.UserID).HasColumnName("UserID")
            Me.Property(Function(t) t.Function).HasColumnName("Function")
        End Sub
    End Class
End Namespace


Namespace DevExpress.DemoData.Models.Mapping
    Public Class SchedulerMap
        Inherits EntityTypeConfiguration(Of Scheduler)

        Public Sub New()

            Me.HasKey(Function(t) t.ID)

            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)

            Me.ToTable("Scheduler")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
            Me.Property(Function(t) t.UserID).HasColumnName("UserID")
            Me.Property(Function(t) t.Sunday).HasColumnName("Sunday")
            Me.Property(Function(t) t.Monday).HasColumnName("Monday")
            Me.Property(Function(t) t.Tuesday).HasColumnName("Tuesday")
            Me.Property(Function(t) t.Wednesday).HasColumnName("Wednesday")
            Me.Property(Function(t) t.Thursday).HasColumnName("Thursday")
            Me.Property(Function(t) t.Friday).HasColumnName("Friday")
            Me.Property(Function(t) t.Saturday).HasColumnName("Saturday")
        End Sub
    End Class
End Namespace


Namespace DevExpress.DemoData.Models.Mapping
    Public Class TaskMap
        Inherits EntityTypeConfiguration(Of Task)

        Public Sub New()

            Me.HasKey(Function(t) New With {Key t.ID, Key t.Name, Key t.ParentID, Key t.UserID, Key t.Done})

            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            Me.Property(Function(t) t.Name).IsRequired().HasMaxLength(50)
            Me.Property(Function(t) t.ParentID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            Me.Property(Function(t) t.UserID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)

            Me.ToTable("Tasks")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Name).HasColumnName("Name")
            Me.Property(Function(t) t.ParentID).HasColumnName("ParentID")
            Me.Property(Function(t) t.UserID).HasColumnName("UserID")
            Me.Property(Function(t) t.StartDate).HasColumnName("StartDate")
            Me.Property(Function(t) t.EndDate).HasColumnName("EndDate")
            Me.Property(Function(t) t.Done).HasColumnName("Done")
            Me.Property(Function(t) t.Priority).HasColumnName("Priority")
            Me.Property(Function(t) t.Complete).HasColumnName("Complete")
        End Sub
    End Class
End Namespace


Namespace DevExpress.DemoData.Models.Mapping
    Public Class UserMap
        Inherits EntityTypeConfiguration(Of User)

        Public Sub New()

            Me.HasKey(Function(t) t.ID)

            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            Me.Property(Function(t) t.FirstName).HasMaxLength(25)
            Me.Property(Function(t) t.MiddleName).HasMaxLength(20)
            Me.Property(Function(t) t.LastName).HasMaxLength(25)
            Me.Property(Function(t) t.Country).HasMaxLength(15)
            Me.Property(Function(t) t.PostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.Address).HasMaxLength(60)
            Me.Property(Function(t) t.Phone).HasMaxLength(24)
            Me.Property(Function(t) t.Fax).HasMaxLength(24)
            Me.Property(Function(t) t.Email).HasMaxLength(50)
            Me.Property(Function(t) t.HomePage).HasMaxLength(50)

            Me.ToTable("Users")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.FirstName).HasColumnName("FirstName")
            Me.Property(Function(t) t.MiddleName).HasColumnName("MiddleName")
            Me.Property(Function(t) t.LastName).HasColumnName("LastName")
            Me.Property(Function(t) t.Country).HasColumnName("Country")
            Me.Property(Function(t) t.PostalCode).HasColumnName("PostalCode")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.Address).HasColumnName("Address")
            Me.Property(Function(t) t.Phone).HasColumnName("Phone")
            Me.Property(Function(t) t.Fax).HasColumnName("Fax")
            Me.Property(Function(t) t.Email).HasColumnName("Email")
            Me.Property(Function(t) t.HomePage).HasColumnName("HomePage")
            Me.Property(Function(t) t.DepartmentID).HasColumnName("DepartmentID")
        End Sub
    End Class
End Namespace
