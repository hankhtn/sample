Namespace MVVMDemo.DXBindingDemo
    Public NotInheritable Class RegistrationHelper

        Private Sub New()
        End Sub

        Public Shared Function CanRegister(ByVal userName As String, ByVal acceptTerms As Boolean) As Boolean
            Return (Not String.IsNullOrEmpty(userName)) AndAlso acceptTerms
        End Function
    End Class
End Namespace
