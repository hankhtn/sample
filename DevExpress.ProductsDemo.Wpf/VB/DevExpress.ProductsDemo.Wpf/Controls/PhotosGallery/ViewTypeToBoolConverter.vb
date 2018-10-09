Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Data

Namespace ProductsDemo
    Public Class ViewTypeToBoolConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Boolean) AndAlso TypeOf value Is PhotoGalleryViewType AndAlso TypeOf parameter Is PhotoGalleryViewType Then
                Return DirectCast(value, PhotoGalleryViewType) = DirectCast(parameter, PhotoGalleryViewType)
            End If
            Return False
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Boolean Then
                If targetType Is GetType(PhotoGalleryViewType) Then
                    Return If(DirectCast(value, Boolean), PhotoGalleryViewType.Gallery, PhotoGalleryViewType.Map)
                End If
            End If
            Return Nothing
        End Function
    End Class
    Public Class ViewTypeToVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Visibility) AndAlso TypeOf value Is PhotoGalleryViewType AndAlso TypeOf parameter Is PhotoGalleryViewType Then
                Return If(DirectCast(value, PhotoGalleryViewType) = DirectCast(parameter, PhotoGalleryViewType), Visibility.Visible, Visibility.Hidden)
            End If
            Return Visibility.Hidden
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Visibility Then
                If targetType Is GetType(PhotoGalleryViewType) Then
                    Return If(DirectCast(value, Visibility) = Visibility.Visible, PhotoGalleryViewType.Gallery, PhotoGalleryViewType.Map)
                End If
            End If
            Return Nothing
        End Function
    End Class
    Public Enum PhotoGalleryViewType
        Map
        Gallery
        Detail
    End Enum
End Namespace
