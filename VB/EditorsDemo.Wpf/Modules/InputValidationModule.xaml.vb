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
Imports DevExpress.Xpf.Editors.Validation
Imports DevExpress.Xpf.Core
Imports DevExpress.Mvvm

Namespace EditorsDemo



    Partial Public Class InputValidationModule
        Inherits EditorsDemoModule

        Private Property Account() As RegisterNewAccount
        Public Sub New()
            Account = New RegisterNewAccount()
            InitializeComponent()
            InitSources()
            DataContext = Account
            SetJoinButtonBindings()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub
        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            txtLogin.Focus()
            SetSettingsContext(txtLogin)
        End Sub
        Private Sub SetJoinButtonBindings()
            Dim binding As New Binding() With {.Path = New PropertyPath(ValidationService.HasValidationErrorProperty)}
            binding.Source = validationContainer
            binding.Converter = New NegationConverterExtension()
            btnJoin.SetBinding(IsEnabledProperty, binding)
        End Sub
        Private Sub InitSources()
            txtCardType.ItemsSource = RegisterNewAccount.Cards
        End Sub
        Private Sub ConfirmMailValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            Dim [error] As String = String.Empty
            e.IsValid = Account.ValidateMail(TryCast(txtMail.EditValue, String), CStr(e.Value))
            e.ErrorContent = Account.Error
        End Sub
        Protected Sub LoginValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            e.IsValid = Account.ValidateLogin(CStr(e.Value))
            e.ErrorContent = Account.Error
        End Sub
        Private Sub FirstNameValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            e.IsValid = Account.ValidateName(CStr(e.Value))
            e.ErrorContent = Account.Error
        End Sub
        Private Sub SecondNameValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            e.IsValid = Account.ValidateName(CStr(e.Value))
            e.ErrorContent = Account.Error
        End Sub
        Private Sub txtCardType_Validate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            e.IsValid = Account.ValidateCardType(CStr(e.Value))
            e.ErrorContent = Account.Error
        End Sub
        Private Sub txtCardNumber_Validate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            e.IsValid = Account.ValidateCardNumber(TryCast(txtCardType.EditValue, String), CStr(e.Value))
            e.ErrorContent = Account.Error
        End Sub
        Private Sub txtCardType_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If txtCardNumber IsNot Nothing Then
                txtCardNumber.DoValidate()
            End If
        End Sub
        Private Sub txtCardExpDate_Validate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            e.IsValid = If(e.Value IsNot Nothing, Account.ValidateCardExpDate(CDate(e.Value)), False)
            e.ErrorContent = Account.Error
        End Sub
        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("Thank you!")
        End Sub
        Private Sub txtMail_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If txtConfirmMail IsNot Nothing Then
                txtConfirmMail.DoValidate()
            End If
        End Sub
        Private Sub TextEdit_GotFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetSettingsContext(sender)
        End Sub
        Private Sub SetSettingsContext(ByVal context As Object)
            settings.DataContext = context
        End Sub
    End Class
    Public Class InvalidValueBehaviorHelper
        Inherits DependencyObject

        Public Shared ReadOnly InvalidValueBehaviorProperty As DependencyProperty

        Shared Sub New()
            InvalidValueBehaviorProperty = DependencyProperty.RegisterAttached("InvalidValueBehavior", GetType(InvalidValueBehavior), GetType(InvalidValueBehaviorHelper), New PropertyMetadata(InvalidValueBehavior.AllowLeaveEditor, Nothing))
        End Sub

        Public Shared Function GetInvalidValueBehavior(ByVal d As DependencyObject) As InvalidValueBehavior
            Return DirectCast(d.GetValue(InvalidValueBehaviorProperty), InvalidValueBehavior)
        End Function
        Public Shared Sub SetInvalidValueBehavior(ByVal d As DependencyObject, ByVal value As InvalidValueBehavior)
            d.SetValue(InvalidValueBehaviorProperty, value)
        End Sub
    End Class
    Public Class CardInfo
        Public Property Name() As String
        Public Property DisplayName() As String
    End Class
    Public Class RegisterNewAccount
        Inherits BindableBase

        Public Shared ReadOnly Cards As New List(Of CardInfo)()
        Shared Sub New()
            Cards.Add(New CardInfo() With {.Name = "VISA", .DisplayName = "VISA (13 digits)"})
            Cards.Add(New CardInfo() With {.Name = "Master Card", .DisplayName = "Master Card (16 digits)"})
            Cards.Add(New CardInfo() With {.Name = "American Express", .DisplayName = "American Express (15 digits)"})
            Cards.Add(New CardInfo() With {.Name = "Diners Club", .DisplayName = "Diners Club (13 digits)"})
        End Sub
        Public Sub New()
            Login = "TestUser"
            Mail = "testmail@devexpress.com"
            FirstName = "John"
            LastName = "Joe"
            CardType = "VISA"
        End Sub


        Private error_Renamed As String

        Private cardExpDate_Renamed As Date = Date.Today.AddMonths(-3)

        Public ReadOnly Property [Error]() As String
            Get
                Return error_Renamed
            End Get
        End Property

        Public Property Login() As String
            Get
                Return GetProperty(Function() Login)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() Login, value)
            End Set
        End Property
        Public Property Mail() As String
            Get
                Return GetProperty(Function() Mail)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() Mail, value)
            End Set
        End Property
        Public Property ConfirmMail() As String
            Get
                Return GetProperty(Function() ConfirmMail)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() ConfirmMail, value)
            End Set
        End Property
        Public Property FirstName() As String
            Get
                Return GetProperty(Function() FirstName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() FirstName, value)
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return GetProperty(Function() LastName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() LastName, value)
            End Set
        End Property
        Public Property Age() As Decimal
            Get
                Return GetProperty(Function() Age)
            End Get
            Set(ByVal value As Decimal)
                SetProperty(Function() Age, value)
            End Set
        End Property
        Public Property CardType() As String
            Get
                Return GetProperty(Function() CardType)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() CardType, value)
            End Set
        End Property
        Public Property CardNumber() As String
            Get
                Return GetProperty(Function() CardNumber)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() CardNumber, value)
            End Set
        End Property
        Public Property CardExpDate() As Date
            Get
                Return GetProperty(Function() CardExpDate)
            End Get
            Set(ByVal value As Date)
                SetProperty(Function() CardExpDate, value)
            End Set
        End Property

        Private Sub SetError(ByVal isValid As Boolean, ByVal errorString As String)
            If isValid Then
                error_Renamed = String.Empty
            Else
                error_Renamed = errorString
            End If
        End Sub

        Public Function ValidateName(ByVal name As String) As Boolean
            Dim isValid As Boolean = Not String.IsNullOrEmpty(name)
            SetError(isValid, "Name can't be empty")
            Return isValid
        End Function
        Public Function ValidateMail(ByVal mail As String, ByVal confirmMail As String) As Boolean
            Dim isValid As Boolean = Object.Equals(mail, confirmMail)
            SetError(isValid, "Two specified e-mail addresses are different")
            Return isValid
        End Function
        Public Function ValidateCardNumber(ByVal type As String, ByVal number As String) As Boolean
            Dim isValid As Boolean = False
            Select Case type
                Case "VISA"
                    isValid = ValidateVISA(number)
                Case "Master Card"
                    isValid = ValidateMasterCard(number)
                Case "American Express"
                    isValid = ValidateAmericanExpress(number)
                Case "Diners Club"
                    isValid = ValidateDinersClub(number)
                Case Else
                    isValid = False
            End Select
            SetError(isValid, "Invalid number")
            Return isValid
        End Function
        Public Function ValidateLogin(ByVal login As String) As Boolean
            Dim isValid As Boolean = login <> "TestUser"
            SetError(isValid, "A user with this name is already registered")
            Return isValid
        End Function
        Public Function ValidateAge(ByVal age As Decimal) As Boolean
            Dim isValid As Boolean = age > 21 AndAlso age < 200
            If age < 21 Then
                SetError(isValid, "Sorry, you're too young to visit our site!")
                Return False
            ElseIf age > 200 Then
                SetError(isValid, "Congratulations! You're the oldest man on Earth!")
                Return False
            End If
            Return True
        End Function
        Public Function ValidateCardType(ByVal type As String) As Boolean
            Dim isValid As Boolean = type = "American Express" OrElse type = "VISA"
            SetError(isValid, "Sorry, cards of this type are not accepted")
            Return isValid
        End Function
        Private Function ValidateVISA(ByVal num As String) As Boolean
            Return (Not String.IsNullOrEmpty(num)) AndAlso num.Length = 13
        End Function
        Private Function ValidateMasterCard(ByVal num As String) As Boolean
            Return (Not String.IsNullOrEmpty(num)) AndAlso num.Length = 16
        End Function
        Private Function ValidateAmericanExpress(ByVal num As String) As Boolean
            Return (Not String.IsNullOrEmpty(num)) AndAlso num.Length = 15
        End Function
        Private Function ValidateDinersClub(ByVal num As String) As Boolean
            Return (Not String.IsNullOrEmpty(num)) AndAlso num.Length = 14
        End Function
        Public Function ValidateCardExpDate(ByVal expDate As Date) As Boolean
            Dim isValid As Boolean = expDate.CompareTo(Date.Today) > 0
            SetError(isValid, "Sorry, your card has expired")
            Return isValid
        End Function
    End Class
End Namespace
