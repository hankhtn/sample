Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Xml.Linq

Namespace ChartsDemo
    Public NotInheritable Class ExchangeRatesModel

        Private Sub New()
        End Sub

        Private Shared _gbpUsdRate As List(Of PriceByDate)
        Private Shared _eurUsdRate As List(Of PriceByDate)

        Public Shared ReadOnly Property GbpUsdRate() As List(Of PriceByDate)
            Get
                If _gbpUsdRate IsNot Nothing Then
                    Return _gbpUsdRate
                Else
                    _gbpUsdRate = LoadData(DataLoader.LoadXmlFromResources("/Data/GbpUsdRate.xml"))
                    Return _gbpUsdRate
                End If
            End Get
        End Property
        Public Shared ReadOnly Property EurUsdRate() As List(Of PriceByDate)
            Get
                If _eurUsdRate IsNot Nothing Then
                    Return _eurUsdRate
                Else
                    _eurUsdRate = LoadData(DataLoader.LoadXmlFromResources("/Data/EurUsdRate.xml"))
                    Return _eurUsdRate
                End If
            End Get
        End Property

        Private Shared Function LoadData(ByVal document As XDocument) As List(Of PriceByDate)
            If document Is Nothing Then
                Return New List(Of PriceByDate)()
            End If
            Return document.Descendants("CurrencyRate").Select(Function(element)
                Dim culture As CultureInfo = CultureInfo.InvariantCulture
                Dim [date] = Date.Parse(element.Element("DateTime").Value, culture)
                Dim price = Double.Parse(element.Element("Rate").Value, culture)
                Return New PriceByDate([date], price)
            End Function).ToList()
        End Function
    End Class
End Namespace
