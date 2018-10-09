Imports System
Imports System.Globalization
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Xml
Imports System.Xml.Linq

Namespace TreeMapDemo
    Public NotInheritable Class DataLoader

        Private Sub New()
        End Sub

        Private Shared Function GetStream(ByVal fileName As String) As Stream
            Dim uri As Uri = GetResourceUri(fileName)
            Return Application.GetResourceStream(uri).Stream
        End Function
        Public Shared Function GetResourceUri(ByVal fileName As String) As Uri
            fileName = "/TreeMapDemo;component" & fileName
            Return New Uri(fileName, UriKind.RelativeOrAbsolute)
        End Function
        Public Shared Function LoadXDocumentFromResources(ByVal fileName As String) As XDocument
            Try
                Return XDocument.Load(GetStream(fileName))
            Catch
                Return Nothing
            End Try
        End Function
        Public Shared Function LoadXmlDocumentFromResources(ByVal fileName As String) As XmlDocument
            Dim document As New XmlDocument()
            Try
                document.Load(GetStream(fileName))
                Return document
            Catch
                Return Nothing
            End Try
        End Function
    End Class

    Public Class LeafVerticalStackPanel
        Inherits Panel

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            For Each child As UIElement In Children
                child.Measure(availableSize)
            Next child
            Return availableSize
        End Function
        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            Dim freeRect As New Rect(New Point(0, 0), finalSize)
            For Each child As UIElement In Children
                Dim element As FrameworkElement = TryCast(child, FrameworkElement)
                If element IsNot Nothing Then
                    Dim size As Size = element.DesiredSize
                    Dim horzPosition As Double = If(element.HorizontalAlignment = HorizontalAlignment.Right, freeRect.Right - size.Width, freeRect.Left)
                    Dim vertPosition As Double = If(element.VerticalAlignment = VerticalAlignment.Bottom, freeRect.Bottom - size.Height, freeRect.Top)
                    Dim locationRect As New Rect(New Point(horzPosition, vertPosition), size)
                    If freeRect.Contains(locationRect) AndAlso freeRect.Height > locationRect.Height Then
                        element.Visibility = Visibility.Visible
                        element.Arrange(locationRect)
                        If element.VerticalAlignment = VerticalAlignment.Bottom Then
                            freeRect = New Rect(New Point(freeRect.Left, freeRect.Top), New Size(freeRect.Width, freeRect.Height - size.Height))
                        Else
                            freeRect = New Rect(New Point(freeRect.Left, freeRect.Top + size.Height), New Point(freeRect.Right, freeRect.Bottom))
                        End If
                    Else
                        element.Visibility = Visibility.Hidden
                        Exit For
                    End If
                End If
            Next child
            Return finalSize
        End Function
    End Class

    Public Class StringToImagePathConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim stringValue As String = TryCast(value, String)
            If stringValue IsNot Nothing Then
                Return DataLoader.GetResourceUri("/Images/" & stringValue & ".png")
            End If
            Return Nothing
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class CountToTextConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If System.Convert.ToInt32(value) = 1 Then
                Return value.ToString() & " medal"
            End If
            Return value.ToString() & " medals"
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class EnergyTypeToBrushConverter
        Implements IValueConverter

        Private Function GetColor(ByVal typeName As String) As Color
            Select Case typeName
                Case "Nuclear"
                    Return Color.FromRgb(&H9D, &H90, &HA0)
                Case "Oil"
                    Return Color.FromRgb(&H2A, &H5F, &H8E)
                Case "Natural Gas"
                    Return Color.FromRgb(&H62, &H9D, &HD1)
                Case "Hydro Electric"
                    Return Color.FromRgb(&H29, &H7F, &HD5)
                Case "Coal"
                    Return Color.FromRgb(&H4A, &H66, &HAC)
                Case Else
                    Return Colors.Transparent
            End Select
        End Function
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is String Then
                Return New SolidColorBrush(GetColor(DirectCast(value, String)))
            End If
            Return Nothing
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
End Namespace
