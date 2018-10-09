Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization

Namespace ChartsDemo
    <XmlRoot("OilPrices")>
    Public Class OilPrices
        Inherits List(Of OilPricesByCompany)


        Private Shared dataSource_Renamed As List(Of OilPricesByCompany) = Nothing
        Public Shared ReadOnly Property DataSource() As List(Of OilPricesByCompany)
            Get
                If dataSource_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(OilPrices))
                    dataSource_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/OilPrices.xml")), List(Of OilPricesByCompany))
                End If
                Return dataSource_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property EuropeBrentPrices() As List(Of OilPriceByDate)
            Get
                Return OilPrices.DataSource.First(Function(x) x.CompanyName = "Europe Brent").Prices
            End Get
        End Property
    End Class
    Public Class OilPricesByCompany
        Public Property CompanyName() As String
        Public Property Prices() As List(Of OilPriceByDate)
    End Class
    Public Class OilPriceByDate
        Public Property Timestamp() As Date
        Public Property MinValue() As Double
        Public Property MaxValue() As Double
    End Class
End Namespace
