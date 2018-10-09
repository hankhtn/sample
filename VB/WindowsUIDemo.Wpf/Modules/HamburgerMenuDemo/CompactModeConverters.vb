Imports System
Imports System.Globalization
Imports System.IO
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Data

Namespace WindowsUIDemo
    Public Class OrderNameConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            If values.Length <> 2 Then
                Return Nothing
            End If
            If Not(TypeOf values(1) Is ColumnSortOrder) Then
                Return Nothing
            End If
            Dim order As ColumnSortOrder = DirectCast(values(1), ColumnSortOrder)
            If values(0).ToString() = "Received" Then
                Select Case order
                    Case ColumnSortOrder.Ascending
                        Return "Oldest"
                    Case ColumnSortOrder.Descending
                        Return "Newest"
                    Case Else
                        Return Nothing
                End Select
            End If
            Select Case order
                Case ColumnSortOrder.Ascending
                    Return "Ascending"
                Case ColumnSortOrder.Descending
                    Return "Descending"
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class MessageToFillConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim message = TryCast(value, Message)
            If message Is Nothing Then
                Return Nothing
            End If
            If message.Employee.Image Is Nothing Then
                Dim name As String = message.From
                If String.IsNullOrEmpty(name) Then
                    Return Colors.Black
                End If
                Dim color As Byte = CByte(Math.Abs(name.GetHashCode()) Mod 5)
                Select Case color
                    Case 0
                        Return New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FF31669C"), Color))
                    Case 1
                        Return New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FF598A7A"), Color))
                    Case 2
                        Return New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FFCCA464"), Color))
                    Case 3
                        Return New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FFE06B4C"), Color))
                    Case 4
                        Return New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FF558AC0"), Color))
                    Case Else
                        Return New SolidColorBrush(Colors.Black)
                End Select
            Else
                Dim imageData() As Byte = TryCast(message.Employee.Image, Byte())
                If imageData Is Nothing Then
                    Return Nothing
                End If
                Dim biImg As New BitmapImage()
                Dim ms As New MemoryStream(imageData)
                biImg.BeginInit()
                biImg.StreamSource = ms
                biImg.EndInit()
                Dim imgSrc As ImageSource = TryCast(biImg, ImageSource)

                Return New ImageBrush(imgSrc)
            End If
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class NameToInitialsConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim name As String = TryCast(value, String)
            If String.IsNullOrEmpty(name) Then
                Return "JJ"
            End If
            Dim idx As Integer = name.IndexOf(" "c)
            If idx < 0 OrElse name.Length <= idx + 1 OrElse name.Chars(idx + 1) = "("c Then
                Return name.Chars(0).ToString().ToUpper()
            End If
            Return (name.Chars(0).ToString() & name.Chars(idx + 1).ToString()).ToUpper()
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class PriorityGroupValueConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Integer Then
                Dim priority As Integer = DirectCast(value, Integer)
                Select Case priority
                    Case 0
                        Return "Low priority"
                    Case 1
                        Return "Normal priority"
                    Case 2
                        Return "Hight priority"
                    Case Else
                        Return Nothing
                End Select
            End If
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class AttachmentGroupValueConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean Then
                Dim attachment As Boolean = DirectCast(value, Boolean)
                Return If(attachment, "Has Attachment", "Hasn't Attachment")
            End If
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class ByteImageConverter
        Implements IValueConverter

        Public Property DecodePixelHeight() As Integer

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim imageData() As Byte = TryCast(value, Byte())
            If imageData Is Nothing Then
                Return Nothing
            End If
            Dim biImg As New BitmapImage()
            RenderOptions.SetBitmapScalingMode(biImg, BitmapScalingMode.HighQuality)
            Dim ms As New MemoryStream(imageData)
            biImg.BeginInit()
            biImg.DecodePixelHeight = DecodePixelHeight
            biImg.StreamSource = ms
            biImg.EndInit()
            Dim imgSrc As ImageSource = TryCast(biImg, ImageSource)
            Return imgSrc
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class NameToColorConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim name As String = TryCast(value, String)
            If String.IsNullOrEmpty(name) Then
                Return Colors.Black
            End If
            Dim color As Byte = CByte(Math.Abs(name.GetHashCode()) Mod 5)
            Select Case color
                Case 0
                    Return DirectCast(ColorConverter.ConvertFromString("#FF31669C"), Color)
                Case 1
                    Return DirectCast(ColorConverter.ConvertFromString("#FF598A7A"), Color)
                Case 2
                    Return DirectCast(ColorConverter.ConvertFromString("#FFCCA464"), Color)
                Case 3
                    Return DirectCast(ColorConverter.ConvertFromString("#FFE06B4C"), Color)
                Case 4
                    Return DirectCast(ColorConverter.ConvertFromString("#FF558AC0"), Color)
                Case Else
                    Return Colors.Black
            End Select
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace
