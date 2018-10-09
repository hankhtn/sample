Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Utils
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls

Namespace GridDemo
    Public Class GridSummaryList
        Inherits List(Of GridSummaryItem)

    End Class
    Public Class NameTextControl
        Inherits Control

        Public Shared ReadOnly NameValueProperty As DependencyProperty = DependencyProperty.Register("NameValue", GetType(String), GetType(NameTextControl), New PropertyMetadata(Nothing))
        Public Shared ReadOnly TextValueProperty As DependencyProperty = DependencyProperty.Register("TextValue", GetType(String), GetType(NameTextControl), New PropertyMetadata(Nothing))
        Public Property NameValue() As String
            Get
                Return DirectCast(GetValue(NameValueProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(NameValueProperty, value)
            End Set
        End Property
        Public Property TextValue() As String
            Get
                Return DirectCast(GetValue(TextValueProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(TextValueProperty, value)
            End Set
        End Property
        Public Sub New()
            Me.SetDefaultStyleKey(GetType(NameTextControl))
        End Sub
    End Class
    Public Class HintControl
        Inherits ContentControl

        Public Sub New()
            Me.SetDefaultStyleKey(GetType(HintControl))
        End Sub
    End Class
End Namespace
