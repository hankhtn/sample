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

Namespace EditorsDemo



    Partial Public Class BindingValidation
        Inherits UserControl

        Private users As New List(Of String)()
        Public Sub New()
            InitializeComponent()
            users.Add("TestUser")
        End Sub

        Private Sub ConfirmMailValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            e.IsValid = Object.Equals(e.Value, txtMail.Text)
        End Sub
        Private Sub LoginValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If users.Contains(txtLogin.Text) Then
                e.IsValid = False
            End If
        End Sub

        Private Sub FirstNameValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            Dim value As String = CStr(e.Value)
            e.IsValid = Not String.IsNullOrEmpty(value)
        End Sub

        Private Sub SecondNameValidate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            Dim value As String = CStr(e.Value)
            e.IsValid = Not String.IsNullOrEmpty(value)
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            txtMail.DoValidate()
            users.Add(txtLogin.Text)
        End Sub
    End Class
End Namespace
