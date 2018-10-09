Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Reflection
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.XtraRichEdit
Imports DevExpress.Mvvm
Imports System.Windows.Data
Imports System.Globalization
Imports System.IO

Namespace GridDemo
    Public NotInheritable Class MailItems
        Private Shared privateDataSet As DataSet
        Friend Shared Property DataSet() As DataSet
            Get
                Return privateDataSet
            End Get
            Private Set(ByVal value As DataSet)
                privateDataSet = value
            End Set
        End Property

        Private Sub New()
        End Sub


        Private Shared messages_Renamed As ObservableCollection(Of Message) = Nothing
        Private Shared ReadOnly Property MailTable() As DataTable
            Get
                Return CreateDataTable("Messages")
            End Get
        End Property
        Private Shared Function CreateDataTable(ByVal table As String) As DataTable
            If DataSet Is Nothing Then
                DataSet = New DataSet()
                Dim assembly As System.Reflection.Assembly = GetType(StockItems).Assembly
                Using stream As Stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) & "MailDevAv.xml")
                    DataSet.ReadXml(stream)
                End Using
                DataSet.Relations.Add(DataSet.Tables("Employees").Columns("Email"), DataSet.Tables("Messages").Columns("From"))
            End If
            Return DataSet.Tables(table)
        End Function

        Public Shared ReadOnly Property Messages() As ObservableCollection(Of Message)
            Get
                Try
                    If messages_Renamed Is Nothing Then
                        messages_Renamed = New ObservableCollection(Of Message)()
                        Dim tbl As DataTable = MailTable
                        If tbl IsNot Nothing Then
                            For Each row As DataRow In tbl.Rows
                                messages_Renamed.Add(New Message(row))
                            Next row
                        End If
                    End If
                Catch e1 As Exception
                    messages_Renamed = New ObservableCollection(Of Message)()
                End Try
                Return messages_Renamed
            End Get
        End Property
    End Class

    Public Class MailEmployee
        Public Sub New(ByVal row As DataRow)
            FirstName = String.Format("{0}", row("FirstName"))
            LastName = String.Format("{0}", row("LastName"))
            FullName = String.Format("{0} {1}", FirstName, LastName)
            Gender = String.Format("{0}", row("Gender"))
            Dim employee = DevAVHelper.GetEmployee(String.Format("{0}", row("Email")))
            If employee IsNot Nothing Then
                FullName = employee.FullName
                Dim picture = employee.Picture
                If picture IsNot Nothing Then
                    Image = picture.Data
                End If
            End If
        End Sub
        Private privateFirstName As String
        Public Property FirstName() As String
            Get
                Return privateFirstName
            End Get
            Private Set(ByVal value As String)
                privateFirstName = value
            End Set
        End Property
        Private privateLastName As String
        Public Property LastName() As String
            Get
                Return privateLastName
            End Get
            Private Set(ByVal value As String)
                privateLastName = value
            End Set
        End Property
        Private privateFullName As String
        Public Property FullName() As String
            Get
                Return privateFullName
            End Get
            Private Set(ByVal value As String)
                privateFullName = value
            End Set
        End Property
        Public ReadOnly Property Initials() As String
            Get
                Return String.Format("{0}{1}", FirstName.First(), LastName.First())
            End Get
        End Property
        Private privateGender As String
        Public Property Gender() As String
            Get
                Return privateGender
            End Get
            Private Set(ByVal value As String)
                privateGender = value
            End Set
        End Property
        Private privateImage As Byte()
        Public Property Image() As Byte()
            Get
                Return privateImage
            End Get
            Private Set(ByVal value As Byte())
                privateImage = value
            End Set
        End Property
    End Class

    Public Class Message
        Public Sub New(ByVal row As DataRow)
            Employee = New MailEmployee(row.GetParentRow(MailItems.DataSet.Relations(0)))
            Received = Date.Now.AddDays(DirectCast(row("Day"), Integer))
            Subject = String.Format("{0}", row("Subject"))
            HasAttachment = DirectCast(row("HasAttachment"), Boolean)
            Read = Delay > TimeSpan.FromHours(6)
            If Delay > TimeSpan.FromHours(50) AndAlso Delay < TimeSpan.FromHours(100) Then
                Read = False
            End If
            Text = String.Format("{0}", row("Text"))
            Priority = CalcPriority()
            If Not(TypeOf row("CategoryID") Is DBNull) Then
                MailFolder = DirectCast(row("CategoryID"), MailFolder)
            End If
        End Sub

        Public Property Employee() As MailEmployee

        Public Property Received() As Date

        Public Property Subject() As String

        Public Property HasAttachment() As Boolean

        Public Property Read() As Boolean

        Public Property Text() As String

        Public Property Priority() As Integer

        Public Property MailFolder() As MailFolder

        Private _plainText As String
        Public ReadOnly Property PlainText() As String
            Get
                If _plainText Is Nothing Then
                    _plainText = PlainTextProvider.GetPlainText(Text)
                End If
                Return _plainText
            End Get
        End Property
        Friend ReadOnly Property Delay() As TimeSpan
            Get
                Return Date.Now.Subtract(Received)
            End Get
        End Property

        Private Function CalcPriority() As Integer
            If Subject.IndexOf("Review") >= 0 OrElse Subject.IndexOf("Important") >= 0 Then
                Return 2
            End If
            If Subject.IndexOf("FW:") >= 0 AndAlso Delay > TimeSpan.FromHours(48) Then
                Return 0
            End If
            Return 1
        End Function
    End Class

    Public Enum MailFolder
        All = 0
        General
        Management
        IT
        Sales
        Support
        Engineering
        HR
        Design
    End Enum

    Public NotInheritable Class PlainTextProvider

        Private Sub New()
        End Sub
        Private Shared rich As New RichEditDocumentServer()

        Public Shared Function GetPlainText(ByVal mhtText As String) As String
            rich.MhtText = mhtText
            Return rich.Text.TrimStart().Substring(0, Math.Min(100, mhtText.Length)).Replace(ControlChars.Cr, " "c).Replace(ControlChars.Lf, " "c)
        End Function
    End Class
End Namespace
