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
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo



    Partial Public Class PasswordStrengthControl
        Inherits UserControl

        Public Shared ReadOnly PasswordStrengthProperty As DependencyProperty
        Public Shared ReadOnly IsValidPasswordProperty As DependencyProperty
        Shared Sub New()
            Dim ownerType As Type = GetType(PasswordStrengthControl)
            PasswordStrengthProperty = DependencyProperty.Register("PasswordStrength", GetType(PasswordStrength), ownerType, New PropertyMetadata(PasswordStrength.Weak, AddressOf PasswordStrengthPropertyChanged))
            IsValidPasswordProperty = DependencyProperty.Register("IsValidPassword", GetType(Boolean), ownerType, New PropertyMetadata(False, AddressOf PasswordStrengthPropertyChanged))
        End Sub
        Private Shared Sub PasswordStrengthPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, PasswordStrengthControl).PasswordStrengthChanged()
        End Sub
        Private Shared Sub IsValidPasswordPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, PasswordStrengthControl).IsValidPasswordChanged()
        End Sub

        Private Property EnabledTemplate() As DataTemplate
        Private Property DisabledTemplate() As DataTemplate
        Private Property EmptyTemplate() As DataTemplate
        Public Property PasswordStrength() As PasswordStrength
            Get
                Return DirectCast(GetValue(PasswordStrengthProperty), PasswordStrength)
            End Get
            Set(ByVal value As PasswordStrength)
                SetValue(PasswordStrengthProperty, value)
            End Set
        End Property
        Public Property IsValidPassword() As Boolean
            Get
                Return DirectCast(GetValue(IsValidPasswordProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsValidPasswordProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf PasswordStrengthControl_Loaded
        End Sub
        Private Sub PasswordStrengthControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            EnabledTemplate = TryCast(ResourceHelper.FindResource(Me, "enabled"), DataTemplate)
            DisabledTemplate = TryCast(ResourceHelper.FindResource(Me, "disabled"), DataTemplate)
            EmptyTemplate = TryCast(ResourceHelper.FindResource(Me, "empty"), DataTemplate)
            Update()
        End Sub
        Private Sub PasswordStrengthChanged()
            Update()
        End Sub
        Private Sub IsValidPasswordChanged()
            Update()
        End Sub
        Private Sub Update()
            Dim enabled As DataTemplate = If(IsValidPassword, EnabledTemplate, DisabledTemplate)
            For i As Integer = 0 To 3
                editor.Buttons(i).Template = If(i < CInt((PasswordStrength)) + 1, enabled, EmptyTemplate)
            Next i
        End Sub
    End Class
End Namespace
