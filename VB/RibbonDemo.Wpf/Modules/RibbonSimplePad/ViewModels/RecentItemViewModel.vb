Namespace RibbonDemo
    Public Class RecentItemViewModel
        Public Property Caption() As String
        Public Property Description() As String
        Public Property Glyph() As String
        Public Sub New()
        End Sub
        Public Sub New(ByVal caption As String, ByVal description As String, ByVal glyph As String)
            Me.Caption = caption
            Me.Description = description
            Me.Glyph = glyph
        End Sub
    End Class
End Namespace
