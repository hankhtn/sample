Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.XtraSpellChecker.Native
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit.SpellChecker

Namespace SpellCheckerDemo
    Public Class SpellCheckerDemoModule
        Inherits DemoModule

        Protected Overridable ReadOnly Property CheckingElements() As List(Of FrameworkElement)
            Get
                Return Nothing
            End Get
        End Property

        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            If CheckingElements Is Nothing Then
                Return
            End If
            For Each element As FrameworkElement In CheckingElements
                element.Visibility = Visibility.Visible
            Next element
        End Sub
        Protected Overrides Sub HidePopupContent()
            If CheckingElements Is Nothing Then
                Return
            End If
            For Each element As FrameworkElement In CheckingElements
                element.Visibility = Visibility.Hidden
            Next element
            MyBase.HidePopupContent()
        End Sub
    End Class
End Namespace
