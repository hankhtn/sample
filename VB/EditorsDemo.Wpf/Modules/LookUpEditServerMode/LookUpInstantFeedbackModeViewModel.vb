Imports DevExpress.Data.Linq
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic
Imports System.Data.SQLite

Namespace GridDemo
    Public Class LookUpInstantFeedbackModeViewModel
        Inherits InstantFeedbackModeViewModelBase(Of Person)

        #Region "DatabaseInfo"
        Private Shared ReadOnly Generator As New PersonGenerator()
        Public Shared ReadOnly DatabaseInfo As New DatabaseInfo(PersonsDataContext.FileName, "People", GetType(Person), AddressOf CreateValues, Function(sql, connection) New SQLiteCommand(sql, CType(connection, SQLiteConnection)))
        Private Shared Function CreateValues() As IDictionary(Of String, Object)
            Dim person = Generator.CreatePerson()
            Dim dict = New Dictionary(Of String, Object)()
            dict.Add("FullName", person.FullName)
            dict.Add("Company", person.Company)
            dict.Add("JobTitle", person.JobTitle)
            dict.Add("City", person.City)
            dict.Add("Address", person.Address)
            dict.Add("Phone", person.Phone)
            dict.Add("Email", person.Email)
            Return dict
        End Function
        #End Region

        Protected Sub New()
            MyBase.New(DatabaseInfo, Function() (New PersonsDataContext()).People)
            Orders = OrderDataGenerator.GenerateOrders(200)
        End Sub

        Private privateOrders As List(Of OrderData)
        Public Property Orders() As List(Of OrderData)
            Get
                Return privateOrders
            End Get
            Private Set(ByVal value As List(Of OrderData))
                privateOrders = value
            End Set
        End Property
        Public Overridable Property InstantFeedbackSource() As EntityInstantFeedbackSource

        Protected Overrides Sub AssignSourcesCore()
            MyBase.AssignSourcesCore()
            DisposeInstantFeedbackSource()
            Dim source = New EntityInstantFeedbackSource() With {.KeyExpression = "Id"}
            AddHandler source.GetQueryable, Sub(o, e) e.QueryableSource = getQueryable()
            InstantFeedbackSource = source
        End Sub
        Public Overrides Sub OnUnloaded()
            MyBase.OnUnloaded()
            DisposeInstantFeedbackSource()
        End Sub

        Private Sub DisposeInstantFeedbackSource()
            If InstantFeedbackSource IsNot Nothing Then
                InstantFeedbackSource.Dispose()
                InstantFeedbackSource = Nothing
            End If
        End Sub
    End Class
End Namespace
