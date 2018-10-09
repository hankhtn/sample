Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Editors
Imports System.ComponentModel

Namespace EditorsDemo



    Partial Public Class BindingValidationModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New RegisterNewAccountWithDataErrorInfo()
        End Sub
        Private Sub Keyboard_OnGotKeyboardFocus(ByVal sender As Object, ByVal e As KeyboardFocusChangedEventArgs)
        End Sub
        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("Thank you!")
        End Sub

        Private Sub txtCardType_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim expression As BindingExpression = BindingOperations.GetBindingExpression(txtCardNumber, BaseEdit.EditValueProperty)
            If expression IsNot Nothing Then
                expression.UpdateTarget()
            End If
        End Sub
        Private Sub txtMail_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If txtConfirmMail Is Nothing Then
                Return
            End If
            Dim expression As BindingExpression = BindingOperations.GetBindingExpression(txtConfirmMail, BaseEdit.EditValueProperty)
            If expression IsNot Nothing Then
                expression.UpdateTarget()
            End If
        End Sub
        Private Sub txtConfirmMail_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If txtMail Is Nothing Then
                Return
            End If
            Dim expression As BindingExpression = BindingOperations.GetBindingExpression(txtMail, BaseEdit.EditValueProperty)
            If expression IsNot Nothing Then
                expression.UpdateTarget()
            End If
        End Sub

    End Class

    Public Class RegisterNewAccountWithDataErrorInfo
        Inherits RegisterNewAccount
        Implements IDataErrorInfo

        #Region "IDataErrorInfo Members"
    Private ReadOnly Property IDataErrorInfo_Error() As String Implements IDataErrorInfo.Error
        Get
            Return [Error]
        End Get
    End Property
    Public ReadOnly Property IDataErrorInfo_Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Select Case columnName
                Case "Login"
                    Return If(ValidateLogin(Login), String.Empty, [Error])
                Case "Mail"
                    Return If(ValidateMail(Mail, ConfirmMail), String.Empty, [Error])
                Case "ConfirmMail"
                    Return If(ValidateMail(Mail, ConfirmMail), String.Empty, [Error])
                Case "Age"
                    Return If(ValidateAge(Age), String.Empty, [Error])
                Case "CardType"
                    Return If(ValidateCardType(CardType), String.Empty, [Error])
                Case "CardExpDate"
                    Return If(ValidateCardExpDate(CardExpDate), String.Empty, [Error])
                Case "CardNumber"
                    Return If(ValidateCardNumber(CardType, CardNumber), String.Empty, [Error])
            End Select
            Return String.Empty
        End Get
    End Property
        #End Region
    End Class

    Public Class EmptyStringValidationRule
        Inherits ValidationRule

        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult
            Return New ValidationResult((Not String.IsNullOrEmpty(DirectCast(value, String))), "Empty")
        End Function
    End Class
End Namespace
