Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/DataRepresentation/TopNOptionsControl.xaml"), CodeFile("Modules/DataRepresentation/TopNOptionsControl.xaml.(cs)"), CodeFile("ViewModels/CountryData.(cs)")>
    Partial Public Class TopNOptionsControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
        Private Sub AnimateChart(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class

    Public Class SelectedIndexToVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Try
                Dim index As Integer = Convert.ToInt32(value)
                Dim expectedIndex As Integer = Convert.ToInt32(parameter)
                If index = expectedIndex Then
                    Return Visibility.Visible
                End If
            Catch
            End Try
            Return Visibility.Collapsed
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
