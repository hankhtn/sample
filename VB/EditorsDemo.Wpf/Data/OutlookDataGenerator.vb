Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports System.Xml
Imports DevExpress.Data.Filtering
Imports System.Windows.Threading
Imports System.Threading
Imports System.ComponentModel
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.Data
Imports DevExpress.Xpf.DemoBase.DemosHelpers.Grid
Imports DevExpress.Mvvm
Imports System.Data.SqlClient
Imports System.Data
Imports GridDemo.Controls
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace EditorsDemo
    Public Class DemoDataProvider
        Public ReadOnly Property Users() As User()
            Get
                Return OutlookData.Users
            End Get
        End Property
        Public ReadOnly Property SummaryItemTypes() As SummaryItemType()
            Get
                Return New SummaryItemType() { SummaryItemType.Sum, SummaryItemType.Min, SummaryItemType.Max, SummaryItemType.Count, SummaryItemType.Average }
            End Get
        End Property
        Public ReadOnly Property SummaryFieldNames() As String()
            Get
                Return New String() { "UnitPrice", "Quantity", "Discount", "ExtendedPrice", "Freight", "Total" }
            End Get
        End Property
        Public Shared ReadOnly PalleteSizesStatic() As String = { "4x4", "10x10", "16x16", "20x20", "25x25" }
        Public ReadOnly Property PalleteSizes() As String()
            Get
                Return PalleteSizesStatic
            End Get
        End Property
    End Class
    Public Enum Priority
        Low
        BelowNormal
        Normal
        AboveNormal
        High
    End Enum
    <Serializable, POCOViewModel>
    Public Class OutlookData
        Public Shared ReadOnly Users() As User = {
            New User() With {.Id = 0, .Name = "Peter Dolan"},
            New User() With {.Id = 1, .Name = "Ryan Fischer"},
            New User() With {.Id = 2, .Name = "Richard Fisher"},
            New User() With {.Id = 3, .Name = "Tom Hamlett"},
            New User() With {.Id = 4, .Name = "Mark Hamilton"},
            New User() With {.Id = 5, .Name = "Steve Lee"},
            New User() With {.Id = 6, .Name = "Jimmy Lewis"},
            New User() With {.Id = 7, .Name = "Jeffrey W McClain"},
            New User() With {.Id = 8, .Name = "Andrew Miller"},
            New User() With {.Id = 9, .Name = "Dave Murrel"},
            New User() With {.Id = 10, .Name = "Bert Parkins"},
            New User() With {.Id = 11, .Name = "Mike Roller"},
            New User() With {.Id = 12, .Name = "Ray Shipman"},
            New User() With {.Id = 13, .Name = "Paul Bailey"},
            New User() With {.Id = 14, .Name = "Brad Barnes"},
            New User() With {.Id = 15, .Name = "Carl Lucas"},
            New User() With {.Id = 16, .Name = "Jerry Campbell"}
        }
        Public Overridable Property OID() As Integer?
        Public Overridable Property Subject() As String
        Public Overridable Property [From]() As String
        Public Overridable Property Sent() As Date?
        Public Overridable Property HasAttachment() As Boolean?
        Public Overridable Property Size() As Long?
        Public Overridable Property HoursActive() As Double?
        Public Overridable Property Priority() As Priority?
        Public Overridable Property UserId() As Integer?

        Public Overrides Function ToString() As String
            Return Subject
        End Function
    End Class
    Public Class User
        Public Property Id() As Integer
        Public Property Name() As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    Public NotInheritable Class OutlookDataGenerator

        Private Sub New()
        End Sub

        Private Shared rnd As New Random()
        Public Shared Subjects() As String = { "Integrating Developer Express MasterView control into an Accounting System.", "Web Edition: Data Entry Page. There is an issue with date validation.", "Payables Due Calculator is ready for testing.", "Web Edition: Search Page is ready for testing.", "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.", "Receivables Calculator. Where can I find the complete specs?", "Ledger: Inconsistency. Please fix it.", "Receivables Printing module is ready for testing.", "Screen Redraw. Somebody has to look at it.", "Email System. What library are we going to use?", "Cannot add new vendor. This module doesn't work!", "History. Will we track sales history in our system?", "Main Menu: Add a File menu. File menu item is missing.", "Currency Mask. The current currency mask in completely unusable.", "Drag & Drop operations are not available in the scheduler module.", "Data Import. What database types will we support?", "Reports. The list of incomplete reports.", "Data Archiving. We still don't have this features in our application.", "Email Attachments. Is it possible to add multiple attachments? I haven't found a way to do this.", "Check Register. We are using different paths for different modules.", "Data Export. Our customers asked us for export to Microsoft Excel"}

        Public Shared Function GetSubject() As String
            Return Subjects(rnd.Next(Subjects.Length - 1))
        End Function

        Public Shared Function GetFrom() As String
            Return OutlookData.Users(GetFromId()).Name
        End Function

        Public Shared Function GetSentDate() As Date
            Dim ret As Date = Date.Today
            Dim r As Integer = rnd.Next(12)
            If r > 1 Then
                ret = ret.AddDays(-rnd.Next(50))
            End If
            Return ret
        End Function
        Public Shared Function GetSize(ByVal largeData? As Boolean) As Integer?
            Return 1000 + (If(largeData.Value, 20 * rnd.Next(10000), 30 * rnd.Next(100)))
        End Function
        Public Shared Function GetHasAttachment() As Boolean
            Return rnd.Next(10) > 5
        End Function
        Public Shared Function GetPriority() As Priority
            Return CType(rnd.Next(5), Priority)
        End Function
        Public Shared Function GetHoursActive() As Integer
            Return CInt((Math.Round(rnd.NextDouble() * 1000, 1)))
        End Function
        Public Shared Function GetFromId() As Integer
            Return rnd.Next(OutlookData.Users.Length)
        End Function
        Public Shared Function CreateNewObject() As OutlookData
            Dim obj As OutlookData = ViewModelSource.Create(Function() New OutlookData())
            obj.Subject = GetSubject()
            obj.From = GetFrom()
            obj.Sent = GetSentDate()
            obj.HasAttachment = GetHasAttachment()
            obj.Size = GetSize(obj.HasAttachment)
            obj.Priority = GetPriority()
            obj.UserId = GetFromId()
            obj.HoursActive = GetHoursActive()
            Return obj
        End Function
        Public Shared Function CreateOutlookData(ByVal id As Integer) As OutlookData
            Dim data As OutlookData = ViewModelSource.Create(Function() New OutlookData())
            data.OID = id
            data.From = GetFrom()
            data.UserId = GetFromId()
            data.Sent = GetSentDate()
            data.HasAttachment = GetHasAttachment()
            data.Priority = GetPriority()
            data.HoursActive = GetHoursActive()
            data.Subject = GetSubject()
            Return data
        End Function
        Public Shared Function CreateOutlookDataTable(ByVal rowCount As Integer) As DataTable
            Dim table As New DataTable()
            table.Columns.Add("OID", GetType(Integer))
            table.Columns.Add("From", GetType(String))
            table.Columns.Add("ToId", GetType(Integer))
            table.Columns.Add("Sent", GetType(Date))
            table.Columns.Add("HasAttachment", GetType(Boolean))
            table.Columns.Add("Priority", GetType(Integer))
            table.Columns.Add("HoursActive", GetType(Integer))
            For i As Integer = 0 To rowCount - 1
                Dim row As DataRow = table.NewRow()
                row("OID") = i
                row("From") = GetFrom()
                row("ToId") = GetFromId()
                row("Sent") = GetSentDate()
                row("HasAttachment") = GetHasAttachment()
                row("Priority") = CInt(GetPriority())
                row("HoursActive") = GetHoursActive()
                table.Rows.Add(row)
            Next i
            Return table
        End Function
        Public Shared Function CreateOutlookArrayList(ByVal rowCount As Integer) As System.Collections.ArrayList
            Dim dataList As New System.Collections.ArrayList(rowCount)
            For i As Integer = 0 To rowCount - 1
                dataList.Add(CreateOutlookData(i))
            Next i
            Return dataList
        End Function
    End Class
End Namespace
