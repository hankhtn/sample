Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/DataBinding/MVVMStyleBinding.xaml"), CodeFile("Modules/DataBinding/MVVMStyleBinding.xaml.(cs)"), CodeFile("ViewModels/DailyWeatherViewModel.(cs)")>
    Partial Public Class MVVMStyleBinding
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class WeatherTemplateSelector
        Inherits DataTemplateSelector

        Public Property AverageTemplate() As DataTemplate
        Public Property MinMaxTemplate() As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim weatherItem As WeatherItem = TryCast(item, WeatherItem)
            If weatherItem IsNot Nothing Then
                Return If(weatherItem.IsAverageWeather, AverageTemplate, MinMaxTemplate)
            End If
            Return Nothing
        End Function
    End Class
End Namespace
