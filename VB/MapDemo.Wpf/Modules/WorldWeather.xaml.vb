Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace MapDemo
    Partial Public Class WorldWeather
        Inherits MapDemoModule
        Implements INotifyPropertyChanged


        Private selectedItem_Renamed As Object

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property OpenWeatherMapService() As OpenWeatherMapService
        Public Property SelectedItem() As Object
            Get
                Return selectedItem_Renamed
            End Get
            Set(ByVal value As Object)
                If selectedItem_Renamed IsNot value Then
                    selectedItem_Renamed = value
                    Dim cityWeatherInfo As CityWeather = TryCast(selectedItem_Renamed, CityWeather)
                    If cityWeatherInfo IsNot Nothing AndAlso cityWeatherInfo.Forecast Is Nothing Then
                        OpenWeatherMapService.GetForecastForCityAsync(cityWeatherInfo)
                    End If
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectedItem"))
                End If
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            OpenWeatherMapService = New OpenWeatherMapService()
            DataContext = Me
            OpenWeatherMapService.GetWeatherAsync()
        End Sub

        Private Sub lbUnitType_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            OpenWeatherMapService.SetCurrentTemperatureType(CType(e.NewValue, TemperatureScale))
        End Sub
    End Class

    Public Class NullObjectToVisibiltyConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value Is Nothing, Visibility.Collapsed, Visibility.Visible)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
End Namespace
