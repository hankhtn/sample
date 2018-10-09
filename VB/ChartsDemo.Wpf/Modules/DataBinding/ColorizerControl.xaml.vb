Imports System
Imports System.Windows
Imports System.Xml.Linq
Imports System.Collections.Generic
Imports System.Globalization
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/DataBinding/ColorizerControl.xaml"), CodeFile("Modules/DataBinding/ColorizerControl.xaml.(cs)")>
    Partial Public Class ColorizerControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            series.DataSource = GetHPIs()
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
        Private Function GetHPIs() As List(Of HPIStatistics)
            Dim document As XDocument = DataLoader.LoadXmlFromResources("/Data/HPI.xml")
            Dim result As New List(Of HPIStatistics)()
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("G20HPIs").Elements()
                    Dim country As String = element.Element("Country").Value
                    Dim population As Double = Integer.Parse(element.Element("Population").Value) / 1000000.0R
                    Dim hpi As Double = Convert.ToDouble(element.Element("HPI").Value, CultureInfo.InvariantCulture)
                    Dim product As Decimal = Convert.ToDecimal(element.Element("Product").Value, CultureInfo.InvariantCulture)
                    result.Add(New HPIStatistics(country, population, hpi, product, String.Format("{0:n2}", hpi)))
                Next element
            End If
            Return result
        End Function
    End Class

    Public Class HPIStatistics
        Private privateCountry As String
        Public Property Country() As String
            Get
                Return privateCountry
            End Get
            Private Set(ByVal value As String)
                privateCountry = value
            End Set
        End Property
        Private privatePopulation As Double
        Public Property Population() As Double
            Get
                Return privatePopulation
            End Get
            Private Set(ByVal value As Double)
                privatePopulation = value
            End Set
        End Property
        Private privateHPI As Double
        Public Property HPI() As Double
            Get
                Return privateHPI
            End Get
            Private Set(ByVal value As Double)
                privateHPI = value
            End Set
        End Property
        Private privateHPIHint As String
        Public Property HPIHint() As String
            Get
                Return privateHPIHint
            End Get
            Private Set(ByVal value As String)
                privateHPIHint = value
            End Set
        End Property
        Private privateProduct As Decimal
        Public Property Product() As Decimal
            Get
                Return privateProduct
            End Get
            Private Set(ByVal value As Decimal)
                privateProduct = value
            End Set
        End Property

        Public Sub New(ByVal country As String, ByVal population As Double, ByVal hpi As Double, ByVal product As Decimal, ByVal hpiHint As String)
            Me.Country = country
            Me.Population = population
            Me.HPI = hpi
            Me.Product = product
            Me.HPIHint = hpiHint
        End Sub
    End Class
End Namespace
