Imports System.Collections.Generic
Imports System.Windows

Namespace SpellCheckerDemo
    Partial Public Class TextEdit
        Inherits SpellCheckerDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides ReadOnly Property CheckingElements() As List(Of FrameworkElement)
            Get
                Return New List(Of FrameworkElement)() From {textEdit}
            End Get
        End Property
    End Class
End Namespace
