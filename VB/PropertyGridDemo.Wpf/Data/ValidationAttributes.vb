Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Resources
Imports System.Security
Imports System.Text
Imports System.Text.RegularExpressions






Namespace DevExpress.DataAnnotations
    Public MustInherit Class RegexAttributeBase
        Inherits DataTypeAttribute

        Protected Const DefaultRegexOptions As RegexOptions = RegexOptions.Compiled Or RegexOptions.ExplicitCapture Or RegexOptions.IgnoreCase

        Private ReadOnly regex As Regex

        Public Sub New(ByVal regex As String, ByVal defaultErrorMessage As String, ByVal dataType As DataType)
            Me.New(New Regex(regex, DefaultRegexOptions), defaultErrorMessage, dataType)
        End Sub
        Public Sub New(ByVal regex As Regex, ByVal defaultErrorMessage As String, ByVal dataType As DataType)
            MyBase.New(dataType)
            Me.regex = CType(regex, Regex)
            Me.ErrorMessage = defaultErrorMessage
        End Sub
        Public NotOverridable Overrides Function IsValid(ByVal value As Object) As Boolean
            If value Is Nothing Then
                Return True
            End If
            Dim input As String = TryCast(value, String)
            Return input IsNot Nothing AndAlso regex.Match(input).Length > 0
        End Function
    End Class
    Public NotInheritable Class PhoneAttribute
        Inherits RegexAttributeBase

        Private Shared ReadOnly regex As New Regex("^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", DefaultRegexOptions)
        Private Const Message As String = "The {0} field is not a valid phone number."
        Public Sub New()
            MyBase.New(regex, Message, DataType.PhoneNumber)
        End Sub
    End Class
    Public NotInheritable Class EmailAddressAttribute
        Inherits RegexAttributeBase

        Private Shared ReadOnly regex As New Regex("^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", DefaultRegexOptions)
        Private Const Message As String = "The {0} field is not a valid e-mail address."
        Public Sub New()
            MyBase.New(regex, Message, DataType.EmailAddress)
        End Sub
    End Class
    Public NotInheritable Class UrlAttribute
        Inherits RegexAttributeBase

        Private Shared regex As New Regex("^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$", DefaultRegexOptions)
        Private Const Message As String = "The {0} field is not a valid fully-qualified http, https, or ftp URL."
        Public Sub New()
            MyBase.New(regex, Message, DataType.Url)
        End Sub
    End Class
    Public NotInheritable Class ZipCodeAttribute
        Inherits RegexAttributeBase

        Private Shared regex As New Regex("^[0-9][0-9][0-9][0-9][0-9]$", DefaultRegexOptions)
        Private Const Message As String = "The {0} field is not a valid ZIP code."
        Public Sub New()
            MyBase.New(regex, Message, DataType.Url)
        End Sub
    End Class
    Public NotInheritable Class CreditCardAttribute
        Inherits DataTypeAttribute

        Private Const Message As String = "The {0} field is not a valid credit card number."
        Public Sub New()
            MyBase.New(DataType.Custom)
            Me.ErrorMessage = Message
        End Sub
        Public Overrides Function IsValid(ByVal value As Object) As Boolean
            If value Is Nothing Then
                Return True
            End If
            Dim stringValue As String = TryCast(value, String)
            If stringValue Is Nothing Then
                Return False
            End If
            stringValue = stringValue.Replace("-", "").Replace(" ", "")
            Dim number As Integer = 0
            Dim oddEvenFlag As Boolean = False
            For Each ch As Char In stringValue.Reverse()
                If ch < "0"c OrElse ch > "9"c Then
                    Return False
                End If
                Dim digitValue As Integer = (AscW(ch) - AscW("0"c)) * (If(oddEvenFlag, 2, 1))
                oddEvenFlag = Not oddEvenFlag
                Do While digitValue > 0
                    number += digitValue Mod 10
                    digitValue = digitValue \ 10
                Loop
            Next ch
            Return (number Mod 10) = 0
        End Function
    End Class
End Namespace
