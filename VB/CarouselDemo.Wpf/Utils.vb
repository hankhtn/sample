Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Windows.Controls
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Xpf.Carousel
Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Effects

Namespace CarouselDemo
    Public Enum ItemType
        BinaryImage
        DrawingImage
    End Enum
    Public Class ContentLoadHelper
        Public Property Path() As String
        Public Sub New()
        End Sub
        Public Function LoadItems(ByVal it As ItemType) As List(Of FrameworkElement)
            Dim loadDelegate As LoadDelegate = Nothing
            Select Case it
                Case ItemType.BinaryImage
                    loadDelegate = AddressOf LoadImage
                Case ItemType.DrawingImage
                    loadDelegate = AddressOf LoadDrawingImage
            End Select
            Dim items = New List(Of FrameworkElement)()
            If loadDelegate IsNot Nothing Then
                For Each name In ResourcesTable.UriTable
                    If name.StartsWith(Path, StringComparison.OrdinalIgnoreCase) Then
                        Try
                            Dim resUri As Uri = ResourcesTable.GetUri(name)
                            Dim stream As Stream = Application.GetResourceStream(resUri).Stream
                            items.Add(loadDelegate(stream))
                        Catch
                        End Try
                    End If
                Next name
            End If
            Return items
        End Function
        Public Delegate Function LoadDelegate(ByVal stream As Stream) As FrameworkElement
        Public Function LoadDrawingImage(ByVal stream As Stream) As Image
            Dim rd = DirectCast(XamlReader.Load(stream), ResourceDictionary)
            Dim di = DirectCast(rd("Layer_1"), DrawingImage)
            Return New Image() With {.Source = di}
        End Function
        Public Function LoadImage(ByVal stream As Stream) As Image
            Dim image = New Image()
            image.Source = BitmapFrame.Create(stream)
            Return image
        End Function
    End Class
    Public Class ImageContainer
        Public Property Name() As String
        Public Property Source() As ImageSource
    End Class
    Public Class PathContainer
        Public Property Name() As String
        Public Property Path() As PathGeometry
    End Class
    Public Class FunctionContainer
        Public Property Name() As String
        Public Property FunctionBase() As FunctionBase
    End Class
    Public Class BitmapEffectContainer
        Public Property Name() As String
        Public Property BitmapEffect() As BitmapEffect
    End Class
    Public Class LanguageContainer
        Public Property Name() As String
        Public Property FlagImageSource() As String
        Public Property Phrase() As String
    End Class
    Public Class PhraseConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        #Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim cc = DirectCast(value, ContentControl)
            If cc IsNot Nothing Then
                Dim lc = DirectCast(cc.Content, LanguageContainer)
                Return lc.Phrase
            End If
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function

        #End Region
    End Class
    Public Class LanguageContainerConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        #Region "IValueConverter Members"
        Public Shared Function LoadFromResources(ByVal path As String, ByVal asm As System.Reflection.Assembly) As Image
            Try
                Dim resUri As Uri = ResourcesTable.GetUri(path)
                Dim stream As Stream = Application.GetResourceStream(resUri).Stream
                Dim rd = DirectCast(XamlReader.Load(stream), ResourceDictionary)
                Return New Image() With {.Source = DirectCast(rd("Layer_1"), DrawingImage), .Stretch = Stretch.Fill, .UseLayoutRounding = True}
            Catch
            End Try
            Return New Image()
        End Function
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim fileName = DirectCast(value, String)
            Return LoadFromResources("/CarouselDemo;component/Data/Images/Flags/" & fileName, System.Reflection.Assembly.GetExecutingAssembly())
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function

        #End Region
    End Class
    Public Class FunctionContainerConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        #Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim list As IList = DirectCast(parameter, IList)
            For Each item As Object In list
                Dim container As FunctionContainer = TryCast(item, FunctionContainer)
                If container IsNot Nothing AndAlso container.FunctionBase Is value Then
                    Return container
                End If
            Next item
            Return list(0)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DirectCast(value, FunctionContainer).FunctionBase
        End Function

        #End Region
    End Class
    Public Class BitmapEffectContainerConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        #Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return DirectCast(parameter, IList)(0)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DirectCast(value, FunctionContainer).FunctionBase
        End Function

        #End Region
    End Class
    Public Class DoubleToVisibleItemCountConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        #Region "IValueConverter Members"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim doubleValue As Double = DirectCast(value, Double)
            Dim returnValue As Integer = CInt((doubleValue))
            returnValue *= 2
            Return returnValue + 1
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If targetType IsNot GetType(Double) Then
                Throw New InvalidOperationException()
            End If
            Return CDbl(((DirectCast(value, Integer)) - 1) \ 2)
        End Function
        #End Region
    End Class
    Public Class VisibleItemCountToActiveElementIndexConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        #Region "IValueConverter Members"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If targetType IsNot GetType(Integer) Then
                Throw New InvalidOperationException()
            End If
            Return (((DirectCast(value, Integer)) - 1) \ 2)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
    Public Class DoubleToIntConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        #Region "IValueConverter Members"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If targetType IsNot GetType(Integer) Then
                Throw New InvalidOperationException()
            End If
            Return CInt((DirectCast(value, Double)))
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
    Public Class PathSizingModeConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        #Region "IValueConverter Members"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return Object.Equals(value, PathSizingMode.Stretch)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If DirectCast(value, Boolean) Then
                Return PathSizingMode.Stretch
            End If
            Return PathSizingMode.Proportional
        End Function
        #End Region
    End Class
    Public Class SizeTransformConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return Me
        End Function
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            'if ((string)parameter == "Width")
            Return 200R * DirectCast(value, Double)
            'else
            '    return 150d * (double)value;
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class
End Namespace
