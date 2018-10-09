Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Mvvm.UI.Interactivity

Namespace DevExpress.DevAV.Common.View
    Public Class DeleteMarginFromParentBorderBehavior
        Inherits Behavior(Of FrameworkElement)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            SearchBorderAndMarginReset(AssociatedObject)
        End Sub

        Private Sub SearchBorderAndMarginReset(ByVal obj As FrameworkElement)
            Do While obj IsNot Nothing AndAlso obj.Name <> "Border"
                obj = TryCast(VisualTreeHelper.GetParent(obj), FrameworkElement)
            Loop
            If obj Is Nothing Then
                Return
            End If
            obj.Margin = New Thickness(0)
        End Sub
    End Class
End Namespace
