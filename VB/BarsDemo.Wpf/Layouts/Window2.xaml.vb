Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Docking
Imports System.Windows
Imports System.Windows.Controls

Namespace BarsDemo



    Partial Public Class AlignmentView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class AlignmentModel
        <BindableProperty(OnPropertyChangedMethodName := "OnChanged")>
        Public Overridable Property IsLeft() As Boolean
        <BindableProperty(OnPropertyChangedMethodName := "OnChanged")>
        Public Overridable Property IsRight() As Boolean
        <BindableProperty(OnPropertyChangedMethodName := "OnChanged")>
        Public Overridable Property IsCenter() As Boolean
        <BindableProperty(OnPropertyChangedMethodName := "OnChanged")>
        Public Overridable Property IsJustify() As Boolean
        Public Overridable Property Alignment() As TextAlignment

        Public Sub New()
            IsLeft = True
        End Sub

        Public Sub OnChanged()
            If IsLeft Then
                Alignment = TextAlignment.Left
            End If
            If IsRight Then
                Alignment = TextAlignment.Right
            End If
            If IsJustify Then
                Alignment = TextAlignment.Justify
            End If
            If IsCenter Then
                Alignment = TextAlignment.Center
            End If
        End Sub
    End Class
End Namespace
