Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Media
Imports System.Xml.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo
    Public Class DailyWeatherViewModel
        Private Const vostokStationName As String = "Vostok Station"
        Private Const deathValleyName As String = "Death Valley, NV"


        Private ReadOnly weather_Renamed As List(Of WeatherItem)

        Public ReadOnly Property Weather() As List(Of WeatherItem)
            Get
                Return weather_Renamed
            End Get
        End Property

        Public Sub New()
            Dim valleyData As List(Of WeatherRecord) = LoadWeatherData("DeathValley.xml")
            Dim vostokData As List(Of WeatherRecord) = LoadWeatherData("VostokStation.xml")

            Dim palette As Palette = New OfficePalette()
            weather_Renamed = New List(Of WeatherItem)() From {
                New WeatherItem(valleyData, False, palette(1), deathValleyName),
                New WeatherItem(valleyData, True, palette(1), deathValleyName),
                New WeatherItem(vostokData, False, palette(0), vostokStationName),
                New WeatherItem(vostokData, True, palette(0), vostokStationName)
            }
        End Sub
        Private Function LoadWeatherData(ByVal fileName As String) As List(Of WeatherRecord)
            Dim items As New List(Of WeatherRecord)()
            Dim weatherDocument As XDocument = DataLoader.LoadXmlFromResources("/Data/DailyWeather/" & fileName)
            For Each element As XElement In weatherDocument.Root.Elements("Weather")
                items.Add(WeatherRecord.Load(element))
            Next element
            Return items
        End Function
        Public Overridable ReadOnly Property ChartAnimationService() As IChartAnimationService
            Get
                Return Nothing
            End Get
        End Property
        Public Sub OnModuleLoaded()
            If ChartAnimationService IsNot Nothing Then
                ChartAnimationService.Animate()
            End If
        End Sub
    End Class

    Public Class WeatherItem
        Inherits BindableBase

        Public Property AverageLineThickness() As Integer
            Get
                Return GetProperty(Function() AverageLineThickness)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Function() AverageLineThickness, value)
            End Set
        End Property
        Private privateData As List(Of WeatherRecord)
        Public Property Data() As List(Of WeatherRecord)
            Get
                Return privateData
            End Get
            Private Set(ByVal value As List(Of WeatherRecord))
                privateData = value
            End Set
        End Property
        Private privateIsAverageWeather As Boolean
        Public Property IsAverageWeather() As Boolean
            Get
                Return privateIsAverageWeather
            End Get
            Private Set(ByVal value As Boolean)
                privateIsAverageWeather = value
            End Set
        End Property
        Private privateColor As Color
        Public Property Color() As Color
            Get
                Return privateColor
            End Get
            Private Set(ByVal value As Color)
                privateColor = value
            End Set
        End Property
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property

        Public Sub New(ByVal data As List(Of WeatherRecord), ByVal isAverageWeather As Boolean, ByVal color As Color, ByVal name As String)
            Me.Data = data
            Me.IsAverageWeather = isAverageWeather
            Me.Color = color
            Me.Name = name
            AverageLineThickness = 2
        End Sub
    End Class

    Public Class WeatherRecord
        Public Shared Function Load(ByVal element As XElement) As WeatherRecord
            Dim culture As CultureInfo = CultureInfo.InvariantCulture
            Dim _date As Date = Date.Parse(element.Attribute("Date").Value, culture)
            Dim min As Double = Double.Parse(element.Attribute("Min").Value, culture)
            Dim max As Double = Double.Parse(element.Attribute("Max").Value, culture)
            Dim avg As Double = Double.Parse(element.Attribute("Avg").Value, culture)
            Return New WeatherRecord(_date, max, avg, min)
        End Function


        Private date_Renamed As Date

        Private privateMinValue As Double
        Public Property MinValue() As Double
            Get
                Return privateMinValue
            End Get
            Private Set(ByVal value As Double)
                privateMinValue = value
            End Set
        End Property
        Private privateMaxValue As Double
        Public Property MaxValue() As Double
            Get
                Return privateMaxValue
            End Get
            Private Set(ByVal value As Double)
                privateMaxValue = value
            End Set
        End Property
        Private privateAvgValue As Double
        Public Property AvgValue() As Double
            Get
                Return privateAvgValue
            End Get
            Private Set(ByVal value As Double)
                privateAvgValue = value
            End Set
        End Property
        Public Property [Date]() As Date
            Get
                Return date_Renamed
            End Get
            Private Set(ByVal value As Date)
                date_Renamed = value
            End Set
        End Property

        Private Sub New(ByVal _date As Date, ByVal maxValue As Double, ByVal avgValue As Double, ByVal minValue As Double)
            Me.Date = _date
            Me.MaxValue = maxValue
            Me.AvgValue = avgValue
            Me.MinValue = minValue
        End Sub
    End Class
End Namespace
