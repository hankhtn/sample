Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Xml.Linq
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Map
Imports System.Xml.Serialization
Imports System.Linq

Namespace ChartsDemo
    Public Class DashboardViewModel
        Public ReadOnly Property CountriesData() As List(Of CountryStatisticInfo)
            Get
                Return CountriesInfo.DataSource
            End Get
        End Property
        Public Overridable Property SelectedCountry() As CountryStatisticInfo
        Public Overridable Property SelectedCountryName() As String

        Public Sub New()
            SelectedCountry = CountriesData.First()
        End Sub

        Protected Sub OnSelectedCountryChanged()
            SelectedCountryName = If(SelectedCountry IsNot Nothing, SelectedCountry.Name, String.Empty)
        End Sub
        Protected Sub OnSelectedCountryNameChanged()
            SelectedCountry = CountriesData.FirstOrDefault(Function(x) x.Name = SelectedCountryName)
        End Sub
    End Class

    <XmlRoot("CountriesInfo")>
    Public Class CountriesInfo
        Inherits List(Of CountryStatisticInfo)


        Private Shared dataSource_Renamed As List(Of CountryStatisticInfo) = Nothing
        Public Shared ReadOnly Property DataSource() As List(Of CountryStatisticInfo)
            Get
                If dataSource_Renamed Is Nothing Then
                    Dim s = New XmlSerializer(GetType(CountriesInfo))
                    dataSource_Renamed = DirectCast(s.Deserialize(DataLoader.LoadFromResources("/Data/Top10LargestCountriesInfo.xml")), List(Of CountryStatisticInfo))
                End If
                Return dataSource_Renamed
            End Get
        End Property
    End Class

    <XmlType("CountryInfo")>
    Public Class CountryStatisticInfo
        Public Property Name() As String
        <XmlArray("Statistic"), XmlArrayItem("PopulationStatisticByYear")>
        Public Property PopulationDynamic() As List(Of PopulationStatisticByYear)
        Public Property AreaSqrKilometers() As Double
        Public ReadOnly Property AreaMSqrKilometers() As Double
            Get
                Return AreaSqrKilometers / 1000000
            End Get
        End Property
    End Class

    Public Class PopulationStatisticByYear
        Public Property Year() As Integer
        Public Property Population() As Double
        Public Property UrbanPercent() As Double

        Public ReadOnly Property PopulationMillionsOfPeople() As Double
            Get
                Return Population / 1000000
            End Get
        End Property
        Public ReadOnly Property RuralPercent() As Double
            Get
                Return 100 - UrbanPercent
            End Get
        End Property
    End Class
End Namespace
