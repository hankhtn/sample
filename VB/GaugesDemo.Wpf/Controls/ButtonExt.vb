Imports System.Windows
Imports System.Windows.Controls

Namespace GaugesDemo
    Public Class ButtonExt
        Inherits Button

        Public Property IsPressedExt() As Boolean
            Get
                Return DirectCast(GetValue(IsPressedExtProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsPressedExtProperty, value)
            End Set
        End Property
        Public Shared ReadOnly IsPressedExtProperty As DependencyProperty = DependencyProperty.Register("IsPressedExt", GetType(Boolean), GetType(ButtonExt), New PropertyMetadata(False))
        Protected Overrides Sub OnIsPressedChanged(ByVal e As DependencyPropertyChangedEventArgs)
            MyBase.OnIsPressedChanged(e)
            IsPressedExt = IsPressed
        End Sub
    End Class
End Namespace
