Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Markup
Imports System.Windows.Data

Namespace PdfViewerDemo.Utils
    Public Class FormatWrapper
        Public Sub New()
        End Sub
        Public Sub New(ByVal name As String, ByVal format As String)
            FormatName = name
            FormatString = format
        End Sub
        Public Property FormatName() As String
        Public Property FormatString() As String
    End Class
    Public Class BaseKindHelper(Of T)
        Public Function GetEnumMemberList() As Array
            Return System.Enum.GetValues(GetType(T))
        End Function
    End Class
    Public Class ClickModeKindHelper
        Inherits BaseKindHelper(Of ClickMode)

    End Class
    Public Class TextWrappingKindHelper
        Inherits BaseKindHelper(Of TextWrapping)

    End Class
    Public Class ScrollBarVisibilityKindHelper
        Inherits BaseKindHelper(Of ScrollBarVisibility)

    End Class
    Public Class CharacterCasingKindHelper
        Inherits BaseKindHelper(Of CharacterCasing)

    End Class
    Public Class NullableToStringConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        Private nullString As String = "Null"
        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return nullString
            End If
            Return value.ToString()
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
    Public Class DecimalToConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = TryCast(parameter, Type)
            If target Is Nothing Then
                Return value
            End If
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return System.Convert.ToDecimal(value)
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class IConvertibleConverter
        Implements IValueConverter

        Public Property ToType() As String
        Public Property FromType() As String

        #Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = Type.GetType(ToType, False)
            If target Is Nothing Then
                Return value
            End If
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim target As Type = Type.GetType(FromType, False)
            If target Is Nothing Then
                Return value
            End If
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        #End Region
    End Class

End Namespace
