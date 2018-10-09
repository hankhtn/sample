Imports System.Windows

Namespace MapDemo
    Public Class HotelLabel
        Inherits VisibleControl

        Public Shared ReadOnly HotelInfoProperty As DependencyProperty = DependencyProperty.Register("HotelInfo", GetType(HotelInfo), GetType(HotelLabel), New PropertyMetadata(Nothing))

        Public Property HotelInfo() As HotelInfo
            Get
                Return DirectCast(GetValue(HotelInfoProperty), HotelInfo)
            End Get
            Set(ByVal value As HotelInfo)
                SetValue(HotelInfoProperty, value)
            End Set
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(HotelLabel)
        End Sub
    End Class
End Namespace
