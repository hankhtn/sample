Imports DevExpress.XtraEditors.DXErrorProvider

Namespace GridDemo
    Public Class PersonInfo
        Inherits Object
        Implements IDXDataErrorInfo

        Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal address As String, ByVal phoneNumber As String, ByVal email As String)
            Me.FirstName = firstName
            Me.LastName = lastName
            Me.Address = address
            Me.PhoneNumber = phoneNumber
            Me.Email = email
        End Sub
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Property Address() As String
        Public Property PhoneNumber() As String
        Public Property Email() As String
        #Region "IDXDataErrorInfo Members"
        Private Sub IDXDataErrorInfo_GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetPropertyError
            Select Case propertyName
                Case "FirstName", "LastName"
                    If IsStringEmpty(If(propertyName = "FirstName", FirstName, LastName)) Then
                        SetErrorInfo(info, propertyName & " field can't be empty", ErrorType.Critical)
                    End If
                Case "Address"
                    If IsStringEmpty(Address) Then
                        SetErrorInfo(info, "Address hasn't been entered", ErrorType.Information)
                    End If
                Case "Email"
                    If IsStringEmpty(Email) Then
                        SetErrorInfo(info, "Email hasn't been entered", ErrorType.Information)
                    ElseIf Email <> "none" AndAlso (Not IsEmailCorrect(Email)) Then
                        SetErrorInfo(info, "Wrong email address", ErrorType.Warning)
                    End If
            End Select
        End Sub
        Private Sub IDXDataErrorInfo_GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
            If IsStringEmpty(PhoneNumber) AndAlso (Email = "none" OrElse (Not IsEmailCorrect(Email))) Then
                SetErrorInfo(info, "Either Phone Number or Email should be specified", ErrorType.Information)
            End If
        End Sub
        #End Region
        Private Function IsStringEmpty(ByVal str As String) As Boolean
            Return str Is Nothing OrElse str.Trim().Length = 0
        End Function
        Private Function IsEmailCorrect(ByVal email As String) As Boolean
            Return email Is Nothing OrElse (email.IndexOf("@") >= 1 AndAlso email.Length > email.IndexOf("@") + 1)
        End Function
        Private Sub SetErrorInfo(ByVal info As ErrorInfo, ByVal errorText As String, ByVal errorType As ErrorType)
            info.ErrorText = errorText
            info.ErrorType = errorType
        End Sub
    End Class
End Namespace
