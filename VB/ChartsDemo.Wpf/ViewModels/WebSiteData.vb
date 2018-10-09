Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ChartsDemo
    Public NotInheritable Class WebSiteData

        Private Sub New()
        End Sub
        Public Shared ReadOnly Categories As List(Of WebSiteCategoryInfo)

        Shared Sub New()
            Dim data = CreateDataSource()
            Categories = { "Politics", "Entertainment", "Travel" }.Select(Function(x) New WebSiteCategoryInfo With {.Category = x, .Data = data}).ToList()
        End Sub

        Private Shared Function CreateDataSource() As IList(Of WebSitePopularity)
            Dim dataSource As New List(Of WebSitePopularity)()
            Dim year As Integer = Date.Now.Year
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 1), 65, 56, 45))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 2), 78, 45, 40))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 3), 95, 70, 56))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 4), 110, 82, 47))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 5), 108, 80, 38))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 6), 52, 20, 31))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 7), 46, 10, 27))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 8), 70, Nothing, 37))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 9), 86, Nothing, 42))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 10), 92, 65, Nothing))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 11), 108, 45, 37))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 12), 115, 56, 21))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 13), 75, 10, 10))
            dataSource.Add(New WebSitePopularity(New Date(year, 1, 14), 65, 0, 5))
            Return dataSource
        End Function
    End Class
    Public Class WebSiteCategoryInfo
        Public Property Category() As String
        Public Property Data() As IList(Of WebSitePopularity)
    End Class
    Public Class WebSitePopularity

        Private ReadOnly date_Renamed As Date

        Private ReadOnly politics_Renamed? As Double

        Private ReadOnly entertainment_Renamed? As Double

        Private ReadOnly travel_Renamed? As Double

        Public ReadOnly Property [Date]() As Date
            Get
                Return date_Renamed
            End Get
        End Property
        Public ReadOnly Property Politics() As Double?
            Get
                Return politics_Renamed
            End Get
        End Property
        Public ReadOnly Property Entertainment() As Double?
            Get
                Return entertainment_Renamed
            End Get
        End Property
        Public ReadOnly Property Travel() As Double?
            Get
                Return travel_Renamed
            End Get
        End Property

        Public Sub New(ByVal [date] As Date, ByVal politics? As Double, ByVal entertainment? As Double, ByVal travel? As Double)
            Me.date_Renamed = [date]
            Me.politics_Renamed = politics
            Me.entertainment_Renamed = entertainment
            Me.travel_Renamed = travel
        End Sub
    End Class
End Namespace
