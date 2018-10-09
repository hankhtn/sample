Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace EditorsDemo
    Partial Public Class RatingEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class RatingEditViewModel
        Public Overridable Property Background() As Color
        Public Overridable Property HoverBackground() As Color
        Public Overridable Property SelectedBackground() As Color
        Public Overridable Property Cars() As List(Of CarRating)
        Public Overridable Property SelectedCar() As CarRating
        Public Sub New()
            Background = Colors.Transparent
            HoverBackground = Colors.Transparent
            SelectedBackground = Colors.Transparent
            CreateCarsSource()
            SelectedCar = Cars(0)
        End Sub

        Private Sub CreateCarsSource()
            Dim carsSource = CarsData.DataSource

            Dim cars_Renamed As New List(Of CarRating)()
            Dim rand As New Random()
            For Each car In carsSource
                cars_Renamed.Add(New CarRating() With {.Car = car, .SpeedRating = rand.Next(5, 10), .ComfortRating = rand.Next(5, 10), .QualityRating = rand.Next(5, 10), .PriceRating = rand.Next(5, 10)})
            Next car
            Cars = cars_Renamed
        End Sub
    End Class
    Public Class ColorDisplayTextConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If DirectCast(value, Color) = Colors.Transparent Then
                Return "Automatic"
            End If
            Return Nothing
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return New ColorDisplayTextConverter()
        End Function
    End Class
    Public Class IsNullColorConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return (DirectCast(value, Color)) = Colors.Transparent
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return New IsNullColorConverter()
        End Function
    End Class
    Public Class ColorToBrushConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value Is Nothing, Nothing, New SolidColorBrush(DirectCast(value, Color)))
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return New ColorToBrushConverter()
        End Function
    End Class
    Public Class CarRating
        Public Property Car() As Cars
        Public Property SpeedRating() As Double
        Public Property ComfortRating() As Double
        Public Property QualityRating() As Double
        Public Property PriceRating() As Double
    End Class
End Namespace
